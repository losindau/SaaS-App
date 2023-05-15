using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

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
            return _context.DetailRestockLogs.Where(d => d.RestockLogID == restocklogID && d.isDeleted == false).ToList();
        }

        public RestockLog GetRestockLogById(int restocklogID)
        {
            return _context.RestockLogs.Include(r => r.AppUser).Where(d => d.RestockLogID == restocklogID).FirstOrDefault();
        }

        public ICollection<RestockLog> GetRestockLogByUserId(string userID)
        {
            return _context.RestockLogs.Where(d => d.AppUserID == userID && d.isDeleted == false).ToList();
        }

        public ICollection<RestockLog> GetRestockLogs()
        {
            return _context.RestockLogs.Where(r => r.isDeleted == false).OrderBy(r => r.RestockLogID).ToList();
        }

        public bool RestockLogExists(int restocklogID)
        {
            return _context.RestockLogs.Where(r => r.isDeleted == false).Any(r => r.RestockLogID == restocklogID);
        }

        public bool CreateRestockLog(RestockLog restockLog)
        {
            _context.Add(restockLog);
            return Save();
        }

        public bool UpdateRestockLog(RestockLog restockLog)
        {
            _context.Update(restockLog);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
