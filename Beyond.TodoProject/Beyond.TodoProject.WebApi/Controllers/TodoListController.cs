using Beyond.TodoProject.Application.Interfaces;
using Beyond.TodoProject.Domain.Aggregates;
using Beyond.TodoProject.Domain.DTOs;
using Beyond.TodoProject.WebApi.ApiModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

		/// <summary>
		/// Method used to return the list of items
		/// </summary>
		/// <returns>Returns the items from the list.</returns>
		[HttpGet("PrintItems")]
		[ProducesResponseType(typeof(BaseResultDto<List<TodoItem>>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(BaseResultDto<List<TodoItem>>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> PrintItems()
		{
			var result = await _todoListService.PrintItems();

			if(!string.IsNullOrWhiteSpace(result.ErrorMessage))
				return StatusCode((int)HttpStatusCode.InternalServerError, result);

			return Ok(result);
		}

		/// <summary>
		/// Method used to add list items
		/// </summary>
		/// <returns>Returns the item id created.</returns>
		[HttpPost]
		[ProducesResponseType(typeof(BaseResultDto<int>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(BaseResultDto<int>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> AddItem([FromBody] AddItemRequest request)
		{
			var result = await _todoListService.AddItem(request.Title, request.Description, request.Category);

			if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
				return StatusCode((int)HttpStatusCode.InternalServerError, result);

			return Ok(result);
		}

		/// <summary>
		/// Method used to update list items
		/// </summary>
		/// <returns>Returns a boolean.</returns>
		[HttpPut("{id}")]
		[ProducesResponseType(typeof(BaseResultDto<int>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(BaseResultDto<int>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> UpdateItem(int id, [FromBody] UpdateItemRequest UpdateItemRequest)
		{
			var result = await _todoListService.UpdateItem(id, UpdateItemRequest.Description);

			if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
				return StatusCode((int)HttpStatusCode.InternalServerError, result);

			return Ok(result);
		}

		/// <summary>
		/// Method used to delete list items
		/// </summary>
		/// <returns>Returns a boolean.</returns>
		[HttpDelete("{id}")]
		[ProducesResponseType(typeof(BaseResultDto<int>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(BaseResultDto<int>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> RemoveItem(int id)
		{
			var result = await _todoListService.RemoveItem(id);

			if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
				return StatusCode((int)HttpStatusCode.InternalServerError, result);

			return Ok(result);
		}

		/// <summary>
		/// Method used to register a progression in an item
		/// </summary>
		/// <returns>Returns a boolean.</returns>
		[HttpPost("{id}/RegisterProgression")]
		[ProducesResponseType(typeof(BaseResultDto<int>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(BaseResultDto<int>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> RegisterProgression(int itemId, RegisterProgressionRequest request)
		{
			var result = await _todoListService.RegisterProgression(itemId, request.Date, request.Percent);

			if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
				return StatusCode((int)HttpStatusCode.InternalServerError, result);

			return Ok(result);
		}
	}
}
