using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace SocioBoard.Helper
{
    public static class Extensions
    {
        public static DateTime ParseTwitterTime(string date)
        {
            const string format = "ddd MMM dd HH:mm:ss zzzz yyyy";
            return DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
        }

        public static DateTime ParseAnotherTwitterTime(string date)
        {
                const string format = "ddd, dd MMM yyyy HH:mm:ss zzzz";
                return DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
        }
    }
}