using Microsoft.EntityFrameworkCore;

namespace MVC.Models
{
    public class MobileContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public MobileContext(DbContextOptions<MobileContext> options) : base(options) { }
    }
}
