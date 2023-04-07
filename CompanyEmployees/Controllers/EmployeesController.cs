using Contracts;
using Entities.Dtos;
using Entities.Extensions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public EmployeesController(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetEmployeesForCompany(Guid companyId)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);

            if (company is null)
            {
                return NotFound($"No company exists with id: {companyId}");
            }

            var employees = _repository.Employee.GetEmployees(companyId, trackChanges: false);

            var employeeDtos = employees.Select(e => e.ConvertToDto<EmployeeDto>());

            return Ok(employeeDtos);
        }
    }
}
