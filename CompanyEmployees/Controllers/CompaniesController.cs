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

        [HttpGet("{id}", Name = "CompanyById")]
        public IActionResult GetCompany(Guid id)
        {
            Company? company = _repository.Company.GetCompany(id, false);

            if (company is null)
            {
                return NotFound();
            }

            return Ok(company.ConvertToDto());
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto companyDto)
        {
            if (companyDto is null)
            {
                return BadRequest("CompanyForCreationDto object is null");
            }

            Company companyEntity = companyDto.Map<Company>();

            _repository.Company.CreateCompany(companyEntity);
            _repository.Save();

            CompanyDto companyToReturn = companyEntity.ConvertToDto();

            return CreatedAtAction(nameof(GetCompany), new { id = companyToReturn.Id }, companyToReturn);
        }
    }
}
