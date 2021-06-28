using System;
using System.Net;
using System.Threading.Tasks;
using EShop.Domain.Catalog;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace EShop.ApiTests.CatalogItems
{
    [TestFixture]
    public class GetByIdTests
    {
        [Test]
        public async Task GetById_Ok()
        {
            // Setup
            var id = Guid.NewGuid();

            var item = new Domain.Catalog.CatalogItems(id, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>());

            var serviceMock = new Mock<ICatalogItemsService>();

            serviceMock
                .Setup(s => s.FindItemByIdAsync(id))
                .ReturnsAsync(item);

            var client = TestServerHelper.New(collection =>
            {
                collection.AddScoped(provider => serviceMock.Object);
            });
            
            // Act
            var response = await client.GetAsync($"/Catalog/{id}/");
            
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetById_NotFound()
        {
            // Setup
            var id = Guid.NewGuid();
            
            var serviceMock = new Mock<ICatalogItemsService>();

            serviceMock
                .Setup(s => s.FindItemByIdAsync(id))
                .ReturnsAsync(default(Domain.Catalog.CatalogItems));
            
            var client = TestServerHelper.New(collection =>
            {
                collection.AddScoped(provider => serviceMock.Object);
            });
            
            // Act
            var response = await client.GetAsync($"/api/Catalog/{id}/");
            
            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}