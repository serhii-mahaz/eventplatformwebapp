using MediatR;
using MSA.EventPlatform.API.Repositories.Users;

namespace MSA.EventPlatform.API.Models.Queries.Users
{
    public class UpdateUserProfileCommand : IRequest<Unit>
    {
        public long UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string? AvatarUrl { get; set; }
    }

    public class UpdateUserProfileCommandHandler(IUserRepository userRepository)
        : IRequestHandler<UpdateUserProfileCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken)
                ?? throw new Exception("User not found.");

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UserName = request.UserName;
            user.AvatarUrl = request.AvatarUrl;
            
            await userRepository.UpdateUserAsync(user, cancellationToken);
            return Unit.Value;
        }
    }
}
