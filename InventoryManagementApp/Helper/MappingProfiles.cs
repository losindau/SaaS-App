using AutoMapper;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.ViewModels;

namespace InventoryManagementApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UsageLog, StockItemVM>();
            CreateMap<Equipment, EquipmentVM>();
            CreateMap<UsageLog, UsageLogVM>();
            CreateMap<DetailUsageLog, DetailUsageLogVM>();
        }   
    }
}
