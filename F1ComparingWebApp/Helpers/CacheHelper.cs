using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1ComparingWebApp.Helpers
{
    public class CacheHelper
    {
        private IMemoryCache _cache;

        public CacheHelper(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T CachedResult<T>(string key, Func<T> valueRetrieval, TimeSpan cachingTime)
        {
            if (_cache.TryGetValue<T>(key, out T cachedItem))
            {
                return cachedItem;
            }
            var cachingValue = valueRetrieval();

            _cache.Set(key, cachingValue, cachingTime);

            return cachingValue;
        }

        public async Task<T> CachedResultAsync<T>(string key, Func<Task<T>> valueRetrieval, TimeSpan cachingTime)
        {
            if (_cache.TryGetValue<T>(key, out T cachedItem))
            {
                return cachedItem;
            }
            var cachingValue = await valueRetrieval();

            _cache.Set(key, cachingValue, cachingTime);

            return cachingValue;
        }
    }
}
