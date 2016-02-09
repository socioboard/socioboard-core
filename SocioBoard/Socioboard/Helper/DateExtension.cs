using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Socioboard.Helper
{

    public static class DateExtension
    {
        public static long ToUnixTimestamp(this DateTime target)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
            var unixTimestamp = System.Convert.ToInt64((target - date).TotalSeconds);

            return unixTimestamp;
        }

        public static DateTime ToDateTime(this DateTime target, long timestamp)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);

            return dateTime.AddSeconds(timestamp);
        }
        public static string CalculateTimeDiff(this DateTime date)
        {
            string ret_string = string.Empty;
            TimeSpan diff = DateTime.UtcNow.Subtract(date);
            double date1 = diff.TotalDays;
            double date2 = diff.TotalHours;
            double date3 = diff.TotalMinutes;
            double date4 = ((date.Year - DateTime.UtcNow.Year) * 12) + date.Month - DateTime.UtcNow.Month;
            double date5 = date.Year - DateTime.UtcNow.Year;

            int _date1 = (int)date1;
            int _date2 = (int)date2;
            int _date3 = (int)date3;
            int _date4 = (int)date4;
            int _date5 = (int)date5;

            if (date5 >= 1)
            {
                if (_date5 > 1)
                {
                    ret_string = _date5.ToString() + " Years ago";
                }
                else {
                    ret_string = _date5.ToString() + " Year ago";
                }
            }
            else if (date4 >= 1)
            {
                if (_date4 > 1)
                {
                    ret_string = _date4.ToString() + " Months ago";
                }
                else
                {
                    ret_string = _date4.ToString() + " Month ago";
                }
            }
            else if (date1 >= 1)
            {
                ret_string = _date1.ToString() + " Days ago";

                if (date1 >= 7)
                {
                    int weeks = _date1 / 7;
                    if (weeks > 1)
                    {
                        ret_string = weeks.ToString() + " Weeks ago";
                    }
                    else {
                        ret_string = weeks.ToString() + " Week ago";
                    }
                }
            }
            else
            {
                if (date2 >= 1)
                {
                    if (_date2 > 1)
                    {
                        ret_string = _date2.ToString() + " Hours ago";
                    }
                    else {
                        ret_string = _date2.ToString() + " Hour ago";
                    }
                }
                else if (date3 >= 1)
                {
                    if (_date3 > 1)
                    {
                        ret_string = _date3.ToString() + " Minute ago";
                    }
                    else {
                        ret_string = _date3.ToString() + " Minut ago";
                    }
                }
                else {
                    ret_string = " Just now";
                }
            }

            return ret_string;
        }

        public static DateTime ParseTwitterTime(this string date)
        {
            const string format = "ddd MMM dd HH:mm:ss zzzz yyyy";
            return DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
        }

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
