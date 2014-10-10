using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GlobusGooglePlusLib.Authentication;

namespace GlobusGooglePlusLib.Youtube.Core
{
    class Search
    {
        oAuthTokenYoutube objoAuthTokenYoutube = new oAuthTokenYoutube();
        //This methods for searching Video list by Searching keyword
        public string Get_Search_List(string query, string accesstoken, string part, int maxResults)
        {                   
            string RequestUrl = "https://www.googleapis.com/youtube/v3/search?part=" + part + "&maxResults=" + maxResults + "&q=" + query + "&key=" + accesstoken;
            Uri path = new Uri(RequestUrl);
            string[] header = { "Authorization", "X-JavaScript-User-Agent" };
            string[] val = { "Bearer " + accesstoken, "Google APIs Explorer" };
            string response = string.Empty;
            try
            {
                response = objoAuthTokenYoutube.WebRequestHeader(path, header, val, GlobusGooglePlusLib.Authentication.oAuthToken.Method.GET.ToString());
                //if (!response.StartsWith("["))
                //    response = "[" + response + "]";
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }

            return response;
        }

        



    }
}
