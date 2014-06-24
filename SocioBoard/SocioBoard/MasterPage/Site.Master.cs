using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using GlobusLinkedinLib.Authentication;
using SocioBoard.Helper;
using GlobusInstagramLib.Authentication;
using System.Configuration;

namespace SocioBoard.MasterPage
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
       
            if (!IsPostBack)
            {
                if (Session["LoggedUser"] == null)
                    Response.Redirect("/Login.aspx");

                User user = (User)Session["LoggedUser"];
                username.Text = user.UserName;

            }
        }

        public void AuthenticateTwitter(object sender, EventArgs e)
        {
            TwitterHelper twthelper = new TwitterHelper();
            string twtredirecturl = twthelper.TwitterRedirect(ConfigurationManager.AppSettings["consumerKey"], ConfigurationManager.AppSettings["consumerSecret"], ConfigurationManager.AppSettings["callbackurl"]);
            Response.Redirect(twtredirecturl);

        }
        public void AuthenticateLinkedin(object sender, EventArgs e)
        {
            oAuthLinkedIn Linkedin_oauth = new oAuthLinkedIn();
            string authLink = Linkedin_oauth.AuthorizationLinkGet();
            Session["reuqestToken"] = Linkedin_oauth.Token;
            Session["reuqestTokenSecret"] = Linkedin_oauth.TokenSecret;

            
            Response.Redirect(authLink);
        }

        public void AuthenticateInstagram(object sender, EventArgs e)
        {
            GlobusInstagramLib.Authentication.ConfigurationIns config = new GlobusInstagramLib.Authentication.ConfigurationIns("https://instagram.com/oauth/authorize/", ConfigurationManager.AppSettings["InstagramClientKey"].ToString(), ConfigurationManager.AppSettings["InstagramClientSec"].ToString(), ConfigurationManager.AppSettings["InstagramCallBackURL"].ToString(), "https://api.instagram.com/oauth/access_token", "https://api.instagram.com/v1/", "");

            oAuthInstagram _api = oAuthInstagram.GetInstance(config);

           
            Response.Redirect(_api.AuthGetUrl("likes+comments+basic+relationships"));
        }
        public void AuthenticateFacebook(object sender, EventArgs e)
        {


            Response.Redirect("http://www.facebook.com/dialog/oauth/?scope=publish_stream,read_stream,read_insights,manage_pages,user_checkins,user_photos,read_mailbox,manage_notifications,read_page_mailboxes,email,user_videos,offline_access&client_id=" + ConfigurationManager.AppSettings["ClientId"] + "&redirect_uri=" + ConfigurationManager.AppSettings["RedirectUrl"] + "&response_type=code");

        }

    }
}