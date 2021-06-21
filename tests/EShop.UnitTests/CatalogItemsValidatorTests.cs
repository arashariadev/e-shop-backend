using System.Linq;
using EShop.Domain.Catalog;
using NUnit.Framework;

namespace EShop.UnitTests
{
    public class CatalogItemsValidatorTests
    {
        [Test]
        public void Name_Null_Argument()
        {
            // Setup
            var validator = new CatalogItemValidator();
            
            // Act
            var result = validator.Validate(new CatalogItemContext(null, "Description", 12));
            
            // Assert
            Assert.IsFalse(result.Successed && result.Errors.Count() == 1);
        }

        [Test]
        public void Description_Null_Argument()
        {
            // Setup
            var validator = new CatalogItemValidator();
            
            // Act
            var result = validator.Validate(new CatalogItemContext("Name", null, 12));
            
            // Assert
            Assert.IsFalse(result.Successed && result.Errors.Count() == 1);
        }

        [Test]
        public void Price_LessOrEqualsZero_Argument()
        {
            // Setup
            var validator = new CatalogItemValidator();
            
            // Act
            var result = validator.Validate(new CatalogItemContext("Name", "Description", 0));
            
            // Assert
            Assert.IsFalse(result.Successed && result.Errors.Count() == 1);
        }
    }
}