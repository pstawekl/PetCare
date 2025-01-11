using Microsoft.EntityFrameworkCore;
using PetCare;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<User> GetByEmailAsync(string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<IEnumerable<User>> FindByNameNativeAsync(string searchTerm)
    {
        return await _context.Users
            .FromSqlRaw(@"
                SELECT * FROM Users 
                WHERE FirstName LIKE {0} OR LastName LIKE {0}
            ", $"%{searchTerm}%")
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetUsersWithPetsAsync()
    {
        return await _context.Users
            .FromSqlRaw(@"
                SELECT u.* 
                FROM public.""Users"" u
                INNER JOIN public.""Pets"" p ON u.Id = p.OwnerId;
            ")
            .ToListAsync();
    }
}