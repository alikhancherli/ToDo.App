using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ToDo.App.Application.Commands.User;
using ToDo.App.Application.Common;
using ToDo.App.Application.Dto;
using ToDo.App.Domain.Entities;

namespace ToDo.App.Application.Handlers.Users
{
    public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResultHandler<UserDto>>
    {
        private UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IMapper mapper, UserManager<User> userManager)
        {
            ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ResultHandler<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            var result = new ResultHandler<UserDto>();
            if (user is null)
            {
                result.WithMessage("Not found!");
                return result;
            }

            user.Edit(request.FirstName, request.LastName, request.Email);

            var updateResult = await _userManager.UpdateAsync(user);

            if (updateResult.Succeeded)
            {
                result = new ResultHandler<UserDto>(_mapper.Map<UserDto>(user));
                return result;
            }
            else
            {
                result.WithMessage("Updated Error!");
                return result;
            }
        }
    }
}
