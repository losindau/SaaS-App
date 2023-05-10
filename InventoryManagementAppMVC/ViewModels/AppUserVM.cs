namespace InventoryManagementAppMVC.ViewModels
{
    public class AppUserVM
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public int? TruckID { get; set; }
        public TruckVM? Truck { get; set; }
        public int? CompanyID { get; set; }

    }
}
