using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementApp.Data.Repository
{
    public class DetailEqDamageLogRepository : IDetailEqDamageLogRepository
    {
        private readonly DataContext _context;

        public DetailEqDamageLogRepository(DataContext context)
        {
            this._context = context;
        }

        public DetailEqDamageLog GetDetailEqDamageLogById(int detailEqdamagelogID)
        {            
            return _context.DetailEqDamageLogs.Where(e => e.DetailEqDamageLogID == detailEqdamagelogID).FirstOrDefault();
        }

        public bool DetailEqDamageLogExists(int detailEqdamagelogID)
        {            
            return _context.DetailEqDamageLogs.Where(e => e.isDeleted == false).Any(e => e.DetailEqDamageLogID == detailEqdamagelogID);
        }

        public bool CreateDetailEqDamageLogs(List<DetailEqDamageLog> detailEqDamageLog)
        {
            _context.AddRange(detailEqDamageLog);
            return Save();
        }

        public bool UpdateDetailEqDamageLog(DetailEqDamageLog detailEqDamageLog)
        {
            _context.Update(detailEqDamageLog);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
