using Coffee.Application.Events.Commands.CreateEvent;
using Coffee.Application.Events.Commands.DeleteEvent;
using Coffee.Application.Events.Commands.UpdateEvent;
using Coffee.Application.Events.Queries.GetEventDetail;
using Coffee.Application.Events.Queries.GetEventList;
using Coffee.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult> GetNews()
        {
            GetEventListQuery query = new GetEventListQuery();
            List<Event> events = await _mediator.Send(query);

            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetNewsDetail(int id)
        {
            GetEventDetailQuery query = new GetEventDetailQuery(id);
            Event events = await _mediator.Send(query);

            return Ok(events);
        }

        [HttpPost]
        public async Task<ActionResult> CreateNews(CreateEventCommand command) => Ok(await _mediator.Send(command));

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNews(int id, UpdateEventCommand command)
        {
            if (id != command.Id)
            {
                return NotFound();
            }

            Event events = await _mediator.Send(command);

            return Ok(events);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNews(int id)
        {
            DeleteEventCommand command = new DeleteEventCommand(id);

            return Ok(await _mediator.Send(command));
        }
    }
}
