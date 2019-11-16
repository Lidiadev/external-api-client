using RealEstate.Domain.Dtos;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RealEstate.Presentation.Common.Configuration;
using RealEstate.Presentation.Common.Extensions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RealEstate.Presentation.Services
{
    /// <summary>
    /// The API Client.
    /// </summary>
    public class ApiClient : IApiClient
    {
        private readonly ILogger<ApiClient> _logger;
        private readonly HttpClient _httpClient;
        private readonly PartnerApiCredentials _credentials;

        public ApiClient(HttpClient httpClient, IOptions<PartnerApiCredentials> credentials, ILogger<ApiClient> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _credentials = credentials.Value ?? throw new ArgumentNullException(nameof(credentials));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<ApiResponseDto> GetAsync(string url)
        {
            try
            {
                var request = CreateRequest(HttpMethod.Get, url);

                var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException();
                }

                using var contentStream = await response.Content.ReadAsStreamAsync();
                return await contentStream.DeserializeFromJsonAsync<ApiResponseDto>();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Creates an http request message.
        /// </summary>
        /// <param name="method"><see cref="HttpMethod"/>.</param>
        /// <param name="uri">The request uri.</param>
        /// <returns>The <see cref="HttpRequestMessage"/>.</returns>
        private HttpRequestMessage CreateRequest(HttpMethod method, string uri)
            => new HttpRequestMessage(method, $"json/{_credentials.APIKey}{uri}");
    }
}
