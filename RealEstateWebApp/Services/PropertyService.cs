using Domain.Common;
using Domain.Dtos;
using Microsoft.Extensions.Logging;
using RealEstateWebApp.Common.Constants;
using RealEstateWebApp.Common.Extensions;
using RealEstateWebApp.Factories.Interfaces;
using RealEstateWebApp.Models.Property;
using RealEstateWebApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateWebApp.Services
{
    /// <summary>
    /// The service for Properties.
    /// </summary>
    public class PropertyService : IPropertyService
    {
        private ILogger<PropertyService> _logger;
        private readonly IApiClientFactory _apiClientFactory;

        public PropertyService(IApiClientFactory apiClientFactory, ILogger<PropertyService> logger)
        {
            _apiClientFactory = apiClientFactory ?? throw new ArgumentNullException(nameof(apiClientFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<PropertyViewModel>> GetTopAsync(int topElements)
            => (await GetAllAsync())
                .Select(x => x.ToViewModel())
                .Take(Math.Min(topElements, Constants.MaxTopElements))
                .ToList();

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
                      .GetAsync(GetUri(pageNumber++));

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
