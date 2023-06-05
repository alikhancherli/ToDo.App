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
            var todoList = await _unitOfWork.TodoRepository.GetAsync(x => x.Id == request.Id && x.UserId == request.UserId, cancellationToken);
            var result = new ResultHandler<bool>(false);
            
            if (todoList is null)
            {
                result.WithMessage("Not found!");
                return result;
            }

            var deleteResult = await _unitOfWork.TodoRepository.DeleteAsync(request.Id, cancellationToken);

            result = new ResultHandler<bool>(deleteResult);
            if (deleteResult)
                result.WithMessage("Success!");
            else
                result.WithMessage("Failed!");

            return result;
        }
    }
}
