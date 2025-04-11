using MediatR;
using MSA.EventPlatform.API.Exceptions;
using MSA.EventPlatform.API.Models.Responses.Auth;
using MSA.EventPlatform.API.Repositories.Users;
using MSA.EventPlatform.API.Services;

namespace MSA.EventPlatform.API.Models.Commands.Auth
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<AuthResponseDto>;

    public class RefreshTokenCommandHandler(IUserRepository userRepository, ITokenService tokenService)
        : IRequestHandler<RefreshTokenCommand, AuthResponseDto>
    {
        public async Task<AuthResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByRefreshTokenAsync(request.RefreshToken, cancellationToken);
            if (user == null || user.RefreshTokenExpires < DateTime.UtcNow)
            {
                throw new BadRequestException("Invalid refresh token.");
            }
                
            var newAccessToken = tokenService.GenerateAccessToken(user);
            var newRefreshToken = tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpires = DateTime.UtcNow.AddDays(1);
            await userRepository.UpdateUserAsync(user, cancellationToken);

            return new AuthResponseDto
            {
                UserId = user.UserId,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                AvatarUrl = user.AvatarUrl,
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
    }

}
