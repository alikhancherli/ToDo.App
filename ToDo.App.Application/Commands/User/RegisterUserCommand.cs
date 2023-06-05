using MediatR;
using ToDo.App.Application.Common;
using ToDo.App.Application.Dto;

namespace ToDo.App.Application.Commands.User;

public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Username,
    string Password) : IRequest<ResultHandler<UserDto>>;
