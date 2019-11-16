using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace RealEstate.Presentation.Common.Extensions
{
    /// <summary>
    /// Extensions for Stream.
    /// </summary>
    public static class StreamExtensions
    {
        public async static Task<T> DeserializeFromJsonAsync<T>(this Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new NotSupportedException("can't read from stream");
            }

            return await JsonSerializer.DeserializeAsync<T>(stream, DefaultJsonSerializerOptions.Options);
        }
    }
}
