using FluentAssertions;
using NUnit.Framework;
using RealEstate.Domain.Dtos;
using RealEstate.Presentation.Infrastructure;
using RealEstate.Presentation.Infrastructure.Interfaces;
using RealEstate.UnitTests.Utilities;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RealEstate.UnitTests.Common.Extensions
{
    [TestFixture]
    public class JsonSerializerTest
    {
        private ISerializer<ApiResponseDto> _subject;

        [SetUp]
        public void Setup()
        {
            _subject = new JsonSerializer<ApiResponseDto>();
        }

        [Test]
        public async Task DeserializeFromJsonAsync_ShouldDeserializeTheStream()
        {
            // Arrange
            var bytes = Encoding.UTF8.GetBytes(TestData.ApiReponse);

            // Act
            var result = await _subject.DeserializeAsync(new MemoryStream(bytes)) as ApiResponseDto;

            // Assert
            result.Should().NotBeNull();
            result.Objects.Should().NotBeNull();
            result.Objects.Count.Should().Be(3);
            result.Pagination.Should().NotBeNull();
            result.Pagination.CurrentPage.Should().Be(1);
            result.Pagination.NumberOfPages.Should().Be(1);
        }

        [Test]
        public void DeserializeFromJsonAsync_ShouldThrowException_WhenStreamIsNull()
        {
            // Arrange
            Stream stream = null;

            // Act
            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _subject.DeserializeAsync(stream));
        }

        [Test]
        public void DeserializeFromJsonAsync_ShouldThrowException_WhenStreamIsEmpty()
        {
            // Arrange
            using Stream stream = new MemoryStream();

            // Act
            // Assert
            Assert.ThrowsAsync<JsonException>(async () => await _subject.DeserializeAsync(stream));
        }

        [Test]
        public void DeserializeFromJsonAsync_ShouldThrowException_WhenStreamCannotBeDeserializedIntoT()
        {
            // Arrange
            using Stream stream = new MemoryStream();
            var buffer = Encoding.ASCII.GetBytes("test");
            stream.Write(buffer: buffer, offset: 0, count: buffer.Length);

            // Act
            // Assert
            Assert.ThrowsAsync<JsonException>(async () => await _subject.DeserializeAsync(stream));
        }
    }
}
