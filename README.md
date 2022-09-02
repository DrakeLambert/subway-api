# Subway API

This HTTP API enables users to get a predefined list of subway stations and save their favorites. Currently, all state is stored in memory and is lost during restart.

## Build

1. Install .NET 6 SDK, *required for run and test*
2. `dotnet build`

## Run

1. `cd src`
2. `dotnet dev-certs https --trust`
3. `dotnet run`
4. Browse `https://localhost:7295/swagger`

## Test

Unit tests:

1. `dotnet test`

E2E tests:

1. Install Powershell Core
2. Follow [steps to run](#run)
3. Open an additional terminal running Powershell
4. `cd e2e-tests`
5. `./Test-SubwayApi.ps1`

## Built On

**Web framework**: ASP.NET  
**ORM**: Entity Framework  
**Unit testing**: Xunit  
**E2E testing**: Powershell
