using AspnetCoreWebApiProjectPractice.Models;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreWebApiProjectPractice.Context
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }



    }

}
