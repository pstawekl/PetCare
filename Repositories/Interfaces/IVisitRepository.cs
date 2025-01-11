using PetCare;

public interface IVisitRepository : IBaseRepository<Visit>
{
    /// <summary>
    /// Pobiera nadchodzące wizyty.
    /// </summary>
    Task<IEnumerable<Visit>> GetUpcomingVisitsAsync();

    /// <summary>
    /// Pobiera wizyty według imienia weterynarza.
    /// </summary>
    Task<IEnumerable<Visit>> GetVisitsByVetNativeAsync(string vetName);

    /// <summary>
    /// Pobiera wizyty według identyfikatora zwierzęcia.
    /// </summary>
    Task<IEnumerable<Visit>> GetVisitsByPetIdAsync(int petId);
}