using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ToDo.App.Application.Commands.User;
using ToDo.App.Application.Common;
using ToDo.App.Application.Dto;
using ToDo.App.Domain.Entities;

namespace ToDo.App.Application.Handlers.Users;

public sealed class RegisterUserCommandHandler(IMapper _mapper, UserManager<User> _userManager) : IRequestHandler<RegisterUserCommand, ResultHandler<UserDto>>
{
    public async Task<ResultHandler<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.FirstName, request.LastName, request.Username, request.Email);

        var userResult = await _userManager.CreateAsync(user);
        var result = new ResultHandler<UserDto>();
        if (userResult.Succeeded)
        {
            var passwordResult = await _userManager.AddPasswordAsync(user, request.Password);
            if (!passwordResult.Succeeded)
            {
                result.WithMessage(string.Join(", ", passwordResult.Errors.Select(a => a.Description)));
                return result;
            }
            await _userManager.AddToRoleAsync(user, "normaluser");
            result = new ResultHandler<UserDto>(_mapper.Map<UserDto>(user));
            return result;
        }

        result.WithMessage(string.Join(", ", userResult.Errors.Select(a => a.Description)));

        return result;
    }
}
