using PetCare;

public interface IPetRepository : IBaseRepository<Pet>
{
    /// <summary>
    /// Pobiera zwierzęta według gatunku.
    /// </summary>
    Task<IEnumerable<Pet>> GetPetsBySpeciesAsync(string species);

    /// <summary>
    /// Pobiera zwierzęta z nadchodzącymi wizytami.
    /// </summary>
    Task<IEnumerable<Visit>> GetPetsWithUpcomingVisitsAsync();

    /// <summary>
    /// Wyszukuje zwierzęta według imienia właściciela.
    /// </summary>
    Task<IEnumerable<Pet>> FindPetsByOwnerNameNativeAsync(string ownerName);
}