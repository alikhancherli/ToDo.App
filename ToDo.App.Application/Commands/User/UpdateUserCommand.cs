using MediatR;
using ToDo.App.Application.Common;
using ToDo.App.Application.Dto;

namespace ToDo.App.Application.Commands.User;

public record UpdateUserCommand(int UserId, string Email, string FirstName, string LastName) : IRequest<ResultHandler<UserDto>>;