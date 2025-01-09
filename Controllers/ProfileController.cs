namespace PetCare.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IActionResult GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(new
            {
                UserId = userId,
                Name = "John Doe",
                Email = "johndoe@example.com"
            });
        }

        [Authorize]
        [HttpPut]
        public IActionResult UpdateProfile([FromBody] UpdateProfileDto updateProfile)
        {
            // Przykładowa logika aktualizacji danych użytkownika
            return Ok(new { Message = "Profile updated successfully" });
        }
    }

    public class UpdateProfileDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

}
