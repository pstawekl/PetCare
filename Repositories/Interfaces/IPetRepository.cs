using PetCare;

public interface IPetRepository : IBaseRepository<Pet>
{
    Task<IEnumerable<Pet>> GetPetsBySpeciesAsync(string species);
    Task<IEnumerable<Visit>> GetPetsWithUpcomingVisitsAsync();
    Task<IEnumerable<Pet>> FindPetsByOwnerNameNativeAsync(string ownerName);
}