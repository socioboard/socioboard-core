using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GlobusGooglePlusLib.Authentication;

namespace GlobusGooglePlusLib.Youtube.Core
{
    class Playlists
    {
        oAuthTokenYoutube _oAuthTokenYoutube = new oAuthTokenYoutube();
        // include in the parameter value are snippet and status.
        public void Insert_Playlist(string accesstoken, string part, string playlistId, string videoId, string kind, string title, string description)
        {
            string _RequestUrl = "https://www.googleapis.com/youtube/v3/activities?part=" + part + "&key=" + accesstoken;
            Uri path = new Uri(_RequestUrl);
            string[] _header = { "Authorization", "X-JavaScript-User-Agent" };
            string[] _val = { "Bearer " + accesstoken, "Google APIs Explorer" };
            string response = string.Empty;

            String _PostData = "{\"snippet\": {\"title\": \"" + title + "\",\"description\": \"" + description + "\"},\"status\": {\"privacyStatus\": \"public\"}}";

            try
            {
                response = _oAuthTokenYoutube.Post_WebRequest(oAuthToken.Method.POST, _RequestUrl, _PostData, _header, _val);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }


        }



    }
}
