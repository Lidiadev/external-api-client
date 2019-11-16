using Newtonsoft.Json;

namespace RealEstate.Presentation.Common.Configuration
{
    [JsonObject("BaseUrls")]
    public class BaseUrls
    {
        [JsonProperty("PartnerAPI")]
        public string PartnerAPI { get; set; }
    }
}
