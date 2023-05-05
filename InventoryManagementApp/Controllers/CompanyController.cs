using AutoMapper;
using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.Repository;
using InventoryManagementApp.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyRepository companyRepository,IMapper mapper)
        {
            this._companyRepository = companyRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Company>))]
        public IActionResult GetCompanies()
        {
            var companies = _mapper.Map<List<CompanyVM>>(_companyRepository.GetCompanys());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(companies);
        }

        [HttpGet("{companyID}")]
        [ProducesResponseType(200, Type = typeof(Company))]
        [ProducesResponseType(400)]
        public IActionResult GetCompany(int companyID)
        {
            if (!_companyRepository.CompanyExists(companyID))
            {
                return NotFound();
            }

            var company = _mapper.Map<CompanyVM>(_companyRepository.GetCompanyById(companyID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(company);
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyVM companyCreate)
        {
            if (companyCreate == null)
            {
                return BadRequest(ModelState);
            }

            var company = _companyRepository.GetCompanys()
                .Where(i => i.Name.Trim().ToLower().Equals(companyCreate.Name.Trim().ToLower()))
                .FirstOrDefault();

            if (company != null)
            {
                ModelState.AddModelError("", "This item is already exists");
                return StatusCode(422, ModelState);
            }

            var companyMap = _mapper.Map<Company>(companyCreate);

            if (!_companyRepository.CreateCompany(companyMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
