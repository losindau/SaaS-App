using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Interfaces
{
    public interface IRestockLogRepository
    {
        ICollection<RestockLog> GetRestockLogs();
        RestockLog GetRestockLogById(int restocklogID);
        bool RestockLogExists(int restocklogID);
        ICollection<DetailRestockLog> GetDetailRestockLogs(int restocklogID);
        bool CreateRestockLog(RestockLog restockLog);
        bool UpdateRestockLog(RestockLog restockLog);
        bool Save();
    }
}
