using AutoMapper;
using MediatR;
using MSA.EventPlatform.API.Exceptions;
using MSA.EventPlatform.API.Models.Responses.Users;
using MSA.EventPlatform.API.Repositories.Users;

namespace MSA.EventPlatform.API.Models.Queries.Users
{
    public record GetPublicUserInfoQuery(long UserId) : IRequest<PublicUserInfoDto>;
    
    public class GetPublicUserInfoQueryHandler(IUserRepository userRepository, IMapper mapper)
        : IRequestHandler<GetPublicUserInfoQuery, PublicUserInfoDto>
    {
        public async Task<PublicUserInfoDto> Handle(GetPublicUserInfoQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken)
                ?? throw new NotFoundException("User not found.");

            return mapper.Map<PublicUserInfoDto>(user);
        }
    }

}
