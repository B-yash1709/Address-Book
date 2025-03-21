using StackExchange.Redis;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Service
{
    public class RedisCacheService
    {
        private readonly IDatabase _cache;

        public RedisCacheService(IConfiguration config)
        {
            var redis = ConnectionMultiplexer.Connect($"{config["Redis:Host"]}:{config["Redis:Port"]}");
            _cache = redis.GetDatabase();
        }

        // ✅ Store data in Redis cache
        public void Set(string key, string value, TimeSpan? expiry = null)
        {
            _cache.StringSet(key, value, expiry ?? TimeSpan.FromMinutes(30));
        }

        // ✅ Retrieve data from Redis cache
        public string Get(string key)
        {
            return _cache.StringGet(key);
        }

        // ✅ Check if key exists
        public bool Exists(string key)
        {
            return _cache.KeyExists(key);
        }

        // ✅ Remove data from Redis cache
        public void Remove(string key)
        {
            _cache.KeyDelete(key);
        }
    }
}
