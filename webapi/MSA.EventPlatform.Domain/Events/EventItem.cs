using MSA.EventPlatform.Domain.Users;

namespace MSA.EventPlatform.Domain.Events
{
    public class EventItem : TrackedEntity
    {
        public long EventItemId { get; set; }
        public string Title { get; set; } = "New Event";
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public int MaxParticipants { get; set; }
        public long CategoryId { get; set; }
        public EventCategory? Category { get; set; }
        public long OrganizerId { get; set; }
        public User? Organizer { get; set; }
        public bool IsCanceled { get; set; }
    }
}
