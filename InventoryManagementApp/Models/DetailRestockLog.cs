namespace InventoryManagementApp.Models
{
    public class DetailRestockLog
    {
        public int DetailRestockLogID { get; set; }
        public int StockItemID { get; set; }
        public StockItem StockItem { get; set; }
        public int Quantity { get; set; }
        public RestockLog RestockLog { get; set; }
    }
}
