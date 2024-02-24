namespace Coffee.Application.Interfaces
{
    public interface IBaseStatus
    {
        int StatusCode { get; set; }
        string Message { get; set; }
    }
}
