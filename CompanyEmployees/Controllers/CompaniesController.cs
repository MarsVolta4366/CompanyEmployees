using Contracts;
using Entities.Dtos;
using Entities.Extensions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public CompaniesController(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            var companies = _repository.Company.GetAllCompanies(trackChanges: false);
            var companyDtos = companies.Select(c => c.ConvertToDto()).ToList();

            return Ok(companyDtos);
        }

        [HttpGet("company-name")]
        public IActionResult GetCompaniesByCondition(string companyName)
        {
            Company? company = _repository.Company.GetCompanyByName(companyName, false);
            return Ok(company);
        }
    }
}
