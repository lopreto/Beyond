using Beyond.TodoProject.Application.Interfaces;
using Beyond.TodoProject.Application.Services;
using Beyond.TodoProject.Domain.Aggregates;
using Beyond.TodoProject.Domain.Interfaces;
using Beyond.TodoProject.Infraestructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddScoped<ITodoList, TodoList>();
services.AddScoped<ITodoListService, TodoListService>();
services.AddScoped<ITodoListRepository, TodoListRepository>();

var provider = services.BuildServiceProvider();
using var scope = provider.CreateScope();


var todoListService = scope.ServiceProvider.GetRequiredService<ITodoListService>();

todoListService.AddItem("Complete Project Report", "Finish the final report for the project", "Work");

todoListService.RegisterProgression(1, new DateTime(2025, 03, 18), 30);
todoListService.RegisterProgression(1, new DateTime(2025, 03, 19), 50);
todoListService.RegisterProgression(1, new DateTime(2025, 03, 20), 20);

todoListService.PrintItems();