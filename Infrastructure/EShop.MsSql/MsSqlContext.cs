using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EShop.MsSql
{
    public class MsSqlContext : IdentityDbContext<UserEntity>
    {
        public DbSet<UserEntity> ApplicationUsers { get; set; }
        public DbSet<CatalogItemsEntity> CatalogItems { get; set; }
        
        public MsSqlContext(DbContextOptions<MsSqlContext> options) : base(options)
        {
        }
    }
}