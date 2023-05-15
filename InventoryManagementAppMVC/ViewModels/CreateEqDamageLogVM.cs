namespace InventoryManagementAppMVC.ViewModels
{
    public class CreateEqDamageLogVM
    {
        public int? ToolboxID { get; set; }
        public List<ToolboxEquipmentVM> ToolboxEquipments { get; set; }
        public Dictionary<int, int> ToolboxEquipmentQuantities { get; set; } = new Dictionary<int, int>();
        public Dictionary<int, string> ToolboxEquipmentNames { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> ToolboxEquipmentComments { get; set; } = new Dictionary<int, string>();

    }
}
