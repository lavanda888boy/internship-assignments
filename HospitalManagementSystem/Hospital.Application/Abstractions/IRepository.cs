using System.Linq.Expressions;

namespace Hospital.Application.Abstractions
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> SearchByPropertyAsync(Expression<Func<T, bool>> entityPredicate);
        Task<List<T>> GetAllAsync();
    }
}
