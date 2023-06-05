using MapsterMapper;
using MediatR;
using ToDo.App.Application.Commands.ToDoList;
using ToDo.App.Application.Common;
using ToDo.App.Domain.Repositories.UnitOfWork;

namespace ToDo.App.Application.Handlers.ToDoList
{
    public sealed class DeleteToDoListCommandHandler : IRequestHandler<DeleteToDoListCommand, ResultHandler<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteToDoListCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
            ArgumentNullException.ThrowIfNull(unitOfWork, nameof(unitOfWork));

            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultHandler<bool>> Handle(DeleteToDoListCommand request, CancellationToken cancellationToken)
        {
            var todo = await _unitOfWork.TodoRepository.DeleteAsync(request.Id, cancellationToken);
            var result = new ResultHandler<bool>(todo);

            if(todo)
                result.WithMessage("Success!");
            else
                result.WithMessage("Failed!");

            return result;
        }
    }
}
