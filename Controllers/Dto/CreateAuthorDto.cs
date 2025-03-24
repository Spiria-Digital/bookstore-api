using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Controllers.Dto
{
  public class CreateAuthorDto
  {
    [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required.")]
    public string FirstName { get; set; } = null!;
    [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required.")]
    public string LastName { get; set; } = null!;
    [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email is not valid.")]
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Address { get; set; }
  }
}