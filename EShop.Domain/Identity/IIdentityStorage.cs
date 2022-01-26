using System.Threading.Tasks;
using EShop.Domain.Cache;
using Microsoft.AspNetCore.Identity;

namespace EShop.Domain.Identity
{
    public interface IIdentityStorage
    {
        Task<LoginResult> Login(string email, string password);

        Task<IdentityResult> Registration(User user);

        Task<LoginResult> RefreshToken(string refreshToken);
    }
}