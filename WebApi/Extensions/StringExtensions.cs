using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace WebApi.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Convert a UTC Date String of format yyyyMMddThhmmZ into a Local Date
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeFromFormat(this string dateString)
        {
            Regex r = new Regex(@"^\d{4}\d{2}\d{2}T\d{2}\d{2}Z$");
            if (!r.IsMatch(dateString) || dateString == null)
            {
                //throw new FormatException(
                //    string.Format("{0} is not the correct format. Should be yyyyMMddThhmmZ", dateString));
                return DateTime.Now;
            }

            return DateTime.ParseExact(dateString, "yyyyMMddThhmmZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
        }
    }
}
