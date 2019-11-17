using Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using RealEstate.Domain.Dtos;
using RealEstate.Presentation.Common.Configuration;
using RealEstate.Presentation.Infrastructure.Interfaces;
using RealEstate.Presentation.Services;
using RealEstate.UnitTests.Utilities;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace RealEstate.UnitTests.Services
{
    [TestFixture]
    public class ApiClientTests
    {
        private Mock<ILogger<ApiClient>> _logger;
        private Mock<IOptions<PartnerApiCredentials>> _credentials;
        private Mock<ISerializer<ApiResponseDto>> _serializer;
        private IApiClient _subject;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<ApiClient>>();
            _serializer = new Mock<ISerializer<ApiResponseDto>>();
            _credentials = new Mock<IOptions<PartnerApiCredentials>>();

            _credentials.Setup(x => x.Value).Returns(new PartnerApiCredentials { APIKey = "value" });

            _subject = new ApiClient(GetMockHttpClient(), 
                _credentials.Object, 
                _serializer.Object,
                _logger.Object);
        }

        [Test]
        public async Task GetAsync_ShouldReturnTheApiResponse()
        {
            // Arrange
            const string uri = "/test";
            _serializer.Setup(x => x.DeserializeAsync(It.IsAny<MemoryStream>()))
                .Returns(Task.FromResult(new ApiResponseDto()));

            // Act
            var result = await _subject.GetAsync(uri);

            // Assert
            result.Should().NotBeNull();
        }

        private HttpClient GetMockHttpClient()
        {
            var responseMessage = new HttpResponseMessage
            {
                Content = new FakeHttpContent(TestData.ApiReponse)
            };

            return new HttpClient(new FakeHttpMessageHandler(responseMessage))
            { 
                BaseAddress = new Uri("http://test.com") 
            };
        }
    }
}
