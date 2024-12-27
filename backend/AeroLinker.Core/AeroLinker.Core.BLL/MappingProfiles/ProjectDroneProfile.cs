using AeroLinker.Core.Common.DTO.ProjectDrone;
using AeroLinker.Core.DAL.Entities;
using AutoMapper;

namespace AeroLinker.Core.BLL.MappingProfiles;

public sealed class ProjectDroneProfile : Profile
{
    public ProjectDroneProfile()
    {
        CreateMap<ProjectDrone, ProjectDroneDto>()!.ReverseMap();
        CreateMap<ProjectDrone, ProjectAddDroneDto>()!.ReverseMap();
        CreateMap<ProjectDrone, DroneInfoDto>()!.ReverseMap();

        CreateMap<ProjectDrone, ProjectDroneFullDto>()
            .ForMember(dest => dest.DroneFlights, opt => opt.MapFrom(src => src.DroneFlights))
            .ReverseMap()
            .ForMember(dest => dest.DroneFlights, opt => opt.MapFrom(src => src.DroneFlights));
    }
}