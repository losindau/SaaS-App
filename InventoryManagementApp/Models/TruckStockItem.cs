namespace InventoryManagementApp.Models
{
    public class TruckStockItem
    {
        public int TruckStockItemID { get; set; }
        public int TruckID { get; set; }
        public Truck Truck { get; set; }
        public int StockItemID { get; set; }
        public StockItem StockItem { get; set; }
        public int QuantityInTruck { get; set; }
    }
}
