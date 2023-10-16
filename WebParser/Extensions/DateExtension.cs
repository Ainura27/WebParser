using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace WebParser.Extensions
{
    public static class DateExtension
    {
        public static DateTime ToDate(this string value)
        {
            return DateTime.ParseExact(value, "dd MMMM yyyy HH:mm", CultureInfo.GetCultureInfo("ru-RU"));
        }
    }
}
