using AeroLinker.Core.BLL.DroneHubAdapters.Adapters.Base;
using AeroLinker.Core.BLL.DroneHubAdapters.Adapters.Example.Client;
using AeroLinker.Core.DAL.Entities;
using AeroLinker.DroneHub.BLL.WebSocket;

namespace AeroLinker.Core.BLL.DroneHubAdapters.Adapters.Example;

public class ExampleDroneAdapter : BaseDroneAdapter
{
    public ExampleDroneAdapter(DroneConnectionString connectionString) : base(connectionString)
    {
    }

    public override WsClient CreateClient()
    {
        var client = new ExampleClient(this.ConnectionString.ServerName, this.ConnectionString.Port);

        client.ConnectAsync();

        return client;
    }
}
