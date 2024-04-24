using System.Linq.Expressions;

namespace Hospital.Application.Abstractions
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> SearchByPropertyAsync(Expression<Func<T, bool>> entityPredicate);
        Task<List<T>> GetAllAsync();
    }
}
