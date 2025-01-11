using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCare;
namespace PetCare.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RemindersController : ControllerBase
    {
        private readonly IReminderRepository _reminderRepository;

        // Konstruktor kontrolera przypomnień
        public RemindersController(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        /// <summary>
        /// Pobiera wszystkie przypomnienia.
        /// </summary>
        /// <returns>Lista przypomnień.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllReminders()
        {
            var reminders = await _reminderRepository.GetAllAsync();
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
            await _reminderRepository.AddAsync(reminder);
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
            var existingReminder = await _reminderRepository.GetByIdAsync(id);
            if (existingReminder == null) return NotFound();

            existingReminder.Message = reminderCreateDto.Message;
            existingReminder.ReminderDate = reminderCreateDto.ReminderDate;
            existingReminder.IsSent = reminderCreateDto.IsSent;

            await _reminderRepository.UpdateAsync(existingReminder);
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
            var reminder = await _reminderRepository.GetByIdAsync(id);
            if (reminder == null) return NotFound();
            await _reminderRepository.DeleteAsync(reminder.Id);
            return NoContent();
        }
    }
}
