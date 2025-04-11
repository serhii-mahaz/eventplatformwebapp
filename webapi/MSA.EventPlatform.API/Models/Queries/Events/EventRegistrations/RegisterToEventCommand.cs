using MediatR;
using MSA.EventPlatform.API.Models.Responses.Events;
using MSA.EventPlatform.API.Repositories.Events;
using MSA.EventPlatform.Domain.Events;

namespace MSA.EventPlatform.API.Models.Queries.Events.EventRegistrations
{
    public class RegisterToEventCommand : IRequest<EventRegistrationDto>
    {
        public long EventId { get; set; }
        public long ParticipantId { get; set; }
    }

    public class RegisterToEventCommandHandler(IEventRepository eventRepository)
        : IRequestHandler<RegisterToEventCommand, EventRegistrationDto>
    {
        public async Task<EventRegistrationDto> Handle(RegisterToEventCommand request, CancellationToken cancellationToken)
        {
            var newRegistration = new EventRegistration
            {
                ParticipantId = request.ParticipantId,
                RegistrationDate = DateTime.UtcNow,
                TicketCode = Guid.NewGuid().ToString()
            };
            var registrationId = await eventRepository.CreateEventRegistrationAsync(newRegistration, cancellationToken);
            
            return new EventRegistrationDto
            {
                EventRegistrationId = registrationId,
                EventId = newRegistration.EventId,
                ParticipantId = newRegistration.ParticipantId,
                RegistrationDate = newRegistration.RegistrationDate,
                TicketCode = newRegistration.TicketCode
            };
        }
    }
}
