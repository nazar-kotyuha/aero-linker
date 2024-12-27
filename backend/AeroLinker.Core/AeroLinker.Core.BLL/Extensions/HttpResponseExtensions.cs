using Newtonsoft.Json;

namespace AeroLinker.Core.BLL.Extensions;

public static class HttpResponseExtensions
{
    public static async Task<T> GetModelAsync<T>(this HttpResponseMessage response)
    {
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())!;
    }
}