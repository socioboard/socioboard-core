using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Helper
{
    public class SBUtils
    {
        public static bool IsUserWorkingDaysValid(DateTime registrationDate)
        {
            bool isUserWorkingDaysValid = false;
            try
            {
                TimeSpan span = DateTime.Now.Subtract(registrationDate);
                int totalDays = (int)span.TotalDays;

                if (totalDays < 30)
                {
                    isUserWorkingDaysValid = true;
                }
            }
            catch (Exception ex)
            {
                //logger.Error(ex.Message);

                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return isUserWorkingDaysValid;
        }

        public static int DaysBetween(DateTime d1, DateTime d2)
        {
            TimeSpan span = d2.Subtract(d1);
            return (int)span.TotalDays;
        }
    }
}