using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Interfaces
{
    public interface IEqDamageLogRepository
    {
        ICollection<EqDamageLog> GetEqDamageLogs();
        EqDamageLog GetEqDamageLogById(int eqdamagelogID);
        bool EqDamageLogExists(int eqdamagelogID);
        ICollection<DetailEqDamageLog> GetDetailEqDamageLogs(int eqdamagelogID);
        bool CreateEqDamageLog(EqDamageLog eqDamageLog);
        bool CreateDetailEqDamageLogs(List<DetailEqDamageLog> detailEqDamageLog);
        bool Save();
    }
}
