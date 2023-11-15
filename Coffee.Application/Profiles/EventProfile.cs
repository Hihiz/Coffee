using AutoMapper;
using Coffee.Application.Events.Commands.CreateEvent;
using Coffee.Application.Events.Commands.UpdateEvent;
using Coffee.Domain.Entities;

namespace Coffee.Application.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile() 
        {
            CreateMap<Event, CreateEventCommand>().ReverseMap();
            CreateMap<Event, UpdateEventCommand>().ReverseMap();
        }
    }
}
