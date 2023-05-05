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
            CreateMap<EquipmentVM, Equipment>();
            CreateMap<UsageLog, UsageLogVM>();
            CreateMap<UsageLogVM, UsageLog>();
            CreateMap<DetailUsageLog, DetailUsageLogVM>();
            CreateMap<DetailUsageLogVM, DetailUsageLog>();
            CreateMap<RestockLog, RestockLogVM>();
            CreateMap<RestockLogVM, RestockLog>();
            CreateMap<DetailRestockLog, DetailRestockLogVM>(); 
            CreateMap<DetailRestockLogVM, DetailRestockLog>();
            CreateMap<EqDamageLog, EqDamageLogVM>();
            CreateMap<EqDamageLogVM, EqDamageLog>();
            CreateMap<DetailEqDamageLog, DetailEqDamageLogVM>();
            CreateMap<DetailEqDamageLogVM, DetailEqDamageLog>();
            CreateMap<AppUser, AppUserVM>();
            CreateMap<AppUserVM, AppUser>();
            CreateMap<MaintenanceActivity, MaintenanceActivityVM>();
            CreateMap<MaintenanceActivityVM, MaintenanceActivity>();
            CreateMap<Truck, TruckVM>();
            CreateMap<TruckVM, Truck>();
            CreateMap<TruckStockItem, TruckStockItemVM>();
            CreateMap<TruckStockItemVM, TruckStockItem>();
            CreateMap<Company, CompanyVM>();
            CreateMap<CompanyVM, Company>();
            CreateMap<Toolbox, ToolboxVM>();
            CreateMap<ToolboxVM, Toolbox>();
            CreateMap<ToolboxEquipment, ToolboxEquipmentVM>();
            CreateMap<ToolboxEquipmentVM, ToolboxEquipment>();
        }   
    }
}
