using PetCare;

public interface IReminderRepository : IBaseRepository<Reminder>
{
    /// <summary>
    /// Pobiera aktywne przypomnienia.
    /// </summary>
    Task<IEnumerable<Reminder>> GetActiveRemindersAsync();

    /// <summary>
    /// Pobiera przypomnienia według identyfikatora użytkownika.
    /// </summary>
    Task<IEnumerable<Reminder>> GetRemindersByUserIdNativeAsync(int userId);

    /// <summary>
    /// Pobiera nadchodzące przypomnienia dla zwierzęcia.
    /// </summary>
    Task<IEnumerable<Reminder>> GetUpcomingRemindersForPetAsync(int petId);
}