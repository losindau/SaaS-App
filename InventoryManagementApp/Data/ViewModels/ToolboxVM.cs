using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.ViewModels
{
    public class ToolboxVM
    {
        public int ToolboxID { get; set; }
        public TruckVM? Truck { get; set; }
        public int? CompanyID { get; set; }

        public ICollection<ToolboxEquipmentVM>? ToolboxEquipments { get; set; }
    }
}
