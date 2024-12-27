using AeroLinker.Core.DAL.Entities;
using AeroLinker.DroneHub.BLL.WebSocket;

namespace AeroLinker.Core.BLL.DroneHubAdapters.Adapters.Base;

public abstract class BaseDroneAdapter
{
    protected WsSession? mainSession_ { get; private set; }
    protected DroneConnectionString ConnectionString { get; private set; }

    public BaseDroneAdapter(DroneConnectionString connectionString)
    {
        ConnectionString = connectionString;
    }

    public void SetMainSession(WsSession mainSession)
    {
        this.mainSession_ = mainSession;
    }

    //public abstract void OnMainSessionReceived(byte[] buffer, long offset, long size);
    public abstract WsClient CreateClient();
    //public abstract void Disconnect();
    //public abstract void SendCommand(string command);
    //public abstract string ReceiveData();
}
