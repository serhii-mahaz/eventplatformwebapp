namespace MSA.EventPlatform.Domain.Events
{
    public class EventCategory
    {
        public long EventCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
    }
}
