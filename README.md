# Beyond API

## Developer Considerations

* The `ITodoList` and `ITodoListRepository` interfaces are registered as singletons to keep values in memory during the application's lifetime. In a real-world scenario, they should be configured as transient or scoped depending on the requirements.

* Unit tests for the service are implemented only for the `AddItem` method as an example. In a production-ready application, all methods should be covered, although the aggregate already contains unit tests where the domain logic is applied.

* The `PrintItems` method only sorts the list to follow the Single Responsibility Principle. Presentation layers (Console and Angular) are responsible for rendering the data.

## Running the project locally 🚀

### To run the project:

1. Open the solution in Visual Studio or your preferred IDE  
2. Set `Microservice.Host` (or your API project) as the startup project  
3. Run the application (F5 or `dotnet run` if using CLI)

### Then open your browser at:
http://localhost:5000 or the port configured in `launchSettings.json`

## Pre-requisites 📋

* .NET 8.0 SDK  
* Visual Studio 2022 or newer (or Visual Studio Code)  
* SQL Server (or configured database)  
* Swagger/OpenAPI client (optional)  

## Built with 🛠️

* [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) - Cross-platform framework for building APIs and applications.
* [ASP.NET Core Web API](https://learn.microsoft.com/en-us/aspnet/core/web-api/) - For building RESTful services.
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) - ORM for database access.
* [AutoMapper](https://automapper.org/) - Library to map objects automatically.
* [FluentValidation](https://docs.fluentvalidation.net/) - For building strongly-typed validation rules.
* [Swashbuckle (Swagger)](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) - For generating Swagger UI for the API.
