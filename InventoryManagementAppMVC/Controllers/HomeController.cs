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
                var user = new AppUserVM()
                {
                    FirstName = User.FindFirstValue("FirstName"),
                    LastName = User.FindFirstValue("LastName"),
                    Email = User.FindFirstValue(ClaimTypes.Email),
                    Role = User.FindFirstValue(ClaimTypes.Role),
                    CompanyID = int.Parse(User.FindFirstValue("CompanyID"))
                };

                return View(user);
            }

            return RedirectToAction("SignIn", "Account");
        }
    }
}