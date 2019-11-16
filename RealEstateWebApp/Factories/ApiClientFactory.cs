using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Presentation.Factories.Interfaces;
using RealEstate.Presentation.Services;
using System;

namespace RealEstate.Presentation.Factories
{
    public class ApiClientFactory : IApiClientFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ApiClientFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IApiClient Create()
            => _serviceProvider.GetRequiredService<ApiClient>();
    }
}
