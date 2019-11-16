using Newtonsoft.Json;

namespace RealEstate.Presentation.Common.Configuration
{
    [JsonObject("PartnerAPI")]
    public class PartnerApiCredentials
    {
        [JsonProperty("APIKey")]
        public string APIKey { get; set; }
    }
}
