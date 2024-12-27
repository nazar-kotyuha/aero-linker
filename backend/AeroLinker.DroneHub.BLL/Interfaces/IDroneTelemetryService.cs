using AeroLinker.Shared.DTO.Drone;


namespace AeroLinker.DroneHub.BLL.Interfaces;

public interface IDroneTelemetryService
{
    Task<DroneTelemetryDto> UpdateDroneTelemetryAsync(CommandDto commandDto);
    Task DeleteDroneTelemetryAsync(int DroneTelemetryId);
    Task<DroneTelemetryDto> GetDroneTelemetryAsync(int DroneTelemetryId);
}
