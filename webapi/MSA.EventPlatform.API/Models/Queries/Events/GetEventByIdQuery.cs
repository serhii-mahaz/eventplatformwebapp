using AutoMapper;
using MediatR;
using MSA.EventPlatform.API.Exceptions;
using MSA.EventPlatform.API.Models.Responses.Events;
using MSA.EventPlatform.API.Repositories.Events;

namespace MSA.EventPlatform.API.Models.Queries.Events
{
    public record GetEventByIdQuery(long Id) : IRequest<EventDto>;

    public class GetEventByIdQueryHandler(IEventRepository eventRepository, IMapper mapper)
        : IRequestHandler<GetEventByIdQuery, EventDto>
    {
        public async Task<EventDto> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var eventItem = await eventRepository.GetEventByIdAsync(request.Id, cancellationToken);
            return eventItem == null
                ? throw new NotFoundException($"Event with ID {request.Id} not found.")
                : mapper.Map<EventDto>(eventItem);
        }
    }
}
