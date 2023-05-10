using System.ComponentModel.DataAnnotations;

namespace InventoryManagementAppMVC.ViewModels
{
    public class SignUpVM
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string? ConfirmPassword { get; set; }
        public string? Role { get; set; }
        public int? TruckID { get; set; }
        public int? CompanyID { get; set; }
    }
}
