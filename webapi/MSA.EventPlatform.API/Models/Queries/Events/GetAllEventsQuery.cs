using AutoMapper;
using MediatR;
using MSA.EventPlatform.API.Models.Responses.Events;
using MSA.EventPlatform.API.Repositories.Events;

namespace MSA.EventPlatform.API.Models.Queries.Events
{
    public record GetAllEventsQuery : IRequest<IEnumerable<EventDto>>;

    public class GetAllEventsQueryHandler(IEventRepository eventRepository, IMapper mapper)
        : IRequestHandler<GetAllEventsQuery, IEnumerable<EventDto>>
    {
        public async Task<IEnumerable<EventDto>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            var events = await eventRepository.GetAllEventsAsync(cancellationToken);
            return  mapper.Map<IEnumerable<EventDto>>(events);
        }
    }
}
