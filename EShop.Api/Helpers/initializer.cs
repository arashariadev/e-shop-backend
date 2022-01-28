using System.Threading.Tasks;
using EShop.MsSql;
using Microsoft.AspNetCore.Identity;

namespace EShop.Api.Helpers;

public class initializer
{
    public static async Task InitializeRoleAsync(RoleManager<IdentityRole> roleManager,
        UserManager<UserEntity> userManager)
    {
        if (await roleManager.FindByNameAsync("Customer") == null)
        {
            await roleManager.CreateAsync(new IdentityRole("Customer"));
        }

        if (await roleManager.FindByNameAsync("SuperAdmin") == null)
        {
            await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        }

        var superAdmin = new UserEntity()
        {
            Email = "e.shop.api.ua@gmail.com",
            UserName = "e.shop.api.ua@gmail.com",
            FirstName = "Super",
            LastName = "Admin",
            PhoneNumber = "0546854854"
        };

        var result = await userManager.CreateAsync(superAdmin, "!234Qwer");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
        }
    }
}