using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Interfaces
{
    public interface ICompanyRepository
    {
        ICollection<Company> GetCompanies();
        Company GetCompanyById(int companyID);
        bool CompanyExists(int companyID); 
        bool CreateCompany(Company company);
        bool UpdateCompany(Company company);
        bool Save();
    }
}
