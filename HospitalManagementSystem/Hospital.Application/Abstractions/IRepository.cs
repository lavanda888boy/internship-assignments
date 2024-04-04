namespace Hospital.Application.Abstractions
{
    public interface IRepository<T>
    {
        T Create(T entity);
        T? Update(T entity);
        bool Delete(T entity);
        T GetById(int id);
        List<T>? GetByProperty(Func<T, bool> entityPredicate);
        List<T> GetAll();
        int GetLastId();
    }
}
