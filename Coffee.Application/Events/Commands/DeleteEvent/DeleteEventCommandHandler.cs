using Coffee.Application.Interfaces;
using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, int>
    {
        private readonly IRepository<Event> _repository;

        public DeleteEventCommandHandler(IRepository<Event> repository) => _repository = repository;

        public async Task<int> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            Event events = await _repository.GetById(request.Id);

            if (events == null)
            {
                throw new Exception("Новость не найдена");
            }

            try
            {
                _repository.Delete(events);
                await _repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Новость не найдена");
            }

            return events.Id;
        }
    }
}
