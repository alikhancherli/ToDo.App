using MediatR;
using Microsoft.Extensions.Logging;
using ToDo.App.Domain.Events;

namespace ToDo.App.Application.EventHandlers
{
    public sealed class TodoHasUpdatedEventHandler : INotificationHandler<TodoHasUpdatedEvent>
    {
        private readonly ILogger<TodoHasUpdatedEventHandler> _logger;

        public TodoHasUpdatedEventHandler(ILogger<TodoHasUpdatedEventHandler> logger)
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            _logger = logger;
        }

        public async Task Handle(TodoHasUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"The event raised : {typeof(TodoHasUpdatedEvent).Name}");
            await Task.CompletedTask;
        }
    }
}
