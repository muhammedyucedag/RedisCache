using Microsoft.AspNetCore.Mvc;
using RedisCache.Interfaces;
using RedisCache.Model;

namespace RedisCache.Controller;

[ApiController]
[Route("api/cache")]
public class CacheController(IRedisCacheService redisCacheService) : ControllerBase
{
    [HttpGet("{key}")]
    public async Task<IActionResult> Get(string key)
    {
        return Ok(await redisCacheService.GetValueAsync(key));
    }

    [HttpPost("set")]
    public async Task<IActionResult> Set([FromBody] RedisCacheRequestModel redisCacheRequestModel)
    {
        await redisCacheService.SetValueAsync(redisCacheRequestModel.Key, redisCacheRequestModel.Value);
        return Ok();
    }

    [HttpDelete("{key}")]
    public async Task<IActionResult> Delete(string key)
    {
        await redisCacheService.Clear(key);
        return Ok();
    }
}
