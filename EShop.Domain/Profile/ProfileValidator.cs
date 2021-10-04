using System.Collections.Generic;
using System.Linq;
using Domain;

namespace EShop.Domain.Profile
{
    public class ProfileValidator : IValidator<ProfileContext>
    {
        public DomainResult Validate(ProfileContext param)
        {
            var result = Validate(param.FirstName, param.LastName, param.PhoneNumber);

            return result;
        }

        private DomainResult Validate(string firstName, string lastName, string phoneNumber)
        {
            List<DomainError> results = new List<DomainError>();
            
            if (firstName == null || firstName.Length > 160 || string.IsNullOrEmpty(firstName))
            {
                results.Add(new DomainError($"{nameof(firstName)} cannot be null, empty or more than 150 chars"));
            }
            
            if (lastName == null || lastName.Length > 150 || string.IsNullOrEmpty(lastName))
            {
                results.Add(new DomainError($"{nameof(lastName)} cannot be null, empty or more than 150 chars"));
            }
            
            if (string.IsNullOrEmpty(phoneNumber))
            {
                results.Add(new DomainError($"{nameof(phoneNumber)} cannot be empty"));
            }

            return !results.Any() ? DomainResult.Success() : DomainResult.Error(results);
        }
    }
}