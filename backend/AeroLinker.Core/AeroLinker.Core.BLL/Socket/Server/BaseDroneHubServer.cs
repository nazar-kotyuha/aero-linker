using AeroLinker.Core.BLL.Socket.Session;
using AeroLinker.DroneHub.BLL.WebSocket;
using System.Net;
using System.Net.Sockets;

namespace AeroLinker.Core.BLL.Socket.Server;
public class BaseDroneHubServer : WsServer
{
    private readonly IServiceProvider serviceProvider_;

    public BaseDroneHubServer(IPAddress address, int port, IServiceProvider serviceProvider) : base(address, port)
    {
        serviceProvider_ = serviceProvider;
    }

    protected override TcpSession CreateSession()
    {
        return new BaseDroneHubSession(this, serviceProvider_);
    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Chat WebSocket server caught an error with code {error}");
    }
}