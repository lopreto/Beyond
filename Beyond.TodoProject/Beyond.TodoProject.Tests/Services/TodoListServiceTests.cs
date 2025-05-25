using Beyond.TodoProject.Application.Services;
using Beyond.TodoProject.Domain.Aggregates;
using Beyond.TodoProject.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Beyond.TodoProject.Tests.Services
{
	public class TodoListServiceTests
	{
		private readonly Mock<ITodoList> _todoListMock;
		private readonly Mock<ITodoListRepository> _todoListRepositoryMock;
		private readonly Mock<ILogger<TodoListService>> _loggerMock;
		private readonly TodoListService _service;

		public TodoListServiceTests()
		{

			_todoListMock = new Mock<ITodoList>();
			_todoListRepositoryMock = new Mock<ITodoListRepository>();
			_loggerMock = new Mock<ILogger<TodoListService>>();
			_service = new TodoListService(_todoListMock.Object, _todoListRepositoryMock.Object, _loggerMock.Object);
		}

		[Fact]
		public async Task AddItem_ShouldReturnError_WhenCategoryNotExists()
		{
			_todoListRepositoryMock.Setup(x => x.GetAllCategories()).Returns(new List<string>() { "Work" });

			_todoListRepositoryMock.Setup(x => x.GetNextId()).Returns(1);
			var result = await _service.AddItem("Tarea 1", "Descripción 1", "Works");
			
			result.ErrorMessage.Should().BeEquivalentTo("Category not found.");
		}

		[Fact]
		public async Task AddItem_ShouldReturnTrue_WhenAddItem()
		{
			_todoListRepositoryMock.Setup(x => x.GetAllCategories()).Returns(new List<string>() { "Work" });

			_todoListRepositoryMock.Setup(x => x.GetNextId()).Returns(1);
			var result = await _service.AddItem("Tarea 1", "Descripción 1", "Work");

			result.ErrorMessage.Should().BeNull();
			result.Data.Should().BeGreaterThan(0);
		}

		[Fact]
		public async Task PrintItems_ShouldReturnTodoItems_WhenSuccessful()
		{
			_todoListRepositoryMock.Setup(x => x.GetAllCategories()).Returns(new List<string>() { "Work" });

			_todoListRepositoryMock.Setup(x => x.GetNextId()).Returns(1);
			await _service.AddItem("Tarea 1", "Descripción 1", "Work");

			_todoListRepositoryMock.Setup(x => x.GetNextId()).Returns(2);
			await _service.AddItem("Tarea 2", "Descripción 2", "Work");

			var result = await _service.PrintItems();

			result.Data.Should().HaveCountGreaterThan(1);
			result?.Data?.FirstOrDefault(x => x.Id == 1)?.Title.Should().BeEquivalentTo("Tarea 1");
		}
	}
}