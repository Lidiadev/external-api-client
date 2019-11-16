using System.Text.Json;

namespace RealEstate.Presentation.Common
{
    public sealed class DefaultJsonSerializerOptions
    {
        public static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
}
