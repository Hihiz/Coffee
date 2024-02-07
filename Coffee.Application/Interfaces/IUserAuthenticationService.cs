using Coffee.Application.Models;

namespace Coffee.Application.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<IBaseStatus> LoginAsync(LoginModel model);
        Task LogoutAsync();
        Task<IBaseStatus> RegisterAsync(RegistrationModel model);
    }
}
