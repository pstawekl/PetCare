public interface IBaseRepository<T> where T : class
{
    /// <summary>
    /// Pobiera wszystkie elementy.
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Pobiera element według identyfikatora.
    /// </summary>
    Task<T> GetByIdAsync(int id);

    /// <summary>
    /// Dodaje nowy element.
    /// </summary>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Aktualizuje istniejący element.
    /// </summary>
    Task UpdateAsync(T entity);

    /// <summary>
    /// Usuwa element według identyfikatora.
    /// </summary>
    Task DeleteAsync(int id);
}