using System;
using System.Collections.Generic;
using System.Text;
using Azure.Storage.Blobs;
using EShop.Api.Helpers.OpenApi;
using EShop.Azure;
using EShop.Domain;
using EShop.Domain.Azure;
using EShop.Domain.Cache;
using EShop.Domain.Catalog;
using EShop.Domain.Identity;
using EShop.Domain.Identity.JWT;
using EShop.Domain.Profile;
using EShop.MsSql;
using EShop.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.Filters;

namespace EShop.Api
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            var jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                try
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });

            services.AddIdentityCore<UserEntity>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                })
                .AddEntityFrameworkStores<MsSqlContext>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "EShop.Api",
                    Description = "Http client for EShop",
                    Version = "1.0.0"
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {new OpenApiSecurityScheme{Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }}, new List<string>()}
                });

                options.OperationFilter<OperationIdFilter>();
                options.OperationFilter<AddResponseHeadersFilter>();
                options.IncludeXmlComments(XmlPathProvider.XmlPath);
            });

            services.AddSingleton(_ => new BlobStorageSettings(
                new BlobServiceClient(AzureConnectionString()), "eshop"));
            services.AddSingleton<IImagesStorage, ImagesStorage>();

            services.AddDbContext<MsSqlContext>(options =>
                options.UseSqlServer(
                    MssqlConnectionStringDev(),
                    b => b.MigrationsAssembly("EShop.Api")));
            
            services.AddSingleton<IConnectionMultiplexer>(_ =>
                ConnectionMultiplexer.Connect(Configuration.GetValue<string>("REDIS_CONNECTION")));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<ICacheIdentityStorage, CacheIdentityStorage>();
            services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

            services.AddScoped<IValidator<CatalogItemContext>, CatalogItemValidator>();
            services.AddScoped<ICatalogItemsStorage, CatalogItemsStorage>();
            services.AddScoped<ICatalogItemsService, CatalogItemsService>();

            services.AddScoped<IValidator<UserContext>, UserValidator>();
            services.AddScoped<IIdentityStorage, IdentityStorage>();
            services.AddScoped<IIdentityService, IdentityService>();
            
            services.AddScoped<IValidator<ProfileContext>, ProfileValidator>();
            services.AddScoped<IProfileStorage, ProfileStorage>();
            services.AddScoped<IProfileService, ProfileService>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MsSqlContext context, ILogger<Startup> logger)
        {
            // try
            // {
            //     context.Database.Migrate();
            // }
            // catch (Exception ex)
            // {
            //     logger.LogError(ex, "An error occurred with auto migrate the DB in Startup.cs.");
            // }
            
            if (env.IsDevelopment())
            {
                app.UseSwagger()
                    .UseSwaggerUI(config =>
                    {
                        config.SwaggerEndpoint("/swagger/v1/swagger.json", "EShop.Api V1");
                        config.DisplayRequestDuration();
                    });
            }

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        
        //Need to change. Use key vault or local user secrets to save azure connection string
        private string AzureConnectionString()
        {
            return @"DefaultEndpointsProtocol=https;AccountName=ehsopblob;AccountKey=P4QxkwgxdjjbJ5mcgWy5w5RN77KHpDUyj2Iol+NEIe5hU6o9ELXyM1/yeX3pzj1ya5U+M+1PeXFkw8J3I+BvRQ==;EndpointSuffix=core.windows.net";
        }

        //Need to delete. Connection string for dev
        private string MssqlConnectionStringDev()
        {
            return @"Server=localhost;Database=sql1;User=SA;Password=!234Qwer";
        }
        
        //Connection string for prod (check launch settings)
        //TODO implement this 
        // private string MsSqlConnectionString()
        // {
        //     var connectionString = $"Server={Configuration["MSSQL_ADDRESS"]};Database={Configuration["MSSQL_CATALOG"]};User={Configuration["MSSQL_USER"]};Password={Configuration["MSSQL_PASSWORD"]};";
        //     return connectionString;
        // }
    }
}