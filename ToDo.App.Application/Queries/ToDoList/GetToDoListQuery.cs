using MediatR;
using ToDo.App.Application.Dto;

namespace ToDo.App.Application.Queries.ToDoList;

public record GetToDoListQuery(int UserId) : IRequest<IList<ToDoDto>>;