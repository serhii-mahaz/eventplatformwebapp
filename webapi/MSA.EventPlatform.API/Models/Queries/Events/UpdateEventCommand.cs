using AutoMapper;
using MediatR;
using MSA.EventPlatform.API.Repositories.Events;
using MSA.EventPlatform.Domain.Events;

namespace MSA.EventPlatform.API.Models.Queries.Events
{
    public class UpdateEventCommand : IRequest<Unit>
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public int MaxParticipants { get; set; }
        public long OrganizerId { get; set; }
        public long CategoryId { get; set; }
    }

    public class UpdateEventCommandHandler(IEventRepository eventRepository, IMapper mapper)
        : IRequestHandler<UpdateEventCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var updatedEvent = mapper.Map<EventItem>(request);
            var success = await eventRepository.UpdateEventAsync(updatedEvent, cancellationToken);
            return success ? Unit.Value : throw new Exception("Failed to update event.");
        }
    }
}
