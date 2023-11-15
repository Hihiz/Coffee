using Coffee.Application.Interfaces;
using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Events.Queries.GetEventList
{
    public class GetEventListQueryHandler : IRequestHandler<GetEventListQuery, List<Event>>
    {
        private readonly IRepository<Event> _repository;

        public GetEventListQueryHandler(IRepository<Event> repository) => (_repository) = (repository);

        public async Task<List<Event>> Handle(GetEventListQuery request, CancellationToken cancellationToken)
        {
            List<Event> eventList = await _repository.GetAll();

            return eventList;
        }
    }
}
