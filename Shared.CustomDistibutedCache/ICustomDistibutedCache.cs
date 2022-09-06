using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace eveDirect.Caching
{
    public class CustomDistibutedCache : ICustomDistibutedCache
    {
        IDistributedCache _cache { get; set; }
        ILogger<CustomDistibutedCache> _logger { get; set; }
        public CustomDistibutedCache(IDistributedCache cache, ILogger<CustomDistibutedCache> logger)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public bool Get<T>(string key, out T value)
        {
            var objectBytes = _cache.Get(key);
            value = default;
            if (objectBytes != null)
            {
                var tempValue = new ReadOnlySpan<byte>(objectBytes);
                value = JsonSerializer.Deserialize<T>(tempValue);
                return true;
            }
            return false;
        }

        public async Task SetAsync<T>(string key, T value)
        {
            await SetAsync(key, value, new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(2 * 365)));
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan slideExpire)
        {
            await SetAsync(key, value, new DistributedCacheEntryOptions().SetSlidingExpiration(slideExpire));
        }

        async Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options) {
            var jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(value, new JsonSerializerOptions { WriteIndented = true });
            await _cache.SetAsync(key, jsonUtf8Bytes, options);

            _logger.LogInformation($"Cached {key}");
        }
    }
    public interface ICustomDistibutedCache
    {
        bool Get<T>(string key, out T value);
        /// <summary>
        /// Absolute expiration
        /// </summary>
        Task SetAsync<T>(string key, T value);
        /// <summary>
        /// Slide expiration
        /// </summary>
        /// <returns></returns>
        Task SetAsync<T>(string key, T value, TimeSpan slideExpire);
    }
}
