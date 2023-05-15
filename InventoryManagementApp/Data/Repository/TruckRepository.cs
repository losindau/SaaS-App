using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementApp.Data.Repository
{
    public class TruckRepository : ITruckRepository
    {
        private readonly DataContext _context;

        public TruckRepository(DataContext context)
        {
            this._context = context;
        }

        public Truck GetTruckById(int truckID)
        {
            return _context.Trucks.Include(u => u.AppUser).Include(x => x.Toolbox.ToolboxEquipments).ThenInclude(x => x.Equipment).Where(t => t.TruckID == truckID).FirstOrDefault();
        }

        public ICollection<Truck> GetTrucks()
        {
            return _context.Trucks.Include(u => u.AppUser).Include(x => x.Toolbox.ToolboxEquipments).ThenInclude(x => x.Equipment).Where(t => t.isDeleted == false).OrderBy(t => t.TruckID).ToList();

        }

        public ICollection<Truck> GetAssignTrucks()
        {
            return _context.Trucks.Include(u => u.AppUser).Include(x => x.Toolbox.ToolboxEquipments).ThenInclude(x => x.Equipment).Where(t => t.isDeleted == false && t.AppUser == null).OrderBy(t => t.TruckID).ToList();

        }

        public ICollection<TruckStockItem> GetTruckStockItems(int truckID)
        {
            return _context.TruckStockItems.Include(x => x.StockItem).Where(t => t.TruckID == truckID && t.isDeleted == false).ToList();
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

        public bool UpdateTruck(Truck truck)
        {
            _context.Update(truck);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
