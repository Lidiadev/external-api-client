using System.Text.Json.Serialization;

namespace Domain.Dtos
{
    public class PaginationDto
    {
        [JsonPropertyName("AantalPaginas")]
        public int NumberOfPages { get; set; }

        [JsonPropertyName("HuidigePagina")]
        public int CurrentPage { get; set; }
    }
}
