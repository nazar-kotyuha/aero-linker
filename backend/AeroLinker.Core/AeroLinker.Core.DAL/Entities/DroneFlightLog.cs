using AeroLinker.Core.DAL.Entities.Common;

namespace AeroLinker.Core.DAL.Entities;

public class DroneFlightLog : Entity<int>
{
    public Guid Guid { get; set; }
    public Guid ProjectDroneId { get; set; }
    public string FlightLogId { get; set; }

    public ProjectDrone ProjectDrone { get; set; } = null!;
}