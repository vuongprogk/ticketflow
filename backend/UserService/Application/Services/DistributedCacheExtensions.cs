using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Services
{
    public static class DistributedCacheExtensions
    {
        private static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = null,
            WriteIndented = true,
            AllowTrailingCommas = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        public static Task SetAsync<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options)
        {
            var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, jsonSerializerOptions));
            return cache.SetAsync(key, bytes, options);
        }
        public static bool TryGetValue<T>(this IDistributedCache cache, string key, out T? value)
        {
            var val = cache.Get(key);
            value = default;
            if (val is null) return false;
            value = JsonSerializer.Deserialize<T>(val, jsonSerializerOptions);
            return true;
        }
        public static async Task<T?> GetOrSetAsync<T>(this IDistributedCache cache, string key, Func<Task<T>> task, DistributedCacheEntryOptions? options = null)
        {
            options ??= new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10)).SetAbsoluteExpiration(TimeSpan.FromHours(1));
            if (cache.TryGetValue(key, out T? value) && value is not null){
                return value;
            }
            value = await task();
            if (value is not null){
                await cache.SetAsync<T>(key, value, options);
            }
            return value;
        }
    }
}