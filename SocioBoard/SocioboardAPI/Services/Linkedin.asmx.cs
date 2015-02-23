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
using GlobusLinkedinLib.LinkedIn.Core.SocialStreamMethods;
using log4net;
using System.Text.RegularExpressions;

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
    public class Linkedin : System.Web.Services.WebService
    {
        ILog logger = LogManager.GetLogger(typeof(Linkedin));
        GroupsRepository objGroupsRepository = new GroupsRepository();
        TeamRepository objTeamRepository = new TeamRepository();
        Domain.Socioboard.Domain.Team objteam;
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile;
        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
        Domain.Socioboard.Domain.SocialProfile objSocialProfile;
        LinkedInAccountRepository objLinkedInAccountRepository = new LinkedInAccountRepository();
        Domain.Socioboard.Domain.LinkedInAccount objLinkedInAccount;
        LinkedInFeedRepository objLinkedInFeedRepository = new LinkedInFeedRepository();
        Domain.Socioboard.Domain.LinkedInFeed objLinkedInFeed;
        ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
        Domain.Socioboard.Domain.ScheduledMessage objScheduledMessage;


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetLinkedinRedirectUrl(string consumerKey, string consumerSecret)
        {
            logger.Error("GetLinkedinRedirectUrl()");

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;

            string authLink = string.Empty;
            oAuthLinkedIn Linkedin_oauth = new oAuthLinkedIn();
            Linkedin_oauth.ConsumerKey = consumerKey;
            Linkedin_oauth.ConsumerSecret = consumerSecret;
            authLink = Linkedin_oauth.AuthorizationLinkGet() + "~" + Linkedin_oauth.Token + "~" + Linkedin_oauth.TokenSecret;
            //Session["reuqestToken"] = Linkedin_oauth.Token;
            //Session["reuqestTokenSecret"] = Linkedin_oauth.TokenSecret;
            return authLink;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddLinkedinAccount(string oauth_token, string oauth_verifier, string reuqestTokenSecret, string consumerKey, string consumerSecret, string UserId, string GroupId)
        {
            try
            {
                logger.Error("AddLinkedinAccount()");

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;

                string ret = string.Empty;
                LinkedInProfile objProfile = new LinkedInProfile();
                LinkedInProfile.UserProfile objUserProfile = new LinkedInProfile.UserProfile();
                objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
                oAuthLinkedIn _oauth = new oAuthLinkedIn();
                objLinkedInAccount = new LinkedInAccount();
                #region Get linkedin Profile data from Api
                try
                {
                    _oauth.ConsumerKey = consumerKey;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    logger.Error(ex.Message);
                }
                try
                {
                    _oauth.ConsumerSecret = consumerSecret;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    logger.Error(ex.Message);
                }
                try
                {
                    _oauth.Token = oauth_token;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    logger.Error(ex.Message);
                }
                try
                {
                    _oauth.TokenSecret = reuqestTokenSecret;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    logger.Error(ex.Message);
                }
                try
                {
                    _oauth.Verifier = oauth_verifier;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    logger.Error(ex.Message);
                }
                try
                {
                    _oauth.AccessTokenGet(oauth_token);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    logger.Error(ex.Message);

                }
                try
                {
                    objUserProfile = objProfile.GetUserProfile(_oauth);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    logger.Error(ex.Message);
                }
                #endregion
                dynamic data = objUserProfile;
                try
                {
                    #region LinkedInAccount
                    objLinkedInAccount.UserId = Guid.Parse(UserId);
                    objLinkedInAccount.LinkedinUserId = data.id.ToString();
                    try
                    {
                        objLinkedInAccount.EmailId = data.email.ToString();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                    }
                    objLinkedInAccount.LinkedinUserName = data.first_name.ToString() + data.last_name.ToString();
                    objLinkedInAccount.OAuthToken = _oauth.Token;
                    objLinkedInAccount.OAuthSecret = _oauth.TokenSecret;
                    objLinkedInAccount.OAuthVerifier = _oauth.Verifier;
                    try
                    {
                        objLinkedInAccount.ProfileImageUrl = data.picture_url.ToString();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                    }
                    try
                    {
                        objLinkedInAccount.ProfileUrl = data.profile_url.ToString();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                    }
                    #endregion
                    #region SocialProfiles
                    try
                    {
                        objLinkedInAccount.Connections = data.connections;
                        objLinkedInAccount.IsActive = true;
                        objSocialProfile.UserId = Guid.Parse(UserId);
                        objSocialProfile.ProfileType = "linkedin";
                        objSocialProfile.ProfileId = data.id.ToString();
                        objSocialProfile.ProfileStatus = 1;
                        objSocialProfile.ProfileDate = DateTime.Now;
                        objSocialProfile.Id = Guid.NewGuid();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                    #endregion SocialProfiles
                    #region Add TeamMemberProfile
                    try
                    {
                        Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(GroupId));
                        objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
                        objTeamMemberProfile.Id = Guid.NewGuid();
                        objTeamMemberProfile.TeamId = objTeam.Id;
                        objTeamMemberProfile.Status = 1;
                        objTeamMemberProfile.ProfileType = "linkedin";
                        objTeamMemberProfile.StatusUpdateDate = DateTime.Now;
                        objTeamMemberProfile.ProfileId = objLinkedInAccount.LinkedinUserId;
                        objTeamMemberProfile.ProfileName = objLinkedInAccount.LinkedinUserName;
                        objTeamMemberProfile.ProfilePicUrl = objLinkedInAccount.ProfileImageUrl;

                    }
                    catch (Exception ex)
                    {
                       logger.Error(ex.Message);
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                }
                try
                {
                    if (!objLinkedInAccountRepository.checkLinkedinUserExists(objLinkedInAccount.LinkedinUserId, Guid.Parse(UserId)))
                    {
                        objLinkedInAccountRepository.addLinkedinUser(objLinkedInAccount);
                        ret = "LinkedIn Account Added Successfully";
                    }
                    else
                    {
                        ret = "LinkedIn Account Already Exist";
                    }
                    if (!objSocialProfilesRepository.checkUserProfileExist(objSocialProfile))
                    {
                        objSocialProfilesRepository.addNewProfileForUser(objSocialProfile);
                    }
                    if (!objTeamMemberProfileRepository.checkTeamMemberProfile(objTeamMemberProfile.TeamId, objLinkedInAccount.LinkedinUserId))
                    {
                        objTeamMemberProfileRepository.addNewTeamMember(objTeamMemberProfile);
                    }

                    #region Add LinkedIn Feeds
                    LinkedInNetwork objln = new LinkedInNetwork();
                    List<LinkedInNetwork.Network_Updates> userUPdate = objln.GetNetworkUpdates(_oauth, 20);
                    foreach (var item in userUPdate)
                    {
                        try
                        {
                            objLinkedInFeed = new Domain.Socioboard.Domain.LinkedInFeed();
                            objLinkedInFeed.Feeds = item.Message;
                            objLinkedInFeed.FromId = item.PersonId;
                            objLinkedInFeed.FromName = item.PersonFirstName + " " + item.PersonLastName;
                            objLinkedInFeed.FeedsDate = Convert.ToDateTime(item.DateTime);
                            objLinkedInFeed.EntryDate = DateTime.Now;
                            objLinkedInFeed.ProfileId = objLinkedInAccount.LinkedinUserId;
                            objLinkedInFeed.Type = item.UpdateType;
                            objLinkedInFeed.UserId = Guid.Parse(UserId);
                            objLinkedInFeed.FromPicUrl = item.PictureUrl;
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.StackTrace);
                        }
                        if (!objLinkedInFeedRepository.checkLinkedInFeedExists(objLinkedInFeed.FeedId, Guid.Parse(UserId)))
                        {

                            objLinkedInFeedRepository.addLinkedInFeed(objLinkedInFeed);
                        }
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                }
                return "";
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                return "";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetLinkedUserUpdates(string profileid, string UserId)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
            LinkedInAccount linkacc;
            string authLink = string.Empty;
            LinkedInAccountRepository linkedinAccRepo = new LinkedInAccountRepository();
            if (linkedinAccRepo.checkLinkedinUserExists(profileid, Guid.Parse(UserId)))
            {
                linkacc = linkedinAccRepo.getUserInformation(Guid.Parse(UserId), profileid);
            }
            else
            {
                linkacc = linkedinAccRepo.getUserInformation(profileid);
            }
            oAuthLinkedIn oauthlin = new oAuthLinkedIn();
            oauthlin.ConsumerKey = ConfigurationManager.AppSettings["LiApiKey"];
            oauthlin.ConsumerSecret = ConfigurationManager.AppSettings["LiSecretKey"];
            oauthlin.FirstName = linkacc.LinkedinUserName;
            oauthlin.Id = linkacc.LinkedinUserId;
            oauthlin.Token = linkacc.OAuthToken;
            oauthlin.TokenSecret = linkacc.OAuthSecret;
            oauthlin.Verifier = linkacc.OAuthVerifier;
            GlobusLinkedinLib.App.Core.LinkedInUser l = new GlobusLinkedinLib.App.Core.LinkedInUser();
            List<Domain.Socioboard.Domain.LinkedInUser.User_Updates> lst = l.GetUserUpdate(oauthlin, linkacc.LinkedinUserId, 10);
            return new JavaScriptSerializer().Serialize(lst);
           
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetLinkedUsergroupsDetail(string profileid, string UserId)
        {
             LinkedInAccount linkacc;
            string authLink = string.Empty;
            LinkedInAccountRepository linkedinAccRepo = new LinkedInAccountRepository();
            if (linkedinAccRepo.checkLinkedinUserExists(profileid, Guid.Parse(UserId)))
            {
                linkacc = linkedinAccRepo.getUserInformation(Guid.Parse(UserId), profileid);
            }
            else
            {
                linkacc = linkedinAccRepo.getUserInformation(profileid);
            }
            oAuthLinkedIn oauthlin = new oAuthLinkedIn();
            oauthlin.ConsumerKey = ConfigurationManager.AppSettings["LiApiKey"];
            oauthlin.ConsumerSecret = ConfigurationManager.AppSettings["LiSecretKey"];
            oauthlin.FirstName = linkacc.LinkedinUserName;
            oauthlin.Id = linkacc.LinkedinUserId;
            oauthlin.Token = linkacc.OAuthToken;
            oauthlin.TokenSecret = linkacc.OAuthSecret;
            oauthlin.Verifier = linkacc.OAuthVerifier;
            GlobusLinkedinLib.App.Core.LinkedInUser l = new GlobusLinkedinLib.App.Core.LinkedInUser();
            List<Domain.Socioboard.Domain.LinkedInUser.User_Updates> lst = l.GetUserUpdate(oauthlin, linkacc.LinkedinUserId, 10);
            return new JavaScriptSerializer().Serialize(lst);

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetLinkedGroupsDetail(string profileid,string userid)
        {
            LinkedInAccount linkacc;
            string authLink = string.Empty;
            LinkedInAccountRepository linkedinAccRepo = new LinkedInAccountRepository();
            if (linkedinAccRepo.checkLinkedinUserExists(profileid, Guid.Parse(userid)))
            {
                linkacc = linkedinAccRepo.getUserInformation(Guid.Parse(userid), profileid);
            }
            else
            {
                linkacc = linkedinAccRepo.getUserInformation(profileid);
            }
            oAuthLinkedIn oauthlin = new oAuthLinkedIn();
            oauthlin.ConsumerKey = ConfigurationManager.AppSettings["LiApiKey"];
            oauthlin.ConsumerSecret = ConfigurationManager.AppSettings["LiSecretKey"];
            oauthlin.FirstName = linkacc.LinkedinUserName;
            oauthlin.Id = linkacc.LinkedinUserId;
            oauthlin.Token = linkacc.OAuthToken;
            oauthlin.TokenSecret = linkacc.OAuthSecret;
            oauthlin.Verifier = linkacc.OAuthVerifier;
            GlobusLinkedinLib.App.Core.LinkedInGroup l = new GlobusLinkedinLib.App.Core.LinkedInGroup();
            List<Domain.Socioboard.Domain.LinkedInGroup.Group_Updates> lst = l.GetGroupUpdates(oauthlin, 20);
            return new JavaScriptSerializer().Serialize(lst);

        }
         [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetLinkedGroupsDataDetail(string userid,string groupid, string linkedinId)
        {
            LinkedInAccount linkacc;
            string authLink = string.Empty;
            LinkedInAccountRepository linkedinAccRepo = new LinkedInAccountRepository();
            if (linkedinAccRepo.checkLinkedinUserExists(linkedinId, Guid.Parse(userid)))
            {
                linkacc = linkedinAccRepo.getUserInformation(Guid.Parse(userid), linkedinId);
            }
            else
            {
                linkacc = linkedinAccRepo.getUserInformation(linkedinId);
            }
           // LinkedInAccount linkacc = linkedinAccRepo.getUserInformation(Guid.Parse(userid), linkedinId);
            oAuthLinkedIn oauthlin = new oAuthLinkedIn();
            oauthlin.ConsumerKey = ConfigurationManager.AppSettings["LiApiKey"];
            oauthlin.ConsumerSecret = ConfigurationManager.AppSettings["LiSecretKey"];
            oauthlin.FirstName = linkacc.LinkedinUserName;
            oauthlin.Id = linkacc.LinkedinUserId;
            oauthlin.Token = linkacc.OAuthToken;
            oauthlin.TokenSecret = linkacc.OAuthSecret;
            oauthlin.Verifier = linkacc.OAuthVerifier;
            GlobusLinkedinLib.App.Core.LinkedInGroup l = new GlobusLinkedinLib.App.Core.LinkedInGroup();
            List<Domain.Socioboard.Domain.LinkedInGroup.Group_Updates> lst = l.GetGroupPostData(oauthlin, 20, groupid, linkedinId);
            return new JavaScriptSerializer().Serialize(lst);

        }

         [WebMethod]
         [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
         public string CommentOnLinkedInPost(string groupid, string GpPostid, string message, string LinkedinUserId,string userid)
         {
             LinkedInAccount linkacc;
             string authLink = string.Empty;
             LinkedInAccountRepository linkedinAccRepo = new LinkedInAccountRepository();
             if (linkedinAccRepo.checkLinkedinUserExists(LinkedinUserId, Guid.Parse(userid)))
             {
                 linkacc = linkedinAccRepo.getUserInformation(Guid.Parse(userid), LinkedinUserId);
             }
             else
             {
                 linkacc = linkedinAccRepo.getUserInformation(LinkedinUserId);
             }
             oAuthLinkedIn oauthlin = new oAuthLinkedIn();
             oauthlin.ConsumerKey = ConfigurationManager.AppSettings["LiApiKey"];
             oauthlin.ConsumerSecret = ConfigurationManager.AppSettings["LiSecretKey"];
             oauthlin.FirstName = linkacc.LinkedinUserName;
             oauthlin.Id = linkacc.LinkedinUserId;
             oauthlin.Token = linkacc.OAuthToken;
             oauthlin.TokenSecret = linkacc.OAuthSecret;
             oauthlin.Verifier = linkacc.OAuthVerifier;
             GlobusLinkedinLib.LinkedIn.Core.SocialStreamMethods.SocialStream l = new GlobusLinkedinLib.LinkedIn.Core.SocialStreamMethods.SocialStream();
             string status = l.SetCommentOnPost(oauthlin, GpPostid, message);
             return "success";
            

         }
         [WebMethod]
         [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
         public string LikeOnLinkedinPost(string GpPostid, string LinkedinUserId, string islike,string userid)
         {
             int like = Convert.ToInt32(islike);
             string likestatus = string.Empty;
             if (like == 0)
             {
                 likestatus = "true";
             }
             else
             { 
                 likestatus = "false"; 
             }

             LinkedInAccount linkacc;
             string authLink = string.Empty;
             LinkedInAccountRepository linkedinAccRepo = new LinkedInAccountRepository();
             if (linkedinAccRepo.checkLinkedinUserExists(LinkedinUserId, Guid.Parse(userid)))
             {
                 linkacc = linkedinAccRepo.getUserInformation(Guid.Parse(userid), LinkedinUserId);
             }
             else
             {
                 linkacc = linkedinAccRepo.getUserInformation(LinkedinUserId);
             }
             oAuthLinkedIn oauthlin = new oAuthLinkedIn();
             oauthlin.ConsumerKey = ConfigurationManager.AppSettings["LiApiKey"];
             oauthlin.ConsumerSecret = ConfigurationManager.AppSettings["LiSecretKey"];
             oauthlin.FirstName = linkacc.LinkedinUserName;
             oauthlin.Id = linkacc.LinkedinUserId;
             oauthlin.Token = linkacc.OAuthToken;
             oauthlin.TokenSecret = linkacc.OAuthSecret;
             oauthlin.Verifier = linkacc.OAuthVerifier;
             GlobusLinkedinLib.LinkedIn.Core.SocialStreamMethods.SocialStream l = new GlobusLinkedinLib.LinkedIn.Core.SocialStreamMethods.SocialStream();
             string status = l.SetLikeUpdate(oauthlin, GpPostid, likestatus);
             return "success";
            

         }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string FollowLinkedinPost(string GpPostid, string LinkedinUserId, string isFollowing, string userid)
        {
            int Following = Convert.ToInt32(isFollowing);
            string FollowStatus = string.Empty;
            if (Following == 0)
            {
                FollowStatus = "true";
            }
            else
            {
                FollowStatus = "false";
            }

            LinkedInAccount linkacc;
            string authLink = string.Empty;
            LinkedInAccountRepository linkedinAccRepo = new LinkedInAccountRepository();
            if (linkedinAccRepo.checkLinkedinUserExists(LinkedinUserId, Guid.Parse(userid)))
            {
                linkacc = linkedinAccRepo.getUserInformation(Guid.Parse(userid), LinkedinUserId);
            }
            else
            {
                linkacc = linkedinAccRepo.getUserInformation(LinkedinUserId);
            }
            oAuthLinkedIn oauthlin = new oAuthLinkedIn();
            oauthlin.ConsumerKey = ConfigurationManager.AppSettings["LiApiKey"];
            oauthlin.ConsumerSecret = ConfigurationManager.AppSettings["LiSecretKey"];
            oauthlin.FirstName = linkacc.LinkedinUserName;
            oauthlin.Id = linkacc.LinkedinUserId;
            oauthlin.Token = linkacc.OAuthToken;
            oauthlin.TokenSecret = linkacc.OAuthSecret;
            oauthlin.Verifier = linkacc.OAuthVerifier;
            GlobusLinkedinLib.LinkedIn.Core.SocialStreamMethods.SocialStream l = new GlobusLinkedinLib.LinkedIn.Core.SocialStreamMethods.SocialStream();
            string status = l.SetFollowCountUpdate(oauthlin, GpPostid, FollowStatus);
            return "success";


        }

        
         [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string PostLinkedInGroupFeeds(string gid, string linkedInUserId, string msg, string title, string userid) 
        {
            LinkedInAccount linkacc;
            string authLink = string.Empty;
            LinkedInAccountRepository linkedinAccRepo = new LinkedInAccountRepository();
            if (linkedinAccRepo.checkLinkedinUserExists(linkedInUserId, Guid.Parse(userid)))
            {
                linkacc = linkedinAccRepo.getUserInformation(Guid.Parse(userid), linkedInUserId);
            }
            else
            {
                linkacc = linkedinAccRepo.getUserInformation(linkedInUserId);
            }
            oAuthLinkedIn oauthlin = new oAuthLinkedIn();
            oauthlin.ConsumerKey = ConfigurationManager.AppSettings["LiApiKey"];
            oauthlin.ConsumerSecret = ConfigurationManager.AppSettings["LiSecretKey"];
            oauthlin.FirstName = linkacc.LinkedinUserName;
            oauthlin.Id = linkacc.LinkedinUserId;
            oauthlin.Token = linkacc.OAuthToken;
            oauthlin.TokenSecret = linkacc.OAuthSecret;
            oauthlin.Verifier = linkacc.OAuthVerifier;
            GlobusLinkedinLib.LinkedIn.Core.SocialStreamMethods.SocialStream l = new GlobusLinkedinLib.LinkedIn.Core.SocialStreamMethods.SocialStream();
            string status = l.SetPostUpdate(oauthlin, gid, msg, title);
            return "success";

        }

         [WebMethod]
         [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
         public string LinkedinComposeMessage(String message, String profileid, string userid, string currentdatetime, string picurl)
         {
             string ret = "";
             LinkedInAccount LinkedAccount;
             string authLink = string.Empty;
             LinkedInAccountRepository linkedinAccRepo = new LinkedInAccountRepository();
             if (linkedinAccRepo.checkLinkedinUserExists(profileid, Guid.Parse(userid)))
             {
                 LinkedAccount = linkedinAccRepo.getUserInformation(Guid.Parse(userid), profileid);
             }
             else
             {
                 LinkedAccount = linkedinAccRepo.getUserInformation(profileid);
             }
             oAuthLinkedIn Linkedin_oauth = new oAuthLinkedIn();
             Linkedin_oauth.Verifier = LinkedAccount.OAuthVerifier;
             Linkedin_oauth.TokenSecret = LinkedAccount.OAuthSecret;
             Linkedin_oauth.Token = LinkedAccount.OAuthToken;
             Linkedin_oauth.Id = LinkedAccount.LinkedinUserId;
             Linkedin_oauth.FirstName = LinkedAccount.LinkedinUserName;
             SocialStream sociostream = new SocialStream();

             if (!string.IsNullOrEmpty(picurl))
             {
                 picurl = ConfigurationManager.AppSettings["DomainName"].ToString() + Regex.Split(picurl, "wwwroot")[1].Replace("\\", "/");
                 string res = sociostream.SetImageStatusUpdate(Linkedin_oauth, message, picurl);
             }
             else
             {
                 string res = sociostream.SetStatusUpdate(Linkedin_oauth, message);
             }

             //string res = sociostream.SetStatusUpdate(Linkedin_oauth, message);
             return ret;
         }


         [WebMethod]
         [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
         public string SheduleLinkedInMessage(string LinkedInId, string UserId, string sscheduledmsgguid)
         {
             string str = string.Empty;
             LinkedInAccount LinkedAccount;
             string authLink = string.Empty;
             LinkedInAccountRepository linkedinAccRepo = new LinkedInAccountRepository();
             try
             {
                 objScheduledMessage = objScheduledMessageRepository.GetScheduledMessageDetails(Guid.Parse(sscheduledmsgguid));
                 if (linkedinAccRepo.checkLinkedinUserExists(LinkedInId, Guid.Parse(UserId)))
                 {
                     LinkedAccount = linkedinAccRepo.getUserInformation(Guid.Parse(UserId), LinkedInId);
                 }
                 else
                 {
                     LinkedAccount = linkedinAccRepo.getUserInformation(LinkedInId);
                 }
                 oAuthLinkedIn Linkedin_oauth = new oAuthLinkedIn();
                 Linkedin_oauth.ConsumerKey = System.Configuration.ConfigurationSettings.AppSettings["LiApiKey"].ToString();
                 Linkedin_oauth.ConsumerSecret = System.Configuration.ConfigurationSettings.AppSettings["LiSecretKey"].ToString();
                 Linkedin_oauth.FirstName = LinkedAccount.LinkedinUserName;
                 Linkedin_oauth.Token = LinkedAccount.OAuthToken;
                 Linkedin_oauth.TokenSecret = LinkedAccount.OAuthSecret;
                 Linkedin_oauth.Verifier = LinkedAccount.OAuthVerifier;
                 string message = objScheduledMessage.ShareMessage;
                 string picurl = objScheduledMessage.PicUrl;
                 if (LinkedAccount != null)
                 {
                     try
                     {
                         //GlobusLinkedinLib.App.Core.LinkedInUser linkeduser = new GlobusLinkedinLib.App.Core.LinkedInUser();
                         if (string.IsNullOrEmpty(objScheduledMessage.ShareMessage) && string.IsNullOrEmpty(objScheduledMessage.PicUrl))
                         {
                             //objScheduledMessage.ShareMessage = "There is no data in Share Message !";
                             str = "There is no data in Share Message !";
                         }
                         else
                         {
                             var response = string.Empty; ;
                             try
                             {
                                 //response = linkeduser.SetStatusUpdate(Linkedin_oauth, objScheduledMessage.ShareMessage);
                                 SocialStream sociostream = new SocialStream();
                                 if (!string.IsNullOrEmpty(picurl))
                                 {
                                     picurl = ConfigurationManager.AppSettings["DomainName"].ToString() + Regex.Split(picurl, "wwwroot")[1].Replace("\\", "/");
                                     response = sociostream.SetImageStatusUpdate(Linkedin_oauth, message, picurl);
                                 }
                                 else
                                 {
                                     response = sociostream.SetStatusUpdate(Linkedin_oauth, message);
                                 }
                             }
                             catch (Exception ex)
                             {
                                 Console.WriteLine(ex.StackTrace);
                                 str = ex.Message;
                             }

                             if (!string.IsNullOrEmpty(response))
                             {
                                 str = "Message post on linkedin for Id :" + LinkedAccount.LinkedinUserId + " and Message: " + objScheduledMessage.ShareMessage;
                                 ScheduledMessage schmsg = new ScheduledMessage();
                                 schmsg.UpdateScheduledMessageByMsgId(Guid.Parse(sscheduledmsgguid));
                             }
                             else
                             {
                                 str = "Message not posted";
                             } 
                         }
                     }
                     catch (Exception ex)
                     {
                         Console.WriteLine(ex.StackTrace);
                         str = ex.Message;
                     }
                 }
                 else
                 {
                     str = "Linkedin account not found for id" + objScheduledMessage.ProfileId;
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
         public string ScheduleLinkedinGroupMessage(string scheduledmsgguid, string Userid, string profileid)
         {
             string str = string.Empty;
             try
             {
                 LinkedInAccount linkacc;
                 string authLink = string.Empty;
                 LinkedInAccountRepository linkedinAccRepo = new LinkedInAccountRepository();
                 objScheduledMessage = objScheduledMessageRepository.GetScheduledMessageDetails(Guid.Parse(scheduledmsgguid));
                 GroupScheduleMessageRepository grpschedulemessagerepo = new GroupScheduleMessageRepository();
                 Domain.Socioboard.Domain.GroupScheduleMessage _GroupScheduleMessage = grpschedulemessagerepo.GetScheduleMessageId(objScheduledMessage.Id);
                 if (linkedinAccRepo.checkLinkedinUserExists(profileid, Guid.Parse(Userid)))
                 {
                     linkacc = linkedinAccRepo.getUserInformation(Guid.Parse(Userid), profileid);
                 }
                 else
                 {
                     linkacc = linkedinAccRepo.getUserInformation(profileid);
                 }
                 oAuthLinkedIn oauthlin = new oAuthLinkedIn();
                 oauthlin.ConsumerKey = ConfigurationManager.AppSettings["LiApiKey"];
                 oauthlin.ConsumerSecret = ConfigurationManager.AppSettings["LiSecretKey"];
                 oauthlin.FirstName = linkacc.LinkedinUserName;
                 oauthlin.Id = linkacc.LinkedinUserId;
                 oauthlin.Token = linkacc.OAuthToken;
                 oauthlin.TokenSecret = linkacc.OAuthSecret;
                 oauthlin.Verifier = linkacc.OAuthVerifier;

                 string imgurl = objScheduledMessage.PicUrl;
                 string text = objScheduledMessage.ShareMessage;
                 string[] arrtext = null;
                 try
                 {
                     arrtext = System.Text.RegularExpressions.Regex.Split(text, "$%^_^%$");
                     if (arrtext.Count() == 1)
                     {
                         arrtext = null;
                         arrtext = text.Split(new string[] { "$%^_^%$" }, StringSplitOptions.None);
                     }
                 }
                 catch (Exception ex)
                 {
                     return "somthing went wrong";
                 }
                 string Title = arrtext[0];
                 string Message = arrtext[1];
                 string response = string.Empty;
                 if (linkacc != null)
                 {
                     try
                     {
                         if (string.IsNullOrEmpty(objScheduledMessage.ShareMessage) && string.IsNullOrEmpty(objScheduledMessage.PicUrl))
                         {
                             str = "There is no data in Share Message !";
                         }
                         else
                         {
                             SocialStream sociostream = new SocialStream();
                             if (!string.IsNullOrEmpty(imgurl))
                             {
                                 imgurl = ConfigurationManager.AppSettings["DomainName"].ToString() + Regex.Split(imgurl, "wwwroot")[1].Replace("\\", "/");
                                 response = sociostream.SetImagePostUpdate(oauthlin, _GroupScheduleMessage.GroupId, Message, Title, imgurl);
                             }
                             else
                             {
                                 response = sociostream.SetPostUpdate(oauthlin, _GroupScheduleMessage.GroupId, Message, Title);
                             }
                         }
                     }
                     catch (Exception ex)
                     {
                     }

                     str = "Message post on linkedingroup for Id :" + linkacc.LinkedinUserId + ", Title: " + Title + " and Message: " + Message;
                     ScheduledMessage schmsg = new ScheduledMessage();
                     schmsg.UpdateScheduledMessageByMsgId(Guid.Parse(scheduledmsgguid));

                 }
             }
             catch (Exception ex)
             {
                 Console.WriteLine(ex.Message);
                 str = ex.Message;
             }
             return str;
         }
         [WebMethod]
         [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
         public string getLinkedInData(string UserId, string LinkedinId)
         {
             string ret = string.Empty;
             try
             {
                 Guid userId = Guid.Parse(UserId);
                 //oAuthTwitter OAuth = new oAuthTwitter(ConfigurationManager.AppSettings["consumerKey"], ConfigurationManager.AppSettings["consumerSecret"], ConfigurationManager.AppSettings["callbackurl"]);

                 oAuthLinkedIn Linkedin_Oauth = new oAuthLinkedIn();
                 Linkedin_Oauth.ConsumerKey = ConfigurationManager.AppSettings["LiApiKey"];
                 Linkedin_Oauth.ConsumerSecret = ConfigurationManager.AppSettings["LiSecretKey"];
                 LinkedInAccountRepository objLinkedInAccountRepository = new LinkedInAccountRepository();
                 LinkedInAccount LinkedAccount;
                 LinkedInAccountRepository linkedinAccRepo = new LinkedInAccountRepository();
                 if (linkedinAccRepo.checkLinkedinUserExists(LinkedinId, Guid.Parse(UserId)))
                 {
                     LinkedAccount = linkedinAccRepo.getUserInformation(Guid.Parse(UserId), LinkedinId);
                     #region UpdateTeammemberprofile
                     Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
                     objTeamMemberProfile.ProfileName = LinkedAccount.LinkedinUserName;
                     objTeamMemberProfile.ProfilePicUrl = LinkedAccount.ProfileImageUrl;
                     objTeamMemberProfile.ProfileId = LinkedAccount.LinkedinUserId;
                     objTeamMemberProfileRepository.updateTeamMemberbyprofileid(objTeamMemberProfile);
                     #endregion
                 }
                 else
                 {
                     LinkedAccount = linkedinAccRepo.getUserInformation(LinkedinId);
                     #region UpdateTeammemberprofile
                     Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
                     objTeamMemberProfile.ProfileName = LinkedAccount.LinkedinUserName;
                     objTeamMemberProfile.ProfilePicUrl = LinkedAccount.ProfileImageUrl;
                     objTeamMemberProfile.ProfileId = LinkedAccount.LinkedinUserId;
                     objTeamMemberProfileRepository.updateTeamMemberbyprofileid(objTeamMemberProfile);
                     #endregion
                 }

                 Linkedin_Oauth.Token = LinkedAccount.OAuthToken;
                 Linkedin_Oauth.TokenSecret = LinkedAccount.OAuthSecret;
                 Linkedin_Oauth.Verifier = LinkedAccount.OAuthVerifier;
                 GetUserProfile(Linkedin_Oauth, LinkedAccount.LinkedinUserId, userId);
                 GetLinkedInFeeds(Linkedin_Oauth, LinkedinId, userId);
                 return "linkedin Info Updated";
             }
             catch (Exception ex)
             {
                 
                 Console.WriteLine(ex.StackTrace);
             }


             return ret;
         }


         [WebMethod]
         [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
         public void GetUserProfile(oAuthLinkedIn OAuth, string LinkedinUserId, Guid user)
         {
             LinkedInProfile objLinkedInProfile = new LinkedInProfile();
             LinkedInProfile.UserProfile objUserProfile = new LinkedInProfile.UserProfile();
             objUserProfile = objLinkedInProfile.GetUserProfile(OAuth);
             GetLinkedInUserProfile(objUserProfile, OAuth, user, LinkedinUserId);
         }


         [WebMethod]
         [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
         public void GetLinkedInUserProfile(dynamic data, oAuthLinkedIn _oauth, Guid user, string LinkedinUserId)
         {

             LinkedInAccount objLinkedInAccount = new LinkedInAccount();
             LinkedInAccountRepository objLiRepo = new LinkedInAccountRepository();
             try
             {
                 objLinkedInAccount.UserId = user;
                 objLinkedInAccount.LinkedinUserId = data.id.ToString();
                 try
                 {
                     objLinkedInAccount.EmailId = data.email.ToString();
                 }
                 catch { }
                 objLinkedInAccount.LinkedinUserName = data.first_name.ToString() + data.last_name.ToString();
                 objLinkedInAccount.OAuthToken = _oauth.Token;
                 objLinkedInAccount.OAuthSecret = _oauth.TokenSecret;
                 objLinkedInAccount.OAuthVerifier = _oauth.Verifier;
                 try
                 {
                     objLinkedInAccount.ProfileImageUrl = data.picture_url.ToString();
                 }
                 catch { }
                 try
                 {
                     objLinkedInAccount.ProfileUrl = data.profile_url.ToString();
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine(ex.StackTrace);
                 }
                 objLinkedInAccount.Connections = data.connections;
                 objLinkedInAccount.IsActive = true;
                 objLiRepo.updateLinkedinUser(objLinkedInAccount);
             }
             catch
             {
             }

         }

         [WebMethod]
         [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
         public void GetLinkedInFeeds(oAuthLinkedIn _oauth, string profileId, Guid userId)
         {
             LinkedInNetwork objln = new LinkedInNetwork();
             LinkedInFeedRepository objliFeedsRepo = new LinkedInFeedRepository();
             List<LinkedInNetwork.Network_Updates> userUPdate = objln.GetNetworkUpdates(_oauth, 20);
             Domain.Socioboard.Domain.LinkedInFeed lnkfeeds = new Domain.Socioboard.Domain.LinkedInFeed();
             foreach (var item in userUPdate)
             {
                 lnkfeeds.Feeds = item.Message;
                 lnkfeeds.FromId = item.PersonId;
                 lnkfeeds.FromName = item.PersonFirstName + " " + item.PersonLastName;
                 lnkfeeds.FeedsDate = Convert.ToDateTime(item.DateTime);
                 lnkfeeds.EntryDate = DateTime.Now;
                 lnkfeeds.ProfileId = profileId;
                 lnkfeeds.Type = item.UpdateType;
                 lnkfeeds.UserId = userId;
                 lnkfeeds.FromPicUrl = item.PictureUrl;
                 objliFeedsRepo.addLinkedInFeed(lnkfeeds);
             }

         }

         [WebMethod]
         [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
         public string LinkedinProfileDetails(string Userid, string ProfileId)
         {
             Domain.Socioboard.Domain.LinkedInAccount objLinkedinAccount = new Domain.Socioboard.Domain.LinkedInAccount();
             LinkedInAccountRepository _objLinkedInAccountRepository = new LinkedInAccountRepository();
             if (_objLinkedInAccountRepository.checkLinkedinUserExists(ProfileId, Guid.Parse(Userid)))
             {
                 objLinkedinAccount = _objLinkedInAccountRepository.getUserInformation(Guid.Parse(Userid), ProfileId);
             }
             else
             {
                 objLinkedinAccount = _objLinkedInAccountRepository.getUserInformation(ProfileId);
             }
             return new JavaScriptSerializer().Serialize(objLinkedinAccount);
         }

         [WebMethod]
         [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
         public string UpdateLinkedinAccountByAdmin(string ObjLinkedin)
         {
             Domain.Socioboard.Domain.LinkedInAccount objLinkedinAccount = (Domain.Socioboard.Domain.LinkedInAccount)(new JavaScriptSerializer().Deserialize(ObjLinkedin, typeof(Domain.Socioboard.Domain.LinkedInAccount)));
             try
             {
                 objLinkedInAccountRepository.updateLinkedinUser(objLinkedinAccount);
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
