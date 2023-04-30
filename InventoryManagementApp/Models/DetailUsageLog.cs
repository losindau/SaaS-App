namespace InventoryManagementApp.Models
{
    public class DetailUsageLog
    {
        public int DetailUsageLogID { get; set; }
        public int StockItemID { get; set; }
        public StockItem StockItem { get; set; }
        public int Quantity { get; set; }
        public UsageLog UsageLog { get; set; }
    }
}
