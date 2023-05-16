using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InventoryManagementAppMVC.Enum;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text.Json;
using InventoryManagementAppMVC.ViewModels;
using InventoryManagementAppMVC.Helper;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Twilio.Base;

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

            var companyID = _httpContextAccessor.HttpContext?.User.GetUserCompanyID();
            stockItemVM.CompanyID = int.Parse(companyID);
            stockItemVM.isDeleted = false;

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var responsePostStockItemVM = await _httpClient.PostAsJsonAsync("api/StockItem", stockItemVM);
            var postStockItemVMContent = await responsePostStockItemVM.Content.ReadAsStringAsync();
            if (!responsePostStockItemVM.IsSuccessStatusCode)
            {
                TempData["Error"] = postStockItemVMContent;
                return View(stockItemVM);
            }

            List<TruckVM> truckVMs = new List<TruckVM>();

            var responseGetTruckVM = await _httpClient.GetAsync("api/Truck");
            if (responseGetTruckVM.IsSuccessStatusCode)
            {
                var apiResponse = await responseGetTruckVM.Content.ReadAsStreamAsync();
                truckVMs = await JsonSerializer.DeserializeAsync<List<TruckVM>>(apiResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() },
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
            }

            List<TruckStockItemVM> truckStockItemVMs = new List<TruckStockItemVM>();

            foreach (var item in truckVMs)
            {
                TruckStockItemVM truckStockItemVM = new TruckStockItemVM()
                {
                    TruckID = item.TruckID,
                    StockItemID = int.Parse(postStockItemVMContent),
                    QuantityInTruck = 0,
                    CompanyID = int.Parse(companyID),
                    isDeleted = false
                };

                truckStockItemVMs.Add(truckStockItemVM);
            }

            var responsePostTruckStockItem = await _httpClient.PostAsJsonAsync("api/TruckStockItem", truckStockItemVMs);
            var postTruckStockItemContent = await responsePostTruckStockItem.Content.ReadAsStringAsync();

            if (!responsePostTruckStockItem.IsSuccessStatusCode)
            {
                TempData["Error"] = postTruckStockItemContent;
                return RedirectToAction("Index", new { page = 1 });
            }

            TempData["Success"] = "Create new stock item successfully";
            return RedirectToAction("Index", new { page = 1 });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            StockItemVM responseStockItem = new StockItemVM();

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync("api/StockItem/" + id);
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                responseStockItem = await JsonSerializer.DeserializeAsync<StockItemVM>(apiResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
            }

            return View(responseStockItem);
        }

        [HttpPost]
        public async Task<IActionResult> Update(StockItemVM stockItemVM)
        {
            if (!ModelState.IsValid)
            {
                return View(stockItemVM);
            }

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var responsePut = await _httpClient.PutAsJsonAsync("api/StockItem/" + stockItemVM.StockItemID, stockItemVM);
            if (!responsePut.IsSuccessStatusCode)
            {
                TempData["Error"] = await responsePut.Content.ReadAsStringAsync();
                return View(stockItemVM);
            }

            TempData["Success"] = "Update stock item successfully";
            return RedirectToAction("Index", new { page = 1 });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            StockItemVM responseStockItem = new StockItemVM();

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync("api/StockItem/" + id);
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                responseStockItem = await JsonSerializer.DeserializeAsync<StockItemVM>(apiResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
            }

            responseStockItem.isDeleted = true;

            var responsePut = await _httpClient.PutAsJsonAsync("api/StockItem/" + responseStockItem.StockItemID, responseStockItem);
            if (!responsePut.IsSuccessStatusCode)
            {
                TempData["Error"] = await responsePut.Content.ReadAsStringAsync();
                return RedirectToAction("Index", new { page = 1 });
            }

            TempData["Success"] = "Delete stock item successfully";
            return RedirectToAction("Index", new { page = 1 });
        }
    }
}
