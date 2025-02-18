//-----------------------------------------------------------------------
// <copyright file="KeyVaultProxy.cs" company="BlueFox">
// Copyright (c) BlueFox. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlueFox.Fx;

using System;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;

public class KeyVaultProxy : HttpPipelinePolicy, IDisposable
{
    private readonly Cache cache;

    public KeyVaultProxy(TimeSpan? ttl = null)
    {
        ttl ??= TimeSpan.FromHours(1);
        if (ttl < TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(ttl));
        }

        this.Ttl = ttl.Value;
        this.cache = new Cache();
    }

    public TimeSpan Ttl { get; internal set; }

    public void Clear() => this.cache.Clear();

    public override void Process(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline) =>

    this.ProcessAsync(false, message, pipeline).AsTask().GetAwaiter().GetResult();

    public override async ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline) =>
        await this.ProcessAsync(true, message, pipeline).ConfigureAwait(false);

    internal static bool IsSupported(string uri)
    {
        // Find the beginning of the path component after the scheme.
        int pos = uri.IndexOf('/', 8);

        if (pos > 0)
        {
            uri = uri.Substring(pos);
            return uri.StartsWith("/secrets/", StringComparison.OrdinalIgnoreCase)
                || uri.StartsWith("/keys/", StringComparison.OrdinalIgnoreCase)
                || uri.StartsWith("/certificates/", StringComparison.OrdinalIgnoreCase);
        }

        return false;
    }

    private static async ValueTask ProcessNextAsync(bool isAsync, HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
    {
        if (isAsync)
        {
            await ProcessNextAsync(message, pipeline).ConfigureAwait(false);
        }
        else
        {
            ProcessNext(message, pipeline);
        }
    }

    private async ValueTask ProcessAsync(bool isAsync, HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
    {
        Request request = message.Request;

        if (request.Method == RequestMethod.Get)
        {
            string uri = request.Uri.ToUri().GetLeftPart(UriPartial.Path);

            if (IsSupported(uri))
            {
                message.Response = await this.cache.GetOrAddAsync(isAsync, uri, this.Ttl, async () =>
                {
                    await ProcessNextAsync(isAsync, message, pipeline).ConfigureAwait(false);
                    return message.Response;
                }).ConfigureAwait(false);

                return;
            }
        }

        await ProcessNextAsync(isAsync, message, pipeline).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    void IDisposable.Dispose()
    {
        this.cache.Dispose();
        GC.SuppressFinalize(this);
    }
}