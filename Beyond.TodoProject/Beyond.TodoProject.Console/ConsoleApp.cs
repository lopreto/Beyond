using Beyond.TodoProject.Application.Interfaces;
using Microsoft.Extensions.Hosting;

namespace Beyond.TodoProject.ConsoleApp
{
	public class ConsoleApp : IHostedService
	{
		private readonly ITodoListService _todoListService;
		public ConsoleApp(ITodoListService todoListService)
		{
			_todoListService = todoListService;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			await PrintItems();
		}

		public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

		private async Task PrintItems()
		{
			await _todoListService.AddItem("Complete Project Report", "Finish the final report for the project", "Work");

			await _todoListService.RegisterProgression(1, new DateTime(2025, 03, 18), 30);
			await _todoListService.RegisterProgression(1, new DateTime(2025, 03, 19), 50);
			await _todoListService.RegisterProgression(1, new DateTime(2025, 03, 20), 20);

			var result = await _todoListService.PrintItems();

			if (result is null)
				Console.WriteLine("Items not found.");

			foreach (var item in result.Data)
			{
				System.Console.WriteLine($"{item.Id}) {item.Title} - {item.Description} ({item.Category}) Completed:{item.IsCompleted}.");
				decimal accumulatedPercent = 0;
				foreach (var progression in item.Progressions)
				{
					accumulatedPercent += progression.Percent;
					int filled = (int)(accumulatedPercent / 2);
					int empty = 50 - filled;

					var progressBar = new string('0', filled) + new string(' ', empty);

					Console.WriteLine($"{progression.DateTime.ToString("M/d/yyyy h:mm:ss tt")} - {accumulatedPercent}%\t|{progressBar}|");

				}
				Console.WriteLine("\n\n");
			}
		}
	}
}
