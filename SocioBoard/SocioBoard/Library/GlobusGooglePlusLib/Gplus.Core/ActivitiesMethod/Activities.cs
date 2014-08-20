using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using GlobusGooglePlusLib.Authentication;
using GlobusGooglePlusLib.App.Core;

namespace GlobusGooglePlusLib.Gplus.Core.ActivitiesMethod
{
    public class Activities
    {
        /// <summary>
        /// List all of the activities in the specified collection for a particular user.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        public JArray Get_Activities_List(string UserId, string access)
        {
            oAuthToken objToken = new oAuthToken();
            string RequestUrl = Globals.strGetActivitiesList + UserId + "/activities/public?access_token=" + access;
            Uri path = new Uri(RequestUrl);
            string[] header = { "token_type", "expires_in" };
            string[] val = { "Bearer", "3600" };
            string response = string.Empty;
            try
            {
                response = objToken.WebRequestHeader(path, header, val);
                if (!response.StartsWith("["))
                    response = "[" + response + "]";
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            
            return JArray.Parse(response);
        }

        /// <summary>
        /// Get an activity by Id.
        /// </summary>
        /// <param name="ActivityId"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        public JArray Get_Activity_By_Id(string ActivityId, string access)
        {
            oAuthToken objToken = new oAuthToken();
            string RequestUrl = Globals.strGetActivityById + ActivityId + "?access_token=" + access;
            Uri path = new Uri(RequestUrl);
            string[] header = { "token_type", "expires_in" };
            string[] val = { "Bearer", "3600" };
            string response = string.Empty;
            try
            {
                response = objToken.WebRequestHeader(path, header, val);
                if (!response.StartsWith("["))
                    response = "[" + response + "]";
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return JArray.Parse(response);
        }

        /// <summary>
        /// Search public activities.
        /// </summary>
        /// <param name="query">Full-text search query string. </param>
        /// <param name="access"></param>
        /// <returns></returns>
        public JArray Get_Activities_Search(string query, string access)
        {
            oAuthToken objToken = new oAuthToken();
            string RequestUrl = Globals.strGetSearchActivity + "?query="+ query +"access_token=" + access;
            Uri path = new Uri(RequestUrl);
            string[] header = { "token_type", "expires_in" };
            string[] val = { "Bearer", "3600" };
            string response = string.Empty;
            try
            {
                response = objToken.WebRequestHeader(path, header, val);
                if (!response.StartsWith("["))
                    response = "[" + response + "]";
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
         
            return JArray.Parse(response);
        }
    }
}
