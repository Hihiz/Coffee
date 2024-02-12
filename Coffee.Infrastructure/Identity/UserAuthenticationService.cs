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

        public async Task<IBaseStatus> RegisterAsync(RegistrationModel model)
        {
            ApplicationUser userExists = await _userManager.FindByEmailAsync(model.Email);

            if (userExists != null)
            {
                return new StatusResponseError()
                {
                    StatusCode = 401,
                    Message = "Пользователь уже существует"
                };
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
                return new StatusResponseError()
                {
                    StatusCode = 401,
                    Message = "Не удалось создать пользователя"
                };
            }

            ApplicationUser findUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (findUser == null)
            {
                return new StatusResponseError()
                {
                    StatusCode = 401,
                    Message = $"Пользователь {model.FirstName} {model.LastName} не найден"
                };
            }

            await _userManager.AddToRoleAsync(findUser, Roles.Member);

            return new StatusResponseError()
            {
                StatusCode = 200,
                Message = "Вы успешно зарегистрировались"
            };
        }

        public async Task<IBaseStatus> LoginAsync(LoginModel model)
        {
            StatusResponse status = new StatusResponse();

            ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new StatusResponseError()
                {
                    StatusCode = 401,
                    Message = "Неверное имя пользователя"
                };
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new StatusResponseError()
                {
                    StatusCode = 401,
                    Message = "Неверный пароль"
                };
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
                status.StatusCode = 200;
                status.Message = "Успешно";
            }
            else
            {
                return new StatusResponseError()
                {
                    StatusCode = 401,
                    Message = "Ошибка при входе в систему"
                };
            }

            return status;
        }

        public async Task LogoutAsync() => await _signInManager.SignOutAsync();
    }
}
