using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCare;
using Serilog;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenService _jwtTokenService;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AuthController> _logger;
    private readonly AppDbContext _context;

    public AuthController(
        JwtTokenService jwtTokenService, 
        IUserRepository userRepository, 
        ILogger<AuthController> logger,
        AppDbContext context)
    {
        _jwtTokenService = jwtTokenService;
        _userRepository = userRepository;
        _logger = logger;
        _context = context;

        if (_jwtTokenService.SecretKey.Length < 16)
        {
            throw new ArgumentOutOfRangeException("SecretKey", "Algorytm szyfrowania 'HS256' wymaga klucza o rozmiarze co najmniej 128 bitów (16 znaków).");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        _logger.LogInformation("User login attempt: {Username}", login.Username);
        try
        {
            var user = await _userRepository.GetByEmailAsync(login.Username);
            if (user != null && BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
            {
                var accessToken = _jwtTokenService.GenerateAccessToken(user.Id.ToString(), user.Username);
                var refreshToken = _jwtTokenService.GenerateRefreshToken(user.Id.ToString(), user.Username);

                _logger.LogInformation("User login successful: {Username}", login.Username);
                return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
            }

            _logger.LogWarning("User login failed: {Username}", login.Username);
            return Unauthorized(new { Message = "Nieprawidłowa nazwa użytkownika lub hasło" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for user {Username}", login.Username);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        _logger.LogInformation("User registration attempt: {Username}", registerDto.Username);
        try
        {
            var existingUser = await _userRepository.GetByEmailAsync(registerDto.Username);
            if (existingUser != null)
            {
                _logger.LogWarning("User registration failed - user exists: {Username}", registerDto.Username);
                return Conflict(new { Message = "Użytkownik już istnieje" });
            }

            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("User registered successfully: {Username}", registerDto.Username);
            return Ok(new { Message = "Użytkownik zarejestrowany pomyślnie" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration for user {Username}", registerDto.Username);
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Odświeża token dostępu.
    /// </summary>
    /// <param name="user">Dane użytkownika.</param>
    /// <returns>Nowe tokeny dostępu i odświeżenia.</returns>
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto user)
    {
        _logger.LogInformation("Token refresh attempt for user: {Username}", user.Username);
        if (user != null && _jwtTokenService.ValidateToken(user.RefreshToken) != null)
        {
            var newAccessToken = _jwtTokenService.GenerateAccessToken(user.Id.ToString(), user.Username);
            var newRefreshToken = _jwtTokenService.GenerateRefreshToken(user.Id.ToString(), user.Username);

            user.RefreshToken = newRefreshToken;

            _logger.LogInformation("Token refresh successful for user: {Username}", user.Username);
            return Ok(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        _logger.LogWarning("Token refresh failed for user: {Username}", user.Username);
        return Unauthorized(new { Message = "Nieprawidłowy token odświeżenia" });
    }

    // Pobiera użytkownika na podstawie nazwy użytkownika
    private async Task<User> GetUser(string username)
    {
        _logger.LogInformation("Getting user by username: {Username}", username);
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

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

    /// <summary>
    /// Weryfikuje token.
    /// </summary>
    /// <param name="token">Token do weryfikacji.</param>
    /// <returns>Komunikat o ważności tokenu.</returns>
    [HttpPost("verify")]
    public IActionResult VerifyToken([FromBody] string token)
    {
        _logger.LogInformation("Verifying token");
        var principal = _jwtTokenService.ValidateToken(token);
        if (principal != null)
        {
            _logger.LogInformation("Token is valid");
            return Ok(new { Message = "Token jest ważny" });
        }
        _logger.LogWarning("Invalid token");
        return Unauthorized(new { Message = "Nieprawidłowy token" });
    }
}

// DTO do logowania
public class LoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}

// DTO do rejestracji
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

// DTO do odświeżania tokenu
public class RefreshTokenDto
{
    public string RefreshToken { get; set; }
    public int Id { get; set; }
    public string Username { get; set; }
}