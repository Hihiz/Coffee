using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Events.Queries.GetEventDetail
{
    public class GetEventDetailQuery : IRequest<Event>
    {
        public int Id { get; set; }

        public GetEventDetailQuery(int id) => Id = id;
    }
}
