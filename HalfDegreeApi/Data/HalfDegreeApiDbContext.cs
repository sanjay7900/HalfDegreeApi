using HalfDegreeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HalfDegreeApi.Data
{
    public class HalfDegreeApiDbContext:DbContext
    {
        public DbSet<Product>? products { get; set; }
        public DbSet<Order>? orders { get; set; }  
        public DbSet<Roles>? roles { get; set; }
        public DbSet<User>? users { get; set; }  
        public HalfDegreeApiDbContext(DbContextOptions<HalfDegreeApiDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Roles>().HasIndex(r => r.Name).IsUnique();

        }
    }
}
