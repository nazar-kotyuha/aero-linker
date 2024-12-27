using AeroLinker.Core.Common.DTO.Drone;
using AeroLinker.Core.DAL.Entities;
using AutoMapper;

namespace AeroLinker.Core.BLL.MappingProfiles;

public sealed class ConnectionStringProfile : Profile
{
    public ConnectionStringProfile()
    {
        CreateMap<ConnectionStringDto, DroneConnectionString>()!.ReverseMap();
    }
}