using Coffee.Application.Interfaces;
using Coffee.Application.Models;
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
        public async Task<ActionResult> Register(RegistrationModel registrationModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("ModelState Error");
            }

            return Ok(await _authService.RegisterAsync(registrationModel));
        }

        [HttpPost("login")]
        public async Task<ActionResult<StatusResponse>> Authenticate(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("ModelState Error");
            }

            return Ok(await _authService.LoginAsync(loginModel));
        }

        [HttpPost("logout")]
        public async Task Logout() => await _authService.LogoutAsync();
    }
}
