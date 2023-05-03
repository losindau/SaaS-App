using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;

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
            return _context.EqDamageLogs.Any(e => e.EqDamageLogID == eqdamagelogID);
        }

        public ICollection<DetailEqDamageLog> GetDetailEqDamageLogs(int eqdamagelogID)
        {
            return _context.DetailEqDamageLogs.Where(d => d.EqDamageLogID == eqdamagelogID).ToList();
        }

        public EqDamageLog GetEqDamageLogById(int eqdamagelogID)
        {
            return _context.EqDamageLogs.Where(e => e.EqDamageLogID == eqdamagelogID).FirstOrDefault();
        }

        public ICollection<EqDamageLog> GetEqDamageLogs()
        {
            return _context.EqDamageLogs.OrderBy(e => e.EqDamageLogID).ToList();
        }
    }
}
