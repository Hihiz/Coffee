using Coffee.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Coffee.Client.Controllers
{
    public class AccountController : Controller
    {
        public async Task<IActionResult> Register()
        {
            if (HttpContext.Session.GetString("userLogin") != null)
            {
                TempData["errorMessage"] = ErrorMessageConst.userAuth;

                return RedirectToAction("Error", "Product");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            StatusResponse statusResponse = new StatusResponse();

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.PostAsJsonAsync("https://localhost:7027/api/Account/register", model))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    statusResponse = JsonConvert.DeserializeObject<StatusResponse>(apiResponse);

                    return await Login(new LoginModel
                    {
                        Email = model.Email,
                        Password = model.Password
                    });
                }
            }
        }

        public async Task<IActionResult> Login()
        {
            if (HttpContext.Session.GetString("userLogin") != null)
            {
                TempData["errorMessage"] = ErrorMessageConst.userAuth;

                return RedirectToAction("Error", "Product");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            StatusResponse statusResponse = new StatusResponse();

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.PostAsJsonAsync("https://localhost:7027/api/Account/login", model))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    statusResponse = JsonConvert.DeserializeObject<StatusResponse>(apiResponse);

                    if (statusResponse.StatusCode == 0)
                    {
                        TempData["statusMessage"] = statusResponse.Message;

                        return View();
                    }

                    HttpContext.Session.SetString("userLogin", statusResponse.Username);
                    HttpContext.Session.SetString("userPassword", statusResponse.Password);

                    HttpContext.Session.SetString("userRole", statusResponse.Role);

                    HttpContext.Session.SetString("userLastName", statusResponse.LastName);
                    HttpContext.Session.SetString("userFirstName", statusResponse.FirstName);

                    return RedirectToAction("GetProducts", "Product");
                }
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
