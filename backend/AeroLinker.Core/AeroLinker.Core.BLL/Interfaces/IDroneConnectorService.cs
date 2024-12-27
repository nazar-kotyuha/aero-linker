using AeroLinker.DroneHub.BLL.WebSocket;

namespace AeroLinker.Core.BLL.Interfaces;

public interface IDroneConnectorService
{
    Task<WsClient> TryConnect(Guid connectionId);
}