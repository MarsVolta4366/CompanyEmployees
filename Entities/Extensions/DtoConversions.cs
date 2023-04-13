using Entities.Dtos;
using Entities.Models;

namespace Entities.Extensions
{
    public static class DtoConversions
    {
        public static CompanyDto ConvertToDto(this Company company) =>
            new()
            {
                Id = company.Id,
                Name = company.Name,
                FullAddress = string.Join(' ', company.Address, company.Country)
            };

        public static Company ConvertToCompany(this CompanyForCreationDto companyForCreationDto) =>
            new()
            {
                Name = companyForCreationDto.Name,
                Address = companyForCreationDto.Address,
                Country = companyForCreationDto.Country,
                Employees = companyForCreationDto.Employees.Select(x => x.ConvertToEmployee()).ToList()
            };

        public static Employee ConvertToEmployee(this EmployeeForCreationDto employeeForCreationDto) =>
            new()
            {
                Name = employeeForCreationDto.Name,
                Age = employeeForCreationDto.Age,
                Position = employeeForCreationDto.Position
            };
    }
}
