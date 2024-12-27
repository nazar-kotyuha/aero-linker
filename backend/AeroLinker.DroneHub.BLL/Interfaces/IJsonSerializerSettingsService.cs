using Newtonsoft.Json;

namespace AeroLinker.DroneHub.BLL.Interfaces;

public interface IJsonSerializerSettingsService
{
    JsonSerializerSettings GetSettings();
}