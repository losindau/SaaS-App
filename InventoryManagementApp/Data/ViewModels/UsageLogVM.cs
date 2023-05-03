using InventoryManagementApp.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementApp.Data.ViewModels
{
    public class UsageLogVM
    {
        public int UsageLogID { get; set; }
        public DateTime Date { get; set; }
        public int? TruckID { get; set; }
        public Truck? Truck { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserID { get; set; }
        public AppUser? AppUser { get; set; }
        public int? CompanyID { get; set; }

        public ICollection<DetailUsageLogVM>? DetailUsageLogs { get; set; }
    }
}
