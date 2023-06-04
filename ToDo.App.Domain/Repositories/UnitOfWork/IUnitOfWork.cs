namespace ToDo.App.Domain.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        Task SaveAsync(CancellationToken cancellationToken);
        void Save();
        Task SaveAndDispatchEventsAsync(CancellationToken cancellationToken);


    }
}
