using RedisCache.Interfaces;
using StackExchange.Redis;

namespace RedisCache.Services;

public class RedisCacheService(IConnectionMultiplexer redisConnection) : IRedisCacheService
{
    private readonly IDatabase _cache = redisConnection.GetDatabase();

    public async Task<string> GetValueAsync(string key)
    {
        return (await _cache.StringGetAsync(key))!;
    }

    public async Task<bool> SetValueAsync(string key, string value)
    {
        return await _cache.StringSetAsync(key, value, TimeSpan.FromMinutes(30));
    }

    public async Task Clear(string key)
    {
        await _cache.KeyDeleteAsync(key);
    }

    public void ClearAll()
    {
        var redisEndpoints = redisConnection.GetEndPoints(true);

        foreach (var redisEndpoint in redisEndpoints)
        {
            var redisServer = redisConnection.GetServer(redisEndpoint);
            redisServer.FlushAllDatabases();
        }
    }
}