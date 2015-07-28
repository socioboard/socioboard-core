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
using SocioBoard.Model;
using log4net;
using Hammock.Web;
using Api.Socioboard.Model;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        ILog logger = LogManager.GetLogger(typeof(Facebook));
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
        FbPagePostRepository objFbPagePostRepository = new FbPagePostRepository();
        Domain.Socioboard.Domain.FbPagePost objFbPagePost = new Domain.Socioboard.Domain.FbPagePost();
        FbPageCommentRepository objFbPageCommentRepository = new FbPageCommentRepository();
        Domain.Socioboard.Domain.FbPageComment objFbPageComment = new Domain.Socioboard.Domain.FbPageComment();
        FbPagePostCommentLikerRepository objFbPagePostCommentLikerRepository = new FbPagePostCommentLikerRepository();
        Domain.Socioboard.Domain.FbPagePostCommentLiker objFbPagePostCommentLiker = new FbPagePostCommentLiker();
        FbPageLikerRepository objFbPageLikerRepository = new FbPageLikerRepository();
        Domain.Socioboard.Domain.FbPageLiker objFbPageLiker = new Domain.Socioboard.Domain.FbPageLiker();
        InboxMessagesRepository objInboxMessagesRepository = new InboxMessagesRepository();
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
                JsonObject fbaccess_token = null;
                try
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                    fbaccess_token = (JsonObject)fb.Get("/oauth/access_token", parameters);

                }
                catch (Exception ex)
                {

                    try
                    {
                        fbaccess_token = (JsonObject)fb.Get("/oauth/access_token", parameters);
                    }
                    catch (Exception ex1)
                    {
                        return "issue_access_token";
                    }
                }

                string accessToken = fbaccess_token["access_token"].ToString();
                if (accessToken != null)
                {
                    fb.AccessToken = accessToken;
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                    dynamic profile = fb.Get("v2.0/me");
                    dynamic friends = fb.Get("v2.0/me/friends");
                    try
                    {
                        friendscount = Convert.ToInt16(friends["summary"]["total_count"].ToString());
                    }
                    catch (Exception ex)
                    {
                        //try
                        //{
                        //    dynamic frndscount = fb.Get("fql", new { q = "SELECT friend_count FROM user WHERE uid=me()" });

                        //    foreach (var friend in frndscount.data)
                        //    {
                        //        frndscount = friend.friend_count;
                        //    }
                        //    friendscount = Convert.ToInt16(frndscount);
                        //}
                        //catch (Exception ex)
                        //{
                        //    friendscount = 0;
                        //    logger.Error(ex.Message);
                        //    logger.Error(ex.StackTrace);
                        //}

                        friendscount = 0;
                        logger.Error("friendscount >> " + ex.Message);
                        logger.Error("friendscount >> " + ex.StackTrace);
                        logger.Error("friendscount >> " + friends.ToString());
                        logger.Error("friendscount >> " + friends["paging"]["next"].ToString());
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
                        try
                        {
                            objFacebookAccount.EmailId = (Convert.ToString(profile["email"]));
                        }
                        catch (Exception ex)
                        {

                        }
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
                        objTeamMemberProfile.ProfileName = (Convert.ToString(profile["name"]));
                        objTeamMemberProfile.ProfilePicUrl = "http://graph.facebook.com/" + objTeamMemberProfile.ProfileId + "/picture?type=small";
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
                        //AddFacebookMessage(UserId, fb, profile);
                        AddFacebookMessageWithPagination(UserId, fb, profile);
                        #endregion
                        #region Add Facebook Stats
                        AddFacebookStats(UserId, fb, profile);
                        #endregion
                        //getUserNotifications(UserId, fb, profile);
                        ret = "Account Added Successfully";

                        ret = new JavaScriptSerializer().Serialize(objFacebookAccount);

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
                //return new JavaScriptSerializer().Serialize(ret);
                return ret;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddFacebookAccountWithPagination(string code, string UserId, string GroupId)
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
                JsonObject fbaccess_token = null;
                try
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                    fbaccess_token = (JsonObject)fb.Get("/oauth/access_token", parameters);

                }
                catch (Exception ex)
                {

                    try
                    {
                        fbaccess_token = (JsonObject)fb.Get("/oauth/access_token", parameters);
                    }
                    catch (Exception ex1)
                    {
                        return "issue_access_token";
                    }
                }

                string accessToken = fbaccess_token["access_token"].ToString();
                if (accessToken != null)
                {
                    fb.AccessToken = accessToken;
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                    dynamic profile = fb.Get("v2.0/me");
                    dynamic friends = fb.Get("v2.0/me/friends");
                    try
                    {
                        friendscount = Convert.ToInt16(friends["summary"]["total_count"].ToString());
                    }
                    catch (Exception ex)
                    {
                        //try
                        //{
                        //    dynamic frndscount = fb.Get("fql", new { q = "SELECT friend_count FROM user WHERE uid=me()" });

                        //    foreach (var friend in frndscount.data)
                        //    {
                        //        frndscount = friend.friend_count;
                        //    }
                        //    friendscount = Convert.ToInt16(frndscount);
                        //}
                        //catch (Exception ex)
                        //{
                        //    friendscount = 0;
                        //    logger.Error(ex.Message);
                        //    logger.Error(ex.StackTrace);
                        //}

                        friendscount = 0;
                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
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
                        try
                        {
                            objFacebookAccount.EmailId = (Convert.ToString(profile["email"]));
                        }
                        catch (Exception ex)
                        {

                        }
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

                        objTeamMemberProfile.ProfileName = (Convert.ToString(profile["name"]));
                        objTeamMemberProfile.ProfilePicUrl = "http://graph.facebook.com/" + objTeamMemberProfile.ProfileId + "/picture?type=small";

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
                        AddFacebookFeedsWithPagination(UserId, fb, profile);
                        #endregion
                        //#region Add Facebook User Home
                        //AddFacebookUserHome(UserId, fb, profile);
                        //#endregion
                        //#region Add Facebook User Inbox Message
                        //AddFacebookMessage(UserId, fb, profile);
                        //#endregion
                        //#region Add Facebook Stats
                        //AddFacebookStats(UserId, fb, profile);
                        //#endregion

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
                //return new JavaScriptSerializer().Serialize(ret);
                return ret;
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
                Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = new Domain.Socioboard.Domain.FacebookAccount();
                if (objFacebookAccountRepository.checkFacebookUserExists(FbId, Guid.Parse(UserId)))
                {
                    objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(FbId, Guid.Parse(UserId));
                }
                else
                {
                    objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(FbId);
                }


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
                        profile = fb.Get("v2.0/me");

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
                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                        return "Token Expired";
                    }
                    try
                    {
                        friends = fb.Get("v2.0/me/friends");

                    }
                    catch (Exception ex)
                    {
                        string errormssg = ex.Message;
                        if (errormssg.Contains("changed the password"))
                        {
                            UpdateSocialprofileStatus(UserId, FbId);
                        }
                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                        return "Token Expired";
                    }

                    try
                    {
                        friendscount = Convert.ToInt16(friends["summary"]["total_count"].ToString());
                    }
                    catch (Exception ex)
                    {
                        //try
                        //{
                        //    dynamic frndscount = fb.Get("fql", new { q = "SELECT friend_count FROM user WHERE uid=me()" });

                        //    foreach (var friend in frndscount.data)
                        //    {
                        //        frndscount = friend.friend_count;
                        //    }
                        //    friendscount = Convert.ToInt16(frndscount);
                        //}
                        //catch (Exception exx)
                        //{
                        //    friendscount = 0;
                        //    logger.Error(exx.Message);
                        //    logger.Error(exx.StackTrace);
                        //}
                        friendscount = 0;
                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                    }
                    if (objFacebookAccountRepository.checkFacebookUserExists(Convert.ToString(profile["id"]), Guid.Parse(UserId)))
                    {
                        #region Update FacebookAccount
                        UpdateFacebookAccount(objFacebookAccount);
                        #endregion
                        #region UpdateTeammemberprofile
                        Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
                        objTeamMemberProfile.ProfileName = objFacebookAccount.FbUserName;
                        objTeamMemberProfile.ProfilePicUrl = "http://graph.facebook.com/" + objFacebookAccount.FbUserId + "/picture?type=small";
                        objTeamMemberProfile.ProfileId = objFacebookAccount.FbUserId;
                        objTeamMemberProfileRepository.updateTeamMemberbyprofileid(objTeamMemberProfile);
                        #endregion

                        #region UpdateFacebook FanPge
                        Domain.Socioboard.Domain.FacebookFanPage objFacebookFanPage = new Domain.Socioboard.Domain.FacebookFanPage();
                        FacebookFanPageRepository objFacebookFanPageRepository = new FacebookFanPageRepository();
                        objFacebookFanPage.Id = Guid.NewGuid();
                        objFacebookFanPage.UserId = Guid.Parse(UserId);
                        objFacebookFanPage.ProfilePageId = FbId;
                        objFacebookFanPage.FanpageCount = friendscount.ToString();
                        objFacebookFanPage.EntryDate = DateTime.Now;
                        objFacebookFanPageRepository.addFacebookUser(objFacebookFanPage);
                        #endregion

                        #region Add Facebook Feeds
                        //AddFacebookFeeds(UserId, fb, profile);
                        AddFacebookFeedsWithPagination(UserId, fb, profile);
                        #endregion
                        #region Add Facebook User Home
                        AddFacebookUserHome(UserId, fb, profile);
                        #endregion
                        #region Add Facebook User Inbox Message
                        AddFacebookMessageWithPagination(UserId, fb, profile);
                        #endregion
                        #region Add Facebook User Notifications
                       // getUserNotifications(UserId, fb, profile);
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
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
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
            try
            {
                dynamic messages = fb.Get("v2.0/me/inbox");
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
                                objFacebookMessage.MessageDate = DateTime.Parse(message["created_time"].ToString());
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
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.StackTrace);
            }
        }

        //Added by Sumit Gupta [02-02-15]
        private void AddFacebookMessageWithPagination(string UserId, FacebookClient fb, dynamic profile)
        {
            objFacebookMessage = new Domain.Socioboard.Domain.FacebookMessage();
            try
            {
                string nexturl = "v2.0/me/inbox";

                for (int i = 0; i < 50; i++)
                {
                    dynamic messages = fb.Get(nexturl);
                    if (messages != null)
                    {
                        try
                        {
                            nexturl = messages["paging"]["next"].ToString();
                            logger.Error("Facebook.asmx >> AddFacebookMessageWithPagination >> nexturl : " + nexturl);
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            logger.Error(ex.StackTrace);
                        }

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
                                    objFacebookMessage.MessageDate = DateTime.Parse(message["created_time"].ToString());
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
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.StackTrace);
            }
        }


        private void AddFacebookUserHome(string UserId, FacebookClient fb, dynamic profile)
        {

            objFacebookMessage = new Domain.Socioboard.Domain.FacebookMessage();
            try
            {
                dynamic home = fb.Get("v2.0/me/home");
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
                        dynamic likes = fb.Get("v2.0/" + result["id"].ToString() + "/likes?limit=50000000");
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
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }
        private List<Domain.Socioboard.Domain.FacebookFeed> AddFacebookFeeds(string UserId, FacebookClient fb, dynamic profile)
        {
            //List of new Feeds
            List<Domain.Socioboard.Domain.FacebookFeed> lstFacebookFeeds = new List<Domain.Socioboard.Domain.FacebookFeed>();

            try
            {

                Domain.Socioboard.Domain.FacebookFeed objFacebookFeed = new Domain.Socioboard.Domain.FacebookFeed();
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;

                string nexturl = "v2.0/me/feed";//"v2.0/me/posts";


                //for (int i = 0; i < 10; i++)
                {
                    dynamic feeds = fb.Get(nexturl);
                    //dynamic feedss = fb.Get("/338818712957141/posts");
                    //dynamic feedsss = fb.Get("/338818712957141/feed");
                    if (feeds != null)
                    {
                        try
                        {
                            nexturl = feeds["paging"]["next"].ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            logger.Error(ex.StackTrace);
                        }

                        foreach (var result in feeds["data"])
                        {
                            objFacebookFeed = new Domain.Socioboard.Domain.FacebookFeed();

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

                            //Commented as these cause errors on API
                            //Added by Sumit Gupta [31-01-15] to get post Shares/Likes/Comments
                            #region Added by Sumit Gupta [31-01-15]
                            //try
                            //{
                            //    dynamic like = fb.Get("v2.0/" + objFbPagePost.PostId + "/likes?summary=1&limit=0");

                            //    objFbPagePost.Likes = Convert.ToInt32(like["summary"]["total_count"]);

                            //}
                            //catch (Exception ex)
                            //{
                            //    Console.WriteLine(ex.StackTrace);
                            //}

                            //try
                            //{
                            //    dynamic comment = fb.Get("v2.0/" + objFbPagePost.PostId + "/comments?summary=1&limit=0");

                            //    objFbPagePost.Comments = Convert.ToInt32(comment["summary"]["total_count"]);
                            //}
                            //catch (Exception ex)
                            //{
                            //    Console.WriteLine(ex.StackTrace);
                            //}
                            //try
                            //{
                            //    dynamic shares = fb.Get("v2.0/" + objFbPagePost.PostId);
                            //    objFbPagePost.Shares = Convert.ToInt32(shares["shares"]["count"]);
                            //}
                            //catch (Exception ex)
                            //{
                            //    Console.WriteLine(ex.StackTrace);
                            //    logger.Error(ex.StackTrace);
                            //}
                            #endregion

                            //Added by Sumit Gupta [17-02-15]
                            try
                            {
                                objFacebookFeed.Picture = result["picture"].ToString();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                                objFacebookFeed.Picture = "";
                            }

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
                            if (message == null)
                            {
                                message = "";
                            }
                            objFacebookFeed.FeedDescription = message;
                            objFacebookFeed.EntryDate = DateTime.Now;

                            //Check to avoid duplicacy
                            if (!objFacebookFeedRepository.checkFacebookFeedExists(objFacebookFeed.FeedId))
                            {
                                objFacebookFeedRepository.addFacebookFeed(objFacebookFeed);

                                lstFacebookFeeds.Add(objFacebookFeed);
                            }
                        }
                    }
                    else
                    {
                        //break; 
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                Console.Write(ex.StackTrace);
            }

            return lstFacebookFeeds;
        }

        //Added by Avinash Verma & Sumit Gupta [30-01-15]
        private void AddFacebookFeedsWithPagination(string UserId, FacebookClient fb, dynamic profile)
        {
            try
            {
                objFacebookFeed = new Domain.Socioboard.Domain.FacebookFeed();
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;

                string nexturl = "v2.0/me/feed";//"v2.0/me/posts";


                for (int i = 0; i < 50; i++)
                {
                    dynamic feeds = fb.Get(nexturl);
                    //dynamic feedss = fb.Get("/338818712957141/posts");
                    //dynamic feedsss = fb.Get("/338818712957141/feed");
                    if (feeds != null)
                    {
                        try
                        {
                            nexturl = feeds["paging"]["next"].ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            logger.Error(ex.StackTrace);
                        }

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
                            //Added by Sumit Gupta [17-02-15]
                            try
                            {
                                objFacebookFeed.Picture = result["picture"].ToString();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                                objFacebookFeed.Picture = null;
                            }

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

                            //// Edited by Antima[20/12/2014]

                            //SentimentalAnalysis _SentimentalAnalysis = new SentimentalAnalysis();
                            //FeedSentimentalAnalysisRepository _FeedSentimentalAnalysisRepository = new FeedSentimentalAnalysisRepository();

                            //try
                            //{
                            //    if (_FeedSentimentalAnalysisRepository.checkFeedExists(objFacebookFeed.ProfileId.ToString(), Guid.Parse(UserId), objFacebookFeed.Id.ToString()))
                            //    {
                            //        if (!string.IsNullOrEmpty(message))
                            //        {
                            //            string Network = "facebook";
                            //            _SentimentalAnalysis.GetPostSentimentsFromUclassify(Guid.Parse(UserId), objFacebookFeed.ProfileId, objFacebookFeed.MessageId, message, Network);
                            //        }
                            //    }
                            //}
                            //catch (Exception ex)
                            //{
                            //    logger.Error(ex.Message);
                            //    logger.Error(ex.StackTrace);
                            //}

                            if (!objFacebookFeedRepository.checkFacebookFeedExists(objFacebookFeed.FeedId))
                            {
                                objFacebookFeedRepository.addFacebookFeed(objFacebookFeed);
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                Console.Write(ex.StackTrace);
            }
        }

        private void AddFacebookStats(string UserId, FacebookClient fb, dynamic profile)
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                dynamic friendsgenderstats = fb.Get("v2.0/" + profile["id"] + "/friends?fields=gender");
                Domain.Socioboard.Domain.FacebookStats objfbStats = new Domain.Socioboard.Domain.FacebookStats();
                FacebookStatsRepository objFBStatsRepo = new FacebookStatsRepository();
                int malecount = 0;
                int femalecount = 0;
                foreach (var item in friendsgenderstats["data"])
                {
                    if (item["gender"] == "male")
                        malecount++;
                    else if (item["gender"] == "female")
                        femalecount++;
                }
                objfbStats.EntryDate = DateTime.Now;
                objfbStats.FbUserId = profile["id"].ToString();
                objfbStats.FemaleCount = femalecount;
                objfbStats.Id = Guid.NewGuid();
                objfbStats.MaleCount = malecount;
                objfbStats.UserId = Guid.Parse(UserId);


                #region fancount
                int fancountPage = 0;
                try
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                    //dynamic fancount = fb.Get("fql", new { q = " SELECT fan_count FROM page WHERE page_id =" + objfbStats.FbUserId });
                    //foreach (var friend in fancount.data)
                    //{
                    //    fancountPage = Convert.ToInt32(friend.fan_count);
                    //}

                    dynamic friends = fb.Get("v2.0/" + objfbStats.FbUserId + "/friends");
                    fancountPage = Convert.ToInt16(friends["summary"]["total_count"].ToString());

                }
                catch (Exception)
                {
                    fancountPage = 0;
                }
                #endregion

                objfbStats.FanCount = fancountPage;//getfanCount(objfbStats,fb.AccessToken.ToString());
                objFBStatsRepo.addFacebookStats(objfbStats);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.StackTrace);
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
        public string GetAllFbGroupdata(string groupid, string accestoken, string profileid)
        {
            List<FacebookGroupData> _FacebookGroupData = new List<FacebookGroupData>();

            string ret = string.Empty;
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = accestoken;
                dynamic groupsfeeds = fb.Get("v2.0/" + groupid + "/feed");
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
                        dynamic likeddata = item["likes"]["data"];
                        foreach (var item_liked in likeddata)
                        {
                            string i = item_liked["id"].ToString();
                            if (i == profileid)
                            {
                                obj.Userlikes = "liked";
                            }
                        }

                    }
                    catch
                    {
                        obj.Userlikes = "";
                    }

                    //try
                    //{
                    //    obj.Userlikes = item["user_likes"].ToString();

                    //}
                    //catch (Exception ex)
                    //{
                    //    Console.WriteLine(ex.StackTrace);
                    //}
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
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                var s = fb.Post("v2.0/" + msgid + "/likes", null);
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
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                var s = fb.Post("v2.0/" + commentid + "/comments", args);
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
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                object postId = fb.Post("v2.0/" + gid + "/feed", dicFeed);
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
            Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(profileid, Guid.Parse(userid));
            FacebookClient fb = new FacebookClient();

            fb.AccessToken = objFacebookAccount.AccessToken;
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
            var args = new Dictionary<string, object>();
            args["message"] = message;
            args["privacy"] = SetPrivacy("Public", fb);//"{\"description\": \"Public\",\"value\": \"EVERYONE\",\"friends\": \"\",\"networks\": \"\",\"allow\": \"\",\"deny\": \"\"}";
            if (!string.IsNullOrEmpty(imagepath))
            {
                var media = new FacebookMediaObject
                {
                    FileName = "filename",
                    ContentType = "image/jpeg"
                };
                byte[] img = System.IO.File.ReadAllBytes(imagepath);
                media.SetValue(img);
                args["source"] = media;
                ret = fb.Post("v2.0/" + objFacebookAccount.FbUserId + "/photos", args).ToString();
            }
            else
            {
                ret = fb.Post("v2.0/" + objFacebookAccount.FbUserId + "/feed", args).ToString();
                //   ret = fb.Post("/" + objFacebookAccount.FbUserId + "/photos", args).ToString();

            }


            //Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(profileid, Guid.Parse(userid));
            //FacebookClient fb = new FacebookClient();

            //fb.AccessToken = objFacebookAccount.AccessToken;
            //fb.AccessToken = "CAACEdEose0cBANZCpywnaiXtamEZBNRpaZAj3zY9y6e62hTfHQ9oKy5LSc9MiZATY4RV5F19CgLNadYuL1neTaCU3ikhQwB7x6FtFcYOuQlQZBNL1xjQuohzHGlL9xrzHd8lQZBcK4KPwObj2ALLgnbNLmqZAywMs2JL3Ke0Vk36lm8fPzHbeode7Kndw9gbgLC5o5CmhdtzWwWzhxxjljk";
            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
            //ret = fb.Post("/" + objFacebookAccount.FbUserId + "/feed", args).ToString();
            return ret;
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string FacebookComposeMessageForPage(String message, String profileid, string userid, string currentdatetime, string imagepath)
        {
            string ret = "";
            Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(profileid, Guid.Parse(userid));
            FacebookClient fb = new FacebookClient();

            fb.AccessToken = objFacebookAccount.AccessToken;
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
            var args = new Dictionary<string, object>();
            args["message"] = message;
            if (!string.IsNullOrEmpty(imagepath))
            {
                var media = new FacebookMediaObject
                {
                    FileName = "filename",
                    ContentType = "image/jpeg"
                };
                byte[] img = System.IO.File.ReadAllBytes(imagepath);
                media.SetValue(img);
                args["source"] = media;
                ret = fb.Post("v2.0/" + objFacebookAccount.FbUserId + "/photos", args).ToString();
            }
            else
            {
                ret = fb.Post("v2.0/" + objFacebookAccount.FbUserId + "/feed", args).ToString();
                //   ret = fb.Post("/" + objFacebookAccount.FbUserId + "/photos", args).ToString();

            }


            //Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(profileid, Guid.Parse(userid));
            //FacebookClient fb = new FacebookClient();

            //fb.AccessToken = objFacebookAccount.AccessToken;
            //fb.AccessToken = "CAACEdEose0cBANZCpywnaiXtamEZBNRpaZAj3zY9y6e62hTfHQ9oKy5LSc9MiZATY4RV5F19CgLNadYuL1neTaCU3ikhQwB7x6FtFcYOuQlQZBNL1xjQuohzHGlL9xrzHd8lQZBcK4KPwObj2ALLgnbNLmqZAywMs2JL3Ke0Vk36lm8fPzHbeode7Kndw9gbgLC5o5CmhdtzWwWzhxxjljk";
            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
            //ret = fb.Post("/" + objFacebookAccount.FbUserId + "/feed", args).ToString();
            return ret;
        }




        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string FacebookLogin(string code)
        {
            string ret = string.Empty;
            string accessToken = string.Empty;
            Domain.Socioboard.Domain.User objUser = new Domain.Socioboard.Domain.User();
            string client_id = ConfigurationManager.AppSettings["ClientId"];
            string redirect_uri = ConfigurationManager.AppSettings["RedirectUrl"];
            //string redirect_uri = redirecturl;
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
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                JsonObject fbaccess_token = (JsonObject)fb.Get("/oauth/access_token", parameters);
                 accessToken = fbaccess_token["access_token"].ToString();
                if (accessToken != null)
                {
                    string fname = string.Empty;
                    string lname = string.Empty;
                    fb.AccessToken = accessToken;
                    dynamic profile = fb.Get("v2.0/me");
                    objUser.UserName = (Convert.ToString(profile["name"]));
                    objUser.EmailId = (Convert.ToString(profile["email"]));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return (new JavaScriptSerializer().Serialize(objUser)) + "_#_" + accessToken;
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SheduleFacebookMessage(string FacebookId, string UserId, string sscheduledmsgguid)
        {
            string str = string.Empty;
            try
            {
                objScheduledMessage = objScheduledMessageRepository.GetScheduledMessageDetails(Guid.Parse(sscheduledmsgguid));

                if (objFacebookAccountRepository.checkFacebookUserExists(FacebookId, Guid.Parse(UserId)))
                {
                    objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(FacebookId, Guid.Parse(UserId));
                }
                else
                {
                    objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(FacebookId);
                }
                if (objFacebookAccount != null)
                {
                    FacebookClient fbclient = new FacebookClient(objFacebookAccount.AccessToken);
                    var args = new Dictionary<string, object>();
                    args["message"] = objScheduledMessage.ShareMessage;
                    string imagepath = objScheduledMessage.PicUrl;

                    var facebookpost = "";
                    try
                    {
                        

                        if (!string.IsNullOrEmpty(imagepath))
                        {
                            try
                            {
                                var media = new FacebookMediaObject
                                {
                                    FileName = "filename",
                                    ContentType = "image/jpeg"
                                };
                                byte[] img = System.IO.File.ReadAllBytes(imagepath);
                                media.SetValue(img);
                                args["source"] = media;
                                facebookpost = fbclient.Post("v2.0/" + objFacebookAccount.FbUserId + "/photos", args).ToString();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(imagepath + "not Found");
                                facebookpost = fbclient.Post("v2.0/" + objFacebookAccount.FbUserId + "/feed", args).ToString();
                            }
                        }
                        else
                        {
                            facebookpost = fbclient.Post("v2.0/" + objFacebookAccount.FbUserId + "/feed", args).ToString();
                        }
                    }

                    catch (Exception ex)
                    {
                        //FacebookAccount ObjFacebookAccount = new FacebookAccount();
                        //objFacebookAccount = new Domain.Socioboard.Domain.FacebookAccount();
                        objFacebookAccountRepository = new FacebookAccountRepository();
                        //objFacebookAccount.IsActive = 2;
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
                        str = ex.Message;
                    }

                    if (!string.IsNullOrEmpty(facebookpost))
                    {
                        str = "Message post on facebook for Id :" + objFacebookAccount.FbUserId + " and Message: " + objScheduledMessage.ShareMessage;
                        ScheduledMessage schmsg = new ScheduledMessage();
                        schmsg.UpdateScheduledMessageByMsgId(Guid.Parse(sscheduledmsgguid));
                    }
                }
                else
                {
                    str = "facebook account not found for id" + objScheduledMessage.ProfileId;
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            return str;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SheduleFacebookGroupMessage(string FacebookId, string UserId, string sscheduledmsgguid)
        {
            string str = string.Empty;
            try
            {
                objScheduledMessage = objScheduledMessageRepository.GetScheduledMessageDetails(Guid.Parse(sscheduledmsgguid));
                GroupScheduleMessageRepository grpschedulemessagerepo = new GroupScheduleMessageRepository();
                Domain.Socioboard.Domain.GroupScheduleMessage _GroupScheduleMessage = grpschedulemessagerepo.GetScheduleMessageId(objScheduledMessage.Id);
                if (objFacebookAccountRepository.checkFacebookUserExists(FacebookId, Guid.Parse(UserId)))
                {
                    objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(FacebookId, Guid.Parse(UserId));
                }
                else
                {
                    objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(FacebookId);
                }
                if (objFacebookAccount != null)
                {
                    FacebookClient fbclient = new FacebookClient(objFacebookAccount.AccessToken);
                    var args = new Dictionary<string, object>();
                    args["message"] = objScheduledMessage.ShareMessage;
                    string imagepath = objScheduledMessage.PicUrl;

                    var facebookpost = "";
                    try
                    {
                        if (!string.IsNullOrEmpty(imagepath))
                        {
                            var media = new FacebookMediaObject
                            {
                                FileName = "filename",
                                ContentType = "image/jpeg"
                            };
                            byte[] img = System.IO.File.ReadAllBytes(imagepath);
                            media.SetValue(img);
                            args["source"] = media;
                            facebookpost = fbclient.Post("v2.0/" + _GroupScheduleMessage.GroupId + "/photos", args).ToString();
                        }
                        else
                        {
                            facebookpost = fbclient.Post("v2.0/" + _GroupScheduleMessage.GroupId + "/feed", args).ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        objFacebookAccountRepository = new FacebookAccountRepository();
                        objFacebookAccount.IsActive = 2;
                        objFacebookAccountRepository.updateFacebookUserStatus(objFacebookAccount);
                        Domain.Socioboard.Domain.SocialProfile objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
                        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
                        string errormsg = ex.Message;
                        if (errormsg.Contains("access token"))
                        {
                            objSocialProfile.UserId = objFacebookAccount.UserId;
                            objSocialProfile.ProfileId = objFacebookAccount.FbUserId;
                            objSocialProfile.ProfileStatus = 2;
                            objSocialProfilesRepository.updateSocialProfileStatus(objSocialProfile);
                        }
                        Console.WriteLine(ex.Message);
                        str = ex.Message;
                    }

                    if (!string.IsNullOrEmpty(facebookpost))
                    {
                        str = "Message post on facebook for Id :" + objFacebookAccount.FbUserId + " and Message: " + objScheduledMessage.ShareMessage;
                        ScheduledMessage schmsg = new ScheduledMessage();
                        schmsg.UpdateScheduledMessageByMsgId(Guid.Parse(sscheduledmsgguid));
                    }
                }
                else
                {
                    str = "facebook account not found for id" + objScheduledMessage.ProfileId;
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
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
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
            JsonObject fbaccess_token = (JsonObject)fb.Get("/oauth/access_token", parameters);
            string accessToken = fbaccess_token["access_token"].ToString();
            dynamic output = null;
            if (accessToken != null)
            {
                fb.AccessToken = accessToken;
                dynamic profile = fb.Get("v2.0/me");
                output = fb.Get("v2.0/me/accounts");
                foreach (var item in output["data"])
                {
                    try
                    {
                        Domain.Socioboard.Domain.AddFacebookPage objAddFacebookPage = new Domain.Socioboard.Domain.AddFacebookPage();
                        objAddFacebookPage.ProfilePageId = item["id"].ToString();

                        try
                        {
                            //dynamic post = fb.Get(item["id"] + "/likes?summary=1&limit=500");
                            dynamic postlike = fb.Get("v2.0/" + item["id"]);
                            objAddFacebookPage.LikeCount = postlike["likes"].ToString();

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            logger.Error(ex.Message);
                            logger.Error(ex.StackTrace);
                        }
                        if (objAddFacebookPage.LikeCount == null)
                        {
                            objAddFacebookPage.LikeCount = "0";
                        }
                        objAddFacebookPage.Name = item["name"].ToString();
                        objAddFacebookPage.AccessToken = item["access_token"].ToString();
                        try
                        {
                            objAddFacebookPage.Email = profile["email"].ToString();
                        }
                        catch (Exception ex)
                        {
                            objAddFacebookPage.Email = null;
                            Console.WriteLine(ex.StackTrace);
                            logger.Error(ex.StackTrace);
                        }
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
        public string AddFacebookPagesInfo(string userid, string profileId, string accessToken, string groupId, string email)
        {
            string ret = string.Empty;
            try
            {
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = accessToken;
                int fancountPage = 0;
                try
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                    //dynamic fancount = fb.Get("fql", new { q = " SELECT fan_count FROM page WHERE page_id =" + profileId });
                    //foreach (var friend in fancount.data)
                    //{
                    //    fancountPage = Convert.ToInt32(friend.fan_count);
                    //}

                    // dynamic friends = fb.Get("v2.0/me/friends");
                    dynamic friends = fb.Get("v2.0/" + profileId);
                    //fancountPage = Convert.ToInt16(friends["summary"]["total_count"].ToString());
                    fancountPage = Convert.ToInt16(friends["likes"].ToString());

                }
                catch (Exception)
                {
                    fancountPage = 0;
                }

                if (accessToken != null)
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;

                    dynamic profile = fb.Get("v2.0/me");

                    #region Add FacebookAccount
                    objFacebookAccount = new Domain.Socioboard.Domain.FacebookAccount();
                    objFacebookAccount.Id = Guid.NewGuid();
                    objFacebookAccount.FbUserId = (Convert.ToString(profile["id"]));
                    objFacebookAccount.FbUserName = (Convert.ToString(profile["name"]));
                    objFacebookAccount.AccessToken = accessToken;
                    objFacebookAccount.Friends = Convert.ToInt32(fancountPage);
                    objFacebookAccount.EmailId = email;
                    objFacebookAccount.Type = "Page";
                    objFacebookAccount.ProfileUrl = (Convert.ToString(profile["link"]));
                    objFacebookAccount.IsActive = 1;
                    objFacebookAccount.UserId = Guid.Parse(userid);
                    if (!objFacebookAccountRepository.checkFacebookUserExists(objFacebookAccount.FbUserId, objFacebookAccount.UserId))
                    {
                        objFacebookAccountRepository.addFacebookUser(objFacebookAccount);
                    }

                    #endregion
                    #region SocialProfile
                    Domain.Socioboard.Domain.SocialProfile objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
                    objSocialProfile.Id = Guid.NewGuid();
                    objSocialProfile.ProfileType = "facebook_page";
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
                    objTeamMemberProfile.ProfileType = "facebook_page";
                    objTeamMemberProfile.StatusUpdateDate = DateTime.Now;
                    objTeamMemberProfile.ProfileId = Convert.ToString(profile["id"]);

                    objTeamMemberProfile.ProfileName = objFacebookAccount.FbUserName;
                    objTeamMemberProfile.ProfilePicUrl = "http://graph.facebook.com/" + objTeamMemberProfile.ProfileId + "/picture?type=small";

                    if (!objTeamMemberProfileRepository.checkTeamMemberProfile(objTeamMemberProfile.TeamId, objTeamMemberProfile.ProfileId))
                    {
                        objTeamMemberProfileRepository.addNewTeamMember(objTeamMemberProfile);
                    }

                    #endregion
                    #region Add Facebook Feeds
                    AddFacebookFeeds(userid, fb, profile);
                    #endregion
                    #region AddFacebook FanPge
                    Domain.Socioboard.Domain.FacebookFanPage objFacebookFanPage = new Domain.Socioboard.Domain.FacebookFanPage();
                    FacebookFanPageRepository objFacebookFanPageRepository = new FacebookFanPageRepository();
                    objFacebookFanPage.Id = Guid.NewGuid();
                    objFacebookFanPage.UserId = Guid.Parse(userid);
                    objFacebookFanPage.ProfilePageId = (Convert.ToString(profile["id"]));
                    objFacebookFanPage.FanpageCount = fancountPage.ToString();
                    objFacebookFanPage.EntryDate = DateTime.Now;
                    objFacebookFanPageRepository.addFacebookUser(objFacebookFanPage);
                    #endregion

                    AddFbPagePost(userid, accessToken, profileId);
                    getPageConversations(userid, fb, profile);

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
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
            JsonObject fbaccess_token = (JsonObject)fb.Get("/oauth/access_token", parameters);
            string accessToken = fbaccess_token["access_token"].ToString();
            dynamic output = null;
            if (accessToken != null)
            {
                fb.AccessToken = accessToken;
                dynamic profile = fb.Get("v2.0/me");
                output = fb.Get("v2.0/me/groups");
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
        public string AddFacebookGroupsInfo(string userid, string fbgroupid, string accessToken, string groupId, string email, string fbgroupname)
        {
            string ret = string.Empty;
            try
            {
                FacebookClient fb = new FacebookClient();

                if (accessToken != null)
                {
                    fb.AccessToken = accessToken;
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                    dynamic profile = fb.Get("v2.0/me");

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
        public string CommentOnFbGroupPost(string GpPostid, string comment, string Accesstoken)
        {
            string ret = "";
            try
            {
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = Accesstoken;
                var args = new Dictionary<string, object>();
                args["message"] = comment;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                var s = fb.Post("v2.0/" + GpPostid + "/comments", args);
                ret = "success";
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddFbPagePost(string userid, string accesstoken, string profileid)
        {
            logger.Error("AddFbPagePost");
            logger.Error(userid + ", " + accesstoken + " , " + profileid);
            string ret = string.Empty;
            try
            {
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = accesstoken;
                dynamic post = null;
                try
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                    post = fb.Get("v2.0/" + profileid + "/posts?limit=10");
                }
                catch (Exception ex)
                {
                    logger.Error("profileid +posts?limit=10");
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
                //dynamic post1 = fb.Get("me/posts");

                foreach (var item in post["data"])
                {
                    objFbPagePost = new Domain.Socioboard.Domain.FbPagePost();
                    objFbPagePost.Id = Guid.NewGuid();
                    objFbPagePost.UserId = Guid.Parse(userid);
                    objFbPagePost.PageId = profileid;

                    try
                    {
                        objFbPagePost.PostId = item["object_id"].ToString();
                    }
                    catch { };
                    if (string.IsNullOrEmpty(objFbPagePost.PostId))
                    {
                        try
                        {
                            string pstid = item["id"];
                            objFbPagePost.PostId = pstid.Split('_')[1];
                        }
                        catch { };
                    }



                    objFbPagePost.PostDate = Convert.ToDateTime(item["created_time"]);
                    objFbPagePost.EntryDate = DateTime.Now;
                    try
                    {
                        objFbPagePost.Post = item["message"].ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPagePost.PictureUrl = item["picture"].ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objFbPagePost.LinkUrl = item["link"].ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPagePost.IconUrl = item["icon"].ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPagePost.StatusType = item["status_type"].ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPagePost.Type = item["type"].ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPagePost.FromId = item["from"]["id"];

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPagePost.FromName = item["from"]["name"];

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        dynamic like = fb.Get("v2.0/" + objFbPagePost.PostId + "/likes?summary=1&limit=0");

                        objFbPagePost.Likes = Convert.ToInt32(like["summary"]["total_count"]);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        dynamic comment = fb.Get("v2.0/" + objFbPagePost.PostId + "/comments?summary=1&limit=0");

                        objFbPagePost.Comments = Convert.ToInt32(comment["summary"]["total_count"]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        //dynamic shares = fb.Get("v2.0/"+objFbPagePost.PostId);
                        //objFbPagePost.Shares = Convert.ToInt32(shares["shares"]["count"]);
                        objFbPagePost.Shares = Convert.ToInt32(item["shares"]["count"]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        logger.Error(ex.StackTrace);
                    }
                    try
                    {
                        if (!objFbPagePostRepository.IsPostExist(objFbPagePost))
                        {
                            objFbPagePostRepository.addFbPagePost(objFbPagePost);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        logger.Error(ex.StackTrace);
                    }

                    try
                    {
                        AddFbPagePostComments(objFbPagePost.PostId, accesstoken, userid);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        AddFbPagePostLiker(objFbPagePost.PostId, accesstoken, userid);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                        Console.WriteLine(ex.StackTrace);
                    }


                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.StackTrace);
            }
            return ret;
        }

        public string AddFbPagePostComments(string postid, string accesstoken, string userid)
        {
            string ret = string.Empty;
            try
            {
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = accesstoken;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                dynamic post = fb.Get("v2.0/" + postid + "/comments?limit=10");
                foreach (var item in post["data"])
                {
                    objFbPageComment.Id = Guid.NewGuid();
                    objFbPageComment.UserId = Guid.Parse(userid);
                    objFbPageComment.EntryDate = DateTime.Now;
                    objFbPageComment.PostId = postid;

                    try
                    {
                        objFbPageComment.CommentId = item["id"];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPageComment.Comment = item["message"];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPageComment.Likes = Convert.ToInt32(item["like_count"]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPageComment.UserLikes = Convert.ToInt32(item["user_likes"]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPageComment.Commentdate = Convert.ToDateTime(item["created_time"]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPageComment.FromName = item["from"]["name"];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPageComment.FromId = item["from"]["id"];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPageComment.PictureUrl = item["id"];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }


                    try
                    {
                        objFbPageCommentRepository.addFbPageComment(objFbPageComment);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        AddFbPagePostCommentsLikes(objFbPageComment.CommentId, accesstoken, userid);
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
            }
            return ret;
        }

        public string AddFbPagePostCommentsLikes(string commentid, string accesstoken, string userid)
        {
            string ret = string.Empty;
            try
            {
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = accesstoken;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                dynamic post = fb.Get("v2.0/" + commentid + "/likes?limit=20");
                foreach (var item in post["data"])
                {
                    objFbPagePostCommentLiker.Id = Guid.NewGuid();
                    objFbPagePostCommentLiker.UserId = Guid.Parse(userid);
                    objFbPagePostCommentLiker.CommentId = commentid;
                    try
                    {
                        objFbPagePostCommentLiker.FromId = item["id"];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPagePostCommentLiker.FromName = item["name"];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    objFbPagePostCommentLikerRepository.addFbPagePostCommentLiker(objFbPagePostCommentLiker);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return ret;
        }

        public string AddFbPagePostLiker(string postid, string accesstoken, string userid)
        {
            string ret = string.Empty;
            try
            {
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = accesstoken;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                dynamic post = fb.Get("v2.0/" + postid + "/likes?limit=20");
                foreach (var item in post["data"])
                {
                    objFbPageLiker.Id = Guid.NewGuid();
                    objFbPageLiker.UserId = Guid.Parse(userid);
                    objFbPageLiker.PostId = postid;
                    try
                    {
                        objFbPageLiker.FromId = item["id"];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPageLiker.FromName = item["name"];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    objFbPageLikerRepository.addFbPageLiker(objFbPageLiker);
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
        public string GetAllFbPagePostDetails(string pageid, string userid)
        {
            try
            {
                List<Domain.Socioboard.Domain.FbPagePost> _FbPagePost = objFbPagePostRepository.getAllPost(pageid, Guid.Parse(userid));
                return new JavaScriptSerializer().Serialize(_FbPagePost);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddFacebookPagesByUrl(string userid, string profileId, string groupId, string name)
        {
            logger.Error("AddFacebookPagesByUrllllllll");
            logger.Error(userid + ", " + profileId + ", " + groupId + ", " + name);
            string ret = string.Empty;
            FacebookAccount _FacebookAccount = new FacebookAccount();
            // string token = _FacebookAccount.getFbToken();
            string token = "CAAKYvwDVmnUBACyqUsvADWoAfBYTxi0kbz2gcw0sDWbBVJCXmIUG6rGez4BFSCE4hKV8eNE86eCD2iOwEWADvYuNlYupZCL4WUAGhFmRIZA6nTkdUOFeiUVHuri571QxhZA3YfSk5YkjhYy81pYtPj9FNM2mENtjCWRr5tN9zWZAKpUkw3gzsXRuEH9ZBTBwZD";
            try
            {
                #region fancount
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = token;
                int fancountPage = 0;
                try
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                    //dynamic fancount = fb.Get("fql", new { q = " SELECT fan_count FROM page WHERE page_id =" + profileId });
                    //foreach (var friend in fancount.data)
                    //{
                    //    fancountPage = Convert.ToInt32(friend.fan_count);
                    //}

                    //dynamic friends = fb.Get("v2.0/me/friends");
                    //fancountPage = Convert.ToInt16(friends["summary"]["total_count"].ToString());
                    dynamic friends = fb.Get("v2.0/" + profileId);
                    fancountPage = Convert.ToInt32(friends["likes"].ToString());

                }
                catch (Exception)
                {
                    fancountPage = 0;
                    fb.AccessToken = "CAAKYvwDVmnUBAFvCcZCQDL53q82jfR5mvgF2whNsFHgR4NmeSSUeRVpdEUpcVVgK1ERs2GZCNhJAwRHtq6MEWiRtBQnxBmZAML6dnwgpsCbjUmyT7ws6EKZBxuWbxhJqjeNCsxhac00b3L9Bf7LLlYa3PG94Uouj7vXZAZC6djZCme5BuszE3vibNFLKQqaLcgZD";
                    try
                    {
                        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                        //dynamic fancount = fb.Get("fql", new { q = " SELECT fan_count FROM page WHERE page_id =" + profileId });
                        //foreach (var friend in fancount.data)
                        //{
                        //    fancountPage = Convert.ToInt32(friend.fan_count);
                        //}
                        //dynamic friends = fb.Get("v2.0/me/friends");
                        //fancountPage = Convert.ToInt16(friends["summary"]["total_count"].ToString());
                        dynamic friends = fb.Get("v2.0/" + profileId);
                        fancountPage = Convert.ToInt32(friends["likes"].ToString());
                    }
                    catch (Exception ex)
                    {
                        logger.Error("fancount : " + ex.Message);
                    }
                }
                #endregion


                #region Add FacebookAccount
                objFacebookAccount = new Domain.Socioboard.Domain.FacebookAccount();
                objFacebookAccount.Id = Guid.NewGuid();
                objFacebookAccount.FbUserId = profileId;
                objFacebookAccount.FbUserName = name;
                objFacebookAccount.AccessToken = "";
                objFacebookAccount.Friends = Convert.ToInt32(fancountPage);
                objFacebookAccount.EmailId = "";
                objFacebookAccount.Type = "Page";
                objFacebookAccount.ProfileUrl = "";
                objFacebookAccount.IsActive = 1;
                objFacebookAccount.UserId = Guid.Parse(userid);
                if (!objFacebookAccountRepository.checkFacebookUserExists(objFacebookAccount.FbUserId, objFacebookAccount.UserId))
                {
                    objFacebookAccountRepository.addFacebookUser(objFacebookAccount);
                }

                #endregion
                #region SocialProfile
                Domain.Socioboard.Domain.SocialProfile objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
                objSocialProfile.Id = Guid.NewGuid();
                objSocialProfile.ProfileType = "facebook_page";
                objSocialProfile.ProfileId = profileId;
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
                objTeamMemberProfile.ProfileType = "facebook_page";
                objTeamMemberProfile.StatusUpdateDate = DateTime.Now;
                objTeamMemberProfile.ProfileId = profileId;
                if (!objTeamMemberProfileRepository.checkTeamMemberProfile(objTeamMemberProfile.TeamId, objTeamMemberProfile.ProfileId))
                {
                    objTeamMemberProfileRepository.addNewTeamMember(objTeamMemberProfile);
                }

                #endregion

                try
                {
                    logger.Error(" Token:" + token);
                    if (token != null)
                    {
                        FacebookClient _FacebookClient = new FacebookClient();
                        _FacebookClient.AccessToken = token;
                        dynamic profile = null;

                        try
                        {
                            profile = fb.Get("v2.0/" + profileId);
                            logger.Error("AddFacebookPagesByUrl Token 1");
                        }
                        catch (Exception ex)
                        {
                            logger.Error("fb.Get(profileId)");
                            logger.Error(ex.StackTrace);
                            Console.WriteLine(ex.StackTrace);
                            try
                            {
                                fb.AccessToken = "CAAKYvwDVmnUBAFvCcZCQDL53q82jfR5mvgF2whNsFHgR4NmeSSUeRVpdEUpcVVgK1ERs2GZCNhJAwRHtq6MEWiRtBQnxBmZAML6dnwgpsCbjUmyT7ws6EKZBxuWbxhJqjeNCsxhac00b3L9Bf7LLlYa3PG94Uouj7vXZAZC6djZCme5BuszE3vibNFLKQqaLcgZD";
                                profile = fb.Get("v2.0/" + profileId);
                                logger.Error("AddFacebookPagesByUrl Token 2");
                            }
                            catch (Exception ex2)
                            {
                                try
                                {
                                    fb.AccessToken = "CAAKYvwDVmnUBAAR2O9hxFkHzfNG8H6KbQLaiGFMRshJkbttdzhDeprklcb1yaV0rwtC7N8Xz1rsL1cykiRv2ouXtBUFxvOZCNnpFELnQGFV8jGUWjm1GYsZA40IKAORLGoAcSaa2lJkuuSoLBksB8LFPHI4cqW7VVqxgDwZCRwObxqR4Qp9QEDHxa7j1yoZD";
                                    profile = fb.Get("v2.0/" + profileId);
                                    logger.Error("AddFacebookPagesByUrl Token 3");
                                }
                                catch (Exception ex3)
                                {
                                    try
                                    {
                                        fb.AccessToken = "CAAKYvwDVmnUBAFtZB8pvVrqYQonmq7MD90oNdoipDc0Te4onP2XlbZAYT4bzOZAKTr8jdhw0P1PclgLOtVxJ9g2qx4vxZAzh2CXqXAZBZAZBwkgWIVjc2B4rcXAp6O5B3gXqd8Ko5ITL9VCZCMOkMZCPc1hBsp0n8zgPt6e3Dd0vaodPBS8nMz7RD";
                                        profile = fb.Get("v2.0/" + profileId);
                                        logger.Error("AddFacebookPagesByUrl Token 4");
                                    }
                                    catch (Exception ex4)
                                    {
                                        try
                                        {
                                            fb.AccessToken = "CAAKYvwDVmnUBALvjTAKIrVKnL719aVDB7BmMRn7e08ySJQwHYtLDZBBjx5yBZBaMeJ04lIT8bCzX2A685YLXR9d8PukZCBZA2LiwZAmj6qhMZC8F0od7NBircdMZAOZAD1xukXDhd24RQRvVk9GyJNRmmGTiZAhMJzXBVczH3TlYb37qi8FRXfTGDRTZAyxjyYSt8ZD";
                                            profile = fb.Get("v2.0/" + profileId);
                                            logger.Error("AddFacebookPagesByUrl Token 5");
                                        }
                                        catch (Exception ex5)
                                        {
                                            logger.Error("Finally :" + fb.AccessToken);
                                            logger.Error(ex5.Message);
                                            logger.Error(ex5.Message);
                                        }
                                    }
                                }
                            }
                        }

                        try
                        {
                            //Edited by Sumit Gupta [10/27/2014]
                            //AddFacebookStats(userid, _FacebookClient, profile);
                            AddFacebookStats(userid, fb, profile);
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.StackTrace);
                            Console.WriteLine(ex.StackTrace);
                        }

                        //Edited by Sumit Gupta [10/29/2014]
                        AddFbPagePost(userid, fb.AccessToken, profileId); // AddFbPagePost(userid, token, profileId);


                    }
                }
                catch (Exception ex)
                {
                    logger.Error("dynamic profile");
                    logger.Error(ex.StackTrace);
                    Console.WriteLine(ex.StackTrace);
                }



            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.StackTrace);
            }
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetFbPageDetails(string url)
        {
            try
            {
                FacebookClient fb = new FacebookClient();
                FacebookAccount _FacebookAccount = new FacebookAccount();
                string token = _FacebookAccount.getFbToken();
                fb.AccessToken = "CAAKYvwDVmnUBACyqUsvADWoAfBYTxi0kbz2gcw0sDWbBVJCXmIUG6rGez4BFSCE4hKV8eNE86eCD2iOwEWADvYuNlYupZCL4WUAGhFmRIZA6nTkdUOFeiUVHuri571QxhZA3YfSk5YkjhYy81pYtPj9FNM2mENtjCWRr5tN9zWZAKpUkw3gzsXRuEH9ZBTBwZD";
                //fb.AccessToken = token;
                // logger.Error(" GetFbPageDetails: " + fb.AccessToken);
                dynamic pageinfo = null;
                try
                {
                    pageinfo = fb.Get(url);
                    logger.Error("Token 1");
                }
                catch (Exception ex1)
                {
                    try
                    {
                        fb.AccessToken = "CAAKYvwDVmnUBAFvCcZCQDL53q82jfR5mvgF2whNsFHgR4NmeSSUeRVpdEUpcVVgK1ERs2GZCNhJAwRHtq6MEWiRtBQnxBmZAML6dnwgpsCbjUmyT7ws6EKZBxuWbxhJqjeNCsxhac00b3L9Bf7LLlYa3PG94Uouj7vXZAZC6djZCme5BuszE3vibNFLKQqaLcgZD";
                        pageinfo = fb.Get(url);
                        logger.Error("Token 2");
                    }
                    catch (Exception ex2)
                    {
                        try
                        {
                            fb.AccessToken = "CAAKYvwDVmnUBAAR2O9hxFkHzfNG8H6KbQLaiGFMRshJkbttdzhDeprklcb1yaV0rwtC7N8Xz1rsL1cykiRv2ouXtBUFxvOZCNnpFELnQGFV8jGUWjm1GYsZA40IKAORLGoAcSaa2lJkuuSoLBksB8LFPHI4cqW7VVqxgDwZCRwObxqR4Qp9QEDHxa7j1yoZD";
                            pageinfo = fb.Get(url);
                            logger.Error("Token 3");
                        }
                        catch (Exception ex3)
                        {
                            try
                            {
                                fb.AccessToken = "CAAKYvwDVmnUBAFtZB8pvVrqYQonmq7MD90oNdoipDc0Te4onP2XlbZAYT4bzOZAKTr8jdhw0P1PclgLOtVxJ9g2qx4vxZAzh2CXqXAZBZAZBwkgWIVjc2B4rcXAp6O5B3gXqd8Ko5ITL9VCZCMOkMZCPc1hBsp0n8zgPt6e3Dd0vaodPBS8nMz7RD";
                                pageinfo = fb.Get(url);
                                logger.Error("Token 4");
                            }
                            catch (Exception ex4)
                            {
                                try
                                {
                                    fb.AccessToken = "CAAKYvwDVmnUBALvjTAKIrVKnL719aVDB7BmMRn7e08ySJQwHYtLDZBBjx5yBZBaMeJ04lIT8bCzX2A685YLXR9d8PukZCBZA2LiwZAmj6qhMZC8F0od7NBircdMZAOZAD1xukXDhd24RQRvVk9GyJNRmmGTiZAhMJzXBVczH3TlYb37qi8FRXfTGDRTZAyxjyYSt8ZD";
                                    pageinfo = fb.Get(url);
                                    logger.Error("Token 5");
                                }
                                catch (Exception ex5)
                                {
                                    logger.Error("Finally :" + fb.AccessToken);
                                    logger.Error(ex5.Message);
                                    logger.Error(ex5.Message);
                                }
                            }
                        }
                    }

                }

                Domain.Socioboard.Domain.AddFacebookPage objAddFacebookPage = new Domain.Socioboard.Domain.AddFacebookPage();
                objAddFacebookPage.ProfilePageId = pageinfo["id"];
                logger.Error(" pageinfo: " + pageinfo["id"]);
                return new JavaScriptSerializer().Serialize(objAddFacebookPage);
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        //Get all Post from page 

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string FacebookProfileDetails(string UserId, string ProfileId)
        {
            objFacebookAccount = new Domain.Socioboard.Domain.FacebookAccount();
            Domain.Socioboard.Domain.FacebookAccount FacebookAccount = new Domain.Socioboard.Domain.FacebookAccount();
            //objFacebookAccountRepository.getFacebookAccountDetailsById(ProfileId, Guid.Parse(UserId));
            if (objFacebookAccountRepository.checkFacebookUserExists(ProfileId, Guid.Parse(UserId)))
            {
                FacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(ProfileId, Guid.Parse(UserId));
            }
            else
            {
                FacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(ProfileId);
            }

            //long friendscount = 0;
            //FacebookClient fb = new FacebookClient();
            //fb.AccessToken = FacebookAccount.AccessToken;
            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
            //dynamic profile = fb.Get("me");
            //dynamic friends = fb.Get("me/friends");
            //try
            //{
            //    friendscount = Convert.ToInt16(friends["summary"]["total_count"].ToString());
            //}
            //catch (Exception)
            //{
            //    friendscount = 0;
            //}
            //objFacebookAccount = new Domain.Socioboard.Domain.FacebookAccount();
            //objFacebookAccount.FbUserId = (Convert.ToString(profile["id"]));
            //objFacebookAccount.FbUserName = (Convert.ToString(profile["name"]));
            //objFacebookAccount.Friends = Convert.ToInt16(friendscount);
            //objFacebookAccount.EmailId = (Convert.ToString(profile["email"]));
            //objFacebookAccount.ProfileUrl = (Convert.ToString(profile["link"]));

            return new JavaScriptSerializer().Serialize(FacebookAccount);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddAllFbPagePost(string userid, string accesstoken, string profileid)
        {
            logger.Error("AddFbPagePost");
            logger.Error(userid + ", " + accesstoken + " , " + profileid);
            string ret = string.Empty;
            Api.Socioboard.Services.FacebookAccount _FacebookAccount = new FacebookAccount();
            Domain.Socioboard.Domain.FacebookAccount _facebookAccount = new Domain.Socioboard.Domain.FacebookAccount();

            try
            {
                string _nextPageDataUrl = string.Empty;
                FacebookClient fb = new FacebookClient();

                if (string.IsNullOrEmpty(accesstoken))
                {
                    try
                    {
                        _facebookAccount = (Domain.Socioboard.Domain.FacebookAccount)(new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(_FacebookAccount.getFacebookAccountDetailsById(userid, profileid), typeof(Domain.Socioboard.Domain.FacebookAccount)));

                        _facebookAccount = new Domain.Socioboard.Domain.FacebookAccount();
                        Api.Socioboard.Services.FacebookAccountRepository _FacebookAccountRepository = new FacebookAccountRepository();

                        System.Collections.ArrayList lstFacebookAccounts = _FacebookAccountRepository.getAllFacebookAccounts();

                        Random _random = new Random();
                        var rnum = _random.Next(0, lstFacebookAccounts.Count - 1);
                        _facebookAccount = (Domain.Socioboard.Domain.FacebookAccount)lstFacebookAccounts[rnum];
                        fb.AccessToken = _facebookAccount.AccessToken;
                    }
                    catch { };
                }
                else
                    fb.AccessToken = accesstoken;


                dynamic post = null;
                try
                {
                    post = fb.Get("v2.0/" + profileid + "/posts");
                }
                catch (Exception ex)
                {
                    logger.Error("profileid +posts");
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            //dynamic post1 = fb.Get("me/posts");
            _NextPageDataUrl:

                if (!string.IsNullOrEmpty(_nextPageDataUrl))
                {
                    fb = new FacebookClient();
                    post = fb.Get(_nextPageDataUrl);
                    _nextPageDataUrl = string.Empty;
                }
                foreach (var item in post["data"])
                {
                    #region
                    objFbPagePost.Id = Guid.NewGuid();
                    objFbPagePost.UserId = Guid.Parse(userid);
                    objFbPagePost.PageId = profileid;
                    objFbPagePost.PostId = item["id"].ToString();

                    objFbPagePost.PostDate = Convert.ToDateTime(item["created_time"]);
                    objFbPagePost.EntryDate = DateTime.Now;
                    try
                    {
                        objFbPagePost.Post = item["message"].ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPagePost.PictureUrl = item["picture"].ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objFbPagePost.LinkUrl = item["link"].ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPagePost.IconUrl = item["icon"].ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPagePost.StatusType = item["status_type"].ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPagePost.Type = item["type"].ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPagePost.FromId = item["from"]["id"];

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPagePost.FromName = item["from"]["name"];

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        dynamic like = fb.Get("v2.0/" + objFbPagePost.PostId + "/likes?summary=1&limit=0");

                        objFbPagePost.Likes = Convert.ToInt32(like["summary"]["total_count"]);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        dynamic comment = fb.Get("v2.0/" + objFbPagePost.PostId + "/comments?summary=1&limit=0");

                        objFbPagePost.Comments = Convert.ToInt32(comment["summary"]["total_count"]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        dynamic shares = fb.Get("v2.0/" + objFbPagePost.PostId);
                        objFbPagePost.Shares = Convert.ToInt32(shares["shares"]["count"]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        logger.Error(ex.StackTrace);
                    }
                    try
                    {
                        if (!objFbPagePostRepository.IsPostExist(objFbPagePost))
                            objFbPagePostRepository.addFbPagePost(objFbPagePost);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        logger.Error(ex.StackTrace);
                    }

                    try
                    {
                        AddAllFbPagePostComments(objFbPagePost.PostId, accesstoken, userid);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        AddAllFbPagePostLiker(objFbPagePost.PostId, accesstoken, userid);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                        Console.WriteLine(ex.StackTrace);
                    }
                    #endregion

                }


                try
                {
                    _nextPageDataUrl = post["paging"]["next"];
                    if (!string.IsNullOrEmpty(_nextPageDataUrl))
                        goto _NextPageDataUrl;

                }
                catch { };


            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.StackTrace);
            }
            return ret;
        }

        public string AddAllFbPagePostComments(string postid, string accesstoken, string userid)
        {
            string ret = string.Empty;
            try
            {
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = accesstoken;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                dynamic post = fb.Get("v2.0/" + postid + "/comments");

                string _nextPageDataUrl = string.Empty;

            _NextPageDataUrl:

                if (!string.IsNullOrEmpty(_nextPageDataUrl))
                {
                    fb = new FacebookClient();
                    post = fb.Get(_nextPageDataUrl);
                    _nextPageDataUrl = string.Empty;
                }


                foreach (var item in post["data"])
                {
                    #region

                    objFbPageComment.Id = Guid.NewGuid();
                    objFbPageComment.UserId = Guid.Parse(userid);
                    objFbPageComment.EntryDate = DateTime.Now;
                    objFbPageComment.PostId = postid;

                    try
                    {
                        objFbPageComment.CommentId = item["id"];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPageComment.Comment = item["message"];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPageComment.Likes = Convert.ToInt32(item["like_count"]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPageComment.UserLikes = Convert.ToInt32(item["user_likes"]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPageComment.Commentdate = Convert.ToDateTime(item["created_time"]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPageComment.FromName = item["from"]["name"];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPageComment.FromId = item["from"]["id"];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPageComment.PictureUrl = item["id"];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }


                    try
                    {
                        if (!objFbPageCommentRepository.IsPostCommentExist(objFbPageComment))
                            objFbPageCommentRepository.addFbPageComment(objFbPageComment);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        AddAllFbPagePostCommentsLikes(objFbPageComment.CommentId, accesstoken, userid);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    #endregion
                }

                try
                {
                    _nextPageDataUrl = post["paging"]["next"];
                    if (!string.IsNullOrEmpty(_nextPageDataUrl))
                        goto _NextPageDataUrl;

                }
                catch { };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return ret;
        }

        public string AddAllFbPagePostCommentsLikes(string commentid, string accesstoken, string userid)
        {
            string ret = string.Empty;
            try
            {
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = accesstoken;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                dynamic post = fb.Get("v2.0/" + commentid + "/likes");


                string _nextPageDataUrl = string.Empty;

            _NextPageDataUrl:

                if (!string.IsNullOrEmpty(_nextPageDataUrl))
                {
                    fb = new FacebookClient();
                    post = fb.Get(_nextPageDataUrl);
                    _nextPageDataUrl = string.Empty;
                }


                foreach (var item in post["data"])
                {
                    #region
                    objFbPagePostCommentLiker.Id = Guid.NewGuid();
                    objFbPagePostCommentLiker.UserId = Guid.Parse(userid);
                    objFbPagePostCommentLiker.CommentId = commentid;
                    try
                    {
                        objFbPagePostCommentLiker.FromId = item["id"];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPagePostCommentLiker.FromName = item["name"];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    if (!objFbPagePostCommentLikerRepository.IsFbPagePostCommentLikerExist(objFbPagePostCommentLiker))
                        objFbPagePostCommentLikerRepository.addFbPagePostCommentLiker(objFbPagePostCommentLiker);



                    #endregion
                }

                try
                {
                    _nextPageDataUrl = post["paging"]["next"];
                    if (!string.IsNullOrEmpty(_nextPageDataUrl))
                        goto _NextPageDataUrl;

                }
                catch { };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return ret;
        }

        public string AddAllFbPagePostLiker(string postid, string accesstoken, string userid)
        {
            string ret = string.Empty;
            try
            {
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = accesstoken;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                dynamic post = fb.Get("v2.0/" + postid + "/likes");


                string _nextPageDataUrl = string.Empty;

            _NextPageDataUrl:

                if (!string.IsNullOrEmpty(_nextPageDataUrl))
                {
                    fb = new FacebookClient();
                    post = fb.Get(_nextPageDataUrl);
                    _nextPageDataUrl = string.Empty;
                }

                foreach (var item in post["data"])
                {
                    #region
                    objFbPageLiker.Id = Guid.NewGuid();
                    objFbPageLiker.UserId = Guid.Parse(userid);
                    objFbPageLiker.PostId = postid;
                    try
                    {
                        objFbPageLiker.FromId = item["id"];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objFbPageLiker.FromName = item["name"];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    if (!objFbPageLikerRepository.IsLikeByPostExist(objFbPageLiker))
                        objFbPageLikerRepository.addFbPageLiker(objFbPageLiker);

                    #endregion

                }
                try
                {
                    _nextPageDataUrl = post["paging"]["next"];
                    if (!string.IsNullOrEmpty(_nextPageDataUrl))
                        goto _NextPageDataUrl;

                }
                catch { };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateFacebookAccountByAdmin(string ObjFacebook)
        {
            Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = (Domain.Socioboard.Domain.FacebookAccount)(new JavaScriptSerializer().Deserialize(ObjFacebook, typeof(Domain.Socioboard.Domain.FacebookAccount)));
            try
            {
                objFacebookAccountRepository.updateFacebookUser(objFacebookAccount);
                return new JavaScriptSerializer().Serialize("Update Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Something went Wrong");
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string LikeFBGroupPost(string postid, string accesstoken, string Isliked)
        {
            string ret = "";
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
            FacebookClient fb = new FacebookClient();
            fb.AccessToken = accesstoken;
            try
            {
                if (Isliked == "liked")
                {
                    dynamic unlike = fb.Delete("v2.0/" + postid + "/likes", null);
                    ret = "unlike";
                }
                else
                {
                    dynamic like = fb.Post("v2.0/" + postid + "/likes", null);
                    ret = "like";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                ret = "Somthing Went Wrong";
            }

            return ret;
        }

        // Edited by Antima[20/12/2014]

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string TicketFacebokReply(String message, String profileid, string commentid)
        {
            string ret = "";
            try
            {
                Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(profileid);
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = objFacebookAccount.AccessToken;
                var args = new Dictionary<string, object>();
                args["message"] = message;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                var s = fb.Post("v2.0/" + commentid + "/comments", args);
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return ret;
        }


        //Added By Sumit Gupta[15-02-15]
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddNewFacebookFeeds(string FbId, string UserId)
        {
            string ret = string.Empty;
            long friendscount = 0;

            List<Domain.Socioboard.Domain.FacebookFeed> lstNewFacebookFeeds = new List<Domain.Socioboard.Domain.FacebookFeed>();

            try
            {
                Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = new Domain.Socioboard.Domain.FacebookAccount();
                if (objFacebookAccountRepository.checkFacebookUserExists(FbId, Guid.Parse(UserId)))
                {
                    objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(FbId, Guid.Parse(UserId));
                }
                else
                {
                    objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(FbId);
                }


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
                        profile = fb.Get("v2.0/me");

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
                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                        return "Token Expired";
                    }
                    //try
                    //{
                    //    friends = fb.Get("v2.0/me/friends");

                    //}
                    //catch (Exception ex)
                    //{
                    //    string errormssg = ex.Message;
                    //    if (errormssg.Contains("changed the password"))
                    //    {
                    //        UpdateSocialprofileStatus(UserId, FbId);
                    //    }
                    //    logger.Error(ex.Message);
                    //    logger.Error(ex.StackTrace);
                    //    return "Token Expired";
                    //}

                    //try
                    //{
                    //    friendscount = Convert.ToInt16(friends["summary"]["total_count"].ToString());
                    //}
                    //catch (Exception ex)
                    //{
                    //    //try
                    //    //{
                    //    //    dynamic frndscount = fb.Get("fql", new { q = "SELECT friend_count FROM user WHERE uid=me()" });

                    //    //    foreach (var friend in frndscount.data)
                    //    //    {
                    //    //        frndscount = friend.friend_count;
                    //    //    }
                    //    //    friendscount = Convert.ToInt16(frndscount);
                    //    //}
                    //    //catch (Exception exx)
                    //    //{
                    //    //    friendscount = 0;
                    //    //    logger.Error(exx.Message);
                    //    //    logger.Error(exx.StackTrace);
                    //    //}
                    //    friendscount = 0;
                    //    logger.Error(ex.Message);
                    //    logger.Error(ex.StackTrace);
                    //}
                    if (objFacebookAccountRepository.checkFacebookUserExists(Convert.ToString(profile["id"]), Guid.Parse(UserId)))
                    {
                        #region Add Facebook Feeds
                        lstNewFacebookFeeds = AddFacebookFeeds(UserId, fb, profile);
                        #endregion
                    }
                    else
                    {
                        //ret = "Account already Exist !";
                    }
                }
                return new JavaScriptSerializer().Serialize(lstNewFacebookFeeds);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        //Added By Sumit Gupta[15-02-15]
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddNewFacebookWallPosts(string FbId, string UserId)
        {
            string ret = string.Empty;
            long friendscount = 0;

            List<Domain.Socioboard.Domain.FacebookMessage> lstFacebookMessage = new List<Domain.Socioboard.Domain.FacebookMessage>();

            try
            {
                Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = new Domain.Socioboard.Domain.FacebookAccount();
                if (objFacebookAccountRepository.checkFacebookUserExists(FbId, Guid.Parse(UserId)))
                {
                    objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(FbId, Guid.Parse(UserId));
                }
                else
                {
                    objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(FbId);
                }


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
                        profile = fb.Get("v2.0/me");

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
                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                        return "Token Expired";
                    }
                    try
                    {
                        friends = fb.Get("v2.0/me/friends");

                    }
                    catch (Exception ex)
                    {
                        string errormssg = ex.Message;
                        if (errormssg.Contains("changed the password"))
                        {
                            UpdateSocialprofileStatus(UserId, FbId);
                        }
                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                        return "Token Expired";
                    }

                    try
                    {
                        //friendscount = Convert.ToInt16(friends["summary"]["total_count"].ToString());
                    }
                    catch (Exception ex)
                    {
                        //try
                        //{
                        //    dynamic frndscount = fb.Get("fql", new { q = "SELECT friend_count FROM user WHERE uid=me()" });

                        //    foreach (var friend in frndscount.data)
                        //    {
                        //        frndscount = friend.friend_count;
                        //    }
                        //    friendscount = Convert.ToInt16(frndscount);
                        //}
                        //catch (Exception exx)
                        //{
                        //    friendscount = 0;
                        //    logger.Error(exx.Message);
                        //    logger.Error(exx.StackTrace);
                        //}
                        friendscount = 0;
                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                    }
                    if (objFacebookAccountRepository.checkFacebookUserExists(Convert.ToString(profile["id"]), Guid.Parse(UserId)))
                    {
                        //#region Add Facebook Feeds
                        //lstNewFacebookFeeds = AddFacebookFeeds(UserId, fb, profile);
                        //#endregion

                        //#region Add Facebook User Home
                        lstFacebookMessage = AddNewFacebookUserHome(UserId, fb, profile);
                        //#endregion
                        //#region Add Facebook User Inbox Message
                        //AddFacebookMessageWithPagination(UserId, fb, profile);
                        //#endregion
                        //ret = "Facebook info Updated Successfully";
                    }
                    else
                    {
                        //ret = "Account already Exist !";
                    }
                }
                return new JavaScriptSerializer().Serialize(lstFacebookMessage);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void getUserNotifications(string UserId, FacebookClient fb, dynamic profile)
        {
            try
            {
                Domain.Socioboard.Domain.InboxMessages _InboxMessages;
                dynamic data = fb.Get("v2.0/me/notifications");
                foreach (var item in data["data"])
                {
                    _InboxMessages = new Domain.Socioboard.Domain.InboxMessages();
                    _InboxMessages.Id = Guid.NewGuid();
                    _InboxMessages.UserId = Guid.Parse(UserId);
                    _InboxMessages.ProfileId = profile["id"].ToString();
                    _InboxMessages.ProfileType = "fb";
                    _InboxMessages.MessageType = "fb_notification";
                    _InboxMessages.EntryTime = DateTime.Now;
                    try
                    {
                        _InboxMessages.Message = item["title"].ToString();
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Facebook.asmx = > getUserNotifications = > " + ex.Message);
                    }
                    try
                    {
                        _InboxMessages.MessageId = item["id"].ToString();
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Facebook.asmx = > getUserNotifications = > " + ex.Message);
                    }
                    try
                    {
                        _InboxMessages.FromId = item["from"]["id"].ToString();
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Facebook.asmx = > getUserNotifications = > " + ex.Message);
                    }
                    try
                    {
                        _InboxMessages.FromName = item["from"]["name"].ToString();
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Facebook.asmx = > getUserNotifications = > " + ex.Message);
                    }
                    try
                    {
                        _InboxMessages.FromImageUrl = "http://graph.facebook.com/" + _InboxMessages.FromId + "/picture?type=small";
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Facebook.asmx = > getUserNotifications = > " + ex.Message);
                    }
                    try
                    {
                        _InboxMessages.RecipientId = item["to"]["id"].ToString();
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Facebook.asmx = > getUserNotifications = > " + ex.Message);
                    }
                    try
                    {
                        _InboxMessages.RecipientName = item["to"]["name"].ToString();
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Facebook.asmx = > getUserNotifications = > " + ex.Message);
                    }
                    try
                    {
                        _InboxMessages.RecipientImageUrl = "http://graph.facebook.com/" + _InboxMessages.RecipientId + "/picture?type=small";
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Facebook.asmx = > getUserNotifications = > " + ex.Message);
                    }
                    try
                    {
                        _InboxMessages.CreatedTime = Convert.ToDateTime(item["created_time"].ToString());
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Facebook.asmx = > getUserNotifications = > " + ex.Message);
                    }

                    if (!objInboxMessagesRepository.checkInboxMessageExists(Guid.Parse(UserId), _InboxMessages.MessageId, _InboxMessages.MessageType))
                    {
                        objInboxMessagesRepository.AddInboxMessages(_InboxMessages);
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Error("getUserNotifications = > "+ex.Message);
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void getPageConversations(string UserId, FacebookClient fb, dynamic profile)
        {
            try
            {
                TwitterDirectMessageRepository _TwitterDirectMessageRepository = new TwitterDirectMessageRepository();
                Domain.Socioboard.Domain.TwitterDirectMessages _TwitterDirectMessages;
                dynamic data = fb.Get("v2.0/me/conversations");
                foreach (var item in data["data"])
                {
                    foreach (var msg_item in item["messages"]["data"])
                    {
                        _TwitterDirectMessages = new Domain.Socioboard.Domain.TwitterDirectMessages();

                        _TwitterDirectMessages.UserId = Guid.Parse(UserId);
                        _TwitterDirectMessages.EntryDate = DateTime.Now;



                        try
                        {
                            _TwitterDirectMessages.MessageId = msg_item["id"].ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Facebook.asmx = > getPageConversations = > " + ex.Message);
                        }
                        try
                        {
                            _TwitterDirectMessages.CreatedDate = Convert.ToDateTime(msg_item["created_time"].ToString());
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Facebook.asmx = > getPageConversations = > " + ex.Message);
                        }
                        try
                        {
                            _TwitterDirectMessages.SenderId = msg_item["from"]["id"].ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Facebook.asmx = > getPageConversations = > " + ex.Message);
                        }
                        try
                        {
                            _TwitterDirectMessages.SenderScreenName = msg_item["from"]["name"].ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Facebook.asmx = > getPageConversations = > " + ex.Message);
                        }
                        try
                        {
                            _TwitterDirectMessages.SenderProfileUrl = "http://graph.facebook.com/" + _TwitterDirectMessages.SenderId + "/picture?type=small";
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Facebook.asmx = > getPageConversations = > " + ex.Message);
                        }
                        try
                        {
                            _TwitterDirectMessages.RecipientId = msg_item["to"]["data"][0]["id"].ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Facebook.asmx = > getPageConversations = > " + ex.Message);
                        }
                        try
                        {
                            _TwitterDirectMessages.RecipientScreenName = msg_item["to"]["data"][0]["name"].ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Facebook.asmx = > getPageConversations = > " + ex.Message);
                        }
                        try
                        {
                            _TwitterDirectMessages.RecipientProfileUrl = "http://graph.facebook.com/" + _TwitterDirectMessages.RecipientId + "/picture?type=small";
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Facebook.asmx = > getPageConversations = > " + ex.Message);
                        }
                        try
                        {
                            _TwitterDirectMessages.Message = msg_item["message"].ToString();
                            if (string.IsNullOrEmpty(_TwitterDirectMessages.Message))
                            {
                                if ((msg_item["attachments"]["data"][0]["mime_type"].ToString()).Contains("image"))
                                {
                                    _TwitterDirectMessages.Image = msg_item["attachments"]["data"][0]["image_data"]["url"].ToString();
                                }
                                else
                                {
                                    _TwitterDirectMessages.Message = msg_item["attachments"]["data"][0]["name"].ToString();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Facebook.asmx = > getPageConversations = > " + ex.Message);
                        }
                        try
                        {
                            _TwitterDirectMessages.Image = msg_item["shares"]["data"][0]["link"].ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Facebook.asmx = > getPageConversations = > " + ex.Message);
                        }
                        if (_TwitterDirectMessages.SenderId == profile["id"].ToString())
                        {
                            _TwitterDirectMessages.Type = "fb_sent";
                        }
                        else
                        {
                            _TwitterDirectMessages.Type = "fb_received";
                        }

                        if (!_TwitterDirectMessageRepository.checkExistsDirectMessages(Guid.Parse(UserId), _TwitterDirectMessages.MessageId))
                        {
                            _TwitterDirectMessageRepository.addNewDirectMessage(_TwitterDirectMessages);
                        }
                        else
                        {
                            _TwitterDirectMessageRepository.updateImage(_TwitterDirectMessages);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("getPageConversations = > "+ex.Message);
            }
        }
        //Added By Sumit Gupta[15-02-15]
        private List<Domain.Socioboard.Domain.FacebookMessage> AddNewFacebookUserHome(string UserId, FacebookClient fb, dynamic profile)
        {
            List<Domain.Socioboard.Domain.FacebookMessage> lstFacebookMessage = new List<Domain.Socioboard.Domain.FacebookMessage>();

            Domain.Socioboard.Domain.FacebookMessage objFacebookMessage = new Domain.Socioboard.Domain.FacebookMessage();
            try
            {
                dynamic home = fb.Get("v2.0/me/home");
                if (home != null)
                {
                    int lstfbcount = 0;
                    foreach (dynamic result in home["data"])
                    {
                        objFacebookMessage = new Domain.Socioboard.Domain.FacebookMessage();

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
                        dynamic likes = fb.Get("v2.0/" + result["id"].ToString() + "/likes?limit=50000000");
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

                        //Check to avoid duplicacy
                        if (!objFacebookMessageRepository.checkFacebookMessageExists(objFacebookMessage.MessageId))
                        {
                            objFacebookMessageRepository.addFacebookMessage(objFacebookMessage);

                            lstFacebookMessage.Add(objFacebookMessage);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return lstFacebookMessage;
        }

        private string SetPrivacy(string privacy, FacebookClient fb)
        {
            try
            {
                JObject Jdata = null;
                string JValue = string.Empty;

                if (!string.IsNullOrEmpty(privacy))
                {
                    if (privacy == "Close Friends")
                    {
                        Jdata = JObject.Parse(fb.Get("/" + objFacebookAccount.FbUserId + "/friendlists/close_friends").ToString());
                        string closefrndid = Jdata["data"][0]["id"].ToString();
                        JValue = "{ \"description\": \"Close Friends\",\"value\": \"CUSTOM\",\"friends\": \"SOME_FRIENDS\",\"networks\": \"\",\"allow\":\"" + closefrndid + "\",\"deny\": \"\"}";
                    }
                    else if (privacy == "Only Me")
                    {
                        JValue = "{\"description\": \"Only Me\",\"value\": \"SELF\",\"friends\": \"\",\"networks\": \"\",\"allow\": \"\",\"deny\": \"\"}";
                    }
                    else if (privacy == "Friends")
                    {
                        JValue = "{\"description\": \"Your friends\",\"value\": \"ALL_FRIENDS\",\"friends\": \"\",\"networks\": \"\",\"allow\": \"\",\"deny\": \"\"}";
                    }
                    else if (privacy == "Friends of Friends")
                    {
                        JValue = "{\"description\": \"Your friends of friends\",\"value\": \"FRIENDS_OF_FRIENDS\",\"friends\": \"\",\"networks\": \"\",\"allow\": \"\",\"deny\": \"\"}";
                    }
                    else if (privacy == "Family")
                    {
                        Jdata = JObject.Parse(fb.Get("/" + objFacebookAccount.FbUserId + "/friendlists/family").ToString());
                        string familyid = Jdata["data"][0]["id"].ToString();
                        JValue = "{\"description\": \"Family\",\"value\": \"CUSTOM\",\"friends\": \"SOME_FRIENDS\",\"networks\": \"\",\"allow\": \"" + familyid + "\",\"deny\": \"\"}";
                    }
                    else if (privacy == "Public")
                    {
                        JValue = "{\"description\": \"Public\",\"value\": \"EVERYONE\",\"friends\": \"\",\"networks\": \"\",\"allow\": \"\",\"deny\": \"\"}";
                    }
                    else if (privacy == "Acquaintances")
                    {
                        Jdata = JObject.Parse(fb.Get("/" + objFacebookAccount.FbUserId + "/friendlists/acquaintances").ToString());
                        string AcquaintancesId = Jdata["data"][0]["id"].ToString();
                        JValue = "{\"description\": \"Acquaintances\",\"value\": \"CUSTOM\",\"friends\": \"SOME_FRIENDS\",\"networks\": \"\",\"allow\": \"" + AcquaintancesId + "\",\"deny\": \"\"}";
                    }
                    return JValue;
                }
                return "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetFacebookPageData(string FbId, string UserId)
        {
            string ret = string.Empty;
            try
            {
                Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = new Domain.Socioboard.Domain.FacebookAccount();
                if (objFacebookAccountRepository.checkFacebookUserExists(FbId, Guid.Parse(UserId)))
                {
                    objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(FbId, Guid.Parse(UserId));
                }
                else
                {
                    objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(FbId);
                }
                FacebookClient fb = new FacebookClient();
                string profileId = string.Empty;
                string accessToken = objFacebookAccount.AccessToken;
                dynamic profile = null;
                dynamic friends = null;
                if (accessToken != null)
                {
                    fb.AccessToken = accessToken;
                    int fancountPage = 0;
                    try
                    {
                        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                        friends = fb.Get("v2.0/" + FbId);
                        fancountPage = Convert.ToInt16(friends["likes"].ToString());
                        objFacebookAccount.Friends = fancountPage;
                    }
                    catch (Exception)
                    {
                        fancountPage = 0;
                    }
                    try
                    {
                        profile = fb.Get("v2.0/me");

                    }
                    catch (Exception ex)
                    {
                        string errormssg = ex.Message;
                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                        return "Token Expired";
                    }
                    if (objFacebookAccountRepository.checkFacebookUserExists(Convert.ToString(profile["id"]), Guid.Parse(UserId)))
                    {
                        #region Update FacebookAccount
                        UpdateFacebookAccount(objFacebookAccount);
                        #endregion
                        #region UpdateTeammemberprofile
                        Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
                        objTeamMemberProfile.ProfileName = objFacebookAccount.FbUserName;
                        objTeamMemberProfile.ProfilePicUrl = "http://graph.facebook.com/" + objFacebookAccount.FbUserId + "/picture?type=small";
                        objTeamMemberProfile.ProfileId = objFacebookAccount.FbUserId;
                        objTeamMemberProfileRepository.updateTeamMemberbyprofileid(objTeamMemberProfile);
                        #endregion

                        //getUserNotifications(UserId,fb,profile);

                        ret = "Facebook page info Updated Successfully";
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
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string FacebookComposeMessageRss(string message, string profileid, string userid, string title, string link)
        {
            string ret = "";
            Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(profileid, Guid.Parse(userid));
            FacebookClient fb = new FacebookClient();

            try
            {
                fb.AccessToken = objFacebookAccount.AccessToken;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                var args = new Dictionary<string, object>();
                args["message"] = message;
                args["description"] = title;
                args["link"] = link;
                ret = fb.Post("v2.0/" + objFacebookAccount.FbUserId + "/feed", args).ToString();
                RssFeedsRepository objrssfeed = new RssFeedsRepository();
                objrssfeed.updateFeedStatus(Guid.Parse(userid), message);
                return ret = "Messages Posted Successfully";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ret = "Message Could Not Posted";
            }
        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddFacebookAccountWithlogin(string accessToken, string UserId, string GroupId)
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
                if (accessToken != null)
                {
                    fb.AccessToken = accessToken;
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                    dynamic profile = fb.Get("v2.0/me");
                    dynamic friends = fb.Get("v2.0/me/friends");
                    try
                    {
                        friendscount = Convert.ToInt16(friends["summary"]["total_count"].ToString());
                    }
                    catch (Exception ex)
                    {
                        //try
                        //{
                        //    dynamic frndscount = fb.Get("fql", new { q = "SELECT friend_count FROM user WHERE uid=me()" });

                        //    foreach (var friend in frndscount.data)
                        //    {
                        //        frndscount = friend.friend_count;
                        //    }
                        //    friendscount = Convert.ToInt16(frndscount);
                        //}
                        //catch (Exception ex)
                        //{
                        //    friendscount = 0;
                        //    logger.Error(ex.Message);
                        //    logger.Error(ex.StackTrace);
                        //}

                        friendscount = 0;
                        logger.Error("friendscount >> " + ex.Message);
                        logger.Error("friendscount >> " + ex.StackTrace);
                        logger.Error("friendscount >> " + friends.ToString());
                        logger.Error("friendscount >> " + friends["paging"]["next"].ToString());
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
                        try
                        {
                            objFacebookAccount.EmailId = (Convert.ToString(profile["email"]));
                        }
                        catch (Exception ex)
                        {

                        }
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
                        objTeamMemberProfile.ProfileName = (Convert.ToString(profile["name"]));
                        objTeamMemberProfile.ProfilePicUrl = "http://graph.facebook.com/" + objTeamMemberProfile.ProfileId + "/picture?type=small";
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
                        //AddFacebookMessage(UserId, fb, profile);
                        AddFacebookMessageWithPagination(UserId, fb, profile);
                        #endregion
                        #region Add Facebook Stats
                        AddFacebookStats(UserId, fb, profile);
                        #endregion
                        //getUserNotifications(UserId, fb, profile);
                        ret = "Account Added Successfully";

                        ret = new JavaScriptSerializer().Serialize(objFacebookAccount);

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
                //return new JavaScriptSerializer().Serialize(ret);
                return ret;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }
    }
}
