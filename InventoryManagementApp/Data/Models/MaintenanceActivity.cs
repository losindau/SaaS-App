using InventoryManagementApp.Data.Enum;
using InventoryManagementApp.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementApp.Data.Models
{
    public class MaintenanceActivity : ITenantEntity
    {
        [Key]
        public int ActivityID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public ActivityState State { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserID { get; set; }
        public AppUser? AppUser { get; set; }
        public int? TruckID { get; set; }
        public Truck? Truck { get; set; }
        public int? CompanyID { get; set; }
        public Company? Company { get; set; }
    }
}
