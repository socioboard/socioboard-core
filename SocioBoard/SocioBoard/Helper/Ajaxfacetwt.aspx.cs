using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using SocioBoard.Domain;

namespace SocioBoard.Helper
{
    public partial class Ajaxfacetwt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //string redi = "http://www.facebook.com/dialog/oauth/?scope=publish_stream,read_stream,read_insights,manage_pages,user_checkins,user_photos,read_mailbox,manage_notifications,read_page_mailboxes,email,user_videos,offline_access&client_id=" + ConfigurationManager.AppSettings["ClientId"] + "&redirect_uri=" + ConfigurationManager.AppSettings["RedirectUrl"] + "&response_type=code";
                //Session["login"] = "facebook";
                //Response.Redirect(redi,true);

                SocioBoard.Domain.User user = (User)Session["LoggedUser"];

                if (Request.QueryString["type"] != null)
                {
                    if (Request.QueryString["type"] == "fb")
                    {
                        facebook(Request.QueryString["msg"]);
                    //   Response.Write("success");
                    }
                    if (Request.QueryString["type"] == "twt")
                    {
                        twitter(Request.QueryString["msg"]);
                       // Response.Write("success");
                    }

                    if (Request.QueryString["type"] == "getuserid")
                    {
                        string sitename = ConfigurationManager.AppSettings["MailSenderDomain"];
                        Response.Write("success<:>" + sitename + "Registration.aspx?refid=" + user.Id);
                    }
                    

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);   
            }
        }
        public void facebook(string msg)
        {
            try
            {
                Session["facemsg"] = msg;
                SessionFactory.configfilepath = Server.MapPath("~/hibernate.cfg.xml");
                string redi = ConfigurationManager.AppSettings["facebookurl"] + "&client_id=" + ConfigurationManager.AppSettings["ClientId"] + "&redirect_uri=" + ConfigurationManager.AppSettings["RedirectUrl"] + "&response_type=code";
                Session["login"] = "facebook";
                Response.Write(redi);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void twitter(string msg)
        {
            try
            {
                Session["twittermsg"] = msg;
                SessionFactory.configfilepath = Server.MapPath("~/hibernate.cfg.xml");
                TwitterHelper twthelper = new TwitterHelper();
                string twtredirecturl = twthelper.TwitterRedirect(ConfigurationManager.AppSettings["consumerKey"], ConfigurationManager.AppSettings["consumerSecret"], ConfigurationManager.AppSettings["callbackurl"]);
                Response.Redirect(twtredirecturl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

    }
}