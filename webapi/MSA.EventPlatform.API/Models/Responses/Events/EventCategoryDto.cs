namespace MSA.EventPlatform.API.Models.Responses.Events
{
    public class EventCategoryDto
    {
        public long EventCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
    }
}
