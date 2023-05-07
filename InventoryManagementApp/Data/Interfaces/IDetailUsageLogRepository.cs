using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Interfaces
{
    public interface IDetailUsageLogRepository
    {
        DetailUsageLog GetDetailUsageLogById(int detailUsagelogID);
        bool DetailUsageLogExists(int detailUsagelogID);
        bool CreateDetailUsageLogs(List<DetailUsageLog> detailUsageLog);
        bool UpdateDetailUsageLog(DetailUsageLog detailUsageLog);
        bool Save();
    }
}
