using MediatR;
using ToDo.App.Application.Common;
using ToDo.App.Application.Dto;
using ToDo.App.Domain.Entities;
using ToDo.App.Domain.Enums;

namespace ToDo.App.Application.Commands.TodoList;

public record AddToDoListCommand(string Title, Tag Tag, IList<ToDoItem> ToDoItems,int UserId) : IRequest<ResultHandler<ToDoDto>>;
