using System.Text;

namespace Beyond.TodoProject.Domain.Aggregates
{
	public class TodoList : ITodoList
	{
		private readonly List<TodoItem> _items = new();

		public void AddItem(int id, string title, string description, string category)
		{
			if (_items.Any(item => item.Id == id))
				throw new InvalidOperationException($"Ya existe un ítem con el ID {id}.");

			if (string.IsNullOrWhiteSpace(title))
				throw new ArgumentException("El título es obligatorio.", nameof(title));

			var newItem = new TodoItem(id, title, description, category);
			_items.Add(newItem);
		}

		public void PrintItems()
		{
			var orderedItems = _items.OrderBy(x => x.Id);
			foreach (var item in orderedItems)
			{
				Console.WriteLine($"{item.Id}) {item.Title} - {item.Description} ({item.Category}) Completed:{item.IsCompleted}.");
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

		public void RegisterProgression(int id, DateTime dateTime, decimal percent)
		{
			if (percent <= 0 || percent >= 100)
				throw new ArgumentException("El porcentaje debe ser mayor que 0 y menor que 100.");

			var item = _items.FirstOrDefault(x => x.Id == id);
			if (item == null)
				throw new ArgumentException($"No se encontró ningún TodoItem con Id {id}.");

			if (item.Progressions.Any(p => p.DateTime >= dateTime))
				throw new InvalidOperationException("La fecha de la nueva progresión debe ser mayor a las fechas ya registradas.");

			var totalProgress = item.Progressions.Sum(p => p.Percent) + percent;
			if (totalProgress > 100)
				throw new InvalidOperationException("La suma total del progreso no puede exceder el 100%.");

			item.Progressions.Add(new Progression(dateTime, percent));

			if (totalProgress == 100)
				item.SetCompleted();
		}

		public void RemoveItem(int id)
		{
			var item = _items.FirstOrDefault(x => x.Id == id);

			if (item == null)
				throw new ArgumentException($"No se encontró ningún TodoItem con Id {id}.");

			var totalProgress = item.Progressions.Sum(p => p.Percent);
			if (totalProgress > 50)
				throw new ArgumentException($"No se puede borrar el TodoItem con Id {id}.");

			_items.Remove(item);
		}

		public void UpdateItem(int id, string description)
		{
			var item = _items.FirstOrDefault(x => x.Id == id);

			if (item == null)
				throw new ArgumentException($"No se encontró ningún TodoItem con Id {id}.");

			var totalProgress = item.Progressions.Sum(p => p.Percent);
			if (totalProgress > 50)
				throw new ArgumentException($"No se puede cambiar el TodoItem con Id {id}. Tiene mas de 50% completo");

			item.UpdateDescription(description);
		}
	}

	internal class TodoItem
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

	internal class Progression
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
