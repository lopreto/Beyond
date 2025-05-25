namespace Beyond.TodoProject.Domain.DTOs
{
	public class BaseResultDto<T>
	{
		public string? ErrorMessage { get; private set; }
		public T? Result { get; private set; }

		public BaseResultDto(T result)
		{
			Result = result;
		}

		public BaseResultDto(string errorMessage)
		{
			ErrorMessage = errorMessage;
		}
	}
}
