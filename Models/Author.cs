namespace BookStoreApi.Models
{
  public class Author
  {
    public int Id { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}