using Microsoft.EntityFrameworkCore;
using BookStoreApi.Models;

namespace BookStoreApi.Models
{
  public class BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : DbContext(options)
  {
    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Publisher> Publishers { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
      public DbSet<BookStoreApi.Models.Order> Order { get; set; } = default!;
  }
}