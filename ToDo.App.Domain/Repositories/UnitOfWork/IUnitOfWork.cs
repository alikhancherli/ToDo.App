namespace ToDo.App.Domain.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        public ITodoRepository TodoRepository { get; }
        Task SaveAsync(CancellationToken cancellationToken);
        void Save();
        Task SaveAndDispatchEventsAsync(CancellationToken cancellationToken);
    }
}
