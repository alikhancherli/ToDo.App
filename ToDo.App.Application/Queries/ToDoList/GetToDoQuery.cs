using MediatR;
using ToDo.App.Application.Dto;

namespace ToDo.App.Application.Queries.ToDoList;

public record GetToDoQuery(int Id) : IRequest<ToDoDto?>;