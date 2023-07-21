using MediatR;
using ToDo.App.Application.Common;
using ToDo.App.Application.Dto;

namespace ToDo.App.Application.Commands.ToDoList;

public record DoneToDoItemCommand(int UserId, Guid ToDoItemId,int Id) : IRequest<ResultHandler<ToDoItemDto>>;