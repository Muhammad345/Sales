namespace Sales.Core.Domain
{
    public class SalesApiResponse<T>
    {
        public bool IsSuccessful { get; set; } = false;
        public T? Data { get; set; }
        public string? Message { get; set; }
        public IEnumerable<string>? Errors { get; set; } = [];
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public static SalesApiResponse<T> Success(T data, string message) => new()
        {
            IsSuccessful = true,
            Data = data,
            Message = message
        };

        public static SalesApiResponse<T> Fail(string errorMessage, IEnumerable<string> errors) => new()
        {
            IsSuccessful = false,
            Message = errorMessage,
            Errors = errors ?? []
        };
    }
}
