using MapsterMapper;
using MediatR;
using ToDo.App.Application.Commands.ToDoList;
using ToDo.App.Application.Common;
using ToDo.App.Application.Dto;
using ToDo.App.Domain.Repositories.UnitOfWork;

namespace ToDo.App.Application.Handlers.ToDoList
{
    public sealed class EditToDoListCommandHandler : IRequestHandler<EditToDoListCommand, ResultHandler<ToDoDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditToDoListCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
            ArgumentNullException.ThrowIfNull(unitOfWork, nameof(unitOfWork));

            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultHandler<ToDoDto>> Handle(EditToDoListCommand request, CancellationToken cancellationToken)
        {
            var todo = await _unitOfWork.TodoRepository.GetAsync(a => a.Id == request.Id && a.UserId == request.UserId, cancellationToken);

            var result = new ResultHandler<ToDoDto>();

            if(todo is null)
            {
                result.WithMessage("Not found!");
                return result;
            }

            todo.Edit(request.Title, request.Tag, request.ToDoItems);

            await _unitOfWork.TodoRepository.UpdateAsync(todo);
            await _unitOfWork.SaveAndDispatchEventsAsync(cancellationToken);

            var mappedToDo = _mapper.Map<ToDoDto>(todo);
            result = new ResultHandler<ToDoDto>(mappedToDo);

            return result;
        }
    }
}
