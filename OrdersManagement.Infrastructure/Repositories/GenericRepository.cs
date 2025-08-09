using Microsoft.EntityFrameworkCore;
using OrdersManagement.Domain.Repositories;
using System.Linq.Expressions;

namespace OrdersManagement.Infrastructure.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<(IEnumerable<TResult> Data, int TotalCount)> GetPagedAsync<TResult>(
        Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, TResult>>? selector = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int pageNumber = 1,
        int pageSize = 10,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        // Apply filter
        if (filter != null)
            query = query.Where(filter);

        // Apply includes
        if (includes != null)
            foreach (var include in includes)
                query = query.Include(include);

        // Apply order
        if (orderBy != null)
            query = orderBy(query);

        // Count before pagination
        var totalCount = await query.CountAsync();

        // Apply pagination
        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        // Apply selector (projection)
        if (selector != null)
            return (await query.Select(selector).ToListAsync(), totalCount);

        // If no selector, return as T but cast to TResult (must match)
        return ((IEnumerable<TResult>)await query.ToListAsync(), totalCount);
    }

    public async Task<T?> GetByIdAsync(object id) => await _dbSet.FindAsync(id);

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}
