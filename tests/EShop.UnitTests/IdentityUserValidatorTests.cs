using System.Linq;
using EShop.Domain.Identity;
using NUnit.Framework;

namespace EShop.UnitTests
{
    public class IdentityUserValidatorTests
    {
        [Test]
        public void First_Name_Null()
        {
            // Setup
            var validator = new UserValidator();
            
            // Act
            var result = validator.Validate(new UserContext(null, "lastName", "12345678910", "email@email.com"));
            
            // Assert
            Assert.IsFalse(result.Successed && result.Errors.Count() == 1);
        }
        
        [Test]
        public void Last_Name_Null()
        {
            // Setup
            var validator = new UserValidator();
            
            // Act
            var result = validator.Validate(new UserContext("firstName", null, "12345678910", "email@email.com"));
            
            // Assert
            Assert.IsFalse(result.Successed && result.Errors.Count() == 1);
        }
        
        [Test]
        public void PhoneNumber_LessOrMore_Length()
        {
            // Setup
            var validator = new UserValidator();
            
            // Act
            var result = validator.Validate(new UserContext("firstName", "lastName", "12345678910", "email@email.com"));
            
            // Assert
            Assert.IsFalse(result.Successed && result.Errors.Count() == 1);
        }
        
        [Test]
        public void Email_Null()
        {
            // Setup
            var validator = new UserValidator();
            
            // Act
            var result = validator.Validate(new UserContext("firstName", "lastName", "12345678910", null));
            
            // Assert
            Assert.IsFalse(result.Successed && result.Errors.Count() == 1);
        }
        
        [Test]
        public void Email_is_NotValid()
        {
            // Setup
            var validator = new UserValidator();
            
            // Act
            var result = validator.Validate(new UserContext("firstName", "lastName", "12345678910", "Somemail.com"));
            
            // Assert
            Assert.IsFalse(result.Successed && result.Errors.Count() == 1);
        }
    }
}