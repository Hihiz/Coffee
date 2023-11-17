using Coffee.Application.Interfaces;
using Coffee.Application.Models;
using Coffee.Domain.Constants;
using Coffee.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Coffee.Infrastructure.Identity
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _db;

        public UserAuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db) =>
            (_userManager, _signInManager, _db) = (userManager, signInManager, db);

        public async Task<StatusResponse> RegisterAsync(RegistrationModel model)
        {
            StatusResponse status = new StatusResponse();
            ApplicationUser userExists = await _userManager.FindByEmailAsync(model.Email);

            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "Пользователь уже существует";

                return status;
            }

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "Не удалось создать пользователя";

                return status;
            }

            ApplicationUser findUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (findUser == null)
            {
                status.StatusCode = 0;
                status.Message = $"Пользователь {model.FirstName} {model.LastName} не найден";

                return status;
            }

            await _userManager.AddToRoleAsync(findUser, Roles.Member);

            status.StatusCode = 1;
            status.Message = $"Вы успешно зарегистрировались";

            return status;
        }

        public async Task<StatusResponse> LoginAsync(LoginModel model)
        {
            StatusResponse status = new StatusResponse();

            ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Неверное имя пользователя";

                return status;
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                status.StatusCode = 0;
                status.Message = "Неверный пароль";

                return status;
            }

            List<long> roleId = await _db.UserRoles.Where(u => u.UserId == user.Id).Select(u => u.RoleId).ToListAsync();

            List<IdentityRole<long>> roles = _db.Roles.Where(x => roleId.Contains(x.Id)).ToList();

            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);

            if (signInResult.Succeeded)
            {
                IList<string> userRoles = await _userManager.GetRolesAsync(user);

                List<Claim> authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                IdentityRole<long> adminRole = roles.Where(a => a.Name == "Admin").FirstOrDefault();

                status.StatusCode = 1;
                status.Message = "Успешно вошел в систему";
                status.UserId = user.Id;

                if (adminRole != null)
                {
                    status.Role = adminRole.Name;
                }
                else
                {
                    status.Role = roles[0].Name;
                }

                status.Username = user.UserName;
                status.Password = model.Password;
                status.FirstName = user.FirstName;
                status.LastName = user.LastName;
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Ошибка при входе в систему";
            }

            return status;
        }

        public async Task LogoutAsync() => await _signInManager.SignOutAsync();
    }
}
