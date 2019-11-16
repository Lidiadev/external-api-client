using FluentAssertions;
using NUnit.Framework;
using RealEstate.Domain.Dtos;
using RealEstate.Presentation.Infrastructure;
using RealEstate.Presentation.Infrastructure.Interfaces;
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
        private ISerializer<object> _subject;

        [SetUp]
        public void Setup()
        {
            _subject = new JsonSerializer<object>();
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
