using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Interfaces
{
    public interface IUsageLogRepository
    {
        ICollection<UsageLog> GetUsageLogs();
        ICollection<UsageLog> GetUsageLogByUserId(string userID);
        UsageLog GetUsageLogById(int usagelogID);
        bool UsageLogExists(int usagelogID);
        ICollection<DetailUsageLog> GetDetailUsageLogs(int usagelogID);
        bool CreateUsageLog(UsageLog usageLog);
        bool UpdateUsageLog(UsageLog usageLog);
        bool Save();
    }
}
