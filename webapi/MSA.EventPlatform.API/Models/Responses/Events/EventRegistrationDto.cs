using MSA.EventPlatform.API.Models.Responses.Users;
using MSA.EventPlatform.Domain.Events;

namespace MSA.EventPlatform.API.Models.Responses.Events
{
    public class EventRegistrationDto
    {
        public long EventRegistrationId { get; set; }
        public long EventId { get; set; }
        public EventItem? Event { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        public EventRegistrationStatus Status { get; set; } = EventRegistrationStatus.None;
        public long ParticipantId { get; set; }
        public PublicUserInfoDto? Participant { get; set; }
    }
}
