using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementApp.Data.Repository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly DataContext _context;

        public EquipmentRepository(DataContext context)
        {
            this._context = context;
        }
        public bool EquipmentExists(int equipmentID)
        {
            return _context.Equipment.Any(e =>  e.EquipmentID == equipmentID);
        }

        public ICollection<Equipment> GetEquipments()
        {
            return _context.Equipment.OrderBy(e => e.EquipmentID).ToList();
        }

        public Equipment GetEquipmnetById(int equipmentID)
        {
            return _context.Equipment.Where(e => e.EquipmentID == equipmentID).FirstOrDefault();
        }
    }
}
