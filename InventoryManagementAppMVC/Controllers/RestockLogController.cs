using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text.Json;
using InventoryManagementAppMVC.ViewModels;
using InventoryManagementAppMVC.Helper;
using InventoryManagementAppMVC.Enum;

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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TruckVM truckVM = new TruckVM();
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var truckID = _httpContextAccessor.HttpContext?.User.GetUserTruckID();
            var response = await _httpClient.GetAsync("api/Truck/" + truckID);
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                truckVM = await JsonSerializer.DeserializeAsync<TruckVM>(apiResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() },
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
            }

            CreateRestockLogVM createRestockLogVM = new CreateRestockLogVM()
            {
                TruckID = truckVM.TruckID,
                LicensePlate = truckVM.LicensePlate,
                TruckStockItems = truckVM.TruckStockItems.ToList()
            };

            foreach (var item in truckVM.TruckStockItems)
            {
                createRestockLogVM.StockItemNames.Add((int)item.StockItemID, item.StockItem.Name);
                createRestockLogVM.StockItemQuantities.Add((int)item.StockItemID, 0);
            }

            return View(createRestockLogVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRestockLogVM createRestockLogVM)
        {
            int count = 0;
            foreach (var item in createRestockLogVM.StockItemQuantities)
            {
                if (item.Value > 0)
                {
                    count++;
                }
            }

            if (count < 1)
            {
                TempData["Error"] = "To create restocklog you must change quantity of at least 1 item";
                return RedirectToAction("MyRestockLog", new { page = 1 });
            }

            //Create master restocklog
            var userID = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userName = _httpContextAccessor.HttpContext?.User.GetUserName();
            var companyID = _httpContextAccessor.HttpContext?.User.GetUserCompanyID();

            RestockLogVM restocklogVM = new RestockLogVM()
            {
                RequestDate = DateTime.Now,
                LogState = LogState.Pending,
                RestockState = RestockState.NeedAccept,
                TruckID = createRestockLogVM.TruckID,
                LicensePlate = createRestockLogVM.LicensePlate,
                AppUserID = userID,
                AppUserName = userName,
                CompanyID = int.Parse(companyID),
                isDeleted = false
            };

            //Post master restocklog then receive master restocklog id
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var responsePostRestockLog = await _httpClient.PostAsJsonAsync("api/RestockLog", restocklogVM);
            var postRestockLogContent = await responsePostRestockLog.Content.ReadAsStringAsync();

            if (!responsePostRestockLog.IsSuccessStatusCode)
            {
                TempData["Error"] = postRestockLogContent;
                return RedirectToAction("MyRestockLog", new { page = 1 });
            }

            //Create detail usagelogs
            List<DetailRestockLogVM> detailRestockLogVMs = new List<DetailRestockLogVM>();

            foreach (var item in createRestockLogVM.StockItemQuantities)
            {
                int stockItemID = item.Key;
                int quantity = item.Value;

                if (quantity > 0)
                {
                    DetailRestockLogVM newDetailRestockLog = new DetailRestockLogVM()
                    {
                        StockItemID = stockItemID,
                        StockItemName = createRestockLogVM.StockItemNames[stockItemID],
                        Quantity = quantity,
                        RestockLogID = int.Parse(postRestockLogContent),
                        CompanyID = int.Parse(companyID),
                        isDeleted = false
                    };

                    detailRestockLogVMs.Add(newDetailRestockLog);
                }
            }

            //Post list of detail restocklogs
            var responsePostDetailRestockLog = await _httpClient.PostAsJsonAsync("api/DetailRestockLog", detailRestockLogVMs);
            var postDetailRestockLogContent = await responsePostDetailRestockLog.Content.ReadAsStringAsync();

            if (!responsePostDetailRestockLog.IsSuccessStatusCode)
            {
                TempData["Error"] = postDetailRestockLogContent;
                return RedirectToAction("MyRestockLog", new { page = 1 });
            }

            TempData["Success"] = "Create new restock log successfully";
            return RedirectToAction("MyRestockLog", new { page = 1 });
        }

        [HttpGet]
        public async Task<IActionResult> MyRestockLog(int page)
        {
            ResponsePagination responsePage = new ResponsePagination();

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var userID = _httpContextAccessor.HttpContext?.User.GetUserId();
            var response = await _httpClient.GetAsync("api/RestockLog/" + page + "/myrestocklogs/" + userID);
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
    }
}
