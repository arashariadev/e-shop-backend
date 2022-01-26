using System;
using System.Net;
using System.Threading.Tasks;
using Domain;
using EShop.Api.Models.Profile;
using EShop.Domain.Identity;
using EShop.Domain.Profile;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace EShop.ApiTests.Profile
{
    //[TestFixture]
    //public class UpdateTests
    //{
        // [Test]
        // public async Task Update_Successfully()
        // {
        //     //Setup
        //     var userId = Guid.NewGuid().ToString();
        //
        //     var profileServiceMock = new Mock<IProfileService>();
        //     profileServiceMock.Setup(s => s.FindProfileByIdAsync(userId))
        //         .ReturnsAsync(new UserProfile( userId,  "Email","FirstName", "LastName", "PhoneNumber"));
        //     profileServiceMock.Setup(s => s.UpdateProfileAsync(userId, "UpdName", "UpdLastName", "UpdPhone"))
        //         .ReturnsAsync(DomainResult.Success);
        //
        //     var currentUserProviderMock = new Mock<ICurrentUserProvider>();
        //     currentUserProviderMock.Setup(s => s.UserId)
        //         .Returns(userId);
        //
        //     var client = TestServerHelper.New(collection =>
        //     {
        //         collection.AddAuthentication("Test")
        //             .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", _ => { });
        //         collection.AddScoped(_ => profileServiceMock.Object);
        //         collection.AddScoped(_ => currentUserProviderMock.Object);
        //     });
        //     
        //     var update = new UpdateProfileViewModel()
        //     {
        //         FirstName = "T",
        //         LastName = "F",
        //         PhoneNumber = "05"
        //     };
        //     
        //     
        //     //Act
        //     var response = await client.PutAsync($"/Profile/{userId}", update.AsJsonContent());
        //     
        //     //Assert
        //     Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        // }
    //}
}