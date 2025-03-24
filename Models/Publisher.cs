namespace BookStoreApi.Models
{
  public class Publisher
  {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string URL { get; set; }
    public required string APIKey { get; set; }
    public string? Description { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}