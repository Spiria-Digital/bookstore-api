using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Controllers.Dto
{
  public class UpdateAuthorDto
  {
    [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required.")]
    public string? FirstName { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required.")]
    public string? LastName { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string? Email { get; set; }

    public string? Phone { get; set; }
    public string? Address { get; set; }
  }
}