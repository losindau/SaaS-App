using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.ViewModels
{
    public class TruckStockItemVM
    {
        public int TruckStockItemID { get; set; }
        public int? TruckID { get; set; }
        public int? StockItemID { get; set; }
        //public StockItemVM? StockItem { get; set; }
        public int QuantityInTruck { get; set; }
        public int? CompanyID { get; set; }
        public bool isDeleted { get; set; }
    }
}
