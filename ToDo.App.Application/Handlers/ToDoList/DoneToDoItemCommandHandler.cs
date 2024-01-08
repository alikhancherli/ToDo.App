using MapsterMapper;
using MediatR;
using ToDo.App.Application.Commands.ToDoList;
using ToDo.App.Application.Common;
using ToDo.App.Application.Dto;
using ToDo.App.Domain.Repositories.UnitOfWork;

namespace ToDo.App.Application.Handlers.ToDoList;

public sealed class DoneToDoItemCommandHandler(IMapper _mapper, IUnitOfWork _unitOfWork) : IRequestHandler<DoneToDoItemCommand, ResultHandler<ToDoItemDto>>
{

    public async Task<ResultHandler<ToDoItemDto>> Handle(DoneToDoItemCommand request, CancellationToken cancellationToken)
    {
        var todo = await _unitOfWork.TodoRepository.GetAsync(a => a.Id == request.Id && a.UserId == request.UserId, cancellationToken);

        var result = new ResultHandler<ToDoItemDto>();

        if (todo is null)
        {
            result.WithMessage("Not found!");
            return result;
        }

        var todoItem = todo.ToDoItems.FirstOrDefault(t => t.Id == request.ToDoItemId);
        if (todoItem is null)
        {
            result.WithMessage("Not found!");
            return result;
        }
        
        todo.ToDoItems.Remove(todoItem);
        todoItem.DoneItem();
        todo.ToDoItems.Add(todoItem);

        await _unitOfWork.TodoRepository.UpdateAsync(todo);
        await _unitOfWork.SaveAndDispatchEventsAsync(cancellationToken);

        var mappedToDo = _mapper.Map<ToDoItemDto>(todoItem);
        result = new ResultHandler<ToDoItemDto>(mappedToDo);

        return result;
    }
}
