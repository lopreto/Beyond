﻿namespace Beyond.TodoProject.Domain.Aggregates
{
	public interface ITodoList
	{
		void AddItem(int id, string title, string description, string category);
		void UpdateItem(int id, string description);
		void RemoveItem(int id);
		void RegisterProgression(int id, DateTime dateTime, decimal percent);
		void PrintItems();
	}
}
