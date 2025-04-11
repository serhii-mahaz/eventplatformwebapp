using MediatR;
using MSA.EventPlatform.API.Repositories.Events;

namespace MSA.EventPlatform.API.Models.Queries.Events
{
    public record DeleteEventCommand(long Id) : IRequest<Unit>;

    public class DeleteEventCommandHandler(IEventRepository eventRepository)
        : IRequestHandler<DeleteEventCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var success = await eventRepository.DeleteEventAsync(request.Id, cancellationToken);
            return success ? Unit.Value : throw new Exception("Failed to delete event.");
        }
    }
}
