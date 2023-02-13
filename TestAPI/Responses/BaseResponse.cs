namespace TestAPI.Responses
{
    public class BaseResponse
    {
        public object? Data { get; set; }
        public string? Message { get; set; }

        public BaseResponse(object? data)
        {
            Data = data;
        }

        public BaseResponse(string? message)
        {
            Message = message;
        }

        public BaseResponse(object? data, string? message)
        {
            Data = data;
            Message = message;
        }
    }
}
