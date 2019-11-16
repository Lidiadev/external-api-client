using Newtonsoft.Json;

namespace RealEstateWebApp.Common.Configuration
{
    [JsonObject("BaseUrls")]
    public class BaseUrls
    {
        [JsonProperty("PartnerAPI")]
        public string PartnerAPI { get; set; }
    }
}
