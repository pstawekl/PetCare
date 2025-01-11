using Microsoft.EntityFrameworkCore;
using PetCare;

public class VisitRepository : BaseRepository<Visit>, IVisitRepository
{
    public VisitRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Visit>> GetUpcomingVisitsAsync()
    {
        return await _context.Visits
            .Where(v => v.VisitDate >= DateTime.Today)
            .Include(v => v.PetId)
            .OrderBy(v => v.VisitDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Visit>> GetVisitsByVetNativeAsync(string vetName)
    {
        return await _context.Visits
            .FromSqlRaw(@"
                SELECT v.* 
                FROM Visits v
                WHERE v.VetName LIKE {0}
            ", $"%{vetName}%")
            .ToListAsync();
    }

    public async Task<IEnumerable<Visit>> GetVisitsByPetIdAsync(int petId)
    {
        return await _context.Visits
            .Where(v => v.PetId == petId)
            .OrderByDescending(v => v.VisitDate)
            .ToListAsync();
    }
}