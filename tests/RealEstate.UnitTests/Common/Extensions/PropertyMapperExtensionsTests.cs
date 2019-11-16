using FluentAssertions;
using NUnit.Framework;
using RealEstate.Domain.Dtos;
using RealEstate.Presentation.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RealEstate.UnitTests.Common.Extensions
{
    [TestFixture]
    public class PropertyMapperExtensionsTests
    {
        [Test]
        public void ToViewModel_ShouldThrowException_WhenDtoIsNull()
        {
            // Arrange
            IGrouping<int, PropertyDto> dto = null;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => dto.ToViewModel());
        }

        [Test]
        public void ToViewModel_ShouldReturnTheViewModel()
        {
            // Arrange
            var properties = new List<PropertyDto>
            {
                new PropertyDto{ RealEstateAgentId = 111, RealEstateAgentName = "Test", Id = "1" },
                new PropertyDto{ RealEstateAgentId = 111, RealEstateAgentName = "Test", Id = "2" },
                new PropertyDto{ RealEstateAgentId = 111, RealEstateAgentName = "Test", Id = "3" },
                new PropertyDto{ RealEstateAgentId = 111, RealEstateAgentName = "Test", Id = "4" }
            };

            IGrouping<int, PropertyDto> dto = properties.GroupBy(p => p.RealEstateAgentId).First();

            // Act
            var result = dto.ToViewModel();

            // Assert
            result.Should().NotBeNull();
            result.RealEstateAgentId.Should().Be(properties.First().RealEstateAgentId);
            result.RealEstateAgentName.Should().Be(properties.First().RealEstateAgentName);
            result.Count.Should().Be(properties.Count());
        }
    }
}
