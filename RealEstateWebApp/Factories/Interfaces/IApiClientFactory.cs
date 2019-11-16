using Domain.Interfaces;

namespace RealEstate.Presentation.Factories.Interfaces
{
    public interface IApiClientFactory
    {
        IApiClient Create();
    }
}
