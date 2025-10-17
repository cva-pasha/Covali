# Covali.Api
[![NuGet](https://img.shields.io/nuget/v/Covali.Api)](https://www.nuget.org/packages/Covali.Api)

## Overview
The `Covali.Api` library for .NET provides a simple and efficient way to organize and register minimal API endpoints in your ASP.NET Core applications. It facilitates a clean architecture by allowing you to define each endpoint in its own class, which can then be automatically discovered and mapped by the framework. This approach leverages dependency injection (DI) to seamlessly integrate with your application's services.

## Features
- **Automatic Endpoint Discovery**: Scans your assemblies to find all implementations of `IEndpoint` and registers them with the DI container.
- **Simplified Endpoint Mapping**: A single extension method call (`MapEndpoints`) is all that's needed to map all discovered endpoints.
- **Clean Architecture**: Encourages separating endpoint definitions into individual files, improving code organization and maintainability.
- **Dependency Injection**: Built to work with the .NET DI container, allowing your endpoints to easily access other services.
- **Extensibility**: Provides a flexible foundation for adding more complex API structures, such as versioning or feature grouping.

## Getting Started

### Installation
Add the `Covali.Api` library to your project via NuGet:
```bash
dotnet add package Covali.Api
```

### Usage
Follow these steps to integrate `Covali.Api` into your ASP.NET Core application.

#### 1. Define Your Endpoints
Create a class for each of your API endpoints and implement the `IEndpoint` interface. This interface has a single method, `MapEndpoint`, where you define your route, HTTP method, and handler.

```csharp
// Endpoints/GetWeatherForecastEndpoint.cs
using Covali.Api;

public class GetWeatherForecastEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/weatherforecast", () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    "Sunny" // A summary for the weather
                ))
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();
    }
}

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
```

#### 2. Configure Services in `Program.cs`
In your `Program.cs` file, call the `AddEndpoints` extension method on `IServiceCollection`. This method scans the specified assembly (or assemblies) for classes that implement `IEndpoint` and registers them for discovery.

```csharp
// Program.cs
using Covali.Api;

var builder = WebApplication.CreateBuilder(args);

// 1. Add and discover endpoints from the specified assembly
builder.Services.AddEndpoints(typeof(Program));

// Add other services like Swagger/OpenAPI if needed
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 2. Map all discovered endpoints
app.MapEndpoints();

app.Run();
```

#### 3. Run Your Application
With the endpoints defined and mapped, run your application. The `GetWeatherForecast` endpoint will be available at `/weatherforecast`. If you have Swagger configured, you can also use its UI to test the endpoint.

### How It Works
- **`AddEndpoints(params Type[] scanMarkers)`**: This extension method scans the assemblies containing the provided marker types (e.g., `typeof(Program)`) for any public, non-abstract classes that implement `IEndpoint`. It then registers these classes in the DI container with a transient lifetime.
- **`MapEndpoints()`**: This method resolves all registered `IEndpoint` services from the DI container and calls the `MapEndpoint` method on each one, effectively mapping all your application's endpoints.

## Contributing
Contributions are welcome! If you have ideas for improvements or find any issues, please open an issue or submit a pull request on the project's GitHub repository.

## License
This project is licensed under the MIT License. See the [LICENSE](https://github.com/cva-pasha/Covali/blob/main/LICENSE.txt) file for more details.
