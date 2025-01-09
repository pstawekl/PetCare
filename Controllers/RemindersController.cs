using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PetCare.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RemindersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RemindersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReminders()
        {
            var reminders = await _context.Reminders.ToListAsync();
            return Ok(reminders);
        }

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
