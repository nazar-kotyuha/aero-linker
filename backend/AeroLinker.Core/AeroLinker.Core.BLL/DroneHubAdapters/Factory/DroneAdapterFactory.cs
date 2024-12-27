using AeroLinker.Core.BLL.DroneHubAdapters.Adapters.Base;
using AeroLinker.Core.BLL.DroneHubAdapters.Adapters.Example;
using AeroLinker.Core.DAL.Entities;
using AeroLinker.Core.DAL.Enums;

namespace AeroLinker.Core.BLL.DroneHubAdapters.Factory;

public class DroneAdapterFactory
{
    public BaseDroneAdapter CreateAdapter(DroneConnectionString connectionString)
    {
        switch (connectionString.AdapterType)
        {
            case DroneConnectorAdapterType.ExampleAdapter:
                return new ExampleDroneAdapter(connectionString);
            // Add cases for other adapter types here
            default:
                throw new ArgumentException("Invalid drone adapter type");
        }
    }
}
