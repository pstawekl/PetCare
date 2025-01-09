using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PetCare.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RemindersController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Konstruktor kontrolera przypomnień
        public RemindersController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Pobiera wszystkie przypomnienia.
        /// </summary>
        /// <returns>Lista przypomnień.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllReminders()
        {
            var reminders = await _context.Reminders.ToListAsync();
            return Ok(reminders);
        }

        /// <summary>
        /// Dodaje nowe przypomnienie.
        /// </summary>
        /// <param name="reminderCreateDto">Przypomnienie do dodania.</param>
        /// <returns>Utworzone przypomnienie.</returns>
        [HttpPost]
        public async Task<IActionResult> AddReminder([FromBody] ReminderCreateDto reminderCreateDto)
        {
            var reminder = new Reminder
            {
                PetId = reminderCreateDto.PetId,
                Message = reminderCreateDto.Message,
                ReminderDate = reminderCreateDto.ReminderDate,
                IsSent = reminderCreateDto.IsSent
            };
            await _context.Reminders.AddAsync(reminder);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllReminders), new { id = reminder.Id }, reminder);
        }

        /// <summary>
        /// Aktualizuje istniejące przypomnienie.
        /// </summary>
        /// <param name="id">ID przypomnienia do aktualizacji.</param>
        /// <param name="reminderCreateDto">Zaktualizowane dane przypomnienia.</param>
        /// <returns>Brak zawartości.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReminder(int id, [FromBody] ReminderCreateDto reminderCreateDto)
        {
            var existingReminder = await _context.Reminders.FindAsync(id);
            if (existingReminder == null) return NotFound();

            var reminder = new Reminder
            {
                Id = id,
                IsSent = reminderCreateDto.IsSent,
                Message = reminderCreateDto.Message,
                PetId = reminderCreateDto.PetId,
                ReminderDate = reminderCreateDto.ReminderDate
            };

            existingReminder.Message = reminder.Message;
            existingReminder.ReminderDate = reminder.ReminderDate;
            existingReminder.IsSent = reminder.IsSent;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Usuwa przypomnienie.
        /// </summary>
        /// <param name="id">ID przypomnienia do usunięcia.</param>
        /// <returns>Brak zawartości.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReminder(int id)
        {
            var reminder = await _context.Reminders.FindAsync(id);
            if (reminder == null) return NotFound();
            _context.Reminders.Remove(reminder);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
