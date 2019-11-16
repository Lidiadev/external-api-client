using Domain.Dtos;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    /// <summary>
    /// The interface for the API Client.
    /// </summary>
    public interface IApiClient
    {
        /// <summary>
        /// Performs a GET request. 
        /// </summary>
        /// <param name="uri">The request uri.</param>
        /// <returns>The <see cref="ApiResponseDto"/>.</returns>
        Task<ApiResponseDto> GetAsync(string uri);
    }
}
