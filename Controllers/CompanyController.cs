using Employee_Accounting_Service.Data;
using Employee_Accounting_Service.Data.Entities;
using Employee_Accounting_Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Accounting_Service.Controllers
{
    [ApiController]
    [Route("/api/v1/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly ApplicationDbContext _databaseContext;

        public CompanyController(ApplicationDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet]
        public IList<Company> GetCompanies()
        {
            return _databaseContext.Companies.ToList();
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyDto companyDto)
        {
            string companyName = companyDto.Name?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(companyName))
                return BadRequest("Company name must not be null or empty!");

            Company? foundCompany = _databaseContext.Companies.FirstOrDefault(
                c => c.Name == companyName
            );

            if (foundCompany != null)
                return BadRequest("Company with specified name already exists!");

            Company company = new Company()
            {
                Name = companyName
            };

            _databaseContext.Companies.Add(company);
            _databaseContext.SaveChanges();

            return Ok(company);
        }
    }
}
