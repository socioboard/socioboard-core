using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Model;
using System.Collections;
using SocioBoard.Helper;
using Facebook;
using GlobusInstagramLib.App.Core;
using System.Configuration;
using GlobusInstagramLib.Authentication;
using GlobusTwitterLib.Authentication;
using System.Text.RegularExpressions;
using GlobusLinkedinLib.App.Core;
using GlobusLinkedinLib.Authentication;
using log4net;
using Newtonsoft.Json.Linq;
using System.Data;
using GlobusTumblrLib.Authentication;
using GlobusTumblerLib;
using GlobusTumblerLib.Tumblr.Core.BlogMethods;
using GlobusGooglePlusLib.Youtube.Core;
using GlobusGooglePlusLib.Authentication;
using System.IO;

namespace SocialSuitePro.Feeds
{
    public partial class AjaxFeeds : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(AjaxFeeds));

        public static int instagramcount = 0;
        public static int facebookwallcount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {


            SocioBoard.Domain.User use = (SocioBoard.Domain.User)Session["LoggedUser"];

            try
            {
                if (use == null)
                    Response.Redirect("/Default.aspx");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            try
            {
                ProcessRequest();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

        }

        public void ProcessRequest()
        {
            SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)Session["GroupName"];
            TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
            TwitterAccountRepository twtaccountrepo = new TwitterAccountRepository();
            FacebookAccountRepository facerepo = new FacebookAccountRepository();
            LinkedInAccountRepository linkaccrepo = new LinkedInAccountRepository();
            InstagramAccountRepository insaccrepo = new InstagramAccountRepository();
            TumblrAccountRepository tumblraccrepo = new TumblrAccountRepository();
            TumblrFeedRepository objTumblrFeedRepository = new TumblrFeedRepository();
            YoutubeAccountRepository ytaccrepo = new YoutubeAccountRepository();
            YoutubeChannelRepository ytrchannelrpo = new YoutubeChannelRepository();


            if (!string.IsNullOrEmpty(Request.QueryString["op"]))
            {
                SocioBoard.Domain.User user = (SocioBoard.Domain.User)Session["LoggedUser"];
                if (Request.QueryString["op"] == "networkprofiles")
                {
                    #region NetworkProfiles
                    string profiles = string.Empty;
                    List<TeamMemberProfile> allprofiles = objTeamMemberProfileRepository.getAllTeamMemberProfilesOfTeam(team.Id);

                    int facebookcount = 0;
                    int twittercount = 0;
                    int linkedincount = 0;
                    int instagramcount = 0;
                    int tumblrcount = 0;
                    int youtubecount = 0;
                    int totalcounts = 0;

                    foreach (TeamMemberProfile items in allprofiles)
                    {
                        totalcounts++;

                        if (Request.QueryString["network"] == "facebook")
                        {

                            if (items.ProfileType == "facebook")
                            {
                                facebookcount++;
                                FacebookAccount faceaccount = facerepo.getFacebookAccountDetailsById(items.ProfileId);

                                profiles += "<li><a id=\"lifb_" + faceaccount.FbUserId + "\" href=\"#\" onclick=\"facebookdetails('" + faceaccount.FbUserId + "');\" class=\"active\">" + faceaccount.FbUserName + "</a> </li>";
                            }
                            if (totalcounts == allprofiles.Count)
                            {
                                if (facebookcount == 0)
                                { profiles = "<li>No Records Found !</li>"; }

                            }

                        }

                        else if (Request.QueryString["network"] == "twitter")
                        {
                            if (items.ProfileType == "twitter")
                            {
                                twittercount++;
                                TwitterAccount twtaccount = twtaccountrepo.getUserInformation(items.ProfileId);

                                profiles += "<li><a id=\"litwt_" + twtaccount.TwitterUserId + "\" href=\"#\" onclick=\"twitterdetails('" + twtaccount.TwitterUserId + "');\" class=\"active\">" + twtaccount.TwitterScreenName + "</a> </li>";
                            }
                            if (totalcounts == allprofiles.Count)
                            {
                                if (twittercount == 0)
                                { profiles = "<li>No Records Found !</li>"; }

                            }

                        }

                        else if (Request.QueryString["network"] == "linkedin")
                        {
                            if (items.ProfileType == "linkedin")
                            {
                                linkedincount++;
                                LinkedInAccount linkedinaccount = linkaccrepo.getLinkedinAccountDetailsById(items.ProfileId);

                                profiles += "<li><a id=\"lilin_" + linkedinaccount.LinkedinUserId + "\" href=\"#\" onclick=\"linkedindetails('" + linkedinaccount.LinkedinUserId + "');\" class=\"active\">" + linkedinaccount.LinkedinUserName + "</a> </li>";
                            }
                            if (totalcounts == allprofiles.Count)
                            {
                                if (linkedincount == 0)
                                { profiles = "<li>No Records Found !</li>"; }

                            }


                        }

                        else if (Request.QueryString["network"] == "tumblr")
                        {
                            if (items.ProfileType == "tumblr")
                            {
                                tumblrcount++;
                                TumblrAccount tumblraccount = tumblraccrepo.getTumblrAccountDetailsById(items.ProfileId);

                                profiles += "<li><a id=\"lilin_" + tumblraccount.tblrUserName + "\" href=\"#\" onclick=\"tumblrdetails('" + tumblraccount.tblrUserName + "');\" class=\"active\">" + tumblraccount.tblrUserName + "</a> </li>";
                            }
                            if (totalcounts == allprofiles.Count)
                            {
                                if (tumblrcount == 0)
                                { profiles = "<li>No Records Found !</li>"; }

                            }
                        }


                        else if (Request.QueryString["network"] == "youtube")
                        {
                            if (items.ProfileType == "youtube")
                            {
                                youtubecount++;
                                YoutubeAccount youtubeaccount = ytaccrepo.getYoutubeAccountDetailsById(items.ProfileId);

                                profiles += "<li><a id=\"lilin_" + youtubeaccount.Ytusername + "\" href=\"#\" onclick=\"youtubedetails('" + youtubeaccount.Ytuserid + "','" + youtubeaccount.Refreshtoken + "');\" class=\"active\">" + youtubeaccount.Ytusername + "</a> </li>";
                            }
                            if (totalcounts == allprofiles.Count)
                            {
                                if (youtubecount == 0)
                                { profiles = "<li>No Records Found !</li>"; }

                            }
                        }





                        else if (Request.QueryString["network"] == "instagram")
                        {
                            if (items.ProfileType == "instagram")
                            {
                                instagramcount++;
                                InstagramAccount alstinstagram = insaccrepo.getInstagramAccountDetailsById(items.ProfileId);

                                profiles += "<li><a id=\"liins_" + alstinstagram.InstagramId + "\" href=\"#\" onclick=\"Instagramdetails('" + alstinstagram.InstagramId + "');\" class=\"active\">" + alstinstagram.InsUserName + "</a> </li>";
                            }
                            if (totalcounts == allprofiles.Count)
                            {
                                if (instagramcount == 0)
                                { profiles = "<li>No Records Found !</li>"; }
                            }
                        }

                    }

                    Response.Write(profiles);
                    #endregion
                }


                else if (Request.QueryString["op"] == "facebookwallposts")
                {
                    #region facebookwallposts
                    string messages = string.Empty;
                    string profileid = string.Empty;
                    string load = Request.QueryString["load"];
                    //Session[""] = profileid;
                    if (load == "first")
                    {
                        profileid = Request.QueryString["profileid"];
                        Session["FacebookProfileIdForFeeds"] = profileid;
                        facebookwallcount = 0;
                    }
                    else
                    {
                        profileid = (string)Session["FacebookProfileIdForFeeds"];
                        facebookwallcount = facebookwallcount + 10;
                    }


                    FacebookMessageRepository fbmsgrepo = new FacebookMessageRepository();
                    FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();

                    List<FacebookMessage> lsgfbmsgs = fbmsgrepo.getAllWallpostsOfProfile(profileid, facebookwallcount);

                    UrlExtractor urlext = new UrlExtractor();
                    foreach (FacebookMessage item in lsgfbmsgs)
                    {
                        try
                        {

                            string[] str = urlext.splitUrlFromString(item.Message);
                            messages += "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"" + item.FromProfileUrl + "\" onclick=\"getFacebookProfiles('" + item.FromId + "');\">" +
                                                         "</div><div class=\"pull-left feedcontent\">" +
                                //"<a href=\"#\" class=\"feednm\" onclick=\"getFacebookProfiles('" + item.FromId + "');\">" + item.FromName + "</a> <span>" + item.MessageDate +
                                                            "<a target=\"_blank\" href=\"http://www.facebook.com/" + item.FromId + "\" class=\"feednm\">" + item.FromName + "</a> <span>" + item.MessageDate +
                                                                " </span>" +
                                                             "<p>";

                            if (!string.IsNullOrEmpty(item.Picture))
                            {
                                //string largeimage = item.Picture.Replace("_s.jpg","_n.jpg");

                                messages += "<img src=\"" + item.Picture + "\" alt=\"\" onclick=\"fbimage('" + item.Picture + "');\" /><br/>";
                            }

                            foreach (string substritem in str)
                            {
                                try
                                {
                                    if (!string.IsNullOrEmpty(substritem))
                                    {
                                        if (substritem.Contains("http"))
                                        {
                                            messages += "<a target=\"_blank\" href=\"" + substritem + "\">" + substritem + "</a>";
                                        }
                                        else
                                        {
                                            string hrefPost = string.Empty;

                                            try
                                            {
                                                hrefPost = "https://www.facebook.com/" + item.FromId + "/posts/" + item.MessageId.Replace(item.FromId, string.Empty).Replace("_", string.Empty).Trim();

                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine("Error : " + ex.StackTrace);

                                            }
                                            if (!string.IsNullOrEmpty(hrefPost))
                                            {
                                                messages += "<a target=\"_blank\" href=\"" + hrefPost + "\">" + substritem + "</a>";//substritem;
                                            }
                                            else
                                            {
                                                messages += substritem;

                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error : " + ex.StackTrace);

                                }
                            }

                            //    messages += "</p>" +
                            //                "<a class=\"retweets\" href=\"#\">" +
                            //                "</a><p><span onclick=\"facebookLike('" + item.FbLike + "','" + profileid + "','" + item.MessageId + "')\" id=\"likefb_" + item.MessageId + "\" class=\"like\">Like</span><span id=\"commentfb_" + item.MessageId + "\" onclick=\"commentText('" + item.MessageId + "');\" class=\"comment\">Comment</span></p>" +
                            //                "<p class=\"commeent_box\"><input id=\"textfb_" + item.MessageId + "\" type=\"text\" class=\"put_comments\"></p>" +
                            //              "<p><span onclick=\"commentFB('" + item.MessageId + "','" + profileid + "')\" id=\"okfb_" + item.MessageId + "\" class=\"ok\">ok</span><span id=\"cancelfb_" + item.MessageId + "\" onclick=\"cancelFB('" + item.MessageId + "');\" class=\"cancel\"> cancel</span></p>" +
                            //                "</div>" +
                            //                "</li>";
                            //}



                            messages += "</p>" +
                                       "<a class=\"retweets\" href=\"#\">" +
                                       "</a><p><span onclick=\"facebookShare('" + profileid + "','" + item.MessageId + "')\" id=\"likefb_" + item.MessageId + "\" class=\"like\">Share</span><span onclick=\"facebookLike('" + item.FbLike + "','" + profileid + "','" + item.MessageId + "')\" id=\"likefb_" + item.MessageId + "\" class=\"like\">Like</span><span id=\"commentfb_" + item.MessageId + "\" onclick=\"commentText('" + item.MessageId + "');\" class=\"comment\">Comment</span></p>" +
                                       "<p class=\"commeent_box\"><input id=\"textfb_" + item.MessageId + "\" type=\"text\" class=\"put_comments\"></p>" +
                                     "<p><span onclick=\"commentFB('" + item.MessageId + "','" + profileid + "')\" id=\"okfb_" + item.MessageId + "\" class=\"ok\">ok</span><span id=\"cancelfb_" + item.MessageId + "\" onclick=\"cancelFB('" + item.MessageId + "');\" class=\"cancel\"> cancel</span></p>" +
                                       "</div>" +
                                       "</li>";
                        }




                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            Console.WriteLine(ex.Message);
                        }
                    }
                    Response.Write(messages);
                    #endregion
                }
                else if (Request.QueryString["op"] == "fblike")
                {
                    #region fblikes
                    try
                    {
                        //System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        //string line = "";
                        //line = sr.ReadToEnd();
                        //JObject jo = JObject.Parse(line);
                        //string accesstoken = Server.UrlDecode((string)jo["access"]);
                        //string id = Server.UrlDecode((string)jo["fbid"]);
                        string profileid = Request.QueryString["profileid"];
                        FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
                        FacebookAccount fbAccount = fbAccRepo.getFacebookAccountDetailsById(profileid, user.Id);
                        string id = Request.QueryString["fbid"];
                        FacebookClient fbClient = new FacebookClient(fbAccount.AccessToken);
                        var s = fbClient.Post(id + "/likes", null);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                    #endregion
                }


                else if (Request.QueryString["op"] == "fbshare")
                {
                    try
                    {
                        string profileid = Request.QueryString["profileid"];
                        string id = Request.QueryString["msgid"];
                        FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
                        FacebookAccount fbAccount = fbAccRepo.getFacebookAccountDetailsById(profileid);
                        FacebookClient fbClient = new FacebookClient(fbAccount.AccessToken);
                        var s = fbClient.Post(id + "/sharedposts", null);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }



                else if (Request.QueryString["op"] == "fbcomment")
                {
                    #region fbcomment
                    string profileid = Request.QueryString["profileid"];
                    string message = Request.QueryString["message"];
                    FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
                    FacebookAccount fbAccount = fbAccRepo.getFacebookAccountDetailsById(profileid, user.Id);
                    string id = Request.QueryString["fbid"];
                    FacebookClient fbClient = new FacebookClient(fbAccount.AccessToken);
                    var args = new Dictionary<string, object>();
                    args["message"] = message;
                    var s = fbClient.Post(id + "/comments", args);

                    #endregion
                }
                else if (Request.QueryString["op"] == "twitternetworkdetails")
                {
                    #region twitternetworkdetails
                    string messages = string.Empty;
                    string profileid = Request.QueryString["profileid"];
                    TwitterFeedRepository fbmsgrepo = new TwitterFeedRepository();
                    List<TwitterFeed> lsgfbmsgs = fbmsgrepo.getTwitterFeedOfProfile(profileid);
                    UrlExtractor urlext = new UrlExtractor();
                    foreach (TwitterFeed item in lsgfbmsgs)
                    {
                        try
                        {
                            messages += "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"" + item.FromProfileUrl + "\" onclick=\"detailsdiscoverytwitter('" + item.FromId + "');\">" +
                                                         "</div><div class=\"pull-left feedcontent\">" +
                                                            "<a href=\"#\" class=\"feednm\" onclick=\"detailsdiscoverytwitter('" + item.FromId + "');\">" + item.FromName + "</a> <span>" + item.FeedDate +
                                                                " </span>" +
                                                             "<p>";

                            string[] str = urlext.splitUrlFromString(item.Feed);

                            foreach (string substritem in str)
                            {
                                try
                                {
                                    if (!string.IsNullOrEmpty(substritem))
                                    {
                                        if (substritem.Contains("http"))
                                        {
                                            messages += "<a target=\"_blank\" href=\"" + substritem + "\">" + substritem + "</a>";
                                        }
                                        else
                                        {
                                            string hrefPost = string.Empty;

                                            try
                                            {
                                                //https://twitter.com/265982289/status/431552741341941760
                                                hrefPost = "https://twitter.com/" + item.FromId + "/status/" + item.MessageId.Replace(item.FromId, string.Empty).Replace("_", string.Empty).Trim();

                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine("Error : " + ex.StackTrace);

                                            }
                                            if (!string.IsNullOrEmpty(hrefPost))
                                            {
                                                messages += "<a target=\"_blank\" href=\"" + hrefPost + "\">" + substritem + "</a>";//substritem;
                                            }
                                            else
                                            {
                                                messages += substritem;

                                            }
                                        }
                                    }
                                }
                                catch (Exception)
                                {

                                }
                            }

                            messages += "</p>" +
                                   "<a class=\"retweets\" href=\"#\">" +
                                /*"<img alt=\"\" src=\"../contents/img/admin/arrow.png\">*/"</a><span></span>" +
                               "</div>" +
                           "</li>";
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            Console.WriteLine(ex.Message);
                        }
                    }
                    Response.Write(messages);
                    #endregion
                }
                else if (Request.QueryString["op"] == "scheduler")
                {
                    #region Schduler
                    string message = string.Empty;
                    string profileid = Request.QueryString["profileid"];
                    string network = Request.QueryString["network"];

                    if (network == "facebook")
                    {
                        ScheduledMessageRepository schmsgrepo = new ScheduledMessageRepository();
                        List<ScheduledMessage> lstschmsg = schmsgrepo.getAllMessagesOfUser(profileid);

                        if (lstschmsg.Count != 0)
                        {
                            foreach (ScheduledMessage item in lstschmsg)
                            {
                                try
                                {
                                    FacebookAccountRepository faceaccrepo = new FacebookAccountRepository();
                                    FacebookAccount faceacc = faceaccrepo.getFacebookAccountDetailsById(profileid);
                                    try
                                    {
                                        message += "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\"  src=\"https://graph.facebook.com/" + item.ProfileId + "/picture?type=small\">" +
                                                                     "</div><div class=\"pull-left feedcontent\">" +
                                                                        "<a href=\"#\" class=\"feednm\">" + faceacc.FbUserName + "</a> <span>" + item.ScheduleTime +
                                                                            " </span>" +
                                                                         "<p>" + item.ShareMessage + "</p>" +
                                                                         "<a class=\"retweets\" href=\"#\">" +
                                                                          "</a><span></span>" +
                                                                     "</div>" +
                                                                 "</li>";
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }

                            }
                        }
                        else
                        {
                            message = "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"../Contents/img/blank_img.png\">" +
                                                                    "</div><div class=\"pull-left feedcontent\">" +
                                                                       "<a href=\"#\" class=\"feednm\"></a> <span>" +
                                                                           " </span>" +
                                                                        "<p>No Scheduled Messages</p>" +
                                                                        "<a class=\"retweets\" href=\"#\">" +
                                                                         "</a><span></span>" +
                                                                    "</div>" +
                                                                "</li>";
                        }



                    }
                    else if (network == "twitter")
                    {
                        ScheduledMessageRepository schmsgrepo = new ScheduledMessageRepository();
                        List<ScheduledMessage> lstschmsg = schmsgrepo.getAllMessagesOfUser(profileid);

                        if (lstschmsg.Count != 0)
                        {
                            foreach (ScheduledMessage item in lstschmsg)
                            {
                                try
                                {
                                    TwitterAccountRepository twtaccrepo = new TwitterAccountRepository();
                                    TwitterAccount twtacc = twtaccrepo.getUserInformation(profileid);
                                    message += "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\"  src=\"" + twtacc.ProfileImageUrl + "\">" +
                                                                    "</div><div class=\"pull-left feedcontent\">" +
                                                                       "<a href=\"#\" class=\"feednm\">" + twtacc.TwitterScreenName + "</a> <span>" + item.ScheduleTime +
                                                                           " </span>" +
                                                                        "<p>" + item.ShareMessage + "</p>" +
                                                                        "<a class=\"retweets\" href=\"#\">" +
                                                                         "</a><span></span>" +
                                                                    "</div>" +
                                                                "</li>";
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                        }
                        else
                        {
                            message = "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"../Contents/img/blank_img.png\">" +
                                                                  "</div><div class=\"pull-left feedcontent\">" +
                                                                     "<a href=\"#\" class=\"feednm\"></a> <span>" +
                                                                         " </span>" +
                                                                      "<p>No Scheduled Messages</p>" +
                                                                      "<a class=\"retweets\" href=\"#\">" +
                                                                       "</a><span></span>" +
                                                                  "</div>" +
                                                              "</li>";
                        }
                    }
                    else if (network == "linkedin")
                    {
                        ScheduledMessageRepository schmsgrepo = new ScheduledMessageRepository();
                        List<ScheduledMessage> lstschmsg = schmsgrepo.getAllMessagesOfUser(profileid);

                        if (lstschmsg.Count != 0)
                        {
                            foreach (ScheduledMessage item in lstschmsg)
                            {
                                try
                                {
                                    LinkedInAccountRepository linkedinrepo = new LinkedInAccountRepository();
                                    LinkedInAccount linkedacc = linkedinrepo.getUserInformation(profileid);
                                    message += "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\"  src=\"" + linkedacc.ProfileImageUrl + "\">" +
                                                                        "</div><div class=\"pull-left feedcontent\">" +
                                                                           "<a href=\"#\" class=\"feednm\">" + linkedacc.LinkedinUserName + "</a> <span>" + item.ScheduleTime +
                                                                               " </span>" +
                                                                            "<p>" + item.ShareMessage + "</p>" +
                                                                            "<a class=\"retweets\" href=\"#\">" +
                                                                             "</a><span></span>" +
                                                                        "</div>" +
                                                                    "</li>";
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error : " + ex.StackTrace);

                                }
                            }
                        }
                        else
                        {
                            message = "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"../Contents/img/blank_img.png\">" +
                                                                 "</div><div class=\"pull-left feedcontent\">" +
                                                                    "<a href=\"#\" class=\"feednm\"></a> <span>" +
                                                                        " </span>" +
                                                                     "<p>No Scheduled Messages</p>" +
                                                                     "<a class=\"retweets\" href=\"#\">" +
                                                                      "</a><span></span>" +
                                                                 "</div>" +
                                                             "</li>";

                        }

                    }




                    Response.Write(message);
                    #endregion
                }
                else if (Request.QueryString["op"] == "facebookfeeds")
                {
                    #region facebookfeeds
                    string message = string.Empty;
                    string profileid = Request.QueryString["profileid"];
                    FacebookAccountRepository faceaccrepo = new FacebookAccountRepository();
                    FacebookAccount faceaac = faceaccrepo.getFacebookAccountDetailsById(profileid);
                    FacebookFeedRepository facefeedrepo = new FacebookFeedRepository();
                    List<FacebookFeed> lstfbfeed = facefeedrepo.getAllFacebookUserFeeds(profileid);
                    UrlExtractor urlext = new UrlExtractor();
                    foreach (FacebookFeed item in lstfbfeed)
                    {
                        try
                        {
                            message += "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"https://graph.facebook.com/" + item.ProfileId + "/picture?type=small\" onclick=\"getFacebookProfiles('" + item.FromId + "');\">" +
                                                                                     "</div><div class=\"pull-left feedcontent\">" +
                                                                                        "<a href=\"#\" class=\"feednm\" onclick=\"getFacebookProfiles('" + item.FromId + "');\">" + faceaac.FbUserName + "</a> <span>" + item.FeedDate +
                                                                                            " </span>" +
                                                                                         "<p>";

                            string[] str = urlext.splitUrlFromString(item.FeedDescription);

                            foreach (string substritem in str)
                            {
                                try
                                {
                                    if (!string.IsNullOrEmpty(substritem))
                                    {
                                        if (substritem.Contains("http"))
                                        {
                                            message += "<a target=\"_blank\" href=\"" + substritem + "\">" + substritem + "</a>";
                                        }
                                        else
                                        {
                                            string hrefPost = string.Empty;

                                            try
                                            {
                                                hrefPost = "https://www.facebook.com/" + item.FromId + "/posts/" + item.FeedId.Replace(item.FromId, string.Empty).Replace("_", string.Empty).Trim();

                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine("Error : " + ex.StackTrace);

                                            }
                                            if (!string.IsNullOrEmpty(hrefPost))
                                            {
                                                message += "<a target=\"_blank\" href=\"" + hrefPost + "\">" + substritem + "</a>";//substritem;
                                            }
                                            else
                                            {
                                                message += substritem;

                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error : " + ex.StackTrace);

                                }
                            }


                            message += "</p>" +
                                         "<a class=\"retweets\" href=\"#\">" +
                                          "</a><span></span>" +
                                     "</div>" +
                                 "</li>";
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error : " + ex.StackTrace);

                        }
                    }
                    Response.Write(message);
                    #endregion
                }
                else if (Request.QueryString["op"] == "twitterfeeds")
                {
                    #region twitternfeeds
                    string message = string.Empty;
                    string profileid = Request.QueryString["profileid"];
                    TwitterMessageRepository twtmsgreop = new TwitterMessageRepository();
                    List<TwitterMessage> lstmsg = twtmsgreop.getAllTwitterMessagesOfProfile(profileid);
                    //TwitterFeedRepository twtmsgrepo = new TwitterFeedRepository();
                    //List<TwitterFeed>  lstfeed =  twtmsgrepo.getTwitterFeedOfProfile(profileid);
                    UrlExtractor urlext = new UrlExtractor();
                    foreach (TwitterMessage item in lstmsg)
                    {
                        try
                        {
                            message += "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"" + item.FromProfileUrl + "\" onclick=\"detailsdiscoverytwitter('" + item.FromId + "');\">" +
                                                         "</div><div class=\"pull-left feedcontent\">" +
                                                            "<a href=\"#\" class=\"feednm\" onclick=\"detailsdiscoverytwitter('" + item.FromId + "');\">" + item.FromName + "</a> <span>" + item.MessageDate +
                                                                " </span>" +
                                                             "<p>";

                            string[] str = urlext.splitUrlFromString(item.TwitterMsg);

                            foreach (string substritem in str)
                            {
                                try
                                {
                                    if (!string.IsNullOrEmpty(substritem))
                                    {
                                        if (substritem.Contains("http"))
                                        {
                                            message += "<a target=\"_blank\" href=\"" + substritem + "\">" + substritem + "</a>";
                                        }
                                        else
                                        {
                                            string hrefPost = string.Empty;

                                            try
                                            {
                                                //https://twitter.com/265982289/status/431552741341941760

                                                hrefPost = "https://twitter.com/" + item.FromId + "/status/" + item.MessageId.Replace(item.FromId, string.Empty).Replace("_", string.Empty).Trim();

                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine("Error : " + ex.StackTrace);

                                            }
                                            if (!string.IsNullOrEmpty(hrefPost))
                                            {
                                                message += "<a target=\"_blank\" href=\"" + hrefPost + "\">" + substritem + "</a>";//substritem;
                                            }
                                            else
                                            {
                                                message += substritem;

                                            }
                                        }
                                    }
                                }
                                catch (Exception)
                                {

                                }
                            }
                            message += "</p>" +
                                                              "<a class=\"retweets\" href=\"#\">" +
                                /*"<img alt=\"\" src=\"../Contents/img/admin/arrow.png\">*/"</a><span></span>" +
                                                          "</div>" +
                                                      "</li>";
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            Console.WriteLine(ex.Message);
                        }
                    }
                    Response.Write(message);
                    #endregion
                }
                else if (Request.QueryString["op"] == "linkedinwallposts")
                {
                    #region linkedinwallposts
                    string message = string.Empty;
                    string profileid = Request.QueryString["profileid"];

                    LinkedInFeedRepository linkedinfeedrepo = new LinkedInFeedRepository();
                    List<LinkedInFeed> lstfeed = linkedinfeedrepo.getAllLinkedInFeedsOfProfile(profileid);


                    if (lstfeed != null)
                    {
                        if (lstfeed.Count != 0)
                        {
                            if (lstfeed.Count > 500)
                            {
                                int check = 0;
                                foreach (LinkedInFeed item in lstfeed)
                                {
                                    check++;
                                    try
                                    {
                                        message += "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"" + item.FromPicUrl + "\">" +
                                                                                              "</div><div class=\"pull-left feedcontent\">" +
                                                                                                 "<a style=\"cursor:default\" class=\"feednm\">" + item.FromName + "</a> <span>" + item.FeedsDate +
                                                                                                     " </span>" +
                                                                                                  "<p>" + item.Feeds + "</p>" +
                                                                                                  "<a class=\"retweets\" href=\"#\">" +
                                                                                                  "</a><span></span>" +
                                                                                              "</div>" +
                                                                                          "</li>";
                                    }
                                    catch (Exception ex)
                                    {
                                        logger.Error(ex.Message);
                                        Console.WriteLine(ex.Message);
                                    }
                                    if (check == 500)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {

                                foreach (LinkedInFeed item in lstfeed)
                                {
                                    try
                                    {

                                        message += "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"" + item.FromPicUrl + "\">" +
                                                                                              "</div><div class=\"pull-left feedcontent\">" +
                                                                                                 "<a style=\"cursor:default\" class=\"feednm\">" + item.FromName + "</a> <span>" + item.FeedsDate +
                                                                                                     " </span>" +
                                                                                                  "<p>" + item.Feeds + "</p>" +
                                                                                                  "<a class=\"retweets\" href=\"#\">" +
                                                                                                  "</a><span></span>" +
                                                                                              "</div>" +
                                                                                          "</li>";
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
                            message = "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"../Contents/img/blank_img.png\">" +
                                                                   "</div><div class=\"pull-left feedcontent\">" +
                                                                      "<a href=\"#\" class=\"feednm\"></a> <span>" +
                                                                          " </span>" +
                                                                       "<p>No Messages Found</p>" +
                                                                       "<a class=\"retweets\" href=\"#\">" +
                                                                        "</a><span></span>" +
                                                                   "</div>" +
                                                               "</li>";
                        }
                    }
                    Response.Write(message);
                    #endregion
                }

                else if (Request.QueryString["op"] == "tumblrimages")
                {

                    #region tumblrBlog
                    string messages = string.Empty;
                    string profileid = Request.QueryString["profileid"];
                    TumblrAccount objTumblrAccount = tumblraccrepo.getTumblrAccountDetailsById(profileid);
                    List<TumblrFeed> lstfeed = objTumblrFeedRepository.getFeedOfProfile(profileid);

                    string strTumblrImage = string.Empty;
                    string image = string.Empty;
                    try
                    {

                        if (lstfeed.Count != 0)
                        {


                            strTumblrImage += "<div class=\"feedcontainer\"><div class=\"pull-left span\"><div id=\"tumblrcontents\">" +
                       "<a href=\"#\"><img onClick=\"Bpopup()\" src=\"../Contents/img/share.png\" width=\"20\" title=\"Share Content\" /></a>" +
                    "</div></div>";


                            foreach (TumblrFeed feed in lstfeed)
                            {
                                if (string.IsNullOrEmpty(feed.imageurl))
                                {
                                    image = "../../Contents/img/admin/Demo-Image.png";


                                }
                                else
                                {
                                    image = feed.imageurl;
                                }



                                try
                                {
                                    //    strTumblrImage += "<div class=\"span3\" class=\"row-fluid\"><div class=\"span box whitebg tumb_bg feedwrap\"><div class=\"tumb_title\"><span class=\"tumb_span\">" + feed.blogname+ "</span></div><div class=\"pic tumb_pic\"><img onclick=\"tumblrimage('" + feed.imageurl + "')\" alt=\"\" src=\"" + image + "\"></div><div class=\"topicon\">" +
                                    //"<div class=\"pull-right\" id=\"like\"><a title=\"\" href=\"#\" onClick=\"LikePic('" + feed.ProfileId + "','" + feed.Id + "','" + objTumblrAccount.tblrAccessToken + "','" + objTumblrAccount.tblrAccessTokenSecret + "','" + feed.liked + "','" + feed.notes + "')\" >" + getlike(feed.liked, feed.ProfileId) + "</a><a title=\"\" href=\"#\"><img onClick=\"UnfollowBlog('" + feed.ProfileId + "','" + feed.Id + "','" + objTumblrAccount.tblrAccessToken + "','" + objTumblrAccount.tblrAccessTokenSecret + "','" + feed.blogname + "')\"   width=\"14\" alt=\"\" src=\"../Contents/img/admin/speech-bubble-left.png\"  style=\"margin-top: 9px;\"></a>" +
                                    //"</div></div><div class=\"desc\"><p></p><span class=\"pull-left pics_space span4\">" +
                                    //"<img width=\"12\" alt=\"\" src=\"../Contents/img/admin/dil.png\"> " + feed.notes + "</span><div class=\"clearfix\">" +
                                    //"<div class=\"tumb_description\"><p class=\"feed_slug\"><strong>" + feed.slug + "</strong></p><p class=\"teaser\">" + feed.description + "</p></div></div>";





                                    strTumblrImage += "<div class=\"span3\" class=\"row-fluid\"><div class=\"span box whitebg tumb_bg feedwrap\"><div class=\"tumb_title\"><span class=\"tumb_span\">" + feed.blogname + "</span></div><div class=\"pic tumb_pic\"><img onclick=\"tumblrimage('" + feed.imageurl + "')\" alt=\"\" src=\"" + image + "\"></div><div class=\"topicon\">" +
                                                                    "<div class=\"pull-right\" id=\"like\"><a title=\"\" href=\"#\" onClick=\"LikePic('" + feed.ProfileId + "','" + feed.Id + "','" + objTumblrAccount.tblrAccessToken + "','" + objTumblrAccount.tblrAccessTokenSecret + "','" + feed.liked + "','" + feed.notes + "')\" >" + getlike(feed.liked, feed.ProfileId) + "</a><a title=\"\" href=\"#\"><img onClick=\"UnfollowBlog('" + feed.ProfileId + "','" + feed.Id + "','" + objTumblrAccount.tblrAccessToken + "','" + objTumblrAccount.tblrAccessTokenSecret + "','" + feed.blogname + "')\"   width=\"14\" alt=\"\" src=\"../Contents/img/admin/speech-bubble-left.png\"  style=\"margin-top: 9px;\"></a>" +
                                                                    "</div></div><div class=\"desc\"><p></p><span class=\"pull-left pics_space span4\">" +
                                                                    "<img width=\"12\" alt=\"\" src=\"../Contents/img/admin/dil.png\"> " + feed.notes + "</span><div class=\"clearfix\">" +
                                                                    "<div class=\"tumb_description\"><p class=\"feed_slug\"><strong>" + feed.slug + "</strong></p><p class=\"teaser\">" + feed.description + "</p></div></div>";





                                    strTumblrImage += "</div></div></div>";
                                }
                                catch (Exception ex)
                                {
                                    logger.Error(ex.Message);
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            strTumblrImage += "</div>";
                        }
                        else
                        {
                            if (instagramcount == 0)
                            {
                                strTumblrImage = "<div class=\"grid\"><div class=\"box whitebg feedwrap\">" +
                                             "<div class=\"topicon\"><div class=\"pull-left\"></div><div class=\"pull-right\">" +
                                 "<a href=\"#\" title=\"\"></a><a href=\"#\" title=\"\"></a></div></div><div class=\"pic\">" +
                                 "<img src=\"../Contents/img/no_image_found.png\" alt=\"\"></div><div class=\"desc\"><p></p></div></div></div>";
                            }
                        }


                        Response.Write(strTumblrImage);


                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }



                    #endregion
                }
                //VIDEOS
                else if (Request.QueryString["op"] == "youtubechannel")
                {
                    #region youtube_channel
                    string thumbnail = string.Empty;
                    string videoid = string.Empty;
                    string strYoutubechanell = string.Empty;
                    string GooglePlusUserId = Request.QueryString["profileid"];
                    string accesstoken = Request.QueryString["accesstoken"];
                    oAuthTokenYoutube objoAuthTokenYoutube = new oAuthTokenYoutube();
                    string finaltoken = objoAuthTokenYoutube.GetAccessToken(accesstoken);
                    string strfinaltoken = string.Empty;


                    try
                    {

                        if (!finaltoken.StartsWith("["))
                            finaltoken = "[" + finaltoken + "]";
                        JArray objArray = JArray.Parse(finaltoken);


                        foreach (var item in objArray)
                        {
                            try
                            {
                                strfinaltoken = item["access_token"].ToString();
                                break;
                            }
                            catch (Exception ex)
                            {
                                //logger.Error(ex.StackTrace);
                                Console.WriteLine(ex.StackTrace);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //logger.Error(ex.StackTrace);
                        Console.WriteLine(ex.StackTrace);
                    }


                    YoutubeChannel objChnnelData = ytrchannelrpo.getYoutubeChannelDetailsById(GooglePlusUserId);
                    PlaylistItems objPlaylistItems = new PlaylistItems();
                    string objDetails = objPlaylistItems.Get_PlaylistItems_List(strfinaltoken, GlobusGooglePlusLib.Authentication.oAuthTokenYoutube.Parts.snippet.ToString(), objChnnelData.Uploadsid);

                    JObject obj = JObject.Parse(objDetails);

                    JArray array = (JArray)obj["items"];


                    //strYoutubechanell = " <div class=\"row\"> ";

                    int rowCount = 0;
                    int columnCount = 0;

                    //strYoutubechanell = "<div class=\"row top_select\"> <div class=\"pull-left\"><a href=\"#\"><div class=\"YtIns\">Hello</div></a></div> <div class=\"pull-right\"><select class=\"form-control\" onchange=\"dropDownChange(this,'" + GooglePlusUserId + "','" + accesstoken + "')\"><option>Video</option> <option>Play list</option> <option>Activities</option></select></div></div><div class=\"container yt_details\">";
                    //strYoutubechanell = "<div class=\"row\">  <div class=\"pull-right\"><select class=\"form-control\" onchange=\"dropDownChange(this,'" + GooglePlusUserId + "','" + accesstoken + "')\"><option>Video</option> <option>Play list</option> <option>Activities</option></select></div></div>";
                    strYoutubechanell = "<div class=\"row top_select\"> <div class=\"pull-left\"><a href=\"#\"><div class=\"YtIns\">Hello</div></a></div> <div class=\"pull-right\">"
                                        + "<ul class=\"nav nav-tabs\"><li class=\"active\"><a href=\"#VIDEO\" onclick=\"dropDownChange(this,'" + GooglePlusUserId + "','" + accesstoken + "')\" data-toggle=\"tab\">VIDEO</a></li><li><a href=\"#ACT\" onclick=\"dropDownChange(this,'" + GooglePlusUserId + "','" + accesstoken + "')\" data-toggle=\"tab\">ACTIVITES</a></li><li>"
                    + "<a href=\"#SUB\" onclick=\"dropDownChange(this,'" + GooglePlusUserId + "','" + accesstoken + "')\" data-toggle=\"tab\">SUBSCRIBTIONS</a></li></ul></div></div><div class=\"tab-content yt_details_container\"><div class=\"tab-pane active\" id=\"ACT\">"
                    + "<div class=\"container yt_details\">";



                    string strYoutubechanell1 = string.Empty;

                    foreach (var item in array)
                    {
                        columnCount++;
                        try
                        {
                            thumbnail = item["snippet"]["thumbnails"]["maxres"]["url"].ToString();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);

                        }
                        if (string.IsNullOrEmpty(thumbnail))
                        {
                            try
                            {
                                thumbnail = item["snippet"]["thumbnails"]["high"]["url"].ToString();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                            }
                        }

                        try
                        {
                            videoid = item["snippet"]["resourceId"]["videoId"].ToString();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);

                        }


                        string viewCount = string.Empty;
                        string likeCount = string.Empty;
                        string dislikeCount = string.Empty;
                        string favoriteCount = string.Empty;
                        string commentCount = string.Empty;
                        if (!string.IsNullOrEmpty(videoid))
                        {

                            try
                            {
                                GlobusGooglePlusLib.Youtube.Core.Video ObjClsVideo = new Video();

                                string videoDetails = ObjClsVideo.Get_VideoDetails_byId(videoid, strfinaltoken, "snippet,statistics");

                                JObject JobjvideoDetails = JObject.Parse(videoDetails);

                                var JArrvideoDetails = (JArray)(JobjvideoDetails["items"]);

                                foreach (var DataVal in JArrvideoDetails)
                                {
                                    viewCount = DataVal["statistics"]["viewCount"].ToString();
                                    likeCount = DataVal["statistics"]["likeCount"].ToString();
                                    dislikeCount = DataVal["statistics"]["dislikeCount"].ToString();
                                    favoriteCount = DataVal["statistics"]["favoriteCount"].ToString();
                                    commentCount = DataVal["statistics"]["commentCount"].ToString();
                                    break;
                                }


                            }
                            catch (Exception)
                            {
                            }

                        }


                        //strYoutubechanell1 += "<div class=\"span4\">" +
                        //                    "<div class=\"well\">" +
                        //                     "<div class=\"video-containers thumbnail\">" +
                        //                     "<img onclick=\"youtubevideo('" + videoid + "')\" alt=\"\" src=\"" + thumbnail + "\">" +
                        //                     "</div><span class=\"pull-left\"><a href=\"#\"> &nbsp;<i class=\"icon-eye-open\"></i></a>" +
                        //                     "</span></div></div>";

                        strYoutubechanell1 += "<div class=\"span3\">" +
                                            "<div class=\"span box whitebg tumb_bg\">" +
                                             "<div class=\"yt_title\"></div><div class=\"video-containers thumbnail\">" +
                                             "<img onclick=\"youtubevideo('" + videoid + "')\" alt=\"\" src=\"" + thumbnail + "\">" +
                                             "</div><div class=\"icons\" style=\"width: 225px; float: left;\"><span class=\"span6 pull-left\">" +
                                             "<a href=\"#\" style=\"float: left;\"> &nbsp;<i style=\"color: green;\" class=\"icon-hand-up\"></i></a><span class=\"pull-left\">" + likeCount + "</span></a><a href=\"#\" style=\"float: left;\"> &nbsp;<i style=\"color: red;\" class=\"icon-hand-down\"></i><span>" + dislikeCount + "</span>" +
                                             "</a></span><span class=\"pull-right\"><a href=\"#\"> &nbsp;<i style=\"color: red;\" class=\"icon-eye-open\"></i><span>" + viewCount + "</span></a></span></div><div class=\"yt_description\"></div></div></div>";


                        try
                        {

                            if (rowCount == 3)
                            {
                                //strYoutubechanell += " <div class=\"row space\">" + strYoutubechanell1 + "</div></div></div>";
                                strYoutubechanell += " <div class=\"row space\">" + strYoutubechanell1 + "</div>";
                                strYoutubechanell1 = string.Empty;
                                rowCount = 0;
                            }
                            else
                            {
                                rowCount++;
                            }
                            if (!strYoutubechanell.Contains(strYoutubechanell1) && array.Count == columnCount)
                            {
                                //strYoutubechanell += " <div class=\"row space\">" + strYoutubechanell1 + "</div></div>";
                                strYoutubechanell += " <div class=\"row space\">" + strYoutubechanell1 + "</div>";
                            }

                        }
                        catch (Exception)
                        {

                        }



                        //strYoutubechanell += "<div class=\"span3\" class=\"row-fluid\"><div class=\"span12 box whitebg feedwrap\"><div class=\"topicon\"><div class=\"pull-left\">" +

                        //                                        "</div><div class=\"pull-right\" id=\"like\"><a title=\"\" href=\"#\" onClick=\"LikePic()\" ></a><a title=\"\" href=\"#\"></a>" +
                        //                                        "</div></div><div class=\"pic\"><img onclick=\"youtubevideo('" + videoid + "')\" alt=\"\" src=\"" + thumbnail + "\"></div><div class=\"desc\"><p></p><span class=\"pull-left span3\">" +
                        //                                        "<img width=\"12\" alt=\"\" src=\"../Contents/img/admin/dil.png\"></span><div class=\"clearfix\">" +
                        //                                        "</div>";



                        //strYoutubechanell += "</div></div></div>";



                    }

                    Response.Write("<div id=\"ACT\" class=\"tab-pane active\"><div class=\"container yt_details\">" + strYoutubechanell + "\"</div></div>");

                    #endregion
                }

                //ACTIVITIES
                else if (Request.QueryString["op"] == "youtubeactivity")
                {
                    #region youtube_ACTIVITIES
                    
                    string strYoutubechanell = string.Empty;
                    string GooglePlusUserId = Request.QueryString["profileid"];
                    string accesstoken = Request.QueryString["accesstoken"];
                    oAuthTokenYoutube objoAuthTokenYoutube = new oAuthTokenYoutube();
                    string finaltoken = objoAuthTokenYoutube.GetAccessToken(accesstoken);
                    string strfinaltoken = string.Empty;

                    try
                    {

                        if (!finaltoken.StartsWith("["))
                            finaltoken = "[" + finaltoken + "]";
                        JArray objArray = JArray.Parse(finaltoken);


                        foreach (var item in objArray)
                        {
                            try
                            {

                                strfinaltoken = item["access_token"].ToString();

                                break;

                            }
                            catch (Exception ex)
                            {
                                //logger.Error(ex.StackTrace);
                                Console.WriteLine(ex.StackTrace);

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //logger.Error(ex.StackTrace);
                        Console.WriteLine(ex.StackTrace);
                    }


                    YoutubeChannel objChnnelData = ytrchannelrpo.getYoutubeChannelDetailsById(GooglePlusUserId);
                    GlobusGooglePlusLib.Youtube.Core.Activities objActivities = new Activities();
                    string objDetails = objActivities.Get_All_Activities(strfinaltoken, oAuthTokenYoutube.Parts.snippet, true, 50);

                    JObject obj = JObject.Parse(objDetails);

                    JArray array = (JArray)obj["items"];

                    int rowCount = 0;
                    int columnCount = 0;

                    strYoutubechanell = "";

                    string strYoutubechanell1 = string.Empty;

                    foreach (var item in array)
                    {
                        string title = string.Empty;
                        string description = string.Empty;
                        string thumbnail = string.Empty;
                        string videoid = string.Empty;

                        columnCount++;

                        #region << Title >>

                        try
                        {
                            title = item["snippet"]["title"].ToString();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);

                        }
                        #endregion

                        #region << Description >>

                        try
                        {
                            description = item["snippet"]["description"].ToString();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);

                        }
                        #endregion

                        #region  << Thumbnail >>

                        try
                        {
                            thumbnail = item["snippet"]["thumbnails"]["maxres"]["url"].ToString();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);

                        }
                        if (string.IsNullOrEmpty(thumbnail))
                        {

                            try
                            {
                                thumbnail = item["snippet"]["thumbnails"]["high"]["url"].ToString();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                            }
                        }
                        #endregion

                        try
                        {
                            videoid = item["snippet"]["resourceId"]["videoId"].ToString();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }


                        string viewCount = string.Empty;
                        string likeCount = string.Empty;
                        string dislikeCount = string.Empty;
                        string favoriteCount = string.Empty;
                        string commentCount = string.Empty;
                        if (!string.IsNullOrEmpty(videoid))
                        {

                            try
                            {
                                GlobusGooglePlusLib.Youtube.Core.Video ObjClsVideo = new Video();

                                string videoDetails = ObjClsVideo.Get_VideoDetails_byId(videoid, strfinaltoken, "snippet,statistics");

                                JObject JobjvideoDetails = JObject.Parse(videoDetails);

                                var JArrvideoDetails = (JArray)(JobjvideoDetails["items"]);

                                foreach (var DataVal in JArrvideoDetails)
                                {
                                    viewCount = DataVal["statistics"]["viewCount"].ToString();
                                    likeCount = DataVal["statistics"]["likeCount"].ToString();
                                    dislikeCount = DataVal["statistics"]["dislikeCount"].ToString();
                                    favoriteCount = DataVal["statistics"]["favoriteCount"].ToString();
                                    commentCount = DataVal["statistics"]["commentCount"].ToString();
                                    break;
                                }


                            }
                            catch (Exception)
                            {
                            }

                        }





                        strYoutubechanell1 += "<div class=\"span3\">" +
                                              "<div class=\"span box whitebg tumb_bg\">" +
                                             "<div class=\"yt_title\">" + title + "</div><div class=\"video-containers thumbnail\">" +
                                             "<img onclick=\"youtubevideo('" + videoid + "')\" alt=\"\" src=\"" + thumbnail + "\">" +
                                             "</div><div class=\"yt_description\">" + description + "</div></div></div>";



                        try
                        {
                            if (rowCount == 3)
                            {
                                strYoutubechanell += " <div class=\"row space\">" + strYoutubechanell1 + "</div>";
                                strYoutubechanell1 = string.Empty;
                                rowCount = 0;
                            }
                            else
                            {
                                rowCount++;
                            }
                            if (!strYoutubechanell.Contains(strYoutubechanell1) && array.Count == columnCount)
                            {
                                strYoutubechanell += " <div class=\"row space\">" + strYoutubechanell1 + "</div>";
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }

                    Response.Write(strYoutubechanell);

                    #endregion
                }
                //SUBSCRIBTIONS
                else if (Request.QueryString["op"] == "youtubesubscribe")
                {
                    #region youtube_SUBSCRIBE
                    
                    string strYoutubechanell = string.Empty;
                    string GooglePlusUserId = Request.QueryString["profileid"];
                    string accesstoken = Request.QueryString["accesstoken"];
                    oAuthTokenYoutube objoAuthTokenYoutube = new oAuthTokenYoutube();
                    string finaltoken = objoAuthTokenYoutube.GetAccessToken(accesstoken);
                    string strfinaltoken = string.Empty;

                    try
                    {

                        if (!finaltoken.StartsWith("["))
                            finaltoken = "[" + finaltoken + "]";
                        JArray objArray = JArray.Parse(finaltoken);


                        foreach (var item in objArray)
                        {
                            try
                            {

                                strfinaltoken = item["access_token"].ToString();

                                break;

                            }
                            catch (Exception ex)
                            {
                                //logger.Error(ex.StackTrace);
                                Console.WriteLine(ex.StackTrace);

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //logger.Error(ex.StackTrace);
                        Console.WriteLine(ex.StackTrace);
                    }


                    Subscriptions _Subscriptions = new Subscriptions();


                    string _strSubscriptions = _Subscriptions.Get_Subscriptions_List(strfinaltoken, oAuthTokenYoutube.Parts.snippet.ToString());

                    //YoutubeChannel objChnnelData = ytrchannelrpo.getYoutubeChannelDetailsById(GooglePlusUserId);
                    //GlobusGooglePlusLib.Youtube.Core.Activities objActivities = new Activities();
                    //string objDetails = objActivities.Get_All_Activities(strfinaltoken, oAuthTokenYoutube.Parts.snippet, true, 50);

                    JObject obj = JObject.Parse(_strSubscriptions);

                    JArray array = (JArray)obj["items"];

                    int rowCount = 0;
                    int columnCount = 0;

                    strYoutubechanell = "";

                    string strYoutubechanell1 = string.Empty;

                    foreach (var item in array)
                    {
                        string title = string.Empty;
                        string description = string.Empty;
                        string _resoucechannelId = string.Empty;
                        string thumbnail = string.Empty;

                        columnCount++;

                        #region << Title >>

                        try
                        {
                            title = item["snippet"]["title"].ToString();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);

                        }
                        #endregion

                        #region << Description >>

                        try
                        {
                            description = item["snippet"]["description"].ToString();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);

                        }
                        #endregion

                        #region  << Thumbnail >>

                        try
                        {
                            thumbnail = item["snippet"]["thumbnails"]["maxres"]["url"].ToString();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);

                        }
                        if (string.IsNullOrEmpty(thumbnail))
                        {

                            try
                            {
                                thumbnail = item["snippet"]["thumbnails"]["high"]["url"].ToString();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                            }
                        }
                        #endregion

                        try
                        {
                            _resoucechannelId = item["snippet"]["resourceId"]["channelId"].ToString();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }


                        string viewCount = string.Empty;
                        string subscriberCount = string.Empty;
                        string hiddenSubscriberCount = string.Empty;
                        string videoCount = string.Empty;
                        string commentCount = string.Empty;
                        if (!string.IsNullOrEmpty(_resoucechannelId))
                        {

                            try
                            {
                                GlobusGooglePlusLib.Youtube.Core.Channels _Channels = new Channels();

                                string videoDetails = _Channels.Get_Channel_List(strfinaltoken, (oAuthTokenYoutube.Parts.snippet.ToString() + "," + oAuthTokenYoutube.Parts.statistics.ToString()), _resoucechannelId);

                                JObject JobjvideoDetails = JObject.Parse(videoDetails);

                                var JArrvideoDetails = (JArray)(JobjvideoDetails["items"]);

                                foreach (var DataVal in JArrvideoDetails)
                                {
                                    viewCount = DataVal["statistics"]["viewCount"].ToString();
                                    subscriberCount = DataVal["statistics"]["subscriberCount"].ToString();
                                    hiddenSubscriberCount = DataVal["statistics"]["hiddenSubscriberCount"].ToString();
                                    videoCount = DataVal["statistics"]["videoCount"].ToString();
                                    commentCount = DataVal["statistics"]["commentCount"].ToString();
                                    break;
                                }


                            }
                            catch (Exception)
                            {
                            }

                        }


                        strYoutubechanell1 += "<div class=\"span3\">" +
                                              "<div class=\"span box whitebg tumb_bg\">" +
                                             "<div class=\"yt_title\">" + title + "</div><div class=\"video-containers thumbnail\">" +
                                             "<img onclick=\"#\" alt=\"\" src=\"" + thumbnail + "\">" +
                                             "</div><div class=\"icons\" style=\"width: 225px; float: left;\"><span class=\"span6 pull-left\">" +
                                             "<a href=\"#\" style=\"float: left;\"> &nbsp;<i style=\"color: green;\" class=\"icon-facetime-video\"></i></a>" +
                                             "<span class=\"pull-left\">&nbsp;" + videoCount + "</span></a><a href=\"#\" style=\"float: left;\"> &nbsp;<i style=\"color: red;\" class=\"icon-comment\"></i>" +
                                             "<span>&nbsp;" + commentCount + "</span></a></span><span class=\"pull-right\"><a href=\"#\"> &nbsp;<i style=\"color: red; padding-right: 5px;\" class=\"icon-eye-open\"></i><span>&nbsp;" + viewCount + "</span></a>" +
                                             "</span></div><div class=\"yt_description\">" + description + "</div></div></div>";



                        try
                        {
                            if (rowCount == 3)
                            {
                                strYoutubechanell += " <div class=\"row space\">" + strYoutubechanell1 + "</div>";
                                strYoutubechanell1 = string.Empty;
                                rowCount = 0;
                            }
                            else
                            {
                                rowCount++;
                            }
                            if (!strYoutubechanell.Contains(strYoutubechanell1) && array.Count == columnCount)
                            {
                                strYoutubechanell += " <div class=\"row space\">" + strYoutubechanell1 + "</div>";
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }

                    Response.Write(strYoutubechanell);

                    #endregion
                }
                else if (Request.QueryString["op"] == "linkedinfeeds")
                {
                    #region linkedinfeeds
                    string profileid = Request.QueryString["profileid"];
                    LinkedInAccountRepository linkedinAccRepo = new LinkedInAccountRepository();
                    LinkedInAccount linkacc = linkedinAccRepo.getUserInformation(profileid);
                    oAuthLinkedIn oauthlin = new oAuthLinkedIn();
                    oauthlin.ConsumerKey = ConfigurationManager.AppSettings["LiApiKey"];
                    oauthlin.ConsumerSecret = ConfigurationManager.AppSettings["LiSecretKey"];
                    oauthlin.FirstName = linkacc.LinkedinUserName;
                    oauthlin.Id = linkacc.LinkedinUserId;
                    oauthlin.Token = linkacc.OAuthToken;
                    oauthlin.TokenSecret = linkacc.OAuthSecret;
                    oauthlin.Verifier = linkacc.OAuthVerifier;


                    LinkedInUser l = new LinkedInUser();
                    List<LinkedInUser.User_Updates> lst = l.GetUserUpdates(oauthlin, linkacc.LinkedinUserId, 10);
                    string message = string.Empty;
                    if (lst.Count != 0)
                    {
                        foreach (LinkedInUser.User_Updates item in lst)
                        {
                            try
                            {
                                string picurl = string.Empty;
                                if (string.IsNullOrEmpty(item.PictureUrl))
                                {
                                    picurl = "../Contents/img/blank_img.png";
                                }
                                else
                                {
                                    picurl = item.PictureUrl;
                                }
                                message += "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"" + picurl + "\">" +
                                                                       "</div><div class=\"pull-left feedcontent\">" +
                                                                          "<a href=\"" + linkacc.ProfileUrl + "\" target=\"_blank\" class=\"feednm\">" + item.PersonFirstName + " " + item.PersonLastName + "</a> <span>" + item.DateTime +
                                                                              " </span>" +
                                                                           "<p>" + item.Message + "</p>" +
                                                                           "<a class=\"retweets\" href=\"#\">" +
                                                                           "</a><span></span>" +
                                                                       "</div>" +
                                                                   "</li>";
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                        }
                    }
                    else
                    {
                        message += "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"../Contents/img/blank_img.png\">" +
                                                                                             "</div><div class=\"pull-left feedcontent\">" +
                                                                                                "<a href=\"#\" class=\"feednm\"></a> <span>" +
                                                                                                    " </span>" +
                                                                                                 "<p>No Messages Found</p>" +
                                                                                                 "<a class=\"retweets\" href=\"#\">" +
                                                                                                  "</a><span></span>" +
                                                                                             "</div>" +
                                                                                         "</li>";
                    }
                    Response.Write(message);
                    #endregion
                }
                else if (Request.QueryString["op"] == "facebookapi")
                {
                    #region facebookapi
                    try
                    {
                        string profileid = Request.QueryString["profileid"];
                        // FacebookAccountRepository facerepo = new FacebookAccountRepository();
                        FacebookAccount faceaccount = facerepo.getFacebookAccountDetailsById(profileid);
                        FacebookHelper fbhelper = new FacebookHelper();
                        FacebookClient fbclient = new FacebookClient(faceaccount.AccessToken);
                        dynamic profile = fbclient.Get("me");
                        var feeds = fbclient.Get("/me/feed");
                        var home = fbclient.Get("me/home");
                        fbhelper.getFacebookUserFeeds(feeds, profile);
                        fbhelper.getFacebookUserHome(home, profile);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        Console.WriteLine(ex.Message);
                    }
                    #endregion
                }
                else if (Request.QueryString["op"] == "twitterapi")
                {
                    string profileid = Request.QueryString["profileid"];
                    TwitterAccountRepository twtAccountRepo = new TwitterAccountRepository();
                    TwitterAccount twtAccount = twtAccountRepo.getUserInformation(user.Id, profileid);
                    oAuthTwitter oAuth = new oAuthTwitter();
                    TwitterHelper twthelper = new TwitterHelper();
                    oAuth.AccessToken = twtAccount.OAuthToken;
                    oAuth.AccessTokenSecret = twtAccount.OAuthSecret;
                    twthelper.SetCofigDetailsForTwitter(oAuth);
                    oAuth.TwitterScreenName = twtAccount.TwitterScreenName;
                    oAuth.TwitterUserId = twtAccount.TwitterUserId;
                    twthelper.getUserTweets(oAuth, twtAccount, user.Id);
                    twthelper.getUserFeed(oAuth, twtAccount, user.Id);
                    twthelper.getSentDirectMessages(oAuth, twtAccount, user.Id);
                    twthelper.getReTweetsOfUser(oAuth, twtAccount, user.Id);

                }
                else if (Request.QueryString["op"] == "instagramlike")
                {
                    string mediaid = Request.QueryString["mediaid"];
                    bool b = this.likefunction(mediaid, Request.QueryString["userid"], Request.QueryString["access"]);
                }
                else if (Request.QueryString["op"] == "instagramunlike")
                {
                    string mediaid = Request.QueryString["mediaid"];
                    bool b = this.unlikefunction(mediaid, Request.QueryString["userid"], Request.QueryString["access"]);

                }
                else if (Request.QueryString["op"] == "instagramimages")
                {
                    if (Request.QueryString["loadtime"] != "first")
                    {
                        instagramcount = instagramcount + 10;
                    }
                    else
                    {
                        instagramcount = 0;
                    }

                    InstagramAccountRepository objInsAccRepo = new InstagramAccountRepository();
                    InstagramFeedRepository objInsFeedRepo = new InstagramFeedRepository();
                    InstagramFeed objInsFeed = new InstagramFeed();
                    InstagramCommentRepository objInsCmtRepo = new InstagramCommentRepository();
                    List<SocioBoard.Domain.InstagramComment> lstInsCmt = new List<SocioBoard.Domain.InstagramComment>();

                    string strInsImage = string.Empty;

                    try
                    {
                        string profileid = Request.QueryString["profileid"];
                        InstagramAccount insaccount = objInsAccRepo.getInstagramAccountDetailsById(profileid, user.Id);
                        List<InstagramFeed> lstInsFeed = objInsFeedRepo.getAllInstagramFeedsOfUser(user.Id, profileid, instagramcount);
                        if (lstInsFeed.Count != 0)
                        {
                            strInsImage += "<div class=\"feedcontainer\">";
                            foreach (InstagramFeed feed in lstInsFeed)
                            {

                                try
                                {
                                    lstInsCmt = objInsCmtRepo.getAllInstagramCommentsOfUser(user.Id, profileid, feed.FeedId);
                                }
                                catch (Exception ex)
                                {
                                    logger.Error(ex.Message);
                                    Console.WriteLine(ex.Message);
                                }
                                try
                                {
                                    strInsImage += "<div class=\"span3\" class=\"row-fluid\"><div class=\"span12 box whitebg feedwrap\"><div class=\"topicon\"><div class=\"pull-left\">" +

                                                                    "</div><div class=\"pull-right\" id=\"like\"><a title=\"\" href=\"#\" onClick=\"insUser('" + feed.FeedId + "','" + insaccount.AccessToken + "')\" ><img id=\"heartEmpty_" + feed.FeedId + "\" width=\"14\" alt=\"\" src=\"../Contents/img/admin/heart-empty.png\"  style=\"margin-top: 9px;\"></a><a title=\"\" href=\"#\"><img width=\"14\" alt=\"\" src=\"../Contents/img/admin/speech-bubble-left.png\"  style=\"margin-top: 9px;\"></a>" +
                                                                    "</div></div><div class=\"pic\"><img alt=\"\" src=\"" + feed.FeedImageUrl + "\"></div><div class=\"desc\"><p></p><span class=\"pull-left span3\">" +
                                                                    "<img width=\"12\" alt=\"\" src=\"../Contents/img/admin/heart-empty.png\"> " + feed.LikeCount + "</span><span class=\"pull-left span3\"><img width=\"12\" alt=\"\" src=\"../Contents/img/admin/speech-bubble-left.png\"> " + lstInsCmt.Count + "</span><div class=\"clearfix\"></div>";

                                    foreach (InstagramComment insCmt in lstInsCmt)
                                    {
                                        try
                                        {
                                            strInsImage += "<div class=\"userprof\"><div class=\"pull-left\"><a href=\"#\">" +
                                              "<img width=\"36\" alt=\"\" src=\"" + insCmt.FromProfilePic + "\"></a></div><div class=\"pull-left descr\"><p>" + insCmt.Comment + "</p>" +
                                               "<span class=\"usert\">" + DateExtension.ToDateTime(DateTime.Now, (long)Convert.ToDouble(insCmt.CommentDate)) + "</span></div></div>";

                                        }
                                        catch (Exception ex)
                                        {
                                            logger.Error(ex.Message);
                                            Console.WriteLine(ex.Message);
                                        }

                                    }

                                    strInsImage += "</div></div></div>";
                                }
                                catch (Exception ex)
                                {
                                    logger.Error(ex.Message);
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            strInsImage += "</div>";
                        }
                        else
                        {
                            if (instagramcount == 0)
                            {
                                strInsImage = "<div class=\"grid\"><div class=\"box whitebg feedwrap\">" +
                                             "<div class=\"topicon\"><div class=\"pull-left\"></div><div class=\"pull-right\">" +
                                 "<a href=\"#\" title=\"\"></a><a href=\"#\" title=\"\"></a></div></div><div class=\"pic\">" +
                                 "<img src=\"../Contents/img/no_image_found.png\" alt=\"\"></div><div class=\"desc\"><p></p></div></div></div>";
                            }
                        }


                        Response.Write(strInsImage);


                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }

                }
                else if (Request.QueryString["op"] == "instagramApi")
                {
                    try
                    {
                        InstagramManager insManager = new InstagramManager();
                        string profileid = Request.QueryString["profileid"];
                        InstagramAccountRepository insAccRepo = new InstagramAccountRepository();
                        InstagramAccount instagramAccount = insAccRepo.getInstagramAccountDetailsById(profileid, user.Id);
                        insManager.getIntagramImages(instagramAccount);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        Console.WriteLine(ex.Message);
                    }
                }



                else if (Request.QueryString["op"] == "UnfollowTumblrBlog")
                {
                    try
                    {
                        string blogname = Request.QueryString["blogname"].ToString();
                        string profileid = Request.QueryString["profileid"];
                        string accesstoken = Request.QueryString["accesstoken"];
                        string accesstokensecret = Request.QueryString["accesstokensecret"];
                        Guid id = Guid.Parse(Request.QueryString["id"]);
                        try
                        {
                            string msg = "success";
                            BlogsFollowers objunfollowblog = new BlogsFollowers();
                            objunfollowblog.Unfollowblog(accesstoken, accesstokensecret, blogname);
                            int result = objTumblrFeedRepository.DeleteTumblrDataByProfileid(profileid, blogname);
                            Response.Write(msg);

                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            Console.WriteLine(ex.Message);
                        }

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        Console.WriteLine(ex.Message);
                    }

                }


                else if (Request.QueryString["op"] == "tumblrTextPost")
                {

                    try
                    {
                        string ProfileId = Request.QueryString["profileid"].ToString();
                        string body = Request.QueryString["msg"].ToString();
                        string title = Request.QueryString["title"].ToString();
                        TumblrAccount objTumblrAccount = tumblraccrepo.getTumblrAccountDetailsById(ProfileId);
                        PublishedPosts objPublishedPosts = new PublishedPosts();
                        objPublishedPosts.PostData(objTumblrAccount.tblrAccessToken, objTumblrAccount.tblrAccessTokenSecret, ProfileId, body, title, "text");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                }


                else if (Request.QueryString["op"] == "tumblrQuotePost")
                {

                    try
                    {
                        string ProfileId = Request.QueryString["profileid"].ToString();
                        string source = Request.QueryString["source"].ToString();
                        string quote = Request.QueryString["quote"].ToString();
                        TumblrAccount objTumblrAccount = tumblraccrepo.getTumblrAccountDetailsById(ProfileId);
                        PublishedPosts objPublishedPosts = new PublishedPosts();
                        objPublishedPosts.PostData(objTumblrAccount.tblrAccessToken, objTumblrAccount.tblrAccessTokenSecret, ProfileId, source, quote, "quote");


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                }

                else if (Request.QueryString["op"] == "tumblrLinkPost")
                {

                    try
                    {
                        string ProfileId = Request.QueryString["profileid"].ToString();
                        string linkurl = Request.QueryString["linkurl"].ToString();
                        string title = Request.QueryString["title"].ToString();
                        string description = Request.QueryString["description"].ToString();
                        TumblrAccount objTumblrAccount = tumblraccrepo.getTumblrAccountDetailsById(ProfileId);
                        PublishedPosts objPublishedPosts = new PublishedPosts();
                        objPublishedPosts.PostdescriptionData(objTumblrAccount.tblrAccessToken, objTumblrAccount.tblrAccessTokenSecret, ProfileId, linkurl, title, description, "link");


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                }





                else if (Request.QueryString["op"] == "tumblrImagePost")
                {
                    string caption = string.Empty;

                    try
                    {
                        string ProfileId = Request.QueryString["profileid"].ToString();
                        try
                        {
                            caption = Request.QueryString["caption"].ToString();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                        var fi = Request.Files["file"];

                        string file = string.Empty;
                        if (fi != null)
                        {
                            var path = Server.MapPath("~/Contents/img/upload");
                            file = path + "/" + fi.FileName;
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            fi.SaveAs(file);
                        }

                        string filepath = file;

                        TumblrAccount objTumblrAccount = tumblraccrepo.getTumblrAccountDetailsById(ProfileId);
                        PublishedPosts objPublishedPosts = new PublishedPosts();
                        objPublishedPosts.PostData(objTumblrAccount.tblrAccessToken, objTumblrAccount.tblrAccessTokenSecret, ProfileId, caption, filepath, "photo");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                }



                else if (Request.QueryString["op"] == "tumblrAudioPost")
                {
                    string caption = string.Empty;

                    try
                    {
                        string ProfileId = Request.QueryString["profileid"].ToString();

                        var fi = Request.Files["file"];

                        string file = string.Empty;
                        if (fi != null)
                        {
                            var path = Server.MapPath("~/Contents/img/upload");
                            file = path + "/" + fi.FileName;
                            //if (!Directory.Exists(path))
                            //{
                            //    Directory.CreateDirectory(path);
                            //}
                            //fi.SaveAs(file);
                        }

                        string filepath = file;

                        TumblrAccount objTumblrAccount = tumblraccrepo.getTumblrAccountDetailsById(ProfileId);
                        PublishedPosts objPublishedPosts = new PublishedPosts();
                        objPublishedPosts.PostAudioData(objTumblrAccount.tblrAccessToken, objTumblrAccount.tblrAccessTokenSecret, ProfileId, filepath, "audio");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }


                }



                else if (Request.QueryString["op"] == "tumblrVideoPost")
                {
                    string caption = string.Empty;

                    try
                    {
                        string ProfileId = Request.QueryString["profileid"].ToString();
                        string VideoUrl = Request.QueryString["VideoUrl"].ToString();
                        string VideoContent = Request.QueryString["VideoContent"].ToString();

                        var fi = Request.Files["file"];

                        string file = string.Empty;
                        if (fi != null)
                        {
                            var path = Server.MapPath("~/Contents/img/upload");
                            file = path + "/" + fi.FileName;
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            fi.SaveAs(file);
                        }

                        string filepath = file;

                        TumblrAccount objTumblrAccount = tumblraccrepo.getTumblrAccountDetailsById(ProfileId);
                        PublishedPosts objPublishedPosts = new PublishedPosts();
                        objPublishedPosts.PostdescriptionData(objTumblrAccount.tblrAccessToken, objTumblrAccount.tblrAccessTokenSecret, ProfileId, filepath, VideoUrl, VideoContent, "video");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }


                }

                else if (Request.QueryString["op"] == "tumblrChatPost")
                {

                    try
                    {
                        string ProfileId = Request.QueryString["profileid"].ToString();
                        string body = Request.QueryString["body"].ToString();
                        string title = Request.QueryString["title"].ToString();
                        string tag = Request.QueryString["tag"].ToString();
                        TumblrAccount objTumblrAccount = tumblraccrepo.getTumblrAccountDetailsById(ProfileId);
                        PublishedPosts objPublishedPosts = new PublishedPosts();
                        objPublishedPosts.PostdescriptionData(objTumblrAccount.tblrAccessToken, objTumblrAccount.tblrAccessTokenSecret, ProfileId, body, title, tag, "chat");


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                }













                else if (Request.QueryString["op"] == "LikeUnlikeTumblrImage")
                {

                    int likestatus = Convert.ToInt16(Request.QueryString["likes"]);
                    string profileid = Request.QueryString["profileid"];
                    string accesstoken = Request.QueryString["accesstoken"];
                    string accesstokensecret = Request.QueryString["accesstokensecret"];
                    Guid id = Guid.Parse(Request.QueryString["id"]);
                    int notes = Convert.ToInt16(Request.QueryString["notes"]);


                    try
                    {
                        int like = 0;
                        if (likestatus == 0)
                        {
                            like = 1;
                        }
                        int i = objTumblrFeedRepository.UpdateDashboardOfProfileLikes(profileid, id, like);

                        int s = objTumblrFeedRepository.UpdateDashboardOfProfileNotes(profileid, id, like, notes);

                        TumblrFeed obj = objTumblrFeedRepository.getFeedOfProfilebyIdProfileId(profileid, id);
                        BlogsLikes objBlogsLikes = new BlogsLikes();

                        objBlogsLikes.likeBlog(accesstoken, accesstokensecret, obj.blogId, obj.reblogkey, like);


                        //KeyValuePair<string, string> LoginDetails = new KeyValuePair<string, string>(accesstoken, accesstokensecret);
                        //var prms = new Dictionary<string, object>();
                        //prms.Add("id", obj.blogId);
                        //prms.Add("reblog_key", obj.reblogkey);
                        //var postUrl = "";

                        //if (like == 1)
                        //{
                        //    postUrl = "https://api.tumblr.com/v2/user/like/";
                        //}
                        //else
                        //{
                        //    postUrl = "https://api.tumblr.com/v2/user/unlike/";
                        //}
                        //string result = oAuthTumbler.OAuthData(postUrl, "POST", LoginDetails.Key, LoginDetails.Value, prms);

                        //string result1 = string.Empty;
                        //result1 = result;
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public string getlike(int a, string profileid)
        {
            string ret = "";
            if (a == 0)
            {
                ret = "<img id=\"heartEmpty_" + profileid + "\" width=\"14\" alt=\"\" src=\"../Contents/img/admin/heart-empty.jpg\" title=\"Like\"  style=\"margin-top: 9px;\">";
            }
            else
            {
                ret = "<img id=\"heartEmpty_" + profileid + "\" width=\"14\" alt=\"\" src=\"../Contents/img/admin/dil.png\"  title=\"UnLike\" style=\"margin-top: 9px;\">";
            }
            return ret;
        }

        public bool likefunction(string id, string userid, string accesstoken)
        {
            oAuthInstagram _api;
            GlobusInstagramLib.Authentication.ConfigurationIns configi = new GlobusInstagramLib.Authentication.ConfigurationIns("https://api.instagram.com/oauth/authorize/", ConfigurationManager.AppSettings["InstagramClientKey"].ToString(), ConfigurationManager.AppSettings["InstagramClientSec"].ToString(), ConfigurationManager.AppSettings["InstagramCallBackURL"].ToString(), "http://api.instagram.com/oauth/access_token", "https://api.instagram.com/v1/", "");
            _api = oAuthInstagram.GetInstance(configi);
            LikesController objlikes = new LikesController();
            bool dd = objlikes.PostUserLike(id, userid, accesstoken);
            return dd;
        }
        public bool unlikefunction(string id, string userid, string accesstoken)
        {
            LikesController objlikes = new LikesController();
            bool dd = objlikes.DeleteLike(id, userid, accesstoken);
            return dd;

        }
        public string BindData(DataTable dt)
        {
            string message = string.Empty;
            DataView dv = dt.DefaultView;
            dv.Sort = "MessageDate desc";
            DataTable sortedDT = dv.ToTable();
            int sorteddatacount = 0;
            LikesController objLikes = new LikesController();
            InstagramCommentRepository objCommentRepo = new InstagramCommentRepository();
            SocioBoard.Domain.User user = (SocioBoard.Domain.User)Session["LoggedUser"];
            foreach (DataRow row in sortedDT.Rows)
            {


                if (row["Network"].ToString() == "twitter")
                {
                    message += "<div id=\"messagetaskable_" + sorteddatacount + "\" class=\"messages taskable\"><section><aside><section class=\"js-avatar_tip\" data-sstip_class=\"twt_avatar_tip\">" +
                                "<a class=\"avatar_link view_profile\" href=\"javascript:void(0)\"><img id=\"formprofileurl_" + sorteddatacount + "\" onclick=\"detailsdiscoverytwitter(this.alt);\" width=\"54\" height=\"54\" border=\"0\" class=\"avatar\" src=\"" + row["FromProfileUrl"] + "\" alt=\"" + row["FromName"] + "\">" +
                                 "<article class=\"message-type-icon\"><span class=\"twitter_bm\"><img src=\"../Contents/Images/twticon.png\" width=\"16\" height=\"16\" /></span></article></a></section><ul></ul></aside><article><div class=\"\">" +
                                 "<a class=\"language\" href=\"\"></a></div><div class=\"message_actions\"><a class=\"gear_small\" href=\"#\"><span title=\"Options\" class=\"ficon\">?</span>" +
                                 "</a></div>";

                    message += "<div id=\"messagedescription_" + sorteddatacount + "\" class=\"message-text font-14\">" + row["Message"] + "</div>" +
                                    "<section class=\"bubble-meta\">" +
                                       "<article class=\"threefourth text-overflow\">" +
                                           "<section class=\"floatleft\">" +
                                               "<a data-tip=\"View Yaroslav Lukashev's Profile\"  class=\"js-avatar_tip view_profile profile_link\" data-sstip_class=\"twt_avatar_tip\"><span id=\"network_" + sorteddatacount + "\" style=\"display:none;\">twitter</span><span style=\"display:none;\" id=\"messageid_" + sorteddatacount + "\">" + row["MessageId"] + "</span><span style=\"display:none;\" id=\"fromid_" + sorteddatacount + "\">" + row["FromId"] + "</span><span style=\"display:none;\" id=\"rowid_" + sorteddatacount + "\">" + row["ProfileId"] + "</span><span id=\"rowname_" + sorteddatacount + "\" onclick=\"detailsdiscoverytwitter(this.id);\">" + row["FromName"] + "</span>" +
                                               "</a>&nbsp;<a data-msg-time=\"1363926699000\" class=\"time\" target=\"_blank\" title=\"View message on Twitter\" href=\"#\">" + row["MessageDate"].ToString() + "</a>, <span class=\"location\" >&nbsp;</span>" +
                                    "</section></article><ul class=\"message-buttons quarter clearfix\"><li><a href=\"#\"><img src=\"../Contents/Images/replay.png\" alt=\"\" width=\"17\" height=\"24\" border=\"none\"  onclick=replyfunction(" + sorteddatacount + ") ></a></li>" +
                                    "<li><a id=\"createtasktwt_" + sorteddatacount + "\" href=\"#\" onclick=\"createtask(this.id);\"><img src=\"../Contents/Images/pin.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a></li>" +

                                       "</ul></section></article></section> </div>";
                }
                else if (row["Network"].ToString() == "linkedin")
                {

                    message += "<div id=\"messagetaskable_" + sorteddatacount + "\" class=\"messages taskable\"><section><aside><section class=\"js-avatar_tip\" data-sstip_class=\"twt_avatar_tip\">" +
                                        "<a class=\"avatar_link view_profile\" href=\"javascript:void(0)\"><img id=\"formprofileurl_" + sorteddatacount + "\" width=\"54\" height=\"54\" border=\"0\" class=\"avatar\" src=\"" + row["FromProfileUrl"] + "\" alt=\"\" onclick=\"getFacebookProfiles(this.alt);\" >" +
                                         "<article class=\"message-type-icon\"><span class=\"facebook_bm\"><img src=\"../Contents/Images/linked_25X24.png\"  width=\"16\" height=\"16\"/></span></article></a></section><ul></ul></aside><article><div class=\"\">" +
                                         "<a class=\"language\" href=\"\"></a></div><div class=\"message_actions\"><a class=\"gear_small\" href=\"#\"><span title=\"Options\" class=\"ficon\">?</span>" +
                                         "</a></div><div id=\"messagedescription_" + sorteddatacount + "\" class=\"message-text font-14\">" + row["Message"] + "</div>" +
                                          "<section class=\"bubble-meta\">" +
                                             "<article class=\"threefourth text-overflow\">" +
                                                 "<section class=\"floatleft\">" +
                                                     "<a data-tip=\"View Yaroslav Lukashev's Profile\"  class=\"js-avatar_tip view_profile profile_link\"  data-sstip_class=\"twt_avatar_tip\"   ><span id=\"network_" + sorteddatacount + "\" style=\"display:none;\">facebook</span><span style=\"display:none;\" id=\"rowid_" + sorteddatacount + "\">" + row["ProfileId"] + "</span><span style=\"display:none;\" id=\"rowname_" + sorteddatacount + "\">" + row["FromName"] + "</span><span style=\"display:none;\" id=\"messageid_" + sorteddatacount + "\">" + row["MessageId"] + "</span><span style=\"display:none;\" id=\"fromid_" + sorteddatacount + "\">" + row["FromId"] + "</span><span id=\"" + row["FromId"] + "\"onclick=\"getFacebookProfiles(this.id);\" >" + row["FromName"] + "</span>" +
                                                     "</a>&nbsp;<a id=\"createdtime_" + sorteddatacount + "\" data-msg-time=\"1363926699000\" class=\"time\" target=\"_blank\" title=\"View message on Twitter\" href=\"#\">" + row["MessageDate"].ToString() + "</a><span class=\"location\">&nbsp;</span>" +
                                          "</section></article><ul class=\"message-buttons quarter clearfix\"><li><a href=\"#\"><img src=\"../Contents/Images/replay.png\" alt=\"\" width=\"17\" height=\"24\" border=\"none\" onclick=replyfunction(" + sorteddatacount + ") ></a></li>" +
                                          "<li><a  id=\"createtasktwt_" + sorteddatacount + "\" href=\"#\" onclick=\"createtask(this.id);\"><img src=\"../Contents/Images/pin.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a></li>" +
                                          "<li><a id=\"savearchive_" + sorteddatacount + "\" href=\"#\" onclick=\"savearchivemsg(" + sorteddatacount + ");\"><img src=\"../Contents/Images/archive.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a></li>" +
                                            "</ul></section></article></section> </div>";

                }
                else if (row["Network"].ToString() == "facebook")
                {
                    message += "<div id=\"messagetaskable_" + sorteddatacount + "\" class=\"messages taskable\"><section><aside><section class=\"js-avatar_tip\" data-sstip_class=\"twt_avatar_tip\">" +
                                       "<a class=\"avatar_link view_profile\" href=\"javascript:void(0)\"><img id=\"formprofileurl_" + sorteddatacount + "\" width=\"54\" height=\"54\" border=\"0\" class=\"avatar\" src=\"" + row["FromProfileUrl"] + "\" alt=\"" + row["FromId"] + "\" onclick=\"getFacebookProfiles(this.alt);\" >" +
                                        "<article class=\"message-type-icon\"><span class=\"facebook_bm\"><img src=\"../Contents/Images/fb_icon.png\"  width=\"16\" height=\"16\"/></span></article></a></section><ul></ul></aside><article><div class=\"\">" +
                                        "<a class=\"language\" href=\"\"></a></div><div class=\"message_actions\"><a class=\"gear_small\" href=\"#\"><span title=\"Options\" class=\"ficon\">?</span>" +
                                        "</a></div><div id=\"messagedescription_" + sorteddatacount + "\" class=\"message-text font-14\">" + row["Message"] + "</div>" +
                                         "<section class=\"bubble-meta\">" +
                                            "<article class=\"threefourth text-overflow\">" +
                                                "<section class=\"floatleft\">" +
                                                    "<a data-tip=\"View Yaroslav Lukashev's Profile\"  class=\"js-avatar_tip view_profile profile_link\"  data-sstip_class=\"twt_avatar_tip\"   ><span id=\"network_" + sorteddatacount + "\" style=\"display:none;\">facebook</span><span style=\"display:none;\" id=\"rowid_" + sorteddatacount + "\">" + row["ProfileId"] + "</span><span style=\"display:none;\" id=\"rowname_" + sorteddatacount + "\">" + row["FromName"] + "</span><span style=\"display:none;\" id=\"messageid_" + sorteddatacount + "\">" + row["MessageId"] + "</span><span style=\"display:none;\" id=\"fromid_" + sorteddatacount + "\">" + row["FromId"] + "</span><span id=\"" + row["FromId"] + "\"onclick=\"getFacebookProfiles(this.id);\" >" + row["FromName"] + "</span>" +
                                                    "</a>&nbsp;<a id=\"createdtime_" + sorteddatacount + "\" data-msg-time=\"1363926699000\" class=\"time\" target=\"_blank\" title=\"View message on Twitter\" href=\"#\">" + row["MessageDate"].ToString() + "</a><span class=\"location\">&nbsp;</span>" +
                                         "</section></article><ul class=\"message-buttons quarter clearfix\"><li><a href=\"#\"><img src=\"../Contents/Images/replay.png\" alt=\"\" width=\"17\" height=\"24\" border=\"none\" onclick=replyfunction(" + sorteddatacount + ") ></a></li>" +
                                         "<li><a  id=\"createtasktwt_" + sorteddatacount + "\" href=\"#\" onclick=\"createtask(this.id);\"><img src=\"../Contents/Images/pin.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a></li>" +
                                         "<li><a id=\"savearchive_" + sorteddatacount + "\" href=\"#\" onclick=\"savearchivemsg(" + sorteddatacount + ");\"><img src=\"../Contents/Images/archive.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a></li>" +
                                           "</ul></section></article></section> </div>";
                }
                else if (row["Network"].ToString() == "instagram")
                {
                    message += "<div id =\"div_" + sorteddatacount + "\" class=\"instagram_img_textbg\">" +

                     "<div class=\"inst_minage_bg\"><img id=img_" + sorteddatacount + " src=\"" + row["Message"] + "\" alt=\"" + row["MessageId"] + "\" border=\"none\" width=\"240\" height=\"240\" /></div>" +
                             " <div class=\"inst_comment_bg\">" +

                               "<div class=\"inputbg\"><input id= textcomment_" + sorteddatacount + " type=\"text\" value=\"\" /></div>" +

                               "<div class=\"instg_liec_bg\">";
                    bool liked = false;
                    try
                    {
                        //liked = objLikes.LikeToggle(row["FeedId"], row["InstagramId"], item.AccessToken);
                    }
                    catch
                    {
                    }
                    if (!liked)
                    {
                        message += "<div class=\"like_btnbg\"><a id = \"" + row["MessageId"] + "\" class=\"instagram unliked_liked\" href=\"javascript:void(0);\"  onclick =\"showinsprof(this.id)\"></a></div>" +
                                    " <div class=\"comment_btnbg\"><a id=\"comment_" + sorteddatacount + "\" href=\"javascript:void(0);\"  onclick=\"showinsprof(this.id);\"></a></div>" +

                                   "</div></div>";

                    }
                    else
                    {
                        message += "<div class=\"like_btnbg\"><a id = \"" + row["MessageId"] + "\" class=\"instagram liked\" href=\"javascript:void(0);\"  onclick =\"showinsprof(this.id)\"></a></div>" +
                                    " <div class=\"comment_btnbg\"><a id=\"comment_" + sorteddatacount + "\" href=\"javascript:void(0);\"  onclick=\"showinsprof(this.id);\"></a></div>" +

                                   "</div></div>";
                    }
                    List<InstagramComment> lstcomment = objCommentRepo.getAllInstagramCommentsOfUser(user.Id, row["ProfileId"].ToString(), row["MessageId"].ToString());
                    if (lstcomment != null)
                    {
                        try
                        {
                            foreach (InstagramComment insFeed in lstcomment)
                            {
                                message += "<div class=\"instagram_comment_div\">" +
                                 "<div class=\"user_photo\"><img src=\"" + insFeed.FromProfilePic + "\" width=\"30\" height=\"30\" alt=\"\" /></div>" +
                                 "<div class=\"comment_details\">" +
                                     "<div class=\"user_name\">" + insFeed.FromName + "</div>" +
                                     " <div class=\"user_name_description\">" + insFeed.Comment + "</div>" +
                                 "</div>" +
                              "</div>";
                            }
                        }
                        catch (Exception err)
                        { }
                    }
                    //  html += "<div class=\"instagram_comment_div\">" +
                    //   "<div class=\"user_photo\"><img src=\"" + usercmts.data[n - 1].from.profile_picture + "\" width=\"30\" height=\"30\" alt=\"\" /></div>" +
                    //   "<div class=\"comment_details\">" +
                    //       "<div class=\"user_name\">" + usercmts.data[n - 1].from.username + "</div>" +
                    //       " <div class=\"user_name_description\">" + usercmts.data[n - 1].text + "</div>" +
                    //   "</div>" +
                    //"</div>" +


                    //               "<div class=\"instagram_comment_div\">" +
                    //   "<div class=\"user_photo\"><img src=\"" + usercmts.data[n - 2].from.profile_picture + "\" width=\"30\" height=\"30\" alt=\"\" /></div>" +
                    //   "<div class=\"comment_details\">" +
                    //       "<div class=\"user_name\">" + usercmts.data[n - 2].from.username + "</div>" +
                    //       " <div class=\"user_name_description\">" + usercmts.data[n - 2].text + "</div>" +
                    //   "</div>" +
                    //"</div>" +


                    //  "<div class=\"instagram_comment_div\">" +
                    //   "<div class=\"user_photo\"><img src=\"" + usercmts.data[n - 3].from.profile_picture + "\" width=\"30\" height=\"30\" alt=\"\" /></div>" +
                    //   "<div class=\"comment_details\">" +
                    //       "<div class=\"user_name\">" + usercmts.data[n - 3].from.username + "</div>" +
                    //       " <div class=\"user_name_description\">" + usercmts.data[n - 3].text + "</div>" +
                    //   "</div>" +
                    //"</div>" +
                    message += "</div>";
                }
                sorteddatacount++;
            }

            return message;


        }



    }
}