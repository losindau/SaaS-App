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

namespace InventoryManagementAppMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
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

            // Create an instance of HttpClient
            // Set the base address of your API
            _httpClient.BaseAddress = new Uri("https://localhost:7105");

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

                // Store the token in the authentication cookie
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

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
