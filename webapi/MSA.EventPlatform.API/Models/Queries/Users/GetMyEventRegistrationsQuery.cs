using MediatR;
using MSA.EventPlatform.API.Models.Responses.Events;
using MSA.EventPlatform.API.Repositories.Events;

namespace MSA.EventPlatform.API.Models.Queries.Users
{
    public record GetMyEventRegistrationsQuery(long UserId) : IRequest<IEnumerable<EventRegistrationDto>>;
    
    public class GetMyEventRegistrationsQueryHandler(IEventRepository eventRepository)
        : IRequestHandler<GetMyEventRegistrationsQuery, IEnumerable<EventRegistrationDto>>
    {
        public async Task<IEnumerable<EventRegistrationDto>> Handle(GetMyEventRegistrationsQuery request, CancellationToken cancellationToken)
        {
            var userRegistrations = await eventRepository.GetMyEventRegistrationsAsync(request.UserId, cancellationToken);
            return userRegistrations.Select(r => new EventRegistrationDto
            {
                EventRegistrationId = r.EventRegistrationId,
                EventId = r.EventId,
                RegistrationDate = r.RegistrationDate,
                TicketCode = r.TicketCode,
                Status = r.Status
            });
        }
    }
}
