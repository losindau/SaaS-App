using InventoryManagementAppMVC.Models;
using InventoryManagementAppMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace InventoryManagementAppMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = new UserVM()
                {
                    FirstName = User.FindFirstValue("FirstName"),
                    LastName = User.FindFirstValue("LastName"),
                    Email = User.FindFirstValue(ClaimTypes.Email),
                    Role = User.FindFirstValue(ClaimTypes.Role),
                    CompanyID = int.Parse(User.FindFirstValue("CompanyID"))
                };

                return View(user);
            }

            return RedirectToAction("Login", "Account");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}