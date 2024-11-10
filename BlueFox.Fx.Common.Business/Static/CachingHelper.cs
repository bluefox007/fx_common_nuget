//-----------------------------------------------------------------------
// <copyright file="CachingHelper.cs" company="BlueFox">
// Copyright (c) BlueFox. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlueFox.Fx;

using System;
using System.Collections.Generic;

internal static class CachingHelper
{
    private static readonly Dictionary<string, Cache> _cache = new Dictionary<string, Cache>();

    public static object AddCache(string key, DateTime? expirationDate, string value)
    {
        // If the item exists we'll remove it and replace it with a new one.
        if (_cache.ContainsKey(key))
        {
            _cache.Remove(key);
        }

        // Add the new key and value
        var cacheValue = new Cache()
        {
            ExpirationDate = expirationDate,
            Value = value,
        };

        _cache.Add(key, cacheValue);

        return cacheValue.Value;
    }

    public static (object? Value, bool IsExpired) GetCurrentCache(string key)
    {
        Cache cache;

        _cache.TryGetValue(key, out cache);

        if (cache is null)
        {
            return (null, false);
        }
        else if (cache.IsExpired)
        {
            // The cache is expired, remove the item
            _cache.Remove(key);

            return (null, true);
        }

        return (cache.Value, false);
    }
}

public class Cache
{
    // If null there is no expiration
    public DateTime? ExpirationDate { get; set; }

    public bool IsExpired
    {
        get
        {
            if (this.ExpirationDate.HasValue)
            {
                if (this.ExpirationDate <= DateTime.UtcNow)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }

    public object Value { get; set; }
}