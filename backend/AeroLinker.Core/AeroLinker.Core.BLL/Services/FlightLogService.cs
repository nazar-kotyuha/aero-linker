using AeroLinker.AzureBlobStorage.Interfaces;
using AeroLinker.AzureBlobStorage.Models;
using AeroLinker.Core.BLL.Services.Abstract;
using AeroLinker.Core.DAL.Context;
using AeroLinker.Core.DAL.Entities;
using AeroLinker.Shared.DTO;
using AeroLinker.Shared.DTO.Drone;
using AeroLinker.Shared.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace AeroLinker.Core.BLL.Services;

public class FlightLogService : BaseService//, IDroneConnectorService
{
    private readonly IBlobStorageService blobStorageService_;
    private static string UserProjectDroneFlighLogContainerName = "user-drone-changes";

    public FlightLogService(AeroLinkerCoreContext context, IMapper mapper, IBlobStorageService blobStorageService) : base(context, mapper)
    {
        this.blobStorageService_ = blobStorageService;
    }

    public async Task StageFlightLog(List<DroneTelemetryDto> flightLog, Guid projectDroneId)
    {
        if ((await _context.ProjectDrones.FirstOrDefaultAsync(d => d.DroneId == projectDroneId)) is null)
        {
            throw new InvalidProjectException();
        }

        var jsonString = JsonConvert.SerializeObject(flightLog);
        var contentBytes = Encoding.UTF8.GetBytes(jsonString);
        var jsonMimeType = "application/json";

        DroneFlightLog droneFlightLog = new DroneFlightLog();
        droneFlightLog.Guid = Guid.NewGuid();
        droneFlightLog.ProjectDroneId = projectDroneId;
        droneFlightLog.FlightLogId = droneFlightLog.Guid + "@" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        var blob = new Blob
        {
            Id = droneFlightLog.FlightLogId,
            ContentType = jsonMimeType,
            Content = contentBytes
        };

        var containerName = UserProjectDroneFlighLogContainerName;

        await blobStorageService_.UploadAsync(containerName, blob);

        await _context.DroneFlightLogs.AddAsync(droneFlightLog);

        await _context.SaveChangesAsync();
    }

    public async Task<List<DroneTelemetryDto>> GetStagedAsync(string changesGuid)
    {
        var blob = await blobStorageService_.DownloadAsync(UserProjectDroneFlighLogContainerName, changesGuid);

        var json = Encoding.UTF8.GetString(blob.Content ?? throw new ArgumentNullException());

        var result = JsonConvert.DeserializeObject<List<DroneTelemetryDto>>(json) ?? throw new JsonException($"{nameof(DroneTelemetryDto)} deserialized as null!");

        return result;
    }
}