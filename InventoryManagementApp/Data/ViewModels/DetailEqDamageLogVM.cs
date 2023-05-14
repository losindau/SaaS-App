using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.ViewModels
{
    public class DetailEqDamageLogVM
    {
        public int DetailEqDamageLogID { get; set; }
        public int? EquipmentID { get; set; }
        public string? EquipmentName { get; set; }
        public int Quantity { get; set; }
        public string Comment { get; set; }
        public int? EqDamageLogID { get; set; }
        public int? CompanyID { get; set; }
        public bool isDeleted { get; set; }
    }
}
