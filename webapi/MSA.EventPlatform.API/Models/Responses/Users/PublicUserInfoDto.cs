namespace MSA.EventPlatform.API.Models.Responses.Users
{
    public class PublicUserInfoDto
    {
        public long UserId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string? AvatarUrl { get; set; }
    }
}
