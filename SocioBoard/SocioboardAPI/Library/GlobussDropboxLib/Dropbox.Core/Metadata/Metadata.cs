using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobussDropboxLib.Dropbox.Core.Metadata
{
    public class Metadata
    {
        public static string GetDropBoxFolder(ref GlobussDropboxLib.Authentication.oAuthToken _oAuthToken, string AccessToken)
        {
            var uri = new Uri(new Uri(GlobussDropboxLib.App.Core.Global._BASIC_URI), String.Format("metadata/{0}/{1}", "dropbox", string.Empty));

            return _oAuthToken.APIWebRequest(GlobussDropboxLib.Authentication.oAuthToken.Method.GET.ToString(), uri + "?access_token=" + AccessToken, null);
        }

        public static string GetDropBoxFiles(ref GlobussDropboxLib.Authentication.oAuthToken _oAuthToken, string AccessToken, string root, string path)
        {
            //    var uri = new Uri(new Uri(GlobussDropboxLib.App.Core.Global._BASIC_URI),
            //String.Format("metadata/{0}/{1}", "dropbox", "MyFolder"));
            var uri = new Uri(new Uri(GlobussDropboxLib.App.Core.Global._BASIC_URI), String.Format("metadata/{0}/{1}", root, path));
            return _oAuthToken.APIWebRequest(GlobussDropboxLib.Authentication.oAuthToken.Method.GET.ToString(), uri + "?access_token=" + AccessToken, null);
        }

    }
}
