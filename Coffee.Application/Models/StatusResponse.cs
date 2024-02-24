using Coffee.Application.Interfaces;

namespace Coffee.Application.Models
{
    public class StatusResponse : IBaseStatus
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
