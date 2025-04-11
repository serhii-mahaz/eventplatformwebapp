using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSA.EventPlatform.API.Exceptions;
using MSA.EventPlatform.API.Models.Queries.Events;
using MSA.EventPlatform.DataContext;
using MSA.EventPlatform.Domain.Events;

namespace MSA.EventPlatform.API.Repositories.Events
{
    public interface IEventRepository
    {
        Task<IEnumerable<EventItem>> GetAllEventsAsync(CancellationToken cancellationToken, bool track = false);
        Task<EventItem?> GetEventByIdAsync(long eventId, CancellationToken cancellationToken, bool track = false);
        Task<long> CreateEventAsync(EventItem eventItem, CancellationToken cancellationToken);
        Task<bool> UpdateEventAsync(EventItem updatedEvent, CancellationToken cancellationToken);
        Task<bool> DeleteEventAsync(long eventId, CancellationToken cancellationToken);
        Task<long> CreateEventRegistrationAsync(EventRegistration eventRegistration, CancellationToken cancellationToken);
        Task<bool> CancelEventRegistrationAsync(long eventId, long participantId, CancellationToken cancellationToken);
        Task<IEnumerable<EventRegistration>> GetEventRegistrationsByEventIdAsync(long eventId, CancellationToken cancellationToken, bool track = false);
        Task<IEnumerable<EventRegistration>> GetMyEventRegistrationsAsync(long participantId, CancellationToken cancellationToken, bool track = false);
    }

    public class EventRepository(EventPlatformDbContext dbContext, IMapper mapper) : IEventRepository
    {
        public async Task<IEnumerable<EventItem>> GetAllEventsAsync(CancellationToken cancellationToken, bool track = false)
        {
            var query = dbContext.Events
                .Include(e => e.Category)
                .Include(e => e.Organizer)
                .AsQueryable();
            query = track ? query : query.AsNoTracking();

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<EventItem?> GetEventByIdAsync(long eventId, CancellationToken cancellationToken, bool track = false)
        {
            var query = dbContext.Events
                .Include(e => e.Category)
                .Include(e => e.Organizer)
                .AsQueryable();
            query = track ? query : query.AsNoTracking();

            return await query.FirstOrDefaultAsync(e => e.EventItemId == eventId, cancellationToken);
        }

        public async Task<long> CreateEventAsync(EventItem eventItem, CancellationToken cancellationToken)
        {
            dbContext.Events.Add(eventItem);
            await dbContext.SaveChangesAsync(cancellationToken);

            return eventItem.EventItemId;
        }

        public async Task<bool> UpdateEventAsync(EventItem updatedEvent, CancellationToken cancellationToken)
        {
            var eventItem = await GetEventByIdAsync(updatedEvent.EventItemId, cancellationToken, true);
            if (eventItem == null) return false;

            mapper.Map(updatedEvent, eventItem);
            dbContext.Events.Update(eventItem);
            await dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> DeleteEventAsync(long eventId, CancellationToken cancellationToken)
        {
            var eventItem = await GetEventByIdAsync(eventId, cancellationToken, true);
            if (eventItem == null) return false;

            dbContext.Events.Remove(eventItem);
            await dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<long> CreateEventRegistrationAsync(EventRegistration eventRegistration, CancellationToken cancellationToken)
        {
            dbContext.EventRegistrations.Add(eventRegistration);
            await dbContext.SaveChangesAsync(cancellationToken);

            return eventRegistration.EventRegistrationId;
        }

        public async Task<bool> CancelEventRegistrationAsync(long eventId, long participantId, CancellationToken cancellationToken)
        {
            var eventRegistration = await dbContext.EventRegistrations.FirstOrDefaultAsync(e => e.EventId == eventId && e.ParticipantId == participantId, cancellationToken);
            if (eventRegistration == null) return false;

            eventRegistration.Status = EventRegistrationStatus.Canceled;
            dbContext.EventRegistrations.Update(eventRegistration);
            await dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IEnumerable<EventRegistration>> GetEventRegistrationsByEventIdAsync(long eventId, CancellationToken cancellationToken, bool track = false)
        {
            var query = dbContext.EventRegistrations
                .Include(e => e.Participant)
                .Where(e => e.EventId == eventId)
                .AsQueryable();
            query = track ? query : query.AsNoTracking();

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<EventRegistration>> GetMyEventRegistrationsAsync(long participantId, CancellationToken cancellationToken, bool track = false)
        {
            var query = dbContext.EventRegistrations
                .Include(e => e.Participant)
                .Where(e => e.ParticipantId == participantId)
                .AsQueryable();
            query = track ? query : query.AsNoTracking();

            return await query.ToListAsync(cancellationToken);
        }
    }
}
