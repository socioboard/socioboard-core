using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using GlobusGooglePlusLib.Gplus.Core.ActivitiesMethod;

namespace GlobusGooglePlusLib.App.Core
{
    public class ActivitiesController
    {
        JArray objArr;
        public ActivitiesController()
        {
            objArr = new JArray();
        }

         /// <summary>
        /// List all of the activities in the specified collection for a particular user.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        public JArray GetActivitiesList(string UserId, string access)
        {
            Activities objActivity = new Activities();
            try
            {
                objArr = objActivity.Get_Activities_List(UserId, access);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return objArr;
        }

         /// <summary>
        /// Get an activity by Id.
        /// </summary>
        /// <param name="ActivityId"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        public JArray GetActivityById(string ActivityId, string access)
        {
            Activities objActivity = new Activities();
            try
            {
                objArr = objActivity.Get_Activity_By_Id(ActivityId, access);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return objArr;
        }

         /// <summary>
        /// Search public activities.
        /// </summary>
        /// <param name="query">Full-text search query string. </param>
        /// <param name="access"></param>
        /// <returns></returns>
        public JArray GetActivitiesSearch(string query, string access)
        {
            Activities objActivity = new Activities();
            try
            {
                objArr = objActivity.Get_Activities_Search(query, access);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return objArr;
        }
    }
}
