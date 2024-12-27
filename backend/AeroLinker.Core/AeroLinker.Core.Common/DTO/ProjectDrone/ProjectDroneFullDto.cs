namespace AeroLinker.Core.Common.DTO.ProjectDrone;

public sealed class ProjectDroneFullDto
{
    public Guid DroneId { get; set; }
    public string DroneName { get; set; } = string.Empty;
    public int ProjectId { get; set; }

    public ICollection<DroneFlightLogDto> DroneFlights { get; set; } = null!;
}