using MediatR;
using ToDo.App.Application.Common;
using ToDo.App.Application.Dto;
using ToDo.App.Domain.Entities;
using ToDo.App.Domain.Enums;

namespace ToDo.App.Application.Commands.ToDoList;

public record AddToDoListCommand(string Title, Tag Tag, IList<ToDoItem> ToDoItems, int UserId) : IRequest<ResultHandler<ToDoDto>>;
