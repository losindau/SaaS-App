using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Repository
{
    public class ToolboxRepository : IToolboxRepository
    {
        private readonly DataContext _context;

        public ToolboxRepository(DataContext context)
        {
            this._context = context;
        }

        public Toolbox GetToolboxById(int toolboxID)
        {
            return _context.Toolboxes.Where(t => t.ToolboxID == toolboxID).FirstOrDefault();
        }

        public ICollection<ToolboxEquipment> GetToolboxEquipments(int toolboxID)
        {
            return _context.ToolboxEquipment.Where(t => t.ToolboxID == toolboxID).ToList();
        }

        public ICollection<Toolbox> GetToolboxes()
        {
            return _context.Toolboxes.OrderBy(t => t.ToolboxID).ToList();
        }

        public bool ToolboxExists(int toolboxID)
        {
            return _context.Toolboxes.Any(t => t.ToolboxID == toolboxID);
        }
    }
}
