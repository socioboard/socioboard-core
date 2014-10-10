using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobussDropboxLib.Dropbox.Core.Media
{
    public class Media
    {
        public static string GetDropBoxDirectlink (ref GlobussDropboxLib.Authentication.oAuthToken _oAuthToken, string AccessToken, string path)
        {
            //    var uri = new Uri(new Uri(GlobussDropboxLib.App.Core.Global._BASIC_URI),
            //String.Format("metadata/{0}/{1}", "dropbox", "MyFolder"));
            var uri = new Uri(new Uri(GlobussDropboxLib.App.Core.Global._BASIC_URI), String.Format("media/auto/{0}", path));
            return _oAuthToken.APIWebRequest(GlobussDropboxLib.Authentication.oAuthToken.Method.GET.ToString(), uri + "?access_token=" + AccessToken, null);
        }
    }
}
