using Facebook;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Domain.Socioboard.Domain;

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
    public class Facebook : System.Web.Services.WebService
    {
        GroupsRepository objGroupsRepository = new GroupsRepository();
        TeamRepository objTeamRepository = new TeamRepository();
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        FacebookAccountRepository objFacebookAccountRepository = new FacebookAccountRepository();
        Domain.Socioboard.Domain.FacebookAccount objFacebookAccount;
        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
        FacebookFeedRepository objFacebookFeedRepository = new FacebookFeedRepository();
        Domain.Socioboard.Domain.FacebookFeed objFacebookFeed;
        FacebookMessageRepository objFacebookMessageRepository = new FacebookMessageRepository();
        Domain.Socioboard.Domain.FacebookMessage objFacebookMessage;
        ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
        Domain.Socioboard.Domain.ScheduledMessage objScheduledMessage;
        FacebookFanPageRepository objFacebookFanPageRepository = new FacebookFanPageRepository();
        Domain.Socioboard.Domain.AddFacebookPage objFacebookPage;



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddFacebookAccount(string code, string UserId, string GroupId)
        {
            string ret = string.Empty;
            string client_id = ConfigurationManager.AppSettings["ClientId"];
            string redirect_uri = ConfigurationManager.AppSettings["RedirectUrl"];
            string client_secret = ConfigurationManager.AppSettings["ClientSecretKey"];
            long friendscount = 0;
            try
            {
                FacebookClient fb = new FacebookClient();
                string profileId = string.Empty;
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("client_id", client_id);
                parameters.Add("redirect_uri", redirect_uri);
                parameters.Add("client_secret", client_secret);
                parameters.Add("code", code);
                JsonObject fbaccess_token = (JsonObject)fb.Get("/oauth/access_token", parameters);
                string accessToken = fbaccess_token["access_token"].ToString();
                if (accessToken != null)
                {
                    fb.AccessToken = accessToken;
                    dynamic profile = fb.Get("me");
                    dynamic friends = fb.Get("me/friends");
                    try
                    {
                        friendscount = Convert.ToInt16(friends["summary"]["total_count"].ToString());
                    }
                    catch (Exception)
                    {
                        friendscount = 0;
                    }
                    if (!objFacebookAccountRepository.checkFacebookUserExists(Convert.ToString(profile["id"]), Guid.Parse(UserId)))
                    {
                        #region Add FacebookAccount
                        objFacebookAccount = new Domain.Socioboard.Domain.FacebookAccount();
                        objFacebookAccount.Id = Guid.NewGuid();
                        objFacebookAccount.FbUserId = (Convert.ToString(profile["id"]));
                        objFacebookAccount.FbUserName = (Convert.ToString(profile["name"]));
                        objFacebookAccount.AccessToken = accessToken;
                        objFacebookAccount.Friends = Convert.ToInt16(friendscount);
                        objFacebookAccount.EmailId = (Convert.ToString(profile["email"]));
                        objFacebookAccount.Type = "account";
                        objFacebookAccount.ProfileUrl = (Convert.ToString(profile["link"]));
                        objFacebookAccount.IsActive = 1;
                        objFacebookAccount.UserId = Guid.Parse(UserId);
                        objFacebookAccountRepository.addFacebookUser(objFacebookAccount);
                        #endregion
                        #region Add TeamMemberProfile
                        Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(GroupId));
                        Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
                        objTeamMemberProfile.Id = Guid.NewGuid();
                        objTeamMemberProfile.TeamId = objTeam.Id;
                        objTeamMemberProfile.Status = 1;
                        objTeamMemberProfile.ProfileType = "facebook";
                        objTeamMemberProfile.StatusUpdateDate = DateTime.Now;
                        objTeamMemberProfile.ProfileId = Convert.ToString(profile["id"]);
                        objTeamMemberProfileRepository.addNewTeamMember(objTeamMemberProfile);
                        #endregion
                        #region SocialProfile
                        Domain.Socioboard.Domain.SocialProfile objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
                        objSocialProfile.Id = Guid.NewGuid();
                        objSocialProfile.ProfileType = "facebook";
                        objSocialProfile.ProfileId = (Convert.ToString(profile["id"]));
                        objSocialProfile.UserId = Guid.Parse(UserId);
                        objSocialProfile.ProfileDate = DateTime.Now;
                        objSocialProfile.ProfileStatus = 1;
                        #endregion
                        if (!objSocialProfilesRepository.checkUserProfileExist(objSocialProfile))
                        {
                            objSocialProfilesRepository.addNewProfileForUser(objSocialProfile);
                        }
                        #region Add Facebook Feeds
                        AddFacebookFeeds(UserId, fb, profile);
                        #endregion
                        #region Add Facebook User Home
                        AddFacebookUserHome(UserId, fb, profile);
                        #endregion
                        #region Add Facebook User Inbox Message
                        AddFacebookMessage(UserId, fb, profile);
                        #endregion
                        ret = "Account Added Successfully";
                    }
                    else
                    {
                        objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(Convert.ToString(profile["id"]), Guid.Parse(UserId));
                        if (objFacebookAccount.IsActive == 2)
                        {
                            objFacebookAccount.IsActive = 1;
                            objFacebookAccount.AccessToken = accessToken;
                            objFacebookAccountRepository.updateFacebookUser(objFacebookAccount);

                            Domain.Socioboard.Domain.SocialProfile objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
                            objSocialProfile.Id = Guid.NewGuid();
                            objSocialProfile.ProfileType = "facebook";
                            objSocialProfile.ProfileId = objFacebookAccount.FbUserId;
                            objSocialProfile.UserId = Guid.Parse(UserId);
                            objSocialProfile.ProfileDate = DateTime.Now;
                            objSocialProfile.ProfileStatus = 1;
                            objSocialProfilesRepository.updateSocialProfileStatus(objSocialProfile);
                            ret = "Account Updated successfully !";
                        }
                        else
                        {
                            ret = "Account already Exist !";
                        }

                    }
                }
                return new JavaScriptSerializer().Serialize(ret);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetFacebookData(string FbId, string UserId)
        {
            string ret = string.Empty;
            long friendscount = 0;
            try
            {
                Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(FbId, Guid.Parse(UserId));


                FacebookClient fb = new FacebookClient();
                string profileId = string.Empty;
                string accessToken = objFacebookAccount.AccessToken;
                dynamic profile = null;
                dynamic friends = null;
                if (accessToken != null)
                {
                    fb.AccessToken = accessToken;
                    try
                    {
                        profile = fb.Get("me");

                    }
                    catch (Exception ex)
                    {
                        string errormssg = ex.Message;
                        if (errormssg.Contains("changed the password"))
                        {
                            UpdateSocialprofileStatus(UserId, FbId);
                            objFacebookAccount.IsActive = 2;
                            UpdateFacebookAccount(objFacebookAccount);
                        }
                        return "Token Expired";
                    }
                    try
                    {
                        friends = fb.Get("me/friends");
                    }
                    catch (Exception ex)
                    {
                        string errormssg = ex.Message;
                        if (errormssg.Contains("changed the password"))
                        {
                            UpdateSocialprofileStatus(UserId, FbId);
                        }
                        return "Token Expired";
                    }
                    friendscount = Convert.ToInt16(friends["summary"]["total_count"].ToString());
                    if (objFacebookAccountRepository.checkFacebookUserExists(Convert.ToString(profile["id"]), Guid.Parse(UserId)))
                    {
                        #region Update FacebookAccount
                        UpdateFacebookAccount(objFacebookAccount);
                        #endregion
                        #region Add Facebook Feeds
                        AddFacebookFeeds(UserId, fb, profile);
                        #endregion
                        #region Add Facebook User Home
                        AddFacebookUserHome(UserId, fb, profile);
                        #endregion
                        #region Add Facebook User Inbox Message
                        AddFacebookMessage(UserId, fb, profile);
                        #endregion
                        ret = "Facebook info Updated Successfully";
                    }
                    else
                    {
                        ret = "Account already Exist !";
                    }
                }
                return new JavaScriptSerializer().Serialize(ret);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        private void UpdateFacebookAccount(Domain.Socioboard.Domain.FacebookAccount objFacebookAccount)
        {
            objFacebookAccountRepository.updateFacebookUser(objFacebookAccount);
        }

        private void AddFacebookMessage(string UserId, FacebookClient fb, dynamic profile)
        {
            objFacebookMessage = new Domain.Socioboard.Domain.FacebookMessage();
            dynamic messages = fb.Get("/me/inbox");
            if (messages != null)
            {
                foreach (dynamic result in messages["data"])
                {
                    try
                    {
                        foreach (dynamic message in result["comments"]["data"])
                        {
                            objFacebookMessage.MessageId = message["id"];
                            objFacebookMessage.FromName = message["from"]["name"];
                            objFacebookMessage.FromId = message["from"]["id"];
                            objFacebookMessage.Message = message["message"];
                            objFacebookMessage.MessageDate = DateTime.Parse(message["created_time"].ToString()); ;
                            objFacebookMessage.FromProfileUrl = "http://graph.facebook.com/" + message["from"]["id"] + "/picture?type=small";
                            objFacebookMessage.EntryDate = DateTime.Now;
                            objFacebookMessage.Id = Guid.NewGuid();
                            objFacebookMessage.ProfileId = profile["id"].ToString();
                            objFacebookMessage.Type = "inbox_message";
                            objFacebookMessage.UserId = Guid.Parse(UserId);
                            if (!objFacebookMessageRepository.checkFacebookMessageExists(objFacebookMessage.MessageId))
                            {
                                objFacebookMessageRepository.addFacebookMessage(objFacebookMessage);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                }
            }
        }

        private void AddFacebookUserHome(string UserId, FacebookClient fb, dynamic profile)
        {
            objFacebookMessage = new Domain.Socioboard.Domain.FacebookMessage();
            dynamic home = fb.Get("/me/home");
            if (home != null)
            {
                int lstfbcount = 0;
                foreach (dynamic result in home["data"])
                {
                    string message = string.Empty;
                    string imgprof = "http://graph.facebook.com/" + result["from"]["id"] + "/picture?type=small";
                    objFacebookMessage.EntryDate = DateTime.Now;
                    objFacebookMessage.MessageId = result["id"].ToString();
                    objFacebookMessage.FromId = result["from"]["id"].ToString();
                    objFacebookMessage.FromName = result["from"]["name"].ToString();
                    objFacebookMessage.FromProfileUrl = imgprof;
                    objFacebookMessage.Id = Guid.NewGuid();
                    objFacebookMessage.MessageDate = DateTime.Parse(result["created_time"].ToString());
                    objFacebookMessage.UserId = Guid.Parse(UserId);

                    try
                    {
                        objFacebookMessage.Picture = result["picture"].ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        objFacebookMessage.Picture = null;
                    }
                    try
                    {
                        if (result["message_tags"][0] != null)
                        {
                            if (result["to"] != null)
                            {
                                foreach (var item in result["to"]["data"])
                                {
                                    if (result["from"] != null)
                                    {

                                        if (item["id"] != profile["id"])
                                        {
                                            if (result["from"]["id"] == profile["id"])
                                            {
                                                objFacebookMessage.Type = "fb_tag";
                                            }
                                            else
                                            {
                                                objFacebookMessage.Type = "fb_home";
                                            }
                                        }
                                        else
                                        {
                                            objFacebookMessage.Type = "fb_home";
                                        }
                                    }
                                }

                            }
                            else
                            {
                                objFacebookMessage.Type = "fb_home";

                            }
                        }
                        else
                        {
                            objFacebookMessage.Type = "fb_home";
                        }
                    }
                    catch (Exception ex)
                    {
                        objFacebookMessage.Type = "fb_home";
                        Console.WriteLine(ex.StackTrace);
                    }
                    objFacebookMessage.ProfileId = profile["id"].ToString();
                    objFacebookMessage.FbComment = "http://graph.facebook.com/" + result["id"] + "/comments";
                    dynamic likes = fb.Get("/" + result["id"].ToString() + "/likes?limit=50000000");
                    string likestatus = "likes";
                    foreach (dynamic like in likes["data"])
                    {
                        try
                        {
                            if (profile["id"].ToString() == like["id"].ToString())
                            {
                                likestatus = "unlike";
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            likestatus = "likes";

                        }
                    }
                    objFacebookMessage.FbLike = likestatus;
                    if (lstfbcount < 25)
                    {
                        try
                        {
                            if (result["message"] != null)
                            {
                                message = result["message"];
                                lstfbcount++;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            try
                            {
                                if (result["description"] != null)
                                {
                                    message = result["description"];
                                    lstfbcount++;
                                }
                            }
                            catch (Exception exx)
                            {
                                try
                                {
                                    Console.WriteLine(exx.StackTrace);
                                    if (result["story"] != null)
                                    {
                                        message = result["story"];
                                        lstfbcount++;
                                    }
                                }
                                catch (Exception exxx)
                                {
                                    Console.WriteLine(exxx.StackTrace);
                                    message = string.Empty;
                                }
                            }
                        }
                    }
                    objFacebookMessage.Message = message;

                    if (!objFacebookMessageRepository.checkFacebookMessageExists(objFacebookMessage.MessageId))
                    {
                        objFacebookMessageRepository.addFacebookMessage(objFacebookMessage);
                    }
                }
            }
        }

        private void AddFacebookFeeds(string UserId, FacebookClient fb, dynamic profile)
        {
            objFacebookFeed = new Domain.Socioboard.Domain.FacebookFeed();
            dynamic feeds = fb.Get("/me/posts");
            dynamic feedss = fb.Get("/338818712957141/posts");
            dynamic feedsss = fb.Get("/338818712957141/feed");
            if (feeds != null)
            {
                foreach (var result in feeds["data"])
                {

                    objFacebookFeed.Type = "fb_feed";

                    try
                    {
                        objFacebookFeed.UserId = Guid.Parse(UserId);
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex.StackTrace); }

                    try
                    {
                        objFacebookFeed.ProfileId = profile["id"].ToString();
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex.StackTrace); }

                    try
                    {
                        objFacebookFeed.Id = Guid.NewGuid();
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex.StackTrace); }



                    objFacebookFeed.FromProfileUrl = "http://graph.facebook.com/" + result["from"]["id"] + "/picture?type=small";
                    objFacebookFeed.FromName = result["from"]["name"].ToString();
                    objFacebookFeed.FromId = result["from"]["id"].ToString();
                    objFacebookFeed.FeedId = result["id"].ToString();
                    objFacebookFeed.FeedDate = DateTime.Parse(result["created_time"].ToString());
                    objFacebookFeed.FbComment = "http://graph.facebook.com/" + result["id"] + "/comments";
                    objFacebookFeed.FbLike = "http://graph.facebook.com/" + result["id"] + "/likes";
                    string message = string.Empty;
                    int lstfbcount = 0;

                    if (lstfbcount < 25)
                    {
                        try
                        {
                            if (result["message"] != null)
                            {
                                message = result["message"];
                                lstfbcount++;

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            try
                            {
                                if (result["description"] != null)
                                {
                                    message = result["description"];
                                    lstfbcount++;

                                }
                            }
                            catch (Exception exx)
                            {
                                try
                                {
                                    Console.WriteLine(exx.StackTrace);
                                    if (result["story"] != null)
                                    {
                                        message = result["story"];
                                        lstfbcount++;
                                    }
                                }
                                catch (Exception exxx)
                                {
                                    Console.WriteLine(exxx.StackTrace);
                                    message = string.Empty;
                                }
                            }

                        }
                    }
                    objFacebookFeed.FeedDescription = message;
                    objFacebookFeed.EntryDate = DateTime.Now;

                    if (!objFacebookFeedRepository.checkFacebookFeedExists(objFacebookFeed.FeedId))
                    {
                        objFacebookFeedRepository.addFacebookFeed(objFacebookFeed);
                    }
                }
            }
        }

        private void UpdateSocialprofileStatus(string UserId, string profile)
        {
            Domain.Socioboard.Domain.SocialProfile objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
            objSocialProfile.Id = Guid.NewGuid();
            objSocialProfile.ProfileType = "facebook";
            objSocialProfile.ProfileId = profile;
            objSocialProfile.UserId = Guid.Parse(UserId);
            objSocialProfile.ProfileDate = DateTime.Now;
            objSocialProfile.ProfileStatus = 2;
            objSocialProfilesRepository.updateSocialProfileStatus(objSocialProfile);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllFbGroupdata(string groupid, string accestoken)
        {
            List<FacebookGroupData> _FacebookGroupData = new List<FacebookGroupData>();

            string ret = string.Empty;
            try
            {

                FacebookClient fb = new FacebookClient();
                fb.AccessToken = accestoken;
                dynamic groupsfeeds = fb.Get(groupid + "/feed");
                foreach (var item in groupsfeeds.data)
                {
                    FacebookGroupData obj = new FacebookGroupData();
                    try
                    {
                        obj.accesstoken = accestoken;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        obj.PostId = item["id"].ToString();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        obj.PostUserId = item["from"]["id"].ToString();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        obj.ProfilePic = "https://graph.facebook.com/" + obj.PostUserId + "/picture?type=small";

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        obj.Message = item["message"].ToString();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        obj.Name = item["from"]["name"].ToString();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        obj.Picture = item["picture"].ToString();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        obj.Link = item["link"].ToString();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        obj.Likecount = item["like_count"].ToString();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        obj.Userlikes = item["user_likes"].ToString();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        obj.CreatedTime = DateTime.Parse(item["created_time"].ToString());

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    _FacebookGroupData.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return new JavaScriptSerializer().Serialize(_FacebookGroupData);

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string FacebookLike(String msgguid, String profileid, string msgid, string userid)
        {
            string ret = "";
            Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(profileid, Guid.Parse(userid));
            FacebookClient fb = new FacebookClient();
            fb.AccessToken = objFacebookAccount.AccessToken;
            objFacebookMessage = objFacebookMessageRepository.GetFacebookUserWallPostDetails(Guid.Parse(msgguid));
            if (objFacebookMessage.FbLike == "likes")
            {
                var s = fb.Post(msgid + "/likes", null);
                objFacebookMessage.FbLike = "unlike";
            }
            else
            {
                var s = fb.Delete(msgid + "/likes", null);
                objFacebookMessage.FbLike = "likes";
            }
            objFacebookMessageRepository.updateFacebookMessage(objFacebookMessage);
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string FacebookComment(String message, String profileid, string commentid, string userid)
        {
            string ret = "";
            try
            {
                Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(profileid, Guid.Parse(userid));
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = objFacebookAccount.AccessToken;
                var args = new Dictionary<string, object>();
                args["message"] = message;
                var s = fb.Post(commentid + "/comments", args);
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string PostOnFBGroupFeeds(string gid, string ack, string msg, string userid)
        {
            string postFBGroupFeeds = string.Empty;
            try
            {
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = ack;

                Dictionary<string, object> dicFeed = new Dictionary<string, object>();
                dicFeed.Add("message", msg);

                object postId = fb.Post(gid + "/feed", dicFeed);
                postFBGroupFeeds = postId.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return postFBGroupFeeds;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string FacebookComposeMessage(String message, String profileid, string userid, string currentdatetime, string imagepath)
        {
            string ret = "";
            var args = new Dictionary<string, object>();
            args["message"] = message;
            if (!string.IsNullOrEmpty(imagepath))
            {
                args["picture"] = imagepath;
            }
            Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(profileid, Guid.Parse(userid));
            FacebookClient fb = new FacebookClient();
            fb.AccessToken = objFacebookAccount.AccessToken;
            ret = fb.Post("/" + objFacebookAccount.FbUserId + "/feed", args).ToString();
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string FacebookLogin(string code)
        {
            string ret = string.Empty;
            Domain.Socioboard.Domain.User objUser = new Domain.Socioboard.Domain.User();
            string client_id = ConfigurationManager.AppSettings["ClientId"];
            string redirect_uri = ConfigurationManager.AppSettings["RedirectUrl"];
            string client_secret = ConfigurationManager.AppSettings["ClientSecretKey"];
            try
            {
                FacebookClient fb = new FacebookClient();
                string profileId = string.Empty;
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("client_id", client_id);
                parameters.Add("redirect_uri", redirect_uri);
                parameters.Add("client_secret", client_secret);
                parameters.Add("code", code);
                JsonObject fbaccess_token = (JsonObject)fb.Get("/oauth/access_token", parameters);
                string accessToken = fbaccess_token["access_token"].ToString();
                if (accessToken != null)
                {
                    string fname = string.Empty;
                    string lname = string.Empty;
                    fb.AccessToken = accessToken;
                    dynamic profile = fb.Get("me");
                    objUser.UserName = (Convert.ToString(profile["name"]));
                    objUser.EmailId = (Convert.ToString(profile["email"]));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new JavaScriptSerializer().Serialize(objUser);
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SheduleFacebookMessage(string FacebookId, string UserId, string sscheduledmsgguid)
        {
            string str = string.Empty;
            try
            {
                objScheduledMessage = objScheduledMessageRepository.GetScheduledMessageDetails(Guid.Parse(sscheduledmsgguid));

                objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(FacebookId, Guid.Parse(UserId));
                if (objFacebookAccount != null)
                {
                    //FacebookAccount fbaccount = null;
                    //foreach (FacebookAccount item in lstfbaccount)
                    //{
                    //    fbaccount = item;
                    //    break;
                    //}

                    FacebookClient fbclient = new FacebookClient(objFacebookAccount.AccessToken);
                    var args = new Dictionary<string, object>();
                    args["message"] = objScheduledMessage.ShareMessage;

                    //var facebookpost = fbclient.Post("/me/feed", args);
                    var facebookpost = "";
                    try
                    {
                        if (objFacebookAccount.Type == "account")
                        {

                            facebookpost = fbclient.Post("/me/feed", args).ToString();


                        }
                        else
                        {
                            facebookpost = fbclient.Post("/" + objFacebookAccount.FbUserId + "/feed", args).ToString();

                        }
                    }
                    catch (Exception ex)
                    {
                        //FacebookAccount ObjFacebookAccount = new FacebookAccount();
                        //objFacebookAccount = new Domain.Socioboard.Domain.FacebookAccount();
                        objFacebookAccountRepository = new FacebookAccountRepository();
                        objFacebookAccount.IsActive = 2;
                        objFacebookAccountRepository.updateFacebookUserStatus(objFacebookAccount);
                        Domain.Socioboard.Domain.SocialProfile objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
                        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
                        //logger.Error(ex.Message);
                        string errormsg = ex.Message;
                        if (errormsg.Contains("access token"))
                        {
                            objSocialProfile.UserId = objFacebookAccount.UserId;
                            objSocialProfile.ProfileId = objFacebookAccount.FbUserId;
                            objSocialProfile.ProfileStatus = 2;
                            objSocialProfilesRepository.updateSocialProfileStatus(objSocialProfile);
                        }
                        Console.WriteLine(ex.Message);
                    }

                    str = "Message post on facebook for Id :" + objFacebookAccount.FbUserId + " and Message: " + objScheduledMessage.ShareMessage;

                    ScheduledMessageRepository schrepo = new ScheduledMessageRepository();
                    ScheduledMessage schmsg = new ScheduledMessage();

                    //objScheduledMessage.Status = true;
                    schmsg.UpdateScheduledMessageByMsgId(Guid.Parse(sscheduledmsgguid));
                }
                else
                {
                    str = "facebook account not found for id" + objScheduledMessage.ProfileId;
                }



            }

            catch (Exception ex)
            {

                throw;
            }
            return str;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetFacebookPages(string code)
        {
            string client_id = ConfigurationManager.AppSettings["ClientId"];
            string redirect_uri = ConfigurationManager.AppSettings["RedirectUrl"];
            string client_secret = ConfigurationManager.AppSettings["ClientSecretKey"];
            List<Domain.Socioboard.Domain.AddFacebookPage> lstAddFacebookPage = new List<Domain.Socioboard.Domain.AddFacebookPage>();
            FacebookClient fb = new FacebookClient();
            string profileId = string.Empty;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("client_id", client_id);
            parameters.Add("redirect_uri", redirect_uri);
            parameters.Add("client_secret", client_secret);
            parameters.Add("code", code);
            JsonObject fbaccess_token = (JsonObject)fb.Get("/oauth/access_token", parameters);
            string accessToken = fbaccess_token["access_token"].ToString();
            dynamic output = null;
            if (accessToken != null)
            {
                fb.AccessToken = accessToken;
                dynamic profile = fb.Get("me");
                output = fb.Get("/me/accounts");
                foreach (var item in output["data"])
                {
                    try
                    {
                        Domain.Socioboard.Domain.AddFacebookPage objAddFacebookPage = new Domain.Socioboard.Domain.AddFacebookPage();
                        objAddFacebookPage.ProfilePageId = item["id"].ToString();
                        objAddFacebookPage.Name = item["name"].ToString();
                        objAddFacebookPage.AccessToken = item["access_token"].ToString();
                        objAddFacebookPage.Email= profile["email"].ToString();
                        lstAddFacebookPage.Add(objAddFacebookPage);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            }
            return new JavaScriptSerializer().Serialize(lstAddFacebookPage);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddFacebookPagesInfo(string userid, string profileId, string accessToken, string groupId,string email)
        {
            string ret = string.Empty;
            try
            {
                FacebookClient fb = new FacebookClient();
                int fancountPage = 0;
                try
                {
                    dynamic fancount = fb.Get("fql", new { q = " SELECT fan_count FROM page WHERE page_id =" + profileId });
                    foreach (var friend in fancount.data)
                    {
                        fancountPage = Convert.ToInt32(friend.fan_count);
                    }
                }
                catch (Exception)
                {
                    fancountPage = 0;
                }
                //string access_Token = accessToken;
                if (accessToken != null)
                {
                    fb.AccessToken = accessToken;
                    dynamic profile = fb.Get("me");
                    // friends = fb.Get("me/friends");
                    //friendscount = Convert.ToInt16(friends["summary"]["total_count"].ToString());
                    #region Add FacebookAccount
                    objFacebookAccount = new Domain.Socioboard.Domain.FacebookAccount();
                    objFacebookAccount.Id = Guid.NewGuid();
                    objFacebookAccount.FbUserId = (Convert.ToString(profile["id"]));
                    objFacebookAccount.FbUserName = (Convert.ToString(profile["name"]));
                    objFacebookAccount.AccessToken = accessToken;
                    objFacebookAccount.Friends = Convert.ToInt16(fancountPage);
                    objFacebookAccount.EmailId = email;
                    objFacebookAccount.Type = "Page";
                    objFacebookAccount.ProfileUrl = (Convert.ToString(profile["link"]));
                    objFacebookAccount.IsActive = 1;
                    objFacebookAccount.UserId = Guid.Parse(userid);
                    if (!objFacebookAccountRepository.checkFacebookUserExists(objFacebookAccount.FbUserId, objFacebookAccount.UserId))
                    {
                        objFacebookAccountRepository.addFacebookUser(objFacebookAccount);
                    }
                    //objFacebookAccountRepository.addFacebookUser(objFacebookAccount);
                    #endregion
                    #region SocialProfile
                    Domain.Socioboard.Domain.SocialProfile objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
                    objSocialProfile.Id = Guid.NewGuid();
                    objSocialProfile.ProfileType = "facebook";
                    objSocialProfile.ProfileId = (Convert.ToString(profile["id"]));
                    objSocialProfile.UserId = Guid.Parse(userid);
                    objSocialProfile.ProfileDate = DateTime.Now;
                    objSocialProfile.ProfileStatus = 1;
                    if (!objSocialProfilesRepository.checkUserProfileExist(objSocialProfile))
                    {
                        objSocialProfilesRepository.addNewProfileForUser(objSocialProfile);
                    }
                    #endregion
                    #region Add TeamMemberProfile
                    Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(groupId));
                    Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
                    objTeamMemberProfile.Id = Guid.NewGuid();
                    objTeamMemberProfile.TeamId = objTeam.Id;
                    objTeamMemberProfile.Status = 1;
                    objTeamMemberProfile.ProfileType = "facebook";
                    objTeamMemberProfile.StatusUpdateDate = DateTime.Now;
                    objTeamMemberProfile.ProfileId = Convert.ToString(profile["id"]);
                    if (!objTeamMemberProfileRepository.checkTeamMemberProfile(objTeamMemberProfile.TeamId, objTeamMemberProfile.ProfileId))
                    {
                        objTeamMemberProfileRepository.addNewTeamMember(objTeamMemberProfile);
                    }
                    
                    #endregion
                    #region Add Facebook Feeds
                    AddFacebookFeeds(userid, fb, profile);
                    #endregion

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetFacebookGroups(string code)
        {
            string client_id = ConfigurationManager.AppSettings["ClientId"];
            string redirect_uri = ConfigurationManager.AppSettings["RedirectUrl"];
            string client_secret = ConfigurationManager.AppSettings["ClientSecretKey"];
            List<Domain.Socioboard.Domain.AddFacebookGroup> lstAddFacebookGroup = new List<Domain.Socioboard.Domain.AddFacebookGroup>();
            FacebookClient fb = new FacebookClient();
            string profileId = string.Empty;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("client_id", client_id);
            parameters.Add("redirect_uri", redirect_uri);
            parameters.Add("client_secret", client_secret);
            parameters.Add("code", code);
            JsonObject fbaccess_token = (JsonObject)fb.Get("/oauth/access_token", parameters);
            string accessToken = fbaccess_token["access_token"].ToString();
            dynamic output = null;
            if (accessToken != null)
            {
                fb.AccessToken = accessToken;
                dynamic profile = fb.Get("me");
                output = fb.Get("/me/groups");
                foreach (var item in output["data"])
                {
                    try
                    {
                        Domain.Socioboard.Domain.AddFacebookGroup objAddFacebookGroup = new Domain.Socioboard.Domain.AddFacebookGroup();
                        objAddFacebookGroup.ProfileGroupId = item["id"].ToString();
                        objAddFacebookGroup.Name = item["name"].ToString();
                    
                        objAddFacebookGroup.AccessToken = accessToken.ToString();
                        objAddFacebookGroup.Email = profile["email"].ToString();
                        lstAddFacebookGroup.Add(objAddFacebookGroup);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            }
            return new JavaScriptSerializer().Serialize(lstAddFacebookGroup);
        }
    
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddFacebookGroupsInfo(string userid, string fbgroupid, string accessToken, string groupId, string email,string fbgroupname)
        {
            string ret = string.Empty;
            try
            {
                FacebookClient fb = new FacebookClient();
               
                if (accessToken != null)
                {
                    fb.AccessToken = accessToken;
                    dynamic profile = fb.Get("me");
                    
                    #region Add FacebookAccount
                    objFacebookAccount = new Domain.Socioboard.Domain.FacebookAccount();
                    objFacebookAccount.Id = Guid.NewGuid();
                    objFacebookAccount.FbUserId = fbgroupid;
                    objFacebookAccount.FbUserName = fbgroupname;
                    objFacebookAccount.AccessToken = accessToken;
                    objFacebookAccount.Friends = 0;
                    objFacebookAccount.EmailId = email;
                    objFacebookAccount.Type = "group";
                    objFacebookAccount.ProfileUrl = (Convert.ToString(profile["link"]));
                    objFacebookAccount.IsActive = 2;
                    objFacebookAccount.UserId = Guid.Parse(userid);
                    if (!objFacebookAccountRepository.checkFacebookUserExists(objFacebookAccount.FbUserId, objFacebookAccount.UserId))
                    {
                        objFacebookAccountRepository.addFacebookUser(objFacebookAccount);
                    }
                    //objFacebookAccountRepository.addFacebookUser(objFacebookAccount);
                    #endregion
                    #region SocialProfile
                    Domain.Socioboard.Domain.SocialProfile objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
                    objSocialProfile.Id = Guid.NewGuid();
                    objSocialProfile.ProfileType = "facebook_group";
                    objSocialProfile.ProfileId = fbgroupid;
                    objSocialProfile.UserId = Guid.Parse(userid);
                    objSocialProfile.ProfileDate = DateTime.Now;
                    objSocialProfile.ProfileStatus = 1;
                    if (!objSocialProfilesRepository.checkUserProfileExist(objSocialProfile))
                    {
                        objSocialProfilesRepository.addNewProfileForUser(objSocialProfile);
                    }
                    #endregion
                    #region Add TeamMemberProfile
                    Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(groupId));
                    Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
                    objTeamMemberProfile.Id = Guid.NewGuid();
                    objTeamMemberProfile.TeamId = objTeam.Id;
                    objTeamMemberProfile.Status = 1;
                    objTeamMemberProfile.ProfileType = "facebook_group";
                    objTeamMemberProfile.StatusUpdateDate = DateTime.Now;
                    objTeamMemberProfile.ProfileId = fbgroupid;
                    if (!objTeamMemberProfileRepository.checkTeamMemberProfile(objTeamMemberProfile.TeamId, objTeamMemberProfile.ProfileId))
                    {
                        objTeamMemberProfileRepository.addNewTeamMember(objTeamMemberProfile);
                    }
                    
                    #endregion
                    #region Add Facebook Feeds
                    AddFacebookFeeds(userid, fb, profile);
                    #endregion

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string CommentOnFbGroupPost(string GpPostid,string comment,string Accesstoken)
        {
            string ret = "";
            try
            {             
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = Accesstoken;
                var args = new Dictionary<string, object>();
                args["message"] = comment;
                var s = fb.Post(GpPostid + "/comments", args);
                ret = "success";
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return ret;
        }





    }
}
