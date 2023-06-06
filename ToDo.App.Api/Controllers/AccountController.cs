using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDo.App.Api.Models;
using ToDo.App.Application.Commands.User;
using ToDo.App.Application.JwtService;
using ToDo.App.Domain.Entities;

namespace ToDo.App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IMediator _mediator;

        public AccountController(IJwtService jwtService, UserManager<User> userManager, IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(jwtService, nameof(jwtService));
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));
            ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));
            _jwtService = jwtService;
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromForm] LoginRequest loginForm)
        {
            var user = await _userManager.FindByNameAsync(loginForm.Username);
            if (!await _userManager.CheckPasswordAsync(user, loginForm.Password))
                return BadRequest("پسورد یا نام کاربری نامعتبر است");

            var token = await _jwtService.GenerateTokenAsync(user);
            return Ok(token);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new RegisterUserCommand(
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    request.Username,
                    request.Password),
                cancellationToken);

            return Ok(result);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var userId = Convert.ToInt32(User.FindFirst(u => u.Type == ClaimTypes.Sid)?.Value);
            var result = await _mediator.Send(new UpdateUserCommand(userId, request.Email, request.FirstName, request.LastName), cancellationToken);

            return Ok(result);
        }
    }
}
