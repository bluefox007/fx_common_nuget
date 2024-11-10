//-----------------------------------------------------------------------
// <copyright file="KeyVaultHelper.cs" company="BlueFox">
// Copyright (c) BlueFox. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlueFox.Fx;

using System.Reflection;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

public static class KeyVaultHelper
{
    private static SecretClient client;

    public static void SetKeyVaultUri(string keyVaultUri, string environment)
    {
        if (client is null)
        {
            SecretClientOptions options = new SecretClientOptions()
            {
                Retry =
                        {
                          Delay = TimeSpan.FromSeconds(2),
                          MaxDelay = TimeSpan.FromSeconds(16),
                          MaxRetries = 5,
                          Mode = RetryMode.Exponential,
                        },
            };

            client = new SecretClient(
                new Uri(keyVaultUri),
                new DefaultAzureCredential(
                new DefaultAzureCredentialOptions()
                //{
                //    ExcludeEnvironmentCredential = true,
                //    ExcludeInteractiveBrowserCredential = true,
                //    ExcludeAzurePowerShellCredential = true,
                //    ExcludeSharedTokenCacheCredential = true,
                //    ExcludeVisualStudioCodeCredential = true,
                //    ExcludeVisualStudioCredential = true,
                //    ExcludeAzureCliCredential = !environment.Equals("development", StringComparison.OrdinalIgnoreCase),
                //    ExcludeManagedIdentityCredential = environment.Equals("development", StringComparison.OrdinalIgnoreCase),
                //}
                ), options);
        }
    }

    public static void SetSecret(string internalKey, string secretKey, int? cachingTimeInSeconds)
    {
        CachingHelper.AddCache(
                            internalKey,
                            cachingTimeInSeconds != null ? DateTime.UtcNow.AddSeconds((double)cachingTimeInSeconds) : null,
                            GetSecret(secretKey));
    }

    public static string GetSecret(string key)
    {
        if (client is null)
        {
            throw new NullReferenceException("Key vault is not set");
        }

        return client.GetSecret(key).Value.Value;
    }
}