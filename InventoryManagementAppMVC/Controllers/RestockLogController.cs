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
    public class RestockLogController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RestockLogController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
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

            var response = await _httpClient.GetAsync("api/RestockLog/" + page + "/restocklogs");
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
            RestockLogVM restocklogVM = new RestockLogVM();

            var companyID = _httpContextAccessor.HttpContext?.User.GetUserCompanyID();

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


            restocklogVM.RequestDate = DateTime.Now;
            restocklogVM.LogState = Enum.LogState.Pending;
            restocklogVM.TruckID = 3;
            restocklogVM.LicensePlate = "ABD-123";
            restocklogVM.AppUserID = _httpContextAccessor.HttpContext?.User.GetUserId();
            restocklogVM.AppUserName = _httpContextAccessor.HttpContext?.User.GetUserName();
            restocklogVM.CompanyID = int.Parse(companyID);
            restocklogVM.isDeleted = false;

            var responsePost = await _httpClient.PostAsJsonAsync("api/RestockLog", restocklogVM);

            if (!responsePost.IsSuccessStatusCode)
            {
                TempData["Error"] = await responsePost.Content.ReadAsStringAsync();
                return RedirectToAction("Index", new { page = 1 });
            }

            TempData["Success"] = "Create new restock log successfully";
            return RedirectToAction("Index", new { page = 1 });
        }
    }
}
