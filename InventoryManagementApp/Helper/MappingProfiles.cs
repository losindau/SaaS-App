using AutoMapper;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.ViewModels;

namespace InventoryManagementApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<StockItem, StockItemVM>();
        }
    }
}
