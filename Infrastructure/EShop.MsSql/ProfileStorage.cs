using System.Threading.Tasks;
using EShop.Domain.Identity;
using EShop.Domain.Profile;
using Microsoft.EntityFrameworkCore;

namespace EShop.MsSql
{
    public class ProfileStorage : IProfileStorage
    {
        private readonly MsSqlContext _context;

        public ProfileStorage(MsSqlContext context)
        {
            _context = context;
        }
        
        public async Task<UserProfile> GetProfileAsync(string id)
        {
            var profile = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == id);

            return profile == null ? null : ToDomain(profile);
        }

        public async Task UpdateProfileAsync(string id, UserProfile userProfile)
        {
            var profile = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == id);

            profile.FirstName = userProfile.FirstName;
            profile.LastName = userProfile.LastName;
            profile.PhoneNumber = userProfile.PhoneNumber;

            _context.ApplicationUsers.Update(profile);
            await _context.SaveChangesAsync();
        }

        private static UserProfile ToDomain(UserEntity entity)
        {
            return new UserProfile(
                entity.Id,
                entity.Email,
                entity.FirstName,
                entity.LastName,
                entity.PhoneNumber,
                entity.ReceiveMails);
        }
    }
}