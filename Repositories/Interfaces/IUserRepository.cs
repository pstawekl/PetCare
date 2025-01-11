using PetCare;

public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    /// Pobiera użytkownika według adresu email.
    /// </summary>
    Task<User> GetByEmailAsync(string email);

    /// <summary>
    /// Wyszukuje użytkowników według imienia lub nazwiska.
    /// </summary>
    Task<IEnumerable<User>> FindByNameNativeAsync(string searchTerm);

    /// <summary>
    /// Pobiera użytkowników posiadających zwierzęta.
    /// </summary>
    Task<IEnumerable<User>> GetUsersWithPetsAsync();
}