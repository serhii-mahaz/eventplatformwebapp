using MediatR;
using MSA.EventPlatform.API.Exceptions;
using MSA.EventPlatform.API.Repositories.Users;

namespace MSA.EventPlatform.API.Models.Commands.Auth
{
    public record LogoutUserCommand(string RefreshToken) : IRequest<Unit>;

    public class LogoutUserCommandHandler(IUserRepository userRepository)
        : IRequestHandler<LogoutUserCommand, Unit>
    {
        public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByRefreshTokenAsync(request.RefreshToken, cancellationToken)
                ?? throw new BadRequestException("Logout error.");

            user.RefreshToken = null;
            user.RefreshTokenExpires = null;
            await userRepository.UpdateUserAsync(user, cancellationToken);

            return Unit.Value;
        }
    }

}
