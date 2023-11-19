using Coffee.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Coffee.Client.Controllers
{
    public class EventController : Controller
    {
        private PostRequestUser _postRequestUser = new PostRequestUser();

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetEvents()
        {
            _postRequestUser.GetInfoCurrentAuthUser(ViewData, HttpContext.Session);

            List<Event> eventList = new List<Event>();

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7027/api/event"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    eventList = JsonConvert.DeserializeObject<List<Event>>(apiResponse);
                }
            }

            if (ViewBag.userRole == null)
            {
                return View(eventList.Where(n => n.IsActive).ToList());
            }

            return View(eventList);
        }

        public async Task<IActionResult> GetEventDetail(int id)
        {
            _postRequestUser.GetInfoCurrentAuthUser(ViewData, HttpContext.Session);

            Event events = new Event();

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7027/api/event/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    events = JsonConvert.DeserializeObject<Event>(apiResponse);
                }
            }

            return View(events);
        }

        public async Task<IActionResult> CreateEvent()
        {
            _postRequestUser.GetInfoCurrentAuthUser(ViewData, HttpContext.Session);

            if (ViewBag.userRole != "Admin")
            {
                TempData["errorMessage"] = ErrorMessageConst.userAdmin;

                return RedirectToAction("Error", "Product");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEvent command)
        {
            CreateEvent events = new CreateEvent();

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage responseAuth = await httpClient.PostAsJsonAsync("https://localhost:7027/api/Account/login", _postRequestUser.GetAuthResponse(HttpContext.Session)))
                {
                    using (HttpResponseMessage response = await httpClient.PostAsJsonAsync($"https://localhost:7027/api/event", command))
                    {
                        response.EnsureSuccessStatusCode();

                        string apiResponse = await response.Content.ReadAsStringAsync();

                        events = JsonConvert.DeserializeObject<CreateEvent>(apiResponse);
                    }
                }
            }

            return RedirectToAction("GetEvents", "Event");
        }

        public async Task<IActionResult> UpdateEvent(int id)
        {
            _postRequestUser.GetInfoCurrentAuthUser(ViewData, HttpContext.Session);

            if (ViewBag.userRole != "Admin")
            {
                TempData["errorMessage"] = ErrorMessageConst.userAdmin;

                return RedirectToAction("Error", "Product");
            }

            CreateEvent events = new CreateEvent();

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7027/api/event/{id}"))
                {
                    response.EnsureSuccessStatusCode();

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    events = JsonConvert.DeserializeObject<CreateEvent>(apiResponse);
                }
            }

            return View(events);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEvent(CreateEvent command)
        {
            CreateEvent events = new CreateEvent();

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage responseAuth = await httpClient.PostAsJsonAsync("https://localhost:7027/api/Account/login", _postRequestUser.GetAuthResponse(HttpContext.Session)))
                {
                    using (HttpResponseMessage response = await httpClient.PutAsJsonAsync($"https://localhost:7027/api/event/{command.Id}", command))
                    {
                        response.EnsureSuccessStatusCode();

                        string apiResponse = await response.Content.ReadAsStringAsync();

                        events = JsonConvert.DeserializeObject<CreateEvent>(apiResponse);
                    }
                }
            }

            return RedirectToAction("GetEvents", "Event");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            if (HttpContext.Session.GetString("userRole") != "Admin")
            {
                TempData["errorMessage"] = ErrorMessageConst.userAdmin;

                return RedirectToAction("Error", "Product");
            }

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage responseAuth = await httpClient.PostAsJsonAsync("https://localhost:7027/api/Account/login", _postRequestUser.GetAuthResponse(HttpContext.Session)))
                {
                    using HttpResponseMessage response = await httpClient.DeleteAsync($"https://localhost:7027/api/event/{id}");
                }
            }

            return RedirectToAction("GetEvents", "Event");
        }
    }
}
