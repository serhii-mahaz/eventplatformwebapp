using MediatR;
using MSA.EventPlatform.API.Models.Responses.Auth;
using MSA.EventPlatform.API.Services;

namespace MSA.EventPlatform.API.Models.Commands.Auth
{
    public record LoginUserCommand(string Email, string Password) : IRequest<AuthResponseDto>;

    public class LoginUserCommandHandler(ILoginService loginService)
        : IRequestHandler<LoginUserCommand, AuthResponseDto>
    {
        public async Task<AuthResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            return await loginService.LoginAsync(request.Email, request.Password, cancellationToken);
        }
    }
}
