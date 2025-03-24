namespace BookStoreApi.Controllers.Dto
{
  public class GetBookDto
  {
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? Isbn { get; set; }
    public int AuthorId { get; set; }
    public int GenreId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? ImageUrl { get; set; }
    public string? Publisher { get; set; }
    public string? Language { get; set; }
    public int? Pages { get; set; }
    public string? Dimensions { get; set; }
    public string? Weight { get; set; }
    public string? Format { get; set; }
    public string? Edition { get; set; }
    public string? Location { get; set; }
    public string? Status { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
  }
}