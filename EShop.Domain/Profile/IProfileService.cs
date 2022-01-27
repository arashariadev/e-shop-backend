using System.Threading.Tasks;
using Domain;
using EShop.Domain.Identity;

namespace EShop.Domain.Profile
{
    public interface IProfileService
    {
        Task<UserProfile> FindProfileByIdAsync(string id);

        Task<DomainResult> UpdateProfileAsync(string id, string firstName, string lastName, string phoneNumber, bool receiveMails);
    }

    public class ProfileService : IProfileService
    {
        private readonly IProfileStorage _profileStorage;
        private readonly IValidator<ProfileContext> _validator;

        public ProfileService(IProfileStorage profileStorage, IValidator<ProfileContext> validator)
        {
            _profileStorage = profileStorage;
            _validator = validator;
        }
        
        public async Task<UserProfile> FindProfileByIdAsync(string id)
        {
            return await _profileStorage.GetProfileAsync(id);
        }

        public async Task<DomainResult> UpdateProfileAsync(string id, string firstName, string lastName, string phoneNumber, bool receiveMails)
        {
            var validate = _validator.Validate(new ProfileContext(firstName, lastName, phoneNumber));
            if (!validate.Successed)
            {
                return validate;
            }

            var profile = await _profileStorage.GetProfileAsync(id);
            if (profile == null)
            {
                return DomainResult.Error("item not found");
            }
            
            profile.Update(firstName, lastName, phoneNumber, receiveMails);
            await _profileStorage.UpdateProfileAsync(id, profile);

            return DomainResult.Success();
        }
    }
}