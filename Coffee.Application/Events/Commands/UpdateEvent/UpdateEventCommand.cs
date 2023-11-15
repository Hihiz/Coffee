using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Events.Commands.UpdateEvent
{
    public class UpdateEventCommand : IRequest<Event>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsActive { get; set; }
    }
}
