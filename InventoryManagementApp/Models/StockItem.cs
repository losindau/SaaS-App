using InventoryManagementApp.Data.Enum;

namespace InventoryManagementApp.Models
{
    public class StockItem
    {
        public int StockItemID { get; set; }
        public string Name { get; set; }
        public StockItemType Type { get; set; }
        public int Quantity { get; set; }
        public QuantityState QuantityState { get; set; }
        public ICollection<TruckStockItem> TruckStockItems { get; set; }
    }
}
