using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace API.Tests.Helpers;

internal static class HttpContentHelpers
{
    public static HttpContent GetJsonContent(this object obj)
    {
        var json = JsonConvert.SerializeObject(obj);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}
