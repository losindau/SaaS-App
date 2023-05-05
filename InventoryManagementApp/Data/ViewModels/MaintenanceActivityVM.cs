using InventoryManagementApp.Data.Enum;
using InventoryManagementApp.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementApp.Data.ViewModels
{
    public class MaintenanceActivityVM
    {
        public int ActivityID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public ActivityState State { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserID { get; set; }
        public AppUserVM? AppUser { get; set; }
        public int? TruckID { get; set; }
        public TruckVM? Truck { get; set; }
        public int? CompanyID { get; set; }
        public bool isDeleted { get; set; }
    }
}
