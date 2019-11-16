using Domain.Interfaces;

namespace RealEstateWebApp.Factories.Interfaces
{
    public interface IApiClientFactory
    {
        IApiClient Create();
    }
}
