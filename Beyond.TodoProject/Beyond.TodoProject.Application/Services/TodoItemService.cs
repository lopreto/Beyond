using Beyond.TodoProject.Application.Interfaces;
using Beyond.TodoProject.Domain.Aggregates;

namespace Beyond.TodoProject.Application.Services
{
	public class TodoItemService : ITodoItemService
	{
		public void Print()
		{
			try
			{
				var aggreg = new TodoList();

				aggreg.AddItem(1, "Complete Project Report", "Finish the final report for the project", "Work");

				aggreg.RegisterProgression(1, new DateTime(2025,03,18), 30);
				aggreg.RegisterProgression(1, new DateTime(2025, 03, 19), 50);
				aggreg.RegisterProgression(1, new DateTime(2025, 03, 20), 20);


				aggreg.PrintItems();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
