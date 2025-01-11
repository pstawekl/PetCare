using PetCare;

public interface IVisitRepository : IBaseRepository<Visit>
{
    Task<IEnumerable<Visit>> GetUpcomingVisitsAsync();
    Task<IEnumerable<Visit>> GetVisitsByVetNativeAsync(string vetName);
    Task<IEnumerable<Visit>> GetVisitsByPetIdAsync(int petId);
}