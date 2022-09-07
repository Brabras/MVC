using Microsoft.EntityFrameworkCore;
using MVC.Models;
using System.Linq;

namespace MVC.Models
{
    public class ShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }
    }
}
