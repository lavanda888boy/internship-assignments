using Hospital.Application.Common;
using System.Linq.Expressions;

namespace Hospital.Application.Abstractions
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<PaginatedResult<T>> SearchByPropertyPaginatedAsync(Expression<Func<T, bool>> entityPredicate, int pageNumber, int pageSize);
        Task<List<T>> GetAllAsync();
        Task<PaginatedResult<T>> GetAllPaginatedAsync(int pageNumber, int pageSize);
    }
}
