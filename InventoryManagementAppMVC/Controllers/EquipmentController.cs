using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text.Json;
using InventoryManagementAppMVC.ViewModels;
using InventoryManagementAppMVC.Enum;
using InventoryManagementAppMVC.Helper;

namespace InventoryManagementAppMVC.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EquipmentController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
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

            var response = await _httpClient.GetAsync("api/Equipment/" + page + "/equipments");
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
        public async Task<IActionResult> Create(EquipmentVM equipmentVM)
        {
            if (!ModelState.IsValid)
            {
                return View(equipmentVM);
            }

            var companyID = _httpContextAccessor.HttpContext?.User.GetUserCompanyID();
            equipmentVM.CompanyID = int.Parse(companyID);
            equipmentVM.isDeleted = false;

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var responsePostEquipment = await _httpClient.PostAsJsonAsync("api/Equipment", equipmentVM);
            var postEquipmentVMContent = await responsePostEquipment.Content.ReadAsStringAsync();
            if (!responsePostEquipment.IsSuccessStatusCode)
            {
                TempData["Error"] = postEquipmentVMContent;
                return View(equipmentVM);
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

            List<ToolboxEquipmentVM> toolboxEquipmentVMs = new List<ToolboxEquipmentVM>();

            foreach (var item in truckVMs)
            {
                ToolboxEquipmentVM toolboxEquipmentVM = new ToolboxEquipmentVM()
                {
                    ToolboxID = item.ToolboxID,
                    EquipmentID = int.Parse(postEquipmentVMContent),
                    QuantityInToolbox = 0,
                    CompanyID = int.Parse(companyID),
                    isDeleted = false
                };

                toolboxEquipmentVMs.Add(toolboxEquipmentVM);
            }

            var responsePostToolboxEquipment = await _httpClient.PostAsJsonAsync("api/ToolboxEquipment", toolboxEquipmentVMs);
            var postToolboxEquipmentContent = await responsePostToolboxEquipment.Content.ReadAsStringAsync();

            if (!responsePostToolboxEquipment.IsSuccessStatusCode)
            {
                TempData["Error"] = postToolboxEquipmentContent;
                return RedirectToAction("Index", new { page = 1 });
            }

            TempData["Success"] = "Create new equipment successfully";
            return RedirectToAction("Index", new { page = 1 });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            EquipmentVM responseEq = new EquipmentVM();

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync("api/Equipment/" + id);
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                responseEq = await JsonSerializer.DeserializeAsync<EquipmentVM>(apiResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
            }

            return View(responseEq);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EquipmentVM equipmentVM)
        {
            if (!ModelState.IsValid)
            {
                return View(equipmentVM);
            }

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var responsePut = await _httpClient.PutAsJsonAsync("api/Equipment/" + equipmentVM.EquipmentID, equipmentVM);
            if (!responsePut.IsSuccessStatusCode)
            {
                TempData["Error"] = await responsePut.Content.ReadAsStringAsync();
                return View(equipmentVM);
            }

            TempData["Success"] = "Update equipment successfully";
            return RedirectToAction("Index", new { page = 1 });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            EquipmentVM responseEq = new EquipmentVM();

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync("api/Equipment/" + id);
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                responseEq = await JsonSerializer.DeserializeAsync<EquipmentVM>(apiResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
            }

            responseEq.isDeleted = true;

            var responsePut = await _httpClient.PutAsJsonAsync("api/Equipment/" + responseEq.EquipmentID, responseEq);
            if (!responsePut.IsSuccessStatusCode)
            {
                TempData["Error"] = await responsePut.Content.ReadAsStringAsync();
                return RedirectToAction("Index", new { page = 1 });
            }

            TempData["Success"] = "Delete Equipment successfully";
            return RedirectToAction("Index", new { page = 1 });
        }
    }
}
