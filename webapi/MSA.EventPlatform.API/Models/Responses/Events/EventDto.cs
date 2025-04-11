using MSA.EventPlatform.API.Models.Responses.Users;

namespace MSA.EventPlatform.API.Models.Responses.Events
{
    public class EventDto
    {
        public long EventItemId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public int MaxParticipants { get; set; }
        public long CategoryId { get; set; }
        public EventCategoryDto Category { get; set; } = new EventCategoryDto();
        public long OrganizerId { get; set; }
        public PublicUserInfoDto Organizer { get; set; } = new PublicUserInfoDto();
        public bool IsCanceled { get; set; }
    }
}
