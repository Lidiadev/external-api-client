using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using RealEstate.Presentation.Models.Agent;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RealEstate.IntegrationTests
{
    [TestFixture]
    public class AgentControllerTests 
    {
        private WebApplicationFactory<RealEstateWebApp.Startup> _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _factory = new WebApplicationFactory<RealEstateWebApp.Startup>();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task Index_ShouldReturnSuccessAndCorrectContentType()
        {
            // Arrange
            // Act
            var response = await _client.GetAsync("/agent");

            // Assert
            response.EnsureSuccessStatusCode();
            var model = await response.Content.ReadAsStringAsync();
            model.Should().NotBeNullOrEmpty();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}