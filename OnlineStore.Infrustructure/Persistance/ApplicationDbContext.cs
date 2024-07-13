

using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain;
using OnlineStore.Infrustructure;

namespace OnlineStore.Infrustructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfig).Assembly);
        }
    }
}
