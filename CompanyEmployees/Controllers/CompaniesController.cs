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
            try
            {
                var companies = _repository.Company.GetAllCompanies(trackChanges: false);
                var companyDtos = companies.Select(c => c.ConvertToDto()).ToList();

                return Ok(companyDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetCompanies)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("company-name")]
        public IActionResult GetCompaniesByCondition(string companyName)
        {
            try
            {
                Company? company = _repository.Company.GetCompanyByName(companyName, false);
                return Ok(company);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetCompaniesByCondition)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
