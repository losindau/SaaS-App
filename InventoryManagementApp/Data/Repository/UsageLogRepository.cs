using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Repository
{
    public class UsageLogRepository : IUsageLogRepository
    {
        private readonly DataContext _context;

        public UsageLogRepository(DataContext context)
        {
            this._context = context;
        }
        public ICollection<DetailUsageLog> GetDetailUsageLogs(int usagelogID)
        {
            return _context.DetailUsageLogs.Where(d => d.UsageLogID == usagelogID).ToList();
        }

        public UsageLog GetUsageLogById(int usagelogID)
        {
            return _context.UsageLogs.Where(u => u.UsageLogID == usagelogID).FirstOrDefault();
        }

        public ICollection<UsageLog> GetUsageLogs()
        {
            return _context.UsageLogs.OrderBy(u => u.UsageLogID).ToList();
        }

        public bool UsageLogExists(int usageuogID)
        {
            return _context.UsageLogs.Any(u => u.UsageLogID == usageuogID);
        }
    }
}
