using MediatR;
using ToDo.App.Application.Common;

namespace ToDo.App.Application.Commands.TodoList;

public record DeleteToDoListCommand(int UserId,int Id) : IRequest<ResultHandler<bool>>;
