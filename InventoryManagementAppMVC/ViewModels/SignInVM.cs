using System.ComponentModel.DataAnnotations;

namespace InventoryManagementAppMVC.ViewModels
{
    public class SignInVM
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
