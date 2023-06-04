using MediatR;

namespace ToDo.App.Shared.Domain
{
    public interface IDomainEvent : INotification
    {
        public IReadOnlyList<IDomainEventFlag> Events { get; }
        void ClearEvents();
    }
}
