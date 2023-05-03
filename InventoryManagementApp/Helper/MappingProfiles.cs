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
            CreateMap<StockItemVM, StockItem>();
            CreateMap<Equipment, EquipmentVM>();
            CreateMap<UsageLog, UsageLogVM>();
            CreateMap<DetailUsageLog, DetailUsageLogVM>();
            CreateMap<RestockLog, RestockLogVM>();
            CreateMap<DetailRestockLog, DetailRestockLogVM>(); 
            CreateMap<EqDamageLog, EqDamageLogVM>();
            CreateMap<DetailEqDamageLog, DetailEqDamageLogVM>();
            CreateMap<AppUser, AppUserVM>();
            CreateMap<MaintenanceActivity, MaintenanceActivityVM>();
            CreateMap<Truck, TruckVM>();
            CreateMap<TruckStockItem, TruckStockItemVM>();
            CreateMap<Company, CompanyVM>();
        }   
    }
}
