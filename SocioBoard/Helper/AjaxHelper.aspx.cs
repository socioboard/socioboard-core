using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SocioBoard.Domain;
using SocioBoard.Model;
using SocioBoard;
using SocialSuitePro.Feeds;
using SocioBoard.Message;
using System.Collections;
using GlobusTwitterLib.Twitter.Core.UserMethods;
using GlobusTwitterLib.Authentication;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Facebook;
using System.Net;
using System.IO;
using System.Text;
using log4net;
using SocialSuitePro;




namespace SocioBoard.Helper
{
    public partial class AjaxHelper : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(AjaxHelper));
        string lnk = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ProcessRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
            }
        }

        void ProcessRequest()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["op"]))
            {
                if (Request.QueryString["op"] == "removedata")
                {

                    string network = Request.QueryString["network"];
                    string message = string.Empty;
                    var users = Request.QueryString["data[]"];
                    SocioBoard.Domain.Messages mstable = new SocioBoard.Domain.Messages();
                    DataSet ds = DataTableGenerator.CreateDataSetForTable(mstable);
                    DataTable dtt = ds.Tables[0];
                    string page = Request.QueryString["page"];
                    if (page == "feed")
                    {
                        AjaxFeeds ajxfed = new AjaxFeeds();
                        DataTable dt = null;
                        if (network == "facebook")
                        {
                            dt = (DataTable)Session["FacebookFeedDataTable"];
                        }
                        else if (network == "twitter")
                        {
                            dt = (DataTable)Session["TwitterFeedDataTable"];
                        }
                        else if (network == "linkedin")
                        {
                            dt = (DataTable)Session["LinkedInFeedDataTable"];
                        }

                        foreach (var parent in users)
                        {
                            DataView dv = new DataView(dtt);
                            DataRow[] foundRows = dt.Select("ProfileId = '" + parent + "'");
                            foreach (var child in foundRows)
                            {
                                dtt.ImportRow(child);
                            }
                        }
                        message = ajxfed.BindData(dtt);

                    }

                    else if (page == "message")
                    {
                        AjaxMessage ajxmes = new AjaxMessage();
                        DataSet dss = (DataSet)Session["MessageDataTable"];
                        //foreach (var parent in users)
                        //{
                        DataView dv = new DataView(dtt);
                        DataRow[] foundRows = dss.Tables[0].Select("ProfileId = '" + users + "'");
                        foreach (var child in foundRows)
                        {
                            dtt.ImportRow(child);
                        }

                        //}
                        message = ajxmes.BindData(dtt);
                    }
                    Response.Write(message);
                }
                else if (Request.QueryString["op"] == "saveRss")
                {
                    try
                    {
                        User user = (User)Session["LoggedUser"];
                        RssFeedsRepository objRssFeedRepo = new RssFeedsRepository();
                        RssFeeds objRssFeeds = new RssFeeds();
                        objRssFeeds.ProfileScreenName = Request.QueryString["user"];
                        objRssFeeds.FeedUrl = Request.QueryString["feedsurl"];
                        objRssFeeds.UserId = user.Id;
                        objRssFeeds.Status = false;
                        objRssFeeds.Message = Request.QueryString["message"];
                        objRssFeeds.Duration = Request.QueryString["duration"];
                        objRssFeeds.CreatedDate = DateTime.Now;
                        objRssFeedRepo.AddRssFeed(objRssFeeds);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        Console.WriteLine(ex.Message);
                    }
                }

                else if (Request.QueryString["op"] == "savedrafts")
                {
                    Guid Id = Guid.Parse(Request.QueryString["id"]);
                    string newstr = Request.QueryString["newstr"];
                    DraftsRepository draftsRepo = new DraftsRepository();
                    draftsRepo.UpdateDrafts(Id, newstr);

                }


                else if (Request.QueryString["op"] == "chkrssurl")
                {
                    try
                    {
                        string url = Request.QueryString["url"];
                        var facerequest = (HttpWebRequest)WebRequest.Create(url);
                        facerequest.Method = "GET";
                        string outputface = string.Empty;
                        using (var response = facerequest.GetResponse())
                        {
                            using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                            {
                                outputface = stream.ReadToEnd();
                                if (outputface.Contains("<rss version=\"2.0\""))
                                {
                                    Response.Write("true");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        Console.WriteLine(ex.Message);
                        Response.Write("Error");
                    }

                }




                else if (Request.QueryString["op"] == "detailsdiscoveryfacebook")
                {
                    User user = (User)Session["LoggedUser"];
                    string userid = Request.QueryString["profileid"];
                    FacebookAccountRepository fbRepo = new FacebookAccountRepository();
                    ArrayList alst = fbRepo.getAllFacebookAccountsOfUser(user.Id);
                    string accesstoken = string.Empty;


                    foreach (FacebookAccount childnoe in alst)
                    {
                        try
                        {

                            //accesstoken = childnoe.AccessToken;
                            if (CheckFacebookTokenByUserId(childnoe.AccessToken.ToString(), userid))
                            {
                                accesstoken = childnoe.AccessToken;
                                break;
                            }
                            //break;
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex.Message);
                        }
                    }

                    FacebookClient fbclient = new FacebookClient(accesstoken);
                    string jstring = string.Empty;
                    dynamic item = fbclient.Get(userid);


                    jstring += "<div class=\"modal-small draggable\">";
                    jstring += "<div class=\"modal-content\">";
                    jstring += "<button class=\"modal-btn button b-close\" type=\"button\">";
                    jstring += "<span class=\"icon close-medium\"><span class=\"visuallyhidden\">X</span></span></button>";
                    jstring += "<div class=\"modal-header\"><h3 class=\"modal-title\">Profile summary</h3></div>";
                    jstring += "<div class=\"modal-body profile-modal\">";
                    jstring += "<div class=\"module profile-card component profile-header\">";

                    try
                    {
                        jstring += "<div class=\"profile-header-inner flex-module clearfix\" style=\"background-image: url('" + item["cover"]["source"] + "');\">";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        jstring += "<div class=\"profile-header-inner flex-module clearfix\" style=\"background-image: url('https://pbs.twimg.com/profile_banners/215936249/1371721359');\">";
                    }
                    jstring += "<div class=\"profile-header-inner-overlay\"></div>";
                    try
                    {
                        jstring += "<a class=\"profile-picture media-thumbnail js-nav\" href=\"#\"><img class=\"avatar size73\" alt=\"\" src=\"http://graph.facebook.com/" + item["id"] + "/picture?type=small\" /></a>";
                    }
                    catch (Exception)
                    {

                        jstring += "<a class=\"profile-picture media-thumbnail js-nav\" href=\"#\"><img class=\"avatar size73\" alt=\"\" src=\"http://graph.facebook.com/picture?type=small\" /></a>";
                    }
                    jstring += "<div class=\"profile-card-inner\">";
                    jstring += "<h1 class=\"fullname editable-group\">";
                    try
                    {
                        jstring += "<a href=\"#\" class=\"js-nav\">" + item["name"] + "</a>";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    jstring += "<a class=\"verified-link js-tooltip\" href=\"#\"><span class=\"icon verified verified-large-border\"><span class=\"visuallyhidden\"></span> </span></a>";
                    jstring += "</h1>";
                    try
                    {
                        jstring += "<h2 class=\"username\"><a href=\"#\" class=\"pretty-link js-nav\"><span class=\"screen-name\"><s>@</s>" + item["username"] + "</span> </a></h2>";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    jstring += "<div class=\"bio-container editable-group\"><p class=\"bio profile-field\">";
                    try
                    {
                        jstring += item["about"];
                    }
                    catch (Exception ex) { logger.Error(ex.Message); }

                    jstring += "</p></div>";
                    jstring += "<p class=\"location-and-url\">";
                    jstring += "<span class=\"location-container editable-group\"><span class=\"location profile-field\"></span></span>";
                    jstring += "<span class=\"divider hidden\"></span> ";
                    jstring += "<span class=\"url editable-group\">  <span class=\"profile-field\"><a title=\"#\" href=\"http://www.facebook.com/" + item["id"] + "\" rel=\"me nofollow\"  </a>";
                    jstring += "<div style=\"cursor: pointer; width: 16px; height: 16px; display: inline-block;\">&nbsp;</div>";
                    jstring += "</span></span></p></div></div>";
                    jstring += "<div class=\"clearfix\">";
                    jstring += "<div class=\"default-footer\">";

                    jstring += "<div class=\"btn-group\">" +
                                  "<div class=\"follow_button\">" +

                                      //"<span class=\"button-text following-text\">Following</span>" +
                        //"<span class=\"button-text unfollow-text\">Unfollow</span>" +
                                  "</div>" +
                               "</div>";
                    jstring += "</div></div>";
                    jstring += "<div class=\"profile-social-proof\"><div class=\"follow-bar\"></div></div></div>";
                    jstring += "<ol class=\"recent-tweets\">" +
                                  "<li class=\"\">" +
                                      "<div>" +
                                        "<i class=\"dogear\"></i>" +

                                      "</div>" +
                                  "</li>" +
                              "</ol>" +
                              "<div class=\"go_to_profile\">" +
                                  "<small><a href=\"http://www.facebook.com/" + item["id"] + "\" target=\"_blank\" class=\"view_profile\">Go to full profile →</a></small>" +
                              "</div>" +
                          "</div>" +
                          "<div class=\"loading\">" +
                              "<span class=\"spinner-bigger\"></span>" +
                          "</div>" +
                      "</div>";
                    jstring += "</div>";



                    Response.Write(jstring);

                }









                else if (Request.QueryString["op"] == "rssusers")
                {
                    try
                    {
                        User user = (User)Session["LoggedUser"];
                        TwitterAccountRepository twtAccRepo = new TwitterAccountRepository();
                        ArrayList alst = twtAccRepo.getAllTwitterAccountsOfUser(user.Id);
                        string message = string.Empty;
                        foreach (TwitterAccount item in alst)
                        {
                            message += "<option value=\"" + item.TwitterScreenName + "\">@" + item.TwitterScreenName + "</option>";
                        }
                        Response.Write(message);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        Console.WriteLine(ex.Message);
                    }


                }
                else if (Request.QueryString["op"] == "searchkeyword")
                {
                    User user = (User)Session["LoggedUser"];
                    DiscoverySearchRepository disrepo = new DiscoverySearchRepository();
                    List<string> alst = disrepo.getAllSearchKeywords(user.Id);
                    string message = string.Empty;

                    foreach (var item in alst)
                    {
                        message += "<li onclick=\"getSearchResults('" + item + "');\"><a href=\"#\"><i class=\"show icon-caret-right\" style=\"visibility:visible;margin-right:5px\"></i>" + item + "</a></li>";
                    }
                    Response.Write(message);

                }
                else if (Request.QueryString["op"] == "getResults")
                {
                    string type = Request.QueryString["type"];
                    string key = Request.QueryString["keyword"];
                    Discovery discoverypage = new Discovery();
                    string search = discoverypage.getlistresults(key);
                    string message = "<ul id=\"message-list\">" + search + "</ul>";
                    Response.Write(message);
                }
                else if (Request.QueryString["op"] == "getFollowers")
                {
                    User user = (User)Session["LoggedUser"];
                    Users twtUser = new Users();
                    oAuthTwitter oauth = new oAuthTwitter();
                    TwitterAccountRepository TwtAccRepo = new TwitterAccountRepository();
                    TwitterAccount TwtAccount = TwtAccRepo.getUserInformation(user.Id, Request.QueryString["id"]);
                    oauth.AccessToken = TwtAccount.OAuthToken;
                    oauth.AccessTokenSecret = TwtAccount.OAuthSecret;
                    oauth.ConsumerKey = ConfigurationManager.AppSettings["consumerKey"];
                    oauth.ConsumerKeySecret = ConfigurationManager.AppSettings["consumerSecret"];
                    oauth.TwitterScreenName = TwtAccount.TwitterScreenName;
                    oauth.TwitterUserId = TwtAccount.TwitterUserId;

                    JArray response = twtUser.Get_Followers_ById(oauth, Request.QueryString["id"]);

                    string jquery = string.Empty;

                    foreach (var item in response)
                    {
                        if (item["ids"] != null)
                        {
                            foreach (var child in item["ids"])
                            {
                                JArray userprofile = twtUser.Get_Users_LookUp(oauth, child.ToString());
                                foreach (var items in userprofile)
                                {

                                    try
                                    {

                                        jquery += "<li class=\"shadower\">" +
                                              "<div class=\"disco-feeds\">" +
                                                  "<div class=\"star-ribbon\"></div>" +
                                                  "<div class=\"disco-feeds-img\">" +
                                                      "<img alt=\"\" src=\"" + items["profile_image_url"] + "\" style=\"height: 100px; width: 100px;\" class=\"pull-left\">" +
                                                  "</div>" +
                                                  "<div class=\"disco-feeds-content\">" +
                                                      "<div class=\"disco-feeds-title\">" +
                                                          "<h3 class=\"no-margin\">" + items["name"] + "</h3>" +
                                                          "<span>@" + items["screen_name"] + "</span>" +
                                                      "</div>" +
                                                      "<p>";

                                        try
                                        {
                                            jquery += items["status"]["text"];
                                        }
                                        catch (Exception ex)
                                        {
                                            logger.Error(ex.Message);
                                        }

                                        lnk = "http://www.twitter.com/" + items["screen_name"].ToString();
                                        jquery += "</p>" +
                                            //"<a href=\"#\" class=\"btn\">Hide</a>" +
                                            //"<a href=\"#\" onclick=\"detailsprofile('" + items["id_str"] + "')\" class=\"btn\">Full Profile <i class=\"icon-caret-right\"></i> </a><div class=\"scl\">" +
                                           "<a href=" + lnk + "  class=\"btn\" target=\"_blank\" rel=\"me nofollow\">Full Profile <i class=\"icon-caret-right\"></i> </a><div class=\"scl\">" +
                                          "<a href=\"#\"><img alt=\"\" src=\"../Contents/img/admin/usergrey.png\"></a>" +
                                          "<a href=\"#\"><img alt=\"\" src=\"../Contents/img/admin/goto.png\"></a>" +
                                          "<a href=\"#\"><img alt=\"\" src=\"../Contents/img/admin/setting.png\"></a>" +
                                      "</div></div></div>" +
                                  "<div class=\"disco-feeds-info\">" +
                                      "<ul class=\"no-margin\">" +
                                          "<li><a href=\"#\"><img src=\"../Contents/img/admin/markerbtn2.png\" alt=\"\">";

                                        if (!string.IsNullOrEmpty(items["time_zone"].ToString()))
                                        {
                                            jquery += items["time_zone"];
                                        }
                                        else
                                        {
                                            jquery += "Not Specific";
                                        }
                                        jquery += "</a></li>";

                                        if (string.IsNullOrEmpty(items["url"].ToString()))
                                        {
                                            jquery += "<li><a href=\"#\"><img src=\"../Contents/img/admin/url.png\" alt=\"\">";
                                            jquery += "Not Specific";
                                        }
                                        else
                                        {
                                            jquery += "<li><a target=\"_blank\" href=\"" + items["url"] + "\"><img src=\"../Contents/img/admin/url.png\" alt=\"\">";
                                            jquery += items["url"];
                                        }
                                        jquery += "</a></li></ul>" +
                                        "<ul class=\"no-margin\" style=\"margin-top:20px\">" +
                                            "<li><a href=\"#\"><img src=\"../Contents/img/admin/twittericon-white.png\" alt=\"\">Followers <big><b>" + items["followers_count"] + "</b></big></a></li>" +
                                            "<li><a href=\"#\"><img src=\"../Contents/img/admin/twitter-white.png\" alt=\"\">Following <big><b>" + items["friends_count"] + "</b></big></a></li>" +
                                            "</ul>" +
                                    "</div>" +
                                "</li>";

                                        #region old
                                        //            jquery += "<div class=\"wentbg\">" +
                                        //                          "<div class=\"over\">" +
                                        //                            "<div class=\"topicon\">" +
                                        //                //"<a href=\"#\"><img border=\"none\" alt=\"\" src=\"../Contents/img/manplus.png\"></a>" +
                                        //                //"<a href=\"#\"><img border=\"none\" alt=\"\" src=\"../Contents/img/replay.png\"></a>" +
                                        //                //"<a href=\"#\"><img border=\"none\" alt=\"\" src=\"../Contents/img/setting.png\"></a>" +
                                        //                            "</div>" +
                                        //                                  "<div class=\"botombtn\">" +
                                        //                              "<div class=\"clickbtn\"><a href=\"#\"><img border=\"none\" alt=\"\" src=\"../Contents/img/full_profile.png\" onclick=\"detailsprofile('" + items["id_str"] + "')\"></a></div>" +
                                        //                            "</div>" +
                                        //                            "</div>" +
                                        //                                   "<div class=\"wentbgf\"><img alt=\"\" src=\"" + items["profile_image_url"] + "\"></div>" +

                                        //                                            "<div class=\"wentbgtext\">" +
                                        //"<span class=\"heading\">\"" + items["name"] + "\"</span> <span>@\"" + items["screen_name"] + "\"</span>" +
                                        //"<div class=\"viegil\">\"" + items["status"]["text"] + "\"</div>" +


                                        //            "<div class=\"avenue\">" +
                                        //             "<img alt=\"\" src=\"../Contents/img/avenue.png\">" +
                                        //             "<div class=\"avenuetext\">\"" + items["time_zone"] + "\"</div>" +
                                        //              "<img class=\"link\" alt=\"\" src=\"../Contents/img/url.png\">" +
                                        //             "<div class=\"nourl\">No URL</div>" +
                                        //         "</div>";

                                        //            jquery += "<div class=\"followerbg\">" +
                                        //                     "<div class=\"follower\">Followers <span>\"" + items["followers_count"] + "\"</span></div>" +
                                        //                     "<div class=\"following\">Friends <span>\"" + items["friends_count"] + "\"</span></div>" +
                                        //                  "</div>" +
                                        //              "</div>" +
                                        //          "</div>"; 
                                        #endregion
                                    }
                                    catch (Exception ex)
                                    {
                                        logger.Error(ex.Message);
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                            }
                        }
                        else
                        {
                            jquery += "None of the User Is Following";
                        }

                    }




                    Response.Write(jquery);
                }
                else if (Request.QueryString["op"] == "deletedrafts")
                {
                    Guid id = Guid.Parse(Request.QueryString["id"]);
                    DraftsRepository draftsRepo = new DraftsRepository();
                    draftsRepo.DeleteDrafts(id);

                }
                else if (Request.QueryString["op"] == "usersearchresults")
                {
                    ArrayList alstallusers = null;
                    if (Session["AllUserList"] == null)
                    {
                        User user = (User)Session["LoggedUser"];
                        alstallusers = new ArrayList();

                        /*facebook*/
                        try
                        {
                            FacebookAccountRepository faceaccount = new FacebookAccountRepository();
                            ArrayList lstfacebookaccount = faceaccount.getAllFacebookAccountsOfUser(user.Id);
                            foreach (FacebookAccount item in lstfacebookaccount)
                            {
                                alstallusers.Add(item.FbUserName + "_fb_" + item.FbUserId);
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            Console.WriteLine(ex.Message);
                        }

                        /*twitter*/
                        try
                        {
                            TwitterAccountRepository twtAccountrepo = new TwitterAccountRepository();
                            ArrayList lsttwitteraccount = twtAccountrepo.getAllTwitterAccountsOfUser(user.Id);

                            foreach (TwitterAccount item in lsttwitteraccount)
                            {
                                alstallusers.Add(item.TwitterScreenName + "_twt_" + item.TwitterUserId);
                            }

                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            Console.WriteLine(ex.Message);
                        }

                        /*linkedin*/
                        try
                        {
                            LinkedInAccountRepository linkedinaccountrepo = new LinkedInAccountRepository();
                            ArrayList lstaccount = linkedinaccountrepo.getAllLinkedinAccountsOfUser(user.Id);

                            foreach (LinkedInAccount item in lstaccount)
                            {
                                alstallusers.Add(item.LinkedinUserName + "_lin_" + item.LinkedinUserId);
                            }

                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            Console.WriteLine(ex.Message);
                        }
                        /*instagram*/
                        try
                        {
                            InstagramAccountRepository instaaccrepo = new InstagramAccountRepository();
                            ArrayList lstinstagramaccount = instaaccrepo.getAllInstagramAccountsOfUser(user.Id);
                            foreach (InstagramAccount item in lstinstagramaccount)
                            {
                                alstallusers.Add(item.InsUserName + "_ins_" + item.InstagramId);
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            Console.WriteLine(ex.Message);
                        }

                        ///*googleplus*/
                        try
                        {
                            GooglePlusAccountRepository gpaccountrepo = new GooglePlusAccountRepository();
                            ArrayList lstgpaccount = gpaccountrepo.getAllGooglePlusAccountsOfUser(user.Id);
                            foreach (GooglePlusAccount item in lstgpaccount)
                            {
                                alstallusers.Add(item.GpUserName + "_gp_" + item.GpUserId);
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            Console.WriteLine(ex.Message);
                        }

                        Session["AllUserList"] = alstallusers;
                    }
                    else
                    {
                        alstallusers = (ArrayList)Session["AllUserList"];
                    }

                }
                else if (Request.QueryString["op"] == "searchingresults")
                {
                    string txtvalue = Request.QueryString["txtvalue"];
                    string message = string.Empty;
                    if (!string.IsNullOrEmpty(txtvalue))
                    {
                        ArrayList alstall = (ArrayList)Session["AllUserList"];

                        if (alstall.Count != 0 && alstall != null)
                        {
                            foreach (string item in alstall)
                            {
                                if (item.ToLower().StartsWith(txtvalue.ToLower()))
                                {
                                    string[] nametype = item.Split('_');

                                    if (nametype[1] == "fb")
                                    {
                                        //message += "<div  class=\"btn srcbtn\">" +
                                        //                "<img width=\"15\" src=\"../Contents/img/facebook.png\" alt=\"\">" +
                                        //          "<span onclick=\"getFacebookProfiles('" + nametype[2] + "')\">" + nametype[0] + "</span>" +
                                        //           "<span data-dismiss=\"alert\" class=\"close pull-right\">×</span>" +
                                        //           "</div>";
                                        lnk = "https://www.facebook.com/" + nametype[2];
                                        message += "<div  class=\"btn srcbtn\">" +
                                                       "<img width=\"15\" src=\"../Contents/img/facebook.png\" alt=\"\">" +
                                                "<a target=\"_blank\" rel=\"me nofollow\" href=" + lnk + ">" + nametype[0] + "</a>" +
                                                  "<span data-dismiss=\"alert\" class=\"close pull-right\">×</span>" +
                                                  "</div>";
                                    }
                                    else if (nametype[1] == "twt" || item.Contains("_twt_"))
                                    {
                                        if (nametype.Count() < 4)
                                        {
                                            //message += "<div class=\"btn srcbtn\">" +
                                            //              "<img width=\"15\" src=\"../Contents/img/twticon.png\" alt=\"\">" +
                                            //                " <span onclick=\"detailsprofile('" + nametype[0] + "');\">" + nametype[0] + "</span>" +
                                            //         "<span data-dismiss=\"alert\" class=\"close pull-right\">×</span>" +
                                            //                 "</div>";
                                            lnk = "https://twitter.com/" + nametype[0];
                                            message += "<div class=\"btn srcbtn\">" +
                                                         "<img width=\"15\" src=\"../Contents/img/twticon.png\" alt=\"\">" +
                                                          "<a target=\"_blank\" rel=\"me nofollow\" href=" + lnk + ">" + nametype[0] + "</a>" +
                                                    "<span data-dismiss=\"alert\" class=\"close pull-right\">×</span>" +
                                                            "</div>";
                                        }
                                        else
                                        {
                                            string[] containstwitter = item.Split(new string[] { "_twt_" }, StringSplitOptions.None);

                                            //message += "<div  class=\"btn srcbtn\">" +
                                            //                 "<img width=\"15\" src=\"../Contents/img/twticon.png\" alt=\"\">" +
                                            //                    "<span onclick=\"detailsprofile('" + containstwitter[0] + "');\"> " + containstwitter[0] + "</span>" +
                                            //            "<span data-dismiss=\"alert\" class=\"close pull-right\">×</span>" +
                                            //                    "</div>";
                                            lnk = "https://twitter.com/" + containstwitter[0];
                                            message += "<div  class=\"btn srcbtn\">" +
                                                            "<img width=\"15\" src=\"../Contents/img/twticon.png\" alt=\"\">" +
                                                             "<a target=\"_blank\" rel=\"me nofollow\" href=" + lnk + ">" + containstwitter[0] + "</a>" +
                                                       "<span data-dismiss=\"alert\" class=\"close pull-right\">×</span>" +
                                                               "</div>";

                                        }
                                    }
                                    else if (nametype[1] == "ins")
                                    {

                                        message += "<div class=\"btn srcbtn\">" +
                                                   "<a target=\"_blank\" rel=\"" + nametype[0] + "\" href=\"http://instagram.com/" + nametype[0] + "\">" +
                                                   "<img width=\"15\" src=\"../Contents/img/instagram_24X24.png\" alt=\"\">" + nametype[0] +
                                                   "<span data-dismiss=\"alert\" class=\"close pull-right\">×</span>" +
                                                   "</a>" +
                                                "</div>";
                                        //message += "<div class=\"btn srcbtn\">" +
                                        //              "<img width=\"15\" src=\"../Contents/img/instagram_24X24.png\" alt=\"\">" +
                                        //         nametype[0] +
                                        //         "<span data-dismiss=\"alert\" class=\"close pull-right\">×</span>" +
                                        //         "</div>";
                                    }
                                    else if (nametype[1] == "lin")
                                    {
                                        LinkedInAccountRepository liRepo = new LinkedInAccountRepository();
                                        LinkedInAccount liaccount = liRepo.getLinkedinAccountDetailsById(nametype[2]);
                                        message += "<div class=\"btn srcbtn\">" +
                                                    "<a target=\"_blank\" rel=\"" + nametype[0] + "\" href=" + liaccount.ProfileUrl + ">" +
                                                    "<img width=\"15\" src=\"../Contents/img/link_icon.png\" alt=\"\">" + nametype[0] +
                                                    "<span data-dismiss=\"alert\" class=\"close pull-right\">×</span>" +
                                                    "</a>" +
                                                 "</div>";
                                        //message += "<div class=\"btn srcbtn\">" +
                                        //              "<img width=\"15\" src=\"../Contents/img/link_icon.png\" alt=\"\">" +
                                        //         nametype[0] +
                                        //         "<span data-dismiss=\"alert\" class=\"close pull-right\">×</span>" +
                                        //         "</div>";
                                    }
                                    else if (nametype[1] == "gp")
                                    {
                                        message += "<div class=\"btn srcbtn\">" +
                                                          "<img width=\"15\" src=\"../Contents/img/google_plus.png\" alt=\"\">" +
                                                     nametype[0] +
                                                     "<span data-dismiss=\"alert\" class=\"close pull-right\">×</span>" +
                                                     "</div>";

                                    }
                                }

                            }
                        }
                        else
                        {
                            message += "<div class=\"btn srcbtn\">" +
                                                  "<img width=\"15\" src=\"../Contents/img/norecord.png\" alt=\"\">" +
                                             "No Records Found" +
                                             "<span data-dismiss=\"alert\" class=\"close pull-right\">×</span>" +
                                             "</div>";

                        }

                        message += "<div class=\"socailtile\">Twitter</div>";

                        /*twitter contact search */

                        #region twitter contact search
                        try
                        {
                            User user = (User)Session["LoggedUser"];
                            Users twtUser = new Users();
                            oAuthTwitter oAuthTwt = new oAuthTwitter();
                            if (Session["oAuthUserSearch"] == null)
                            {
                                oAuthTwitter oauth = new oAuthTwitter();
                                oauth.ConsumerKey = ConfigurationManager.AppSettings["consumerKey"].ToString();
                                oauth.ConsumerKeySecret = ConfigurationManager.AppSettings["consumerSecret"].ToString();
                                oauth.CallBackUrl = ConfigurationManager.AppSettings["callbackurl"].ToString();
                                TwitterAccountRepository twtAccRepo = new TwitterAccountRepository();
                                ArrayList alst = twtAccRepo.getAllTwitterAccountsOfUser(user.Id);
                                foreach (TwitterAccount item in alst)
                                {
                                    oauth.AccessToken = item.OAuthToken;
                                    oauth.AccessTokenSecret = item.OAuthSecret;
                                    oauth.TwitterUserId = item.TwitterUserId;
                                    oauth.TwitterScreenName = item.TwitterScreenName;
                                    if (CheckTwitterToken(oauth, txtvalue))
                                    {
                                        break;
                                    }
                                }
                                Session["oAuthUserSearch"] = oauth;
                                oAuthTwt = oauth;
                            }
                            else
                            {
                                oAuthTwitter oauth = (oAuthTwitter)Session["oAuthUserSearch"];
                                oAuthTwt = oauth;
                            }

                            JArray twtuserjson = twtUser.Get_Users_Search(oAuthTwt, txtvalue, "5");

                            foreach (var item in twtuserjson)
                            {
                                //message += "<div class=\"btn srcbtn\">" +
                                //                         "<img width=\"15\" src=\"../Contents/img/twticon.png\" alt=\"\">" +
                                //                           " <span> " + item["screen_name"].ToString().TrimStart('"').TrimEnd('"') + "</span>" +
                                //                    "<span data-dismiss=\"alert\" class=\"close pull-right\">×</span>" +
                                //                            "</div>";

                                lnk = "https://twitter.com/" + item["screen_name"].ToString().TrimStart('"').TrimEnd('"');

                                message += "<div class=\"btn srcbtn\">" +
                                                        "<img width=\"15\" src=\"../Contents/img/twticon.png\" alt=\"\">" +
                                                          "<a target=\"_blank\" rel=\"me nofollow\" href=" + lnk + ">" + item["screen_name"].ToString().TrimStart('"').TrimEnd('"') + "</a>" +
                                                   "<span data-dismiss=\"alert\" class=\"close pull-right\">×</span>" +
                                                           "</div>";

                            }

                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            Console.WriteLine(ex.Message);
                        }

                        #endregion

                        message += "<div class=\"socailtile\">Facebook</div>";

                        #region Facebook Contact search
                        try
                        {
                            string accesstoken = string.Empty;
                            FacebookAccountRepository facebookaccrepo = new FacebookAccountRepository();
                            ArrayList alstfacbookusers = facebookaccrepo.getAllFacebookAccounts();

                            foreach (FacebookAccount item in alstfacbookusers)
                            {
                                accesstoken = item.AccessToken;
                                if (CheckFacebookToken(accesstoken, txtvalue))
                                {
                                    break;
                                }
                            }

                            string facebookSearchUrl = "https://graph.facebook.com/search?q=" + txtvalue + " &limit=5&type=user&access_token=" + accesstoken;
                            var facerequest = (HttpWebRequest)WebRequest.Create(facebookSearchUrl);
                            facerequest.Method = "GET";
                            string outputface = string.Empty;
                            using (var response = facerequest.GetResponse())
                            {
                                using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                                {
                                    outputface = stream.ReadToEnd();
                                }
                            }
                            if (!outputface.StartsWith("["))
                                outputface = "[" + outputface + "]";


                            JArray facebookSearchResult = JArray.Parse(outputface);

                            foreach (var item in facebookSearchResult)
                            {
                                var data = item["data"];
                                foreach (var chlid in data)
                                {
                                    lnk = "https://www.facebook.com/" + chlid["id"];
                                    message += "<div  class=\"btn srcbtn\">" +
                                                        "<img width=\"15\" src=\"../Contents/img/facebook.png\" alt=\"\">" +
                                                 "<a target=\"_blank\" rel=\"me nofollow\" href=" + lnk + ">" + chlid["name"] + "</a>" +
                                                   "<span data-dismiss=\"alert\" class=\"close pull-right\">×</span>" +
                                                   "</div>";
                                }

                            }

                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            Console.WriteLine(ex.Message);
                        }
                        #endregion


                        Response.Write(message);
                    }
                }

                    //start descovery details


                    //start descovery details for twitter
                else if (Request.QueryString["op"] == "detailsdiscoverytwitter")
                {
                    User user = (User)Session["LoggedUser"];
                    string userid = Request.QueryString["profileid"];
                    //string userid = "813739051";
                    TwitterAccountRepository twtAccountRepo = new TwitterAccountRepository();
                    ArrayList alst = twtAccountRepo.getAllTwitterAccountsOfUser(user.Id);
                    oAuthTwitter oauth = new oAuthTwitter();
                    foreach (TwitterAccount childnoe in alst)
                    {
                        oauth.AccessToken = childnoe.OAuthToken;
                        oauth.AccessTokenSecret = childnoe.OAuthSecret;
                        oauth.ConsumerKey = ConfigurationManager.AppSettings["consumerKey"];
                        oauth.ConsumerKeySecret = ConfigurationManager.AppSettings["consumerSecret"];
                        oauth.TwitterUserId = childnoe.TwitterUserId;
                        oauth.TwitterScreenName = childnoe.TwitterScreenName;
                        if (CheckTwitterTokenByUserId(oauth, userid))
                        {
                            break;
                        }
                    }

                    Users userinfo = new Users();
                    JArray userlookup = userinfo.Get_Users_LookUp(oauth, userid);
                    string jstring = string.Empty;

                    foreach (var item in userlookup)
                    {

                        jstring += "<div class=\"modal-small draggable\">";
                        jstring += "<div class=\"modal-content\">";
                        jstring += "<button class=\"modal-btn button b-close\" type=\"button\">";
                        jstring += "<span class=\"icon close-medium\"><span class=\"visuallyhidden\">X</span></span></button>";
                        jstring += "<div class=\"modal-header\"><h3 class=\"modal-title\">Profile summary</h3></div>";
                        jstring += "<div class=\"modal-body profile-modal\">";
                        jstring += "<div class=\"module profile-card component profile-header\">";
                        jstring += "<div class=\"profile-header-inner flex-module clearfix\" style=\"background-image: url('" + item["profile_banner_url"] + "');\">";
                        jstring += "<div class=\"profile-header-inner-overlay\"></div>";
                        jstring += "<a class=\"profile-picture media-thumbnail js-nav\" href=\"#\"><img class=\"avatar size73\" alt=\"" + item["name"] + "\" src=\"" + item["profile_image_url"] + "\" /></a>";
                        jstring += "<div class=\"profile-card-inner\">";
                        jstring += "<h1 class=\"fullname editable-group\">";
                        jstring += "<a href=\"www.twitter.com/" + item["name"] + "\" class=\"js-nav\">" + item["name"] + "</a>";
                        jstring += "<a class=\"verified-link js-tooltip\" href=\"#\"><span class=\"icon verified verified-large-border\"><span class=\"visuallyhidden\"></span> </span></a>";
                        jstring += "</h1>";
                        jstring += "<h2 class=\"username\"><a href=\"#\" class=\"pretty-link js-nav\"><span class=\"screen-name\"><s>@</s>" + item["screen_name"] + "</span> </a></h2>";
                        jstring += "<div class=\"bio-container editable-group\"><p class=\"bio profile-field\">";
                        try
                        {
                            jstring += item["status"]["text"];
                        }
                        catch (Exception ex) { logger.Error(ex.Message); }

                        jstring += "</p></div>";
                        jstring += "<p class=\"location-and-url\">";
                        jstring += "<span class=\"location-container editable-group\"><span class=\"location profile-field\"></span></span>";
                        jstring += "<span class=\"divider hidden\"></span> ";
                        jstring += "<span class=\"url editable-group\">  <span class=\"profile-field\"><a title=\"#\" href=\"" + item["url"] + "\" rel=\"me nofollow\" target=\"_blank\">" + item["url"] + " </a>";
                        jstring += "<div style=\"cursor: pointer; width: 16px; height: 16px; display: inline-block;\">&nbsp;</div>";
                        jstring += "</span></span></p></div></div>";
                        jstring += "<div class=\"clearfix\">";
                        jstring += "<div class=\"default-footer\">";
                        jstring += "<ul class=\"stats js-mini-profile-stats\">" +
                            //"<li><a href=\"#\" class=\"js-nav\"><strong> 6,274</strong> Tweets </a></li>" +
                                          "<li><a href=\"#\" class=\"js-nav\"><strong>" + item["friends_count"] + "</strong> Following </a></li>" +
                                          "<li><a href=\"#\" class=\"js-nav\"><strong>" + item["followers_count"] + "</strong> Followers </a></li>";
                        jstring += "</ul>";
                        jstring += "<div class=\"btn-group\">" +
                                      "<div class=\"follow_button\">" +
                            //"<span class=\"button-text follow-text\">Follow</span> " +
                                          "<span class=\"button-text following-text\">Following</span>" +
                                          "<span class=\"button-text unfollow-text\">Unfollow</span>" +
                                      "</div>" +
                                   "</div>";
                        jstring += "</div></div>";
                        jstring += "<div class=\"profile-social-proof\"><div class=\"follow-bar\"></div></div></div>";
                        jstring += "<ol class=\"recent-tweets\">" +
                                      "<li class=\"\">" +
                                          "<div>" +
                                            "<i class=\"dogear\"></i>" +

                                          "</div>" +
                                      "</li>" +
                                  "</ol>" +
                                  "<div class=\"go_to_profile\">" +
                                      "<small><a href=\"https://twitter.com/" + item["screen_name"] + "\" target=\"_blank\" class=\"view_profile\">Go to full profile →</a></small>" +
                                  "</div>" +
                              "</div>" +
                              "<div class=\"loading\">" +
                                  "<span class=\"spinner-bigger\"></span>" +
                              "</div>" +
                          "</div>";
                        jstring += "</div>";


                    }
                    Response.Write(jstring);
                }

                    //twitter Descovery End

                    //facebook start




                else if (Request.QueryString["op"] == "detailsdiscoveryfacebook")
                {
                    User user = (User)Session["LoggedUser"];
                    string userid = Request.QueryString["profileid"];
                    FacebookAccountRepository fbRepo = new FacebookAccountRepository();
                    ArrayList alst = fbRepo.getAllFacebookAccountsOfUser(user.Id);
                    string accesstoken = string.Empty;


                    foreach (FacebookAccount childnoe in alst)
                    {
                        accesstoken = childnoe.AccessToken;

                        break;
                    }

                    FacebookClient fbclient = new FacebookClient(accesstoken);
                    string jstring = string.Empty;
                    dynamic item = fbclient.Get(userid);


                    jstring += "<div class=\"modal-small draggable\">";
                    jstring += "<div class=\"modal-content\">";
                    jstring += "<button class=\"modal-btn button b-close\" type=\"button\">";
                    jstring += "<span class=\"icon close-medium\"><span class=\"visuallyhidden\">X</span></span></button>";
                    jstring += "<div class=\"modal-header\"><h3 class=\"modal-title\">Profile summary</h3></div>";
                    jstring += "<div class=\"modal-body profile-modal\">";
                    jstring += "<div class=\"module profile-card component profile-header\">";

                    try
                    {
                        jstring += "<div class=\"profile-header-inner flex-module clearfix\" style=\"background-image: url('" + item["cover"]["source"] + "');\">";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        jstring += "<div class=\"profile-header-inner flex-module clearfix\" style=\"background-image: url('https://pbs.twimg.com/profile_banners/215936249/1371721359');\">";
                    }
                    jstring += "<div class=\"profile-header-inner-overlay\"></div>";
                    try
                    {
                        jstring += "<a class=\"profile-picture media-thumbnail js-nav\" href=\"#\"><img class=\"avatar size73\" alt=\"\" src=\"http://graph.facebook.com/" + item["id"] + "/picture?type=small\" /></a>";
                    }
                    catch (Exception)
                    {

                        jstring += "<a class=\"profile-picture media-thumbnail js-nav\" href=\"#\"><img class=\"avatar size73\" alt=\"\" src=\"http://graph.facebook.com/picture?type=small\" /></a>";
                    }
                    jstring += "<div class=\"profile-card-inner\">";
                    jstring += "<h1 class=\"fullname editable-group\">";
                    try
                    {
                        jstring += "<a href=\"#\" class=\"js-nav\">" + item["name"] + "</a>";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    jstring += "<a class=\"verified-link js-tooltip\" href=\"#\"><span class=\"icon verified verified-large-border\"><span class=\"visuallyhidden\"></span> </span></a>";
                    jstring += "</h1>";
                    try
                    {
                        jstring += "<h2 class=\"username\"><a href=\"#\" class=\"pretty-link js-nav\"><span class=\"screen-name\"><s>@</s>" + item["username"] + "</span> </a></h2>";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    jstring += "<div class=\"bio-container editable-group\"><p class=\"bio profile-field\">";
                    try
                    {
                        jstring += item["about"];
                    }
                    catch (Exception ex) { logger.Error(ex.Message); }

                    jstring += "</p></div>";
                    jstring += "<p class=\"location-and-url\">";
                    jstring += "<span class=\"location-container editable-group\"><span class=\"location profile-field\"></span></span>";
                    jstring += "<span class=\"divider hidden\"></span> ";
                    jstring += "<span class=\"url editable-group\">  <span class=\"profile-field\"><a title=\"#\" href=\"http://www.facebook.com/" + item["id"] + "\" rel=\"me nofollow\"  </a>";
                    jstring += "<div style=\"cursor: pointer; width: 16px; height: 16px; display: inline-block;\">&nbsp;</div>";
                    jstring += "</span></span></p></div></div>";
                    jstring += "<div class=\"clearfix\">";
                    jstring += "<div class=\"default-footer\">";

                    jstring += "<div class=\"btn-group\">" +
                                  "<div class=\"follow_button\">" +

                                      //"<span class=\"button-text following-text\">Following</span>" +
                        //"<span class=\"button-text unfollow-text\">Unfollow</span>" +
                                  "</div>" +
                               "</div>";
                    jstring += "</div></div>";
                    jstring += "<div class=\"profile-social-proof\"><div class=\"follow-bar\"></div></div></div>";
                    jstring += "<ol class=\"recent-tweets\">" +
                                  "<li class=\"\">" +
                                      "<div>" +
                                        "<i class=\"dogear\"></i>" +

                                      "</div>" +
                                  "</li>" +
                              "</ol>" +
                              "<div class=\"go_to_profile\">" +
                                  "<small><a href=\"http://www.facebook.com/" + item["id"] + "\" target=\"_blank\" class=\"view_profile\">Go to full profile →</a></small>" +
                              "</div>" +
                          "</div>" +
                          "<div class=\"loading\">" +
                              "<span class=\"spinner-bigger\"></span>" +
                          "</div>" +
                      "</div>";
                    jstring += "</div>";



                    Response.Write(jstring);

                }







                    //facebook end

                    //End descovery details




                else if (Request.QueryString["op"] == "getTwitterUserDetails")
                {
                    User user = (User)Session["LoggedUser"];
                    string userid = Request.QueryString["profileid"];
                    TwitterAccountRepository twtAccountRepo = new TwitterAccountRepository();
                    ArrayList alst = twtAccountRepo.getAllTwitterAccountsOfUser(user.Id);
                    oAuthTwitter oauth = new oAuthTwitter();
                    foreach (TwitterAccount childnoe in alst)
                    {
                        oauth.AccessToken = childnoe.OAuthToken;
                        oauth.AccessTokenSecret = childnoe.OAuthSecret;
                        oauth.ConsumerKey = ConfigurationManager.AppSettings["consumerKey"];
                        oauth.ConsumerKeySecret = ConfigurationManager.AppSettings["consumerSecret"];
                        oauth.TwitterUserId = childnoe.TwitterUserId;
                        oauth.TwitterScreenName = childnoe.TwitterScreenName;
                        if (CheckTwitterTokenByUserId(oauth, userid))
                        {
                            break;
                        }
                    }

                    Users userinfo = new Users();
                    JArray userlookup = userinfo.Get_Users_LookUp(oauth, userid);
                    string jstring = string.Empty;

                    foreach (var item in userlookup)
                    {
                        //jstring = "<div class=\"big-puff\">";
                        //jstring += "<article><dl>";
                        //jstring += "<img src=\"" + item["profile_image_url"] + "\" alt=\"\" class=\"photo\">";
                        //jstring += "<div class=\"descrption\">";
                        //jstring += "<h3 title=\"Carlos Ullon\" class=\"fn\">" + item["name"] + "<span class=\"screenname prof_meta\">" + item["screen_name"];
                        //jstring += "<span class=\"ficon blue_bird_sm nickname\"></span></span>";
                        //jstring += "</h3><p class=\"note\"></p>";
                        //jstring += "<ul class=\"prof_meta\">";
                        //try
                        //{
                        //    jstring += "<li>" + item["status"]["text"] + "</li>";
                        //}
                        //catch { }
                        //jstring += "<li></li>";
                        //jstring += "</ul></div></dl><section class=\"profile_sub_wrap\"><a class=\"klout_link\" target=\"_blank\" href=\"http://www.klout.com/carlosullon\">";
                        //jstring += "<div class=\"klout_container\"><span class=\"score\"></span>";
                        //jstring += "<div class=\"icon klout_score\"></div>";
                        //jstring += "</div></a>";
                        //jstring += "<ul class=\"follow\">";
                        //jstring += "<li><span class=\"followers filter\"><span>Followers</span>";
                        //jstring += "<a data-msg_type=\"followers\" href=\"javascript:void(0)\">" + item["followers_count"] + "</a></span></li>";
                        //jstring += "<li><span class=\"friends filter\"><span>Following</span> <a data-msg_type=\"friends\" href=\"javascript:void(0)\">" + item["friends_count"] + "</a></span></li>";
                        //jstring += "</ul></section></article>";
                        //jstring += "<div class=\"usertweets\">";
                        //jstring += "<div class=\"tweetstitle\">User Tweets</div>";
                        //jstring += "<div id=\"offmessages\" class=\"usertweets_div\">";
                        //jstring += "</div>";
                        //jstring += "</div>";
                        //jstring += "</div></div>";

                        //============================



                        jstring += "<div class=\"modal-small draggable\">";
                        jstring += "<div class=\"modal-content\">";
                        jstring += "<button class=\"modal-btn button b-close\" type=\"button\">";
                        jstring += "<span class=\"icon close-medium\"><span class=\"visuallyhidden\">X</span></span></button>";
                        jstring += "<div class=\"modal-header\"><h3 class=\"modal-title\">Profile summary</h3></div>";
                        jstring += "<div class=\"modal-body profile-modal\">";
                        jstring += "<div class=\"module profile-card component profile-header\">";
                        jstring += "<div class=\"profile-header-inner flex-module clearfix\" style=\"background-image: url('" + item["profile_banner_url"] + "');\">";
                        jstring += "<div class=\"profile-header-inner-overlay\"></div>";
                        jstring += "<a class=\"profile-picture media-thumbnail js-nav\" href=\"#\"><img class=\"avatar size73\" alt=\"" + item["name"] + "\" src=\"" + item["profile_image_url"] + "\" /></a>";
                        jstring += "<div class=\"profile-card-inner\">";
                        jstring += "<h1 class=\"fullname editable-group\">";
                        jstring += "<a href=\"www.twitter.com/" + item["name"] + "\" class=\"js-nav\">" + item["name"] + "</a>";
                        jstring += "<a class=\"verified-link js-tooltip\" href=\"#\"><span class=\"icon verified verified-large-border\"><span class=\"visuallyhidden\"></span> </span></a>";
                        jstring += "</h1>";
                        jstring += "<h2 class=\"username\"><a href=\"#\" class=\"pretty-link js-nav\"><span class=\"screen-name\"><s>@</s>" + item["screen_name"] + "</span> </a></h2>";
                        jstring += "<div class=\"bio-container editable-group\"><p class=\"bio profile-field\">";
                        try
                        {
                            jstring += item["status"]["text"];
                        }
                        catch (Exception ex) { logger.Error(ex.Message); }

                        jstring += "</p></div>";
                        jstring += "<p class=\"location-and-url\">";
                        jstring += "<span class=\"location-container editable-group\"><span class=\"location profile-field\"></span></span>";
                        jstring += "<span class=\"divider hidden\"></span> ";
                        jstring += "<span class=\"url editable-group\">  <span class=\"profile-field\"><a title=\"#\" href=\"" + item["url"] + "\" rel=\"me nofollow\" target=\"_blank\">" + item["url"] + " </a>";
                        jstring += "<div style=\"cursor: pointer; width: 16px; height: 16px; display: inline-block;\">&nbsp;</div>";
                        jstring += "</span></span></p></div></div>";
                        jstring += "<div class=\"clearfix\">";
                        jstring += "<div class=\"default-footer\">";
                        jstring += "<ul class=\"stats js-mini-profile-stats\">" +
                            //"<li><a href=\"#\" class=\"js-nav\"><strong> 6,274</strong> Tweets </a></li>" +
                                          "<li><a href=\"#\" class=\"js-nav\"><strong>" + item["friends_count"] + "</strong> Following </a></li>" +
                                          "<li><a href=\"#\" class=\"js-nav\"><strong>" + item["followers_count"] + "</strong> Followers </a></li>";
                        jstring += "</ul>";
                        jstring += "<div class=\"btn-group\">" +
                                      "<div class=\"follow_button\">" +
                            //"<span class=\"button-text follow-text\">Follow</span> " +
                                          "<span class=\"button-text following-text\">Following</span>" +
                                          "<span class=\"button-text unfollow-text\">Unfollow</span>" +
                                      "</div>" +
                                   "</div>";
                        jstring += "</div></div>";
                        jstring += "<div class=\"profile-social-proof\"><div class=\"follow-bar\"></div></div></div>";
                        jstring += "<ol class=\"recent-tweets\">" +
                                      "<li class=\"\">" +
                                          "<div>" +
                                            "<i class=\"dogear\"></i>" +
                            //"<div class=\"content\">" +
                            //    "<div class=\"stream-item-header\">" +
                            //        "<a href=\"#\" class=\"account-group\"> " +
                            //            "<img alt=\"\" src=\""+item["profile_img_url"]+"\" class=\"avatar js-action-profile-avatar\" />" +
                            //        "</a>" +
                            //        "<div class=\"content_stream\">" +
                            //            "<div class=\"content_time\">" +
                            //                "<a>" +
                            //                    "<strong class=\"fullname\">"+item["name"]+"</strong>" +
                            //                    "<span>&rlm;</span> " +
                            //                    "<span class=\"username_action_name\"><s>@</s><b>"+item["screen_name"]+"</b></span>" +
                            //                "</a>" +
                            //                "<small class=\"time\">" +
                            //                    "<a title=\"11:42 AM - 10 Jul 13 (GMT+05:30)\" class=\"tweet-timestamp js-permalink js-nav\" href=\"#\">" +
                            //                       "<span class=\"_timestamp js-short-timestamp js-relative-timestamp\">33m</span>" +
                            //                    "</a>" +
                            //                "</small>" +
                            //            "</div>" +
                            //            "<p class=\"tweet_text\">" +
                            //               "RT If you watched Bhuvneshwar Kumar's amazing bowling performance in yesterday's" +
                            //               " match. <a dir=\"ltr\" class=\"twitter_hashtag\" href=\"#\"><s>#</s><b>IndvsSL</b></a>" +
                            //            "</p>" +
                            //            "<div class=\"stream_item_footer\">" +
                            //                "<a href=\"#\" class=\"details\">" +
                            //                    "<b><span class=\"simple-details-link\">Details</span> </b>" +
                            //                "</a>" +
                            //            "</div>" +
                            //        "</div>" +
                            //    "</div>" +
                            //"</div>" +
                                          "</div>" +
                                      "</li>" +
                                  "</ol>" +
                                  "<div class=\"go_to_profile\">" +
                                      "<small><a href=\"https://twitter.com/" + item["screen_name"] + "\" target=\"_blank\" class=\"view_profile\">Go to full profile →</a></small>" +
                                  "</div>" +
                              "</div>" +
                              "<div class=\"loading\">" +
                                  "<span class=\"spinner-bigger\"></span>" +
                              "</div>" +
                          "</div>";
                        jstring += "</div>";


                    }
                    Response.Write(jstring);
                }
                else if (Request.QueryString["op"] == "pauseRssMessage")
                {
                    Guid ID = Guid.Parse(Request.QueryString["id"]);
                    RssFeedsRepository rssRepo = new RssFeedsRepository();
                    rssRepo.updateFeedStatus("pause", ID);
                }
                else if (Request.QueryString["op"] == "deleteRssMessage")
                {
                    Guid ID = Guid.Parse(Request.QueryString["id"]);
                    RssFeedsRepository rssRepo = new RssFeedsRepository();
                    rssRepo.DeleteRssMessage(ID);
                }
                else if (Request.QueryString["op"] == "playRssMessage")
                {
                    Guid ID = Guid.Parse(Request.QueryString["id"]);
                    RssFeedsRepository rssRepo = new RssFeedsRepository();
                    rssRepo.updateFeedStatus("play", ID);
                }


                    //below  code is used for facebook









                else if (Request.QueryString["op"] == "facebookProfileDetails")
                {
                    User user = (User)Session["LoggedUser"];
                    string userid = Request.QueryString["profileid"];
                    FacebookAccountRepository fbRepo = new FacebookAccountRepository();
                    ArrayList alst = fbRepo.getAllFacebookAccountsOfUser(user.Id);
                    string accesstoken = string.Empty;


                    foreach (FacebookAccount childnoe in alst)
                    {
                        accesstoken = childnoe.AccessToken;
                        if (CheckFacebookTokenByUserId(accesstoken, userid))
                        {
                            break;
                        }
                    }

                    FacebookClient fbclient = new FacebookClient(accesstoken);
                    string jstring = string.Empty;
                    dynamic item = fbclient.Get(userid);


                    jstring += "<div class=\"modal-small draggable\">";
                    jstring += "<div class=\"modal-content\">";
                    jstring += "<button class=\"modal-btn button b-close\" type=\"button\">";
                    jstring += "<span class=\"icon close-medium\"><span class=\"visuallyhidden\">X</span></span></button>";
                    jstring += "<div class=\"modal-header\"><h3 class=\"modal-title\">Profile summary</h3></div>";
                    jstring += "<div class=\"modal-body profile-modal\">";
                    jstring += "<div class=\"module profile-card component profile-header\">";

                    try
                    {
                        jstring += "<div class=\"profile-header-inner flex-module clearfix\" style=\"background-image: url('" + item["cover"]["source"] + "');\">";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        jstring += "<div class=\"profile-header-inner flex-module clearfix\" style=\"background-image: url('https://pbs.twimg.com/profile_banners/215936249/1371721359');\">";
                    }
                    jstring += "<div class=\"profile-header-inner-overlay\"></div>";
                    jstring += "<a class=\"profile-picture media-thumbnail js-nav\" href=\"#\"><img class=\"avatar size73\" alt=\"" + item["name"] + "\" src=\"http://graph.facebook.com/" + item["id"] + "/picture?type=small\" /></a>";
                    jstring += "<div class=\"profile-card-inner\">";
                    jstring += "<h1 class=\"fullname editable-group\">";
                    try
                    {
                        jstring += "<a href=\"#\" class=\"js-nav\">" + item["name"] + "</a>";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    jstring += "<a class=\"verified-link js-tooltip\" href=\"#\"><span class=\"icon verified verified-large-border\"><span class=\"visuallyhidden\"></span> </span></a>";
                    jstring += "</h1>";
                    try
                    {
                        jstring += "<h2 class=\"username\"><a href=\"#\" class=\"pretty-link js-nav\"><span class=\"screen-name\"><s>@</s>" + item["username"] + "</span> </a></h2>";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    jstring += "<div class=\"bio-container editable-group\"><p class=\"bio profile-field\">";
                    try
                    {
                        jstring += item["about"];
                    }
                    catch (Exception ex) { logger.Error(ex.Message); }

                    jstring += "</p></div>";
                    jstring += "<p class=\"location-and-url\">";
                    jstring += "<span class=\"location-container editable-group\"><span class=\"location profile-field\"></span></span>";
                    jstring += "<span class=\"divider hidden\"></span> ";
                    jstring += "<span class=\"url editable-group\">  <span class=\"profile-field\"><a title=\"#\" href=\"http://www.facebook.com/" + item["id"] + "\" rel=\"me nofollow\" target=\"_blank\">" + item["link"] + " </a>";
                    jstring += "<div style=\"cursor: pointer; width: 16px; height: 16px; display: inline-block;\">&nbsp;</div>";
                    jstring += "</span></span></p></div></div>";
                    jstring += "<div class=\"clearfix\">";
                    jstring += "<div class=\"default-footer\">";

                    jstring += "<div class=\"btn-group\">" +
                                  "<div class=\"follow_button\">" +

                                      //"<span class=\"button-text following-text\">Following</span>" +
                        //"<span class=\"button-text unfollow-text\">Unfollow</span>" +
                                  "</div>" +
                               "</div>";
                    jstring += "</div></div>";
                    jstring += "<div class=\"profile-social-proof\"><div class=\"follow-bar\"></div></div></div>";
                    jstring += "<ol class=\"recent-tweets\">" +
                                  "<li class=\"\">" +
                                      "<div>" +
                                        "<i class=\"dogear\"></i>" +

                                      "</div>" +
                                  "</li>" +
                              "</ol>" +
                              "<div class=\"go_to_profile\">" +
                                  "<small><a href=\"http://www.facebook.com/" + item["id"] + "\" target=\"_blank\" class=\"view_profile\">Go to full profile →</a></small>" +
                              "</div>" +
                          "</div>" +
                          "<div class=\"loading\">" +
                              "<span class=\"spinner-bigger\"></span>" +
                          "</div>" +
                      "</div>";
                    jstring += "</div>";



                    Response.Write(jstring);

                }
            }
        }



        public bool CheckFacebookToken(string fbtoken, string txtvalue)
        {
            bool checkFacebookToken = false;
            try
            {
                string facebookSearchUrl = "https://graph.facebook.com/search?q=" + txtvalue + " &limit=5&type=user&access_token=" + fbtoken;
                var facerequest = (HttpWebRequest)WebRequest.Create(facebookSearchUrl);
                facerequest.Method = "GET";
                string outputface = string.Empty;
                using (var response = facerequest.GetResponse())
                {
                    using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                    {
                        outputface = stream.ReadToEnd();
                    }
                }
                checkFacebookToken = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return checkFacebookToken;
        }
        public bool CheckTwitterToken(oAuthTwitter objoAuthTwitter, string txtvalue)
        {
            bool CheckTwitterToken = false;
            //oAuthTwitter oAuthTwt = new oAuthTwitter();
            Users twtUser = new Users();
            try
            {
                JArray twtuserjson = twtUser.Get_Users_Search(objoAuthTwitter, txtvalue, "5");
                CheckTwitterToken = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return CheckTwitterToken;
        }


        public bool CheckFacebookTokenByUserId(string fbtoken, string userid)
        {
            bool CheckFacebookTokenByUserId = false;
            try
            {
                FacebookClient fbclient = new FacebookClient(fbtoken);
                string jstring = string.Empty;
                dynamic item = fbclient.Get(userid);
                CheckFacebookTokenByUserId = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return CheckFacebookTokenByUserId;
        }
        public bool CheckTwitterTokenByUserId(oAuthTwitter objoAuthTwitter, string userid)
        {
            bool CheckTwitterTokenByUserId = false;
            //oAuthTwitter oAuthTwt = new oAuthTwitter();
            Users twtUser = new Users();
            try
            {
                Users userinfo = new Users();
                JArray userlookup = userinfo.Get_Users_LookUp(objoAuthTwitter, userid);
                CheckTwitterTokenByUserId = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return CheckTwitterTokenByUserId;
        }

        public bool CheckFacebookTokenIsValid(string fbtoken)
        {
            bool CheckFacebookTokenByUserId = false;
            try
            {
                FacebookClient fb = new FacebookClient(fbtoken);
                string jstring = string.Empty;
                dynamic profile = fb.Get("me");
                CheckFacebookTokenByUserId = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return CheckFacebookTokenByUserId;
        }
    }
}