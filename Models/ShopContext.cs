using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Models
{
    public class ShopContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }

        public DbSet<MVC.Models.Brand> Brand { get; set; }

        public DbSet<MVC.Models.Category> Category { get; set; }
    }
}
