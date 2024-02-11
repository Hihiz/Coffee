using Coffee.Application.Events.Commands.CreateEvent;
using Coffee.Application.Events.Commands.DeleteEvent;
using Coffee.Application.Events.Commands.UpdateEvent;
using Coffee.Application.Events.Queries.GetEventDetail;
using Coffee.Application.Events.Queries.GetEventList;
using Coffee.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventController(IMediator mediator) => _mediator = mediator;

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetEvent()
        {
            GetEventListQuery query = new GetEventListQuery();
            List<Event> events = await _mediator.Send(query);

            return Ok(events);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetEventDetail(int id)
        {
            GetEventDetailQuery query = new GetEventDetailQuery(id);
            Event events = await _mediator.Send(query);

            return Ok(events);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateEvent(CreateEventCommand command)
        {
            var result = new CreateEventCommandValidator().Validate(command);

            if (!result.IsValid)
            {
                return BadRequest(string.Join('\n', result.Errors));
            }

            return Ok(await _mediator.Send(command));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEvent(int id, UpdateEventCommand command)
        {
            if (id != command.Id)
            {
                return NotFound();
            }

            var result = new UpdateEventCommandValidator().Validate(command);

            if (!result.IsValid)
            {
                return BadRequest(string.Join('\n', result.Errors));
            }

            return Ok(await _mediator.Send(command));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEvent(int id)
        {
            DeleteEventCommand command = new DeleteEventCommand(id);

            return Ok(await _mediator.Send(command));
        }
    }
}
