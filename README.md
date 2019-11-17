# external-api-client

[![Build Status](https://travis-ci.com/Lidiadev/external-api-client.png?branch=master)](https://travis-ci.com/Lidiadev/external-api-client)

RealEstate is an ASP.NET Core MVC web application with the following feature:
- get the top 10 real estate agents with the most properties.

## Prerequirements

* Visual Studio 2017 
* .NET Core 3.0.1 SDK 

## Frameworks Used

* .NET Core 3.0
* NUnit 

## Solution Overview

### RealEstate.Domain
- DTOs

### RealEstate.Presentation
- ASP.NET Core Web MVC
- Application contracts and implementation.

### RealEstate.UnitTests
- Unit tests for all layers.

### RealEstate.IntegrationsTests
- Integration tests for getting the agents list.

## Continuous Integration

**Travis CI** has been used to run the tests.
Each pushed commit runs the unit tests.

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
