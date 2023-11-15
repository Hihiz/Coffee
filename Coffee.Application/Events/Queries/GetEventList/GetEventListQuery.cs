using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Events.Queries.GetEventList
{
    public class GetEventListQuery : IRequest<List<Event>>
    {
        public int Id { get; set; } 
    }
}
