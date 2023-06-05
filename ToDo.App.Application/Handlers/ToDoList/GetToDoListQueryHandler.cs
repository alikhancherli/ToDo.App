using MapsterMapper;
using MediatR;
using ToDo.App.Application.Dto;
using ToDo.App.Application.Queries.ToDoList;
using ToDo.App.Domain.Repositories.UnitOfWork;

namespace ToDo.App.Application.Handlers.ToDoList
{
    public sealed class GetToDoListQueryHandler : IRequestHandler<GetToDoListQuery, IList<ToDoDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetToDoListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
            ArgumentNullException.ThrowIfNull(unitOfWork, nameof(unitOfWork));
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<ToDoDto>> Handle(GetToDoListQuery request, CancellationToken cancellationToken)
        {
            var todoList = await _unitOfWork.TodoRepository.GetListAsync(x => x.UserId == request.UserId, cancellationToken);

            return _mapper.Map<IList<ToDoDto>>(todoList);
        }
    }
}
