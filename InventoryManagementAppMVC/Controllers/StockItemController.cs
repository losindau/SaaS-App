using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InventoryManagementAppMVC.Enum;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text.Json;
using InventoryManagementAppMVC.ViewModels;
using InventoryManagementAppMVC.Helper;

namespace InventoryManagementAppMVC.Controllers
{
    public class StockItemController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StockItemController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
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

            var response = await _httpClient.GetAsync("api/StockItem/" + page + "/stockitems");
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
        public async Task<IActionResult> Create(StockItemVM stockItemVM)
        {
            if (!ModelState.IsValid)
            {
                return View(stockItemVM);
            }

            if (stockItemVM.Quantity <= 100)
            {
                stockItemVM.QuantityState = QuantityState.Low;
            }
            else if (stockItemVM.Quantity > 100 && stockItemVM.Quantity <= 200)
            {
                stockItemVM.QuantityState = QuantityState.Medium;
            }
            else
            {
                stockItemVM.QuantityState = QuantityState.High;
            }

            var companyID = _httpContextAccessor.HttpContext?.User.GetUserCompanyID();

            stockItemVM.CompanyID = int.Parse(companyID);

            stockItemVM.isDeleted = false;

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var responsePost = await _httpClient.PostAsJsonAsync("api/StockItem", stockItemVM);

            if (!responsePost.IsSuccessStatusCode)
            {
                TempData["Error"] = "Something went wrong";
                return View(stockItemVM);
            }

            TempData["Success"] = "Create new stock item successfully";
            return RedirectToAction("Index", new {page = 1});
        }
    }
}
