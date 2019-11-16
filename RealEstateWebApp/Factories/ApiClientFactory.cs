using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using RealEstateWebApp.Factories.Interfaces;
using RealEstateWebApp.Services;
using System;

namespace RealEstateWebApp.Factories
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
