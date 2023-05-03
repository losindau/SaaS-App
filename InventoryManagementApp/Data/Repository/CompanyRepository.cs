using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _context;

        public CompanyRepository(DataContext context)
        {
            this._context = context;
        }
        public bool CompanyExists(int companyID)
        {
            return _context.Companies.Any(c => c.CompanyID == companyID);
        }

        public Company GetCompanyById(int companyID)
        {
            return _context.Companies.Where(c => c.CompanyID == companyID).FirstOrDefault();
        }

        public ICollection<Company> GetCompanys()
        {
            return _context.Companies.OrderBy(c => c.CompanyID).ToList();
        }
    }
}
