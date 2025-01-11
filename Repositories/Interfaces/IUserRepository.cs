using PetCare;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetByEmailAsync(string email);
    Task<IEnumerable<User>> FindByNameNativeAsync(string searchTerm);
    Task<IEnumerable<User>> GetUsersWithPetsAsync();
}