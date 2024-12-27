using AutoMapper;
using AeroLinker.Core.Common.DTO.Project;
using AeroLinker.Core.DAL.Entities;

namespace AeroLinker.Core.BLL.MappingProfiles;

public sealed class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectDto>()!.ReverseMap();
        CreateMap<Project, UpdateProjectDto>()!.ReverseMap();
        CreateMap<Project, ProjectResponseDto>()!.ReverseMap();
    }
}