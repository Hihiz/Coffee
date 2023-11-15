using MediatR;

namespace Coffee.Application.Events.Commands.DeleteEvent
{
    public class DeleteEventCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteEventCommand(int id) => Id = id;
    }
}
