namespace AeroLinker.Core.Common.DTO.ProjectDrone;

public sealed class DroneFlightLogDto
{
    public Guid Guid { get; set; }
    public Guid ProjectDroneId { get; set; }
    public string FlightLogId { get; set; }
}