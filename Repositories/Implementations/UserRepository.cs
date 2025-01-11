using Microsoft.EntityFrameworkCore;
using PetCare;
using Serilog;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly Serilog.ILogger _logger;

    public UserRepository(AppDbContext context, Serilog.ILogger logger) : base(context)
    {
        _logger = logger;
    }

    /// <summary>
    /// Pobiera użytkownika według adresu email.
    /// </summary>
    public async Task<User> GetByEmailAsync(string username)
    {
        _logger.Information("Getting user by email: {Username}", username);
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    /// <summary>
    /// Wyszukuje użytkowników według imienia lub nazwiska.
    /// </summary>
    public async Task<IEnumerable<User>> FindByNameNativeAsync(string searchTerm)
    {
        _logger.Information("Finding users by name with search term: {SearchTerm}", searchTerm);
        return await _context.Users
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
        _logger.Information("Getting users with pets");
        return await _context.Users
            .FromSqlRaw(@"
                SELECT u.* 
                FROM public.""Users"" u
                INNER JOIN public.""Pets"" p ON u.Id = p.OwnerId;
            ")
            .ToListAsync();
    }
}