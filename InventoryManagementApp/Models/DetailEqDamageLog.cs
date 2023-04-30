namespace InventoryManagementApp.Models
{
    public class DetailEqDamageLog
    {
        public int DetailEqDamageLogID { get; set; }
        public int EquipmentID { get; set; }
        public Equipment Equipment { get; set; }
        public int Quantity { get; set; }
        public string Comment { get; set; }
        public EqDamageLog EqDamageLog { get; set; }
    }
}
