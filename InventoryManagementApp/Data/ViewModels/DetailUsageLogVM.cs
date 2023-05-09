using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.ViewModels
{
    public class DetailUsageLogVM
    {
        public int DetailUsageLogID { get; set; }
        public int? StockItemID { get; set; }
        public string? StockItemName { get; set; }
        //public StockItemVM? StockItem { get; set; }
        public int Quantity { get; set; }
        public int? UsageLogID { get; set; }
        public int? CompanyID { get; set; }
        public bool isDeleted { get; set; }
    }
}
