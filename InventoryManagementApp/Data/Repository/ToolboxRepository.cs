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
            return _context.ToolboxEquipment.Where(t => t.ToolboxID == toolboxID &&t.isDeleted == false).ToList();
        }

        public ICollection<Toolbox> GetToolboxes()
        {
            return _context.Toolboxes.Where(t => t.isDeleted == false).OrderBy(t => t.ToolboxID).ToList();
        }

        public bool ToolboxExists(int toolboxID)
        {
            return _context.Toolboxes.Where(t => t.isDeleted == false).Any(t => t.ToolboxID == toolboxID);
        }     
        
        public bool CreateToolbox(Toolbox toolbox)
        {
            _context.Add(toolbox);
            return Save();
        }

        public bool CreateToolboxEquipments(List<ToolboxEquipment> toolboxEquipment)
        {
            _context.AddRange(toolboxEquipment);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
