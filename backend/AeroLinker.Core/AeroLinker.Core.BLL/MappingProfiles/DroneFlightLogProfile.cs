using AeroLinker.Core.Common.DTO.ProjectDrone;
using AeroLinker.Core.DAL.Entities;
using AutoMapper;

namespace AeroLinker.Core.BLL.MappingProfiles;

public sealed class DroneFlightLogProfile : Profile
{
    public DroneFlightLogProfile()
    {
        CreateMap<DroneFlightLogDto, DroneFlightLog>()!.ReverseMap();
    }
}