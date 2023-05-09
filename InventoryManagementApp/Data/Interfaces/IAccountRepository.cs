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
    }
}
