using Coffee.Application.Models;

namespace Coffee.Application.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<StatusResponse> LoginAsync(LoginModel model);
        Task LogoutAsync();
        Task<StatusResponse> RegisterAsync(RegistrationModel model);
    }
}
