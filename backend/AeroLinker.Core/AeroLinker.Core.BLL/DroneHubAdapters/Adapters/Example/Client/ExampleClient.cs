using AeroLinker.Core.BLL.Extensions;
using AeroLinker.DroneHub.BLL.WebSocket;
using AeroLinker.Shared.DTO.Drone;
using System.Net.Sockets;
using System.Text;

namespace AeroLinker.Core.BLL.DroneHubAdapters.Adapters.Example.Client;

class ExampleClient : WsClient
{
    public ExampleClient(string address, int port) : base(address, port)
    {
        flightLog_ = new();
    }

    public override void OnWsConnecting(HttpRequest request)
    {
        request.SetBegin("GET", "/");
        request.SetHeader("Host", "localhost");
        request.SetHeader("Origin", "http://localhost");
        request.SetHeader("Upgrade", "websocket");
        request.SetHeader("Connection", "Upgrade");
        request.SetHeader("Sec-WebSocket-Key", Convert.ToBase64String(WsNonce));
        request.SetHeader("Sec-WebSocket-Protocol", "chat, superchat");
        request.SetHeader("Sec-WebSocket-Version", "13");
        request.SetBody();
    }

    public override void OnWsConnected(HttpResponse response)
    {
    }

    public override void OnWsDisconnected()
    {
    }

    public override void OnWsReceived(byte[] buffer, long offset, long size)
    {
        string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);

        if (message.TryParseJson(out DroneTelemetryDto telemetry))
        {
            this.flightLog_.Add(telemetry);
        }

        // retransmit message to a drone hub session level
        this.mainSession_!.SendTextAsync(message);
    }

    public override void OnMainSessionReceived(byte[] buffer, long offset, long size)
    {
        string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);

        if (!message.TryParseJson(out CommandDto command))
        {
            // not a Command - can't send
            return;
        }

        SendTextAsync(message);
    }

    protected override void OnDisconnected()
    {
        base.OnDisconnected();
    }

    protected override void OnError(SocketError error)
    {
    }
}
