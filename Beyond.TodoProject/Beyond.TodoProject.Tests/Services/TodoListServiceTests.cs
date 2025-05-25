using Beyond.TodoProject.Application.Services;
using Beyond.TodoProject.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Beyond.TodoProject.Tests.Services
{
	public class TodoListServiceTests
	{
		private readonly Mock<ITodoListRepository> _todoListRepositoryMock;
		private readonly Mock<ILogger<TodoListService>> _loggerMock;
		private readonly TodoListService _service;

		public TodoListServiceTests()
		{
			_todoListRepositoryMock = new Mock<ITodoListRepository>();
			_loggerMock = new Mock<ILogger<TodoListService>>();
			_service = new TodoListService(_todoListRepositoryMock.Object, _loggerMock.Object);
		}

		[Fact]
		public async Task PrintItems_Should_Return_TodoItems_When_Successful()
		{
			_todoListRepositoryMock.Setup(x => x.GetAllCategories()).Returns(new List<string>() { "Work" });

			_todoListRepositoryMock.Setup(x => x.GetNextId()).Returns(1);
			await _service.AddItem("Tarea 1", "Descripción 1", "Work");

			_todoListRepositoryMock.Setup(x => x.GetNextId()).Returns(2);
			await _service.AddItem("Tarea 2", "Descripción 2", "Work");


			var result = await _service.PrintItems();

			result.Result.Should().HaveCountGreaterThan(1);
			result?.Result?.FirstOrDefault(x => x.Id == 1)?.Title.Should().BeEquivalentTo("Tarea 1");
		}
	}
}