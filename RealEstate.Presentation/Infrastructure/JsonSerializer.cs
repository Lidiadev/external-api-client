using RealEstate.Presentation.Common;
using RealEstate.Presentation.Infrastructure.Interfaces;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace RealEstate.Presentation.Infrastructure
{
    public class JsonSerializer<T> : ISerializer<T>
    {
        public async Task<T> DeserializeAsync(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new NotSupportedException("The stream cannot be read.");
            }

            return await JsonSerializer.DeserializeAsync<T>(stream, DefaultJsonSerializerOptions.Options);
        }
    }
}
