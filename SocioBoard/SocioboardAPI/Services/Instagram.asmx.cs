using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Domain.Socioboard.Domain;
using Hammock.Authentication.OAuth;
using Hammock;
using Newtonsoft.Json.Linq;
using Api.Socioboard.Helper;
using GlobusInstagramLib.Authentication;
using GlobusInstagramLib.App.Core;
using log4net;
using System.Collections;

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
    public class Instagram : System.Web.Services.WebService
    {

        ILog logger = LogManager.GetLogger(typeof(Instagram));
        GroupsRepository objGroupsRepository = new GroupsRepository();
        TeamRepository objTeamRepository = new TeamRepository();
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
        InstagramAccountRepository objInstagramAccountRepository = new InstagramAccountRepository();
        Domain.Socioboard.Domain.InstagramAccount objInstagramAccount;
        InstagramCommentRepository objInstagramCommentRepository = new InstagramCommentRepository();
        Domain.Socioboard.Domain.InstagramComment objInstagramComment = new Domain.Socioboard.Domain.InstagramComment();
        InstagramFeedRepository objInstagramFeedRepository = new InstagramFeedRepository();
        Domain.Socioboard.Domain.InstagramFeed objInstagramFeed = new Domain.Socioboard.Domain.InstagramFeed();
        ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
        Domain.Socioboard.Domain.ScheduledMessage objScheduledMessage;

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getInstagramData(string UserId, string InstagramId)
        {
            Guid userId = Guid.Parse(UserId);
            InstagramAccountRepository objInstagramAccountRepository = new InstagramAccountRepository();
            //LinkedInAccountRepository objLinkedInAccountRepository = new LinkedInAccountRepository();

            Domain.Socioboard.Domain.InstagramAccount objInstagramAccount = objInstagramAccountRepository.getInstagramAccountDetailsById(InstagramId, userId);
            GlobusInstagramLib.Authentication.ConfigurationIns configi = new GlobusInstagramLib.Authentication.ConfigurationIns("https://api.instagram.com/oauth/authorize/", ConfigurationManager.AppSettings["InstagramClientKey"], ConfigurationManager.AppSettings["InstagramClientSec"],ConfigurationManager.AppSettings["RedirectUrl"], "http://api.instagram.com/oauth/access_token", "https://api.instagram.com/v1/", "");
            oAuthInstagram _api = new oAuthInstagram();
            _api = oAuthInstagram.GetInstance(configi);
                GetIntagramImages(objInstagramAccount);
                #region UpdateTeammemberprofile
                Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
                objTeamMemberProfile.ProfileName = objInstagramAccount.InsUserName;
                objTeamMemberProfile.ProfilePicUrl = objInstagramAccount.ProfileUrl;
                objTeamMemberProfile.ProfileId = objInstagramAccount.InstagramId;
                objTeamMemberProfileRepository.updateTeamMemberbyprofileid(objTeamMemberProfile);
                #endregion
                return "Instagram Info is Updated successfully"; 
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SheduleInstagramMessage(string InstagramId, string UserId, string sscheduledmsgguid)
        {
            string str = string.Empty;
            try
            {
                objScheduledMessage = objScheduledMessageRepository.GetScheduledMessageDetails(Guid.Parse(sscheduledmsgguid));
                objInstagramAccount = objInstagramAccountRepository.getInstagramAccountDetailsById(InstagramId, Guid.Parse(UserId));

               // oAuthInstagram Instagram_oauth = new oAuthInstagram();
               // Instagram_oauth.ConsumerKey = System.Configuration.ConfigurationSettings.AppSettings["LiApiKey"].ToString();
               //Instagram_oauth.
            }
            catch (Exception ex)
            {
                
                throw;
            }
            return str;
        }
        
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetInstagramRedirectUrl(string consumerKey, string consumerSecret, string CallBackUrl)
        {
            string rest = string.Empty;
            GlobusInstagramLib.Authentication.ConfigurationIns config = new GlobusInstagramLib.Authentication.ConfigurationIns("https://instagram.com/oauth/authorize/", consumerKey, consumerSecret, CallBackUrl, "https://api.instagram.com/oauth/access_token", "https://api.instagram.com/v1/", "");
            oAuthInstagram _api = oAuthInstagram.GetInstance(config);
            rest = _api.AuthGetUrl("likes+comments+basic+relationships");
            return rest;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddInstagramAccount(string client_id, string client_secret, string redirect_uri, string UserId, string GroupId, string code)
        {
            string ret = string.Empty;
            oAuthInstagram objInsta = new oAuthInstagram();
            GlobusInstagramLib.Authentication.ConfigurationIns configi = new GlobusInstagramLib.Authentication.ConfigurationIns("https://api.instagram.com/oauth/authorize/", client_id, client_secret, redirect_uri, "http://api.instagram.com/oauth/access_token", "https://api.instagram.com/v1/", "");
            oAuthInstagram _api=new oAuthInstagram ();
            _api = oAuthInstagram.GetInstance(configi);
            AccessToken access = new AccessToken();
            access = _api.AuthGetAccessToken(code);
            #region InstagramAccount
            UserController objusercontroller = new UserController();
            InstagramResponse<GlobusInstagramLib.App.Core.User> objuser = objusercontroller.GetUserDetails(access.user.id, access.access_token);
            objInstagramAccount = new Domain.Socioboard.Domain.InstagramAccount();
            objInstagramAccount.AccessToken = access.access_token;
            objInstagramAccount.InstagramId = access.user.id;
            objInstagramAccount.ProfileUrl = access.user.profile_picture;
            objInstagramAccount.InsUserName = access.user.username;
            objInstagramAccount.TotalImages = objuser.data.counts.media;
            objInstagramAccount.FollowedBy = objuser.data.counts.followed_by;
            objInstagramAccount.Followers = objuser.data.counts.follows;
            objInstagramAccount.UserId = Guid.Parse(UserId); 
            #endregion


            if (!objInstagramAccountRepository.checkInstagramUserExists(objInstagramAccount.InstagramId,Guid.Parse(UserId)))
                {
                    objInstagramAccountRepository.addInstagramUser(objInstagramAccount);
                    #region Add TeamMemberProfile
                    Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(GroupId));
                    Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
                    objTeamMemberProfile.Id = Guid.NewGuid();
                    objTeamMemberProfile.TeamId = objTeam.Id;
                    objTeamMemberProfile.Status = 1;
                    objTeamMemberProfile.ProfileType = "instagram";
                    objTeamMemberProfile.StatusUpdateDate = DateTime.Now;
                    objTeamMemberProfile.ProfileId = objInstagramAccount.InstagramId;

                //Modified [13-02-15]
                    objTeamMemberProfile.ProfilePicUrl = objInstagramAccount.ProfileUrl;
                    objTeamMemberProfile.ProfileName = objInstagramAccount.InsUserName;

                    objTeamMemberProfileRepository.addNewTeamMember(objTeamMemberProfile);
                    #endregion
                    #region SocialProfile
                    Domain.Socioboard.Domain.SocialProfile objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
                    objSocialProfile.Id = Guid.NewGuid();
                    objSocialProfile.ProfileType = "instagram";
                    objSocialProfile.ProfileId = objInstagramAccount.InstagramId;
                    objSocialProfile.UserId = Guid.Parse(UserId);
                    objSocialProfile.ProfileDate = DateTime.Now;
                    objSocialProfile.ProfileStatus = 1;
                    #endregion
                    #region Add SocialProfile
                    if (!objSocialProfilesRepository.checkUserProfileExist(objSocialProfile))
                    {
                        objSocialProfilesRepository.addNewProfileForUser(objSocialProfile);
                    }
                    
                    #endregion
                    ret = "Account Added Successfully";
                }
                else
                {
                    ret = "Account already Exist !";
                }

            GetIntagramImages(objInstagramAccount);
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetIntagramImages(Domain.Socioboard.Domain.InstagramAccount objInsAccount)
        {
            {
                InstagramResponse<GlobusInstagramLib.App.Core.User[]> userinf = new InstagramResponse<GlobusInstagramLib.App.Core.User[]>();
                InstagramResponse<GlobusInstagramLib.App.Core.User[]> userinf1 = new InstagramResponse<GlobusInstagramLib.App.Core.User[]>();
                InstagramResponse<InstagramMedia[]> userinf2 = new InstagramResponse<InstagramMedia[]>();
                InstagramResponse<Comment[]> usercmts = new InstagramResponse<Comment[]>();
                MediaController objMedia = new MediaController();
                CommentController objComment = new CommentController();
                LikesController objLikes = new LikesController();
                objInstagramComment = new Domain.Socioboard.Domain.InstagramComment();
                objInstagramFeed = new Domain.Socioboard.Domain.InstagramFeed();
                string html = string.Empty;
                int i = 0;
                string[] allhtmls = new string[0];
                int countofimages = 0;
                GlobusInstagramLib.Instagram.Core.UsersMethods.Users userInstagram = new GlobusInstagramLib.Instagram.Core.UsersMethods.Users();
              try
            {
                userinf2 = userInstagram.UserRecentMedia(objInsAccount.InstagramId, string.Empty, string.Empty, "20", string.Empty, string.Empty, objInsAccount.AccessToken);

            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
            }


            if (userinf2 != null)
            {
                for (int j = 0; j < userinf2.data.Count(); j++)
                {
                    try
                    {
                        usercmts = objComment.GetComment(userinf2.data[j].id, objInsAccount.AccessToken);
                        bool liked = false;
                        try
                        {
                            //liked = objLikes.LikeToggle(userinf2.data[j].id, objInsAccount.InstagramId, objInsAccount.AccessToken);
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.StackTrace);
                        }
                        int n = usercmts.data.Count();
                        for (int cmt = 0; cmt < usercmts.data.Count(); cmt++)
                        {
                            try
                            {
                                objInstagramComment.Comment = Uri.EscapeDataString(usercmts.data[cmt].text);
                                objInstagramComment.CommentDate = usercmts.data[cmt].created_time.ToString();
                                objInstagramComment.CommentId = usercmts.data[cmt].id;
                                objInstagramComment.EntryDate = DateTime.Now.ToString();
                                objInstagramComment.FeedId = userinf2.data[j].id;
                                objInstagramComment.Id = Guid.NewGuid();
                                objInstagramComment.InstagramId = objInsAccount.InstagramId;
                                objInstagramComment.UserId = objInsAccount.UserId;
                                objInstagramComment.FromName = usercmts.data[cmt].from.full_name;
                                objInstagramComment.FromProfilePic = usercmts.data[cmt].from.profile_picture;
                                if (!objInstagramCommentRepository.checkInstagramCommentExists(usercmts.data[cmt].id, objInsAccount.UserId))
                                    objInstagramCommentRepository.addInstagramComment(objInstagramComment);
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.StackTrace);
                                Console.WriteLine(ex.StackTrace);
                            }
                        }
                        objInstagramFeed.EntryDate = DateTime.Now;
                        try
                        {
                            objInstagramFeed.FeedDate = userinf2.data[j].created_time.ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.StackTrace);
                        }
                        try
                        {
                            objInstagramFeed.FeedId = userinf2.data[j].id;
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.StackTrace);
                        }
                        try
                        {
                            objInstagramFeed.FeedImageUrl = userinf2.data[j].images.low_resolution.url.ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.StackTrace);
                        }
                        try
                        {
                            objInstagramFeed.InstagramId = objInsAccount.InstagramId;
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.StackTrace);
                        }
                        try
                        {
                            objInstagramFeed.LikeCount = userinf2.data[j].likes.count;
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.StackTrace);
                        }
                        try
                        {
                            objInstagramFeed.UserId = objInsAccount.UserId;
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.StackTrace);
                        }
                        try
                        {
                            objInstagramFeed.CommentCount = userinf2.data[j].comments.count;
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.StackTrace);
                        }
                        try
                        {
                            string str = userinf2.data[j].user_has_liked.ToString();

                            if (str == "False")
                            {
                                objInstagramFeed.IsLike = 0;
                            }
                            else { objInstagramFeed.IsLike = 1; }
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.StackTrace);
                        }
                       

                        try
                        {
                            objInstagramFeed.AdminUser = userinf2.data[j].caption.from.username;
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.StackTrace);
                        }


                        if (!objInstagramFeedRepository.checkInstagramFeedExists(userinf2.data[j].id, objInsAccount.UserId))
                            objInstagramFeedRepository.addInstagramFeed(objInstagramFeed);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                    }
                    i++;
                }
            }

            try
            {

                userinf2 = userInstagram.CurrentUserFeed(string.Empty, string.Empty, "20", objInsAccount.AccessToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
            }


            if (userinf2 != null)
            {
                for (int j = 0; j < userinf2.data.Count(); j++)
                {
                    try
                    {
                        usercmts = objComment.GetComment(userinf2.data[j].id, objInsAccount.AccessToken);
                        bool liked = false;
                        try
                        {
                           // liked = objLikes.LikeToggle(userinf2.data[j].id, objInsAccount.InstagramId, objInsAccount.AccessToken);
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.StackTrace);
                        }
                        int n = usercmts.data.Count();
                        for (int cmt = 0; cmt < usercmts.data.Count(); cmt++)
                        {
                            try
                            {
                                objInstagramComment.Comment = Uri.EscapeDataString(usercmts.data[cmt].text);
                                objInstagramComment.CommentDate = usercmts.data[cmt].created_time.ToString();
                                objInstagramComment.CommentId = usercmts.data[cmt].id;
                                objInstagramComment.EntryDate = DateTime.Now.ToString();
                                objInstagramComment.FeedId = userinf2.data[j].id;
                                objInstagramComment.Id = Guid.NewGuid();
                                objInstagramComment.InstagramId = objInsAccount.InstagramId;
                                objInstagramComment.UserId = objInsAccount.UserId;
                                objInstagramComment.FromName = usercmts.data[cmt].from.full_name;
                                objInstagramComment.FromProfilePic = usercmts.data[cmt].from.profile_picture;
                                if (!objInstagramCommentRepository.checkInstagramCommentExists(usercmts.data[cmt].id, objInsAccount.UserId))
                                    objInstagramCommentRepository.addInstagramComment(objInstagramComment);
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.StackTrace);
                                Console.WriteLine(ex.StackTrace);
                            }
                        }
                        objInstagramFeed.EntryDate = DateTime.Now;
                        objInstagramFeed.FeedDate = userinf2.data[j].created_time.ToString();
                        objInstagramFeed.FeedId = userinf2.data[j].id;
                        objInstagramFeed.FeedImageUrl = userinf2.data[j].images.low_resolution.url.ToString();
                        objInstagramFeed.InstagramId = objInsAccount.InstagramId;
                        objInstagramFeed.LikeCount = userinf2.data[j].likes.count;
                        objInstagramFeed.CommentCount = userinf2.data[j].comments.count;
                        string str = userinf2.data[j].user_has_liked.ToString();
                        if (str == "False")
                        {
                            objInstagramFeed.IsLike = 0;
                        }
                        else { objInstagramFeed.IsLike = 1; }
                        objInstagramFeed.AdminUser = userinf2.data[j].caption.from.username + "-" + userinf2.data[j].caption.from.full_name;

                        objInstagramFeed.UserId = objInsAccount.UserId;
                        if (!objInstagramFeedRepository.checkInstagramFeedExists(userinf2.data[j].id, objInsAccount.UserId))
                            objInstagramFeedRepository.addInstagramFeed(objInstagramFeed);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                    }
                    i++;
                }
            }
          
            i++;
            
            string totalhtml = string.Empty;
            try
            {
                for (int k = 0; k < countofimages; k++)
                {
                    totalhtml = totalhtml + allhtmls[k];
                }
            }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
               // Session["AllHtmls"] = allhtmls;
                return totalhtml;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]

        public string InstagramLikeUnLike(string LikeCount, string IsLike, string FeedId, string InstagramId, string UserId)
        {
            string str = string.Empty;
            try
            {
                objInstagramAccount = objInstagramAccountRepository.getInstagramAccountDetailsById(InstagramId, Guid.Parse(UserId));

                LikesController objlikes = new LikesController();
                int islike = Convert.ToInt32(IsLike);
                int LikeCounts = Convert.ToInt32(LikeCount);

                if (islike == 0)
                {
                    islike = 1;
                    bool dd = objlikes.PostUserLike(FeedId,objInstagramAccount.AccessToken);
                    LikeCounts++;
                    objInstagramFeedRepository.UpdateLikesOfProfile(FeedId, islike, LikeCounts);
                    str = "unlike";
               
                }
                else {
                    
                    islike = 0;
                    bool i = objlikes.DeleteLike(FeedId, objInstagramAccount.AccessToken);
                    LikeCounts = LikeCounts - 1;                   
                    objInstagramFeedRepository.UpdateLikesOfProfile(FeedId, islike, LikeCounts);
                    str = "like";
                
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);     
            }
            return str;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllInstagramAccounts()
        {
            try
            {
                ArrayList lstLiAcc = objInstagramAccountRepository.getAllInstagramAccounts();
                return new JavaScriptSerializer().Serialize(lstLiAcc);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateInstagramAccountByAdmin(string ObjInstagram)
        {
            Domain.Socioboard.Domain.InstagramAccount ObjInstagramAccount = (Domain.Socioboard.Domain.InstagramAccount)(new JavaScriptSerializer().Deserialize(ObjInstagram, typeof(Domain.Socioboard.Domain.InstagramAccount)));
            try
            {
                objInstagramAccountRepository.updateInstagramUser(ObjInstagramAccount);
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
