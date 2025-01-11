using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using PetCare;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    protected BaseRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        try
        {
            return await _dbSet.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Error retrieving {typeof(T).Name} entities", ex);
        }
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        try
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                throw new NotFoundException($"{typeof(T).Name} with id {id} not found");
            return entity;
        }
        catch (Exception ex) when (!(ex is NotFoundException))
        {
            throw new RepositoryException($"Error retrieving {typeof(T).Name}", ex);
        }
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Error adding {typeof(T).Name}", ex);
        }
    }

    public virtual async Task UpdateAsync(T entity)
    {
        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Error updating {typeof(T).Name}", ex);
        }
    }

    public virtual async Task DeleteAsync(int id)
    {
        try
        {
            var entity = await GetByIdAsync(id);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex) when (!(ex is NotFoundException))
        {
            throw new RepositoryException($"Error deleting {typeof(T).Name}", ex);
        }
    }

    protected virtual IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;
        return includes.Aggregate(query, (current, include) => current.Include(include));
    }
}