using AeroLinker.Core.BLL.DroneHubAdapters.Adapters.Example.Client;
using AeroLinker.Core.BLL.Interfaces;
using AeroLinker.Core.BLL.Services;
using AeroLinker.DroneHub.BLL.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Sockets;

namespace AeroLinker.Core.BLL.Socket.Session;
public class BaseDroneHubSession : WsSession
{
    private static string UserProjectDroneFlighLogContainerName = "user-drone-changes";
    private readonly Dictionary<Guid, WsClient> clients_ = new();

    private readonly IServiceProvider serviceProvider_;

    public BaseDroneHubSession(WsServer server, IServiceProvider serviceProvider) : base(server)
    {
        serviceProvider_ = serviceProvider;
    }

    public async override void OnWsConnected(HttpRequest request)
    {
        Guid connectionId = ExtractConnectionId(request.Url);

        WsClient clientSession;

        using (var scope = serviceProvider_.CreateScope())
        {
            var droneConnectorService_ = scope.ServiceProvider.GetRequiredService<IDroneConnectorService>();

            clientSession = await droneConnectorService_.TryConnect(connectionId);
        }

        clientSession.SetMainSession(this);

        this.clients_.Add(Id, clientSession);
    }

    public async override void OnWsDisconnected()
    {
        var client = this.clients_.FirstOrDefault(x => x.Key == Id).Value;

        if (client == null) return;

        await OnClientSessionDisconnected(client);

        client.DisconnectAndStop();
    }

    public async override void OnWsReceived(byte[] buffer, long offset, long size)
    {
        var client = this.clients_.FirstOrDefault(x => x.Key == Id).Value;

        if (client == null) return;

        client.OnMainSessionReceived(buffer, offset, size);
    }

    protected override void OnError(SocketError error)
    {
    }

    private Guid ExtractConnectionId(string url)
    {
        if (url[0] == '/')
            url = url[1..];

        Guid connectionId;

        if (!Guid.TryParse(url, out connectionId))
        {
            Disconnect();
        }

        return connectionId;
    }

    public async Task OnClientSessionDisconnected(WsClient wsClient)
    {
        using (var scope = serviceProvider_.CreateScope())
        {
            var flightLogService_ = scope.ServiceProvider.GetRequiredService<FlightLogService>();

            await flightLogService_.StageFlightLog(((ExampleClient)wsClient).flightLog_, wsClient.ProjectDroneId);
        }
    }
}
