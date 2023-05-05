namespace InventoryManagementApp.Data.ViewModels
{
    public class CompanyVM
    {
        public int CompanyID { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int? Phone { get; set; }
        public string? Email { get; set; }
        public bool isDeleted { get; set; }
    }
}
