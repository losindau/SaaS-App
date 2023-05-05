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
            return _context.Trucks.Where(t => t.isDeleted == false).OrderBy(t => t.TruckID).ToList();
        }

        public ICollection<TruckStockItem> GetTruckStockItems(int truckID)
        {
            return _context.TruckStockItems.Where(t => t.TruckID == truckID && t.isDeleted == false).ToList();
        }

        public bool TruckExists(int truckID)
        {
            return _context.Trucks.Where(t => t.isDeleted == false).Any(t => t.TruckID == truckID);
        }

        public bool CreateTruck(Truck truck)
        {
            _context.Add(truck);
            return Save();
        }

        public bool CreateTruckStockItems(List<TruckStockItem> truckStockItem)
        {
            _context.AddRange(truckStockItem);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
