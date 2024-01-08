using MapsterMapper;
using MediatR;
using ToDo.App.Application.Commands.ToDoList;
using ToDo.App.Application.Common;
using ToDo.App.Domain.Repositories.UnitOfWork;

namespace ToDo.App.Application.Handlers.ToDoList;

public sealed class DeleteToDoListCommandHandler(IMapper _mapper, IUnitOfWork _unitOfWork) : IRequestHandler<DeleteToDoListCommand, ResultHandler<bool>>
{

    public async Task<ResultHandler<bool>> Handle(DeleteToDoListCommand request, CancellationToken cancellationToken)
    {
        var todoList = await _unitOfWork.TodoRepository.GetAsync(x => x.Id == request.Id && x.UserId == request.UserId, cancellationToken);
        var result = new ResultHandler<bool>(false);
        
        if (todoList is null)
        {
            result.WithMessage("Not found!");
            return result;
        }

        var deleteResult = await _unitOfWork.TodoRepository.DeleteAsync(request.Id, cancellationToken);
        await _unitOfWork.SaveAndDispatchEventsAsync(cancellationToken);

        result = new ResultHandler<bool>(deleteResult);
        if (deleteResult)
            result.WithMessage("Success!");
        else
            result.WithMessage("Failed!");

        return result;
    }
}
