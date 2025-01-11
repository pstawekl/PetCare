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
    private readonly UserRepository _userRepository;
    private readonly Serilog.ILogger _logger;

    // Konstruktor kontrolera autoryzacji
    public AuthController(AppDbContext context, JwtTokenService jwtTokenService, UserRepository userRepository, Serilog.ILogger logger)
    {
        _jwtTokenService = jwtTokenService;
        _userRepository = userRepository;
        _logger = logger;

        // Upewnij się, że tajny klucz JWT ma co najmniej 128 bitów (16 znaków)
        if (_jwtTokenService.SecretKey.Length < 16)
        {
            throw new ArgumentOutOfRangeException("SecretKey", "Algorytm szyfrowania 'HS256' wymaga klucza o rozmiarze co najmniej 128 bitów (16 znaków).");
        }
    }

    /// <summary>
    /// Logowanie użytkownika.
    /// </summary>
    /// <param name="login">Dane logowania.</param>
    /// <param name="dbContext">Kontekst bazy danych.</param>
    /// <returns>Tokeny dostępu i odświeżenia.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login, [FromServices] AppDbContext dbContext)
    {
        _logger.Information("User login attempt: {Username}", login.Username);
        var user = await _userRepository.GetByEmailAsync(login.Username);

        if (user != null)
        {
            if (BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
            {
                var accessToken = _jwtTokenService.GenerateAccessToken(user.Id.ToString(), user.Username);
                var refreshToken = _jwtTokenService.GenerateRefreshToken(user.Id.ToString(), user.Username);

                _logger.Information("User login successful: {Username}", login.Username);
                return Ok(new
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                });
            }
        }

        _logger.Warning("User login failed: {Username}", login.Username);
        return Unauthorized(new { Message = "Nieprawidłowa nazwa użytkownika lub hasło" });
    }

    /// <summary>
    /// Rejestracja nowego użytkownika.
    /// </summary>
    /// <param name="registerDto">Dane rejestracyjne.</param>
    /// <param name="dbContext">Kontekst bazy danych.</param>
    /// <returns>Komunikat o sukcesie.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto, [FromServices] AppDbContext dbContext)
    {
        _logger.Information("User registration attempt: {Username}", registerDto.Username);
        // Sprawdź, czy użytkownik już istnieje
        var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == registerDto.Username);
        if (existingUser != null)
        {
            _logger.Warning("User registration failed - user already exists: {Username}", registerDto.Username);
            return Conflict(new { Message = "Użytkownik już istnieje" });
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

        _logger.Information("User registered successfully: {Username}", registerDto.Username);
        return Ok(new { Message = "Użytkownik zarejestrowany pomyślnie" });
    }

    /// <summary>
    /// Odświeża token dostępu.
    /// </summary>
    /// <param name="user">Dane użytkownika.</param>
    /// <param name="dbContext">Kontekst bazy danych.</param>
    /// <returns>Nowe tokeny dostępu i odświeżenia.</returns>
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto user, [FromServices] AppDbContext dbContext)
    {
        _logger.Information("Token refresh attempt for user: {Username}", user.Username);
        if (user != null && _jwtTokenService.ValidateToken(user.RefreshToken) != null)
        {
            var newAccessToken = _jwtTokenService.GenerateAccessToken(user.Id.ToString(), user.Username);
            var newRefreshToken = _jwtTokenService.GenerateRefreshToken(user.Id.ToString(), user.Username);

            user.RefreshToken = newRefreshToken;

            _logger.Information("Token refresh successful for user: {Username}", user.Username);
            return Ok(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        _logger.Warning("Token refresh failed for user: {Username}", user.Username);
        return Unauthorized(new { Message = "Nieprawidłowy token odświeżenia" });
    }

    // Pobiera użytkownika na podstawie nazwy użytkownika
    private async Task<User> GetUser(string username, AppDbContext dbContext)
    {
        _logger.Information("Getting user by username: {Username}", username);
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

    /// <summary>
    /// Weryfikuje token.
    /// </summary>
    /// <param name="token">Token do weryfikacji.</param>
    /// <returns>Komunikat o ważności tokenu.</returns>
    [HttpPost("verify")]
    public IActionResult VerifyToken([FromBody] string token)
    {
        _logger.Information("Verifying token");
        var principal = _jwtTokenService.ValidateToken(token);
        if (principal != null)
        {
            _logger.Information("Token is valid");
            return Ok(new { Message = "Token jest ważny" });
        }
        _logger.Warning("Invalid token");
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