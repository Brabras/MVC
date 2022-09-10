using Microsoft.EntityFrameworkCore;
using MVC.Models;
using System.Linq;

namespace MVC.Models
{
    public class LibContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<TakeBookInfo> TakeBookInfos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public LibContext(DbContextOptions<LibContext> options) : base(options) { }
    }
}
