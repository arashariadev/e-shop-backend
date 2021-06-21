using System;
using System.Threading.Tasks;
using Domain;
using EShop.Api.Models;
using EShop.Domain.Catalog;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace EShop.ApiTests.CatalogItems
{
    [TestFixture]
    public class CreateTests
    {
        [Test]
        public async Task CreateItem_Created()
        {
            // Setup
            var input = new CreateItemViewModel
            {
                Name = "Name",
                Description = "Description",
                Price = 12,
                PictureFileName = "Some",
                PictureUri = "Some",
                AvailableStock = 10
            };

            var id = Guid.NewGuid();
            var item = new Domain.Catalog.CatalogItems(id, input.Name, input.Description, input.Price,
                input.PictureFileName, input.PictureUri, input.AvailableStock);

            var serviceMock = new Mock<ICatalogItemsService>();

            serviceMock.Setup(s => s.InsertItemAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>()))
                .ReturnsAsync((DomainResult.Success(), id));

            serviceMock
                .Setup(s => s.FindItemByIdAsync(id))
                .ReturnsAsync(item);
            
            var client = TestServerHelper.New(collection =>
            {
                collection.AddScoped(provider => serviceMock.Object);
            });
            
            // Act 
            var response = await client.PostAsync("/api/Catalog", input.AsJsonContent());
            
            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task Create_ServiceError_BadRequest()
        {
            // Setup
            var input = new CreateItemViewModel
            {
                Name = "Name",
                Description = "Description",
                Price = 12,
                PictureFileName = "Some",
                PictureUri = "Some",
                AvailableStock = 10
            };
            
            var serviceMock = new Mock<ICatalogItemsService>();
            serviceMock
                .Setup(s => s.InsertItemAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<decimal>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<int>()))
                .ReturnsAsync((DomainResult.Error("Smt went wrong"), default));
            
            var client = TestServerHelper.New(collection =>
            {
                collection.AddScoped(provider => serviceMock.Object);
            });
            
            // Act
            var response = await client.PostAsync("/api/Catalog", input.AsJsonContent());
            
            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}