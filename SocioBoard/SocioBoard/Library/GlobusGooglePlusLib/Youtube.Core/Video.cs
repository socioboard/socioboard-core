using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GlobusGooglePlusLib.Authentication;

namespace GlobusGooglePlusLib.Youtube.Core
{
    public class Video
    {
        oAuthTokenYoutube objoAuthTokenYoutube = new oAuthTokenYoutube();

        public string Get_Video_List(string query, string accesstoken, string part, int maxResults)
        {
            string RequestUrl = "https://www.googleapis.com/youtube/v3/videos?part=" + part + "&chart=mostpopular&maxResults=" + maxResults + "&key=" + accesstoken;
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

        public string Get_VideoCategories(string accesstoken, string part)
        {
            //https://www.googleapis.com/youtube/v3/videoCategories?part=snippet&hl=en_US&regionCode=IN&key={YOUR_API_KEY}
            string RequestUrl = "https://www.googleapis.com/youtube/v3/videoCategories?part=" + part + "&hl=en_US&regionCode=IN&key=" + accesstoken;
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

        public string Get_VideoRating(string accesstoken, string videoId)
        {
            string RequestUrl = "https://www.googleapis.com/youtube/v3/videos?id=" + videoId + "&key=" + accesstoken + "&part=snippet,statistics&fields=items(id,snippet,statistics)";
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

        public string Get_VideoDetails_byId(string Videoid, string accesstoken, string part)
        {
            string RequestUrl = "https://www.googleapis.com/youtube/v3/videos?part=" + part + "&id=" + Videoid + "&key=" + accesstoken;
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
