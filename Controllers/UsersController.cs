using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCare;

namespace PetCare.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Pobierz wszystkich użytkowników
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }

        // Zarejestruj nowego użytkownika
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
            await _userRepository.AddAsync(user);
            return CreatedAtAction(nameof(GetAllUsers), new { id = user.Id }, user);
        }

        // Zaktualizuj istniejącego użytkownika
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserCreateDto userCreateDto)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null) return NotFound();

            existingUser.Email = userCreateDto.Email;
            existingUser.Username = userCreateDto.Username;

            await _userRepository.UpdateAsync(existingUser);
            return NoContent();
        }

        // Usuń użytkownika
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return NotFound();
            await _userRepository.DeleteAsync(user.Id);
            return NoContent();
        }
    }
}
