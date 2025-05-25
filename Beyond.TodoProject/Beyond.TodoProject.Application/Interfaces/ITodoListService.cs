using Beyond.TodoProject.Domain.Aggregates;
using Beyond.TodoProject.Domain.DTOs;

namespace Beyond.TodoProject.Application.Interfaces
{
	public interface ITodoListService
	{
		Task<BaseResultDto<int>> AddItem(string title, string description, string category);
		Task<BaseResultDto<bool>> UpdateItem(int id, string description);
		Task<BaseResultDto<bool>> RemoveItem(int id);
		Task<BaseResultDto<bool>> RegisterProgression(int id, DateTime dateTime, decimal percent);
		Task<BaseResultDto<List<TodoItem>>> PrintItems();
	}
}
