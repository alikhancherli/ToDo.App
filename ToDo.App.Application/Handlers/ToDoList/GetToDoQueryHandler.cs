using MapsterMapper;
using MediatR;
using ToDo.App.Application.Dto;
using ToDo.App.Application.Queries.ToDoList;
using ToDo.App.Domain.Repositories.UnitOfWork;

namespace ToDo.App.Application.Handlers.ToDoList
{
    public sealed class GetToDoQueryHandler(IMapper _mapper, IUnitOfWork _unitOfWork) : IRequestHandler<GetToDoQuery, ToDoDto?>
    {
        public async Task<ToDoDto?> Handle(GetToDoQuery request, CancellationToken cancellationToken)
        {
            var todo = await _unitOfWork.TodoRepository.GetAsync(request.Id, cancellationToken);
            
            if (todo is null)
                return null;

            return _mapper.Map<ToDoDto>(todo);
        }
    }
}
