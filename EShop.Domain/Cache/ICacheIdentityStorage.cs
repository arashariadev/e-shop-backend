using System.Threading.Tasks;
using EShop.Domain.Identity;

namespace EShop.Domain.Cache;

public interface ICacheIdentityStorage
{
    Task<RefreshToken> GetCacheValueAsync(string key);

    Task SetCacheValueAsync(string key, RefreshToken value);
}