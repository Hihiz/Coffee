using Coffee.Application.Interfaces;
using Coffee.Infrastructure.Models;

namespace Coffee.Infrastructure.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<IBaseStatus> LoginAsync(LoginModel model);
        Task LogoutAsync();
        Task<IBaseStatus> RegisterAsync(RegistrationModel model);
    }
}
