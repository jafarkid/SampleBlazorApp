using Microsoft.EntityFrameworkCore;
using MyDataAccess.Entities;

namespace MyDataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Seed default roles
        //    modelBuilder.Entity<Role>().HasData(
        //        new Role { Id = 1, Name = "Admin" },
        //        new Role { Id = 2, Name = "Power User" },
        //        new Role { Id = 3, Name = "User" }
        //    );
        //}
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }
    }
}