using MediatR;
using Microsoft.Extensions.Logging;
using ToDo.App.Domain.Events;

namespace ToDo.App.Application.EventHandlers
{
    public sealed class TodoHasCreatedEventHandler : INotificationHandler<TodoHasCreatedEvent>
    {
        private readonly ILogger<TodoHasCreatedEventHandler> _logger;

        public TodoHasCreatedEventHandler(ILogger<TodoHasCreatedEventHandler> logger)
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            _logger = logger;
        }

        public async Task Handle(TodoHasCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"The event raised : {typeof(TodoHasCreatedEvent).Name}");
            await Task.CompletedTask;
        }
    }
}
