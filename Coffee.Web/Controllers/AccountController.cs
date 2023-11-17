using Coffee.Application.Interfaces;
using Coffee.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserAuthenticationService _authService;

        public AccountController(IUserAuthenticationService authService) => (_authService) = (authService);

        [HttpPost("register")]
        public async Task<ActionResult<StatusResponse>> Register(RegistrationModel registrationModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("ModelState Error");
            }

            StatusResponse result = await _authService.RegisterAsync(registrationModel);

            if (result.StatusCode == 0)
            {
                return BadRequest(result);
            }

            return result;
        }

        [HttpPost("login")]
        public async Task<ActionResult<StatusResponse>> Authenticate(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("ModelState Error");
            }

            StatusResponse result = await _authService.LoginAsync(loginModel);

            if (result.StatusCode == 0)
            {
                return BadRequest(new StatusResponse { StatusCode = result.StatusCode, Message = result.Message });
            }

            return Ok(new StatusResponse
            {
                UserId = (long)result.UserId,
                Role = result.Role,
                Username = result.Username,
                Password = result.Password,
                FirstName = result.FirstName,
                LastName = result.LastName,
                StatusCode = result.StatusCode,
                Message = result.Message
            });
        }

        [HttpPost("logout")]
        public async Task Logout() => await _authService.LogoutAsync();
    }
}
