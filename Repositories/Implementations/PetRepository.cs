using Microsoft.EntityFrameworkCore;
using PetCare;

public class PetRepository : BaseRepository<Pet>, IPetRepository
{
    public PetRepository(AppDbContext context) : base(context) { }

    /// <summary>
    /// Pobiera zwierzęta według gatunku.
    /// </summary>
    public async Task<IEnumerable<Pet>> GetPetsBySpeciesAsync(string type)
    {
        return await _context.Set<Pet>()
            .Where(p => p.Type == type)
            .Include(p => p.Owner)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    /// <summary>
    /// Pobiera zwierzęta z nadchodzącymi wizytami.
    /// </summary>
    public async Task<IEnumerable<Visit>> GetPetsWithUpcomingVisitsAsync()
    {
        var today = DateTime.Today;
        return await _context.Set<Visit>()
            .Include(v => v.PetId)
            .Where(v => v.VisitDate >= today)
            .OrderBy(v => v.VisitDate)
            .ToListAsync();
    }

    /// <summary>
    /// Wyszukuje zwierzęta według imienia właściciela.
    /// </summary>
    public async Task<IEnumerable<Pet>> FindPetsByOwnerNameNativeAsync(string ownerName)
    {
        return await _context.Pets
            .FromSqlRaw(@"
                SELECT p.* 
                FROM Pets p
                INNER JOIN Users u ON p.OwnerId = u.Id
                WHERE u.FirstName LIKE {0} OR u.LastName LIKE {0}
            ", $"%{ownerName}%")
            .ToListAsync();
    }
}