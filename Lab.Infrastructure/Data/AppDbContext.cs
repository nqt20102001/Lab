
using Lab.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<BookCatalog> BookCatalogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User - Cart: 1-n
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Book - Genre: n-1
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            // Decimal precision configuration
            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .HasPrecision(18, 2);  // decimal(18,2)

            modelBuilder.Entity<Cart>()
                .Property(c => c.TotalPrice)
                .HasPrecision(18, 2);  // decimal(18,2)

            modelBuilder.Entity<CartDetail>()
                .Property(cd => cd.Price)
                .HasPrecision(18, 2);  // decimal(18,2)

            // BookCatalog - Book & Catalog: n-n (Many-to-Many)
            modelBuilder.Entity<BookCatalog>()
                .HasKey(bc => new { bc.BookId, bc.CatalogId });

            modelBuilder.Entity<BookCatalog>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCatalogs)
                .HasForeignKey(bc => bc.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookCatalog>()
                .HasOne(bc => bc.Catalog)
                .WithMany(c => c.BookCatalogs)
                .HasForeignKey(bc => bc.CatalogId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cart - CartDetail: 1-n
            modelBuilder.Entity<CartDetail>()
                .HasOne(cd => cd.Cart)
                .WithMany(c => c.CartDetails)
                .HasForeignKey(cd => cd.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            // Book - CartDetail: 1-n
            modelBuilder.Entity<CartDetail>()
                .HasOne(cd => cd.Book)
                .WithMany(b => b.CartDetails)
                .HasForeignKey(cd => cd.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

    }
}
