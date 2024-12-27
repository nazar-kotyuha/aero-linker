using AeroLinker.Core.Common.DTO.Drone;

namespace AeroLinker.Core.Common.DTO.ProjectDrone;

public sealed class ProjectAddDroneDto
{
    public string DroneName { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public ConnectionStringDto ConnectionString {get; set;} = null!;
}