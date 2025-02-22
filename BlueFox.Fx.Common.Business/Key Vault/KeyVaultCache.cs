//-----------------------------------------------------------------------
// <copyright file="KeyVaultCache.cs" company="BlueFox">
// Copyright (c) BlueFox. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlueFox.Fx;

using System;

public class KeyVaultCache
{
    public KeyVaultCache(string value, double ttl)
    {
        this.Value = value;
        this.EndDate = DateTime.Now.AddSeconds(ttl);
    }

    public DateTime EndDate { get; private set; }

    public string Value { get; private set; }
}