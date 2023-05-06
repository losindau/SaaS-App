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
        bool CreateDetailRestockLogs(List<DetailRestockLog> detailRestockLog);
        bool UpdateRestockLog(RestockLog restockLog);
        bool UpdateDetailRestockLog(DetailRestockLog detailRestockLog);
        bool Save();
    }
}
