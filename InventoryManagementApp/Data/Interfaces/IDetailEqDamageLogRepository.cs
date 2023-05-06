using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Interfaces
{
    public interface IDetailEqDamageLogRepository
    {
        DetailEqDamageLog GetDetailEqDamageLogById(int detailEqdamagelogID);
        bool DetailEqDamageLogExists(int detailEqdamagelogID);
        bool CreateDetailEqDamageLogs(List<DetailEqDamageLog> detailEqDamageLog);
        bool UpdateDetailEqDamageLog(DetailEqDamageLog detailEqDamageLog);
        bool Save();
    }
}
