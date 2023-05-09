using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.ViewModels;

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
            return _context.Companies.Where(c => c.isDeleted == false).Any(c => c.CompanyID == companyID);
        }

        public Company GetCompanyById(int companyID)
        {
            return _context.Companies.Where(c => c.CompanyID == companyID).FirstOrDefault();
        }

        public ICollection<Company> GetCompanies()
        {
            return _context.Companies.Where(c => c.isDeleted == false).OrderBy(c => c.CompanyID).ToList();
        }

        public bool CreateCompany(Company company)
        {
            _context.Add(company);
            return Save();
        }

        public bool UpdateCompany(Company company)
        {
            _context.Update(company);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
