namespace InventoryManagementApp.Models
{
    public class ToolboxEquipment
    {
        public int ToolboxEquipmentID { get; set; }
        public int ToolboxID { get; set; }
        public Toolbox Toolbox { get; set; }
        public int EquipmentID { get; set; }
        public Equipment Equipment { get; set; }
        public int QuantityInToolbox { get; set; }

    }
}
