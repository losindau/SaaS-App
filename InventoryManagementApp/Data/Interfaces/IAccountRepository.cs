using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagementApp.Data.Interfaces
{
    public interface IAccountRepository
    {
        public Task<string> SignInAsync(SignInVM signInVM);
        public Task<IdentityResult> SignUpAsync(AppUser appUser, string password);
        public ICollection<AppUser> GetUsers();
        public Task<AppUser> GetUserById(string userID);
        public Task<AppUser> GetUserByEmail(string email);
        public bool UserExists(string userID);
        public bool UpdateAccount(AppUser appUser);
        public bool Save();
    }
}
