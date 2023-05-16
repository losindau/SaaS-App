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
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace InventoryManagementAppMVC.Controllers
{
    public class EqDamageLogController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public EqDamageLogController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this._httpClient = httpClientFactory.CreateClient("myclient");
            this._httpContextAccessor = httpContextAccessor;
            this._configuration = configuration;
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

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> AcceptEqDamageLog(int eqDamageLogID)
        {
            EqDamageLogVM eqDamageLogVM = new EqDamageLogVM();

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync("api/EqDamageLog/" + eqDamageLogID);
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                eqDamageLogVM = await JsonSerializer.DeserializeAsync<EqDamageLogVM>(apiResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() },
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
            }

            string phoneNumber = eqDamageLogVM.AppUser.PhoneNumber;
            phoneNumber.Remove(0, 1).Insert(0, "+84");

            if (eqDamageLogVM.LogState == LogState.Declined)
            {
                TempData["Error"] = "Damage/Lost log id #" + eqDamageLogID + " has been rejected before";
                return RedirectToAction("Index", new { page = 1 });
            }

            if (eqDamageLogVM.LogState == LogState.Accepted)
            {
                TempData["Error"] = "Damage/Lost id #" + eqDamageLogID + " has been accepted before";
                return RedirectToAction("Index", new { page = 1 });
            }

            eqDamageLogVM.LogState = LogState.Accepted;
            eqDamageLogVM.RestockState = RestockState.ReadyToRestock;
            eqDamageLogVM.AppUser = null;

            //Put restock log
            var responsePutEqDamageLog = await _httpClient.PutAsJsonAsync("api/EqDamageLog/" + eqDamageLogID, eqDamageLogVM);
            var putEqDamageLogContent = await responsePutEqDamageLog.Content.ReadAsStringAsync();

            if (!responsePutEqDamageLog.IsSuccessStatusCode)
            {
                TempData["Error"] = putEqDamageLogContent;
                return RedirectToAction("Index", new { page = 1 });
            }

            //string accountSid = _configuration["TwilioAccountDetails:AccountSid"];
            //string authToken = _configuration["TwilioAccountDetails:AuthToken"];

            //TwilioClient.Init(accountSid, authToken);

            //var message = MessageResource.Create(
            //    body: "Equipment Damage/Lost log id: #" + eqDamageLogVM.EqDamageLogID +
            //    "\nReport Date: " + eqDamageLogVM.ReportDate +
            //    "\nYour log has been accepted, please go to warehouse and restock.",
            //    from: new Twilio.Types.PhoneNumber("+12543213907"),
            //    to: new Twilio.Types.PhoneNumber("+84946777827")
            //);

            TempData["Success"] = "You have accepted Damage/Lost log id #" + eqDamageLogID;
            return RedirectToAction("Index", new { page = 1 });
        }

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> RejectEqDamageLog(int eqDamageLogID)
        {
            EqDamageLogVM eqDamageLogVM = new EqDamageLogVM();

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync("api/EqDamageLog/" + eqDamageLogID);
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                eqDamageLogVM = await JsonSerializer.DeserializeAsync<EqDamageLogVM>(apiResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() },
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
            }

            if (eqDamageLogVM.LogState == LogState.Declined)
            {
                TempData["Error"] = "Damage/Lost log id #" + eqDamageLogID + " has been rejected before";
                return RedirectToAction("Index", new { page = 1 });
            }

            if (eqDamageLogVM.LogState == LogState.Accepted)
            {
                TempData["Error"] = "Damage/Lost id #" + eqDamageLogID + " has been accepted before";
                return RedirectToAction("Index", new { page = 1 });
            }

            eqDamageLogVM.LogState = LogState.Declined;
            eqDamageLogVM.RestockState = RestockState.Canceled;
            eqDamageLogVM.AppUser = null;

            //Put restock log
            var responsePutEqDamageLog = await _httpClient.PutAsJsonAsync("api/EqDamageLog/" + eqDamageLogID, eqDamageLogVM);
            var putEqDamageLogContent = await responsePutEqDamageLog.Content.ReadAsStringAsync();

            if (!responsePutEqDamageLog.IsSuccessStatusCode)
            {
                TempData["Error"] = putEqDamageLogContent;
                return RedirectToAction("Index", new { page = 1 });
            }

            TempData["Success"] = "You have rejected Damage/Lost log id #" + eqDamageLogID;
            return RedirectToAction("Index", new { page = 1 });
        }

        public async Task<IActionResult> Restock(int eqDamageLogID)
        {
            EqDamageLogVM eqDamageLogVM = new EqDamageLogVM();

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var responseEqDamageLog = await _httpClient.GetAsync("api/EqDamageLog/" + eqDamageLogID);
            if (responseEqDamageLog.IsSuccessStatusCode)
            {
                var apiResponse = await responseEqDamageLog.Content.ReadAsStreamAsync();
                eqDamageLogVM = await JsonSerializer.DeserializeAsync<EqDamageLogVM>(apiResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() },
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
            }

            if (eqDamageLogVM.RestockState == RestockState.Restocked)
            {
                TempData["Error"] = "Damage/Lost log id #" + eqDamageLogID + " has been restocked before";
                return RedirectToAction("MyEqDamageLog", new { page = 1 });
            }

            if (eqDamageLogVM.LogState == LogState.Declined)
            {
                TempData["Error"] = "Damage/Lost log id #" + eqDamageLogID + " has been rejected before, contact your manager";
                return RedirectToAction("MyEqDamageLog", new { page = 1 });
            }

            if (eqDamageLogVM.LogState != LogState.Accepted)
            {
                TempData["Error"] = "Damage/Lost id #" + eqDamageLogID + " has not been accepted before, contact your manager";
                return RedirectToAction("MyEqDamageLog", new { page = 1 });
            }

            eqDamageLogVM.RestockState = RestockState.Restocked;
            eqDamageLogVM.ReplaceDate = DateTime.Now;
            eqDamageLogVM.AppUser = null;

            //Put restock log
            var responsePutRestockLog = await _httpClient.PutAsJsonAsync("api/EqDamageLog/" + eqDamageLogID, eqDamageLogVM);
            var putRestockLogContent = await responsePutRestockLog.Content.ReadAsStringAsync();

            if (!responsePutRestockLog.IsSuccessStatusCode)
            {
                TempData["Error"] = putRestockLogContent;
                return RedirectToAction("MyEqDamageLog", new { page = 1 });
            }

            //Update quantity in toolbox
            foreach (var item in eqDamageLogVM.DetailEqDamageLogs)
            {
                //Get toolbox equipment
                ToolboxEquipmentVM toolboxEquipmentVM = new ToolboxEquipmentVM();

                var responseToolboxEquipment = await _httpClient.GetAsync("api/ToolboxEquipment/" + item.EquipmentID + "/equipmentid/" + eqDamageLogVM.ToolboxID + "/toolboxid");
                if (responseToolboxEquipment.IsSuccessStatusCode)
                {
                    var apiResponse = await responseToolboxEquipment.Content.ReadAsStreamAsync();
                    toolboxEquipmentVM = await JsonSerializer.DeserializeAsync<ToolboxEquipmentVM>(apiResponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    });
                }
                toolboxEquipmentVM.QuantityInToolbox += item.Quantity;

                //Put toolbox equipment to update quantity in toolbox
                var responsePutTruckStockItem = await _httpClient.PutAsJsonAsync("api/ToolboxEquipment/" + toolboxEquipmentVM.ToolboxEquipmentID, toolboxEquipmentVM);
                var putToolboxEquipmentContent = await responsePutTruckStockItem.Content.ReadAsStringAsync();

                if (!responsePutTruckStockItem.IsSuccessStatusCode)
                {
                    TempData["Error"] = putToolboxEquipmentContent;
                    return RedirectToAction("MyEqDamageLog", new { page = 1 });
                }
            }

            //Update quantity in warehouse
            foreach (var item in eqDamageLogVM.DetailEqDamageLogs)
            {
                //Get equipment in warehouse
                EquipmentVM equipmentVM = new EquipmentVM();

                var responseEquipment = await _httpClient.GetAsync("api/Equipment/" + item.EquipmentID);
                if (responseEquipment.IsSuccessStatusCode)
                {
                    var apiResponse = await responseEquipment.Content.ReadAsStreamAsync();
                    equipmentVM = await JsonSerializer.DeserializeAsync<EquipmentVM>(apiResponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    });
                }

                equipmentVM.Quantity -= item.Quantity;

                //Put equipment to update quantity in warehouse
                var responsePutEquipment = await _httpClient.PutAsJsonAsync("api/Equipment/" + equipmentVM.EquipmentID, equipmentVM);
                var putEquipmentContent = await responsePutEquipment.Content.ReadAsStringAsync();

                if (!responsePutEquipment.IsSuccessStatusCode)
                {
                    TempData["Error"] = putEquipmentContent;
                    return RedirectToAction("MyEqDamageLog", new { page = 1 });
                }
            }

            TempData["Success"] = "You have restocked log id #" + eqDamageLogID + " check your quantity in truck";
            return RedirectToAction("MyEqDamageLog", new { page = 1 });
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

                var response = await _httpClient.GetAsync("api/ToolboxEquipment/" + item.EquipmentID + "/equipmentid/" + eqdamagelogVM.ToolboxID + "/toolboxid");
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
