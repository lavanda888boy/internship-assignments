namespace Hospital.Application.Abstractions
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
