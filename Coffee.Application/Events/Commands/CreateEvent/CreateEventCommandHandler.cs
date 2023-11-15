using AutoMapper;
using Coffee.Application.Interfaces;
using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Event>
    {
        private readonly IRepository<Event> _repository;
        private readonly IMapper _mapper;

        public CreateEventCommandHandler(IRepository<Event> repository, IMapper mapper) => (_repository, _mapper) = (repository, mapper);

        public async Task<Event> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            Event events = _mapper.Map<Event>(request);

            try
            {
                _repository.Create(events);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return events;
        }
    }
}
