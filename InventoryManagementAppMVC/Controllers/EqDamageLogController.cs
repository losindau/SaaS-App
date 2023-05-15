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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            TruckVM truckVM = new TruckVM();

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

            CreateEqDamageLogVM createEqDamageLogVM = new CreateEqDamageLogVM()
            {
                ToolboxID = truckVM.ToolboxID,
                ToolboxEquipments = truckVM.Toolbox.ToolboxEquipments.ToList()
            };

            foreach (var item in truckVM.Toolbox.ToolboxEquipments)
            {
                createEqDamageLogVM.ToolboxEquipmentNames.Add((int)item.EquipmentID, item.Equipment.Name);
                createEqDamageLogVM.ToolboxEquipmentQuantities.Add((int)item.EquipmentID, 0);
                createEqDamageLogVM.ToolboxEquipmentComments.Add((int)item.EquipmentID, "");
            }

            return View(createEqDamageLogVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEqDamageLogVM createEqDamageLogVM)
        {
            int count = 0;
            foreach (var item in createEqDamageLogVM.ToolboxEquipmentQuantities)
            {
                if (item.Value > 0)
                {
                    count++;
                }
            }

            if (count < 1)
            {
                TempData["Error"] = "To create equipment damage log you must change quantity of at least 1 item";
                return RedirectToAction("MyEqDamageLog", new { page = 1 });
            }

            // Create master equipment damage log
            var userID = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userName = _httpContextAccessor.HttpContext?.User.GetUserName();
            var companyID = _httpContextAccessor.HttpContext?.User.GetUserCompanyID();

            EqDamageLogVM eqdamagelogVM = new EqDamageLogVM()
            {
                ReportDate = DateTime.Now,
                LogState = LogState.Pending,
                RestockState = RestockState.NeedAccept,
                ToolboxID = createEqDamageLogVM.ToolboxID,
                AppUserID = userID,
                AppUserName = userName,
                CompanyID = int.Parse(companyID),
                isDeleted = false
            };

            //Post master equipment damage log then receive master equipment damage log id
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var responsePostEqDamageLog = await _httpClient.PostAsJsonAsync("api/EqDamageLog", eqdamagelogVM);
            var postEqDamageLogContent = await responsePostEqDamageLog.Content.ReadAsStringAsync();

            if (!responsePostEqDamageLog.IsSuccessStatusCode)
            {
                TempData["Error"] = postEqDamageLogContent;
                return RedirectToAction("MyEqDamageLog", new { page = 1 });
            }

            //Create detail usagelogs
            List<DetailEqDamageLogVM> detailEqDamageLogVMs = new List<DetailEqDamageLogVM>();

            foreach (var item in createEqDamageLogVM.ToolboxEquipmentQuantities)
            {
                int equipmentID = item.Key;
                int quantity = item.Value;

                if (quantity > 0)
                {
                    DetailEqDamageLogVM newDetailEqDamageLog = new DetailEqDamageLogVM()
                    {
                        EquipmentID = equipmentID,
                        EquipmentName = createEqDamageLogVM.ToolboxEquipmentNames[equipmentID],
                        Quantity = quantity,
                        Comment = createEqDamageLogVM.ToolboxEquipmentComments[equipmentID],
                        EqDamageLogID = int.Parse(postEqDamageLogContent),
                        CompanyID = int.Parse(companyID),
                        isDeleted = false
                    };

                    detailEqDamageLogVMs.Add(newDetailEqDamageLog);
                }
            }

            //Post list of detail usagelogs
            var responsePostDetailEqDamageLog = await _httpClient.PostAsJsonAsync("api/DetailEqDamageLog", detailEqDamageLogVMs);
            var postDetailEqDamageLogContent = await responsePostDetailEqDamageLog.Content.ReadAsStringAsync();

            if (!responsePostDetailEqDamageLog.IsSuccessStatusCode)
            {
                TempData["Error"] = postDetailEqDamageLogContent;
                return RedirectToAction("MyEqDamageLog", new { page = 1 });
            }


            //Update quantity in toolbox
            foreach (var item in detailEqDamageLogVMs)
            {
                //Get toolbox equipment
                ToolboxEquipmentVM responseToolboxEquipmentVM = new ToolboxEquipmentVM();

                var response = await _httpClient.GetAsync("api/ToolboxEquipment/" + item.EquipmentID + "/equipmentid");
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    responseToolboxEquipmentVM = await JsonSerializer.DeserializeAsync<ToolboxEquipmentVM>(apiResponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    });
                }
                responseToolboxEquipmentVM.QuantityInToolbox -= item.Quantity;

                //Put toolbox equipment to update quantity in toolbox
                var responsePutTruckStockItem = await _httpClient.PutAsJsonAsync("api/ToolboxEquipment/" + responseToolboxEquipmentVM.ToolboxEquipmentID, responseToolboxEquipmentVM);
                var putToolboxEquipmentContent = await responsePutTruckStockItem.Content.ReadAsStringAsync();

                if (!responsePutTruckStockItem.IsSuccessStatusCode)
                {
                    TempData["Error"] = putToolboxEquipmentContent;
                    return RedirectToAction("MyEqDamageLog", new { page = 1 });
                }
            }

            TempData["Success"] = "Create new equipment damage log successfully";
            return RedirectToAction("MyEqDamageLog", new { page = 1 });
        }

        [HttpGet]
        public async Task<IActionResult> MyEqDamageLog(int page)
        {
            ResponsePagination responsePage = new ResponsePagination();

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var userID = _httpContextAccessor.HttpContext?.User.GetUserId();
            var response = await _httpClient.GetAsync("api/EqDamageLog/" + page + "/myeqdamagelogs/" + userID);
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
