using InventoryManagementApp.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementApp.Data.ViewModels
{
    public class TruckVM
    {
        public int TruckID { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public int? ToolboxID { get; set; }
        public AppUserVM? AppUser { get; set; }
        public ToolboxVM? Toolbox { get; set; }
        public string? DriverName { get; set; }
        public int? CompanyID { get; set; }
        public bool isDeleted { get; set; }

        public ICollection<TruckStockItemVM>? TruckStockItems { get; set; }
    }
}
