using BooksStore_RazorTemp.Model;
using Microsoft.EntityFrameworkCore;

namespace BooksStore_RazorTemp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                
                new Category {Id=1, Name="Action", DisplayOrders=1},
                new Category { Id = 2, Name ="Sifi", DisplayOrders=2},
                new Category { Id = 3, Name ="Thrill", DisplayOrders=3}
                );
        }
    }
}
