using Entities.Dtos;
using Entities.Models;
using System.Reflection;

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

        // Allows dynamic converting from one type to another as long as property names are exactly the same.
        public static T ConvertToDto<T>(this object convertFrom) where T : new()
        {
            T convertTo = new();

            foreach (PropertyInfo propertyInfo in convertFrom.GetType().GetProperties())
            {
                var property = convertFrom.GetType().GetProperty(propertyInfo.Name);
                var value = property?.GetValue(convertFrom);

                var propToSet = convertTo.GetType().GetProperty(propertyInfo.Name);

                if (propToSet is not null)
                {
                    propToSet.SetValue(convertTo, value);
                }
            }

            return convertTo;
        }
    }
}
