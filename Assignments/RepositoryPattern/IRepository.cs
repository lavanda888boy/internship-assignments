namespace RepositoryPattern
{
    internal interface IRepository<T> where T : Entity
    {
        IEnumerable<T> FindAll(Func<T, bool> pred);
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
