using InventoryManagementApp.Data.Enum;

namespace InventoryManagementApp.Data.ViewModels
{
    public class StockItemVM
    {
        public int StockItemID { get; set; }
        public string Name { get; set; }
        public StockItemType Type { get; set; }
        public int Quantity { get; set; }
        public QuantityState QuantityState { get; set; }
        public int? CompanyID { get; set; }
        public bool isDeleted { get; set; }
    }
}
