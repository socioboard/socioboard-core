using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GlobusGooglePlusLib.Authentication;

namespace GlobusGooglePlusLib.Youtube.Core
{
    public class Subscriptions
    {

        oAuthTokenYoutube objoAuthTokenYoutube = new oAuthTokenYoutube();



        /// <Get_Subscriptions_List>
        /// 
        /// </summary>
        /// <param name="accesstoken">true</param>
        /// <param name="part">snippet</param>
        /// <returns></returns>
        public string Get_Subscriptions_List(string accesstoken, string part)
        {
            string RequestUrl = "https://www.googleapis.com/youtube/v3/subscriptions?part=" + part + "&mine=true&maxResults=50&key=" + accesstoken;
            Uri path = new Uri(RequestUrl);
            string[] header = { "Authorization", "X-JavaScript-User-Agent" };
            string[] val = { "Bearer " + accesstoken, "Google APIs Explorer" };
            string response = string.Empty;
            try
            {
                response = objoAuthTokenYoutube.WebRequestHeader(path, header, val, GlobusGooglePlusLib.Authentication.oAuthToken.Method.GET.ToString());
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }

            return response;
        }

    }
}
