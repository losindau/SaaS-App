using InventoryManagementApp.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementApp.Data.Models
{
    public class Truck : ITenantEntity
    {
        public int TruckID { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public int? ToolboxID { get; set; }
        public Toolbox? Toolbox { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserID { get; set; }
        public AppUser? AppUser { get; set; }
        public int? CompanyID { get; set; }
        public Company? Company { get; set; }
        public bool isDeleted { get; set; }

        public ICollection<TruckStockItem>? TruckStockItems { get; set; }
        public ICollection<RestockLog>? RestockLogs { get; set; }
        public ICollection<UsageLog>? UsageLogs { get; set; }
        public ICollection<MaintenanceActivity>? MaintenanceActivities { get; set; }
    }
}
