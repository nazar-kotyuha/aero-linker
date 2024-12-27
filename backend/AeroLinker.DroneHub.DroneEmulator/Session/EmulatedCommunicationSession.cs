using AeroLinker.DroneHub.BLL.Interfaces;
using AeroLinker.DroneHub.BLL.Services;
using AeroLinker.DroneHub.BLL.WebSocket;
using AeroLinker.Shared.DTO.Drone;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AeroLinker.DroneHub.BLL.Session;
class CommunicationSession : WsSession
{
    private readonly VideoStreamer videoStreamer_;
    private readonly IDroneTelemetryService droneTelemetryService_;
    private string videoFilePath = @"C:\Users\nazar\OneDrive\Documents\grad-program\aero-linker\backend\AeroLinker.DroneHub.BLL\www\video-source\frames";

    public CommunicationSession(WsServer server) : base(server)
    {
        this.droneTelemetryService_ = new DroneTelemetryService();
        this.videoStreamer_ = new VideoStreamer();
    }

    public async override void OnWsConnected(HttpRequest request)
    {
        Console.WriteLine($"Chat WebSocket session with Id {Id} connected!");

        _ = Task.Run(() => StreamVideoAsync(videoFilePath));

        string message = JsonConvert.SerializeObject(await this.droneTelemetryService_.GetDroneTelemetryAsync(0));
        SendTextAsync(message);
        Console.WriteLine("Initial Outcoming: " + message);

    }

    public override void OnWsDisconnected()
    {
        Console.WriteLine($"Chat WebSocket session with Id {Id} disconnected!");
    }

    public async override void OnWsReceived(byte[] buffer, long offset, long size)
    {
        string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
        Console.WriteLine("Incoming: " + message);

        var telemetry = JsonConvert.DeserializeObject<CommandDto>(message);

        string response = JsonConvert.SerializeObject(await this.droneTelemetryService_.UpdateDroneTelemetryAsync(telemetry));

        Console.WriteLine("Outcoming: " + response);

        // Multicast message to all connected sessions
        ((WsServer)Server).MulticastText(response);
    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Chat WebSocket session caught an error with code {error}");
    }

    private async Task StreamVideoAsync(string filePath)
    {
        await videoStreamer_.StreamVideoAsync(filePath, chunk =>
        {
            // Send video chunk to the client
            SendTextAsync(chunk);
        });
    }
}
