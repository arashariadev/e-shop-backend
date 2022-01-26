using System;
using System.Net;
using System.Threading.Tasks;
using Domain;
using EShop.Api.Models;
using EShop.Api.Models.CatalogItem;
using EShop.Domain.Catalog;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace EShop.ApiTests.CatalogItems
{
    // [TestFixture]
    // public class UpdateTests
    // {
        // [Test]
        // public async Task Update_NoContent()
        // {
        //     // Setup
        //     var id = Guid.NewGuid();
        //
        //     var input = new UpdateItemViewModel
        //     {
        //         Name = "Name",
        //         Description = "Description",
        //         Price = 12,
        //         PictureFileName = "Some name",
        //         PictureUri = "SomeUrl",
        //         AvailableStock = 10
        //     };
        //
        //     var item = new Domain.Catalog.CatalogItems(id, input.Name, input.Description, input.Price,
        //         input.PictureFileName, input.PictureUri, input.AvailableStock);
        //
        //     var serviceMock = new Mock<ICatalogItemsService>();
        //
        //     serviceMock
        //         .Setup(s => s.UpdateItemAsync(id, item.Name, item.Description, item.Price, item.PictureFileName,
        //             item.PictureUri, item.AvailableStock))
        //         .ReturnsAsync(DomainResult.Success);
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
        //     var response = await client.PutAsync($"/Catalog/{id}", input.AsJsonContent());
        //     
        //     // Assert
        //     Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        // }
        //
        // [Test]
        // public async Task Update_NotFound()
        // {
        //     // Setup
        //     var id = Guid.NewGuid();
        //     
        //     var input = new UpdateItemViewModel
        //     {
        //         Name = "Name",
        //         Description = "Description",
        //         Price = 12,
        //         PictureFileName = "Some name",
        //         PictureUri = "SomeUrl",
        //         AvailableStock = 10
        //     };
        //     
        //     var serviceMock = new Mock<ICatalogItemsService>();
        //     
        //     serviceMock
        //         .Setup(s => s.FindItemByIdAsync(id))
        //         .ReturnsAsync(default(Domain.Catalog.CatalogItems));
        //     
        //     var client = TestServerHelper.New(collection =>
        //     {
        //         collection.AddScoped(provider => serviceMock.Object);
        //     });
        //     
        //     // Act
        //     var response = await client.PutAsync($"/api/Catalog/{id}", input.AsJsonContent());
        //     
        //     // Assert
        //     Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        // }
        //
        // [Test]
        // public async Task Update_ServiceError_BadRequest()
        // {
        //     // Setup
        //     var id = Guid.NewGuid();
        //     
        //     var input = new UpdateItemViewModel
        //     {
        //         Name = "Name",
        //         Description = "Description",
        //         Price = 12,
        //         PictureFileName = "Some name",
        //         PictureUri = "SomeUrl",
        //         AvailableStock = 10
        //     };
        //
        //     var item = new Domain.Catalog.CatalogItems(id, input.Name, input.Description, input.Price,
        //         input.PictureFileName, input.PictureUri, input.AvailableStock);
        //     
        //     var serviceMock = new Mock<ICatalogItemsService>();
        //     
        //     serviceMock
        //         .Setup(s => s.UpdateItemAsync(id, item.Name, item.Description, item.Price, item.PictureFileName,
        //             item.PictureUri, item.AvailableStock))
        //         .ReturnsAsync(DomainResult.Error("Smt went wrong"));
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
        //     var response = await client.PutAsync($"/Catalog/{id}", input.AsJsonContent());
        //     
        //     // Assert
        //     Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        // }
    // }
}