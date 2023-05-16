using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Interfaces
{
    public interface ITruckStockItemRepository
    {
        TruckStockItem GetTruckStockItemById(int truckStockItemID);
        TruckStockItem GetTruckStockItemByItemId(int itemID, int truckID);
        bool TruckStockItemExists(int truckStockItemID);
        bool CreateTruckStockItems(List<TruckStockItem> truckStockItem);
        bool UpdateTruckStockItem(TruckStockItem truckStockItem);
        bool Save();
    }
}
