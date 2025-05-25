using Beyond.TodoProject.Application.Interfaces;
using Beyond.TodoProject.Application.Services;
using Beyond.TodoProject.Domain.Aggregates;
using Beyond.TodoProject.Domain.Interfaces;
using Beyond.TodoProject.Infraestructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Beyond.TodoProject.ConsoleApp
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddLogging(config =>
			{
				config.AddConsole();
			});

			services.AddScoped<ITodoList, TodoList>();
			services.AddScoped<ITodoListService, TodoListService>();
			services.AddScoped<ITodoListRepository, TodoListRepository>();
		}
	}
}
