using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Repository
{
    public class TruckRepository : ITruckRepository
    {
        private readonly DataContext _context;

        public TruckRepository(DataContext context)
        {
            this._context = context;
        }
        public Truck GetTruckId(int truckID)
        {
            return _context.Trucks.Where(t => t.TruckID == truckID).FirstOrDefault();
        }

        public ICollection<Truck> GetTrucks()
        {
            return _context.Trucks.OrderBy(t => t.TruckID).ToList();
        }

        public ICollection<TruckStockItem> GetTruckStockItems(int truckID)
        {
            return _context.TruckStockItems.Where(t => t.TruckID == truckID).ToList();
        }

        public bool TruckExists(int truckID)
        {
            return _context.Trucks.Any(t => t.TruckID != truckID);
        }
    }
}
