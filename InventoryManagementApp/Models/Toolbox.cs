namespace InventoryManagementApp.Models
{
    public class Toolbox
    {
        public int ToolboxID { get; set; }
        public Truck Truck { get; set; }
        public ICollection<ToolboxEquipment> ToolboxEquipments { get; set; }
    }
}
