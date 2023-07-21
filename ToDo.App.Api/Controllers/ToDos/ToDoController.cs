using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDo.App.Api.Models.ToDos;
using ToDo.App.Application.Commands.ToDoList;
using ToDo.App.Application.Queries.ToDoList;
using ToDo.App.Domain.Entities;
using ToDo.App.Domain.ValueObjects;

namespace ToDo.App.Api.Controllers.ToDos
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ToDoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ToDoController(IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddTodoList([FromBody] AddToDoRequest request, CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(u => u.Type == ClaimTypes.Sid);

            var result = await _mediator.Send(new AddToDoListCommand(
                request.Title,
                request.Tag,
                request.ToDoItemRequests.Select(
                    g => ToDoItem.Create(g.Title, g.Note, g.Reminder, new PriorityLevel(g.PriorityLevel))).ToList(),
                Convert.ToInt32(userId?.Value)
                ), cancellationToken);

            return Ok(result);
        }

        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> EditTodoList(int id, [FromBody] EditToDoListRequest request, CancellationToken cancellationToken)
        {
            var userId = Convert.ToInt32(User.FindFirst(u => u.Type == ClaimTypes.Sid)?.Value);

            var result = await _mediator.Send(new EditToDoListCommand(
                id,
                request.Title,
                request.Tag,
                request.ToDoItemRequests.Select(
                    g => ToDoItem.Edit(g.Id, g.Title, g.Note, g.Reminder, new PriorityLevel(g.PriorityLevel))).ToList(),
                userId
                ), cancellationToken);

            return Ok(result);
        }

        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteTodoList(int id, CancellationToken cancellationToken)
        {
            var userId = Convert.ToInt32(User.FindFirst(u => u.Type == ClaimTypes.Sid)?.Value);
            var result = await _mediator.Send(new DeleteToDoListCommand(userId, id), cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTodoList(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetToDoQuery(id), cancellationToken);

            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTodoList(int userId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetToDoListQuery(userId), cancellationToken);
            return Ok(result);
        }

        [HttpPost("[action]/{id:int}")]
        public async Task<IActionResult> DoneToDoItem(int id,Guid todoItemId,CancellationToken cancellationToken)
        {
            var userId = Convert.ToInt32(User.FindFirst(u => u.Type == ClaimTypes.Sid)?.Value);
            var result = await _mediator.Send(new DoneToDoItemCommand(userId,todoItemId,id));

            return Ok(result);
        }
    }
}
