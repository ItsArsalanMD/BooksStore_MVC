using Books.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BooksWeb.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id=1, Name="Action", DisplayOrders=1 },
                new Category { Id=2, Name="Action", DisplayOrders=2 },
                new Category { Id=3, Name="Action", DisplayOrders=3 }
                );
        }
    }
}
