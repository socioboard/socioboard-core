using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GlobusTwitterLib.Authentication;
using WooSuite.Helper;
using WooSuite.Model;

using GlobusTwitterLib.Twitter.Core.UserMethods;
using GlobusTwitterLib.App.Core;
using GlobusFacebookLib.App.Core;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Data;
using System.Xml.Linq;
namespace WooSuite.Discovery
{
    public partial class YourFollowers : System.Web.UI.Page
    {

        oAuthTwitter OAuth = new oAuthTwitter();
        FacebookWebRequest objWebRequest = new FacebookWebRequest();
        clsTwitterMethods objTwitterMethod = new clsTwitterMethods();
        TwitterRepository twtrepo = new TwitterRepository();
        TWitterAccount objTWitterAccount = new TWitterAccount();
        Users user = new Users();
        TwitterProcess twtprocess = new TwitterProcess();
        public string QueryStringPrfId = string.Empty;
        public static int custid = 0;
        static string custname = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            clsUserLoginInfo loginInfoEmail = (clsUserLoginInfo)Session["LoggedUser"];
            if (Session["LoggedUser"] != null)
            {
                custid = loginInfoEmail.Customer_Id;
                custname = loginInfoEmail.customer_name;
                if (!IsPostBack)
                {
                    bindtwitterAccount(custid);
                    followersdatabind(hdntwtaccountid.Value);
                }
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }

        }

        protected void followersdatabind(string twtaccId)
        {
            IEnumerable<dynamic> getdata = null;
            string followdata = "";
            getdata = twtrepo.GetTwtUserDetail(twtaccId, custid);
            foreach (var item in getdata)
            {
                string strtwtUri = "https://api.twitter.com/1/followers/ids.json?cursor=-1&screen_name=" + item.TwitterScreenName;
                JObject outputreg = objWebRequest.FacebookRequest(strtwtUri, "Get");

                foreach (var itemr in outputreg["ids"])
                {
                    DataSet dsProfile = new DataSet();
                    string strdata = "https://api.twitter.com/1/users/lookup.xml?user_id=" + itemr;
                    dsProfile.ReadXml(strdata);

                    string location = "";
                    string description = "";
                    string url = "";

                    DataTable dt = null;
                    DataTable dtst = null;
                    dt = (DataTable)dsProfile.Tables["user"];

                    if (!string.IsNullOrEmpty(dt.Rows[0]["location"].ToString()))
                        location = dt.Rows[0]["location"].ToString();
                    else
                        location = "Not Specified";

                    if (!string.IsNullOrEmpty(dt.Rows[0]["description"].ToString()))
                        description = dt.Rows[0]["description"].ToString();
                    else
                        description = "";

                    if (dt.Rows[0]["url"].ToString() != "")
                        url = dt.Rows[0]["url"].ToString();
                    else
                        url = "No Url";

                    followdata += "<div id=\"divfollowers_" + dt.Rows[0]["id"] + "\" class=\"messages\" style=\"display:block\">" +
                                "<section>" +
                                    "<aside> " +
                                        "<section class=\"js-avatar_tip\" data-sstip_class=\"twt_avatar_tip\">" +
                                            "<a class=\"avatar_link view_profile\">" +
                                                "<img width=\"54\" height=\"54\" border=\"0\" class=\"avatar\" src=\"" + dt.Rows[0]["profile_image_url"] + "\">" +
                                                "<span class=\"twitter_bm\"><img width=\"16\" height=\"16\" src=\"../Contents/Images/twticon.png\"></span></a>" +
                                        "</section>" +
                                        "<ul></ul>" +
                                     "</aside>" +
                                     "<div class=\"yf_article\">" +
                                        "<div class=\"yf_div\">" +
                                            "<div class=\"dd\">" +
                                                "<ul class=\"top_actions band\"> " +

                                               "</ul>" +
                                               "<h3 title=" + dt.Rows[0]["screen_name"] + " class=\"fn js-view_profile\">" +
                                                    "<span>" + dt.Rows[0]["name"] + "</span> " +
                                                   "<span class=\"screenname prof_meta\"></span>" +
                                               "</h3>" +
                                                 "<p class=\"note\">" + description + "</p>" +
                                                "<ul class=\"prof_meta\"> " +
                                                     "<li class=\"dim\"><span class=\"loc_pointer_sm\"><img src=\"../Contents/Images/location.png\" /></span>" + location + "</li>  " +
                                                     "<li class=\"dim\"><span class=\"loc_pointer_sm\"><img src=\"../Contents/Images/no_url.png\" /></span>" + url + "</li> " +
                                                "</ul>" +

                                               "<section class=\"profile_sub_wrap\">" +
                                                    "<ul class=\"follow\">" +
                                                     "<li><span class=\"followers\"><span>Followers</span> <span class=\"count\">" + dt.Rows[0]["followers_count"] + "</span></span></li>" +
                                                        "<li><span class=\"following\"><span>Following</span> <span class=\"count\"> 0 </span></span></li>" +
                                                    "</ul>" +
                                                    "<ul class=\"band_actions\">" +
                                                        "<li class=\"half\"><a id=" + dt.Rows[0]["id"] + " onclick=\"followersdivhide(this.id)\" class=\"yf_button subtle js-hide_suggestion\" >Hide</a></li>" +
                                                        "<li class=\"half\"><a class=\"yf_button subtle js-view_profile\" onclick=\"detailsprofile('" + dt.Rows[0]["screen_name"] + "')\" >Full Profile»</a></li>" +

                                                        "</ul>" +
                                              "</section>" +

                                            "</div>" +
                                        "</div>" +
                                      "</div>" +
                                    "</section>" +
                            "</div>";

                }

                divsuggestion.InnerHtml = followdata;
            }

        }
        protected void bindtwitterAccount(int custid)
        {
            string twtprofiles = string.Empty;
            List<TWitterAccount> twtacc = new List<TWitterAccount>();
            twtacc = twtrepo.GetAllTwitAccountbyCustId(custid);
            twtprofiles = "<ul style=\"width: 96%; background: none; border: none;\">";
            int i = 0;
            foreach (TWitterAccount item in twtacc)
            {
                i++;
                if (i == 1)
                {
                    twtprofiles += "<li>" +
                   "<input type=\"radio\" checked=\"checked\" name=\"followers\" " +
                   "id=\"" + item.TwtAccID + "_" + item.TwitterId + "_" + i + "\" onclick=\"PerformClick(this.id)\" /> " +
                   item.TwitterScreenName +
                    "</li>";
                    followersdatabind(item.TwitterId);
                    hdntwtaccountid.Value = item.TwitterId.ToString();
                }
                else
                {
                    twtprofiles += "<li>" +
                  "<input type=\"radio\" name=\"followers\" " +
                  "id=\"" + item.TwtAccID + "_" + item.TwitterId + "_" + i + "\" onclick=\"PerformClick(this.id)\" /> " +
                  item.TwitterScreenName +
                   "</li>";
                }

            }
            twtprofiles += "</ul>";
            divtwtprofiles.InnerHtml = twtprofiles;

        }

        protected void btn_Click(object sender, EventArgs e)
        {
            string twtaccountid = hdntwtaccountid.Value;
            string twtid = hdntwtid.Value;
            followersdatabind(twtaccountid);
        }
    }
}