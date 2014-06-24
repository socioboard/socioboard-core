using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SocioBoard.Domain;
using SocioBoard.Model;
using SocioBoard.Feeds;
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
using GlobusTwitterLib.Twitter.Core.TimeLineMethods;
using SocioBoard.Helper;

namespace letTalkNew.Helper
{
    public partial class AjaxHelper : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(AjaxHelper));

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["LoggedUser"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
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
                        AjaxFeed ajxfed = new AjaxFeed();
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
                        letTalkNew.Message.AjaxMessage ajxmes = new letTalkNew.Message.AjaxMessage();
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
                        message = ajxmes.BindNewData(dtt);
                    }
                    Response.Write(message);
                }
                else if (Request.QueryString["op"] == "upgradeplan")
                {
                    User user = (User)Session["LoggedUser"];
                    UserRepository userRepo = new UserRepository();
                    string accounttype = Request.QueryString["planid"];
                    if (accounttype == AccountType.Deluxe.ToString().ToLower())
                    {
                        userRepo.UpdateAccountType(user.Id, AccountType.Deluxe.ToString());
                        user.AccountType = AccountType.Deluxe.ToString();
                    }
                    else if (accounttype == AccountType.Standard.ToString().ToLower())
                    {
                        userRepo.UpdateAccountType(user.Id, AccountType.Standard.ToString());
                        user.AccountType = AccountType.Standard.ToString();
                    }
                    else if (accounttype == AccountType.Premium.ToString().ToLower())
                    {
                        userRepo.UpdateAccountType(user.Id, AccountType.Premium.ToString());
                        user.AccountType = AccountType.Premium.ToString();
                    }
                    Session["LoggedUser"] = user;

                }
                else if (Request.QueryString["op"] == "bindrssActive")
                {
                    User user = (User)Session["LoggedUser"];
                    RssFeedsRepository rssFeedsRepo = new RssFeedsRepository();
                    List<RssFeeds> lstrssfeeds = rssFeedsRepo.getAllActiveRssFeeds(user.Id);
                    TwitterAccountRepository twtAccountRepo = new TwitterAccountRepository();
                    if (lstrssfeeds != null)
                    {
                        if (lstrssfeeds.Count != 0)
                        {
                            int rssCount = 0;
                            string rssData = string.Empty;
                            rssData += "<h2 class=\"league section-ttl rss_header\">Active RSS Feeds</h2>";
                            foreach (RssFeeds item in lstrssfeeds)
                            {
                                TwitterAccount twtAccount = twtAccountRepo.getUserInformation(item.ProfileScreenName, user.Id);
                                string picurl = string.Empty;


                                if (string.IsNullOrEmpty(twtAccount.ProfileUrl))
                                {
                                    picurl = "../Contents/img/blank_img.png";

                                }
                                else
                                {
                                    picurl = twtAccount.ProfileUrl;

                                }
                                rssData +=

                                   " <section id=\"" + item.Id + "\" class=\"publishing\">" +
                                        "<section class=\"twothird\">" +
                                            "<article class=\"quarter\">" +
                                                "<div href=\"#\" class=\"avatar_link view_profile\" title=\"\">" +
                                                    "<img title=\"" + item.ProfileScreenName + "\" src=\"" + picurl + "\" data-src=\"\" class=\"avatar sm\">" +
                                                    "<article class=\"rss_ava_icon\"><span title=\"Twitter\" class=\"icon twitter_16\"></span></article>" +
                                                "</div>" +
                                            "</article>" +
                                            "<article class=\"threefourth\">" +
                                                "<ul>" +
                                                    "<li>" + item.FeedUrl + "</li>" +
                                                    "<li>Prefix: </li>" +
                                                    "<li class=\"freq\" title=\"New items from this feed will be posted at most once every hour\">Max Frequency: " + item.Duration + "</li>" +
                                                "</ul>" +
                                            "</article>" +
                                        "</section>" +
                                        "<section class=\"third\">" +
                                            "<ul class=\"rss_action_buttons\">" +
                                                "<li onclick=\"pauseFunction('" + item.Id + "');\" class=\"\"><a id=\"pause_" + item.Id + "\" href=\"#\" title=\"Pause\" class=\"small_pause icon pause\"></a></li>" +
                                                "<li onclick=\"deleteRssFunction('" + item.Id + "');\" class=\"show-on-hover\"><a id=\"delete_" + item.Id + "\" href=\"#\" title=\"Delete\" class=\"small_remove icon delete\"></a></li>" +
                                            "</ul>" +
                                        "</section>" +
                                     "</section>";
                            }
                            Response.Write(rssData);
                        }
                    }

                }

                else if (Request.QueryString["op"] == "savedrafts")
                {
                    Guid Id = Guid.Parse(Request.QueryString["id"]);
                    string newstr = Request.QueryString["newstr"];
                    DraftsRepository draftsRepo = new DraftsRepository();
                    draftsRepo.UpdateDrafts(Id, newstr);

                }
                else if (Request.QueryString["op"] == "getTwitterUserTweets")
                {
                    UrlExtractor urlext = new UrlExtractor();
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
                        break;
                    }

                    TimeLine timeLine = new TimeLine();

                    //need to be implement waiting for design
                    string mes = string.Empty;
                    JArray userlookup = timeLine.Get_Statuses_User_Timeline(oauth,userid);
                    string jstring = string.Empty;
                    int i = 0;
                    foreach (var item in userlookup)
                    {

                        if (i < 2)
                        {
                            string[] str = urlext.splitUrlFromString(item["text"].ToString());
                            mes += "<li class=\"\">" +
                                              "<div class=\"twtcommands\">" +
            "<a class=\"account-group\">" +
                "<img class=\"avatar\" alt=\"\" src=\"" + item["user"]["profile_image_url"] + "\" alt=\"\" />" +
            "</a>" +
            "<div class=\"stream-item-header\">" +
                "<div class=\"user-details\">" +
                    "<strong class=\"fullname\">" + item["user"]["name"] + "</strong>" +
                    "<span class=\"username\">" +
                        "<s>@</s>" +
                        "<b>" + item["screen_name"] + "</b>" +
                    "</span>" +
                    "<small class=\"time\"></small>" +
                "</div><p class=\"tweet-text\">";




                            foreach (string substritem in str)
                            {
                                if (!string.IsNullOrEmpty(substritem))
                                {
                                    if (substritem.Contains("http"))
                                    {
                                        mes += "<a target=\"_blank\" href=\"" + substritem + "\">" + substritem + "</a>";
                                    }
                                    else
                                    {
                                        mes += substritem;
                                    }
                                }
                            }

                            //item["text"] " +
                            //"<a target=\"_blank\" class=\"twitter-timeline-link\" href=\"#\" f69e857af67d2c=\"true\">" +
                            //    "<span class=\"tco-ellipsis\"></span>" +
                            //    "<span class=\"invisible\">http://</span>" +
                            //    "<span class=\"js-display-url\">ow.ly/o4o7l</span>" +
                            //    "<span class=\"invisible\"></span>" +
                            //    "<span class=\"tco-ellipsis\"><span class=\"invisible\">&nbsp;</span></span>" +
                            //"</a>

                            mes += "</p>" +
                                 "<div class=\"details\">" +
                                     "<a class=\"stream_details\"></a>" +
                                 "</div>" +
                             "</div>" +
                       "</div>" +
                                                           "</li>";
                            i++;
                        }
                        else {
                            break;
                        }
                    }
                    Response.Write(mes);

                }
                else if (Request.QueryString["op"] == "saveWooQueue")
                {
                    Guid Id = Guid.Parse(Request.QueryString["id"]);
                    string profileid = Request.QueryString["profid"];
                    string message = Request.QueryString["message"];
                    string network = Request.QueryString["network"];
                    string net = string.Empty;

                    if (network == "fb")
                    {
                        net = "facebook";
                    }
                    else if (network == "twt")
                    {
                        net = "twitter";

                    }
                    else if (network == "lin")
                    {
                        net = "linkedin";
                    }
                    ScheduledMessageRepository schmsgRepo = new ScheduledMessageRepository();
                    schmsgRepo.UpdateProfileScheduleMessage(Id, profileid, message, net);


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
                else if (Request.QueryString["op"] == "deletewooqueuemessage")
                {
                    try
                    {
                        Guid id = Guid.Parse(Request.QueryString["id"]);
                        ScheduledMessageRepository schmsgRepo = new ScheduledMessageRepository();
                        schmsgRepo.deleteMessage(id);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
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
                    //string type = Request.QueryString["type"];
                    //string key = Request.QueryString["keyword"];
                    //Discovery discoverypage = new Discovery();
                    //string search = discoverypage.getresults(key);
                    //string message = "<ul id=\"message-list\">" + search + "</ul>";
                    //Response.Write(message);
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
                                                  //"<div class=\"star-ribbon\"></div>" +
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


                                        jquery += "</p>" +
                                            //"<a href=\"#\" class=\"btn\">Hide</a>" +
                                          "<a href=\"https://twitter.com/" + items["screen_name"] + "\" target=\"_blank\" class=\"btn\">Full Profile <i class=\"icon-caret-right\"></i> </a> " +
                                      //  "<div class=\"scl\">" +
                                      //    "<a href=\"#\"><img alt=\"\" src=\"../Contents/img/admin/usergrey.png\"></a>" +
                                      //    "<a href=\"#\"><img alt=\"\" src=\"../Contents/img/admin/goto.png\"></a>" +
                                      //    "<a href=\"#\"><img alt=\"\" src=\"../Contents/img/admin/setting.png\"></a>" +
                                      //"</div>
                                      "</div></div>" +
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

                        if (alstall.Count != 0)
                        {
                            foreach (string item in alstall)
                            {
                                if (item.ToLower().StartsWith(txtvalue))
                                {
                                    string[] nametype = item.Split('_');

                                    if (nametype[1] == "fb")
                                    {
                                        message += "<div  class=\"btn srcbtn\">" +
                                                        "<img width=\"15\" src=\"../Contents/img/facebook.png\" alt=\"\">" +
                                                  "<span onclick=\"getFacebookProfiles('" + nametype[2] + "')\">" + nametype[0] + "</span>" +
                                                   "<span data-dismiss=\"alert\" class=\"close pull-right\">×</span>" +
                                                   "</div>";
                                    }
                                    else if (nametype[1] == "twt" || item.Contains("_twt_"))
                                    {
                                        if (nametype.Count() < 4)
                                        {
                                            message += "<div class=\"btn srcbtn\">" +
                                                          "<img width=\"15\" src=\"../Contents/img/twticon.png\" alt=\"\">" +
                                                            " <span onclick=\"detailsprofile('" + nametype[0] + "');\">" + nametype[0] + "</span>" +
                                                     "<span data-dismiss=\"alert\" class=\"close pull-right\">×</span>" +
                                                             "</div>";
                                        }
                                        else
                                        {
                                            string[] containstwitter = item.Split(new string[] { "_twt_" }, StringSplitOptions.None);

                                            message += "<div  class=\"btn srcbtn\">" +
                                                             "<img width=\"15\" src=\"../Contents/img/twticon.png\" alt=\"\">" +
                                                                "<span onclick=\"detailsprofile('" + containstwitter[0] + "');\"> " + containstwitter[0] + "</span>" +
                                                        "<span data-dismiss=\"alert\" class=\"close pull-right\">×</span>" +
                                                                "</div>";

                                        }
                                    }
                                    else if (nametype[1] == "ins")
                                    {
                                        message += "<div class=\"btn srcbtn\">" +
                                                      "<img width=\"15\" src=\"../Contents/img/instagram_24X24.png\" alt=\"\">" +
                                                 nametype[0] +
                                                 "<span data-dismiss=\"alert\" class=\"close pull-right\">×</span>" +
                                                 "</div>";
                                    }
                                    else if (nametype[1] == "lin")
                                    {
                                        message += "<div class=\"btn srcbtn\">" +
                                                      "<img width=\"15\" src=\"../Contents/img/link_icon.png\" alt=\"\">" +
                                                 nametype[0] +
                                                 "<span data-dismiss=\"alert\" class=\"close pull-right\">×</span>" +
                                                 "</div>";
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
                                    break;
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
                                message += "<div class=\"btn srcbtn\">" +
                                                         "<img width=\"15\" src=\"../Contents/img/twticon.png\" alt=\"\">" +
                                                           " <span> " + item["screen_name"].ToString().TrimStart('"').TrimEnd('"') + "</span>" +
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
                                break;
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
                                    message += "<div  class=\"btn srcbtn\">" +
                                                        "<img width=\"15\" src=\"../Contents/img/facebook.png\" alt=\"\">" +
                                                  "<span >" + chlid["name"] + "</span>" +
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
                        break;
                    }

                    Users userinfo = new Users();
                    //JArray foll = userinfo.Get_Followers_ById(oauth, userid);
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
                        jstring += "<a href=\"#\" class=\"js-nav\">" + item["name"] + "</a>";
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
                                      "<div class=\"follow_button\">";
                        //"<span class=\"button-text follow-text\">Follow</span> " +



                        //foreach (var child in foll)
                        //{
                        //    foreach (var childItem in child["ids"])
                        //    {
                        //        string pl = childItem.ToString();
                        //    }
                        //}



                        //jstring += "<span class=\"button-text follow-text\">Following</span>";
                        //jstring += "<span class=\"button-text unfollow-text\">Unfollow</span>";

                        jstring += "</div>" +
                              "</div>";
                        jstring += "</div></div>";
                        jstring += "<div class=\"profile-social-proof\"><div class=\"follow-bar\"></div></div></div>";
                        jstring += "<ol id=\"twitterUserTweets\" class=\"recent-tweets\">" +

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
                    jstring += "<span class=\"url editable-group\">  <span class=\"profile-field\"><a title=\"#\" href=\"http://graph.facebook.com/" + item["id"] + "\" rel=\"me nofollow\" target=\"_blank\">" + item["link"] + " </a>";
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
                                  "<small><a href=\"http://graph.facebook.com/" + item["id"] + "\" target=\"_blank\" class=\"view_profile\">Go to full profile →</a></small>" +
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
    }
}