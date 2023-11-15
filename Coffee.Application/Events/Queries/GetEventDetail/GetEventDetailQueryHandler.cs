using Coffee.Application.Interfaces;
using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Events.Queries.GetEventDetail
{
    public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery, Event>
    {
        private readonly IRepository<Event> _repository;

        public GetEventDetailQueryHandler(IRepository<Event> repository) => (_repository) = (repository);

        public async Task<Event> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
            Event events = await _repository.GetById(request.Id);

            if (events == null)
            {
                throw new Exception("Новость не найдена");
            }

            return events;
        }
    }
}
