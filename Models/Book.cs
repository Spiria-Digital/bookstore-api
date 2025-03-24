namespace BookStoreApi.Models
{
  public class Book
  {
    public int Id { get; set; }
    public required string ISBN { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public DateOnly PublishedDate { get; set; }
    public required string Language { get; set; }
    public required int Quantity { get; set; }

    // Foreign key for Author
    public int AuthorId { get; set; }
    public Author? Author { get; set; }

    // Foreign key for Genre
    public int GenreId { get; set; }
    public Genre? Genre { get; set; }

    // Foreign key for Publisher
    public int PublisherId { get; set; }
    public Publisher? Publisher { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}


