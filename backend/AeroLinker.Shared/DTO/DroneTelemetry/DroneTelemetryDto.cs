namespace AeroLinker.Shared.DTO.Drone;

public sealed class DroneTelemetryDto
{
    public long timestamp { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
    public double altitude { get; set; }
    public double speed { get; set; }
    public int batteryLevel { get; set; }
    public string status { get; set; }
}