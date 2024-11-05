using Coffee.Application.Common.Exceptions;
using Coffee.Application.Interfaces;
using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Events.Queries.GetEventDetail
{
    public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery, Event>
    {
        private readonly IBaseRepository<Event> _repository;

        public GetEventDetailQueryHandler(IBaseRepository<Event> repository) => (_repository) = (repository);

        public async Task<Event> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
            Event events = await _repository.GetByIdAsync(request.Id);

            if (events == null)
            {
                throw new NotFoundException(nameof(Event), request.Id);
            }

            return events;
        }
    }
}
