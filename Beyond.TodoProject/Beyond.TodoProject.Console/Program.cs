using Beyond.TodoProject.Application.Interfaces;
using Beyond.TodoProject.Application.Services;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddScoped<ITodoItemService, TodoItemService>();

var provider = services.BuildServiceProvider();
using var scope = provider.CreateScope();

var miServicio = scope.ServiceProvider.GetRequiredService<ITodoItemService>();

miServicio.Print();