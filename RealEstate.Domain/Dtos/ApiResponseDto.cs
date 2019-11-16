using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RealEstate.Domain.Dtos
{
    /// <summary>
    /// The API response DTO.
    /// </summary>
    public class ApiResponseDto
    {
        public IReadOnlyCollection<PropertyDto> Objects { get; set; }

        [JsonPropertyName("Paging")]
        public PaginationDto Pagination { get; set; }
    }
}
