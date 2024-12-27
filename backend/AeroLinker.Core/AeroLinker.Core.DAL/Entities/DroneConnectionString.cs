using AeroLinker.Core.DAL.Entities.Common;
using AeroLinker.Core.DAL.Enums;

namespace AeroLinker.Core.DAL.Entities;

public class DroneConnectionString : Entity<int>
{
    public Guid ConnectionId { get; set; }
    public string ServerName { get; set; } = string.Empty;
    public int Port { get; set; }
    public DroneConnectorAdapterType AdapterType { get; set; }
    public ProjectDrone ProjectDrone { get; set; } = null!;
}