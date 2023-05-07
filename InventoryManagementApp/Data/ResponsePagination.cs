using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.ViewModels;

namespace InventoryManagementApp.Data
{
    public class ResponsePagination
    {
        public List<object> Entities { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
