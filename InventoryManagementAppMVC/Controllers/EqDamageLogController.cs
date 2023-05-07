using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text.Json;
using InventoryManagementAppMVC.ViewModels;

namespace InventoryManagementAppMVC.Controllers
{
    public class EqDamageLogController : Controller
    {
        private readonly HttpClient _httpClient;

        public EqDamageLogController(IHttpClientFactory httpClientFactory)
        {
            this._httpClient = httpClientFactory.CreateClient("myclient");
        }

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Index()
        {
            List<EqDamageLogVM> eqDamageLogs = new List<EqDamageLogVM>();

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync("api/EqDamageLog");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                eqDamageLogs = await JsonSerializer.DeserializeAsync<List<EqDamageLogVM>>(apiResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() },
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
            }

            return View(eqDamageLogs);
        }
    }
}
