using AutoMapper;
using AeroLinker.Core.BLL.MappingProfiles.MappingActions;
using AeroLinker.Core.Common.DTO.Users;
using AeroLinker.Core.DAL.Entities;

namespace AeroLinker.Core.BLL.MappingProfiles;

public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()!.AfterMap<BuildAvatarLinkAction>()!.ReverseMap();
        CreateMap<User, UserProfileDto>()!.AfterMap<BuildAvatarLinkAction>()!.ReverseMap();
    }
}