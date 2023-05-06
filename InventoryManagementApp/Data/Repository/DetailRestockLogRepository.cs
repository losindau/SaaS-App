using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.ViewModels;

namespace InventoryManagementApp.Data.Repository
{
    public class DetailRestockLogRepository : IDetailRestockLogRepository
    {
        private readonly DataContext _context;

        public DetailRestockLogRepository(DataContext context)
        {
            this._context = context;
        }

        public DetailRestockLog GetDetailRestockLogById(int detailRestocklogID)
        {
            return _context.DetailRestockLogs.Where(d => d.DetailRestockLogID == detailRestocklogID).FirstOrDefault();
        }

        public bool DetailRestockLogExists(int detailRestocklogID)
        {
            return _context.DetailRestockLogs.Where(r => r.isDeleted == false).Any(r => r.DetailRestockLogID == detailRestocklogID);
        }

        public bool CreateDetailRestockLogs(List<DetailRestockLog> detailRestockLog)
        {
            _context.AddRange(detailRestockLog);
            return Save();
        }

        public bool UpdateDetailRestockLog(DetailRestockLog detailRestockLog)
        {
            _context.Update(detailRestockLog);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
