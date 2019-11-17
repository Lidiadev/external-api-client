using Domain.Common;
using Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RealEstate.Domain.Dtos;
using RealEstate.Presentation.Factories.Interfaces;
using RealEstate.Presentation.Models.Agent;
using RealEstate.Presentation.Services;
using RealEstate.Presentation.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.UnitTests.Services
{
    [TestFixture]
    public class PropertyServiceTests
    {
        private Mock<ILogger<PropertyService>> _logger;
        private Mock<IApiClientFactory> _apiClientFactory; 
        private Mock<IApiClient> _apiClient;
        private Mock<IMemoryCache> _memoryCache;
        private IPropertyService _subject;

        [SetUp]
        public void Setup()
        {
            _apiClientFactory = new Mock<IApiClientFactory>();
            _apiClient = new Mock<IApiClient>();
            _memoryCache = new Mock<IMemoryCache>();
            _logger = new Mock<ILogger<PropertyService>>();
            _subject = new PropertyService(_apiClientFactory.Object, 
                _memoryCache.Object, 
                _logger.Object);
        }

        [Test]
        public async Task GetTopAsync_ShouldReturnAgentViewModelsFromTheApiClient()
        {
            // Arrange
            const int numberOfElements = 10;
            _apiClientFactory.Setup(x => x.Create())
                .Returns(_apiClient.Object);
            _apiClient.Setup(x => x.GetAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new ApiResponseDto { Objects = new List<PropertyDto>() }));

            // Act
            var result = await _subject.GetTopAsync(numberOfElements);

            // Assert
            result.Should().NotBeNull();
        }
    }
}
