namespace InventoryManagementApp.Models
{
    public class Staff
    {
        public int StaffID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int CompanyID { get; set; }
        public Company Company { get; set; }
        public Truck Truck { get; set; }
    }
}
