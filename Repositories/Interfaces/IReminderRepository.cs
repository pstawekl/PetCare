using PetCare;

public interface IReminderRepository : IBaseRepository<Reminder>
{
    Task<IEnumerable<Reminder>> GetActiveRemindersAsync();
    Task<IEnumerable<Reminder>> GetRemindersByUserIdNativeAsync(int userId);
    Task<IEnumerable<Reminder>> GetUpcomingRemindersForPetAsync(int petId);
}