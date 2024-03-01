namespace Rideshare.Application.Common.Response;

public class BaseResponse<T>
{
        public bool IsSuccess { get; set; }

        public object Error { get; set; }

        public T Value { get; set; }

        public string Message { get; set; }

        public static BaseResponse<T> Success(T value) =>
            new() { Value = value, IsSuccess = true };

        public static BaseResponse<T> Failure(string message) =>
            new() { Message = message, IsSuccess = false };

        public static BaseResponse<T> FailureWithError(string message, object error) =>
            new()
            {
                Message = message,
                Error = error,
                IsSuccess = false
            };
}
