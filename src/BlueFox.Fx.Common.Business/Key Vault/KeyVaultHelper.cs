//-----------------------------------------------------------------------
// <copyright file="KeyVaultHelper.cs" company="BlueFox">
// Copyright (c) BlueFox. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlueFox.Fx;

using System;
using System.Collections.Generic;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

public class KeyVaultHelper : IKeyVaultHelper
{
    private static Dictionary<string, KeyVaultCache> cache = new Dictionary<string, KeyVaultCache>();

    private SecretClient? client;

    public void SetKeyvaultConnection(DefaultAzureCredential credentials, SecretClientOptions options, string connectionString)
    {
        this.client = new SecretClient(
                                        new Uri(connectionString),
                                        credentials,
                                        options);
    }

    public string GetSecret(string secretName, double ttl = 30)
    {
        string secret;

        if (cache.ContainsKey(secretName))
        {
            // Key is in the cache
            cache.TryGetValue(secretName, out KeyVaultCache cachedValue);

            if (cachedValue?.EndDate < DateTime.Now)
            {
                // Key is expired
                cache.Remove(secretName);

                secret = this.client.GetSecret(secretName).Value.Value;

                cache.Add(secretName, new KeyVaultCache(secret, ttl));
            }
            else
            {
                // Key is not expired yet
                secret = cachedValue!.Value;
            }
        }
        else
        {
            // Key is not in the cache
            secret = this.client.GetSecret(secretName).Value.Value;

            cache.Add(secretName, new KeyVaultCache(secret, ttl));
        }

        return secret;
    }
}