﻿namespace Beyond.TodoProject.Domain.Interfaces
{
	public interface ITodoListRepository
	{
		int GetNextId();
		List<string> GetAllCategories();
	}
}