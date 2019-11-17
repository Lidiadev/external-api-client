# external-api-client

[![Build Status](https://travis-ci.com/Lidiadev/external-api-client.png?branch=master)](https://travis-ci.com/Lidiadev/external-api-client)

RealEstate is an ASP.NET Core MVC web application with the following feature:
- get the top 10 real estate agents with the most properties.

Design considerations:
* In order to get the top 10 real estate agents, the `PropertyService` is making calls to the PartnerAPI. 
* Because the external API is returning a paginated response, all the pages have to be retrieved to be able to get the top 10 agents.
* In order to minimize the data stored in memory, every time a partial list is retrieved, the list of the top 10 is computed in place. That means that there won't be more than the size of the page agents stored in memory.
* Cache is used to improve performance.
* Retry pattern is used with Polly.

## Prerequirements

* Visual Studio 2017 
* .NET Core 3.0.1 SDK 

## Frameworks Used

* .NET Core 3.0
* NUnit 

## Solution Overview

#### RealEstate.Domain
- DTOs

#### RealEstate.Presentation
- ASP.NET Core Web MVC
- Application contracts and implementation.

#### RealEstate.UnitTests
- Unit tests for all layers.

#### RealEstate.IntegrationsTests
- Integration tests for getting the agents list.

## Continuous Integration

**Travis CI** has been used to run the tests.
Each pushed commit runs the unit tests.

## How to Run the Application
* Replace in [appsettings.json](https://github.com/Lidiadev/external-api-client/blob/master/RealEstate.Presentation/appsettings.json) the `APIKey` and the `PartnerAPI` base Url:
``` 
    "PartnerAPI": {
        "APIKey": "TBR"
    },
    "BaseUrls": {
        "PartnerAPI": "TBR"
    }
```    
* Replace the `SalesObjectsPath` the [Constants](https://github.com/Lidiadev/external-api-client/blob/master/RealEstate.Presentation/Common/Constants/ApiConstants.cs) file:
```
/// <summary>
/// The path for objects which are on sale.
/// </summary>
public const string SaleObjectsPath = "TBR";
```
## Polly 
Polly is a library that provides resilience and transient-fault handling capabilities.
Polly has been used to define a `Retry policy` for the `HttpClient`:
```
 services.AddHttpClient<ApiClient>(x => 
        {
            x.BaseAddress = new Uri(baseUrls.PartnerAPI); 
        })
        .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
        {
            TimeSpan.FromSeconds(1),
            TimeSpan.FromSeconds(5),
            TimeSpan.FromSeconds(10)
        }));
```
## Memory Cache
Memory Cache has been used to improve perfomance. The cached item has a sliding expiration and an absolute expiration. 

## How To Run Unit Tests

* Open solution in Visual Studio 2019
* Open **Test Explorer** 
* Run the **Order.UnitTests** tests.

## How To Run Integration Tests

* Open solution in Visual Studio 2019
* Open **Test Explorer** 
* Run the **Order.IntegrationTests** tests.

## Libraries Used for Testing

* Moq - mocking framewework used to mimic the behavior of classes and interfaces
* FluentAssertions
