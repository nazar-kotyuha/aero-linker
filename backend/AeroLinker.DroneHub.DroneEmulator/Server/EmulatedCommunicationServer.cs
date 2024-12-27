using AeroLinker.DroneHub.BLL.Session;
using AeroLinker.DroneHub.BLL.WebSocket;
using System;
using System.Net;
using System.Net.Sockets;

namespace AeroLinker.DroneHub.DroneEmulator;
public class EmulatedCommunicationServer : WsServer
{
    public EmulatedCommunicationServer(IPAddress address, int port) : base(address, port) { }

    protected override TcpSession CreateSession() { return new CommunicationSession(this); }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Chat WebSocket server caught an error with code {error}");
    }
}
