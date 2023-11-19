using Coffee.Client.Models;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.Client.Controllers
{
    public class HomeController : Controller
    {
        private PostRequestUser _postRequestUser = new PostRequestUser();

        public IActionResult Index()
        {
            _postRequestUser.GetInfoCurrentAuthUser(ViewData, HttpContext.Session);

            return View();
        }
    }
}
