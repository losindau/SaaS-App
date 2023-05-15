using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementApp.Data.Repository
{
    public class ToolboxEquipmentRepository : IToolboxEquipmentRepository
    {
        private readonly DataContext _context;

        public ToolboxEquipmentRepository(DataContext context)
        {
            this._context = context;
        }

        public ToolboxEquipment GetToolboxEquipmentById(int toolboxEquipmentID)
        {
            return _context.ToolboxEquipment.Where(t => t.ToolboxEquipmentID == toolboxEquipmentID).FirstOrDefault();
        }

        public ToolboxEquipment GetToolboxEquipmentByEqId(int equipmentID)
        {
            return _context.ToolboxEquipment.Where(t => t.EquipmentID == equipmentID).FirstOrDefault();
        }

        public bool ToolboxEquipmentExists(int toolboxEquipmentID)
        {
            return _context.ToolboxEquipment.Where(t => t.isDeleted == false).Any(t => t.ToolboxEquipmentID == toolboxEquipmentID);
        }

        public bool CreateToolboxEquipments(List<ToolboxEquipment> toolboxEquipment)
        {
            _context.AddRange(toolboxEquipment);
            return Save();
        }

        public bool UpdateToolboxEquipment(ToolboxEquipment toolboxEquipment)
        {
            _context.Update(toolboxEquipment);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
