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
    }
}
