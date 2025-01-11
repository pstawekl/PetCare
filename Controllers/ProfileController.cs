using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;

namespace PetCare.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly Serilog.ILogger _logger;

        // Konstruktor kontrolera wizyt
        public ProfileController(AppDbContext context, Serilog.ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Pobiera profil użytkownika.
        /// </summary>
        /// <param name="id">Id użytkownika.</param>
        /// <param name="dbContext">Kontekst bazy danych.</param>
        /// <returns>Profil użytkownika.</returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> GetProfile(int id, [FromServices] AppDbContext dbContext)
        {
            _logger.Information("Getting profile for user ID: {UserId}", id);
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (existingUser != null)
            {
                return Ok(new
                {
                    Id = existingUser.Id,
                    Email = existingUser.Email,
                    Username = existingUser.Username
                });
            }
            _logger.Warning("User profile not found for ID: {UserId}", id);
            return NotFound();
        }

        /// <summary>
        /// Aktualizuje profil użytkownika.
        /// </summary>
        /// <param name="updateProfile">Zaktualizowane dane profilu.</param>
        /// <returns>Komunikat o sukcesie.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] UpdateProfileDto updateProfile, [FromServices] AppDbContext dbContext)
        {
            _logger.Information("Updating profile for user ID: {UserId}", id);
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (existingUser != null)
            {
                existingUser.Username = updateProfile.Name;
                existingUser.Email = updateProfile.Email;

                await dbContext.SaveChangesAsync();

                _logger.Information("Profile updated successfully for user ID: {UserId}", id);
                return Ok(new { Message = "Profil zaktualizowany pomyślnie" });
            }
            _logger.Warning("User profile not found for ID: {UserId}", id);
            return NotFound();
        }
    }

    // DTO do aktualizacji profilu
    public class UpdateProfileDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
