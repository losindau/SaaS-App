using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.ViewModels;

namespace InventoryManagementApp.Data.Repository
{
    public class TruckStockItemRepository : ITruckStockItemRepository
    {
        private readonly DataContext _context;

        public TruckStockItemRepository(DataContext context)
        {
            this._context = context;
        }

        public TruckStockItem GetTruckStockItemById(int truckStockItemID)
        {            
            return _context.TruckStockItems.Where(t => t.TruckStockItemID == truckStockItemID).FirstOrDefault();
        }

        public bool TruckStockItemExists(int truckStockItemID)
        {           
            return _context.TruckStockItems.Where(t => t.isDeleted == false).Any(t => t.TruckStockItemID == truckStockItemID);
        }

        public bool CreateTruckStockItems(List<TruckStockItem> truckStockItem)
        {
            _context.AddRange(truckStockItem);
            return Save();
        }

        public bool UpdateTruckStockItem(TruckStockItem truckStockItem)
        {
            _context.Update(truckStockItem);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
