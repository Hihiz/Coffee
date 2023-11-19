using Coffee.Client.Dto;
using Coffee.Client.Interfaces;
using Coffee.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Coffee.Client.Controllers
{
    public class ProductController : Controller
    {
        private readonly IFileService _fileService;
        private PostRequestUser _postRequestUser = new PostRequestUser();

        public ProductController(IFileService fileService) => _fileService = fileService;

        public async Task<IActionResult> GetProducts()
        {
            _postRequestUser.GetInfoCurrentAuthUser(ViewData, HttpContext.Session);

            List<ProductListDto> productList = new List<ProductListDto>();

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7027/api/product"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    productList = JsonConvert.DeserializeObject<List<ProductListDto>>(apiResponse);
                }
            }

            return View(productList);
        }

        public async Task<IActionResult> GetProductDetail(int id)
        {
            _postRequestUser.GetInfoCurrentAuthUser(ViewData, HttpContext.Session);

            ProductDetailDto productDetail = new ProductDetailDto();

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7027/api/product/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    productDetail = JsonConvert.DeserializeObject<ProductDetailDto>(apiResponse);
                }
            }

            return View(productDetail);
        }

        public async Task<IActionResult> CreateProduct()
        {
            _postRequestUser.GetInfoCurrentAuthUser(ViewData, HttpContext.Session);

            if (ViewBag.userRole != "Admin")
            {
                TempData["errorMessage"] = ErrorMessageConst.userAdmin;

                return View("Error");
            }

            using (HttpClient httpClient = new HttpClient())
            {
                List<Category> categoryList = await httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7027/api/category");
                if (categoryList != null)
                {
                    ViewData["CityList"] = new SelectList(categoryList, "Id", "Name");
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProduct command)
        {
            CreateProduct product = new CreateProduct();

            if (command.ImageFile != null)
            {
                Tuple<int, string> fileResult = _fileService.SaveImage(command.ImageFile);

                if (fileResult.Item1 == 0)
                {
                    Console.WriteLine("Файл не удалось сохранить");

                    return View();
                }

                string imageName = fileResult.Item2;

                command.Image = imageName;
            }

            if (command.ImageFile == null)
            {
                command.Image = "picture.jpg";
            }

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage responseAuth = await httpClient.PostAsJsonAsync("https://localhost:7027/api/Account/login", _postRequestUser.GetAuthResponse(HttpContext.Session)))
                {
                    using (HttpResponseMessage response = await httpClient.PostAsJsonAsync("https://localhost:7027/api/product", command))
                    {
                        response.EnsureSuccessStatusCode();

                        string apiResponse = await response.Content.ReadAsStringAsync();

                        product = JsonConvert.DeserializeObject<CreateProduct>(apiResponse);

                        List<Category> categoryList = await httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7027/api/category");
                        if (categoryList != null)
                        {
                            ViewData["CityList"] = new SelectList(categoryList, "Id", "Name"/*, product.Id*/);
                        }
                    }
                }
            }

            return RedirectToAction("GetProducts", "Product");
        }

        public async Task<IActionResult> UpdateProduct(int id)
        {
            _postRequestUser.GetInfoCurrentAuthUser(ViewData, HttpContext.Session);

            if (ViewBag.userRole != "Admin")
            {
                TempData["errorMessage"] = ErrorMessageConst.userAdmin;

                return View("Error");
            }

            CreateProduct product = new CreateProduct();

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7027/api/product/{id}"))
                {
                    response.EnsureSuccessStatusCode();

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    product = JsonConvert.DeserializeObject<CreateProduct>(apiResponse);

                    List<Category> categoryList = await httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7027/api/category");
                    if (categoryList != null)
                    {
                        ViewData["CityList"] = new SelectList(categoryList, "Id", "Name");
                    }
                }
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(CreateProduct command)
        {
            CreateProduct product = new CreateProduct();

            if (command.ImageFile != null)
            {
                Tuple<int, string> fileResult = _fileService.SaveImage(command.ImageFile);

                if (fileResult.Item1 == 0)
                {
                    Console.WriteLine("Файл не удалось сохранить");

                    return View();
                }

                string imageName = fileResult.Item2;

                command.Image = imageName;
            }
            if (command.ImageFile == null)
            {
                command.Image = "picture.jpg";
            }

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage responseAuth = await httpClient.PostAsJsonAsync("https://localhost:7027/api/Account/login", _postRequestUser.GetAuthResponse(HttpContext.Session)))
                {
                    using (HttpResponseMessage response = await httpClient.PutAsJsonAsync($"https://localhost:7027/api/product/{command.Id}", command))
                    {
                        response.EnsureSuccessStatusCode();

                        string apiResponse = await response.Content.ReadAsStringAsync();

                        product = JsonConvert.DeserializeObject<CreateProduct>(apiResponse);

                        List<Category> categoryList = await httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7027/api/category");
                        if (categoryList != null)
                        {
                            ViewData["CityList"] = new SelectList(categoryList, "Id", "Name", product.CategoryId);
                        }
                    }
                }
            }

            return RedirectToAction("GetProducts", "Product");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (HttpContext.Session.GetString("userRole") != "Admin")
            {
                TempData["errorMessage"] = ErrorMessageConst.userAdmin;

                return View("Error");
            }
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage responseAuth = await httpClient.PostAsJsonAsync("https://localhost:7027/api/Account/login", _postRequestUser.GetAuthResponse(HttpContext.Session)))
                {
                    using HttpResponseMessage response = await httpClient.DeleteAsync($"https://localhost:7027/api/product/{id}");
                }
            }

            return RedirectToAction("GetProducts", "Product");
        }

        public async Task<IActionResult> Error()
        {
            _postRequestUser.GetInfoCurrentAuthUser(ViewData, HttpContext.Session);

            return View();
        }
    }
}
