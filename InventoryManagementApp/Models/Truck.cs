namespace InventoryManagementApp.Models
{
    public class Truck
    {
        public int TruckID { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public Toolbox Toolbox { get; set; }
        public Staff Staff { get; set; }
        public ICollection<TruckStockItem> TruckStockItems { get; set; }
    }
}
