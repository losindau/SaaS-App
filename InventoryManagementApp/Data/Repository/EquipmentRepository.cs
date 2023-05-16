using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.ViewModels;
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
            return _context.Equipment.Where(c => c.isDeleted == false).Any(e =>  e.EquipmentID == equipmentID);
        }

        public ICollection<Equipment> GetEquipments()
        {
            return _context.Equipment.Where(c => c.isDeleted == false).OrderBy(e => e.EquipmentID).ToList();
        }

        public ICollection<Equipment> GetEquipmentsNoPage()
        {
            return _context.Equipment.Where(c => c.isDeleted == false).OrderBy(e => e.EquipmentID).ToList();
        }

        public Equipment GetEquipmnetById(int equipmentID)
        {
            return _context.Equipment.Where(e => e.EquipmentID == equipmentID).FirstOrDefault();
        }

        public bool CreateEquipment(Equipment equipment)
        {
            _context.Add(equipment);
            return Save();
        }

        public bool UpdateEquipment(Equipment equipment)
        {
            _context.Update(equipment);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
