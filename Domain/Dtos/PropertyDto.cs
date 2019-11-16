using System.Text.Json.Serialization;

namespace Domain.Dtos
{
    public class PropertyDto
    {
        public string Id { get; set; }

        public long GlobalId { get; set; }

        [JsonPropertyName("MakelaarId")]
        public int RealEstateAgentId { get; set; }

        [JsonPropertyName("MakelaarNaam")]
        public string RealEstateAgentName { get; set; }
    }
}
