using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text.Json;
using InventoryManagementAppMVC.ViewModels;
using InventoryManagementAppMVC.Helper;

namespace InventoryManagementAppMVC.Controllers
{
    public class TruckController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TruckController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            this._httpClient = httpClientFactory.CreateClient("myclient");
            this._httpContextAccessor = httpContextAccessor;
        }

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Index(int page)
        {
            ResponsePagination responsePage = new ResponsePagination();

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync("api/Truck/" + page + "/trucks");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                responsePage = await JsonSerializer.DeserializeAsync<ResponsePagination>(apiResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() },
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
            }

            return View(responsePage);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TruckVM truckVM)
        {
            if (!ModelState.IsValid)
            {
                return View(truckVM);
            }

            var companyID = _httpContextAccessor.HttpContext?.User.GetUserCompanyID();

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            ToolboxVM toolboxVM = new ToolboxVM()
            {
                CompanyID = int.Parse(companyID),
                isDeleted = false,
            };

            var responsePostToolbox = await _httpClient.PostAsJsonAsync("api/Toolbox", toolboxVM);

            if (!responsePostToolbox.IsSuccessStatusCode)
            {
                TempData["Error"] = "Create toolbox failed";
                return View();
            }

            var toolboxID = await responsePostToolbox.Content.ReadAsStringAsync();

            truckVM.ToolboxID = int.Parse(toolboxID);
            truckVM.CompanyID = int.Parse(companyID);
            truckVM.isDeleted = false;

            var responsePostTruck = await _httpClient.PostAsJsonAsync("api/Truck", truckVM);

            if (!responsePostTruck.IsSuccessStatusCode)
            {
                TempData["Error"] = await responsePostTruck.Content.ReadAsStringAsync();
                return View(truckVM);
            }

            TempData["Success"] = "Create new truck successfully";
            return RedirectToAction("Index", new { page = 1 });
        }
    }
}
