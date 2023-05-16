using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Interfaces
{
    public interface IStockItemRepository
    {
        ICollection<StockItem> GetStockItems();
        ICollection<StockItem> GetStockItemsNoPage();
        StockItem GetStockItemById(int stockitemID);
        bool StockItemExists(int stockitemID);
        bool CreateStockItem(StockItem stockItem);
        bool UpdateStockItem(StockItem stockItem);
        bool Save();
    }
}
