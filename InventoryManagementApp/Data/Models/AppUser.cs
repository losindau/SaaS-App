using InventoryManagementApp.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagementApp.Data.Models
{
    public class AppUser : IdentityUser, ITenantEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? TruckID { get; set; }
        public Truck? Truck { get; set; }
        public int? CompanyID { get; set; }
        public Company? Company { get; set; }

        public ICollection<EqDamageLog>? EqDamageLogs { get; set; }
        public ICollection<RestockLog>? RestockLogs { get; set; }
        public ICollection<UsageLog>? UsageLogs { get; set; }
        public ICollection<MaintenanceActivity>? MaintenanceActivities { get; set; }
    }
}
