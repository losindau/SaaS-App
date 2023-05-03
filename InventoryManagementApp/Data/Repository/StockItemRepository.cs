using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Repository
{
    public class StockItemRepository : IStockItemRepository
    {
        private readonly DataContext _context;

        public StockItemRepository(DataContext context)
        {
            this._context = context;
        }

        public ICollection<StockItem> GetStockItems()
        {
            return _context.StockItems.OrderBy(i => i.StockItemID).ToList();
        }

        public StockItem GetStockItemById(int stockitemID)
        {
            return _context.StockItems.Where(i => i.StockItemID == stockitemID).FirstOrDefault();
        }

        public bool StockItemExists(int stockitemID)
        {
            return _context.StockItems.Any(i => i.StockItemID == stockitemID);
        }

        public bool CreateStockItem(StockItem stockitem)
        {
            _context.Add(stockitem);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
