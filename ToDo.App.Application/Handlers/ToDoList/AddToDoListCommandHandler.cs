using MapsterMapper;
using MediatR;
using ToDo.App.Application.Commands.ToDoList;
using ToDo.App.Application.Common;
using ToDo.App.Application.Dto;
using ToDo.App.Domain.Repositories.UnitOfWork;

namespace ToDo.App.Application.Handlers.ToDoList
{
    public sealed class AddToDoListCommandHandler : IRequestHandler<AddToDoListCommand, ResultHandler<ToDoDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddToDoListCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
            ArgumentNullException.ThrowIfNull(unitOfWork, nameof(unitOfWork));
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultHandler<ToDoDto>> Handle(AddToDoListCommand request, CancellationToken cancellationToken)
        {
            var todo = new Domain.Entities.ToDoList(request.Title, request.Tag, request.UserId, request.ToDoItems);

            await _unitOfWork.TodoRepository.AddAsync(todo);
            await _unitOfWork.SaveAndDispatchEventsAsync(cancellationToken);

            var mappedTodo = _mapper.Map<ToDoDto>(todo);

            return new ResultHandler<ToDoDto>(mappedTodo);
        }
    }
}
