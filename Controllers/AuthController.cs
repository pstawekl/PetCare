using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCare;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenService _jwtTokenService;


    public AuthController(AppDbContext context, JwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;

        // Ensure the JWT secret key is at least 128 bits (16 characters)
        if (_jwtTokenService.SecretKey.Length < 16)
        {
            throw new ArgumentOutOfRangeException("SecretKey", "The encryption algorithm 'HS256' requires a key size of at least 128 bits (16 characters).");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login, [FromServices] AppDbContext dbContext)
    {
        var user = await GetUser(login.Username, dbContext);

        if (user != null)
        {
            if (BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
            {
                var accessToken = _jwtTokenService.GenerateAccessToken(user.Id.ToString(), user.Username);
                var refreshToken = _jwtTokenService.GenerateRefreshToken(user.Id.ToString(), user.Username);

                return Ok(new
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                });
            }
        }

        return Unauthorized(new { Message = "Invalid username or password" });
    }

    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto, [FromServices] AppDbContext dbContext)
    {
        // Sprawdź, czy użytkownik już istnieje
        var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == registerDto.Username);
        if (existingUser != null)
        {
            return Conflict(new { Message = "User already exists" });
        }

        // Zaszyfruj hasło
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

        // Utwórz nowego użytkownika
        var user = new User
        {
            Username = registerDto.Username,
            PasswordHash = passwordHash
        };

        // Zapisz użytkownika w bazie danych
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        return Ok(new { Message = "User registered successfully" });
    }




    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto user, [FromServices] AppDbContext dbContext)
    {
        if (user != null && _jwtTokenService.ValidateToken(user.RefreshToken) != null)
        {
            var newAccessToken = _jwtTokenService.GenerateAccessToken(user.Id.ToString(), user.Username);
            var newRefreshToken = _jwtTokenService.GenerateRefreshToken(user.Id.ToString(), user.Username);

            user.RefreshToken = newRefreshToken;

            return Ok(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        return Unauthorized(new { Message = "Invalid refresh token" });
    }
    private async Task<User> GetUser(string username, AppDbContext dbContext)
    {
        var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);

        if (existingUser != null)
        {
            var user = new User
            {
                Email = existingUser.Email,
                Id = existingUser.Id,
                PasswordHash = existingUser.PasswordHash,
                Username = existingUser.Username
            };
            return user;
        }
        return null;
    }

    [HttpPost("verify")]
    public IActionResult VerifyToken([FromBody] string token)
    {
        var principal = _jwtTokenService.ValidateToken(token);
        if (principal != null)
        {
            return Ok(new { Message = "Token is valid" });
        }
        return Unauthorized(new { Message = "Invalid token" });
    }
}

public class LoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class RegisterDto
{
    [Required]
    [MaxLength(100)]
    public string Username { get; set; }

    [Required]
    [MaxLength(100)]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }
}


public class RefreshTokenDto
{
    public string RefreshToken { get; set; }
    public int Id { get; set; }
    public string Username { get; set; }
}