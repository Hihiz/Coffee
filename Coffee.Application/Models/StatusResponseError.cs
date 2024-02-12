using Coffee.Application.Interfaces;

namespace Coffee.Application.Models
{
    public class StatusResponseError : IBaseStatus
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
