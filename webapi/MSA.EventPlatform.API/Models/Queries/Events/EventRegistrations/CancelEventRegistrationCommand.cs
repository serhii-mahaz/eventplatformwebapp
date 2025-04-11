using MediatR;
using MSA.EventPlatform.API.Repositories.Events;

namespace MSA.EventPlatform.API.Models.Queries.Events.EventRegistrations
{
    public class CancelEventRegistrationCommand : IRequest<Unit>
    {
        public long EventId { get; set; }
        public long ParticipantId { get; set; }
    }

    public class CancelEventRegistrationCommandHandler(IEventRepository eventRepository)
        : IRequestHandler<CancelEventRegistrationCommand, Unit>
    {
        public async Task<Unit> Handle(CancelEventRegistrationCommand request, CancellationToken cancellationToken)
        {
            await eventRepository.CancelEventRegistrationAsync(request.EventId, request.ParticipantId, cancellationToken);
            return Unit.Value;
        }
    }
}
