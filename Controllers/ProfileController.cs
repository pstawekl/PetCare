using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace PetCare.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Konstruktor kontrolera wizyt
        public ProfileController(AppDbContext context)
        {
            _context = context;
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
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (existingUser != null)
            {
                existingUser.Username = updateProfile.Name;
                existingUser.Email = updateProfile.Email;

                await dbContext.SaveChangesAsync();

                return Ok(new { Message = "Profil zaktualizowany pomyślnie" });
            }
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
