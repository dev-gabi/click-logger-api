namespace Entities.Response
{
    public class ApiResponse
    {

        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string StatusCodeTitle { get; set; }
#nullable enable
        public string? Error { get; set; }
#nullable disable
    }
}
