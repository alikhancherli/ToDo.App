using MapsterMapper;
using MediatR;
using ToDo.App.Application.Dto;
using ToDo.App.Application.Queries.ToDoList;
using ToDo.App.Domain.Repositories.UnitOfWork;

namespace ToDo.App.Application.Handlers.ToDoList;

public sealed class GetToDoListQueryHandler(IMapper _mapper, IUnitOfWork _unitOfWork) : IRequestHandler<GetToDoListQuery, IList<ToDoDto>>
{
    public async Task<IList<ToDoDto>> Handle(GetToDoListQuery request, CancellationToken cancellationToken)
    {
        var todoList = await _unitOfWork.TodoRepository.GetListAsync(x => x.UserId == request.UserId, cancellationToken);

        return _mapper.Map<IList<ToDoDto>>(todoList);
    }
}
