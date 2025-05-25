namespace Beyond.TodoProject.Domain.DTOs
{
	public class BaseResultDto<T>
	{
		public string? ErrorMessage { get; private set; }
		public T? Data { get; private set; }

		public BaseResultDto(T data)
		{
			Data = data;
		}

		public BaseResultDto(string errorMessage)
		{
			ErrorMessage = errorMessage;
			Data = default;
		}
	}
}
