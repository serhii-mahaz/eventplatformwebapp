using AutoMapper;
using MediatR;
using MSA.EventPlatform.API.Exceptions;
using MSA.EventPlatform.API.Models.Responses.Users;
using MSA.EventPlatform.API.Repositories.Users;

namespace MSA.EventPlatform.API.Models.Queries.Users
{
    public record GetUserProfileQuery(long UserId) : IRequest<UserProfielDto>;

    public class GetUserProfileQueryHandler(IUserRepository userRepository, IMapper mapper)
        : IRequestHandler<GetUserProfileQuery, UserProfielDto>
    {
        public async Task<UserProfielDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            return mapper.Map<UserProfielDto>(user);
        }
    }
}
