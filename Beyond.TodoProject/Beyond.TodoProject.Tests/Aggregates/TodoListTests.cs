using Beyond.TodoProject.Domain.Aggregates;
using FluentAssertions;
using Moq;

namespace Beyond.TodoProject.Tests.Aggregates
{
	public class TodoListTests
	{
		[Fact]
		public void RegisterProgression_PercentEquals0_ReturnsError()
		{
			var todoList = new TodoList();
			Action result = () => todoList.RegisterProgression(It.IsAny<int>(), It.IsAny<DateTime>(), 0);

			result.Should().Throw<ArgumentException>().WithMessage("The percentage must be greater than 0 and less than 100.");
		}

		[Fact]
		public void RegisterProgression_PercentGreater100_ReturnsError()
		{
			var todoList = new TodoList();
			Action result = () => todoList.RegisterProgression(It.IsAny<int>(), It.IsAny<DateTime>(), 101);

			result.Should().Throw<ArgumentException>().WithMessage("The percentage must be greater than 0 and less than 100.");
		}

		[Fact]
		public void RegisterProgression_DateIsLessThanLastAdded_AddToProgressionList()
		{
			var todoList = new TodoList();

			todoList.AddItem(1, "Complete Project Report", "Finish the final report for the project", "Work");
			todoList.RegisterProgression(1, DateTime.Now, 50);

			Action result = () => todoList.RegisterProgression(1, DateTime.Now.AddMinutes(-1), 50);

			result.Should().Throw<ArgumentException>().WithMessage("The date of the new progression must be greater than the dates already recorded.");
		}

		[Fact]
		public void RegisterProgression_PercentIsGreaterThan100_AddToProgressionList()
		{
			var todoList = new TodoList();

			todoList.AddItem(1, "Complete Project Report", "Finish the final report for the project", "Work");
			todoList.RegisterProgression(1, DateTime.Now, 50);

			Action result = () => todoList.RegisterProgression(1, DateTime.Now, 51);

			result.Should().Throw<ArgumentException>().WithMessage("The total sum of progress cannot exceed 100%.");
		}

		[Fact]
		public void RegisterProgression_AddProgressionPercentLessThan100_AddToProgressionList()
		{
			var todoList = new TodoList();

			todoList.AddItem(1, "Complete Project Report", "Finish the final report for the project", "Work");

			todoList.RegisterProgression(1, It.IsAny<DateTime>(), 50);

			todoList.TodoItems.Any().Should().BeTrue();
			todoList.TodoItems.FirstOrDefault().Should().NotBeNull();
			todoList?.TodoItems?.FirstOrDefault()?.Progressions.Should().NotBeNull();
			todoList?.TodoItems?.FirstOrDefault()?.Progressions.Should().HaveCountGreaterThan(0);
			todoList?.TodoItems?.FirstOrDefault()?.IsCompleted.Should().BeFalse();
		}

		[Fact]
		public void RegisterProgression_AddProgressionPercentEquals100_AddToProgressionList()
		{
			var todoList = new TodoList();

			todoList.AddItem(1, "Complete Project Report", "Finish the final report for the project", "Work");

			todoList.RegisterProgression(1, DateTime.Now, 50);
			todoList.RegisterProgression(1, DateTime.Now, 50);

			todoList.TodoItems.Any().Should().BeTrue();
			todoList.TodoItems.FirstOrDefault().Should().NotBeNull();
			todoList?.TodoItems?.FirstOrDefault()?.Progressions.Should().NotBeNull();
			todoList?.TodoItems?.FirstOrDefault()?.Progressions.Should().HaveCountGreaterThan(0);
			todoList?.TodoItems?.FirstOrDefault()?.IsCompleted.Should().BeTrue();
		}
	}
}
