using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrasructure
{
    public class GolrangDbContext : DbContext
    {
        public GolrangDbContext(DbContextOptions options)
            : base(options)
        {
            
        }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<People> Peoples { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
