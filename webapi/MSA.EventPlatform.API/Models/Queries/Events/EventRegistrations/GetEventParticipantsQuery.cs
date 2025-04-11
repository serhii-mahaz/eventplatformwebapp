using MediatR;
using MSA.EventPlatform.API.Models.Responses.Events;
using MSA.EventPlatform.API.Models.Responses.Users;
using MSA.EventPlatform.API.Repositories.Events;

namespace MSA.EventPlatform.API.Models.Queries.Events.EventRegistrations
{
    public record GetEventParticipantsQuery(long EventId) : IRequest<IEnumerable<EventRegistrationDto>>;

    public class GetEventParticipantsQueryHandler(IEventRepository eventRepository)
        : IRequestHandler<GetEventParticipantsQuery, IEnumerable<EventRegistrationDto>>
    {
        public async Task<IEnumerable<EventRegistrationDto>> Handle(GetEventParticipantsQuery request, CancellationToken cancellationToken)
        {
            var participants = await eventRepository.GetEventRegistrationsByEventIdAsync(request.EventId, cancellationToken);
            return participants.Select(p => new EventRegistrationDto
            {
                EventRegistrationId = p.EventRegistrationId,
                EventId = p.EventId,
                ParticipantId = p.ParticipantId,
                RegistrationDate = p.RegistrationDate,
                TicketCode = p.TicketCode,
                Status = p.Status,
                Participant = new PublicUserInfoDto
                {
                    UserId = p.ParticipantId,
                    FullName = $"{p.Participant!.FirstName} {p.Participant.LastName}",
                    Email = p.Participant.Email,
                    AvatarUrl = p.Participant.AvatarUrl,
                }
            });
        }
    }
}
