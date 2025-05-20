namespace Beyond.TodoProject.Application.Interfaces
{
	public interface ITodoListService
	{
		void AddItem(string title, string description, string category);
		void UpdateItem(int id, string description);
		void RemoveItem(int id);
		void RegisterProgression(int id, DateTime dateTime, decimal percent);
		void PrintItems();
	}
}
