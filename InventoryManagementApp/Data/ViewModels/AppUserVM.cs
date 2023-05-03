using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.ViewModels
{
    public class AppUserVM
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Truck? Truck { get; set; }
        public int? CompanyID { get; set; }

        public ICollection<MaintenanceActivity>? MaintenanceActivities { get; set; }
    }
}
