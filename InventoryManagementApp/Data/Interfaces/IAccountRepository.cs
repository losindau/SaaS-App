﻿using InventoryManagementApp.Data.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagementApp.Data.Interfaces
{
    public interface IAccountRepository
    {
        public Task<string> SignInAsync(SignInVM signInVM);
    }
}
