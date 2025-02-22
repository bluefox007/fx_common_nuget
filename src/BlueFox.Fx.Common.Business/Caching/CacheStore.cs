//-----------------------------------------------------------------------
// <copyright file="CacheStore.cs" company="BlueFox">
// Copyright (c) BlueFox. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlueFox.Fx.Common.Business.Caching;

public static class CacheStore
{
    public static void AddCache(string secret, string secretKey, int? cachingTime)
    {
        CachingHelper.AddCache(
                                secretKey,
                                cachingTime != null ? DateTime.UtcNow.AddSeconds((double)cachingTime) : null,
                                secret);
    }

    public static (object? Value, bool IsExpired) GetCache(string secretKey)
    {
        return CachingHelper.GetCurrentCache(secretKey);
    }
}