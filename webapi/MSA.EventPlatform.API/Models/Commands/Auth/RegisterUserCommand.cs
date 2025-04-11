using MediatR;
using Microsoft.EntityFrameworkCore;
using MSA.EventPlatform.API.Exceptions;
using MSA.EventPlatform.API.Models.Responses.Auth;
using MSA.EventPlatform.API.Services;
using MSA.EventPlatform.DataContext;
using MSA.EventPlatform.Domain.Users;

namespace MSA.EventPlatform.API.Models.Commands.Auth
{
    public record RegisterUserCommand(string Email, string FirstName, string LastName, string Password) : IRequest<AuthResponseDto>;

    public class RegisterUserCommandHandler(
        EventPlatformDbContext dbContext,
        ILoginService loginService,
        IPasswordHasher passwordHasher
        ) : IRequestHandler<RegisterUserCommand, AuthResponseDto>
    {
        public async Task<AuthResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (await dbContext.Users.AnyAsync(u => u.Email == request.Email, cancellationToken: cancellationToken))
                throw new ConflictException("A user with this email already exists.");

            var user = new User
            {
                Email = request.Email,
                PasswordHash = passwordHasher.HashPassword(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync(cancellationToken);

            return await loginService.LoginAsync(user.Email, request.Password, cancellationToken);
        }
    }
}
