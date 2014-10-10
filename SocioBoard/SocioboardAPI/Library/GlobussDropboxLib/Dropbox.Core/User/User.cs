using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobussDropboxLib.Dropbox.Core.User
{
    public class User
    {
        public static string GetUserInfo(ref GlobussDropboxLib.Authentication.oAuthToken _oAuthToken, string AccessToken)
        {
            return _oAuthToken.APIWebRequest(GlobussDropboxLib.Authentication.oAuthToken.Method.GET.ToString(), GlobussDropboxLib.App.Core.Global._USER_INFO + "?access_token=" + AccessToken, null);
        }
    }
}
