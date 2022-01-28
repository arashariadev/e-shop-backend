using System;
using System.Threading.Tasks;
using EShop.Api.Helpers;
using EShop.MsSql;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace EShop.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            
            var service = scope.ServiceProvider;

            try
            {
                var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = service.GetRequiredService<UserManager<UserEntity>>();

                await initializer.InitializeRoleAsync(roleManager, userManager);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).
                UseSerilog((context, configuration) =>
                {
                    configuration.Enrich.FromLogContext()
                        .WriteTo.Console()
                        .WriteTo.File("../eShopLogs.txt");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}