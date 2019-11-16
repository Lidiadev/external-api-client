using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RealEstate.Presentation.Controllers;
using RealEstate.Presentation.Models.Agent;
using RealEstate.Presentation.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.UnitTests.Controllers
{
    [TestFixture]
    public class AgentControllerTests
    {
        private Mock<ILogger<HomeController>> _logger;
        private Mock<IPropertyService> _propertyService;
        private AgentController _subject;

        [SetUp]
        public void Setup()
        {
            _propertyService = new Mock<IPropertyService>();
            _logger = new Mock<ILogger<HomeController>>();
            _subject = new AgentController(_logger.Object, _propertyService.Object);
        }

        [Test]
        public async Task Index_ShouldReturnTheViewModel()
        {
            // Arrange
            const int numberOfElements = 10;
            IReadOnlyCollection<AgentViewModel> viewModels = new List<AgentViewModel>
            {
                new AgentViewModel(),
                new AgentViewModel()
            };
            _propertyService.Setup(x => x.GetTopAsync(numberOfElements))
                .Returns(Task.FromResult(viewModels));

            // Act
            var result = await _subject.Index() as ViewResult;

            // Assert
            result.Should().NotBeNull();
            var model = result.Model as IReadOnlyCollection<AgentViewModel>;
            model.Should().NotBeNull();
            model.Count.Should().Be(viewModels.Count);
            _propertyService.Verify(x => x.GetTopAsync(numberOfElements), Times.Once);
        }
    }
}
