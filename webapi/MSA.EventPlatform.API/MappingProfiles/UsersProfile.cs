using AutoMapper;
using MSA.EventPlatform.API.Models.Responses.Users;
using MSA.EventPlatform.Domain.Users;

namespace MSA.EventPlatform.API.MappingProfiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, PublicUserInfoDto>()
                .ForMember(x => x.FullName, o => o.MapFrom(f => $"{f.FirstName} {f.LastName}"));
            CreateMap<User, UserProfielDto>();
        }
    }
}
