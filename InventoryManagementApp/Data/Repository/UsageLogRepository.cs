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
            return _context.DetailUsageLogs.Where(d => d.UsageLogID == usagelogID && d.isDeleted == false).ToList();
        }

        public UsageLog GetUsageLogById(int usagelogID)
        {
            return _context.UsageLogs.Where(u => u.UsageLogID == usagelogID).FirstOrDefault();
        }

        public ICollection<UsageLog> GetUsageLogs()
        {
            return _context.UsageLogs.Where(u => u.isDeleted == false).OrderBy(u => u.UsageLogID).ToList();
        }

        public bool UsageLogExists(int usagelogID)
        {
            return _context.UsageLogs.Where(u => u.isDeleted == false).Any(u => u.UsageLogID == usagelogID);
        }

        public bool CreateUsageLog(UsageLog usageLog)
        {
            _context.Add(usageLog);
            return Save();
        }

        public bool CreateDetailUsageLogs(List<DetailUsageLog> detailUsageLogs)
        {
            _context.AddRange(detailUsageLogs);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
