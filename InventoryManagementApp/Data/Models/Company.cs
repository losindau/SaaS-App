using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementApp.Data.Models
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int? Phone { get; set; }
        public string? Email { get; set; }
        public bool isDeleted { get; set; }
        public ICollection<MaintenanceActivity>? MaintenanceActivities { get; set; }
        public ICollection<AppUser>? AppUsers { get; set; }
        public ICollection<Equipment>? Equipment { get; set; }
        public ICollection<Toolbox>? Toolboxes { get; set; }
        public ICollection<ToolboxEquipment>? ToolboxEquipment { get; set; }
        public ICollection<StockItem>? StockItems { get; set; }
        public ICollection<Truck>? Trucks { get; set; }
        public ICollection<TruckStockItem>? TruckStockItems { get; set; }
        public ICollection<UsageLog>? UsageLogs { get; set; }
        public ICollection<DetailUsageLog>? DetailUsageLogs { get; set; }
        public ICollection<RestockLog>? RestockLogs { get; set; }
        public ICollection<DetailRestockLog>? DetailRestockLogs { get; set; }
        public ICollection<EqDamageLog>? EqDamageLogs { get; set; }
        public ICollection<DetailEqDamageLog>? DetailEqDamageLogs { get; set; }
    }
}
