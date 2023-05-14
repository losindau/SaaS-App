using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Interfaces
{
    public interface ITruckRepository
    {
        ICollection<Truck> GetTrucks();
        Truck GetTruckById(int truckID);
        bool TruckExists(int truckID);
        ICollection<TruckStockItem> GetTruckStockItems(int truckID);
        bool CreateTruck(Truck truck);
        bool UpdateTruck(Truck truck);
        bool Save();
    }
}
