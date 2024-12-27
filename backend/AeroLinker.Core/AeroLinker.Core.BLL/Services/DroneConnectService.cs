/*using Microsoft.Extensions.Configuration;
using AeroLinker.Core.BLL.Interfaces;
using AeroLinker.Shared.DTO.DroneConnection;

namespace AeroLinker.Core.BLL.Services;

public class DroneConnectService : IDroneConnectService
{
    private readonly IHttpClientService _httpClientService;
    private readonly IConfiguration _configuration;

    public DroneConnectService(IHttpClientService httpClientService, IConfiguration configuration)
    {
        _httpClientService = httpClientService;
        _configuration = configuration;
    }

    public async Task TryConnect(RemoteConnect remoteConnect)
    {
        await _httpClientService.SendAsync($"{_configuration["SqlServiceUrl"]}/api/DroneAppHub/db-connect",
            remoteConnect, HttpMethod.Post);
    }
}*/