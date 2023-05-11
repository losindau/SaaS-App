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
    public class EqDamageLogController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EqDamageLogController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
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

            var response = await _httpClient.GetAsync("api/EqDamageLog/" + page + "/eqdamagelogs");
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
            EqDamageLogVM eqdamagelogVM = new EqDamageLogVM();

            var companyID = _httpContextAccessor.HttpContext?.User.GetUserCompanyID();

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


            eqdamagelogVM.ReportDate = DateTime.Now;
            eqdamagelogVM.LogState = Enum.LogState.Pending;
            eqdamagelogVM.ToolboxID = 3;
            eqdamagelogVM.AppUserID = _httpContextAccessor.HttpContext?.User.GetUserId();
            eqdamagelogVM.AppUserName = _httpContextAccessor.HttpContext?.User.GetUserName();
            eqdamagelogVM.CompanyID = int.Parse(companyID);
            eqdamagelogVM.isDeleted = false;

            var responsePost = await _httpClient.PostAsJsonAsync("api/EqDamageLog", eqdamagelogVM);

            if (!responsePost.IsSuccessStatusCode)
            {
                TempData["Error"] = await responsePost.Content.ReadAsStringAsync();
                return RedirectToAction("Index", new { page = 1 });
            }

            TempData["Success"] = "Create new equipment damage log successfully";
            return RedirectToAction("Index", new { page = 1 });
        }
    }
}
