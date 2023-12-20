using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodDelieveryManagementAPI.Data
{
    public class ApiDbContext : IdentityDbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> option) : base(option) { }
        public DbSet<AppUser> UserDetails { get; set; }
        public DbSet<MenuProduct> MenuProducts { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<Order>Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
