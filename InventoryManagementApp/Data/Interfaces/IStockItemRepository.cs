using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Interfaces
{
    public interface IStockItemRepository
    {
        ICollection<StockItem> GetStockItems();
        StockItem GetStockItemById(int stockitemID);
        bool StockItemExists(int stockitemID);
    }
}
