using Microsoft.AspNetCore.Mvc;
using RedisExample.Api.Requests;
using RedisExample.Api.Services;

namespace RedisExample.Api.Controllers
{
    [ApiController]
    public class CacheController : Controller
    {
        public ICacheService _cacheService;

        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet("cache/{key}")]
        public async Task<IActionResult> GetCacheValue([FromRoute] string key)
        {
            var value = await _cacheService.GetCacheValueAsync(key);
            return string.IsNullOrEmpty(value) ? (IActionResult) NotFound() : Ok(value);
        }

        [HttpPost("cache")]
        public async Task<IActionResult> SetCacheValue([FromBody] NewCacheRequest request)
        {
            await _cacheService.SetCacheValueAsync(request.Key, request.Value);
            return Ok();
        }
    }
}
