using MSA.EventPlatform.Domain.Users;

namespace MSA.EventPlatform.Domain.Events
{
    public enum EventRegistrationStatus
    {
        None,
        Confirmed,
        Canceled
    }

    public class EventRegistration
    {
        public long EventRegistrationId { get; set; }
        public long EventId { get; set; }
        public EventItem? Event { get; set; }
        public long ParticipantId { get; set; }
        public User? Participant { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        public EventRegistrationStatus Status { get; set; } = EventRegistrationStatus.None;
    }
}
