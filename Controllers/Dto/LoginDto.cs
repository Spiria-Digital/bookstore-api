using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Controllers.Dto
{
  public class LoginDto
  {
    [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [MinLength(5, ErrorMessage = "Email is too short.")]
    [MaxLength(100, ErrorMessage = "Email is too long.")]
    public string Email { get; set; } = null!;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
    [MinLength(8, ErrorMessage = "Password is too short.")]
    public string Password { get; set; } = null!;
  }
}