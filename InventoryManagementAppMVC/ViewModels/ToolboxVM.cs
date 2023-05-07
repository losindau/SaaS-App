namespace InventoryManagementAppMVC.ViewModels
{
    public class ToolboxVM
    {
        public int ToolboxID { get; set; }
        public int? CompanyID { get; set; }
        public bool isDeleted { get; set; }

        public ICollection<ToolboxEquipmentVM>? ToolboxEquipments { get; set; }
    }
}
