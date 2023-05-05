using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementAppMVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
