using Coffee.Application.Common.Exceptions;
using Coffee.Application.Interfaces;
using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, int>
    {
        private readonly IBaseRepository<Event> _repository;

        public DeleteEventCommandHandler(IBaseRepository<Event> repository) => _repository = repository;

        public async Task<int> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            Event events = await _repository.GetByIdAsync(request.Id) ?? 
                throw new NotFoundException("Новость не найдена", request.Id);

            try
            {
                await _repository.Delete(events);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }

            return events.Id;
        }
    }
}
