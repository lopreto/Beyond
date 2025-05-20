using Beyond.TodoProject.Domain.Interfaces;

namespace Beyond.TodoProject.Infraestructure.Repositories
{
	public class TodoListRepository : ITodoListRepository
	{
		private int _lastId = 0;
		private readonly List<string> _categories;

		public TodoListRepository()
		{
			_categories = new List<string> { "Work" };
		}

		public List<string> GetAllCategories()
		{
			return _categories;
		}

		public int GetNextId()
		{
			_lastId++;
			return _lastId;
		}
	}
}
