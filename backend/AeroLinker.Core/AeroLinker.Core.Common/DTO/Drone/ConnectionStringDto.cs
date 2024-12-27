using AeroLinker.Shared.Enums;

namespace AeroLinker.Core.Common.DTO.Drone;

public class ConnectionStringDto
{
    public string ServerName { get; set; } = string.Empty;
    public int Port { get; set; }
    public DroneConnectorAdapterType AdapterType { get; set; }
}