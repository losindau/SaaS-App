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
    public class UsageLogController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsageLogController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
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

            var response = await _httpClient.GetAsync("api/UsageLog/" + page + "/usagelogs");
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

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            UsageLogVM usagelogVM = new UsageLogVM();

            var companyID = _httpContextAccessor.HttpContext?.User.GetUserCompanyID();

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


            usagelogVM.Date = DateTime.Now;
            usagelogVM.TruckID = 3;
            usagelogVM.LicensePlate = "ABD-123";
            usagelogVM.AppUserID = _httpContextAccessor.HttpContext?.User.GetUserId();
            usagelogVM.AppUserName = _httpContextAccessor.HttpContext?.User.GetUserName();
            usagelogVM.CompanyID = int.Parse(companyID);
            usagelogVM.isDeleted = false;

            var responsePost = await _httpClient.PostAsJsonAsync("api/UsageLog", usagelogVM);

            if (!responsePost.IsSuccessStatusCode)
            {
                TempData["Error"] = await responsePost.Content.ReadAsStringAsync();
                return RedirectToAction("Index", new { page = 1 });
            }

            TempData["Success"] = "Create new usage log successfully";
            return RedirectToAction("Index", new { page = 1 });
        }
    }
}
