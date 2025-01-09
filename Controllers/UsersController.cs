using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PetCare.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserCreateDto userCreateDto)
        {
            var user = new User
            {
                Email = userCreateDto.Email,
                Username = userCreateDto.Username,
                PasswordHash = userCreateDto.PasswordHash
            };
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllUsers), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserCreateDto userCreateDto)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null) return NotFound();

            var user = new User
            {
                Id = id,
                Email = userCreateDto.Email,
                Username = userCreateDto.Username,
                PasswordHash = userCreateDto.PasswordHash
            };

            existingUser.Email = user.Email;
            existingUser.Username = user.Username;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
