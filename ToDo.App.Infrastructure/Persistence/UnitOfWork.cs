using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ToDo.App.Domain.Repositories;
using ToDo.App.Domain.Repositories.UnitOfWork;
using ToDo.App.Infrastructure.Persistence.Repositories;
using ToDo.App.Shared.Domain;

namespace ToDo.App.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private ITodoRepository _todoRepository;

        private readonly IMediator _mediator;

        public UnitOfWork(AppDbContext context, IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));
            ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));

            _context = context;
            _mediator = mediator;
        }

        public ITodoRepository TodoRepository =>
            _todoRepository ??= new TodoRepository(_context);


        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveAndDispatchEventsAsync(CancellationToken cancellationToken)
        {
            await SaveAsync(cancellationToken);

            // TODO: This section needs to be refactored better than now.
            IList<EntityEntry<IDomainEvent>> entryList = (from x in _context.ChangeTracker.Entries<IDomainEvent>()
                                                          where x.Entity.Events.Any()
                                                          select x).ToList();

            List<IDomainEventFlag> source = entryList.SelectMany((x) => x.Entity.Events).ToList();

            foreach (var sourceEvent in source)
                await _mediator.Publish(sourceEvent, cancellationToken);

            foreach (EntityEntry<IDomainEvent> entry in entryList)
                entry.Entity.ClearEvents();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
