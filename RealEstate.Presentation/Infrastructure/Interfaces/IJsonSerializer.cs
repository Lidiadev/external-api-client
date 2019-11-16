using System.IO;
using System.Threading.Tasks;

namespace RealEstate.Presentation.Infrastructure.Interfaces
{
    public interface ISerializer<T>
    {
        Task<T> DeserializeAsync(Stream stream);
    }
}
