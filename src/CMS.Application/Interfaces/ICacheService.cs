using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Application.Services;

public interface ICacheService
{
    T? Get<T>(string key);
    void Set<T>(string key, T value);
    void Set<T>(string key, T value, TimeSpan expirationTime);
    void Remove(string key);
    bool TryGetValue<T>(string key, out T? value);
}