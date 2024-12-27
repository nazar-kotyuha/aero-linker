using AeroLinker.Core.Common.DTO.ProjectDrone;

namespace AeroLinker.Core.BLL.Interfaces;

public interface IProjectDroneService
{
    Task<ICollection<ProjectDroneDto>> GetAllProjectDroneAsync(int projectId);
    Task<ProjectDroneFullDto> GetProjectDroneAsync(Guid droneId);

    Task<ProjectDroneDto> AddNewProjectDroneAsync(ProjectAddDroneDto dto);
}