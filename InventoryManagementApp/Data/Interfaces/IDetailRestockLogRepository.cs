using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Interfaces
{
    public interface IDetailRestockLogRepository
    {
        DetailRestockLog GetDetailRestockLogById(int detailRestocklogID);
        bool DetailRestockLogExists(int detailRestocklogID);
        bool CreateDetailRestockLogs(List<DetailRestockLog> detailRestockLog);
        bool UpdateDetailRestockLog(DetailRestockLog detailRestockLog);
        bool Save();
    }
}
