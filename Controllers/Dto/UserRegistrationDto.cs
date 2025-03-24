namespace BookStoreApi.Controllers.Dto
{
  public class UserRegistrationDto
  {
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Address { get; set; }
  }
}