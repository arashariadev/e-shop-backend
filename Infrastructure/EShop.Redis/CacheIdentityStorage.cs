using EShop.Domain.Cache;
using EShop.Domain.Identity;
using EShop.Domain.Identity.JWT;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace EShop.Redis;

public class CacheIdentityStorage : ICacheIdentityStorage
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    public CacheIdentityStorage(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }

    public async Task<RefreshToken> GetCacheValueAsync(string key)
    {
        var db = _connectionMultiplexer.GetDatabase();
        
        var serializedValue = await db.StringGetAsync(key);

        var deserializedValue = JsonConvert.DeserializeObject<RefreshToken>(serializedValue);

        return deserializedValue;
    }

    public async Task SetCacheValueAsync(string key, RefreshToken value)
    {
        var db = _connectionMultiplexer.GetDatabase();

        var jsonValue = JsonConvert.SerializeObject(value);
        
        await db.StringSetAsync(key, jsonValue);
    }
}