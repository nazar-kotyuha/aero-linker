namespace AeroLinker.Core.Common.DTO.ProjectDrone;

public sealed class ProjectDroneDto
{
    public Guid DroneId { get; set; }
    public string DroneName { get; set; } = string.Empty;
    public int ProjectId { get; set; }
}