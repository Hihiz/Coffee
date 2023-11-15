using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Events.Commands.CreateEvent
{
    public class CreateEventCommand : IRequest<Event>
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsActive { get; set; }
    }
}
