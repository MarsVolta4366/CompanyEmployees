using Contracts;
using Entities.Dtos;
using Entities.Extensions;
using Entities.Models;
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

            return Ok(employees.MapIEnumerable<EmployeeDto>());
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);

            if (company is null)
            {
                return NotFound();
            }

            var employee = _repository.Employee.GetEmployee(companyId, id, trackChanges: false);

            if (employee is null)
            {
                return NotFound();
            }

            return Ok(employee.Map<EmployeeDto>());
        }

        [HttpPost]
        public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employeeDto)
        {
            if (employeeDto is null)
            {
                return BadRequest("EmployeeForCreationDto object is null.");
            }

            var company = _repository.Company.GetCompany(companyId, trackChanges: false);

            if (company is null)
            {
                return NotFound($"No company exists with id: {companyId}");
            }

            Employee employeeEntity = employeeDto.Map<Employee>();

            _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            _repository.Save();

            EmployeeDto employeeToReturn = employeeEntity.Map<EmployeeDto>();

            return CreatedAtAction(nameof(GetEmployeeForCompany), new { companyId, id = employeeToReturn.Id }, employeeToReturn);
        }
    }
}
