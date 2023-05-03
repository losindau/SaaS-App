using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Repository
{
    public class RestockLogRepository : IRestockLogRepository
    {
        private readonly DataContext _context;

        public RestockLogRepository(DataContext context)
        {
            this._context = context;
        }
        public ICollection<DetailRestockLog> GetDetailRestockLogs(int restocklogID)
        {
            return _context.DetailRestockLogs.Where(d => d.RestockLogID == restocklogID).ToList();
        }

        public RestockLog GetRestockLogById(int restocklogID)
        {
            return _context.RestockLogs.Where(d => d.RestockLogID == restocklogID).FirstOrDefault();
        }

        public ICollection<RestockLog> GetRestockLogs()
        {
            return _context.RestockLogs.OrderBy(r => r.RestockLogID).ToList();
        }

        public bool RestockLogExists(int restocklogID)
        {
            return _context.RestockLogs.Any(r => r.RestockLogID == restocklogID);
        }
    }
}
