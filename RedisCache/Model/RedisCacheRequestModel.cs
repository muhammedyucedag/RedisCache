namespace RedisCache.Model;

public class RedisCacheRequestModel
{
    public string Key { get; set; } = null!;
    public string Value { get; set; } = null!;
}