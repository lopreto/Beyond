using Beyond.TodoProject.Domain.Aggregates;
using FluentAssertions;
using Moq;

namespace Beyond.TodoProject.Tests.Aggregates
{
	public class TodoListTests
	{
		#region RegisterProgression
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

		#endregion

		#region AddItem

		[Fact]
		public void AddItem_IdAlreadyExists_ReturnsError()
		{
			var id = 1;
			var todoList = new TodoList();
			todoList.AddItem(id, "Title", It.IsAny<string>(), It.IsAny<string>());
			Action result = () => todoList.AddItem(id, "Title 2", It.IsAny<string>(), It.IsAny<string>());

			result.Should().Throw<ArgumentException>().WithMessage($"An item with Id {id} already exists.");
		}

		[Fact]
		public void AddItem_TitleEmpty_ReturnsError()
		{
			var id = 1;
			var title = "";
			var todoList = new TodoList();
			Action result = () => todoList.AddItem(id, title, It.IsAny<string>(), It.IsAny<string>());

			result.Should().Throw<ArgumentException>().WithMessage("The title is required.");
		}

		[Fact]
		public void AddItem_TitleNull_ReturnsError()
		{
			var id = 1;
			var todoList = new TodoList();
			Action result = () => todoList.AddItem(id, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

			result.Should().Throw<ArgumentException>().WithMessage("The title is required.");
		}

		[Fact]
		public void AddItem_Sucess_AddToList()
		{
			var id = 1;
			var todoList = new TodoList();
			todoList.AddItem(id, "Title", It.IsAny<string>(), It.IsAny<string>());

			todoList.TodoItems.Should().HaveCount(1);
		}

		#endregion

		#region UpdateItem
		
		[Fact]
		public void UpdateItem_ItemNotFounded_ReturnsError()
		{
			var id = 1;
			var todoList = new TodoList();
			
			Action result = () => todoList.UpdateItem(id, "Description 2");

			result.Should().Throw<ArgumentException>().WithMessage($"No TodoItem found with Id {id}.");
		}

		[Fact]
		public void UpdateItem_ProgressionGreater50Percent_ReturnsError()
		{
			var id = 1;
			var todoList = new TodoList();
			
			todoList.AddItem(id, "Title", It.IsAny<string>(), It.IsAny<string>());
			todoList.RegisterProgression(id, DateTime.UtcNow, 51);

			Action result = () => todoList.UpdateItem(id, "Description 2");

			result.Should().Throw<ArgumentException>().WithMessage($"The TodoItem with Id {id} cannot be changed. It is more than 50% complete.");
		}

		[Fact]
		public void UpdateItem_Success_UpdateItemDescription()
		{
			var id = 1;
			var description = "Description 2";
			var todoList = new TodoList();

			todoList.AddItem(id, "Title", "", It.IsAny<string>());
			todoList.RegisterProgression(id, DateTime.UtcNow, 50);

			todoList.UpdateItem(id, description);

			todoList?.TodoItems?.FirstOrDefault()?.Description.Should().BeEquivalentTo(description);
		}

		#endregion

		#region RemoveItem

		[Fact]
		public void RemoveItem_IdAlreadyExists_ReturnsError()
		{
			var id = 1;
			var todoList = new TodoList();

			Action result = () => todoList.RemoveItem(id);

			result.Should().Throw<ArgumentException>().WithMessage($"No TodoItem found with Id {id}.");
		}

		[Fact]
		public void RemoveItem_ProgressionGreater50Percent_ReturnsError()
		{
			var id = 1;
			var todoList = new TodoList();

			todoList.AddItem(id, "Title", It.IsAny<string>(), It.IsAny<string>());
			todoList.RegisterProgression(id, DateTime.UtcNow, 51);

			Action result = () => todoList.RemoveItem(id);

			result.Should().Throw<ArgumentException>().WithMessage($"The TodoItem with Id {id} cannot be deleted. It is more than 50% complete.");
		}

		[Fact]
		public void RemoveItem_Success_RemoveItemFromList()
		{
			var id = 1;
			var todoList = new TodoList();

			todoList.AddItem(id, "Title", It.IsAny<string>(), It.IsAny<string>());
			todoList.RegisterProgression(id, DateTime.UtcNow, 50);

			todoList.RemoveItem(id);

			todoList.TodoItems.Should().HaveCount(0);
		}

		#endregion

		#region PrintItems

		[Fact]
		public void PrintItems_WhenSuccess_OrderList()
		{
			var description = "Description 2";
			var todoList = new TodoList();

			todoList.AddItem(2, "Title 2", "", It.IsAny<string>());
			todoList.AddItem(1, "Title", "", It.IsAny<string>());

			todoList.PrintItems();

			todoList?.TodoItems?.FirstOrDefault()?.Id.Should().Be(1);
		}

		#endregion
	}
}
