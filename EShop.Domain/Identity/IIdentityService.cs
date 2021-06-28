using System.Threading.Tasks;
using Domain;
using EShop.Domain.Catalog;

namespace EShop.Domain.Identity
{
    public interface IIdentityService
    {
        Task<(DomainResult, string)> LoginAsync(string email, string password);

        Task<DomainResult> RegistrationAsync(
            string firstName,
            string lastName,
            string phoneNumber,
            string email,
            string password,
            string confirmationPassword);
    }

    public class IdentityService : IIdentityService
    {
        private readonly IIdentityStorage _identityStorage;
        private readonly IValidator<UserContext> _validator;

        public IdentityService(IIdentityStorage identityStorage, IValidator<UserContext> validator)
        {
            _identityStorage = identityStorage;
            _validator = validator;
        }

        public async Task<(DomainResult, string)> LoginAsync(string email, string password)
        {
            var token = await _identityStorage.Login(email, password);

            return token == null ? (DomainResult.Error("Wrong login/password combination"), null) : (DomainResult.Success() , token);
        }

        public async Task<DomainResult> RegistrationAsync(
            string firstName,
            string lastName,
            string phoneNumber,
            string email,
            string password,
            string confirmationPassword)
        {
            var result = _validator.Validate(new UserContext(firstName, lastName, phoneNumber, email));
            
            if (!result.Successed)
            {
                return result;
            }

            await _identityStorage.Registration(new User(firstName, lastName, phoneNumber, email, password, confirmationPassword));
            
            return DomainResult.Success();
        }
    }
}