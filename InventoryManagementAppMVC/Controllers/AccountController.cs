using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using InventoryManagementAppMVC.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Data;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace InventoryManagementAppMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("myclient");
        }

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Index()
        {
            List<AppUserVM> users = new List<AppUserVM>();

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync("api/Account");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                users = await JsonSerializer.DeserializeAsync<List<AppUserVM>>(apiResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() },
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
            }

            return View(users);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(SignInVM signInVM)
        {
            if (!ModelState.IsValid)
            {
                return View(signInVM);
            }

            // Send a POST request to the login API
            var response = await _httpClient.PostAsJsonAsync("api/Account/SignIn", signInVM);

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a JWT token
                var token = await response.Content.ReadAsStringAsync();

                // Decode the JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);

                // Get the claims from the JWT token
                var claims = jwtToken.Claims.ToList();

                // Create a new ClaimsIdentity and add user information
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Create a new AuthenticationProperties object and set the access token
                var props = new AuthenticationProperties();
                props.StoreTokens(new List<AuthenticationToken>
                {
                    new AuthenticationToken { Name = "access_token", Value = token }
                });

                // Store the token in the authentication cookie and save it in the HTTP context
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), props);

                // Redirect the user to a protected page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Handle unsuccessful login (e.g., display an error message)
                TempData["Error"] = "Wrong credentials. Please, try again";
                return View(signInVM);
            }
        }

    }
}
