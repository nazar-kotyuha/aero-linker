using Newtonsoft.Json;
using AeroLinker.DroneHub.BLL.Interfaces;

namespace AeroLinker.DroneHub.BLL.Services;

public class JsonSerializerSettingsService : IJsonSerializerSettingsService
{
    public JsonSerializerSettings GetSettings()
    {
        var jsonSerializerSettings = new JsonSerializerSettings();
        jsonSerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        jsonSerializerSettings.Formatting = Formatting.Indented;

        return jsonSerializerSettings;
    }
}
