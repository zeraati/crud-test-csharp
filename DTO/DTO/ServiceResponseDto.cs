using System.Net;

namespace DTO
{
    public class ServiceResponseDto
    {
        public int StatusCode { get; set; } = (int)HttpStatusCode.OK;
        public string? Message { get; set; }
        public object Errors { get; set; }
        public object Data { get; set; }
    }
}
