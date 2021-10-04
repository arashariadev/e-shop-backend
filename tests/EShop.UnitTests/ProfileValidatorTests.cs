using System.Linq;
using EShop.Domain.Profile;
using NUnit.Framework;

namespace EShop.UnitTests
{
    public class ProfileValidatorTests
    {
        [Test]
        public void FirstName_Is_Null()
        {
            //Setup
            var validator = new ProfileValidator();
            //Act
            var result = validator.Validate(new ProfileContext(null, "lastName", "054949564"));
            //Assert
            Assert.IsFalse(result.Successed && result.Errors.Count() == 1);
        }

        [Test]
        public void LastName_Is_Null()
        {
            //Setup
            var validator = new ProfileValidator();
            //Act
            var result = validator.Validate(new ProfileContext("firstName", null, "5045649864"));
            //Assert
            Assert.IsFalse(result.Successed && result.Errors.Count() == 1);
        }

        [Test]
        public void PhoneNumber_Is_Null()
        {
            //Setup
            var validator = new ProfileValidator();
            //Act
            var result = validator.Validate(new ProfileContext("firstName", "lastName", null));
            //Assert
            Assert.IsFalse(result.Successed && result.Errors.Count() == 1);
        }
        
        
    }
}