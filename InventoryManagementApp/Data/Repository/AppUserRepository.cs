using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Repository
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly DataContext _context;

        public AppUserRepository(DataContext context)
        {
            this._context = context;
        }
        public bool AppUserExists(int appuserID)
        {
            throw new NotImplementedException();
        }

        public AppUser GetAppUserById(int appuserID)
        {
            throw new NotImplementedException();
        }

        public ICollection<AppUser> GetAppUsers()
        {
            throw new NotImplementedException();
        }

        public ICollection<MaintenanceActivity> GetMaintenanceActivitys(int appuserID)
        {
            throw new NotImplementedException();
        }
    }
}
