using System;
using System.Threading.Tasks;
using Domain;
using Moq;
using NUnit.Framework;
using System.Net;
using EShop.Api.Models.CatalogItem;
using EShop.Domain.Catalog;
using Microsoft.Extensions.DependencyInjection;


namespace EShop.ApiTests.CatalogItems
{
    // [TestFixture]
    // public class CreateTests
    // {
        //TODO fix api tests
        // [Test]
        // public async Task CreateItem_Created()
        // {
        //     // Setup
        //     var input = new CreateItemViewModel
        //     {
        //         Name = "Name",
        //         Description = "Description",
        //         Price = 12,
        //         PictureFileName = "Some",
        //         PictureUri = "Some",
        //         AvailableStock = 10
        //     };
        //
        //     var id = Guid.NewGuid();
        //     var item = new Domain.Catalog.CatalogItems(id, input.Name, input.Description, input.Price,
        //         input.PictureFileName, input.PictureUri, input.AvailableStock);
        //
        //     var serviceMock = new Mock<ICatalogItemsService>();
        //
        //     serviceMock.Setup(s => s.InsertItemAsync(
        //         It.IsAny<string>(),
        //         It.IsAny<string>(),
        //         It.IsAny<decimal>(),
        //         It.IsAny<string>(),
        //         It.IsAny<string>(),
        //         It.IsAny<int>()))
        //         .ReturnsAsync((DomainResult.Success(), id));
        //
        //     serviceMock
        //         .Setup(s => s.FindItemByIdAsync(id))
        //         .ReturnsAsync(item);
        //     
        //     var client = TestServerHelper.New(collection =>
        //     {
        //         collection.AddScoped(provider => serviceMock.Object);
        //     });
        //     
        //     // Act 
        //     var response = await client.PostAsync("/Catalog", input.AsJsonContent());
        //     
        //     // Assert
        //     Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        // }
        //
        // [Test]
        // public async Task Create_ServiceError_BadRequest()
        // {
        //     // Setup
        //     var input = new CreateItemViewModel
        //     {
        //         Name = "Name",
        //         Description = "Description",
        //         Price = 12,
        //         PictureFileName = "Some",
        //         PictureUri = "Some",
        //         AvailableStock = 10
        //     };
        //     
        //     var serviceMock = new Mock<ICatalogItemsService>();
        //     serviceMock
        //         .Setup(s => s.InsertItemAsync(
        //             It.IsAny<string>(),
        //             It.IsAny<string>(),
        //             It.IsAny<decimal>(),
        //             It.IsAny<string>(),
        //             It.IsAny<string>(),
        //             It.IsAny<int>()))
        //         .ReturnsAsync((DomainResult.Error("Smt went wrong"), default));
        //     
        //     var client = TestServerHelper.New(collection =>
        //     {
        //         collection.AddScoped(provider => serviceMock.Object);
        //     });
        //     
        //     // Act
        //     var response = await client.PostAsync("/Catalog", input.AsJsonContent());
        //     
        //     // Assert
        //     Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        // }
   // }
}