using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Model;
using System.Collections;
using SocioBoard.Domain;
using Facebook;
using GlobusTwitterLib.Authentication;
using GlobusTwitterLib.App.Core;
using Newtonsoft.Json.Linq;
using SocioBoard.Helper;
using GlobusLinkedinLib.Authentication;
using GlobusLinkedinLib.LinkedIn.Core.SocialStreamMethods;
using GlobusInstagramLib.App.Core;
using System.Configuration;

namespace SocioBoard
{
    public partial class AjaxHome : System.Web.UI.Page
    {

        public static int profilelimit = 0;
        protected void Page_Load(object sender, EventArgs e)
        {



            try
            {
                ProcessRequest();
            }
            catch
            { }
        }



        public void ProcessRequest()
        {
            SocialProfilesRepository socio = new SocialProfilesRepository();
            List<SocialProfile> alstsocioprofiles = new List<SocialProfile>();
            if (!string.IsNullOrEmpty(Request.QueryString["op"]))
            {
                Domain.User user = (Domain.User)Session["LoggedUser"];

                if (Request.QueryString["op"] == "social_connectivity")
                {

                    #region social connectivity
                    alstsocioprofiles = socio.getAllSocialProfilesOfUser(user.Id);

                    string profiles = string.Empty;

                    foreach (SocialProfile item in alstsocioprofiles)
                    {
                        if (item.ProfileType == "facebook")
                        {
                            try
                            {
                                FacebookAccountRepository facereop = new FacebookAccountRepository();
                                FacebookAccount faceaccount = facereop.getFacebookAccountDetailsById(item.ProfileId, user.Id);
                                profiles += "<div id=\"" + item.ProfileId + "\" class=\"ws_conct\"><span class=\"img\">" +
                                   "<div id='fb_del' onClick=\"confirmDel('" + item.ProfileId + "','fb')\"><span class=\"delete\"></span></div>" +
                                    "<a href=\"" + faceaccount.ProfileUrl + "\" target=\"_blank\" ><img src=\"http://graph.facebook.com/" + item.ProfileId + "/picture?type=small\" width=\"48\" height=\"48\" alt=\"\" /></a>" +
                                          "<i>" +
                                              "<img src=\"../Contents/Images/fb_icon.png\" width=\"16\" height=\"16\" alt=\"\" /></i>" +
                                      "</span>" +
                                  "</div>";
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                            }
                        }
                        else if (item.ProfileType == "twitter")
                        {
                            try
                            {
                                TwitterAccountRepository twtrepo = new TwitterAccountRepository();
                                TwitterAccount twtaccount = twtrepo.getUserInformation(user.Id, item.ProfileId);

                                profiles += "<div id=\"" + item.ProfileId + "\" class=\"ws_conct\">" +
                                                          "<span class=\"img\"><div id='twt_del' onClick=\"confirmDel('" + item.ProfileId + "','twt')\"><span class=\"delete\"></span></div>" +
                                                              "<img width=\"48\" height=\"48\" alt=\"\" src=\"" + twtaccount.ProfileImageUrl + "\">" +
                                                              "<i><img src=\"../Contents/Images/twticon.png\" width=\"16\" height=\"16\" alt=\"\" /></i>" +
                                                          "</span>" +
                                                     "</div>";
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                            }

                        }
                        else if (item.ProfileType == "linkedin")
                        {

                            LinkedInAccountRepository liRepo = new LinkedInAccountRepository();
                            string access = string.Empty, tokenSecrate = string.Empty, LdprofileName = string.Empty, LdPreofilePic = string.Empty;
                            LinkedInAccount liaccount = liRepo.getUserInformation(user.Id, item.ProfileId);

                            if (liaccount != null)
                            {
                                try
                                {
                                    if (!string.IsNullOrEmpty(liaccount.ProfileImageUrl))
                                    {
                                        LdPreofilePic = liaccount.ProfileImageUrl;
                                    }
                                    else
                                    {
                                        LdPreofilePic = "../../Contents/Images/blank_img.png";
                                    }

                                    profiles += "<div id=\"" + item.ProfileId + "\" class=\"ws_conct\">" +
                                              "<span class=\"img\">" +
                                                   "<div id='fb_del' onClick=\"confirmDel('" + item.ProfileId + "','linkedin')\"><span class=\"delete\"></span></div>" +
                                                  "<a href=\"" + liaccount.ProfileUrl + "\" target=\"_blank\"><img width=\"48\" height=\"48\"  src=\"" + LdPreofilePic + "\"  alt=\"\"></a>" +
                                                  "<i><img src=\"../Contents/Images/link_icon.png\" width=\"16\" height=\"16\" alt=\"\" /></i>" +
                                              "</span>" +
                                         "</div>";
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.StackTrace);
                                }

                            }
                        }
                        else if (item.ProfileType == "instagram")
                        {
                            try
                            {
                                InstagramAccountRepository objInsAccRepo = new InstagramAccountRepository();
                                InstagramAccount objInsAcc = objInsAccRepo.getInstagramAccountDetailsById(item.ProfileId, user.Id);
                                string accessToken = string.Empty;

                                profiles += "<div id=\"" + item.ProfileId + "\" class=\"ws_conct\">" +
                                  "<span class=\"img\">" +
                                       "<div id='fb_del' onClick=\"confirmDel('" + item.ProfileId + "','instagram')\"><span class=\"delete\"></span></div>" +
                                      "<img width=\"48\" height=\"48\" alt=\"\" src=\"" + objInsAcc.ProfileUrl + "\">" +
                                      "<i><img src=\"../Contents/Images/instagram_24X24.png\" width=\"16\" height=\"16\" alt=\"\" /></i>" +
                                  "</span>" +
                             "</div>";
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                            }
                        }
                    }
                    Response.Write(profiles); 
                    #endregion
                }

                else if (Request.QueryString["op"] == "midsnaps")
                {
                    #region midsnaps
                    Random rNum = new Random();
                    string loadtype = Request.QueryString["loadtype"];
                    string midsnaps = string.Empty;
                    if (loadtype == "load")
                        profilelimit = 0;

                    if (profilelimit != -1)
                    {
                        ArrayList alst = socio.getLimitProfilesOfUser(user.Id, profilelimit);

                        if (alst.Count == 0)
                            profilelimit = -1;
                        else
                            profilelimit += 3;



                        foreach (SocialProfile item in alst)
                        {
                            if (item.ProfileType == "facebook")
                            {
                                FacebookAccountRepository fbrepo = new FacebookAccountRepository();
                                FacebookMessageRepository fbmsgrepo = new FacebookMessageRepository();
                                FacebookAccount fbaccount = fbrepo.getFacebookAccountDetailsById(item.ProfileId, user.Id);
                                List<FacebookMessage> fbmsgs = fbmsgrepo.getAllFacebookMessagesOfUser(user.Id, item.ProfileId);


                                midsnaps += "<div id=\"midsnap_" + item.ProfileId + "\" class=\"col_two_fb\"> " +
                                                    "<div class=\"col_two_fb_my_accounts\">" +
                                                     "<div class=\"dt\"><a class=\"img\"><img src=\"http://graph.facebook.com/" + item.ProfileId + "/picture?type=small\" width=\"48\" height=\"48\" alt=\"\" /></a>" +
                                                    "<span class=\"icon\"></span></div><div class=\"dd\"><h5>" + fbaccount.FbUserName + "</h5><div class=\"friends_avg\"><div class=\"article_friends\">" +
                                                    "<div class=\"facebook_blue\">" + fbaccount.Friends +
                                                    "</div>" +
                                                   "<div class=\"font-10\">Friends</div></div>" +
                                                       "<div class=\"article_avg\"><div class=\"facebook_blue\">" + Math.Round(rNum.NextDouble(), 2) + "</div><div class=\"font-10\">Avg. Posts per Day</div>" +
                                                     "</div>    </div></div> </div>" +
                                                      "<div class=\"pillow_fade\">" +
                                                        "<div class=\"fb_notifications\">" +
                                                                     "Recent Messages</div>" +
                                                               "<div class=\"empty-state\">";

                                if (fbmsgs.Count != 0)
                                {
                                    try
                                    {
                                        int msgcount = 0;
                                        foreach (FacebookMessage child in fbmsgs)
                                        {
                                            string mess = string.Empty;
                                            if (msgcount < 2)
                                            {
                                                if (child.Message.Length > 40)
                                                {
                                                    mess = child.Message.Substring(0, 39);
                                                    mess = mess + "...........";
                                                }
                                                else
                                                {
                                                    mess = child.Message;
                                                }


                                                midsnaps += "<strong><img src=\"http://graph.facebook.com/" + child.FromId + "/picture?type=small\" />" + mess + "</strong><br/>";
                                            }
                                            else
                                            {
                                                break;
                                            }
                                            msgcount++;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.StackTrace);
                                    }
                                }
                                else
                                {
                                    midsnaps += "<strong>No messages were found within the past few days.</strong> \"Messages will be displayed once there is activity in this date range.\"";
                                }


                                midsnaps += "</div></div></div>";
                            }
                            else if (item.ProfileType == "twitter")
                            {
                                TwitterAccountRepository twtrepo = new TwitterAccountRepository();
                                TwitterAccount twtaccount = twtrepo.getUserInformation(user.Id, item.ProfileId);
                                TwitterMessageRepository twtmsgrepo = new TwitterMessageRepository();
                                List<TwitterMessage> lsttwtmsgs = twtmsgrepo.getAllTwitterMessagesOfUser(user.Id, item.ProfileId);
                                int tweetcount = 0;
                                midsnaps += "<div id=\"midsnap_" + item.ProfileId + "\" class=\"col_four_twitter\"><div class=\"col_four_twitter_my_accounts\">" +
                                                          "<div class=\"dt\"><a class=\"img\"><img src=\"" + twtaccount.ProfileImageUrl + "\" width=\"48\" height=\"48\" alt=\"\" /></a>" +
                                                          "<span class=\"icon\"></span></div><div class=\"dd\"><h5>" + twtaccount.TwitterScreenName + "</h5>" +
                                                            "<div class=\"friends_avg\"><div class=\"article_friends\"><div class=\"facebook_blue\">" + twtaccount.FollowersCount + "</div>" +
                                                           "<div class=\"font-10\">Followers</div></div><div class=\"article_avg\"><div class=\"facebook_blue\">" +
                                                              " " + Math.Round(rNum.NextDouble(), 2) + "</div><div class=\"font-10\">Avg. Posts per Day</div></div></div></div></div><div class=\"pillow_fade\">" +
                                                            "<div class=\"fb_notifications\"> Recent Messages</div><div class=\"empty-state\">";


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
                                                    midsnaps += "<strong><img src=\"" + msg.FromProfileUrl + "\" />" + msg.TwitterMsg + "</strong><br/>";
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex.StackTrace);

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

                                    Console.WriteLine(ex.StackTrace);
                                }
                                midsnaps += "</div></div> </div>";
                            }
                            else if (item.ProfileType == "linkedin")
                            {
                                string access = string.Empty, tokenSecrate = string.Empty, LdprofileName = string.Empty, LdPreofilePic = string.Empty;
                                LinkedInAccountRepository objLiRepo = new LinkedInAccountRepository();
                                LinkedInFeedRepository objliFeedRepo = new LinkedInFeedRepository();
                                LinkedInAccount liAccount = objLiRepo.getUserInformation(user.Id, item.ProfileId);
                                // IEnumerable<dynamic> data = linkdrepo.GetAccessToken(item.profile_id, loginInfoEmail.Customer_Id);
                                //foreach (var child in data)
                                //{
                                if (liAccount != null)
                                {
                                    LdprofileName = liAccount.LinkedinUserName;
                                    LdPreofilePic = liAccount.ProfileImageUrl;
                                }
                                // }

                                if (string.IsNullOrEmpty(LdPreofilePic))
                                {
                                    LdPreofilePic = "../../Contents/Images/blank_img.png";
                                }
                                int linkedinConcount = liAccount.Connections;

                                midsnaps += " <div id=\"midsnap_" + item.ProfileId + "\" class=\"col_three_linkedin\">" +
                                           "<div class=\"col_three_link_my_accounts\">" +
                                             "<div class=\"dt\"><a class=\"img\">" +
                                               "<img src=\"" + LdPreofilePic + "\" width=\"48\" height=\"48\" alt=\"\" /></a>" +
                                             "<span class=\"icon\"></span></div><div class=\"dd\">" +
                                           "<h5>" + LdprofileName + "</h5><div class=\"friends_avg\">" +
                                       "<div class=\"article_friends\">" +
                                        "   <div class=\"facebook_blue\">" +
                                         "   " + linkedinConcount + "</div>" +
                                          " <div class=\"font-10\">" +
                                           "    Friends</div>" +
                                       "</div>" +
                                       "<div class=\"article_avg\">" +
                                         " <div class=\"facebook_blue\">" +
                                          " " + Math.Round(rNum.NextDouble(), 2) + "</div>" +
                                           "<div class=\"font-10\">" +
                                               "Avg. Posts per Day</div>" +
                                       "</div>" +
                                   "</div>" +
                               "</div>" +
                           "</div>" +
                               "<div class=\"pillow_fade\">" +

                                    "<div class=\"fb_notifications\">Recent Messages</div>" +
                                     "<div class=\"empty-state\">";

                                IEnumerable<dynamic> linkfed = objliFeedRepo.getAllLinkedInFeedsOfUser(user.Id, item.ProfileId);// = facerepo.GetMessages(item.profile_id, loginInfoEmail.Customer_Id);//linkdrepo.GetAllLinkedinFeeds(loginInfoEmail.Customer_Id, item.ToString());
                                int link = 0;


                                if (linkfed.Count() == 0)
                                {
                                    midsnaps += "<strong>No messages were found within the past 14 days.</strong> \"Messages will be displayed once there is activity in this date range.\"";
                                }
                                else
                                {

                                    try
                                    {

                                        foreach (var l in linkfed)
                                        {
                                            try
                                            {
                                                if (link < 2)
                                                {
                                                    string ms = string.Empty;
                                                    if (l.Feeds.Length > 20)
                                                    {
                                                        ms = l.Feeds.Substring(0, 20) + "..."; ;

                                                    }
                                                    else
                                                    {
                                                        ms = l.Feeds;
                                                    }
                                                    midsnaps += "<strong><img src=\"" + l.FromPicUrl + "\">" + ms + " </strong><br/>";
                                                    link++;
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                            catch
                                            {
                                            }
                                        }
                                    }
                                    catch { }

                                }

                                midsnaps += "</div></div> </div>";


                            }
                            else if (item.ProfileType == "instagram")
                            {
                                InstagramAccountRepository objInsAccRepo = new InstagramAccountRepository();
                                InstagramAccount objInsAcc = objInsAccRepo.getInstagramAccountDetailsById(item.ProfileId, user.Id);

                                midsnaps += " <div class=\"col_seven_instagram\">" +
                                         "<div class=\"col_seven_instagram_link_my_accounts\">" +
                                           "<div class=\"dt\"><a class=\"img\">" +
                                             "<img src=\"" + objInsAcc.ProfileUrl + "\" width=\"48\" height=\"48\" alt=\"\" /></a>" +
                                           "<span class=\"icon\"></span></div><div class=\"dd\">" +
                                         "<h5>" + objInsAcc.InsUserName + "</h5><div class=\"friends_avg\">" +

                                 "</div>" +
                             "</div>" +
                         "</div>" +
                             "<div class=\"pillow_fade\">" +
                                 "<div class=\"fb_notifications\">" +
                                     "<ul class=\"user-stats\">" +
                                                "<li>" +
                                                   "<div class=\"photo_stat\">  photos" +
                                                   "</div>" +
                                                   "<div class=\"number-stat\">" + objInsAcc.TotalImages +
                                                   "</div>" +
                                                "</li>" +
                                                "<li>" +
                                                    "<div class=\"photo_stat\"> followers" +
                                                   "</div>" +
                                                   "<div class=\"number-stat\">" + objInsAcc.FollowedBy +
                                                   "</div>" +
                                                "</li>" +
                                                "<li>" +
                                                    "<div class=\"photo_stat\"> following" +
                                                   "</div>" +
                                                   "<div class=\"number-stat\">" + objInsAcc.Followers +
                                                   "</div>" +
                                                "</li>" +
                                             "</ul>" +
                                     "</div>" +
                             "</div>" +
                         "</div>";
                            }

                        }

                        Response.Write(midsnaps);
                    } 
                    #endregion

                }
                else if (Request.QueryString["op"] == "accountdelete")
                {
                    #region accountdelete

                    string Profiletype = Request.QueryString["profile"];
                    string profileid = Request.QueryString["profileid"];

                    if (Profiletype == "fb")
                    {
                        try
                        {
                            FacebookAccountRepository fbrepo = new FacebookAccountRepository();
                            int delacc = fbrepo.deleteFacebookUser(profileid, user.Id);
                            if (delacc != 0)
                            {
                                SocialProfilesRepository socioprofile = new SocialProfilesRepository();
                                socioprofile.deleteProfile(user.Id, profileid);
                                FacebookMessageRepository fbmsgrepo = new FacebookMessageRepository();
                                fbmsgrepo.deleteAllMessagesOfUser(profileid, user.Id);
                                FacebookFeedRepository fbfeedrepo = new FacebookFeedRepository();
                                fbfeedrepo.deleteAllFeedsOfUser(profileid, user.Id);
                            }

                        }
                        catch (Exception exx)
                        {
                            Console.WriteLine(exx.StackTrace);

                        }
                    }
                    else if (Profiletype == "twt")
                    {
                        TwitterAccountRepository twtaccountrepo = new TwitterAccountRepository();
                        int deltwtacc = twtaccountrepo.deleteTwitterUser(user.Id, profileid);
                        if (deltwtacc != 0)
                        {
                            SocialProfilesRepository socioprofile = new SocialProfilesRepository();
                            socioprofile.deleteProfile(user.Id, profileid);


                        }
                    }
                    
                    #endregion
                }
                else if (Request.QueryString["op"] == "MasterCompose")
                {
                    #region mastercompose
                    string profiles = string.Empty;

                    if (Session["profilesforcomposemessage"] == null)
                    {
                        profiles += "<div class=\"drop_top\"></div><div class=\"drop_mid\">";

                        /*facebook users binding*/
                        FacebookAccountRepository fbrepo = new FacebookAccountRepository();
                        ArrayList lstfbaccounts = fbrepo.getAllFacebookAccountsOfUser(user.Id);


                        profiles += "<div class=\"twitte_text\">FACEBOOK</div><div class=\"teitter\"><ul>";
                        if (lstfbaccounts.Count == 0)
                        {
                            profiles += "<li>No Records Found</li>";
                        }
                        else
                        {
                            foreach (FacebookAccount fbacc in lstfbaccounts)
                            {
                                profiles += "<li id=\"liid_" + fbacc.FbUserId + "\"   onclick=\"composemessage(this.id,'fb')\"><a href=\"#\"><img id=\"img_" + fbacc.FbUserId + "\" src=\"../Contents/Images/facebook.png\" alt=\"" + fbacc.AccessToken + "\" border=\"none\" width=\"18\" style=\"float:left;\" /><span id=\"composename_" + fbacc.FbUserId + "\" style=\"float:left;margin: 3px 0 0 5px;\">" + fbacc.FbUserName + "</span><span id=\"imgurl_" + fbacc.FbUserId + "\" style=\"display:none;\">http://graph.facebook.com/" + fbacc.FbUserId + "/picture?type=small</span></a></li>";
                            }
                        }
                        profiles += "</ul> </div>";


                        /*twitter users binding*/
                        TwitterAccountRepository twtaccountrepo = new TwitterAccountRepository();
                        ArrayList alsttwtaccounts = twtaccountrepo.getAllTwitterAccountsOfUser(user.Id);
                        profiles += "<div class=\"twitte_text\">TWITTER</div><div class=\"teitter\"><ul>";
                        if (alsttwtaccounts.Count == 0)
                        {
                            profiles += "<li>No Records Found</li>";
                        }
                        else
                        {
                            foreach (TwitterAccount item in alsttwtaccounts)
                            {
                                profiles += "<li id=\"liid_" + item.TwitterUserId + "\"   onclick=\"composemessage(this.id,'twt')\"><a href=\"#\"><img id=\"img_" + item.TwitterUserId + "\" src=\"../Contents/Images/twitter.png\" alt=\"" + item.OAuthToken + "\" border=\"none\" width=\"18\" style=\"float:left;\" /><span id=\"imgurl_" + item.TwitterUserId + "\" style=\"display:none;\">" + item.ProfileImageUrl + "</span><span id=\"composename_" + item.TwitterUserId + "\" style=\"float:left;margin: 3px 0 0 5px;\">" + item.TwitterScreenName + "</span></a></li>";
                            }
                        }
                        profiles += "</ul> </div>";


                        /*linkedinuserbinding*/
                        LinkedInAccountRepository linkaccountrepo = new LinkedInAccountRepository();
                        ArrayList alstlinkacc = linkaccountrepo.getAllLinkedinAccountsOfUser(user.Id);
                        profiles += "<div class=\"twitte_text\">LinkedIn</div><div class=\"teitter\"><ul>";



                        if (alstlinkacc.Count == 0)
                        {
                            profiles += "<li>No Records Found</li>";
                        }
                        else
                        {

                            foreach (LinkedInAccount item in alstlinkacc)
                            {
                                string profileurl = string.Empty;

                                if (!string.IsNullOrEmpty(item.ProfileUrl))
                                {
                                    profileurl = item.ProfileUrl;
                                }
                                else
                                {
                                    profileurl = "../../Contents/Images/blank_img.png";
                                }
                                profiles += "<li id=\"liid_" + item.LinkedinUserId + "\"   onclick=\"composemessage(this.id,'lin')\"><a href=\"#\"><img id=\"img_" + item.LinkedinUserId + "\" src=\"../Contents/Images/link.png\" alt=\"" + item.OAuthToken + "\" border=\"none\" width=\"18\" style=\"float:left;\" /><span id=\"imgurl_" + item.LinkedinUserId + "\" style=\"display:none;\">" + profileurl + "</span><span id=\"composename_" + item.LinkedinUserId + "\" style=\"float:left;margin: 3px 0 0 5px;\">" + item.LinkedinUserName + "</span></a></li>";
                            }
                        }
                        profiles += "</ul> </div>";
                        Session["profilesforcomposemessage"] = profiles;
                    }
                    else
                    {
                        profiles = (string)Session["profilesforcomposemessage"];
                    }
                    Response.Write(profiles); 
                    #endregion
                }
                else if (Request.QueryString["op"] == "sendmessage")
                {

                    #region sendmessage
                    string message = Request.QueryString["message"];
                    var userid = Request.QueryString["userid[]"].Split(',');



                    foreach (var item in userid)
                    {
                        string[] networkingwithid = item.Split('_');
                        if (networkingwithid[0] == "fb")
                        {
                            FacebookAccountRepository fbaccountrepo = new FacebookAccountRepository();
                            FacebookAccount fbaccount = fbaccountrepo.getFacebookAccountDetailsById(networkingwithid[1], user.Id);
                            var args = new Dictionary<string, object>();
                            args["message"] = message;
                            FacebookClient fc = new FacebookClient(fbaccount.AccessToken);
                            var facebookpost = fc.Post("/me/feed", args);
                            if (facebookpost.ToString() != string.Empty)
                            {
                                Response.Write("Succesfully posted");
                            }
                            else
                            {
                                Response.Write("Not posted");
                            }

                        }
                        else if (networkingwithid[0] == "twt")
                        {
                            TwitterAccountRepository twtaccountrepo = new TwitterAccountRepository();
                            TwitterAccount twtaccount = twtaccountrepo.getUserInformation(user.Id, networkingwithid[1]);
                            oAuthTwitter OAuthTwt = new oAuthTwitter();
                            TwitterHelper twthelper = new TwitterHelper();
                            OAuthTwt.AccessToken = twtaccount.OAuthToken;
                            OAuthTwt.AccessTokenSecret = twtaccount.OAuthSecret;
                            OAuthTwt.TwitterScreenName = twtaccount.TwitterScreenName;
                            twthelper.SetCofigDetailsForTwitter(OAuthTwt);
                            TwitterUser twtuser = new TwitterUser();
                            JArray post = twtuser.Post_Status_Update(OAuthTwt, message);
                            if (post.ToString() != string.Empty)
                            {
                                Response.Write("Succesfully posted");
                            }
                            else
                            {
                                Response.Write("Not posted");
                            }
                        }
                        else if (networkingwithid[0] == "lin")
                        {
                            LinkedInAccountRepository linkedinaccrepo = new LinkedInAccountRepository();
                            LinkedInAccount linkedaccount = linkedinaccrepo.getUserInformation(user.Id, networkingwithid[1]);
                            oAuthLinkedIn Linkedin_oauth = new oAuthLinkedIn();

                            Linkedin_oauth.Verifier = linkedaccount.OAuthVerifier;
                            Linkedin_oauth.TokenSecret = linkedaccount.OAuthSecret;
                            Linkedin_oauth.Token = linkedaccount.OAuthToken;
                            Linkedin_oauth.Id = linkedaccount.LinkedinUserId;
                            Linkedin_oauth.FirstName = linkedaccount.LinkedinUserName;
                            SocialStream sociostream = new SocialStream();
                            string res = sociostream.SetStatusUpdate(Linkedin_oauth, message);
                        }
                    } 
                    #endregion
                }
                else if (Request.QueryString["op"] == "wooqueuemessages")
                {
                    ScheduledMessageRepository schmsgRepo = new ScheduledMessageRepository();
                    List<ScheduledMessage> lstschMsg = schmsgRepo.getAllMessagesOfUser(user.Id);
                    string schmessages = string.Empty;
                    string profurl = string.Empty;
                    if (string.IsNullOrEmpty(user.ProfileUrl))
                    {
                        profurl = "../Contents/img/blank_img.png";
                    }
                    else
                    {
                        profurl = user.ProfileUrl;
                    }

                    foreach (ScheduledMessage item in lstschMsg)
                    {
                        schmessages += "<div  class=\"js-task-cont read\">" +
                                                 "<section class=\"task-owner\">" +
                                                     "<img width=\"32\" height=\"32\" border=\"0\" src=\""+profurl+"\" class=\"avatar\">" +
                                                 "</section>" +
                                                 "<section class=\"task-activity third\">" +
                                                     "<p>"+user.UserName+"</p>" +
                                                     "<div>"+item.CreateTime+"</div>" +
                                                     "<input type=\"hidden\" value=\"#\" id=\"hdntaskid_1\">" +
                                                     "<p></p>" +
                                               "</section>" +
                                               "<section class=\"task-message font-13 third\"><a class=\"tip_left\">"+item.ShareMessage+"</a></section>" +
                                               "<section class=\"task-status\">" +
                                                 "<span class=\"ficon task_active\" id=\"taskcomment\">" +
                                                    // "<img width=\"14\" height=\"17\" alt=\"\" src=\"../Contents/img/task/task_pin.png\" onclick=\"getmemberdata('7fd5773f-c5b0-4624-bba1-b8a6c0fbd56d');\">" +
                                                "</span>" +
                                                "<div class=\"ui_light floating task_status_change\">" +
                                                     "<a href=\"#nogo\" class=\"ui-sproutmenu\">" +
                                                         "<span class=\"ui-sproutmenu-status\">True" +
                                                            // "<img title=\"Edit Status\" onclick=\"PerformClick(this.id)\" src=\"../Contents/img/icon_edit.png\" class=\"edit_button\" id=\"img_7fd5773f-c5b0-4624-bba1-b8a6c0fbd56d_True\">
                                                            "</span>" +
                                                    "</a>" +
                                                "</div>" +
                                            "</section>" +
                                         "</div>";
                    }
                    Response.Write(schmessages);

                }
                else if (Request.QueryString["op"] == "schedulemessage")
                {

                    var userid = Request.QueryString["users[]"].Split(',');
                    var datearr = Request.QueryString["datearr[]"].Split(',');
                    string message = Request.QueryString["message"];
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
                                    string servertime = this.CompareDateWithServer(clienttime, child + " " + time);
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
            }
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


    }
}