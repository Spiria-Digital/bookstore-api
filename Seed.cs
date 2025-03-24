namespace BookStoreApi;

using BookStoreApi.Models;
using Microsoft.EntityFrameworkCore;


public static class SeedData
{

  public static void SeedDatabase(BookStoreDbContext context)
  {
    // context.Database.Migrate();
    SeedUsers(context);
    SeedPublishers(context);
    SeedAuthors(context);
    SeedGenre(context);
    SeedBooks(context);
    context.SaveChanges();
  }

  public static void SeedUsers(BookStoreDbContext context)
  {
    // Look for any Users
    if (context.Users.Any())
    {
      return; // DB has been seeded
    }

    var users = new List<User>
      {
        new() {
          FirstName = "Admin",
          LastName = "User",
          Password = BCrypt.Net.BCrypt.HashPassword("secret@123!"),
          Email = "admin@company.com",
          Role = "Admin",
          Address = "123 Admin St, Admin City, Admin Country",
          Phone = "1234567890",
          CreatedAt = DateTime.Now,
          UpdatedAt = DateTime.Now
        }
      };

    context.Users.AddRange(users);
    context.SaveChanges();
  }

  public static void SeedPublishers(BookStoreDbContext context)
  {
    // Look for any Publishers
    if (context.Publishers.Any())
    {
      return; // DB has been seeded
    }

    var publishers = new List<Publisher>
      {
        new() {
          Name = "Atlas Books",
          URL = "https://atlasbooks.com",
          APIKey = "1234567890",
          Description = "Classic and modern literature books",
          Address = "123 Publisher St, Toronto, ON, M1M 1L1",
          Phone = "1234567890",
          Email = "atlas.books@publisher.com",
          CreatedAt = DateTime.Now,
          UpdatedAt = DateTime.Now
        },
      };

    context.Publishers.AddRange(publishers);
    context.SaveChanges();
  }

  public static void SeedAuthors(BookStoreDbContext context)
  {
    // Look for any Authors
    if (context.Authors.Any())
    {
      return; // DB has been seeded
    }

    var authors = new List<Author>
      {
        new() {
          FirstName = "Plato",
          LastName = "Plato",
          Email = "plato@amazon.com",
          Phone = "1234567890",
          Address = "123 Plato St, Plato City, Plato Country",
          CreatedAt = DateTime.Now,
          UpdatedAt = DateTime.Now
        },
      };

    context.Authors.AddRange(authors);
    context.SaveChanges();
  }

  public static void SeedGenre(BookStoreDbContext context)
  {
    if (context.Genres.Any())
    {
      return; // DB has been seeded
    }

    var genres = new List<Genre>
    {
      new(){
        Name = "Fiction",
        Description = "Fiction genre",
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
      }
    };

    context.Genres.AddRange(genres);
    context.SaveChanges();
  }

  public static void SeedBooks(BookStoreDbContext context)
  {
    // Look for any Books
    if (context.Books.Any())
    {
      return; // DB has been seeded
    }

    // get the first author
    var author = context.Authors.First()!;
    var publisher = context.Publishers.First()!;
    var genre = context.Genres.First()!;

    var books = new List<Book>
      {
        new() {
          Title = "The Republic",
          AuthorId = author.Id,
          ISBN = " 978-1503379985",
          PublishedDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-3650)),
          PublisherId = publisher.Id,
          Price =  19.99M,
          GenreId = genre.Id,
          Description = "Book 1 Description",
          Language = "English",
          Quantity = 6,
          CreatedAt = DateTime.Now,
          UpdatedAt = DateTime.Now
        },
        new() {
          Title = "Apology",
          AuthorId = author.Id,
          Quantity = 10,
          ISBN = "978-1774264362",
          PublishedDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-365*2)),
          PublisherId = publisher.Id,
          Price = 9.99M,
          GenreId = genre.Id,
          Description = "Book 2 Description",
          Language = "English",
          CreatedAt = DateTime.Now,
          UpdatedAt = DateTime.Now
        },
      };

    context.Books.AddRange(books);
    context.SaveChanges();
  }
}
