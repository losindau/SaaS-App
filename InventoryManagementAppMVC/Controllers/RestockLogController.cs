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
using Microsoft.AspNetCore.Mvc.RazorPages;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

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

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> AcceptRestockLog(int restockLogID)
        {
            RestockLogVM restockLogVM = new RestockLogVM();

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync("api/RestockLog/" + restockLogID);
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                restockLogVM = await JsonSerializer.DeserializeAsync<RestockLogVM>(apiResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() },
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
            }

            string phoneNumber = restockLogVM.AppUser.PhoneNumber;
            phoneNumber.Remove(0, 1).Insert(0, "+84");

            if (restockLogVM.LogState == LogState.Declined)
            {
                TempData["Error"] = "Restock log id #" + restockLogID + " has been rejected before";
                return RedirectToAction("Index", new { page = 1 });
            }

            if (restockLogVM.LogState == LogState.Accepted)
            {
                TempData["Error"] = "Restock log id #" + restockLogID + " has been accepted before";
                return RedirectToAction("Index", new { page = 1 });
            }

            restockLogVM.LogState = LogState.Accepted;
            restockLogVM.RestockState = RestockState.ReadyToRestock;
            restockLogVM.AppUser = null;

            //Put restock log
            var responsePutRestockLog = await _httpClient.PutAsJsonAsync("api/RestockLog/" + restockLogID, restockLogVM);
            var putRestockLogContent = await responsePutRestockLog.Content.ReadAsStringAsync();

            if (!responsePutRestockLog.IsSuccessStatusCode)
            {
                TempData["Error"] = putRestockLogContent;
                return RedirectToAction("Index", new { page = 1 });
            }

            string accountSid = "ACfa04bb234e7b4c07cd8effc2adac0419";
            string authToken = "61e4f6dd4c10dc60420c257f6e51c44b";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Restock log id: #" + restockLogVM +
                "\nRequest Date: " + restockLogVM.RequestDate +
                "\nYour log has been accepted, please go to warehouse and restock.",
                from: new Twilio.Types.PhoneNumber("+12542806183"),
                to: new Twilio.Types.PhoneNumber(phoneNumber)
            );

            TempData["Success"] = "You have accepted restock log id #" + restockLogID;
            return RedirectToAction("Index", new { page = 1 });
        }

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> RejectRestockLog(int restockLogID)
        {
            RestockLogVM restockLogVM = new RestockLogVM();

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync("api/RestockLog/" + restockLogID);
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                restockLogVM = await JsonSerializer.DeserializeAsync<RestockLogVM>(apiResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() },
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
            }


            if (restockLogVM.LogState == LogState.Declined)
            {
                TempData["Error"] = "Restock log id #" + restockLogID + " has been rejected before";
                return RedirectToAction("Index", new { page = 1 });
            }

            if (restockLogVM.LogState == LogState.Accepted)
            {
                TempData["Error"] = "Restock log id #" + restockLogID + " has been accepted before";
                return RedirectToAction("Index", new { page = 1 });
            }

            restockLogVM.LogState = LogState.Declined;
            restockLogVM.RestockState = RestockState.Canceled;

            //Put restock log
            var responsePutRestockLog = await _httpClient.PutAsJsonAsync("api/RestockLog/" + restockLogID, restockLogVM);
            var putRestockLogContent = await responsePutRestockLog.Content.ReadAsStringAsync();

            if (!responsePutRestockLog.IsSuccessStatusCode)
            {
                TempData["Error"] = putRestockLogContent;
                return RedirectToAction("Index", new { page = 1 });
            }

            TempData["Success"] = "You have rejected restock log id #" + restockLogID;
            return RedirectToAction("Index", new { page = 1 });
        }

        public async Task<IActionResult> Restock(int restockLogID)
        {
            RestockLogVM restockLogVM = new RestockLogVM();

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var responseRestockLog = await _httpClient.GetAsync("api/RestockLog/" + restockLogID);
            if (responseRestockLog.IsSuccessStatusCode)
            {
                var apiResponse = await responseRestockLog.Content.ReadAsStreamAsync();
                restockLogVM = await JsonSerializer.DeserializeAsync<RestockLogVM>(apiResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() },
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
            }

            if (restockLogVM.RestockState == RestockState.Restocked)
            {
                TempData["Error"] = "Restock log id #" + restockLogID + " has been restocked before";
                return RedirectToAction("MyRestockLog", new { page = 1 });
            }

            if (restockLogVM.LogState == LogState.Declined)
            {
                TempData["Error"] = "Restock log id #" + restockLogID + " has been rejected before, contact your manager";
                return RedirectToAction("MyRestockLog", new { page = 1 });
            }

            if (restockLogVM.LogState != LogState.Accepted)
            {
                TempData["Error"] = "Restock log id #" + restockLogID + " has not been accepted before, contact your manager";
                return RedirectToAction("MyRestockLog", new { page = 1 });
            }

            restockLogVM.RestockState = RestockState.Restocked;
            restockLogVM.RestockDate = DateTime.Now;

            //Put restock log
            var responsePutRestockLog = await _httpClient.PutAsJsonAsync("api/RestockLog/" + restockLogID, restockLogVM);
            var putRestockLogContent = await responsePutRestockLog.Content.ReadAsStringAsync();

            if (!responsePutRestockLog.IsSuccessStatusCode)
            {
                TempData["Error"] = putRestockLogContent;
                return RedirectToAction("MyRestockLog", new { page = 1 });
            }

            //Update quantity in truck
            foreach (var item in restockLogVM.DetailRestockLogs)
            {
                //Get truck stock item
                TruckStockItemVM truckStockItemVM = new TruckStockItemVM();

                var responseTruckStockItem = await _httpClient.GetAsync("api/TruckStockItem/" + item.StockItemID + "/itemid");
                if (responseTruckStockItem.IsSuccessStatusCode)
                {
                    var apiResponse = await responseTruckStockItem.Content.ReadAsStreamAsync();
                    truckStockItemVM = await JsonSerializer.DeserializeAsync<TruckStockItemVM>(apiResponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    });
                }
                truckStockItemVM.QuantityInTruck += item.Quantity;

                //Put truck stock item to update quantity in truck
                var responsePutTruckStockItem = await _httpClient.PutAsJsonAsync("api/TruckStockItem/" + truckStockItemVM.TruckStockItemID, truckStockItemVM);
                var putTruckStockItemContent = await responsePutTruckStockItem.Content.ReadAsStringAsync();

                if (!responsePutTruckStockItem.IsSuccessStatusCode)
                {
                    TempData["Error"] = putTruckStockItemContent;
                    return RedirectToAction("MyRestockLog", new { page = 1 });
                }
            }

            //Update quantity in warehouse
            foreach (var item in restockLogVM.DetailRestockLogs)
            {
                //Get equipment in warehouse
                StockItemVM stockItemVM = new StockItemVM();

                var responseStockItem = await _httpClient.GetAsync("api/StockItem/" + item.StockItemID);
                if (responseStockItem.IsSuccessStatusCode)
                {
                    var apiResponse = await responseStockItem.Content.ReadAsStreamAsync();
                    stockItemVM = await JsonSerializer.DeserializeAsync<StockItemVM>(apiResponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    });
                }

                stockItemVM.Quantity -= item.Quantity;

                //Put stock item to update quantity in warehouse
                var responsePutEquipment = await _httpClient.PutAsJsonAsync("api/StockItem/" + stockItemVM.StockItemID, stockItemVM);
                var putStockItemContent = await responsePutEquipment.Content.ReadAsStringAsync();

                if (!responsePutEquipment.IsSuccessStatusCode)
                {
                    TempData["Error"] = putStockItemContent;
                    return RedirectToAction("MyRestockLog", new { page = 1 });
                }
            }

            TempData["Success"] = "You have restocked log id #" + restockLogID + " check your quantity in truck";
            return RedirectToAction("MyRestockLog", new { page = 1 });
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
