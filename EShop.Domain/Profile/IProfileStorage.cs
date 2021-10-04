using System.Threading.Tasks;

namespace EShop.Domain.Profile
{
    public interface IProfileStorage
    {
        Task<UserProfile> GetProfileAsync(string id);

        Task UpdateProfileAsync(string id, UserProfile userProfile);
    }
}