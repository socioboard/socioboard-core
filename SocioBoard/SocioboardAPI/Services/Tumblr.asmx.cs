using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Domain.Socioboard.Domain;
using GlobusLinkedinLib.Authentication;
using GlobusLinkedinLib.App.Core;
using GlobusTumblrLib.Authentication;
using Newtonsoft.Json.Linq;
using GlobusTumblerLib.App.Core;
using System.Text.RegularExpressions;
using GlobusTumblerLib.Tumblr.Core.BlogMethods;
using log4net;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for Group
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class Tumblr : System.Web.Services.WebService
    {
        ILog logger = LogManager.GetLogger(typeof(Tumblr));
        GroupsRepository objGroupsRepository = new GroupsRepository();
        TeamRepository objTeamRepository = new TeamRepository();
        Domain.Socioboard.Domain.Team objteam;
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile;
        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
        Domain.Socioboard.Domain.SocialProfile objSocialProfile;

        TumblrAccountRepository objTumblrAccountRepository = new TumblrAccountRepository();
        Domain.Socioboard.Domain.TumblrAccount objTumblrAccount;
        TumblrFeedRepository objTumblrFeedRepository = new TumblrFeedRepository();
        Domain.Socioboard.Domain.TumblrFeed objTumblrFeed;
        ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
        Domain.Socioboard.Domain.ScheduledMessage objScheduledMessage;

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]


        public string GetTumblrRedirectUrl(string consumerKey, string consumerSecret, string CallBackUrl)
        {
            string ret = string.Empty;
            try
            {
                oAuthTumbler requestHelper = new oAuthTumbler();
                oAuthTumbler.TumblrConsumerKey = consumerKey;
                oAuthTumbler.TumblrConsumerSecret = consumerSecret;
                requestHelper.CallBackUrl = CallBackUrl;
                ret = requestHelper.GetAuthorizationLink();
                logger.Error("GetTumblrRedirectUrl => " + ret);
            }
            catch (Exception ex)
            {
                logger.Error("GetTumblrRedirectUrl => "+ex.StackTrace);
            }
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddTumblrAccount(string client_id, string client_secret, string redirect_uri, string UserId, string GroupId, string code)
        {
            string ret = string.Empty;
            string AccessTokenResponse = string.Empty;
            try
            {
                oAuthTumbler requestHelper = new oAuthTumbler();
                oAuthTumbler.TumblrConsumerKey = client_id;
                oAuthTumbler.TumblrConsumerSecret = client_secret;
                requestHelper.TumblrCallBackUrl = redirect_uri;
                AccessTokenResponse = requestHelper.GetAccessToken(oAuthTumbler.TumblrToken, code);
                logger.Error(AccessTokenResponse);

                string[] tokens = AccessTokenResponse.Split('&'); //extract access token & secret from response
                logger.Error(tokens);
                string accessToken = tokens[0].Split('=')[1];
                logger.Error(accessToken);
                string accessTokenSecret = tokens[1].Split('=')[1];
                logger.Error(accessTokenSecret);

                KeyValuePair<string, string> LoginDetails = new KeyValuePair<string, string>(accessToken, accessTokenSecret);

                JObject profile = new JObject();
                try
                {
                    profile = JObject.Parse(oAuthTumbler.OAuthData(Globals.UsersInfoUrl, "GET", LoginDetails.Key, LoginDetails.Value, null));
                }
                catch (Exception ex)
                {
                }


                #region Add Tumblr Account
                objTumblrAccount = new Domain.Socioboard.Domain.TumblrAccount();
                objTumblrAccount.Id = Guid.NewGuid();
                objTumblrAccount.tblrUserName = profile["response"]["user"]["name"].ToString();
                objTumblrAccount.UserId = Guid.Parse(UserId);
                objTumblrAccount.tblrAccessToken = accessToken;
                objTumblrAccount.tblrAccessTokenSecret = accessTokenSecret;
                objTumblrAccount.tblrProfilePicUrl = "http://api.tumblr.com/v2/blog/" + objTumblrAccount.tblrUserName + ".tumblr.com/avatar";//profile["response"]["user"]["name"].ToString();
                objTumblrAccount.IsActive = 1;
                if (!objTumblrAccountRepository.checkTubmlrUserExists(objTumblrAccount))
                {
                    TumblrAccountRepository.Add(objTumblrAccount);

                    #region Add Socialprofiles
                    objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
                    objSocialProfile.Id = Guid.NewGuid();
                    objSocialProfile.UserId = Guid.Parse(UserId);
                    objSocialProfile.ProfileId = profile["response"]["user"]["name"].ToString();
                    objSocialProfile.ProfileType = "tumblr";
                    objSocialProfile.ProfileDate = DateTime.Now;
                    objSocialProfile.ProfileStatus = 1;
                    if (!objSocialProfilesRepository.checkUserProfileExist(objSocialProfile))
                    {
                        objSocialProfilesRepository.addNewProfileForUser(objSocialProfile);
                    }
                    #endregion

                    #region Add TeamMemeberProfile
                    Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(GroupId));
                    Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
                    objTeamMemberProfile.Id = Guid.NewGuid();
                    objTeamMemberProfile.TeamId = objTeam.Id;
                    objTeamMemberProfile.Status = 1;
                    objTeamMemberProfile.ProfileType = "tumblr";
                    objTeamMemberProfile.StatusUpdateDate = DateTime.Now;
                    objTeamMemberProfile.ProfileId = objTumblrAccount.tblrUserName;

                    //Modified [13-02-15]
                    objTeamMemberProfile.ProfilePicUrl = objTumblrAccount.tblrProfilePicUrl;
                    objTeamMemberProfile.ProfileName = objTumblrAccount.tblrUserName;

                    objTeamMemberProfileRepository.addNewTeamMember(objTeamMemberProfile);
                    #endregion
                }
                #endregion

              
                //if (!objTeamMemberProfileRepository.checkTeamMemberProfile(objTeam.Id, objTumblrAccount.tblrUserName))
                //{
                //    objTeamMemberProfileRepository.addNewTeamMember(objTeamMemberProfile);
                //}

                #region Add Tumblr Feeds
                AddTunblrFeeds(UserId, LoginDetails, profile["response"]["user"]["name"].ToString());
                #endregion

            }
            catch (Exception ex)
            {
                logger.Error("AddTumblrAccount => "+ex.StackTrace);
            }
            return ret;
        }

        private void AddTunblrFeeds(string UserId, KeyValuePair<string, string> LoginDetails, string username)
        {
            JObject UserDashboard = JObject.Parse(oAuthTumbler.OAuthData(Globals.UsersDashboardUrl, "GET", LoginDetails.Key, LoginDetails.Value, null));
            JArray objJarray = (JArray)UserDashboard["response"]["posts"];
            foreach (var item in objJarray)
            {
                objTumblrFeed = new Domain.Socioboard.Domain.TumblrFeed();
                objTumblrFeed.Id = Guid.NewGuid();
                objTumblrFeed.UserId = Guid.Parse(UserId);
                try
                {
                    objTumblrFeed.ProfileId = username;

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                try
                {
                    objTumblrFeed.blogname = item["blog_name"].ToString();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                try
                {
                    objTumblrFeed.blogId = item["id"].ToString();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                try
                {
                    objTumblrFeed.blogposturl = item["post_url"].ToString();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                try
                {
                    String result = item["caption"].ToString();
                    objTumblrFeed.description = Regex.Replace(result, @"<[^>]*>", String.Empty);
                }
                catch (Exception ex)
                {
                    objTumblrFeed.description = null;
                    Console.WriteLine(ex.Message);
                }
                try
                {
                    objTumblrFeed.slug = item["slug"].ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                try
                {
                    objTumblrFeed.type = item["type"].ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                try
                {
                    string test = item["date"].ToString();
                    DateTime dt;
                    if (test.Contains("GMT"))
                    {
                        test = test.Replace("GMT", "").Trim().ToString();
                        dt = Convert.ToDateTime(test);
                    }
                    else
                    {
                        test = test.Replace("GMT", "").Trim().ToString();
                        dt = Convert.ToDateTime(test);
                    }
                    objTumblrFeed.date = dt;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                try
                {
                    objTumblrFeed.reblogkey = item["reblog_key"].ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                try
                {
                    string str = item["liked"].ToString();
                    if (str == "False")
                    {
                        objTumblrFeed.liked = 0;
                    }
                    else { objTumblrFeed.liked = 1; }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                try
                {
                    string str = item["followed"].ToString();
                    if (str == "false")
                    {
                        objTumblrFeed.followed = 0;
                    }
                    else { objTumblrFeed.followed = 1; }
                    // objTumblrDashboard.followed = Convert.ToInt16(item["followed"]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                try
                {
                    objTumblrFeed.canreply = Convert.ToInt16(item["can_reply"]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                try
                {
                    objTumblrFeed.sourceurl = item["source_url"].ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                try
                {
                    objTumblrFeed.sourcetitle = item["source_title"].ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                try
                {
                    JArray asdasd12 = (JArray)item["photos"];
                    foreach (var item1 in asdasd12)
                    {
                        objTumblrFeed.imageurl = item1["original_size"]["url"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                try
                {
                    objTumblrFeed.videourl = item["permalink_url"].ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                try
                {
                    string str = item["note_count"].ToString();
                    objTumblrFeed.notes = Convert.ToInt16(str);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                objTumblrFeed.timestamp = DateTime.Now;
                if (!objTumblrFeedRepository.checkTumblrMessageExists(objTumblrFeed))
                {
                    try
                    {
                        TumblrFeedRepository.Add(objTumblrFeed);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getTumblrData(string UserId, string Tumblrid)
        {
            Guid userId = Guid.Parse(UserId);
            oAuthTumbler Obj_oAuthTumbler = new oAuthTumbler();
            oAuthTumbler.TumblrConsumerKey = ConfigurationManager.AppSettings["TumblrClientKey"];
            oAuthTumbler.TumblrConsumerSecret = ConfigurationManager.AppSettings["TumblrClientSec"];
            Obj_oAuthTumbler.TumblrCallBackUrl = ConfigurationManager.AppSettings["TumblrCallBackURL"];
            TumblrAccountRepository objTumblrAccountRepository = new TumblrAccountRepository();
            Domain.Socioboard.Domain.TumblrAccount ObjTumblrAccount = objTumblrAccountRepository.getTumblrAccountDetailsById(Tumblrid, userId);
            #region UpdateTeammemberprofile
            Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
            objTeamMemberProfile.ProfileName = ObjTumblrAccount.tblrUserName;
            objTeamMemberProfile.ProfilePicUrl = ObjTumblrAccount.tblrProfilePicUrl;
            objTeamMemberProfile.ProfileId = ObjTumblrAccount.tblrUserName;
            objTeamMemberProfileRepository.updateTeamMemberbyprofileid(objTeamMemberProfile);
            #endregion
            oAuthTumbler.TumblrToken = ObjTumblrAccount.tblrAccessToken;
            oAuthTumbler.TumblrTokenSecret = ObjTumblrAccount.tblrAccessTokenSecret;
            KeyValuePair<string, string> LoginDetails = new KeyValuePair<string, string>(ObjTumblrAccount.tblrAccessToken, ObjTumblrAccount.tblrAccessTokenSecret);
            AddTunblrFeeds(UserId, LoginDetails, ObjTumblrAccount.tblrUserName);
            Domain.Socioboard.Domain.TumblrFeed tumblrTumblrFeed = new Domain.Socioboard.Domain.TumblrFeed();
            TumblrFeedRepository.Add(tumblrTumblrFeed);
            return "Tumblr info is updated successfully";
            //Obj_oAuthTumbler.TumblrOAuthVerifier=ObjTumblrAccount.tbl

        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SheduleTumblrMessage(string TumblrId, string UserId, string sscheduledmsgguid)
        {
            string str = string.Empty;
            try
            {
                Guid userId = Guid.Parse(UserId);
                oAuthTumbler Obj_oAuthTumbler = new oAuthTumbler();
                oAuthTumbler.TumblrConsumerKey = ConfigurationManager.AppSettings["TumblrClientKey"];
                oAuthTumbler.TumblrConsumerSecret = ConfigurationManager.AppSettings["TumblrClientSec"];
                Obj_oAuthTumbler.TumblrCallBackUrl = ConfigurationManager.AppSettings["TumblrCallBackURL"];
                objScheduledMessage = objScheduledMessageRepository.GetScheduledMessageDetails(Guid.Parse(sscheduledmsgguid));
                Domain.Socioboard.Domain.TumblrAccount ObjTumblrAccount = objTumblrAccountRepository.getTumblrAccountDetailsById(TumblrId, Guid.Parse(UserId));
                oAuthTumbler.TumblrToken = ObjTumblrAccount.tblrAccessToken;
                oAuthTumbler.TumblrTokenSecret = ObjTumblrAccount.tblrAccessTokenSecret;
                PublishedPosts objPublishedPosts = new PublishedPosts();
                string picurl = objScheduledMessage.PicUrl;
                string message = objScheduledMessage.ShareMessage;
                if (string.IsNullOrEmpty(objScheduledMessage.ShareMessage) && string.IsNullOrEmpty(objScheduledMessage.PicUrl))
                {
                    //objScheduledMessage.ShareMessage = "There is no data in Share Message !";
                    str = "There is no data in Share Message !";
                }
                else
                {
                    try
                    {
                        //objPublishedPosts.PostData(ObjTumblrAccount.tblrAccessToken, ObjTumblrAccount.tblrAccessTokenSecret, ObjTumblrAccount.tblrUserName, objScheduledMessage.ShareMessage, "", "Text");
                        if (!string.IsNullOrEmpty(picurl))
                        {
                            objPublishedPosts.PostData(ObjTumblrAccount.tblrAccessToken, ObjTumblrAccount.tblrAccessTokenSecret, objScheduledMessage.ProfileId, message, picurl, "photo");
                        }
                        else
                        {
                            objPublishedPosts.PostData(ObjTumblrAccount.tblrAccessToken, ObjTumblrAccount.tblrAccessTokenSecret, objScheduledMessage.ProfileId, message, "", "text");
                        }
                        str = "Message post on tumblr for Id :" + ObjTumblrAccount.tblrUserName + " and Message: " + objScheduledMessage.ShareMessage;
                        ScheduledMessage schmsg = new ScheduledMessage();
                        schmsg.UpdateScheduledMessageByMsgId(Guid.Parse(sscheduledmsgguid));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        str = "Message is not posted";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                str = ex.Message;
            }
            return str;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string TumblerData(string tumblrusername)
        {
            GlobusTumblerLib.Tumblr.Core.BlogMethods.BlogInfo objBlogInfo = new GlobusTumblerLib.Tumblr.Core.BlogMethods.BlogInfo();

            try
            {
                string PostCount = "";
                string LikesCount = "";

                string objData = objBlogInfo.getTumblrUserInfo(tumblrusername);
                string[] words = objData.Split('&');
                PostCount = words[1].ToString();
                LikesCount = words[0].ToString();
                Domain.Socioboard.Domain.TumblerData objDomainTumblerData = new Domain.Socioboard.Domain.TumblerData();
                objDomainTumblerData.PostCount = PostCount;
                objDomainTumblerData.LikesCount = LikesCount;

                return new JavaScriptSerializer().Serialize(objDomainTumblerData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "";
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string TumblrComposeMessage(String message, String profileid, string userid, string currentdatetime, string picurl)
        {
            string ret = "";
            Domain.Socioboard.Domain.TumblrAccount objTumblrAccount = objTumblrAccountRepository.getTumblrAccountDetailsById(profileid, Guid.Parse(userid));
            oAuthTumbler Obj_oAuthTumbler = new oAuthTumbler();
            oAuthTumbler.TumblrToken = objTumblrAccount.tblrAccessToken;
            oAuthTumbler.TumblrTokenSecret = objTumblrAccount.tblrAccessTokenSecret;
            PublishedPosts objPublishedPosts = new PublishedPosts();
            if (!string.IsNullOrEmpty(picurl))
            {
                objPublishedPosts.PostData(objTumblrAccount.tblrAccessToken, objTumblrAccount.tblrAccessTokenSecret, profileid, message, picurl, "photo");
            }
            else
            {
                objPublishedPosts.PostData(objTumblrAccount.tblrAccessToken, objTumblrAccount.tblrAccessTokenSecret, profileid, message, "", "text");
            }
            return ret;
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateTumblrAccountByAdmin(string ObjTumblr)
        {
            Domain.Socioboard.Domain.TumblrAccount ObjTumblrAccount = (Domain.Socioboard.Domain.TumblrAccount)(new JavaScriptSerializer().Deserialize(ObjTumblr, typeof(Domain.Socioboard.Domain.TumblrAccount)));
            try
            {
                objTumblrAccountRepository.updateTumblrUser(ObjTumblrAccount);
                return new JavaScriptSerializer().Serialize("Update Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Something went Wrong");
            }
        }


    }
}
