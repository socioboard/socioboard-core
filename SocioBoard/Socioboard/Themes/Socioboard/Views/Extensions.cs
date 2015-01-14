using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Socioboard.Themes.Socioboard.Views
{
    public static class Extensions
    {
        // Convert the passed datetime into client timezone.
        
        public static string ToClientTime(this DateTime dt)
        {
            var timeOffSet = HttpContext.Current.Session["timezoneoffset"];  // read the value from session

            if (timeOffSet != null)
            {
                var offset = int.Parse(timeOffSet.ToString());
                dt = dt.AddMinutes(-1 * offset);

                return dt.ToString();
            }

            // if there is no offset in session return the datetime in server timezone
            return dt.ToLocalTime().ToString();
        }
    }
}