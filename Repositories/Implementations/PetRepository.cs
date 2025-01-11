using Microsoft.EntityFrameworkCore;
using PetCare;

public class PetRepository : BaseRepository<Pet>, IPetRepository
{
    public PetRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Pet>> GetPetsBySpeciesAsync(string type)
    {
        return await _context.Pets
            .Where(p => p.Type == type)
            .Include(p => p.Owner)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Visit>> GetPetsWithUpcomingVisitsAsync()
    {
        var today = DateTime.Today;
        return await _context.Visits
            .Include(v => v.PetId)
            .Where(v => v.VisitDate >= today)
            .OrderBy(v => v.VisitDate)
            .ToListAsync();
    }

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