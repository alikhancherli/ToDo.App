using MapsterMapper;
using MediatR;
using ToDo.App.Application.Dto;
using ToDo.App.Application.Queries.ToDoList;
using ToDo.App.Domain.Repositories.UnitOfWork;

namespace ToDo.App.Application.Handlers.ToDoList
{
    public sealed class GetToDoQueryHandler : IRequestHandler<GetToDoQuery, ToDoDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetToDoQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
            ArgumentNullException.ThrowIfNull(unitOfWork, nameof(unitOfWork));
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ToDoDto?> Handle(GetToDoQuery request, CancellationToken cancellationToken)
        {
            var todo = await _unitOfWork.TodoRepository.GetAsync(request.Id, cancellationToken);
            
            if (todo is null)
                return null;

            return _mapper.Map<ToDoDto>(todo);
        }
    }
}
