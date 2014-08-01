using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobusTumblerLib.App.Core
{
    public static class Globals
    {
        #region
        public static string mainurl = "http://api.tumblr.com/v2/";
        #endregion

        #region Search API Methods
        public static string RetrieveBlogInfoUrl = "http://api.tumblr.com/v2/blog/";
        #endregion


        #region User's Dashboard
        public static string UsersInfoUrl = "https://api.tumblr.com/v2/user/info";
        public static string UsersDashboardUrl = "http://api.tumblr.com/v2/user/dashboard";
        public static string UsersBlogUrl = "http://api.tumblr.com/v2/blog/";
        public static string UsersLikesdUrl = "http://api.tumblr.com/v2/user/likes";
        #endregion
    }
}
