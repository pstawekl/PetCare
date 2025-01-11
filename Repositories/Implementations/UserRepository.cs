using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetCare;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(AppDbContext context, ILogger<UserRepository> logger) : base(context)
    {
        _logger = logger;
    }

    /// <summary>
    /// Pobiera użytkownika według adresu email.
    /// </summary>
    public async Task<User> GetByEmailAsync(string username)
    {
        _logger.LogInformation("Getting user by email: {Username}", username);
        return await _dbSet
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    /// <summary>
    /// Wyszukuje użytkowników według imienia lub nazwiska.
    /// </summary>
    public async Task<IEnumerable<User>> FindByNameNativeAsync(string searchTerm)
    {
        _logger.LogInformation("Finding users by name with search term: {SearchTerm}", searchTerm);
        return await _dbSet
            .FromSqlRaw(@"
                SELECT * FROM Users 
                WHERE FirstName LIKE {0} OR LastName LIKE {0}
            ", $"%{searchTerm}%")
            .ToListAsync();
    }

    /// <summary>
    /// Pobiera użytkowników posiadających zwierzęta.
    /// </summary>
    public async Task<IEnumerable<User>> GetUsersWithPetsAsync()
    {
        _logger.LogInformation("Getting users with pets");
        return await _dbSet
            .FromSqlRaw(@"
                SELECT u.* 
                FROM public.""Users"" u
                INNER JOIN public.""Pets"" p ON u.Id = p.OwnerId;
            ")
            .ToListAsync();
    }
}