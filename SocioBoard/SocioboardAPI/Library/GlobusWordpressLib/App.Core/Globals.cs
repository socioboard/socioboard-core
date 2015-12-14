using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobusWordpressLib.App.Core
{
    public class Globals
    {
        public static string _TokenUrl = "https://public-api.wordpress.com/oauth2/token";

        public static string _UserInfo = "https://public-api.wordpress.com/rest/v1/me/?pretty=1";

        public static string _Usersofblog = "https://public-api.wordpress.com/rest/v1/sites/[SiteId]/users/";

        public static string _UserSites = "https://public-api.wordpress.com/rest/v1/me/sites";

        public static string _UserPosts = "https://public-api.wordpress.com/rest/v1/sites/[SiteId]/posts/?number=10&pretty=1";

        public static string _PostBlog = "https://public-api.wordpress.com/rest/v1/sites/[SiteId]/posts/new/";

    }
}
