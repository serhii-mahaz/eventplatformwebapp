namespace MSA.EventPlatform.API.Models.Responses.Users
{
    public class UserProfielDto
    {
        public long UserId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string AvatarUrl { get; set; } = string.Empty;
    }
}
