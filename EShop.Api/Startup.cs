using Azure.Storage.Blobs;
using EShop.Api.Helpers.OpenApi;
using EShop.Azure;
using EShop.Domain.Azure;
using EShop.Domain.Catalog;
using EShop.MsSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;

namespace EShop.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "EShop.Api",
                    Description = "Http client for EShop",
                    Version = "1.0.0"
                });

                options.OperationFilter<OperationIdFilter>();
                options.OperationFilter<AddResponseHeadersFilter>();

                options.IncludeXmlComments(XmlPathProvider.XmlPath);
            });
            
            services.AddSingleton(provider => new BlobStorageSettings(
                new BlobServiceClient(AzureConnectionString()), "gh-network"));
            services.AddSingleton<IImagesStorage, ImagesStorage>();

            services.AddDbContext<MsSqlContext>(options =>
                options.UseSqlServer(
                    MssqlConnectionStringDev(),
                    b => b.MigrationsAssembly("EShop.Api")));

            services.AddScoped<IValidator<CatalogItemContext>, CatalogItemValidator>();
            services.AddScoped<ICatalogItemsStorage, CatalogItemsStorage>();
            services.AddScoped<ICatalogItemsService, CatalogItemsService>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger()
                    .UseSwaggerUI(config =>
                    {
                        config.SwaggerEndpoint("/swagger/v1/swagger.json", "EShop.Api V1");
                    });
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
        //Need to change. Use key vault or local user secrets to save azure connection string
        private string AzureConnectionString()
        {
            return @"";
        }

        //Need delete. Connection string for dev
        private string MssqlConnectionStringDev()
        {
            return @"Server=DESKTOP-FQ83PKI;Database=EShop;Trusted_Connection=True;";
        }
        
        //Connection string for prod (check launch settings)
        private string MssqlConnectionString()
        {
            return $"Server={Configuration["MSSQL_ADDRESS"]},{Configuration["MSSQL_PORT"]};Database=EShop;User={Configuration["MSSQL_USER"]};Password={Configuration["MSSQL_PASSWORD"]}";
        }
    }
}