using Beyond.TodoProject.Domain.Aggregates;
using FluentAssertions;
using Moq;

namespace Beyond.TodoProject.Tests
{
	public class TodoListTests
	{
		[Fact]
		public void Test1()
		{
			var a = new TodoList();
			a.AddItem(1, "test", "descr", "category");

			a.TodoItems.Should().HaveCount(1);
		}

		[Fact]
		public void Test2()
		{
			var a = new TodoList();
			Action act1 = () => a.RegisterProgression(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<decimal>());

			act1.Should().Throw<ArgumentException>().WithMessage("El porcentaje debe ser mayor que 0 y menor que 100.");
		}
	}
}