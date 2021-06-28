using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain;
using EShop.Domain.Catalog;

namespace EShop.Domain.Identity
{
    public class UserValidator : IValidator<UserContext>
    {
        public DomainResult Validate(UserContext param)
        {
            var result = Validate(param.FirstName, param.LastName, param.PhoneNumber, param.Email);

            return result;
        }
        
        public DomainResult Validate(string firstName, string lastName, string phoneNumber, string email)
        {
            List<DomainError> results = new List<DomainError>();

            if (string.IsNullOrEmpty(firstName) || firstName.Length > 75)
            {
                results.Add(new DomainError($"{nameof(firstName)} can not be null, empty or more than 75 chars"));
            }
            
            if (string.IsNullOrEmpty(lastName) || lastName.Length > 75)
            {
                results.Add(new DomainError($"{nameof(lastName)} can not be null, empty or more than 75 chars"));
            }
            
            if (string.IsNullOrEmpty(phoneNumber) || phoneNumber.Length is > 12 or < 10)
            {
                results.Add(new DomainError($"{nameof(phoneNumber)} can not be empty or more than 12 or less than 10 chars"));
            }
            
            //Need to fix
            if (string.IsNullOrEmpty(email) || email.Length > 70 || !email.Contains("@"))
            {
                results.Add(new DomainError($"{nameof(email)} is not valid"));
            }
            
            return DomainResult.Error(results);
        }
    }
}