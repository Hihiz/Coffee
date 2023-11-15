using AutoMapper;
using Coffee.Application.Interfaces;
using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Event>
    {
        private readonly IRepository<Event> _repository;
        private readonly IMapper _mapper;

        public UpdateEventCommandHandler(IRepository<Event> repository, IMapper mapper) => (_repository, _mapper) = (repository, mapper);

        public async Task<Event> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            Event events = _mapper.Map<Event>(request);

            try
            {
                _repository.Update(events);
                await _repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Новость не найдена");
            }

            return events;
        }
    }
}
