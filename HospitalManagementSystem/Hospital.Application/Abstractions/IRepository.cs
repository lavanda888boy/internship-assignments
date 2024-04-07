namespace Hospital.Application.Abstractions
{
    public interface IRepository<T>
    {
        T Create(T entity);
        bool Update(T entity);
        bool Delete(int id);
        T GetById(int id);
        List<T>? SearchByProperty(Func<T, bool> entityPredicate);
        List<T> GetAll();
    }
}
