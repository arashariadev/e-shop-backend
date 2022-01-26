using System;
using System.Net;
using System.Threading.Tasks;
using EShop.Domain.Identity;
using EShop.Domain.Profile;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace EShop.ApiTests.Profile
{
    //[TestFixture]
    //public class GetByIdTests
    //{
        // [Test]
        // public async Task User_Exist()
        // {
        //     //Setup
        //     var id = Guid.NewGuid().ToString();
        //     var user = new UserProfile(id, "email", "fn", "ln", "pn");
        //     var serviceMock = new Mock<IProfileService>();
        //     serviceMock
        //         .Setup(x => x.FindProfileByIdAsync(id))
        //         .ReturnsAsync(user);
        //
        //     var client = TestServerHelper.New(collection =>
        //     {
        //         collection.AddAuthentication("Test")
        //             .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", _ => { });
        //         collection.AddScoped(_ => serviceMock.Object);
        //     });
        //     
        //     //Act
        //     var response = await client.GetAsync($"/Profile");
        //     
        //     //Assert
        //     Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        // }
    //}
}