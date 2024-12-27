using AutoMapper;
using AeroLinker.Core.Common.DTO.Tag;
using AeroLinker.Core.DAL.Entities;

namespace AeroLinker.Core.BLL.MappingProfiles;

public sealed class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagDto>()!.ReverseMap();
    }
}