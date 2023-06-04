using MediatR;

namespace ToDo.App.Shared.Domain
{
    public interface IDomainEventFlag : INotification
    {
        DateTimeOffset TriggeredIn { get; }
    }
}
