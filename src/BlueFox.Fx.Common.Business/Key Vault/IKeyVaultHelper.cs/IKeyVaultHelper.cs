//-----------------------------------------------------------------------
// <copyright file="IKeyVaultHelper.cs" company="BlueFox">
// Copyright (c) BlueFox. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlueFox.Fx;

using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

/// <summary>
/// Helper to connect to the KeyVault.
/// </summary>
public interface IKeyVaultHelper
{
    string GetSecret(string secretName, double ttl = 30);

    public void SetKeyvaultConnection(DefaultAzureCredential credentials, SecretClientOptions options, string connectionString);
}
