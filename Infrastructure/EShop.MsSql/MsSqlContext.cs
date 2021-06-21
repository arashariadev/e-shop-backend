using Microsoft.EntityFrameworkCore;

namespace EShop.MsSql
{
    public class MsSqlContext : DbContext
    {
        public DbSet<CatalogItemsEntity> CatalogItems { get; set; }
        
        public MsSqlContext(DbContextOptions<MsSqlContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}