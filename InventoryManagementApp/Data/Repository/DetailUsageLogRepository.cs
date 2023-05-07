using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementApp.Data.Repository
{
    public class DetailUsageLogRepository : IDetailUsageLogRepository
    {
        private readonly DataContext _context;

        public DetailUsageLogRepository(DataContext context)
        {
            this._context = context;
        }

        public DetailUsageLog GetDetailUsageLogById(int detailUsagelogID)
        {
            return _context.DetailUsageLogs.Include(d => d.StockItem).Where(u => u.DetailUsageLogID == detailUsagelogID).FirstOrDefault();
        }

        public bool DetailUsageLogExists(int detailUsagelogID)
        {
            return _context.DetailUsageLogs.Where(u => u.isDeleted == false).Any(u => u.DetailUsageLogID == detailUsagelogID);
        }

        public bool CreateDetailUsageLogs(List<DetailUsageLog> detailUsageLogs)
        {
            _context.AddRange(detailUsageLogs);
            return Save();
        }

        public bool UpdateDetailUsageLog(DetailUsageLog detailUsageLog)
        {
            _context.Update(detailUsageLog);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }


    }
}
