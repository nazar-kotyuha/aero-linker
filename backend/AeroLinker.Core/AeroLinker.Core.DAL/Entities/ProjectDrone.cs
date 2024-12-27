using AeroLinker.Core.DAL.Entities.Common;

namespace AeroLinker.Core.DAL.Entities;

public sealed class ProjectDrone : Entity<int>
{
    public Guid DroneId { get; set; }
    public string DroneName { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public Guid ConnectionId { get; set; }
    public Project Project { get; set; } = null!;
    public DroneConnectionString DroneConnectionString { get; set; } = null!;

    public ICollection<DroneFlightLog> DroneFlights { get; set; } = new List<DroneFlightLog>();
}