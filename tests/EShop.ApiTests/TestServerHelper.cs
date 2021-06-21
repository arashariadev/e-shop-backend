using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using EShop.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.ApiTests
{
    public static class TestServerHelper
    {
        public static HttpClient New(Action<IServiceCollection> configureServices)
        {
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureTestServices(configureServices));

            return server.CreateClient();
        }

        public static StringContent AsJsonContent<T>(this T input)
        {
            return new StringContent(JsonSerializer.Serialize(input), Encoding.Default, "application/json");
        }
    }
}