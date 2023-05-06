using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.ViewModels
{
    public class DetailRestockLogVM
    {
        public int DetailRestockLogID { get; set; }
        public int? StockItemID { get; set; }
        //public StockItemVM? StockItem { get; set; }
        public int Quantity { get; set; }
        public int? RestockLogID { get; set; }
        public int? CompanyID { get; set; }
        public bool isDeleted { get; set; }
    }
}
