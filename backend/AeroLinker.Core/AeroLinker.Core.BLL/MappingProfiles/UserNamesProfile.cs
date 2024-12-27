using AutoMapper;
using AeroLinker.Core.Common.DTO.Users;
using AeroLinker.Core.DAL.Entities;

namespace AeroLinker.Core.BLL.MappingProfiles;

public sealed class UserNamesProfile : Profile
{
    public UserNamesProfile()
    {
        CreateMap<User, UpdateUserNamesDto>()!.ReverseMap();
    }
}