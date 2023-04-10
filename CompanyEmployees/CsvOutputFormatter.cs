using Entities.Dtos;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace CompanyEmployees
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<object> enumerable)
            {
                foreach (var item in enumerable)
                {
                    FormatCsv(buffer, item);
                }
            }
            await response.WriteAsync(buffer.ToString());
        }

        private static void FormatCsv(StringBuilder buffer, object item)
        {
            string result = "";

            for (int i = 0; i < item.GetType().GetProperties().Length; i++)
            {
                PropertyInfo propertyInfo = item.GetType().GetProperties()[i];
                var value = propertyInfo.GetValue(item);
                result += $"{value}";

                // Only append a comma if this is not the last value in this row.
                if (i != item.GetType().GetProperties().Length - 1)
                {
                    result += ",";
                }
            }
            buffer.AppendLine(result);
        }
    }
}
