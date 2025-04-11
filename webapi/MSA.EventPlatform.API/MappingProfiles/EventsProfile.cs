using AutoMapper;
using MSA.EventPlatform.API.Models.Queries.Events;
using MSA.EventPlatform.API.Models.Queries.Events.EventRegistrations;
using MSA.EventPlatform.API.Models.Responses.Events;
using MSA.EventPlatform.Domain.Events;

namespace MSA.EventPlatform.API.MappingProfiles
{
    public class EventsProfile : Profile
    {
        public EventsProfile()
        {
            CreateMap<EventItem, EventDto>();
            CreateMap<EventCategory, EventCategoryDto>();
            CreateMap<CreateEventCommand, EventItem>();
            CreateMap<UpdateEventCommand, EventItem>();
            CreateMap<EventRegistration, EventRegistrationDto>();
            CreateMap<RegisterToEventCommand, EventRegistration>();
        }
    }
}
