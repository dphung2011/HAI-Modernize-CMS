using CMS.Application.DTOs;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Application.Services;

public class CacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;
    private readonly TimeSpan _defaultCacheTime = TimeSpan.FromMinutes(10);

    public CacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public T? Get<T>(string key)
    {
        _memoryCache.TryGetValue(key, out T? value);
        return value;
    }

    public void Set<T>(string key, T value)
    {
        Set(key, value, _defaultCacheTime);
    }

    public void Set<T>(string key, T value, TimeSpan expirationTime)
    {
        _memoryCache.Set(key, value, expirationTime);
    }

    public void Remove(string key)
    {
        _memoryCache.Remove(key);
    }

    public bool TryGetValue<T>(string key, out T? value)
    {
        return _memoryCache.TryGetValue(key, out value);
    }
}