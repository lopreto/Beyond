using Beyond.TodoProject.Application.Interfaces;
using Beyond.TodoProject.Domain.Aggregates;
using Beyond.TodoProject.Domain.Interfaces;
using System;

namespace Beyond.TodoProject.Application.Services
{
	public class TodoListService : ITodoListService
	{
		public ITodoList _todoList;
		public ITodoListRepository _todoListRepository;
		public TodoListService(ITodoList todoList, ITodoListRepository todoListRepository)
		{
			_todoList = todoList;
			_todoListRepository = todoListRepository;
		}

		public void AddItem(string title, string description, string category)
		{
			try
			{
				var allCategories = _todoListRepository.GetAllCategories();

				if (allCategories == null || !allCategories.Any(x => x == category))
					throw new ArgumentException("Category not found.");
				
				_todoList.AddItem(_todoListRepository.GetNextId(), title, description, category);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void UpdateItem(int id, string description)
		{
			try
			{
				_todoList.UpdateItem(id, description);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void RemoveItem(int id)
		{
			try
			{
				_todoList.RemoveItem(id);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void RegisterProgression(int id, DateTime dateTime, decimal percent)
		{
			try
			{
				_todoList.RegisterProgression(id, dateTime, percent);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void PrintItems()
		{
			try
			{
				_todoList.PrintItems();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
