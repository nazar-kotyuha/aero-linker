using AeroLinker.Core.BLL.DroneHubAdapters.Factory;
using AeroLinker.Core.BLL.Interfaces;
using AeroLinker.Core.BLL.Services.Abstract;
using AeroLinker.Core.DAL.Context;
using AeroLinker.DroneHub.BLL.WebSocket;
using AeroLinker.Shared.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AeroLinker.Core.BLL.Services;

public class DroneConnectorService : BaseService, IDroneConnectorService
{
    private DroneAdapterFactory droneAdapterFactory_ { get; set; }
    public DroneConnectorService(AeroLinkerCoreContext context, IMapper mapper) : base(context, mapper)
    {
        this.droneAdapterFactory_ = new();
    }

    public async Task<WsClient> TryConnect(Guid connectionId)
    {
        var projectDrone = await _context.ProjectDrones
            .Include(drone => drone.DroneConnectionString)
            .FirstOrDefaultAsync(x => x.DroneId == connectionId);

        if (projectDrone is null)
        {
            throw new InvalidDroneConnectionException();
        }

        var adapter = droneAdapterFactory_.CreateAdapter(projectDrone.DroneConnectionString);

        var client = adapter.CreateClient();

        client.ProjectDroneId = connectionId;

        client.ConnectAsync();

        return client;
    }
}