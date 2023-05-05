using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.ViewModels
{
    public class ToolboxEquipmentVM
    {
        public int ToolboxEquipmentID { get; set; }
        public int? ToolboxID { get; set; }
        public int? EquipmentID { get; set; }
        public EquipmentVM? Equipment { get; set; }
        public int QuantityInToolbox { get; set; }
        public int? CompanyID { get; set; }
    }
}
