using Microsoft.EntityFrameworkCore;
using PetCare;

public class ReminderRepository : BaseRepository<Reminder>, IReminderRepository
{
    public ReminderRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Reminder>> GetActiveRemindersAsync()
    {
        return await _context.Reminders
            .Where(r => !r.IsSent && r.ReminderDate >= DateTime.Today)
            .Include(r => r.PetId)
            .OrderBy(r => r.ReminderDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reminder>> GetRemindersByUserIdNativeAsync(int userId)
    {
        return await _context.Reminders
            .FromSqlRaw(@"
                SELECT r.* 
                FROM Reminders r
                INNER JOIN Pets p ON r.PetId = p.Id
                WHERE p.OwnerId = {0}
            ", userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reminder>> GetUpcomingRemindersForPetAsync(int petId)
    {
        return await _context.Reminders
            .Where(r => r.PetId == petId && r.ReminderDate >= DateTime.Today)
            .OrderBy(r => r.ReminderDate)
            .ToListAsync();
    }
}