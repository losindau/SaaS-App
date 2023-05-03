using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Interfaces
{
    public interface ICompanyRepository
    {
        ICollection<Company> GetCompanys();
        Company GetCompanyById(int companyID);
        bool CompanyExists(int companyID);
    }
}
