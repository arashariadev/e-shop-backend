using System.Threading.Tasks;
using Domain;
using EShop.Domain.Identity.JWT;

namespace EShop.Domain.Identity
{
    public interface IIdentityService
    {
        Task<(DomainResult, LoginResult)> LoginAsync(string email, string password);

        Task<DomainResult> RegistrationAsync(
            string firstName,
            string lastName,
            string phoneNumber,
            string email,
            bool receiveMails,
            string password,
            string confirmationPassword);

        Task<(DomainResult, LoginResult)> RefreshTokenAsync(string refreshToken);

        Task<(DomainResult, LoginResult)> FacebookLogin(string accessToken);
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

        public async Task<(DomainResult, LoginResult)> LoginAsync(string email, string password)
        {
            var loginResult = await _identityStorage.Login(email, password);

            return loginResult == null 
                ? (DomainResult.Error("Wrong login/password combination"), null) 
                : (DomainResult.Success() , loginResult);
        }

        public async Task<DomainResult> RegistrationAsync(
            string firstName,
            string lastName,
            string phoneNumber,
            string email,
            bool receiveMails,
            string password,
            string confirmationPassword)
        {
            var result = _validator.Validate(new UserContext(firstName, lastName, phoneNumber, email));
            
            if (!result.Successed)
            {
                return result;
            }

            await _identityStorage.Registration(new User(firstName, lastName, phoneNumber, email, receiveMails, password, confirmationPassword));
            
            return DomainResult.Success();
        }

        public async Task<(DomainResult, LoginResult)> RefreshTokenAsync(string refreshToken)
        {
            var result = await _identityStorage.RefreshToken(refreshToken);

            return result == null
                ? (DomainResult.Error("Smt went wrong! Login again, please"), default)
                : (DomainResult.Success(), result);
        }

        public async Task<(DomainResult, LoginResult)> FacebookLogin(string accessToken)
        {
            var result = await _identityStorage.FacebookLoginAsync(accessToken);

            return result is null
                ? (DomainResult.Error("Invalid access token (or developer)"), default)
                : (DomainResult.Success(), result);
        }
    }
}