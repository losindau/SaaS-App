using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Interfaces
{
    public interface IAppUserRepository
    {
        ICollection<AppUser> GetAppUsers();
        AppUser GetAppUserById(int appuserID);
        bool AppUserExists(int appuserID);
        ICollection<MaintenanceActivity> GetMaintenanceActivitys(int appuserID);

    }
}
