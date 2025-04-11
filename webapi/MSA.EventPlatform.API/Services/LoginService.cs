using MSA.EventPlatform.API.Exceptions;
using MSA.EventPlatform.API.Models.Responses.Auth;
using MSA.EventPlatform.API.Repositories.Users;

namespace MSA.EventPlatform.API.Services
{
    public interface ILoginService
    {
        Task<AuthResponseDto> LoginAsync(string email, string password, CancellationToken cancellationToken);
    }

    public class LoginService(IUserRepository userRepository, ITokenService tokenService, IPasswordHasher passwordHasher)
        : ILoginService
    {
        public async Task<AuthResponseDto> LoginAsync(string email, string password, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByEmailAsync(email, cancellationToken, true);
            if (user == null || !passwordHasher.VerifyPassword(password, user.PasswordHash))
            {
                throw new BadRequestException("Incorrect email or password.");
            }

            var accessToken = tokenService.GenerateAccessToken(user);
            var refreshToken = tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpires = DateTime.UtcNow.AddDays(1);
            await userRepository.UpdateUserAsync(user, cancellationToken);

            return new AuthResponseDto
            {
                UserId = user.UserId,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                AvatarUrl = user.AvatarUrl,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }
    }
}
