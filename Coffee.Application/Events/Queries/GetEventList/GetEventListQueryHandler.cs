using Coffee.Application.Interfaces;
using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Events.Queries.GetEventList
{
    public class GetEventListQueryHandler : IRequestHandler<GetEventListQuery, List<Event>>
    {
        private readonly IBaseRepository<Event> _repository;

        public GetEventListQueryHandler(IBaseRepository<Event> repository) => (_repository) = (repository);

        public async Task<List<Event>> Handle(GetEventListQuery request, CancellationToken cancellationToken)
        {
            List<Event> eventList = await _repository.GetAllAsync();

            return eventList;
        }
    }
}
