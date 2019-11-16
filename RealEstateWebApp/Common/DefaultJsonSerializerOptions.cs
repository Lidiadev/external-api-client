using System.Text.Json;

namespace RealEstateWebApp.Common
{
    public sealed class DefaultJsonSerializerOptions
    {
        public static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
}
