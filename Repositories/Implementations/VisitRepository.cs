using Microsoft.EntityFrameworkCore;
using PetCare;

public class VisitRepository : BaseRepository<Visit>, IVisitRepository
{
    public VisitRepository(AppDbContext context) : base(context) { }

    /// <summary>
    /// Pobiera nadchodzące wizyty.
    /// </summary>
    public async Task<IEnumerable<Visit>> GetUpcomingVisitsAsync()
    {
        return await _dbSet
            .Where(v => v.VisitDate >= DateTime.Today)
            .Include(v => v.PetId)
            .OrderBy(v => v.VisitDate)
            .ToListAsync();
    }

    /// <summary>
    /// Pobiera wizyty według imienia weterynarza.
    /// </summary>
    public async Task<IEnumerable<Visit>> GetVisitsByVetNativeAsync(string vetName)
    {
        return await _dbSet
            .FromSqlRaw(@"
                SELECT v.* 
                FROM Visits v
                WHERE v.VetName LIKE {0}
            ", $"%{vetName}%")
            .ToListAsync();
    }

    /// <summary>
    /// Pobiera wizyty według identyfikatora zwierzęcia.
    /// </summary>
    public async Task<IEnumerable<Visit>> GetVisitsByPetIdAsync(int petId)
    {
        return await _dbSet
            .Where(v => v.PetId == petId)
            .OrderByDescending(v => v.VisitDate)
            .ToListAsync();
    }
}