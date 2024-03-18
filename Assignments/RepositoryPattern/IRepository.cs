namespace RepositoryPattern
{
    internal interface IRepository<T> where T : Entity
    {
        IEnumerable<T> FindAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
