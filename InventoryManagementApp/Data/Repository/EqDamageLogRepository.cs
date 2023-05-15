using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementApp.Data.Repository
{
    public class EqDamageLogRepository : IEqDamageLogRepository
    {
        private readonly DataContext _context;

        public EqDamageLogRepository(DataContext context)
        {
            this._context = context;
        }

        public bool EqDamageLogExists(int eqdamagelogID)
        {
            return _context.EqDamageLogs.Where(e => e.isDeleted == false).Any(e => e.EqDamageLogID == eqdamagelogID);
        }

        public ICollection<DetailEqDamageLog> GetDetailEqDamageLogs(int eqdamagelogID)
        {
            return _context.DetailEqDamageLogs.Where(d => d.EqDamageLogID == eqdamagelogID && d.isDeleted == false).ToList();
        }

        public EqDamageLog GetEqDamageLogById(int eqdamagelogID)
        {
            return _context.EqDamageLogs.Include(e => e.AppUser).Where(e => e.EqDamageLogID == eqdamagelogID).FirstOrDefault();
        }

        public ICollection<EqDamageLog> GetEqDamageLogByUserId(string userID)
        {
            return _context.EqDamageLogs.Where(e => e.AppUserID == userID && e.isDeleted == false).ToList();
        }

        public ICollection<EqDamageLog> GetEqDamageLogs()
        {
            return _context.EqDamageLogs.Where(e => e.isDeleted == false).OrderBy(e => e.EqDamageLogID).ToList();
        }

        public bool CreateEqDamageLog(EqDamageLog eqDamageLog)
        {
            _context.Add(eqDamageLog);
            return Save();
        }

        public bool UpdateEqDamageLog(EqDamageLog eqDamageLog)
        {
            _context.Update(eqDamageLog);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
