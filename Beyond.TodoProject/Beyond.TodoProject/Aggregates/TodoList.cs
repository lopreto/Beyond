using System.Text;

namespace Beyond.TodoProject.Domain.Aggregates
{
	public class TodoList : ITodoList
	{
		private List<TodoItem> _todoItems = new();

		public IReadOnlyList<TodoItem> TodoItems => _todoItems;

		public void AddItem(int id, string title, string description, string category)
		{
			if (_todoItems.Any(item => item.Id == id))
				throw new ArgumentException($"An item with Id {id} already exists.");

			if (string.IsNullOrWhiteSpace(title))
				throw new ArgumentException("The title is required.", nameof(title));

			var newItem = new TodoItem(id, title, description, category);
			_todoItems.Add(newItem);
		}

		public void PrintItems()
		{
			var orderedItems = _todoItems.OrderBy(x => x.Id);
			_todoItems = orderedItems.ToList();
		}

		public void RegisterProgression(int id, DateTime dateTime, decimal percent)
		{
			if (percent <= 0 || percent >= 100)
				throw new ArgumentException("The percentage must be greater than 0 and less than 100.");

			var item = _todoItems.FirstOrDefault(x => x.Id == id);
			if (item == null)
				throw new ArgumentException($"No TodoItem found with Id {id}.");

			if (item.Progressions.Any(p => p.DateTime >= dateTime))
				throw new ArgumentException("The date of the new progression must be greater than the dates already recorded.");

			var totalProgress = item.Progressions.Sum(p => p.Percent) + percent;
			if (totalProgress > 100)
				throw new ArgumentException("The total sum of progress cannot exceed 100%.");

			item.Progressions.Add(new Progression(dateTime, percent));

			if (totalProgress == 100)
				item.SetCompleted();
		}

		public void RemoveItem(int id)
		{
			var item = _todoItems.FirstOrDefault(x => x.Id == id);

			if (item == null)
				throw new ArgumentException($"No TodoItem found with Id {id}.");

			var totalProgress = item.Progressions.Sum(p => p.Percent);
			if (totalProgress > 50)
				throw new ArgumentException($"Cannot delete TodoItem with Id {id}.");

			_todoItems.Remove(item);
		}

		public void UpdateItem(int id, string description)
		{
			var item = _todoItems.FirstOrDefault(x => x.Id == id);

			if (item == null)
				throw new ArgumentException($"No TodoItem found with Id {id}.");

			var totalProgress = item.Progressions.Sum(p => p.Percent);

			if (totalProgress > 50)
				throw new ArgumentException($"The TodoItem with Id {id} cannot be changed. It is more than 50% complete.");

			item.UpdateDescription(description);
		}
	}

	public class TodoItem
	{
		public int Id { get; private set; }
		public string Title { get; private set; }
		public string Description { get; private set; }
		public string Category { get; private set; }
		public List<Progression> Progressions { get; private set; }
		public bool IsCompleted { get; private set; }

		public TodoItem(int id, string title, string description, string category)
		{
			Id = id;
			Title = title;
			Description = description;
			Category = category;
			Progressions = new();
			IsCompleted = false;
			Progressions = new();
		}

		public void UpdateDescription(string description)
		{
			Description = description;
		}

		public void SetCompleted()
		{
			IsCompleted = true;
		}
	}

	public class Progression
	{
		public DateTime DateTime { get; set; }
		public decimal Percent { get; set; }

		public Progression(DateTime dateTime, decimal percent)
		{
			DateTime = dateTime;
			Percent = percent;
		}
	}
}
