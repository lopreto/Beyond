# Beyond API

## Developer Considerations

* The `ITodoList` and `ITodoListRepository` interfaces are registered as singletons to keep values in memory during the application's lifetime. In a real-world scenario, they should be configured as transient or scoped depending on the requirements.

* Unit tests for the service are implemented only for the `AddItem` method as an example. In a production-ready application, all methods should be covered, although the aggregate already contains unit tests where the domain logic is applied.

* The PrintItems method only sorts the list, while rendering the data is delegated to the presentation layers (Console and Angular). This separation of concerns adheres to the Single Responsibility Principle.

## Running the project locally 🚀

### To run the project:

1. Open the solution in Visual Studio or your preferred IDE  
2. Set `Beyond.Todo.Project.Console` o `Beyond.Todo.Project.WebApi` as the startup project  
3. Run the application (F5 or `dotnet run` if using CLI)

### Then open your browser at:
http://localhost:7269 or the port configured in `launchSettings.json`

## Pre-requisites 📋

* .NET 8.0 SDK  
* Visual Studio 2022 or newer (or Visual Studio Code)  
* Swagger/OpenAPI client (optional)  

## Built with 🛠️


* [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) - Cross-platform framework for building APIs and applications.
* [ASP.NET Core Web API](https://learn.microsoft.com/en-us/aspnet/core/web-api/) - For building RESTful services.
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) - ORM for data access.
* [AutoMapper](https://automapper.org/) - Library to map objects automatically.
* [FluentValidation](https://docs.fluentvalidation.net/) - For strongly-typed model validation.
* [Swashbuckle (Swagger)](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) - For generating Swagger UI for the API.
* [xUnit](https://xunit.net/) - Unit testing framework for .NET.
* [Moq](https://github.com/moq/moq4) - Mocking library for unit tests.
* [FluentAssertions](https://fluentassertions.com/) - Provides more readable and expressive assertions in tests.
* [Newtonsoft.Json](https://www.newtonsoft.com/json) - Popular JSON framework for .NET.
