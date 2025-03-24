using BookStoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using BookStoreApi.Controllers.Dto;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using BCrypt;

namespace BookStoreApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(BookStoreDbContext context, IConfiguration configuration) : ControllerBase
{
  private readonly BookStoreDbContext _context = context;
  private readonly string _jwtSecret = configuration["Jwt:Key"] ?? "BookStoreApiSecret";
  private readonly string _jwtIssuer = configuration["Jwt:Issuer"] ?? "BookStoreApi";
  private readonly string _jwtAudience = configuration["Jwt:Audience"] ?? "BookStoreApi";
  private readonly string _jwtExpiryHours = configuration["Jwt:ExpiryHours"] ?? "30";

  [HttpPost("login")]
  public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto loginDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(loginDto.Email, StringComparison.CurrentCultureIgnoreCase));
    if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
    {
      return Unauthorized();
    }

    var token = GenerateJwtToken(user);
    return Ok(new LoginResponseDto { Token = token });
  }

  [HttpPost("register")]
  public async Task<ActionResult<LoginResponseDto>> Register([FromBody] UserRegistrationDto registerDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(registerDto.Email, StringComparison.CurrentCultureIgnoreCase));
    if (existingUser != null)
    {
      return BadRequest("User already exists");
    }

    var user = new User
    {
      Email = registerDto.Email,
      FirstName = registerDto.FirstName,
      LastName = registerDto.LastName,
      Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
      Role = "User",
      Phone = registerDto.Phone,
      Address = registerDto.Address,
      CreatedAt = DateTime.UtcNow,
    };

    _context.Users.Add(user);
    await _context.SaveChangesAsync();

    try
    {
      var token = GenerateJwtToken(user);
      return CreatedAtAction(nameof(Login), new { email = user.Email }, new LoginResponseDto { Token = token });
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }

  private string GenerateJwtToken(User user)
  {
    Console.WriteLine("Generating token for user: " + user.Id.ToString() + " - " + user.Email);
    Console.WriteLine("Secret: " + _jwtSecret);
    Console.WriteLine("Issuer: " + _jwtIssuer);
    Console.WriteLine("Audience: " + _jwtAudience);
    Console.WriteLine("Expiry: " + _jwtExpiryHours);
    try
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.UTF8.GetBytes(_jwtSecret);

      var now = DateTime.UtcNow;
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(
        [
          new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
          new Claim(ClaimTypes.Role, user.Role)
        ]),
        Issuer = _jwtIssuer,
        Audience = _jwtAudience,
        Claims = new Dictionary<string, object>
        {
          { "id", user.Id },
          { "email", user.Email },
          { "role", user.Role }
        },
        IssuedAt = now,
        NotBefore = now,
        Expires = DateTime.UtcNow.AddDays(30),  // change this later
        SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(key),
          SecurityAlgorithms.HmacSha256Signature
        )
      };
      var jwtToken = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(jwtToken);
    }
    catch (Exception ex)
    {
      throw new Exception("Error generating token", ex);
    }
  }
}
