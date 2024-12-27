using AeroLinker.Core.BLL.Socket.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Reflection;

namespace AeroLinker.Core.BLL.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }

    public static void StartDroneHub(this IApplicationBuilder app)
    {
        // WebSocket server port
        int port = 8080;

        // WebSocket server content path
        string www = "C:/Users/nazar/OneDrive/Documents/grad-program/aero-linker/backend/AeroLinker.DroneHub.DroneEmulator/www/ws";//Path.GetFullPath("www/ws");

        //var droneConnectorService = app.ApplicationServices.GetRequiredService<IDroneConnectorService>();
        // Create a new WebSocket server
        var server = new BaseDroneHubServer(IPAddress.Any, port, app.ApplicationServices);

        server.AddStaticContent(www, "/chat");

        // Start the server
        server.Start();
    }
}