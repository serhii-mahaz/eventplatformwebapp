using AutoMapper;
using MediatR;
using MSA.EventPlatform.API.Repositories.Events;
using MSA.EventPlatform.Domain.Events;

namespace MSA.EventPlatform.API.Models.Queries.Events
{
    public class CreateEventCommand : IRequest<long>
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public int MaxParticipants { get; set; }
        public long OrganizerId { get; set; }
        public long CategoryId { get; set; }
    }

    public class CreateEventCommandHandler(IEventRepository eventRepository, IMapper mapper)
        : IRequestHandler<CreateEventCommand, long>
    {
        public async Task<long> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var newEvent = mapper.Map<EventItem>(request);
            return await eventRepository.CreateEventAsync(newEvent, cancellationToken);
        }
    }
}
