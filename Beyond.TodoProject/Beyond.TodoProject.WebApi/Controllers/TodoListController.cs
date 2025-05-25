using Beyond.TodoProject.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Beyond.TodoProject.WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TodoListController : ControllerBase
	{
		private readonly ITodoListService _todoListService;
		private readonly ILogger<TodoListController> _logger;

		public TodoListController(ITodoListService todoListService, ILogger<TodoListController> logger)
		{
			_todoListService = todoListService;
			_logger = logger;
		}

		[HttpGet("PrintItems")]
		public async Task<IEnumerable<WeatherForecast>> PrintItems()
		{
			var result = await _todoListService.PrintItems();

			return null;
		}
	}
}
