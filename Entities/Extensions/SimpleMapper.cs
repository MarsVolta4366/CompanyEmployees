using Entities.Models;
using System.Reflection;

namespace Entities.Extensions
{
    public static class SimpleMapper
    {
        /// <summary>
        /// Extension method that dynamically maps objects of a type in an IEnumerable to an IEnumerable of a different type T.
        /// </summary>
        /// <typeparam name="T">The type to convert the objects to.</typeparam>
        /// <param name="list">The list being converted.</param>
        /// <returns>A list of type T with values mapped to matching property names.</returns>
        public static IEnumerable<T> MapIEnumerable<T>(this IEnumerable<object> list) where T : new()
        {
            return list.Select(x => x.Map<T>());
        }

        /// <summary>
        /// Extension method that dynamically converts from type of this to type T.
        /// Property names must be the same in order to map values, types don't have to share all properties though.
        /// </summary>
        /// <typeparam name="T">The type to convert the object to.</typeparam>
        /// <param name="convertFrom">The object to be converted.</param>
        /// <returns>An instance of type T with mapped property values.</returns>
        public static T Map<T>(this object convertFrom) where T : new()
        {
            T convertTo = new();

            foreach (PropertyInfo propertyInfo in convertFrom.GetType().GetProperties())
            {
                var property = convertFrom.GetType().GetProperty(propertyInfo.Name);
                var value = property?.GetValue(convertFrom);

                var propToSet = convertTo.GetType().GetProperty(propertyInfo.Name);

                if (typeof(IEnumerable<object>).IsAssignableFrom(property?.PropertyType))
                {
                    throw new Exception("Simple Mapper can't handle this yet.");
                    //IEnumerable<object>? items = value as IEnumerable<object>;
                    //if (items is not null)
                    //{

                    //}

                }
                else if (propToSet is not null)
                {
                    propToSet.SetValue(convertTo, value);
                }
            }

            return convertTo;
        }
    }
}
