using Beyond.TodoProject.ConsoleApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder(args)
	.ConfigureServices((context, services) =>
	{
		var startup = new Startup();
		startup.ConfigureServices(services);
		services.AddHostedService<ConsoleApp>();
	})
	.Build()
	.RunAsync();
