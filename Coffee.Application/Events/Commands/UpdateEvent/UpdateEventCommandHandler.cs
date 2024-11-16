using AutoMapper;
using Coffee.Application.Common.Exceptions;
using Coffee.Application.Interfaces;
using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Event>
    {
        private readonly IBaseRepository<Event> _repository;
        private readonly IMapper _mapper;

        public UpdateEventCommandHandler(IBaseRepository<Event> repository, IMapper mapper) => (_repository, _mapper) = (repository, mapper);

        public async Task<Event> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            Event events = _mapper.Map<Event>(request) ??
                throw new NotFoundException("Новость не найдена", request.Id); 

            try
            {
                await _repository.UpdateAsync(events);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }

            return events;
        }
    }
}
