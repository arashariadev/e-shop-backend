using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EShop.Domain.Identity
{
    public interface IIdentityStorage
    {
        Task<string> Login(string email, string password);

        Task<IdentityResult> Registration(User user);
    }
}