using BooksApp.Data.Concrete.EfCore.Config;
using BooksApp.Data.Concrete.EfCore.Extensions;
using BooksApp.Entity.Concrete;
using BooksApp.Entity.Concrete.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Data.Concrete.EfCore.Context
{
    public class BooksAppContext : IdentityDbContext<User,Role,string>
    {
        public BooksAppContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedData();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfig).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}