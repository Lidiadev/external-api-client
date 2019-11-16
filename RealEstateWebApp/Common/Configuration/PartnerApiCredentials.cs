using Newtonsoft.Json;

namespace RealEstateWebApp.Common.Configuration
{
    [JsonObject("PartnerAPI")]
    public class PartnerApiCredentials
    {
        [JsonProperty("APIKey")]
        public string APIKey { get; set; }
    }
}
