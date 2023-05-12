using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Interfaces
{
    public interface IEqDamageLogRepository
    {
        ICollection<EqDamageLog> GetEqDamageLogs();
        ICollection<EqDamageLog> GetEqDamageLogByUserId(string userID);
        EqDamageLog GetEqDamageLogById(int eqdamagelogID);
        bool EqDamageLogExists(int eqdamagelogID);
        ICollection<DetailEqDamageLog> GetDetailEqDamageLogs(int eqdamagelogID);
        bool CreateEqDamageLog(EqDamageLog eqDamageLog);
        bool UpdateEqDamageLog(EqDamageLog eqDamageLog);
        bool Save();
    }
}
