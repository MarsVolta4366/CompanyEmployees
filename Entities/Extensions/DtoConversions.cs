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

        /// <summary>
        /// Extension method that dynamically converts from type of this to type T.
        /// Property names must be the same in order to map values, types don't have to share all properties though.
        /// </summary>
        /// <typeparam name="T">The type to convert the object to.</typeparam>
        /// <param name="convertFrom">The object to be converted.</param>
        /// <returns>An instance of type T with mapped property values.</returns>
        public static T ConvertToType<T>(this object convertFrom) where T : new()
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
