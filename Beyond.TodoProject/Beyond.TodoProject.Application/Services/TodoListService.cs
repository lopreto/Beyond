using Beyond.TodoProject.Application.Interfaces;
using Beyond.TodoProject.Domain.Aggregates;
using Beyond.TodoProject.Domain.DTOs;
using Beyond.TodoProject.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Beyond.TodoProject.Application.Services
{
	public class TodoListService : ITodoListService
	{
		private readonly TodoList _todoList;
		private readonly ITodoListRepository _todoListRepository;
		private readonly ILogger<TodoListService> _logger;

		public TodoListService(ITodoList todoListTest, ITodoListRepository todoListRepository, ILogger<TodoListService> logger)
		{
			_todoList = todoListTest as TodoList ?? throw new InvalidOperationException("A specific instance of TodoList was expected.");
			_todoListRepository = todoListRepository;
			_logger = logger;
		}

		public async Task<BaseResultDto<int>> AddItem(string title, string description, string category)
		{
			try
			{
				var allCategories = _todoListRepository.GetAllCategories();
				var id = _todoListRepository.GetNextId();

				if (allCategories == null || !allCategories.Any(x => x.Equals(category, StringComparison.InvariantCultureIgnoreCase)))
					throw new ArgumentException("Category not found.");

				_todoList.AddItem(id, title, description, category);
				return new BaseResultDto<int>(id);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return new BaseResultDto<int>(ex.Message);
			}
		}

		public async Task<BaseResultDto<bool>>UpdateItem(int id, string description)
		{
			try
			{
				_todoList.UpdateItem(id, description);
				return new BaseResultDto<bool>(true);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return new BaseResultDto<bool>(ex.Message);
			}
		}

		public async Task<BaseResultDto<bool>>RemoveItem(int id)
		{
			try
			{
				_todoList.RemoveItem(id);
				return new BaseResultDto<bool>(true);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return new BaseResultDto<bool>(ex.Message);
			}
		}

		public async Task<BaseResultDto<bool>>RegisterProgression(int id, DateTime dateTime, decimal percent)
		{
			try
			{
				_todoList.RegisterProgression(id, dateTime, percent);
				return new BaseResultDto<bool>(true);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return new BaseResultDto<bool>(ex.Message);
			}
		}
		public async Task<BaseResultDto<List<TodoItem>>> PrintItems()
		{
			try
			{
				_todoList.PrintItems();
				return new BaseResultDto<List<TodoItem>>(_todoList.TodoItems.ToList());
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return new BaseResultDto<List<TodoItem>>(ex.Message);
			}
		}
	}
}
