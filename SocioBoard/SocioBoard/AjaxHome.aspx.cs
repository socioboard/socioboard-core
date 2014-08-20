using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Model;
using SocioBoard.Domain;
using System.Collections;
using Facebook;
using GlobusTwitterLib.Authentication;
using SocioBoard.Helper;
using GlobusTwitterLib.App.Core;
using GlobusLinkedinLib.Authentication;
using GlobusLinkedinLib.LinkedIn.Core.SocialStreamMethods;
using Newtonsoft.Json.Linq;
using System.Configuration;
using GlobusTwitterLib.Twitter.Core.TweetMethods;
using GlobusTwitterLib.Twitter.Core.UserMethods;
using GlobusTwitterLib.Twitter.Core.FriendshipMethods;
using System.Data;
using System.IO;
using log4net;
using SocioBoard.Message;
using GlobusTumblrLib.Authentication;
using GlobusTumblerLib.Tumblr.Core.BlogMethods;

namespace SocialSuitePro
{
    public partial class AjaxHome : System.Web.UI.Page
    {
        public static int profilelimit = 0;
        ILog logger = LogManager.GetLogger(typeof(AjaxHome));
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ProcessRequest();
            }
            catch (Exception Err)
            {
                Console.WriteLine(Err.Message);
                logger.Error(Err.Message);
            }
        }
        public void ProcessRequest()
        {

            //experimental code selected index changed of dropdown
            if (!string.IsNullOrEmpty(Request.QueryString["groupsselection"]))
            {
                SocioBoard.Domain.User user = (SocioBoard.Domain.User)Session["LoggedUser"];

                string selectedValue = Request.QueryString["groupsselection"];

                TeamRepository objTeamRepository = new TeamRepository();
                Team lstDetails = objTeamRepository.getAllGroupsDetails(user.EmailId.ToString(), Guid.Parse(selectedValue), user.Id);

                Session["GroupName"] = lstDetails;
                Session["groupcheck"] = selectedValue;

                SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)Session["GroupName"];

                Response.Write(selectedValue);

                return;
            }

            SocialProfilesRepository socio = new SocialProfilesRepository();
            List<SocialProfile> alstsocioprofiles = new List<SocialProfile>();
            TeamRepository objTeamRepo = new TeamRepository();


            if (!string.IsNullOrEmpty(Request.QueryString["op"]))
            {

                SocioBoard.Domain.User user = (SocioBoard.Domain.User)Session["LoggedUser"];
                Team team = (SocioBoard.Domain.Team)Session["GroupName"];

                TeamRepository objTeamRepository = new TeamRepository();
                TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
                GroupRepository objGroupRepository = new GroupRepository();


                if (Request.QueryString["op"] == "social_connectivity")
                {
                    List<TeamMemberProfile> allprofiles = objTeamMemberProfileRepository.getAllTeamMemberProfilesOfTeam(team.Id);

                    string profiles = string.Empty;
                    profiles += "<ul class=\"rsidebar-profile\">";
                    foreach (TeamMemberProfile item in allprofiles)
                    {
                        try
                        {
                            if (item.ProfileType == "facebook")
                            {
                                try
                                {
                                    FacebookAccountRepository facerepo = new FacebookAccountRepository();
                                    FacebookAccount faceaccount = facerepo.getFacebookAccountDetailsById(item.ProfileId);

                                    if (faceaccount != null)
                                    {
                                        profiles += "<li id=\"so_" + item.ProfileId + "\"><div id=\"" + item.ProfileId + "\" class=\"userpictiny\"><div class=\"delet_icon\" onclick=\"confirmDel('" + item.ProfileId + "','" + faceaccount.Type + "','fb');\"></div><a href=\"http://www.facebook.com/" + faceaccount.FbUserId + "\" target=\"_blank\"><img src=\"http://graph.facebook.com/" + item.ProfileId + "/picture?type=small\" height=\"48\" width=\"48\" alt=\"\" title=\"" + faceaccount.FbUserName + "\" /></a>" +
                                                    "<a href=\"#\" class=\"userurlpic\" title=\"\"><img src=\"../Contents/img/fb_icon.png\" width=\"16\" height=\"16\" alt=\"\"></a></div></li>";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    logger.Error(ex.Message);
                                }

                            }
                            else if (item.ProfileType == "youtube")
                            {
                                try
                                {
                                    YoutubeAccountRepository facerepo = new YoutubeAccountRepository();
                                    YoutubeAccount youtube = facerepo.getYoutubeAccountDetailsById(item.ProfileId);

                                    if (youtube != null)
                                    {
                                        profiles += "<li id=\"so_" + item.ProfileId + "\"><div id=\"" + item.ProfileId + "\" class=\"userpictiny\"><div class=\"delet_icon\" onclick=\"confirmDel('" + item.ProfileId + "','youtube','youtube');\"></div><a href=\"https://plus.google.com/" + youtube.Ytuserid + "\" target=\"_blank\"><img src=\"" + youtube.Ytprofileimage + " height=\"48\" width=\"48\" alt=\"\" title=\"" + youtube.Ytusername + "\" /></a>" +
                                                    "<a href=\"#\" class=\"userurlpic\" title=\"\"><img src=\"../Contents/img/youtube.png\" width=\"16\" height=\"16\" alt=\"\"></a></div></li>";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    logger.Error(ex.Message);
                                }

                            }
                            else if (item.ProfileType == "tumblr")
                            {
                                try
                                {

                                    TumblrAccountRepository tumbrepo = new TumblrAccountRepository();
                                    TumblrAccount tumblraccount = tumbrepo.getTumblrAccountDetailsById(item.ProfileId);

                                    if (tumblraccount != null)
                                    {
                                        //if (tumblraccount!=null)
                                        //{
                                        //    profiles += "<li id=\"so_" + item.ProfileId + "\"><div id=\"" + item.ProfileId + "\" class=\"userpictiny\"><div class=\"delet_icon\" onClick=\"confirmDel('" + item.ProfileId + "','tumblr','tumblr')\"></div><a href=\"http://twitter.com/" + tumblraccount.tblrUserName + "\" target=\"_blank\"><img src=\"http://api.tumblr.com/v2/blog/" + tumblraccount.tblrUserName + ".tumblr.com/avatar\" height=\"48\" width=\"48\" alt=\"\" title=\"" + tumblraccount.tblrUserName + "\" /></a>" +
                                        //                "<a href=\"#\" class=\"userurlpic\" title=\"\"><img src=\"../Contents/img/tumblr.png\" width=\"16\" height=\"16\" alt=\"\"></a></div></li>";
                                        //}
                                        //else
                                        //{
                                        profiles += "<li id=\"so_" + item.ProfileId + "\"><div id=\"" + item.ProfileId + "\" class=\"userpictiny\"><div class=\"delet_icon\" onClick=\"confirmDel('" + item.ProfileId + "','tumblr','tumblr')\"></div><a href=\"http://" + tumblraccount.tblrUserName + ".tumblr.com\"  target=\"_blank\"><img src=\"http://api.tumblr.com/v2/blog/" + tumblraccount.tblrUserName + ".tumblr.com/avatar\" height=\"48\" width=\"48\" alt=\"\" title=\"" + tumblraccount.tblrUserName + "\" /></a>" +
                                                        "<a href=\"#\" class=\"userurlpic\" title=\"\"><img src=\"../Contents/img/tumblr.png\" width=\"16\" height=\"16\" alt=\"\"></a></div></li>";
                                        // }

                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    logger.Error(ex.Message);
                                }

                            }


                            else if (item.ProfileType == "twitter")
                            {
                                try
                                {
                                    TwitterAccountRepository twtrepo = new TwitterAccountRepository();
                                    SocioBoard.Domain.TwitterAccount twtaccount = twtrepo.getUserInformation(item.ProfileId);
                                    if (twtaccount != null)
                                    {
                                        profiles += "<li id=\"so_" + item.ProfileId + "\"><div id=\"" + item.ProfileId + "\" class=\"userpictiny\"><div class=\"delet_icon\" onClick=\"confirmDel('" + item.ProfileId + "','twt','twt')\"></div><a href=\"http://twitter.com/" + twtaccount.TwitterScreenName + "\" target=\"_blank\"><img src=\"" + twtaccount.ProfileImageUrl + "\" height=\"48\" width=\"48\" alt=\"\" title=\"" + twtaccount.TwitterScreenName + "\" /></a>" +
                                                    "<a href=\"#\" class=\"userurlpic\" title=\"\"><img src=\"../Contents/img/twticon.png\" width=\"16\" height=\"16\" alt=\"\"></a></div></li>";
                                    }

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    logger.Error(ex.Message);
                                }
                            }
                            else if (item.ProfileType == "linkedin")
                            {
                                try
                                {

                                    LinkedInAccountRepository liRepo = new LinkedInAccountRepository();
                                    string access = string.Empty, tokenSecrate = string.Empty, LdprofileName = string.Empty, LdPreofilePic = string.Empty;
                                    LinkedInAccount liaccount = liRepo.getUserInformation(item.ProfileId);

                                    if (liaccount != null)
                                    {
                                        if (!string.IsNullOrEmpty(liaccount.ProfileImageUrl))
                                        {
                                            LdPreofilePic = liaccount.ProfileImageUrl;
                                        }
                                        else
                                        {
                                            LdPreofilePic = "../../Contents/img/blank_img.png";
                                        }


                                        profiles += "<li id=\"so_" + item.ProfileId + "\"><div id=\"" + item.ProfileId + "\" class=\"userpictiny\"><div class=\"delet_icon\" onClick=\"confirmDel('" + item.ProfileId + "','linkedin')\"></div><a href=\"" + liaccount.ProfileUrl + "\" target=\"_blank\"><img src=\"" + LdPreofilePic + "\" height=\"48\" width=\"48\" alt=\"\" title=\"" + liaccount.LinkedinUserName + "\" /></a>" +
                                                   "<a href=\"#\" class=\"userurlpic\" title=\"\"><img src=\"../Contents/img/link_icon.png\" width=\"16\" height=\"16\" alt=\"\"></a></div></li>";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    logger.Error(ex.Message);
                                }

                            }

                            else if (item.ProfileType == "instagram")
                            {
                                try
                                {
                                    InstagramAccountRepository objInsAccRepo = new InstagramAccountRepository();
                                    InstagramAccount objInsAcc = objInsAccRepo.getInstagramAccountDetailsById(item.ProfileId);
                                    string accessToken = string.Empty;
                                    if (objInsAcc != null)
                                    {
                                        profiles += "<li id=\"so_" + item.ProfileId + "\"><div id=\"" + item.ProfileId + "\" class=\"userpictiny\"><div class=\"delet_icon\" onClick=\"confirmDel('" + item.ProfileId + "','instagram')\"></div><a href=\"http://instagram.com/" + objInsAcc.InsUserName + "\" target=\"_blank\"><img src=\"" + objInsAcc.ProfileUrl + "\" height=\"48\" width=\"48\" alt=\"\" title=\"" + objInsAcc.InsUserName + "\" /></a>" +
                                                    "<a href=\"#\" class=\"userurlpic\" title=\"\"><img src=\"../Contents/img/instagram_24X24.png\" width=\"16\" height=\"16\" alt=\"\"></a></div></li>";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    logger.Error(ex.Message);
                                }
                            }
                            else if (item.ProfileType == "googleplus")
                            {
                                try
                                {

                                    //GooglePlusAccountRepository objgpAccRepo = new GooglePlusAccountRepository();
                                    //GooglePlusAccount objgpAcc = objgpAccRepo.getGooglePlusAccountDetailsById(item.ProfileId, user.Id);
                                    //string accessToken = string.Empty;

                                    //profiles += "<li id=\"so_" + item.ProfileId + "\"><div id=\"" + item.ProfileId + "\" class=\"userpictiny\"><div class=\"delet_icon\" onClick=\"confirmDel('" + item.ProfileId + "','googleplus')\"></div><a href=\"http://plus.google.com/" + item.ProfileId + "\" target=\"_blank\"><img src=\"" + objgpAcc.GpProfileImage + "\" height=\"48\" width=\"48\" alt=\"\" title=\"" + objgpAcc.GpUserName + "\" /></a>" +
                                    //            "<a href=\"#\" class=\"userurlpic\" title=\"\"><img src=\"../Contents/img/google_plus.png\" width=\"16\" height=\"16\" alt=\"\"></a></div></li>";

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    logger.Error(ex.Message);
                                }
                            }
                            else if (item.ProfileType == "googleanalytics")
                            {
                                try
                                {

                                    //GoogleAnalyticsAccountRepository objgaAccRepo = new GoogleAnalyticsAccountRepository();
                                    //GoogleAnalyticsAccount objgaAcc = objgaAccRepo.getGoogelAnalyticsAccountHomeDetailsById(user.Id,item.ProfileId);
                                    //string accessToken = string.Empty;

                                    //profiles += "<li id=\"so_" + item.ProfileId + "\"><div id=\"" + item.ProfileId + "\" class=\"userpictiny\"><div class=\"delet_icon\" onClick=\"confirmDel('" + item.ProfileId + "','googleanalytics')\"></div><a href=\"http://plus.google.com/" + item.ProfileId + "\" target=\"_blank\"><img src=\"../Contents/img/google_analytics.png\" height=\"48\" width=\"48\" alt=\"\" title=\"" + objgaAcc.GaAccountName + "\" /></a>" +
                                    //            "<a href=\"#\" class=\"userurlpic\" title=\"\"><img src=\"\" width=\"16\" height=\"16\" alt=\"\"></a></div></li>";

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    logger.Error(ex.Message);
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);

                        }
                    } profiles += "</ul>";
                    Response.Write(profiles);
                }
                else if (Request.QueryString["op"] == "woodrafts")
                {
                    string message = string.Empty;
                    try
                    {
                        DraftsRepository draftsRepository = new DraftsRepository();
                        List<Drafts> lstDrafts = draftsRepository.getAllDrafts(team.GroupId);
                        string profurl = string.Empty;
                        if (string.IsNullOrEmpty(user.ProfileUrl))
                        {
                            profurl = "../Contents/img/blank_img.png";
                        }
                        else
                        {
                            profurl = user.ProfileUrl;
                        }
                        if (lstDrafts.Count != 0)
                        {
                            foreach (Drafts item in lstDrafts)
                            {
                                try
                                {
                                    message += "<section class=\"section\" style=\"width:648px;\">" +
                                                                       "<div class=\"js-task-cont read\"><section class=\"task-owner\">" +
                                                            "<img width=\"32\" height=\"32\" border=\"0\" class=\"avatar\" src=\"../Contents/img/task_pin.png\">" +
                                        //  "<img width=\"32\" height=\"32\" border=\"0\" class=\"avatar\" src=\"" + profurl + "\" />" +
                                                            "</section><section class=\"task-activity third\" style=\"width: 19.6%;\"><p>" + user.UserName + "</p><div>" + item.CreatedDate + " </div><p>" +
                                        //"</p></section><section style=\"margin-right: 6px; height: auto; width: 35%;\" class=\"task-message font-13 third\"><a onclick=\"writemessage(this.innerHTML);\" class=\"tip_left\">" + item.Message + "</a></section>" +
                                                             "</p></section><section style=\"margin-right: 6px; height: auto; width: 31%;\" class=\"task-message font-13 third\"><span class=\"tip_left\">" + gethtmlfromstring(item.Message) + "</span></section>" +
                                                                "<div class=\"userpictiny\" style=\"height:70px; margin-top: 0;\"><img alt=\"\" src=\"" + profurl + "\" />" +
                                                             "</div>" +
                                                             "<a class=\"small_remove icon publish_delete\" href=\"#\" style=\"top: 7px; float: right; margin-top: 13px; margin-right: 10px;\" title=\"Delete\" onclick=\"deleteDraftMessage('" + item.Id + "')\"></a>" +
                                                            "<section style=\"margin-top: 18px; width: 45px; margin-right: 17px;\" class=\"task-status\"><div class=\"ui_light floating task_status_change\"><a class=\"ui-sproutmenu\" href=\"#nogo\">" +
                                                                "<span onclick=\"editDraftsMessage('" + item.Id + "','" + item.Message + "');\" class=\"ui-sproutmenu-status\">" +
                                                            "<img class=\"edit_button\" src=\"../Contents/img/icon_edit.png\" alt=\"\" />" +
                                                            "</span>" +
                                                            "</a></div></section></div></section>";
                                }
                                catch (Exception ex)
                                {

                                    logger.Error(ex.Message);
                                }
                            }
                        }
                        else
                        {
                            message += "<div style=\"margin-left: 2%; margin-top: 3%;\">No Messages in Drafts</div>";
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                    Response.Write(message);
                }
                else if (Request.QueryString["op"] == "savedrafts")
                {
                    try
                    {
                        string message = Request.QueryString["message"];
                        message = Request.Form["messagee"];
                        Drafts d = new Drafts();
                        d.CreatedDate = DateTime.Now;
                        d.Message = message;
                        d.ModifiedDate = DateTime.Now;
                        d.UserId = user.Id;
                        d.GroupId = team.GroupId;
                        d.Id = Guid.NewGuid();
                        DraftsRepository dRepo = new DraftsRepository();
                        if (!dRepo.IsDraftsMessageExist(user.Id, message))
                        {
                            dRepo.AddDrafts(d);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                    Response.Write("added successfully");
                }



                else if (Request.QueryString["op"] == "midsnaps")
                {
                    try
                    {
                        Random rNum = new Random();
                        string loadtype = Request.QueryString["loadtype"];
                        string midsnaps = string.Empty;
                        if (loadtype == "load")
                            profilelimit = 0;

                        if (profilelimit != -1)
                        {
                            // Team lstDetails = objTeamRepository.getAllDetails(team.GroupId, team.EmailId);

                            // List<TeamMemberProfile> alst = objTeamMemberProfileRepository.getAllTeamMemberProfilesOfTeam(team.Id);
                            ArrayList alst = objTeamMemberProfileRepository.getLimitProfilesOfUser(team.Id, profilelimit);


                            // ArrayList alst = socio.getLimitProfilesOfUser(user.Id, profilelimit);
                            if (alst.Count == 0)
                                profilelimit = -1;
                            else if (profilelimit == 0)
                                //profilelimit += 2;
                                profilelimit += 6;
                            else
                                profilelimit += 6;
                            midsnaps += "<div class=\"row-fluid\" >";
                            if (loadtype == "load")
                            {
                                AdsRepository objAdsRepo = new AdsRepository();
                                ArrayList lstads = objAdsRepo.getAdsForHome();
                                int i = 0;
                                if (lstads.Count <= 1)
                                {
                                    if (user.PaymentStatus.ToUpper() == "PAID")
                                    {
                                        midsnaps += "";
                                    }
                                }
                                else
                                {
                                    foreach (var item in lstads)
                                    {
                                        Array temp = (Array)item;
                                        i++;
                                        if (temp != null)
                                        {
                                            if (i == 2)
                                            {
                                                if (user.AccountType == "Paid")
                                                {
                                                    midsnaps += "<div class=\"span4 rounder recpro\"><button data-dismiss=\"alert\" class=\"close pull-right\" type=\"button\">×</button>" +
                                              "<a href=\"#\"><img src=\"" + temp.GetValue(2).ToString() + "\"  alt=\"\" style=\"width:246px;height:331px\"></a></div>";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            foreach (TeamMemberProfile item in alst)
                            {
                                if (item.ProfileType == "facebook")
                                {
                                    try
                                    {
                                        FacebookAccountRepository fbrepo = new FacebookAccountRepository();
                                        FacebookFeedRepository facefeedrepo = new FacebookFeedRepository();
                                        List<FacebookFeed> fbmsgs = facefeedrepo.getAllFacebookUserFeeds(item.ProfileId);
                                        FacebookAccount fbaccount = fbrepo.getFacebookAccountDetailsById(item.ProfileId);
                                        midsnaps += "<div id=\"mid_" + item.ProfileId + "\" style=\"height:213px;\" class=\"span4 rounder recpro\"><div class=\"concotop\">" +
                                                   "<div onclick=\"detailsdiscoveryfacebook('" + fbaccount.FbUserId + "');\"  class=\"userpictiny\"><img width=\"56\" height=\"56\" title=\"" + fbaccount.FbUserName + "\" alt=\"\" src=\"http://graph.facebook.com/" + item.ProfileId + "/picture?type=small\"\">" +
                                                   "<a title=\"\" class=\"userurlpic\" href=\"#\"><img alt=\"\" src=\"../Contents/img/fb_icon.png\" width=\"16\" height=\"16\"></a></div>" +
                                                   "<div onclick=\"detailsdiscoveryfacebook('" + fbaccount.FbUserId + "');\" class=\"useraccname\">" + getsortpofilename(fbaccount.FbUserName) + "</div><div class=\"usercounter\">" +
                                                   "<div class=\"userfoll\">" + fbaccount.Friends;
                                        if (fbaccount.Type == "page")
                                        {
                                            midsnaps += "<span><b style=\"font-size: 13px;\">Fans</b></span>";
                                        }
                                        else
                                        {
                                            midsnaps += "<span><b style=\"font-size: 13px;\">Friends</b></span>";
                                        }
                                        midsnaps += "</div>" +
                                                   "<div class=\"userppd\">" + Math.Round(rNum.NextDouble(), 2) + "<span>Avg. Post <br> Per Day</span></div></div><h5>Recent message</h5></div>" +
                                                   "<div class=\"concoteng\"> <ul class=\"mess\">";
                                        if (fbmsgs.Count != 0)
                                        {
                                            int msgcount = 0;
                                            foreach (FacebookFeed child in fbmsgs)
                                            {
                                                string mess = string.Empty;
                                                if (msgcount < 2)
                                                {
                                                    if (child.FeedDescription.Length > 40)
                                                    {
                                                        mess = child.FeedDescription.Substring(0, 39);
                                                        mess = mess + "...........";
                                                    }
                                                    else
                                                    {
                                                        mess = child.FeedDescription;
                                                    }
                                                    midsnaps += "<li><div class=\"messpic\"><img title=\"\" alt=\"\" src=\"http://graph.facebook.com/" + child.FromId + "/picture?type=small\"></div>" +
                                                              "<div class=\"messtext\">" + mess + "</div></li>";
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                                msgcount++;
                                            }
                                        }
                                        else
                                        {
                                            midsnaps += "<strong>No messages were found within the past few days.</strong> \"Messages will be displayed once there is activity in this date range.\"";
                                        }

                                        midsnaps += "</ul></div></div>";
                                    }
                                    catch (Exception ex)
                                    {
                                        logger.Error(ex.Message);
                                    }
                                }


                                else if (item.ProfileType == "googleplus")
                                {

                                }
                                else if (item.ProfileType == "twitter")
                                {
                                    TwitterAccountRepository twtrepo = new TwitterAccountRepository();
                                    SocioBoard.Domain.TwitterAccount twtaccount = twtrepo.getUserInformation(item.ProfileId);
                                    TwitterMessageRepository twtmsgrepo = new TwitterMessageRepository();
                                    List<TwitterMessage> lsttwtmsgs = twtmsgrepo.getAllTwitterMessagesOfProfile(item.ProfileId);
                                    int tweetcount = 0;

                                    midsnaps += "<div id=\"mid_" + item.ProfileId + "\" style=\"height:213px;\" class=\"span4 rounder recpro\"><div class=\"concotop\">" +
                                     "<div onclick=\"detailsdiscoverytwitter('" + item.ProfileId + "');\" class=\"userpictiny\"><img width=\"56\" height=\"56\" title=\"" + twtaccount.TwitterScreenName + "\" alt=\"\" src=\"" + twtaccount.ProfileImageUrl + "\">" +
                                     "<a title=\"\" class=\"userurlpic\" href=\"#\"><img alt=\"\" src=\"../Contents/img/twticon.png\" width=\"16\" height=\"16\"></a></div>" +
                                     "<div onclick=\"detailsdiscoverytwitter('" + twtaccount.TwitterUserId + "');\" class=\"useraccname\">" + getsortpofilename(twtaccount.TwitterScreenName) + "</div><div class=\"usercounter\">" +
                                     "<div class=\"userfoll\">" + twtaccount.FollowersCount + "<span><b style=\"font-size: 13px;\">Followers</b></span></div>" +
                                     "<div class=\"userppd\">" + Math.Round(rNum.NextDouble(), 2) + "<span>Avg. tweet <br> Per Day</span></div></div><h5>Recent message</h5></div>" +
                                     "<div class=\"concoteng\"> <ul class=\"mess\">";
                                    try
                                    {
                                        if (lsttwtmsgs.Count == 0)
                                        {
                                            midsnaps += "<strong>No messages were found within the past few days.</strong> \"Messages will be displayed once there is activity in this date range.\"";
                                        }
                                        else
                                        {
                                            foreach (TwitterMessage msg in lsttwtmsgs)
                                            {
                                                if (tweetcount < 2)
                                                {
                                                    try
                                                    {
                                                        string ms = string.Empty;
                                                        if (msg.TwitterMsg.Length > 20)
                                                        {
                                                            ms = msg.TwitterMsg.Substring(0, 20) + "...";

                                                        }
                                                        else
                                                        {
                                                            ms = msg.TwitterMsg;
                                                        }
                                                        midsnaps += "<li><div class=\"messpic\"><img title=\"\" alt=\"\" src=\"" + msg.FromProfileUrl + "\"></div>" +
                                                        "<div class=\"messtext\">" + ms + "</div></li>";
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Console.WriteLine(ex.Message);

                                                    }
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                                tweetcount++;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                        Console.WriteLine(ex.Message);
                                        logger.Error(ex.Message);
                                    }
                                    midsnaps += "</ul></div></div>";
                                }


                                else if (item.ProfileType == "tumblr")
                                {
                                    try
                                    {
                                        string PostCount = string.Empty;
                                        string LikesCount = string.Empty;

                                        TumblrAccountRepository tumblrrepo = new TumblrAccountRepository();
                                        SocioBoard.Domain.TumblrAccount tumblrccount = tumblrrepo.getTumblrAccountDetailsById(item.ProfileId);
                                        TumblrFeedRepository tumblrfeedrepo = new TumblrFeedRepository();
                                        List<TumblrFeed> lsttumblrmsgs = tumblrfeedrepo.getFeedOfProfile(item.ProfileId);
                                        BlogInfo objBlogInfo = new BlogInfo();
                                        string objData = objBlogInfo.getTumblrUserInfo(tumblrccount.tblrUserName);
                                        //string objFollower = objBlogInfo.getTumblrUserfollower(tumblrccount.tblrUserName);
                                        string[] words = objData.Split('&');

                                        PostCount = words[1].ToString();
                                        LikesCount = words[0].ToString();




                                        midsnaps += "<div id=\"mid_" + item.ProfileId + "\" style=\"height:213px;\" class=\"span4 rounder recpro\"><div class=\"concotop\">" +
                                                    "<div  onclick=\"detailsdiscoveryTumblr('" + item.ProfileId + "')\" class=\"userpictiny\"><img width=\"56\" height=\"56\" title=\"" + tumblrccount.tblrUserName + "\" alt=\"\" src=\"http://api.tumblr.com/v2/blog/" + tumblrccount.tblrUserName + ".tumblr.com/avatar\">" +
                                                    "<a title=\"\" class=\"userurlpic\" href=\"#\"><img alt=\"\" src=\"../Contents/img/tumblr.png\" width=\"16\" height=\"16\"></a></div>" +
                                                    "<a  href=\"http://" + item.ProfileId + ".tumblr.com\" target=\"_blank\"><div class=\"useraccname\">" + getsortpofilename(tumblrccount.tblrUserName) + "</div></a></div>" +
                                                    "<div class=\"concoteng\"><div class=\"pillow_fade\">" +
                                                    " <div class=\"fb_notifications\">" +
                                                    "<ul class=\"user-stats\"> " +
                                                         "<li><div class=\"photo_stat\">  post</div>  <div class=\"number-stat\">" + PostCount + "</div></li>" +
                                                         "<li><div class=\"photo_stat\">likes</div><div class=\"number-stat\">" + LikesCount + "</div></li>" +
                                                     "</ul></div></div></div></div>";


                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        logger.Error(ex.Message);
                                    }
                                }





                                else if (item.ProfileType == "youtube")
                                {
                                    try
                                    {


                                        YoutubeAccountRepository ytrepo = new YoutubeAccountRepository();
                                        YoutubeChannelRepository objYoutubeChannelRepository = new YoutubeChannelRepository();
                                        SocioBoard.Domain.YoutubeAccount ytacount = ytrepo.getYoutubeAccountDetailsById(item.ProfileId);
                                        YoutubeChannel objYoutubeChannel = objYoutubeChannelRepository.getYoutubeChannelDetailsById(item.ProfileId);

                                        if (string.IsNullOrEmpty(ytacount.Ytprofileimage))
                                        {
                                            ytacount.Ytprofileimage = "../../Contents/img/blank_img.png";
                                        }

                                        midsnaps += "<div id=\"mid_" + item.ProfileId + "\" style=\"height:213px;\" class=\"span4 rounder recpro\"><div class=\"concotop\">" +
                                               "<div  onclick=\"detailsdiscoveryYoutube('" + ytacount.Ytuserid + "')\" class=\"userpictiny\"><img width=\"56\" height=\"56\" title=\"" + ytacount.Ytuserid + "\" alt=\"\" src=\"" + ytacount.Ytprofileimage + "\">" +
                                                  "<a title=\"\" class=\"userurlpic\" href=\"#\"><img alt=\"\" src=\"../Contents/img/youtube.png\" width=\"16\" height=\"16\"></a></div>" +
                                                  "<div  onclick=\"detailsdiscoveryYoutube('" + ytacount.Ytuserid + "')\" class=\"useraccname\">" + getsortpofilename(ytacount.Ytusername) + "</div></div>" +
                                                  "<div class=\"concoteng\"><div class=\"pillow_fade\">" +
                                                  " <div class=\"fb_notifications\">" +
                                                  "<ul class=\"user-stats\"> " +
                                                       "<li><div class=\"photo_stat\">Total View</div>  <div class=\"number-stat\">" + objYoutubeChannel.ViewCount + "</div></li>" +
                                                       "<li><div class=\"photo_stat\">Total Subcriber</div><div class=\"number-stat\">" + objYoutubeChannel.SubscriberCount + "</div></li>" +
                                                       "<li><div class=\"photo_stat\">Total Video</div><div class=\"number-stat\">" + objYoutubeChannel.VideoCount + "</div></li>" +
                                                   "</ul></div></div></div></div>";

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        logger.Error(ex.Message);
                                    }
                                }









                                else if (item.ProfileType == "linkedin")
                                {
                                    try
                                    {
                                        string access = string.Empty, tokenSecrate = string.Empty, LdprofileName = string.Empty, LdPreofilePic = string.Empty;
                                        LinkedInAccountRepository objLiRepo = new LinkedInAccountRepository();
                                        LinkedInFeedRepository objliFeedRepo = new LinkedInFeedRepository();
                                        LinkedInAccount liAccount = objLiRepo.getUserInformation(item.ProfileId);
                                        LinkedInFeedRepository lifeedrepo = new LinkedInFeedRepository();
                                        List<LinkedInFeed> alstliaccount = lifeedrepo.getAllLinkedInFeedsOfProfile(item.ProfileId);
                                        if (liAccount != null)
                                        {
                                            LdprofileName = liAccount.LinkedinUserName;
                                            LdPreofilePic = liAccount.ProfileImageUrl;
                                        }

                                        if (string.IsNullOrEmpty(LdPreofilePic))
                                        {
                                            LdPreofilePic = "../../Contents/img/blank_img.png";
                                        }
                                        int linkedinConcount = 0;
                                        try
                                        {
                                            linkedinConcount = liAccount.Connections;
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }

                                        midsnaps += "<div id=\"mid_" + item.ProfileId + "\" style=\"height:213px;\" class=\"span4 rounder recpro\"><div class=\"concotop\">" +
                                           "<div onclick=\"detailsdiscoverylnk('" + liAccount.LinkedinUserId + "')\" class=\"userpictiny\"><img width=\"56\" height=\"56\" title=\"" + LdprofileName + "\" alt=\"\" src=\"" + LdPreofilePic + "\">" +
                                           "<a title=\"\" class=\"userurlpic\" href=\"#\"><img alt=\"\" src=\"../Contents/img/link_icon.png\" width=\"16\" height=\"16\"></a></div>" +
                                           "<div class=\"useraccname\">" + getsortpofilename(LdprofileName) + "</div><div class=\"usercounter\">" +
                                           "<div class=\"userfoll\">" + linkedinConcount + "<span>Connections</span></div>" +
                                           "<div class=\"userppd\">" + Math.Round(rNum.NextDouble(), 2) + "<span>Avg. Post <br> Per Day</span></div></div><h5>Recent message</h5></div>" +
                                           "<div class=\"concoteng\"> <ul class=\"mess\">";
                                        int link = 0;
                                        if (alstliaccount.Count == 0)
                                        {
                                            midsnaps += "<strong>No messages were found within the past 14 days.</strong> \"Messages will be displayed once there is activity in this date range.\"";
                                        }
                                        else
                                        {

                                            try
                                            {

                                                foreach (LinkedInFeed liFeed in alstliaccount)
                                                {
                                                    try
                                                    {
                                                        if (link < 2)
                                                        {
                                                            string ms = string.Empty;
                                                            if (liFeed.Feeds.Length > 20)
                                                            {
                                                                ms = liFeed.Feeds.Substring(0, 20) + "...";

                                                            }
                                                            else
                                                            {
                                                                ms = liFeed.Feeds;
                                                            }
                                                            midsnaps += "<li><div class=\"messpic\"><img title=\"\" alt=\"\" src=\"" + liFeed.FromPicUrl + "\"></div>" +
                                                           "<div class=\"messtext\">" + ms + "</div></li>";
                                                            link++;
                                                        }
                                                        else
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        logger.Error(ex.Message);
                                                    }
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                logger.Error(ex.Message);
                                            }

                                        }
                                        midsnaps += "</ul></div></div>";
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        logger.Error(ex.Message);
                                    }
                                }
                                else if (item.ProfileType == "instagram")
                                {
                                    try
                                    {
                                        InstagramAccountRepository objInsAccRepo = new InstagramAccountRepository();
                                        InstagramAccount objInsAcc = objInsAccRepo.getInstagramAccountDetailsById(item.ProfileId);

                                        midsnaps += "<div id=\"mid_" + item.ProfileId + "\" style=\"height:213px;\" class=\"span4 rounder recpro\"><div class=\"concotop\">" +
                                               "<div onclick=\"detailsdiscoveryInstagram('" + item.ProfileId + "')\" class=\"userpictiny\"><img width=\"56\" height=\"56\" title=\"" + objInsAcc.InsUserName + "\" alt=\"\" src=\"" + objInsAcc.ProfileUrl + "\">" +
                                               "<a title=\"\" class=\"userurlpic\" href=\"#\"><img alt=\"\" src=\"../Contents/img/instagram_24X24.png\" width=\"16\" height=\"16\"></a></div>" +
                                               "<a href=\"http://instagram.com/" + objInsAcc.InsUserName + "\" target=\"_blank\"><div class=\"useraccname\">" + getsortpofilename(objInsAcc.InsUserName) + "</div></a></div>" +
                                               "<div class=\"concoteng\"><div class=\"pillow_fade\">" +
                                               " <div class=\"fb_notifications\">" +
                                               "<ul class=\"user-stats\"> " +
                                                    "<li><div class=\"photo_stat\">  photos</div>  <div class=\"number-stat\">" + objInsAcc.TotalImages + "</div></li>" +
                                                    "<li><div class=\"photo_stat\">following</div><div class=\"number-stat\">" + objInsAcc.Followers + "</div></li>" +
                                                    "<li><div class=\"photo_stat\">followers</div><div class=\"number-stat\">" + objInsAcc.FollowedBy + "</div></li>" +
                                                "</ul></div></div></div></div>";
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        logger.Error(ex.Message);
                                    }
                                }
                                else if (item.ProfileType == "googleanalytics")
                                {
                                }
                            }
                            midsnaps += "</div>";
                            Response.Write(midsnaps);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                }

                else if (Request.QueryString["op"] == "accountdelete")
                {
                    Groups lstDetails = objGroupRepository.getGroupName(team.GroupId);
                    if (lstDetails.GroupName == "Socioboard")
                    {

                        Session["facebooktotalprofiles"] = null;
                        SocialProfilesRepository socioprofilerepo = new SocialProfilesRepository();
                        string Profiletype = Request.QueryString["profile"];
                        string profileid = Request.QueryString["profileid"];

                        if (Profiletype == "fb")
                        {
                            try
                            {
                                FacebookAccountRepository fbrepo = new FacebookAccountRepository();
                                int delaccFromTeamMemberProfile = objTeamMemberProfileRepository.DeleteTeamMemberProfileByUserid(profileid);

                                int delacc = fbrepo.deleteFacebookUser(profileid, user.Id);
                                if (delacc > 0)
                                {

                                    socioprofilerepo.deleteProfile(user.Id, profileid);
                                    List<SocialProfile> lstsocioprofile = socioprofilerepo.checkProfileExistsMoreThanOne(profileid);
                                    if (lstsocioprofile.Count >= 0)
                                    {
                                        try
                                        {
                                            FacebookMessageRepository fbmsgrepo = new FacebookMessageRepository();
                                            fbmsgrepo.deleteAllMessagesOfUser(profileid, user.Id);
                                            FacebookFeedRepository fbfeedrepo = new FacebookFeedRepository();
                                            fbfeedrepo.deleteAllFeedsOfUser(profileid, user.Id);
                                            FacebookStatsRepository fbstatsrepo = new FacebookStatsRepository();
                                            fbstatsrepo.deleteFacebookStats(profileid, user.Id);


                                            ScheduledMessageRepository schedulemsgrepo = new ScheduledMessageRepository();
                                            schedulemsgrepo.deleteMessage(user.Id, profileid);
                                            ArchiveMessageRepository archmsgrepo = new ArchiveMessageRepository();
                                            int del = archmsgrepo.DeleteArchiveMessage(user.Id, profileid);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.StackTrace);
                                        }



                                    }
                                }



                            }
                            catch (Exception exx)
                            {
                                Console.WriteLine(exx.Message);
                                logger.Error(exx.Message);
                            }
                        }



                        else if (Profiletype == "tumblr")
                        {
                            try
                            {
                                TumblrAccountRepository tumblraccountrepo = new TumblrAccountRepository();
                                TumblrFeedRepository objTumblrFeedRepository = new TumblrFeedRepository();
                                int deltwtacc = tumblraccountrepo.deleteTumblrUser(profileid, user.Id);
                                int delaccFromTeamMemberProfile = objTeamMemberProfileRepository.DeleteTeamMemberProfileByUserid(profileid);
                                if (deltwtacc > 0)
                                {
                                    try
                                    {
                                        socioprofilerepo.deleteProfile(user.Id, profileid);
                                        objTumblrFeedRepository.DeleteTumblrDataByUserid(user.Id, profileid);
                                        ScheduledMessageRepository schedulemsgrepo = new ScheduledMessageRepository();
                                        schedulemsgrepo.deleteMessage(user.Id, profileid);
                                        ArchiveMessageRepository archmsgrepo = new ArchiveMessageRepository();
                                        int del = archmsgrepo.DeleteArchiveMessage(user.Id, profileid);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.StackTrace);
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.Message);
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else if (Profiletype == "youtube")
                        {
                            try
                            {
                                YoutubeAccountRepository youtubeaccountrepo = new YoutubeAccountRepository();
                                YoutubeChannelRepository objYoutubeChannelRepository = new YoutubeChannelRepository();
                                int deltwtacc = youtubeaccountrepo.deleteYoutubeUser(user.Id, profileid);
                                int delaccFromTeamMemberProfile = objTeamMemberProfileRepository.DeleteTeamMemberProfileByUserid(profileid);
                                if (deltwtacc > 0)
                                {
                                    socioprofilerepo.deleteProfile(user.Id, profileid);
                                    objYoutubeChannelRepository.DeleteProfileDataByUserid(profileid);

                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.Message);
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else if (Profiletype == "twt")
                        {
                            try
                            {
                                TwitterAccountRepository twtaccountrepo = new TwitterAccountRepository();
                                int deltwtacc = twtaccountrepo.deleteTwitterUser(user.Id, profileid);
                                int delaccFromTeamMemberProfile = objTeamMemberProfileRepository.DeleteTeamMemberProfileByUserid(profileid);
                                if (deltwtacc > 0)
                                {
                                    socioprofilerepo.deleteProfile(user.Id, profileid);
                                    List<SocialProfile> lstsocioprofile = socioprofilerepo.checkProfileExistsMoreThanOne(profileid);
                                    if (lstsocioprofile.Count >= 0)
                                    {
                                        try
                                        {
                                            TwitterMessageRepository twtmsgrepo = new TwitterMessageRepository();
                                            int d = twtmsgrepo.deleteTwitterMessage(profileid, user.Id);
                                            TwitterFeedRepository twtfeedrepo = new TwitterFeedRepository();
                                            int s = twtfeedrepo.deleteTwitterFeed(profileid, user.Id);
                                            TwitterStatsRepository twtstatsrepo = new TwitterStatsRepository();
                                            int a = twtstatsrepo.deleteTwitterStats(user.Id, profileid);
                                            TwitterDirectMessageRepository twtdirectmsgrepo = new TwitterDirectMessageRepository();
                                            int f = twtdirectmsgrepo.deleteDirectMessage(user.Id, profileid);

                                            ScheduledMessageRepository schedulemsgrepo = new ScheduledMessageRepository();
                                            schedulemsgrepo.deleteMessage(user.Id, profileid);
                                            ArchiveMessageRepository archmsgrepo = new ArchiveMessageRepository();
                                            int del = archmsgrepo.DeleteArchiveMessage(user.Id, profileid);

                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.StackTrace);
                                        }



                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.Message);
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else if (Profiletype == "linkedin")
                        {
                            try
                            {
                                LinkedInAccountRepository linkedaccrepo = new LinkedInAccountRepository();
                                int delaccFromTeamMemberProfile = objTeamMemberProfileRepository.DeleteTeamMemberProfileByUserid(profileid);
                                int dellinacc = linkedaccrepo.deleteLinkedinUser(profileid, user.Id);
                                if (dellinacc > 0)
                                {


                                    socioprofilerepo.deleteProfile(user.Id, profileid);

                                    List<SocialProfile> lstsocioprofile = socioprofilerepo.checkProfileExistsMoreThanOne(profileid);

                                    if (lstsocioprofile.Count >= 0)
                                    {
                                        try
                                        {
                                            LinkedInFeedRepository linkedfeedrepo = new LinkedInFeedRepository();
                                            int s = linkedfeedrepo.deleteAllFeedsOfUser(profileid, user.Id);
                                            ScheduledMessageRepository schedulemsgrepo = new ScheduledMessageRepository();
                                            schedulemsgrepo.deleteMessage(user.Id, profileid);
                                            ArchiveMessageRepository archmsgrepo = new ArchiveMessageRepository();
                                            int del = archmsgrepo.DeleteArchiveMessage(user.Id, profileid);

                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.StackTrace);
                                        }



                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.Message);
                            }


                        }
                        else if (Profiletype == "instagram")
                        {
                            try
                            {
                                InstagramAccountRepository insaccrepo = new InstagramAccountRepository();
                                int delaccFromTeamMemberProfile = objTeamMemberProfileRepository.DeleteTeamMemberProfileByUserid(profileid);
                                int insacc = insaccrepo.deleteInstagramUser(profileid, user.Id);
                                if (insacc > 0)
                                {


                                    socioprofilerepo.deleteProfile(user.Id, profileid);

                                    List<SocialProfile> lstsocioprofile = socioprofilerepo.checkProfileExistsMoreThanOne(profileid);

                                    if (lstsocioprofile.Count >= 0)
                                    {
                                        try
                                        {
                                            ScheduledMessageRepository schedulemsgrepo = new ScheduledMessageRepository();
                                            schedulemsgrepo.deleteMessage(user.Id, profileid);
                                            ArchiveMessageRepository archmsgrepo = new ArchiveMessageRepository();
                                            int del = archmsgrepo.DeleteArchiveMessage(user.Id, profileid);

                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.StackTrace);
                                        }



                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.Message);

                            }
                        }
                        else if (Profiletype == "googleplus")
                        {
                            try
                            {
                                GooglePlusAccountRepository googleplusaccrepo = new GooglePlusAccountRepository();
                                int delaccFromTeamMemberProfile = objTeamMemberProfileRepository.DeleteTeamMemberProfileByUserid(profileid);
                                int gplusacc = googleplusaccrepo.deleteGooglePlusUser(profileid, user.Id);
                                if (gplusacc > 0)
                                {

                                    socioprofilerepo.deleteProfile(user.Id, profileid);

                                    List<SocialProfile> lstsocioprofile = socioprofilerepo.checkProfileExistsMoreThanOne(profileid);

                                    if (lstsocioprofile.Count >= 0)
                                    {
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.Message);

                            }
                        }

                        string profiles = string.Empty;
                        profiles += "<div class=\"drop_top\"></div><div class=\"drop_mid\">";

                        /*facebook users binding*/
                        FacebookAccountRepository fbprepo = new FacebookAccountRepository();
                        ArrayList lstfbaccounts = fbprepo.getFacebookAccountsOfUser(user.Id);


                        profiles += "<div class=\"twitte_text\">FACEBOOK</div><div class=\"teitter\"><ul>";

                        if (lstfbaccounts.Count == 0)
                        {
                            profiles += "<li>No Records Found</li>";
                        }
                        else
                        {
                            foreach (FacebookAccount fbacc in lstfbaccounts)
                            {
                                profiles += "<li id=\"liid_" + fbacc.FbUserId + "\"   onclick=\"composemessage(this.id,'fb')\"><a><img id=\"img_" + fbacc.FbUserId + "\" src=\"../Contents/img/facebook.png\" alt=\"" + fbacc.AccessToken + "\" border=\"none\" width=\"18\" style=\"float:left;\" /><span id=\"composename_" + fbacc.FbUserId + "\" style=\"float:left;margin: 3px 0 0 5px;\">" + fbacc.FbUserName + "</span><span id=\"imgurl_" + fbacc.FbUserId + "\" style=\"display:none;\">http://graph.facebook.com/" + fbacc.FbUserId + "/picture?type=small</span></a></li>";
                            }
                        }
                        profiles += "</ul> </div>";


                        /*twitter users binding*/
                        TwitterAccountRepository twtpaccountrepo = new TwitterAccountRepository();
                        ArrayList alsttwtaccounts = twtpaccountrepo.getAllTwitterAccountsOfUser(user.Id);
                        profiles += "<div class=\"twitte_text\">TWITTER</div><div class=\"teitter\"><ul>";

                        if (alsttwtaccounts.Count == 0)
                        {
                            profiles += "<li>No Records Found</li>";
                        }
                        else
                        {
                            foreach (SocioBoard.Domain.TwitterAccount item in alsttwtaccounts)
                            {
                                profiles += "<li id=\"liid_" + item.TwitterUserId + "\"   onclick=\"composemessage(this.id,'twt')\"><a><img id=\"img_" + item.TwitterUserId + "\" src=\"../Contents/img/twitter.png\" alt=\"" + item.OAuthToken + "\" border=\"none\" width=\"18\" style=\"float:left;\" /><span id=\"imgurl_" + item.TwitterUserId + "\" style=\"display:none;\">" + item.ProfileImageUrl + "</span><span id=\"composename_" + item.TwitterUserId + "\" style=\"float:left;margin: 3px 0 0 5px;\">" + item.TwitterScreenName + "</span></a></li>";
                            }
                        }
                        profiles += "</ul> </div>";


                        /*linkedinuserbinding*/
                        LinkedInAccountRepository linkaccountrepo = new LinkedInAccountRepository();
                        ArrayList alstlinkacc = linkaccountrepo.getAllLinkedinAccountsOfUser(user.Id);
                        profiles += "<div class=\"twitte_text\">LINKEDIN</div><div class=\"teitter\"><ul>";

                        if (alstlinkacc.Count == 0)
                        {
                            profiles += "<li>No Records Found</li>";
                        }
                        else
                        {

                            foreach (LinkedInAccount item in alstlinkacc)
                            {
                                string profileurl = string.Empty;

                                if (!string.IsNullOrEmpty(item.ProfileImageUrl))
                                {
                                    profileurl = item.ProfileImageUrl;
                                }
                                else
                                {
                                    profileurl = "../../Contents/img/blank_img.png";
                                }
                                profiles += "<li id=\"liid_" + item.LinkedinUserId + "\"   onclick=\"composemessage(this.id,'lin')\"><a><img id=\"img_" + item.LinkedinUserId + "\" src=\"../Contents/img/link.png\" alt=\"" + item.OAuthToken + "\" border=\"none\" width=\"18\" style=\"float:left;\" /><span id=\"imgurl_" + item.LinkedinUserId + "\" style=\"display:none;\">" + profileurl + "</span><span id=\"composename_" + item.LinkedinUserId + "\" style=\"float:left;margin: 3px 0 0 5px;\">" + item.LinkedinUserName + "</span></a></li>";
                            }
                        }
                        profiles += "</ul> </div>";
                        Response.Write(RemainingAccount(user));

                    }
                }


                else if (Request.QueryString["op"] == "MasterCompose")
                {
                    string profiles = string.Empty;
                    string FbprofileId = string.Empty;
                    string TwtprofileId = string.Empty;
                    string TumblrprofileId = string.Empty;
                    string LinkedInprofileId = string.Empty;



                    profiles += "<div class=\"drop_top\"></div><div class=\"drop_mid\">";
                    try
                    {
                        List<TeamMemberProfile> allprofiles = objTeamMemberProfileRepository.getTeamMemberProfileData(team.Id);
                        foreach (TeamMemberProfile item in allprofiles)
                        {
                            FbprofileId += item.ProfileId + ',';

                        }
                        FbprofileId = FbprofileId.Substring(0, FbprofileId.Length - 1);

                        /*facebook users binding*/
                        FacebookAccountRepository fbrepo = new FacebookAccountRepository();
                        List<FacebookAccount> lstfbaccounts = fbrepo.getAllAccountDetail(FbprofileId);


                        profiles += "<div class=\"twitte_text\">FACEBOOK</div><div class=\"teitter\"><ul>";

                        if (lstfbaccounts.Count == 0)
                        {
                            profiles += "<li>No Records Found</li>";
                        }
                        else
                        {
                            foreach (FacebookAccount fbacc in lstfbaccounts)
                            {
                                profiles += "<li nwtk='fb' class=\"getAllNetworkProfile\" id=\"liid_" + fbacc.FbUserId + "\"   onclick=\"composemessage(this.id,'fb')\"><a><img id=\"img_" + fbacc.FbUserId + "\" src=\"../Contents/img/facebook.png\" alt=\"" + fbacc.AccessToken + "\" border=\"none\" width=\"18\" style=\"float:left;\" /><span id=\"composename_" + fbacc.FbUserId + "\" style=\"float:left;margin: 3px 0 0 5px;\">" + fbacc.FbUserName + "</span><span id=\"imgurl_" + fbacc.FbUserId + "\" style=\"display:none;\">http://graph.facebook.com/" + fbacc.FbUserId + "/picture?type=small</span></a></li>";
                            }
                        }
                        profiles += "</ul> </div>";

                    }
                    catch (Exception ex)
                    {

                        logger.Error(ex.Message);

                    }

                    /*tumbler users binding*/

                    try
                    {
                        List<TeamMemberProfile> allTumblrprofiles = objTeamMemberProfileRepository.getTumblrTeamMemberProfileData(team.Id);
                        foreach (TeamMemberProfile item in allTumblrprofiles)
                        {
                            TumblrprofileId += item.ProfileId + ',';

                        }
                        TumblrprofileId = TumblrprofileId.Substring(0, TumblrprofileId.Length - 1);

                        TumblrAccountRepository tumblrtaccountrepo = new TumblrAccountRepository();
                        List<TumblrAccount> alsttumblrtaccounts = tumblrtaccountrepo.getAllAccountDetail(TumblrprofileId);
                        profiles += "<div class=\"twitte_text\">TUMBLR</div><div class=\"teitter\"><ul>";

                        if (alsttumblrtaccounts.Count == 0)
                        {
                            profiles += "<li>No Records Found</li>";
                        }
                        else
                        {
                            foreach (TumblrAccount item in alsttumblrtaccounts)
                            {
                                profiles += "<li nwtk='tumb' class=\"getAllNetworkProfile\" id=\"liid_" + item.tblrUserName + "\"   onclick=\"composemessage(this.id,'tumb')\"><a><img id=\"img_" + item.tblrUserName + "\" src=\"../Contents/img/tumblr.png\" alt=\"" + item.tblrAccessToken + "\" border=\"none\" width=\"18\" style=\"float:left;\" /><span id=\"imgurl_" + item.tblrUserName + "\" style=\"display:none;\">http://api.tumblr.com/v2/blog/" + item.tblrProfilePicUrl + ".tumblr.com/avatar</span><span id=\"composename_" + item.tblrUserName + "\" style=\"float:left;margin: 3px 0 0 5px;\">" + item.tblrUserName + "</span></a></li>";
                            }
                        }
                        profiles += "</ul> </div>";

                    }
                    catch (Exception ex)
                    {

                        logger.Error(ex.Message);

                    }



                    /*twitter users binding*/
                    try
                    {
                        List<TeamMemberProfile> allTwtprofiles = objTeamMemberProfileRepository.getTwtTeamMemberProfileData(team.Id);
                        foreach (TeamMemberProfile item in allTwtprofiles)
                        {
                            TwtprofileId += item.ProfileId + ',';

                        }
                        TwtprofileId = TwtprofileId.Substring(0, TwtprofileId.Length - 1);

                        TwitterAccountRepository twtaccountrepo = new TwitterAccountRepository();
                        List<TwitterAccount> alsttwtaccounts = twtaccountrepo.getAllAccountDetail(TwtprofileId);
                        profiles += "<div class=\"twitte_text\">TWITTER</div><div class=\"teitter\"><ul>";

                        if (alsttwtaccounts.Count == 0)
                        {
                            profiles += "<li>No Records Found</li>";
                        }
                        else
                        {
                            foreach (TwitterAccount item in alsttwtaccounts)
                            {
                                profiles += "<li nwtk='twt' class=\"getAllNetworkProfile\" id=\"liid_" + item.TwitterUserId + "\"   onclick=\"composemessage(this.id,'twt')\"><a><img id=\"img_" + item.TwitterUserId + "\" src=\"../Contents/img/twitter.png\" alt=\"" + item.OAuthToken + "\" border=\"none\" width=\"18\" style=\"float:left;\" /><span id=\"imgurl_" + item.TwitterUserId + "\" style=\"display:none;\">" + item.ProfileImageUrl + "</span><span id=\"composename_" + item.TwitterUserId + "\" style=\"float:left;margin: 3px 0 0 5px;\">" + item.TwitterScreenName + "</span></a></li>";
                            }
                        }
                        profiles += "</ul> </div>";

                    }
                    catch (Exception ex)
                    {

                        logger.Error(ex.Message);

                    }



                    /*linkedinuserbinding*/


                    try
                    {
                        List<TeamMemberProfile> allLinkedInprofiles = objTeamMemberProfileRepository.getLinkedInTeamMemberProfileData(team.Id);
                        foreach (TeamMemberProfile item in allLinkedInprofiles)
                        {
                            LinkedInprofileId += item.ProfileId + ',';

                        }
                        LinkedInprofileId = LinkedInprofileId.Substring(0, LinkedInprofileId.Length - 1);


                        LinkedInAccountRepository linkaccountrepo = new LinkedInAccountRepository();
                        List<LinkedInAccount> alstlinkacc = linkaccountrepo.getAllAccountDetail(LinkedInprofileId);
                        profiles += "<div class=\"twitte_text\">LINKEDIN</div><div class=\"teitter\"><ul>";

                        if (alstlinkacc.Count == 0)
                        {
                            profiles += "<li>No Records Found</li>";
                        }
                        else
                        {

                            foreach (LinkedInAccount item in alstlinkacc)
                            {
                                string profileurl = string.Empty;

                                if (!string.IsNullOrEmpty(item.ProfileImageUrl))
                                {
                                    profileurl = item.ProfileImageUrl;
                                }
                                else
                                {
                                    profileurl = "../../Contents/img/blank_img.png";
                                }
                                profiles += "<li nwtk='lin' class=\"getAllNetworkProfile\" id=\"liid_" + item.LinkedinUserId + "\"   onclick=\"composemessage(this.id,'lin')\"><a><img id=\"img_" + item.LinkedinUserId + "\" src=\"../Contents/img/link.png\" alt=\"" + item.OAuthToken + "\" border=\"none\" width=\"18\" style=\"float:left;\" /><span id=\"imgurl_" + item.LinkedinUserId + "\" style=\"display:none;\">" + profileurl + "</span><span id=\"composename_" + item.LinkedinUserId + "\" style=\"float:left;margin: 3px 0 0 5px;\">" + item.LinkedinUserName + "</span></a></li>";
                            }
                        }
                        profiles += "</ul> </div>";
                    }
                    catch (Exception ex)
                    {

                        logger.Error(ex.Message);

                    }

                    Response.Write(profiles);
                }







              //=============================================================







                else if (Request.QueryString["op"] == "MasterComposesc")
                {
                    string profiles = string.Empty;
                    string FbprofileId = string.Empty;
                    string TwtprofileId = string.Empty;
                    string LinkedInprofileId = string.Empty;
                    string TumblrprofileId = string.Empty;


                    //if (Session["profilesforcomposemessage"] == null)
                    //{
                    profiles += "<div class=\"drop_top\"></div><div class=\"drop_mid\">";
                    try
                    {
                        List<TeamMemberProfile> allprofiles = objTeamMemberProfileRepository.getTeamMemberProfileData(team.Id);
                        foreach (TeamMemberProfile item in allprofiles)
                        {
                            FbprofileId += item.ProfileId + ',';

                        }
                        FbprofileId = FbprofileId.Substring(0, FbprofileId.Length - 1);

                        /*facebook users binding*/
                        FacebookAccountRepository fbrepo = new FacebookAccountRepository();
                        List<FacebookAccount> lstfbaccounts = fbrepo.getAllAccountDetail(FbprofileId);


                        profiles += "<div class=\"twitte_text\">FACEBOOK</div><div class=\"teitter\"><ul>";

                        if (lstfbaccounts.Count == 0)
                        {
                            profiles += "<li>No Records Found</li>";
                        }
                        else
                        {
                            foreach (FacebookAccount fbacc in lstfbaccounts)
                            {
                                profiles += "<li nwtk='fb' class=\"getAllNetworkProfile\" id=\"liid_" + fbacc.FbUserId + "\"   onclick=\"composemessage(this.id,'fb')\"><a><img id=\"img_" + fbacc.FbUserId + "\" src=\"../Contents/img/facebook.png\" alt=\"" + fbacc.AccessToken + "\" border=\"none\" width=\"18\" style=\"float:left;\" /><span id=\"composename_" + fbacc.FbUserId + "\" style=\"float:left;margin: 3px 0 0 5px;\">" + fbacc.FbUserName + "</span><span id=\"imgurl_" + fbacc.FbUserId + "\" style=\"display:none;\">http://graph.facebook.com/" + fbacc.FbUserId + "/picture?type=small</span></a></li>";
                            }
                        }
                        profiles += "</ul> </div>";

                    }
                    catch (Exception ex)
                    {

                        logger.Error(ex.Message);

                    }

                    /*tumbler users binding*/

                    try
                    {
                        List<TeamMemberProfile> allTumblrprofiles = objTeamMemberProfileRepository.getTumblrTeamMemberProfileData(team.Id);
                        foreach (TeamMemberProfile item in allTumblrprofiles)
                        {
                            TumblrprofileId += item.ProfileId + ',';

                        }
                        TumblrprofileId = TumblrprofileId.Substring(0, TumblrprofileId.Length - 1);

                        TumblrAccountRepository tumblrtaccountrepo = new TumblrAccountRepository();
                        List<TumblrAccount> alsttumblrtaccounts = tumblrtaccountrepo.getAllAccountDetail(TumblrprofileId);
                        profiles += "<div class=\"twitte_text\">TUMBLR</div><div class=\"teitter\"><ul>";

                        if (alsttumblrtaccounts.Count == 0)
                        {
                            profiles += "<li>No Records Found</li>";
                        }
                        else
                        {
                            foreach (TumblrAccount item in alsttumblrtaccounts)
                            {
                                //    profiles += "<li nwtk='twt' class=\"getAllNetworkProfile\" id=\"liid_" + item.tblrUserName + "\"   onclick=\"composemessage(this.id,'twt')\"><a><img id=\"img_" + item.tblrUserName + "\" src=\"../Contents/img/tumblr.png\" alt=\"" + item.tblrAccessToken + "\" border=\"none\" width=\"18\" style=\"float:left;\" /><span id=\"imgurl_" + item.tblrUserName + "\" style=\"display:none;\">" + item.tblrProfilePicUrl + "</span><span id=\"composename_" + item.tblrUserName + "\" style=\"float:left;margin: 3px 0 0 5px;\">" + item.tblrUserName + "</span></a></li>";
                                //}
                                profiles += "<li nwtk='tumb' class=\"getAllNetworkProfile\" id=\"liid_" + item.tblrUserName + "\"   onclick=\"composemessage(this.id,'tumb')\"><a><img id=\"img_" + item.tblrUserName + "\" src=\"../Contents/img/tumblr.png\" alt=\"" + item.tblrAccessToken + "\" border=\"none\" width=\"18\" style=\"float:left;\" /><span id=\"imgurl_" + item.tblrUserName + "\" style=\"display:none;\">http://api.tumblr.com/v2/blog/" + item.tblrProfilePicUrl + ".tumblr.com/avatar</span><span id=\"composename_" + item.tblrUserName + "\" style=\"float:left;margin: 3px 0 0 5px;\">" + item.tblrUserName + "</span></a></li>";
                            }
                        }
                        profiles += "</ul> </div>";

                    }
                    catch (Exception ex)
                    {

                        logger.Error(ex.Message);

                    }



                    /*twitter users binding*/
                    try
                    {
                        List<TeamMemberProfile> allTwtprofiles = objTeamMemberProfileRepository.getTwtTeamMemberProfileData(team.Id);
                        foreach (TeamMemberProfile item in allTwtprofiles)
                        {
                            TwtprofileId += item.ProfileId + ',';

                        }
                        TwtprofileId = TwtprofileId.Substring(0, TwtprofileId.Length - 1);

                        TwitterAccountRepository twtaccountrepo = new TwitterAccountRepository();
                        List<TwitterAccount> alsttwtaccounts = twtaccountrepo.getAllAccountDetail(TwtprofileId);
                        profiles += "<div class=\"twitte_text\">TWITTER</div><div class=\"teitter\"><ul>";

                        if (alsttwtaccounts.Count == 0)
                        {
                            profiles += "<li>No Records Found</li>";
                        }
                        else
                        {
                            foreach (TwitterAccount item in alsttwtaccounts)
                            {
                                profiles += "<li nwtk='twt' class=\"getAllNetworkProfile\" id=\"liid_" + item.TwitterUserId + "\"   onclick=\"composemessage(this.id,'twt')\"><a><img id=\"img_" + item.TwitterUserId + "\" src=\"../Contents/img/twitter.png\" alt=\"" + item.OAuthToken + "\" border=\"none\" width=\"18\" style=\"float:left;\" /><span id=\"imgurl_" + item.TwitterUserId + "\" style=\"display:none;\">" + item.ProfileImageUrl + "</span><span id=\"composename_" + item.TwitterUserId + "\" style=\"float:left;margin: 3px 0 0 5px;\">" + item.TwitterScreenName + "</span></a></li>";
                            }
                        }
                        profiles += "</ul> </div>";

                    }
                    catch (Exception ex)
                    {

                        logger.Error(ex.Message);

                    }



                    /*linkedinuserbinding*/


                    try
                    {
                        List<TeamMemberProfile> allLinkedInprofiles = objTeamMemberProfileRepository.getLinkedInTeamMemberProfileData(team.Id);
                        foreach (TeamMemberProfile item in allLinkedInprofiles)
                        {
                            LinkedInprofileId += item.ProfileId + ',';

                        }
                        LinkedInprofileId = LinkedInprofileId.Substring(0, LinkedInprofileId.Length - 1);


                        LinkedInAccountRepository linkaccountrepo = new LinkedInAccountRepository();
                        List<LinkedInAccount> alstlinkacc = linkaccountrepo.getAllAccountDetail(LinkedInprofileId);
                        profiles += "<div class=\"twitte_text\">LINKEDIN</div><div class=\"teitter\"><ul>";

                        if (alstlinkacc.Count == 0)
                        {
                            profiles += "<li>No Records Found</li>";
                        }
                        else
                        {

                            foreach (LinkedInAccount item in alstlinkacc)
                            {
                                string profileurl = string.Empty;

                                if (!string.IsNullOrEmpty(item.ProfileImageUrl))
                                {
                                    profileurl = item.ProfileImageUrl;
                                }
                                else
                                {
                                    profileurl = "../../Contents/img/blank_img.png";
                                }
                                profiles += "<li nwtk='lin' class=\"getAllNetworkProfile\" id=\"liid_" + item.LinkedinUserId + "\"   onclick=\"composemessage(this.id,'lin')\"><a><img id=\"img_" + item.LinkedinUserId + "\" src=\"../Contents/img/link.png\" alt=\"" + item.OAuthToken + "\" border=\"none\" width=\"18\" style=\"float:left;\" /><span id=\"imgurl_" + item.LinkedinUserId + "\" style=\"display:none;\">" + profileurl + "</span><span id=\"composename_" + item.LinkedinUserId + "\" style=\"float:left;margin: 3px 0 0 5px;\">" + item.LinkedinUserName + "</span></a></li>";
                            }
                        }
                        profiles += "</ul> </div>";
                    }
                    catch (Exception ex)
                    {

                        logger.Error(ex.Message);

                    }

                    Response.Write(profiles);
                }


                else if (Request.QueryString["op"] == "sendmessage")
                {
                    string messagecount = string.Empty;
                    string message = Request.QueryString["message"];
                    string time = Request.QueryString["now"];
                    //message = Request.Form["massagee"];
                    var userid = Request.QueryString["userid[]"].Split(',');
                    //var userid = Request.Form["userid[]"].Split(',');
                    var files = Request.Files.Count;
                    var fi = Request.Files["file"];
                    string file = string.Empty;
                    List<string> listMessage = new List<string>();
                    int interval = Convert.ToInt16(Request.QueryString["interval"]);
                    int count = 1;

                    try
                    {
                        if (Request.Files.Count > 0)
                        {
                            if (fi != null)
                            {
                                var path = Server.MapPath("~/Contents/img/upload");

                                // var path = System.Configuration.ConfigurationManager.AppSettings["MailSenderDomain"]+"Contents/img/upload";
                                file = path + "/" + fi.FileName;
                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }
                                fi.SaveAs(file);
                            }
                        }

                    }
                    catch (Exception ex)
                    {

                        logger.Error(ex.Message);

                    }



                    try
                    {
                        StreamReader reader = new StreamReader(Request.Files["filesText"].InputStream);
                        //string fileContent = reader.ReadLine();

                        string text = "";
                        while ((text = reader.ReadLine()) != null)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(text))
                                {
                                    listMessage.Add(text);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                            }
                        }

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.StackTrace);
                        Console.WriteLine(ex.Message);
                    }


                    if (listMessage.Count > 0)
                    {
                        foreach (var msg in listMessage)
                        {

                            message = msg;
                            foreach (var item in userid)
                            {
                                string[] networkingwithid = item.Split('_');
                                string profiletype = "";
                                if (networkingwithid[0] == "fb")
                                {
                                    profiletype = "facebook";
                                }
                                else if (networkingwithid[0] == "twt")
                                {
                                    profiletype = "twitter";
                                }
                                else if (networkingwithid[0] == "lin")
                                {
                                    profiletype = "linkedin";
                                }
                                else if (networkingwithid[0] == "tumb")
                                {
                                    profiletype = "tumblr";
                                }

                                ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
                                ScheduledMessage objScheduledMessage = new ScheduledMessage();
                                try
                                {
                                    objScheduledMessage.ShareMessage = message;
                                    objScheduledMessage.ClientTime = Convert.ToDateTime(time);
                                    objScheduledMessage.ScheduleTime = Convert.ToDateTime(time).AddMinutes(count * interval);
                                    objScheduledMessage.CreateTime = Convert.ToDateTime(time);
                                    objScheduledMessage.Status = false;
                                    objScheduledMessage.UserId = user.Id;
                                    objScheduledMessage.ProfileType = profiletype;
                                    if (file != null)
                                    {
                                        try
                                        {
                                            var path = System.Configuration.ConfigurationManager.AppSettings["MailSenderDomain"] + "Contents/img/upload";
                                            file = path + "/" + fi.FileName;
                                            objScheduledMessage.PicUrl = file;
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.StackTrace);
                                        }
                                    }

                                    objScheduledMessage.ProfileId = networkingwithid[1];
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.StackTrace);
                                }


                                objScheduledMessageRepository.addNewMessage(objScheduledMessage);
                                messagecount = objScheduledMessageRepository.getAllSentMessagesOfUser(user.Id).Count().ToString();
                            }
                            count++;
                        }
                    }


                    foreach (var item in userid)
                    {
                        string[] networkingwithid = item.Split('_');
                        if (networkingwithid[0] == "fb")
                        {
                            try
                            {
                                //for (int i = 0; i < 5000; i++)
                                // {

                                FacebookAccountRepository fbaccountrepo = new FacebookAccountRepository();
                                FacebookAccount fbaccount = fbaccountrepo.getFacebookAccountDetailsById(networkingwithid[1]);
                                var args = new Dictionary<string, object>();

                                args["message"] = message;

                                if (Request.Files.Count > 0 && Request.Files["file"] != null)
                                {
                                    string strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                                    string strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                    string filepath = strUrl + "/Contents/img/upload/" + fi.FileName;
                                    args["picture"] = filepath;
                                }
                                FacebookClient fc = new FacebookClient(fbaccount.AccessToken);

                                string facebookpost = string.Empty;
                                if (fbaccount.Type == "page")
                                {
                                    facebookpost = fc.Post("/" + fbaccount.FbUserId + "/feed", args).ToString();
                                }
                                else
                                {
                                    facebookpost = fc.Post("/me/feed", args).ToString();
                                }



                                //}


                            }
                            catch (Exception ex)
                            {

                                logger.Error(ex.Message);

                                Console.WriteLine(ex.Message);
                            }

                        }
                        else if (networkingwithid[0] == "twt")
                        {
                            try
                            {
                                TwitterAccountRepository twtaccountrepo = new TwitterAccountRepository();
                                SocioBoard.Domain.TwitterAccount twtaccount = twtaccountrepo.getUserInformation(networkingwithid[1]);

                                TwitterHelper twthelper = new TwitterHelper();

                                oAuthTwitter OAuthTwt = new oAuthTwitter();
                                OAuthTwt.AccessToken = twtaccount.OAuthToken;
                                OAuthTwt.AccessTokenSecret = twtaccount.OAuthSecret;
                                OAuthTwt.TwitterScreenName = twtaccount.TwitterScreenName;
                                OAuthTwt.TwitterUserId = twtaccount.TwitterUserId;



                                twthelper.SetCofigDetailsForTwitter(OAuthTwt);

                                #region For Testing
                                // For Testing 

                                //OAuthTwt.ConsumerKey = "udiFfPxtCcwXWl05wTgx6w";
                                //OAuthTwt.ConsumerKeySecret = "jutnq6N32Rb7cgbDSgfsrUVgRQKMbUB34yuvAfCqTI";
                                //OAuthTwt.AccessToken = "1453351098-Lz4H7cHKp26pXarF6l9zEwdiHDnwH7D0H4zteH3";
                                //OAuthTwt.AccessTokenSecret = "dGBPxR9wxhQMioIcj5P4Wemxo5EZIZ8wlvDz7i39lSNFg";
                                //OAuthTwt.TwitterScreenName = "";
                                //OAuthTwt.TwitterUserId = "";
                                #endregion

                                Tweet twt = new Tweet();
                                if (Request.Files.Count > 0)
                                {

                                    PhotoUpload ph = new PhotoUpload();
                                    //ph.Tweet(file, message, OAuthTwt);
                                    string res = string.Empty;
                                    ph.NewTweet(file, message, OAuthTwt, ref res);



                                    // for testing

                                    Response.Write(res);
                                    Console.WriteLine(res);
                                }
                                else
                                {
                                    JArray post = twt.Post_Statuses_Update(OAuthTwt, message);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                logger.Error(ex.Message);

                            }
                        }
                        else if (networkingwithid[0] == "lin")
                        {
                            try
                            {
                                LinkedInAccountRepository linkedinaccrepo = new LinkedInAccountRepository();
                                LinkedInAccount linkedaccount = linkedinaccrepo.getUserInformation(networkingwithid[1]);
                                oAuthLinkedIn Linkedin_oauth = new oAuthLinkedIn();

                                Linkedin_oauth.Verifier = linkedaccount.OAuthVerifier;
                                Linkedin_oauth.TokenSecret = linkedaccount.OAuthSecret;
                                Linkedin_oauth.Token = linkedaccount.OAuthToken;
                                Linkedin_oauth.Id = linkedaccount.LinkedinUserId;
                                Linkedin_oauth.FirstName = linkedaccount.LinkedinUserName;
                                SocialStream sociostream = new SocialStream();
                                string res = sociostream.SetStatusUpdate(Linkedin_oauth, message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                logger.Error(ex.Message);

                            }
                        }
                        else if (networkingwithid[0] == "tumb")
                        {
                            string title = string.Empty;
                            try
                            {
                                TumblrAccountRepository objTumblrAccountRepository = new TumblrAccountRepository();
                                TumblrAccount tumblraccount = objTumblrAccountRepository.getTumblrAccountDetailsById(networkingwithid[1]);

                                PublishedPosts objPublishedPosts = new PublishedPosts();
                                objPublishedPosts.PostData(tumblraccount.tblrAccessToken, tumblraccount.tblrAccessTokenSecret, networkingwithid[1], message, title, "text");

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                logger.Error(ex.Message);

                            }
                        }
                        string profiletype = "";
                        if (networkingwithid[0] == "fb")
                        {
                            profiletype = "facebook";
                        }
                        else if (networkingwithid[0] == "twt")
                        {
                            profiletype = "twitter";
                        }
                        else if (networkingwithid[0] == "lin")
                        {
                            profiletype = "linkedin";
                        }
                        else if (networkingwithid[0] == "tumb")
                        {
                            profiletype = "tumblr";
                        }

                        ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
                        ScheduledMessage objScheduledMessage = new ScheduledMessage();
                        try
                        {
                            objScheduledMessage.ShareMessage = message;
                            objScheduledMessage.ClientTime = Convert.ToDateTime(time);
                            objScheduledMessage.ScheduleTime = Convert.ToDateTime(time);
                            objScheduledMessage.CreateTime = Convert.ToDateTime(time);
                            objScheduledMessage.Status = true;
                            objScheduledMessage.UserId = user.Id;
                            objScheduledMessage.ProfileType = profiletype;
                            if (file != null)
                            {
                                try
                                {
                                    var path = System.Configuration.ConfigurationManager.AppSettings["MailSenderDomain"] + "Contents/img/upload";
                                    file = path + "/" + fi.FileName;
                                    objScheduledMessage.PicUrl = file;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.StackTrace);
                                }
                            }

                            objScheduledMessage.ProfileId = networkingwithid[1];
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }


                        objScheduledMessageRepository.addNewMessage(objScheduledMessage);
                        messagecount = objScheduledMessageRepository.getAllSentMessagesOfUser(user.Id).Count().ToString();
                    }

                    Response.Write("~" + messagecount);

                }
                else if (Request.QueryString["op"] == "wooqueue_messages")
                {
                    ScheduledMessageRepository schmsgrepo = new ScheduledMessageRepository();
                }



                else if (Request.QueryString["op"] == "schedulemessage")
                {

                    var userid = Request.QueryString["users[]"].Split(',');
                    var datearr = Request.QueryString["datearr[]"].Split(',');
                    string message = Request.QueryString["message"];
                    message = Request.Form["messagee"];
                    ScheduledMessageRepository schmsgrepo = new ScheduledMessageRepository();
                    string time = Request.QueryString["time"];
                    string clienttime = Request.QueryString["clittime"];


                    foreach (var item in userid)
                    {
                        if (!string.IsNullOrEmpty(item.ToString()))
                        {
                            foreach (var child in datearr)
                            {


                                ScheduledMessage schmessage = new ScheduledMessage();
                                string[] networkingwithid = item.Split('_');

                                if (networkingwithid[0] == "fbscheduler")
                                {
                                    schmessage.CreateTime = DateTime.Now;
                                    schmessage.ProfileType = "facebook";
                                    schmessage.ProfileId = networkingwithid[1];
                                    schmessage.Id = Guid.NewGuid();
                                    schmessage.PicUrl = string.Empty;
                                    schmessage.ClientTime = Convert.ToDateTime(clienttime);
                                    string servertime = this.CompareDateWithclient(clienttime, child + " " + time);
                                    schmessage.ScheduleTime = Convert.ToDateTime(servertime);
                                    schmessage.ShareMessage = message;
                                    schmessage.UserId = user.Id;
                                    schmessage.Status = false;

                                }
                                else if (networkingwithid[0] == "twtscheduler")
                                {

                                    schmessage.CreateTime = DateTime.Now;
                                    schmessage.ProfileType = "twitter";
                                    schmessage.ProfileId = networkingwithid[1];
                                    schmessage.Id = Guid.NewGuid();
                                    schmessage.PicUrl = string.Empty;
                                    schmessage.ClientTime = Convert.ToDateTime(clienttime);
                                    string servertime = this.CompareDateWithServer(clienttime, child + " " + time);
                                    schmessage.ScheduleTime = Convert.ToDateTime(servertime);
                                    schmessage.ShareMessage = message;
                                    schmessage.UserId = user.Id;
                                    schmessage.Status = false;

                                }
                                else if (networkingwithid[0] == "linscheduler")
                                {
                                    schmessage.ClientTime = Convert.ToDateTime(clienttime);
                                    schmessage.CreateTime = DateTime.Now;
                                    schmessage.ProfileType = "linkedin";
                                    schmessage.ProfileId = networkingwithid[1];
                                    schmessage.Id = Guid.NewGuid();
                                    schmessage.PicUrl = string.Empty;
                                    string servertime = this.CompareDateWithServer(clienttime, child + " " + time);
                                    schmessage.ScheduleTime = Convert.ToDateTime(servertime);
                                    schmessage.ShareMessage = message;
                                    schmessage.UserId = user.Id;
                                    schmessage.Status = false;

                                }
                                if (!string.IsNullOrEmpty(message))
                                {
                                    if (!schmsgrepo.checkMessageExistsAtTime(user.Id, schmessage.ShareMessage, schmessage.ScheduleTime, schmessage.ProfileId))
                                    {
                                        schmsgrepo.addNewMessage(schmessage);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (Request.QueryString["op"] == "insight")
                {
                    string check = "";
                    FacebookAccount objFacebookAccount = (FacebookAccount)Session["fbpagedetail"];
                    FacebookHelper objFbHelper = new FacebookHelper();
                    SocialProfile socioprofile = new SocialProfile();
                    SocialProfilesRepository socioprofilerepo = new SocialProfilesRepository();
                    FacebookAccountRepository fbrepo = new FacebookAccountRepository();
                    FacebookClient fbClient = new FacebookClient(objFacebookAccount.AccessToken);
                    int fancountPage = 0;
                    dynamic fancount = fbClient.Get("fql", new { q = " SELECT fan_count FROM page WHERE page_id =" + Request["id"].ToString() });




                    //dynamic fancount11 = fbClient.Get("fql", new
                    //{
                    //    q = "SELECT metric, value FROM insights WHERE object_id=" + Request["id"].ToString() + "" +
                    //        "AND metric='page_posts_impressions_unique'" +
                    //        "AND end_time=end_time_date('2014-08-04')" +
                    //        "AND period=period('day')"
                    //});


                    //dynamic fancount12 = fbClient.Get("fql", new
                    //{
                    //    q = "SELECT metric, value FROM insights WHERE object_id=" + Request["id"].ToString() + "" +
                    //        "AND metric='page_posts_impressions_unique'" +
                    //        "AND end_time=end_time_date('2014-08-03')" +
                    //        "AND period=period('day')"
                    //});

                    //dynamic fancount13 = fbClient.Get("fql", new
                    //{
                    //    q = "SELECT metric, value FROM insights WHERE object_id=" + Request["id"].ToString() + "" +
                    //        "AND metric='page_posts_impressions_unique'" +
                    //        "AND end_time=end_time_date('2014-08-02')" +
                    //        "AND period=period('day')"
                    //});


                    foreach (var friend in fancount.data)
                    {
                        fancountPage = Convert.ToInt32(friend.fan_count);
                    }
                    objFacebookAccount.Friends = Convert.ToInt32(fancountPage);
                    objFacebookAccount.FbUserId = Request["id"].ToString();
                    objFacebookAccount.FbUserName = Request["name"].ToString();
                    objFacebookAccount.Type = "page";
                    objFacebookAccount.UserId = user.Id;
                    socioprofile.Id = Guid.NewGuid();
                    socioprofile.ProfileDate = DateTime.Now;
                    socioprofile.ProfileId = Request["id"].ToString();
                    socioprofile.ProfileStatus = 1;
                    socioprofile.ProfileType = "facebook";
                    socioprofile.UserId = user.Id;
                    if (!fbrepo.checkFacebookUserExists(objFacebookAccount.FbUserId, user.Id))
                    {
                        fbrepo.addFacebookUser(objFacebookAccount);


                        Groups lstDetails = objGroupRepository.getGroupName(team.GroupId);
                        if (lstDetails.GroupName == "Socioboard")
                        {

                            TeamMemberProfile teammemberprofile = new TeamMemberProfile();
                            teammemberprofile.Id = Guid.NewGuid();
                            teammemberprofile.TeamId = team.Id;
                            teammemberprofile.ProfileId = objFacebookAccount.FbUserId;
                            teammemberprofile.ProfileType = "facebook";
                            teammemberprofile.StatusUpdateDate = DateTime.Now;

                            objTeamMemberProfileRepository.addNewTeamMember(teammemberprofile);

                        }




                        if (!socioprofilerepo.checkUserProfileExist(socioprofile))
                        {
                            socioprofilerepo.addNewProfileForUser(socioprofile);
                        }
                        else
                        {
                            socioprofilerepo.updateSocialProfile(socioprofile);
                        }
                    }
                    else
                    {
                        check = "exist";
                        Session["alreadypageexist"] = objFacebookAccount;
                        fbrepo.updateFacebookUser(objFacebookAccount);
                        if (!socioprofilerepo.checkUserProfileExist(socioprofile))
                        {
                            socioprofilerepo.addNewProfileForUser(socioprofile);
                        }
                        else
                        {
                            socioprofilerepo.updateSocialProfile(socioprofile);
                        }
                    }

                    //get facebookpagefeeds
                    fbClient = new FacebookClient(objFacebookAccount.AccessToken);
                    FacebookHelper fbhelper = new FacebookHelper();
                    FacebookInsightStatsHelper fbiHelper = new FacebookInsightStatsHelper();
                    var feeds = fbClient.Get("/" + Request["id"].ToString() + "/feed");

                    fbiHelper.getPageImpresion(Request["id"].ToString(), user.Id, 15);
                    //fbiHelper.getFanPageLikesByGenderAge(Request["id"].ToString(), user.Id, 15);
                    //fbiHelper.getLocation(Request["id"].ToString(), user.Id, 15);
                    fbiHelper.getFanPost(Request["id"].ToString(), user.Id, 10);
                    dynamic profile = fbClient.Get(Request["id"].ToString());
                    fbhelper.getFacebookUserFeeds(feeds, profile);
                    //end facebookpagefeeds





                    string id = "id";
                    string value = Request["id"].ToString();

                    Dictionary<string, string> Did = new Dictionary<string, string>();
                    Did.Add(id, value);

                    dynamic Pageid = Did;



                    // var friendgenderstats=fbClient.Get("me/friends?fields=gender");
                    objFbHelper.getfbFriendsGenderStatsForFanPage(Pageid, user.Id, ref objFacebookAccount);

                    Session["fbSocial"] = null;
                    Response.Write(RemainingAccount(user));
                }
                else if (Request.QueryString["op"] == "countmessages")
                {
                    try
                    {
                        int val = 0;
                        /*facebook*/
                        FacebookAccountRepository fbAccoutsRepo = new FacebookAccountRepository();
                        ArrayList lstfacebookAccounts = fbAccoutsRepo.getAllFacebookAccountsOfUser(user.Id);
                        foreach (FacebookAccount item in lstfacebookAccounts)
                        {
                            try
                            {
                                FacebookClient fb = new FacebookClient(item.AccessToken);
                                dynamic unreadcount = fb.Get("fql", new { q = "SELECT unread_count FROM mailbox_folder WHERE folder_id = 0 AND viewer_id = " + item.FbUserId + "" });
                                foreach (var chile in unreadcount.data)
                                {
                                    var count = chile.unread_count;
                                    int countable = Convert.ToInt32(count.ToString());
                                    val = val + countable;

                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.Message);
                                Console.WriteLine(ex.Message);
                            }
                        }
                        /*Twitter*/
                        Session["CountMessages"] = val;
                        Response.Write(val);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }

                else if (Request.QueryString["op"] == "recentfollowers")
                {
                    string recentfollowers = string.Empty;
                    #region RecentFollowers
                    Users twtUsers = new Users();
                    TwitterAccountRepository twtAccRepo = new TwitterAccountRepository();
                    ArrayList lstAccRepo = twtAccRepo.getAllTwitterAccountsOfUser(user.Id);
                    oAuthTwitter oauth = null;
                    foreach (TwitterAccount itemTwt in lstAccRepo)
                    {
                        oauth = new oAuthTwitter();
                        oauth.AccessToken = itemTwt.OAuthToken;
                        oauth.AccessTokenSecret = itemTwt.OAuthSecret;
                        oauth.ConsumerKey = ConfigurationManager.AppSettings["consumerKey"];
                        oauth.ConsumerKeySecret = ConfigurationManager.AppSettings["consumerSecret"];
                        oauth.TwitterScreenName = itemTwt.TwitterScreenName;
                        oauth.TwitterUserId = itemTwt.TwitterUserId;
                        JArray jarresponse = twtUsers.Get_Followers_ById(oauth, itemTwt.TwitterUserId);
                        foreach (var item in jarresponse)
                        {
                            int resposecount = 0;
                            if (item["ids"] != null)
                            {
                                foreach (var child in item["ids"])
                                {
                                    if (resposecount < 2)
                                    {
                                        JArray userprofile = twtUsers.Get_Users_LookUp(oauth, child.ToString());

                                        foreach (var items in userprofile)
                                        {
                                            resposecount++;
                                            try
                                            {
                                                recentfollowers += "<li><a href=\"https://twitter.com/" + items["screen_name"] + "\" target=\"_blank\"><img style=\"border:3px solid #FCFCFC;\" title=\"" + items["name"] + "\" width=\"48\" height=\"48\" alt=\"\" src=\"" + items["profile_image_url"] + "\"></a></li>";
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine(ex.Message);
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }

                    Response.Write(recentfollowers);

                    #endregion

                }
                else if (Request.QueryString["op"] == "removefollowers")
                {
                    string removeid = Request.QueryString["removeid"];
                    string userid = Request.QueryString["userid"];
                    Friendship friendship = new Friendship();
                    oAuthTwitter oauth = new oAuthTwitter();
                    TwitterAccountRepository twtaccrepo = new TwitterAccountRepository();
                    TwitterAccount twtAccount = twtaccrepo.getUserInformation(user.Id, userid);
                    oauth.TwitterUserId = twtAccount.TwitterUserId;
                    oauth.TwitterScreenName = twtAccount.TwitterScreenName;
                    oauth.ConsumerKey = ConfigurationManager.AppSettings["consumerKey"];
                    oauth.ConsumerKeySecret = ConfigurationManager.AppSettings["consumerSecret"];
                    oauth.AccessToken = twtAccount.OAuthToken;
                    oauth.AccessTokenSecret = twtAccount.OAuthSecret;
                    JArray responseremove = friendship.Post_Friendship_Destroy(oauth, removeid);

                }
                //for Deleting socialqueue Message

                else if (Request.QueryString["op"] == "deletequeuemsg")
                {
                    try
                    {
                        string res = string.Empty;
                        string messageId = Request.QueryString["messageid"].ToString();
                        Guid userid = user.Id;
                        ScheduledMessageRepository obj = new ScheduledMessageRepository();
                        bool check = obj.deleteScheduleMessage(userid, messageId);
                        if (check == true)
                        {
                            res = "success";
                        }
                        else
                        {

                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                }

                    //for Editing socialqueue Message
                else if (Request.QueryString["op"] == "Editqueuemsg")
                {
                    try
                    {
                        string messageId = Request.QueryString["messageid"].ToString();
                        string newstr = Request.QueryString["newstr"];
                        Guid userid = user.Id;
                        DateTime dt = DateTime.Now;
                        ScheduledMessageRepository obj = new ScheduledMessageRepository();
                        obj.UpdateScheduleMessage(userid, messageId, newstr, dt);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);

                    }
                }






                else if (Request.QueryString["op"] == "wooqueuemessages")
                {
                    string profileid = string.Empty;
                    ScheduledMessageRepository schmsgRepo = new ScheduledMessageRepository();
                    List<TeamMemberProfile> allprofiles = objTeamMemberProfileRepository.getAllTeamMemberProfilesOfTeam(team.Id);
                    foreach (TeamMemberProfile item in allprofiles)
                    {
                        profileid += item.ProfileId + ',';

                    }
                    profileid = profileid.Substring(0, profileid.Length - 1);

                    List<ScheduledMessage> lstschMsg = schmsgRepo.getAllMessagesDetail(profileid);
                    string schmessages = string.Empty;
                    //string profurl = string.Empty;
                    //if (string.IsNullOrEmpty(user.ProfileUrl))
                    //{
                    string profurls = "../Contents/img/blank_img.png";
                    //}
                    //else
                    //{
                    //    // profurl = "../Contents/img/blank_img.png";
                    //    profurl = user.ProfileUrl;
                    //}
                    if (lstschMsg.Count != 0)
                    {
                        foreach (ScheduledMessage item in lstschMsg)
                            try
                            {
                                UserRepository objUserRepository = new UserRepository();
                                User objuser = objUserRepository.getUsersById(item.UserId);
                                string profurl = string.Empty;
                                if (string.IsNullOrEmpty(objuser.ProfileUrl))
                                {
                                    profurl = "../Contents/img/blank_img.png";
                                }
                                else
                                {
                                    // profurl = "../Contents/img/blank_img.png";
                                    profurl = objuser.ProfileUrl;
                                }



                                {
                                    schmessages += "<section class=\"section\"><div  class=\"js-task-cont read\">" +
                                                             "<section class=\"task-owner\">" +
                                                                 "<img width=\"32\" height=\"32\" border=\"0\" src=\"" + profurl + "\" class=\"avatar\">" +
                                                             "</section>" +
                                                             "<section class=\"task-activity third\">" +
                                                                 "<p>" + objuser.UserName + "</p>" +
                                                                 "<div>" + CompareDateWithServerNew(item.ClientTime, item.CreateTime, item.ScheduleTime) + "</div>" +
                                                                 "<input type=\"hidden\" value=\"#\" id=\"hdntaskid_1\">" +
                                                                 "<p></p>" +
                                                           "</section>" +
                                                           "<section class=\"task-message font-13 third\" style=\"height: auto; width: 21%; margin-right: 9px;\"><a id=\"edit_" + item.Id + "\" onclick=\"Editqueue('" + item.Id + "','" + item.ShareMessage + "');\" class=\"tip_left\">" + gethtmlfromstring(item.ShareMessage) + "</a></section>";

                                    if (item.ProfileType == "facebook")
                                    {
                                        schmessages += "<div style=\"height:70px; margin-top: 0;\" class=\"userpictiny\">" +
                                                            "<img width=\"48\" height=\"48\" src=\"http://graph.facebook.com/" + item.ProfileId + "/picture?type=small\" alt=\"\">" +
                                                            "<a style=\"right: 2px; top: 40px;\" title=\"\" class=\"userurlpic\" href=\"#\">" +
                                                                "<img  alt=\"\" src=\"../Contents/img/facebook.png\" style=\"height: 16px;width: 16x;\"></a></div>";
                                    }
                                    else if (item.ProfileType == "twitter")
                                    {
                                        TwitterAccountRepository twtAccRepo = new TwitterAccountRepository();
                                        TwitterAccount twtAccount = twtAccRepo.getUserInformation(item.ProfileId);
                                        if (twtAccount != null)
                                        {
                                            schmessages += "<div style=\"height:70px; margin-top: 0;\" class=\"userpictiny\">" +
                                                            "<img width=\"48\" height=\"48\" src=\"" + twtAccount.ProfileImageUrl + "\" alt=\"\">" +
                                                            "<a style=\"right: 2px; top: 40px;\" title=\"\" class=\"userurlpic\" href=\"#\">" +
                                                                "<img  alt=\"\" src=\"../Contents/img/twitter.png\" style=\"height: 16px;width: 16x;\"></a></div>";
                                        }
                                    }
                                    else if (item.ProfileType == "linkedin")
                                    {
                                        LinkedInAccountRepository linkAccRepo = new LinkedInAccountRepository();
                                        LinkedInAccount linkedAccount = linkAccRepo.getUserInformation(item.ProfileId);
                                        if (linkedAccount != null)
                                        {
                                            schmessages += "<div style=\"height:70px; margin-top: 0;\" class=\"userpictiny\">" +
                                                                "<img width=\"48\" height=\"48\" src=\"" + linkedAccount.ProfileImageUrl + "\" alt=\"\">" +
                                                                "<a style=\"right: 2px; top: 40px;\" title=\"\" class=\"userurlpic\" href=\"#\">" +
                                                                    "<img  alt=\"\" src=\"../Contents/img/link.png\" style=\"height: 16px;width: 16x;\"></a></div>";
                                        }

                                    }
                                    string status = (item.Status == true) ? "Completed" : "Pending";
                                    schmessages += "<section class=\"task-status\" style=\"width:34px;\"><div class=\"ui_light floating task_status_change\">" +
                                        //"<a href=\"#nogo\" class=\"ui-sproutmenu\"><span class=\"ui-sproutmenu-status\"><img title=\"Edit Status\" onclick=\"PerformClick(this.id)\" src=\"../Contents/img/icon_edit.png\" class=\"edit_button\" id=\"img_" + item.Id + "_" + item.Status + "\"></span></a></div></section>" +
                                                    "<a class=\"ui-sproutmenu\"><span class=\"ui-sproutmenu-status\" style=\"margin-left:-110px;\"><img title=\"Edit Status\" onclick=\"PerformClick(this.id)\" src=\"../Contents/img/icon_edit.png\" class=\"edit_button\" id=\"img_" + item.Id + "_" + item.Status + "\"></span></a></div></section>" +
                                                   "<section class=\"task-status\" style=\"width: 65px; margin-right: 39px;\">" +
                                                  "<div class=\"ui_light floating task_status_change\">" +
                                                           "<span class=\"ui-sproutmenu-status\">" + status + "</span>" +
                                                  "</div>" +
                                              "</section>" +

                                               "<section class=\"task-status\" style=\"width: 65px; margin-right: 39px;\">" +
                                                  "<div class=\"ui_light floating task_status_change\">" +
                                                           "<span  class=\"ui-sproutmenu-status\"><img title=\"Delete\" onclick=\"deletequeue(this.id)\" style=\"width:33px;margin-left:60px;margin-top:-10px;\" img src=\"../Contents/img/deleteimage.png\" img id=\"" + item.Id + "\"></span>" +
                                                  "</div>" +
                                              "</section>" +

                                           "</div></section>";
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.Message);
                                Console.WriteLine(ex.Message);

                            }
                    }
                    else
                    {
                        schmessages = "<section class=\"section\"><div class=\"js-task-cont read\"><section class=\"task-owner\">" +
                          "<img width=\"32\" height=\"32\" border=\"0\" class=\"avatar\" src=\"" + profurls + "\">" +
                          "</section><section class=\"task-activity third\"><p>" + user.UserName + "</p><div></div><p></p></section><section style=\"margin-right: 6px; width: 31%; height: auto;\" class=\"task-message font-13 third\">" +
                          "<a class=\"tip_left\">No Scheduled Messages</a></section><section style=\"width:113px;\" class=\"task-status\"><span class=\"ficon task_active\"></span>" +
                            //"<div class=\"ui_light floating task_status_change\"><a class=\"ui-sproutmenu\" href=\"#nogo\">" +
                          "<div class=\"ui_light floating task_status_change\"><a class=\"ui-sproutmenu\">" +
                          "<span class=\"ui-sproutmenu-status\"></span></a></div></section></div></section>";
                    }
                    Response.Write(schmessages);

                }
                else if (Request.QueryString["op"] == "drafts_messages")
                {

                }
                else if (Request.QueryString["op"] == "updatequeue")
                {

                    updatequeue(Request.QueryString["id"], Request.QueryString["status"]);

                }


                else if (Request.QueryString["op"] == "messagechk")
                {
                    SocioBoard.Domain.Messages mstable = new SocioBoard.Domain.Messages();
                    string[] types = Request.QueryString["type[]"].Split(',');
                    DataSet ds = (DataSet)Session["MessageDataTable"];
                    DataSet dss = DataTableGenerator.CreateDataSetForTable(mstable);
                    DataTable dtt = dss.Tables[0];
                    DataView dv = new DataView(dtt);
                    AjaxMessage ajxfed = new AjaxMessage();
                    string message = string.Empty;
                    foreach (var item in types)
                    {
                        try
                        {
                            DataRow[] foundRows = ds.Tables[0].Select("Type = '" + item + "'");
                            foreach (var child in foundRows)
                            {
                                dtt.ImportRow(child);
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            Console.WriteLine(ex.Message);
                        }
                    }
                    message = ajxfed.BindData(dtt);
                    Response.Write(message);
                }
            }
        }

        private string RemainingAccount(SocioBoard.Domain.User user)
        {

            string res = "using " + Session["ProfileCount"].ToString() + " of " + Session["TotalAccount"].ToString();
            try
            {
                SocialProfilesRepository objSocioRepo = new SocialProfilesRepository();
                Session["ProfileCount"] = objSocioRepo.getAllSocialProfilesOfUser(user.Id).Count;

                res = "using " + Session["ProfileCount"].ToString() + " of " + Session["TotalAccount"].ToString();
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                Console.WriteLine(ex.Message);
            }
            return res;
        }
        public string CompareDateWithServer(string clientdate, string scheduletime)
        {
            DateTime client = Convert.ToDateTime(clientdate);
            string strTimeNow = String.Format("{0:s}", client).Replace('T', ' ');

            DateTime server = DateTime.Now;
            DateTime schedule = Convert.ToDateTime(scheduletime);
            if (DateTime.Compare(client, server) > 0)
            {

                double minutes = (server - client).TotalMinutes;
                schedule = schedule.AddMinutes(minutes);

            }
            else if (DateTime.Compare(client, server) == 0)
            {

            }
            else if (DateTime.Compare(client, server) < 0)
            {
                double minutes = (server - client).TotalMinutes;
                schedule = schedule.AddMinutes(minutes);
            }
            return schedule.ToString();
        }


        public string CompareDateWithclient(string clientdate, string scheduletime)
        {
            DateTime client = Convert.ToDateTime(clientdate);
            string strTimeNow = String.Format("{0:s}", client).Replace('T', ' ');

            DateTime server = DateTime.Now;
            DateTime schedule = Convert.ToDateTime(scheduletime);
            if (DateTime.Compare(client, server) > 0)
            {

                double minutes = (server - client).TotalMinutes;
                schedule = schedule.AddMinutes(minutes);

            }
            else if (DateTime.Compare(client, server) == 0)
            {


            }
            else if (DateTime.Compare(client, server) < 0)
            {
                double minutes = (server - client).TotalMinutes;
                schedule = schedule.AddMinutes(-minutes);
            }
            return schedule.ToString();
        }


        public string CompareDateWithServerNew(DateTime clientdate, DateTime server, DateTime scheduletime)
        {
            DateTime client = Convert.ToDateTime(clientdate);
            string strTimeNow = String.Format("{0:s}", client).Replace('T', ' ');
            DateTime schedule = Convert.ToDateTime(scheduletime);
            if (DateTime.Compare(client, server) > 0)
            {

                //double minutes = (server - client).TotalMinutes;
                double minutes = (client - server).TotalMinutes;
                schedule = schedule.AddMinutes(minutes);

            }
            else if (DateTime.Compare(client, server) == 0)
            {

            }
            else if (DateTime.Compare(client, server) < 0)
            {
                //double minutes = (server - client).TotalMinutes;
                double minutes = (client - server).TotalMinutes;
                schedule = schedule.AddMinutes(minutes);
            }
            return schedule.ToString();
        }




        public void updatequeue(string id, string stat)
        {
            bool check = false;
            ScheduledMessageRepository obj = new ScheduledMessageRepository();
            if (stat.ToUpper() == "TRUE")
            {
                check = true;
            }
            else
            {
                check = false;
            }
            obj.UpdateProfileScheduleMessageStat(Guid.Parse(id), check);
        }

        public string gethtmlfromstring(string str)
        {
            string ret = string.Empty;

            ret = str.Replace("\n", "</br>");
            return ret;
        }

        public string getsortpofilename(string name)
        {
            string ret = string.Empty;
            try
            {
                if (name.Length > 10)
                {
                    ret = name.Substring(0, 10) + "..";
                }
                else
                {
                    ret = name;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return ret;

        }
    }
}