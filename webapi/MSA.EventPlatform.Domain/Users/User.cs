using System.ComponentModel.DataAnnotations;

namespace MSA.EventPlatform.Domain.Users
{
    public class User : TrackedEntity
    {
        public long UserId { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public string? UserName { get; set; }

        public string? AvatarUrl { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpires { get; set; }
    }
}
