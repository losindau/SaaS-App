using InventoryManagementApp.Data.Enum;

namespace InventoryManagementApp.Models
{
    public class Equipment
    {
        public int EquipmentID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public EquipmentType Type { get; set; }
        public QualityState QualityState { get; set; }
        public ICollection<ToolboxEquipment> ToolboxEquipments { get; set; }
    }
}
