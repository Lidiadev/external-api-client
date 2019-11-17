using Domain.Common;
using RealEstate.Domain.Dtos;
using Microsoft.Extensions.Logging;
using RealEstate.Presentation.Common.Constants;
using RealEstate.Presentation.Common.Extensions;
using RealEstate.Presentation.Factories.Interfaces;
using RealEstate.Presentation.Models.Agent;
using RealEstate.Presentation.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace RealEstate.Presentation.Services
{
    /// <summary>
    /// The service for Properties.
    /// </summary>
    public class PropertyService : IPropertyService
    {
        private ILogger<PropertyService> _logger;
        private readonly IApiClientFactory _apiClientFactory;
        private readonly IMemoryCache _memoryCache;

        public PropertyService(IApiClientFactory apiClientFactory, IMemoryCache memoryCache, ILogger<PropertyService> logger)
        {
            _apiClientFactory = apiClientFactory ?? throw new ArgumentNullException(nameof(apiClientFactory));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<AgentViewModel>> GetTopAsync(int topElements)
        {
            try
            {
                if (_memoryCache.TryGetValue(Constants.TopAgentsKey, out IReadOnlyCollection<AgentViewModel> cachedAgents))
                {
                    return cachedAgents;
                }

                var agents = (await GetAllAsync().ConfigureAwait(false))
                    .Select(x => x.ToViewModel())
                    .Take(Math.Min(topElements, Constants.MaxTopElements))
                    .ToList();

                _memoryCache.Set(Constants.TopAgentsKey, agents, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(Constants.ExpirationMinutes),
                    SlidingExpiration = TimeSpan.FromMinutes(Constants.ExpirationMinutes)
                });

                return agents;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Gets the collection of properties grouped by real estate agent id.
        /// </summary>
        /// <returns>The collection of grouped properties.</returns>
        private async Task<IEnumerable<IGrouping<int, PropertyDto>>> GetAllAsync()
        {
            IEnumerable<IGrouping<int, PropertyDto>> propertyGroupCollection = new List<IGrouping<int, PropertyDto>>();

            ApiResponseDto response;
            int pageNumber = 1;

            try
            {
                do
                {
                    response = await _apiClientFactory.Create()
                      .GetAsync(GetUri(pageNumber++))
                      .ConfigureAwait(false);

                    propertyGroupCollection = MergeGroupings(propertyGroupCollection, response.Objects);
                }
                while (response?.Pagination?.CurrentPage != response?.Pagination?.NumberOfPages);

                return propertyGroupCollection;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Merges the list of new properties with a collection of group of properties.
        /// </summary>
        /// <param name="propertyCollection">The groups of properties.</param>
        /// <param name="properties">The new list of properties.</param>
        /// <returns>The groups of properties.</returns>
        private IEnumerable<IGrouping<int, PropertyDto>> MergeGroupings(IEnumerable<IGrouping<int, PropertyDto>> propertyCollection,
            IReadOnlyCollection<PropertyDto> properties)
        {
            if (propertyCollection == null)
            {
                throw new ArgumentNullException(nameof(propertyCollection));
            }

            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            var flattenedProperties = propertyCollection.SelectMany(properties => properties);

            return Enumerable.Concat(flattenedProperties, properties)
                .GroupBy(p => p.RealEstateAgentId)
                .OrderByDescending(x => x.Count());
        }

        /// <summary>
        /// Gets the uri for a specific page number.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>Returns the uri.</returns>
        private string GetUri(int pageNumber)
            => string.Format(ApiConstants.SaleObjectsPath, pageNumber, ApiConstants.PageSize);
    }
}
