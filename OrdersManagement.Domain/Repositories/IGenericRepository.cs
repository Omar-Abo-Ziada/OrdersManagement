using System.Linq.Expressions;

namespace OrdersManagement.Domain.Repositories;
public interface IGenericRepository<T> where T : class
{
    Task<(IEnumerable<TResult> Data, int TotalCount)> GetPagedAsync<TResult>(
        Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, TResult>>? selector = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int pageNumber = 1,
        int pageSize = 10,
        params Expression<Func<T, object>>[] includes);

    Task<T?> GetByIdAsync(object id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveAsync();
}
