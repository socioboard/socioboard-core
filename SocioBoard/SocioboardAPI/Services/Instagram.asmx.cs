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
using GlobusInstagramLib.Instagram.Core.CommentsMethods;
using Microsoft.Security.Application;
using System.Net;
using NHibernate.Linq;
using System.IO;
using MongoDB.Bson;
using MongoDB.Driver;
using Api.Socioboard.Model;
using System.Threading.Tasks;
using MongoDB.Driver.Builders;
using GlobusInstagramLib.Instagram.Core.RelationshipMethods;

//using System.Text;

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

        InstagramFeedRepository objInstagramFeedRepository = new InstagramFeedRepository();
        Domain.Socioboard.MongoDomain.InstagramFeed objInstagramFeed = new Domain.Socioboard.MongoDomain.InstagramFeed();
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
            GlobusInstagramLib.Authentication.ConfigurationIns configi = new GlobusInstagramLib.Authentication.ConfigurationIns("https://api.instagram.com/oauth/authorize/", ConfigurationManager.AppSettings["InstagramClientKey"], ConfigurationManager.AppSettings["InstagramClientSec"], ConfigurationManager.AppSettings["RedirectUrl"], "http://api.instagram.com/oauth/access_token", "https://api.instagram.com/v1/", "");
            oAuthInstagram _api = new oAuthInstagram();
            _api = oAuthInstagram.GetInstance(configi);
            //GetIntagramImages(objInstagramAccount);
            
            GetInstagramFeeds(objInstagramAccount);
            GetInstagramUserDetails(objInstagramAccount.InstagramId, objInstagramAccount.AccessToken);
            GetInstagramFollowing(objInstagramAccount.UserId.ToString(), objInstagramAccount.InstagramId, objInstagramAccount.AccessToken, 0);
            GetInstagramFollower(objInstagramAccount.UserId.ToString(), objInstagramAccount.InstagramId, objInstagramAccount.AccessToken, 0);
            GetInstagramPostLikes(objInstagramAccount.InstagramId, objInstagramAccount.AccessToken, 0);
            GetInstagramPostComments(objInstagramAccount.InstagramId, objInstagramAccount.AccessToken);
            //GetInstagramUserPosts(objInstagramAccount.InstagramId, objInstagramAccount.AccessToken);
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
        public string getInstaAccounts()
        {
            List<Domain.Socioboard.Domain.InstagramAccount> accounts = new List<Domain.Socioboard.Domain.InstagramAccount>();
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {

                    accounts = session.CreateQuery("from InstagramAccount").List<Domain.Socioboard.Domain.InstagramAccount>().ToList();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                }
            }
            return new JavaScriptSerializer().Serialize(accounts);
        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetInstagramRedirectUrl(string consumerKey, string consumerSecret, string CallBackUrl)
        {
            logger.Error("GetInstagramRedirectUrl");
            string rest = string.Empty;
            GlobusInstagramLib.Authentication.ConfigurationIns config = new GlobusInstagramLib.Authentication.ConfigurationIns("https://instagram.com/oauth/authorize/", consumerKey, consumerSecret, CallBackUrl, "https://api.instagram.com/oauth/access_token", "https://api.instagram.com/v1/", "");
            oAuthInstagram _api = oAuthInstagram.GetInstance(config);
            rest = _api.AuthGetUrl("likes+comments+basic+relationships");
            logger.Error("GetInstagramRedirectUrl => " + rest);
            return rest;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddInstagramAccount(string client_id, string client_secret, string redirect_uri, string UserId, string GroupId, string code)
        {
            string ret = string.Empty;
            oAuthInstagram objInsta = new oAuthInstagram();
            GlobusInstagramLib.Authentication.ConfigurationIns configi = new GlobusInstagramLib.Authentication.ConfigurationIns("https://api.instagram.com/oauth/authorize/", client_id, client_secret, redirect_uri, "http://api.instagram.com/oauth/access_token", "https://api.instagram.com/v1/", "");
            oAuthInstagram _api = new oAuthInstagram();
            _api = oAuthInstagram.GetInstance(configi);
            AccessToken access = new AccessToken();
            access = _api.AuthGetAccessToken(code);

            logger.Error("AddInstagramAccount == >>" + access.user.id + "===" + access.access_token);


            UserController objusercontroller = new UserController();
            try
            {
                #region InstagramAccount
                InstagramResponse<GlobusInstagramLib.App.Core.User> objuser = objusercontroller.GetUserDetails(access.user.id, access.access_token);

                objInstagramAccount = new Domain.Socioboard.Domain.InstagramAccount();
                objInstagramAccount.AccessToken = access.access_token;
                objInstagramAccount.InstagramId = access.user.id;
                try
                {
                    objInstagramAccount.ProfileUrl = access.user.profile_picture;
                }
                catch (Exception ex)
                {
                    logger.Error("Instagram.asmx.cs >> AddInstagramAccount >> " + ex.StackTrace);
                }
                try
                {
                    objInstagramAccount.InsUserName = access.user.username;
                }
                catch (Exception ex)
                {
                    logger.Error("Instagram.asmx.cs >> AddInstagramAccount >> " + ex.StackTrace);
                }
                try
                {
                    objInstagramAccount.TotalImages = objuser.data.counts.media;
                }
                catch (Exception ex)
                {
                    logger.Error("Instagram.asmx.cs >> AddInstagramAccount >> " + ex.StackTrace);
                }
                try
                {
                    objInstagramAccount.FollowedBy = objuser.data.counts.followed_by;
                }
                catch (Exception ex)
                {
                    logger.Error("Instagram.asmx.cs >> AddInstagramAccount >> " + ex.StackTrace);
                }
                try
                {
                    objInstagramAccount.Followers = objuser.data.counts.follows;
                }
                catch (Exception ex)
                {
                    logger.Error("Instagram.asmx.cs >> AddInstagramAccount >> " + ex.StackTrace);
                }
                objInstagramAccount.UserId = Guid.Parse(UserId);
                #endregion


                if (!objInstagramAccountRepository.checkInstagramUserExists(objInstagramAccount.InstagramId, Guid.Parse(UserId)))
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

                //GetIntagramImages(objInstagramAccount);
                GetInstagramFeeds(objInstagramAccount);

                GetInstagramUserDetails(objInstagramAccount.InstagramId, objInstagramAccount.AccessToken);
                GetInstagramFollowing(objInstagramAccount.UserId.ToString(), objInstagramAccount.InstagramId, objInstagramAccount.AccessToken, 1);
                GetInstagramFollower(objInstagramAccount.UserId.ToString(), objInstagramAccount.InstagramId, objInstagramAccount.AccessToken, 1);
                GetInstagramPostLikes(objInstagramAccount.InstagramId, objInstagramAccount.AccessToken, 1);
                GetInstagramPostComments(objInstagramAccount.InstagramId, objInstagramAccount.AccessToken);
                //GetInstagramUserPosts(objInstagramAccount.InstagramId, objInstagramAccount.AccessToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            return ret;
        }

        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string GetIntagramImages(Domain.Socioboard.Domain.InstagramAccount objInsAccount)
        //{
        //    {
        //        InstagramResponse<GlobusInstagramLib.App.Core.User[]> userinf = new InstagramResponse<GlobusInstagramLib.App.Core.User[]>();
        //        InstagramResponse<GlobusInstagramLib.App.Core.User[]> userinf1 = new InstagramResponse<GlobusInstagramLib.App.Core.User[]>();
        //        InstagramResponse<InstagramMedia[]> userinf2 = new InstagramResponse<InstagramMedia[]>();
        //        InstagramResponse<Comment[]> usercmts = new InstagramResponse<Comment[]>();
        //        MediaController objMedia = new MediaController();
        //        CommentController objComment = new CommentController();
        //        LikesController objLikes = new LikesController();
        //        objInstagramComment = new Domain.Socioboard.Domain.InstagramComment();
        //        objInstagramFeed = new Domain.Socioboard.Domain.InstagramFeed();
        //        string html = string.Empty;
        //        int i = 0;
        //        string[] allhtmls = new string[0];
        //        int countofimages = 0;
        //        GlobusInstagramLib.Instagram.Core.UsersMethods.Users userInstagram = new GlobusInstagramLib.Instagram.Core.UsersMethods.Users();
        //        try
        //        {
        //            userinf2 = userInstagram.UserRecentMedia(objInsAccount.InstagramId, string.Empty, string.Empty, "20", string.Empty, string.Empty, objInsAccount.AccessToken);

        //        }
        //        catch (Exception ex)
        //        {
        //            logger.Error(ex.StackTrace);
        //        }


        //        if (userinf2 != null)
        //        {
        //            for (int j = 0; j < userinf2.data.Count(); j++)
        //            {
        //                try
        //                {
        //                    usercmts = objComment.GetComment(userinf2.data[j].id, objInsAccount.AccessToken);
        //                    bool liked = false;
        //                    try
        //                    {
        //                        //liked = objLikes.LikeToggle(userinf2.data[j].id, objInsAccount.InstagramId, objInsAccount.AccessToken);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        logger.Error(ex.StackTrace);
        //                    }
        //                    int n = usercmts.data.Count();
        //                    for (int cmt = 0; cmt < usercmts.data.Count(); cmt++)
        //                    {
        //                        try
        //                        {
        //                            objInstagramComment.Comment = Uri.EscapeDataString(usercmts.data[cmt].text);
        //                            objInstagramComment.CommentDate = usercmts.data[cmt].created_time.ToString();
        //                            objInstagramComment.CommentId = usercmts.data[cmt].id;
        //                            objInstagramComment.EntryDate = DateTime.Now;
        //                            objInstagramComment.FeedId = userinf2.data[j].id;
        //                            objInstagramComment.Id = Guid.NewGuid();
        //                            objInstagramComment.InstagramId = objInsAccount.InstagramId;
        //                            objInstagramComment.UserId = objInsAccount.UserId;
        //                            objInstagramComment.FromName = usercmts.data[cmt].from.full_name;
        //                            objInstagramComment.FromProfilePic = usercmts.data[cmt].from.profile_picture;
        //                            if (!objInstagramCommentRepository.checkInstagramCommentExists(usercmts.data[cmt].id, objInsAccount.UserId))
        //                                objInstagramCommentRepository.addInstagramComment(objInstagramComment);
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            logger.Error(ex.StackTrace);
        //                            Console.WriteLine(ex.StackTrace);
        //                        }
        //                    }
        //                    objInstagramFeed.EntryDate = DateTime.Now;
        //                    try
        //                    {
        //                        objInstagramFeed.FeedDate = userinf2.data[j].created_time.ToString();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        logger.Error(ex.StackTrace);
        //                    }
        //                    try
        //                    {
        //                        objInstagramFeed.FeedId = userinf2.data[j].id;
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        logger.Error(ex.StackTrace);
        //                    }
        //                    try
        //                    {
        //                        objInstagramFeed.FeedImageUrl = userinf2.data[j].images.low_resolution.url.ToString();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        logger.Error(ex.StackTrace);
        //                    }
        //                    try
        //                    {
        //                        objInstagramFeed.InstagramId = objInsAccount.InstagramId;
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        logger.Error(ex.StackTrace);
        //                    }
        //                    try
        //                    {
        //                        objInstagramFeed.LikeCount = userinf2.data[j].likes.count;
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        logger.Error(ex.StackTrace);
        //                    }
        //                    try
        //                    {
        //                        objInstagramFeed.UserId = objInsAccount.UserId;
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        logger.Error(ex.StackTrace);
        //                    }
        //                    try
        //                    {
        //                        objInstagramFeed.CommentCount = userinf2.data[j].comments.count;
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        logger.Error(ex.StackTrace);
        //                    }
        //                    try
        //                    {
        //                        string str = userinf2.data[j].user_has_liked.ToString();

        //                        if (str == "False")
        //                        {
        //                            objInstagramFeed.IsLike = 0;
        //                        }
        //                        else { objInstagramFeed.IsLike = 1; }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        logger.Error(ex.StackTrace);
        //                    }


        //                    try
        //                    {
        //                        objInstagramFeed.AdminUser = userinf2.data[j].caption.from.username;
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        logger.Error(ex.StackTrace);
        //                    }


        //                    if (!objInstagramFeedRepository.checkInstagramFeedExists(userinf2.data[j].id, objInsAccount.UserId))
        //                        objInstagramFeedRepository.addInstagramFeed(objInstagramFeed);
        //                }
        //                catch (Exception ex)
        //                {
        //                    logger.Error(ex.StackTrace);
        //                }
        //                i++;
        //            }
        //        }

        //        try
        //        {

        //            userinf2 = userInstagram.CurrentUserFeed(string.Empty, string.Empty, "20", objInsAccount.AccessToken);
        //        }
        //        catch (Exception ex)
        //        {
        //            logger.Error(ex.StackTrace);
        //        }


        //        if (userinf2 != null)
        //        {
        //            for (int j = 0; j < userinf2.data.Count(); j++)
        //            {
        //                try
        //                {
        //                    usercmts = objComment.GetComment(userinf2.data[j].id, objInsAccount.AccessToken);
        //                    bool liked = false;
        //                    try
        //                    {
        //                        // liked = objLikes.LikeToggle(userinf2.data[j].id, objInsAccount.InstagramId, objInsAccount.AccessToken);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        logger.Error(ex.StackTrace);
        //                    }
        //                    int n = usercmts.data.Count();
        //                    for (int cmt = 0; cmt < usercmts.data.Count(); cmt++)
        //                    {
        //                        try
        //                        {
        //                            objInstagramComment.Comment = Uri.EscapeDataString(usercmts.data[cmt].text);
        //                            objInstagramComment.CommentDate = usercmts.data[cmt].created_time.ToString();
        //                            objInstagramComment.CommentId = usercmts.data[cmt].id;
        //                            objInstagramComment.EntryDate = DateTime.Now;
        //                            objInstagramComment.FeedId = userinf2.data[j].id;
        //                            objInstagramComment.Id = Guid.NewGuid();
        //                            objInstagramComment.InstagramId = objInsAccount.InstagramId;
        //                            objInstagramComment.UserId = objInsAccount.UserId;
        //                            objInstagramComment.FromName = usercmts.data[cmt].from.full_name;
        //                            objInstagramComment.FromProfilePic = usercmts.data[cmt].from.profile_picture;
        //                            if (!objInstagramCommentRepository.checkInstagramCommentExists(usercmts.data[cmt].id, objInsAccount.UserId))
        //                                objInstagramCommentRepository.addInstagramComment(objInstagramComment);
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            logger.Error(ex.StackTrace);
        //                            Console.WriteLine(ex.StackTrace);
        //                        }
        //                    }
        //                    objInstagramFeed.EntryDate = DateTime.Now;
        //                    objInstagramFeed.FeedDate = userinf2.data[j].created_time.ToString();
        //                    objInstagramFeed.FeedId = userinf2.data[j].id;
        //                    objInstagramFeed.FeedImageUrl = userinf2.data[j].images.low_resolution.url.ToString();
        //                    objInstagramFeed.InstagramId = objInsAccount.InstagramId;
        //                    objInstagramFeed.LikeCount = userinf2.data[j].likes.count;
        //                    objInstagramFeed.CommentCount = userinf2.data[j].comments.count;
        //                    string str = userinf2.data[j].user_has_liked.ToString();
        //                    if (str == "False")
        //                    {
        //                        objInstagramFeed.IsLike = 0;
        //                    }
        //                    else { objInstagramFeed.IsLike = 1; }
        //                    objInstagramFeed.AdminUser = userinf2.data[j].caption.from.username + "-" + userinf2.data[j].caption.from.full_name;

        //                    objInstagramFeed.UserId = objInsAccount.UserId;
        //                    if (!objInstagramFeedRepository.checkInstagramFeedExists(userinf2.data[j].id, objInsAccount.UserId))
        //                        objInstagramFeedRepository.addInstagramFeed(objInstagramFeed);
        //                }
        //                catch (Exception ex)
        //                {
        //                    logger.Error(ex.StackTrace);
        //                }
        //                i++;
        //            }
        //        }

        //        i++;

        //        string totalhtml = string.Empty;
        //        try
        //        {
        //            for (int k = 0; k < countofimages; k++)
        //            {
        //                totalhtml = totalhtml + allhtmls[k];
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.StackTrace);
        //        }
        //        // Session["AllHtmls"] = allhtmls;
        //        return totalhtml;
        //    }
        //}

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]

        public string InstagramLikeUnLike(string LikeCount, string IsLike, string FeedId, string InstagramId, string UserId)
        {

            MongoRepository instagramFeedRepo = new MongoRepository("InstagramFeed");
            string str = string.Empty;
            //GlobusInstagramLib.Authentication.ConfigurationIns configi = new GlobusInstagramLib.Authentication.ConfigurationIns("https://api.instagram.com/oauth/authorize/", ConfigurationManager.AppSettings["InstagramClientKey"], ConfigurationManager.AppSettings["InstagramClientSec"], ConfigurationManager.AppSettings["RedirectUrl"], "http://api.instagram.com/oauth/access_token", "https://api.instagram.com/v1/", "");
            //oAuthInstagram _api = new oAuthInstagram();
            //_api = oAuthInstagram.GetInstance(configi);
            try
            {
                objInstagramAccount = objInstagramAccountRepository.getInstagramAccountDetailsById(InstagramId);

                LikesController objlikes = new LikesController();
                int islike = Convert.ToInt32(IsLike);
                int LikeCounts = Convert.ToInt32(LikeCount);

                if (islike == 0)
                {
                    islike = 1;
                    bool dd = objlikes.PostUserLike(FeedId, objInstagramAccount.AccessToken);
                    LikeCounts++;
                    //objInstagramFeedRepository.UpdateLikesOfProfile(FeedId, islike, LikeCounts);
                    str = "unlike";

                }
                else
                {

                    islike = 0;
                    bool i = objlikes.DeleteLike(FeedId, objInstagramAccount.AccessToken);
                    LikeCounts = LikeCounts - 1;
                    //objInstagramFeedRepository.UpdateLikesOfProfile(FeedId, islike, LikeCounts);
                    str = "like";

                }

                FilterDefinition<BsonDocument> filter = new BsonDocument("FeedId", FeedId);
                var update = Builders<BsonDocument>.Update.Set("IsLike", islike).Set("LikeCount", LikeCounts);
                instagramFeedRepo.Update<Domain.Socioboard.MongoDomain.InstagramFeed>(update, filter);
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


        public void GetInstagramFeeds(Domain.Socioboard.Domain.InstagramAccount objInsAccount)
        {
            MongoRepository instagarmCommentRepo = new MongoRepository("InstagramComment");
            MongoRepository instagramFeedRepo = new MongoRepository("InstagramFeed");
            try
            {
                GlobusInstagramLib.Instagram.Core.UsersMethods.Users userInstagram = new GlobusInstagramLib.Instagram.Core.UsersMethods.Users();
                InstagramResponse<InstagramMedia[]> userinf2 = userInstagram.CurrentUserFeed("", "", "30", objInsAccount.AccessToken);
                InstagramResponse<Comment[]> usercmts = new InstagramResponse<Comment[]>();

                CommentController objComment = new CommentController();
                LikesController objLikes = new LikesController();
                if (userinf2 != null)
                {
                    for (int j = 0; j < userinf2.data.Count(); j++)
                    {
                        try
                        {
                            objInstagramFeed = new Domain.Socioboard.MongoDomain.InstagramFeed();
                            //objInstagramFeed.EntryDate = DateTime.Now;
                            try
                            {
                                // objInstagramFeed.Id = ObjectId.GenerateNewId();
                                objInstagramFeed.strId = ObjectId.GenerateNewId().ToString();
                            }
                            catch { }
                            try
                            {
                                objInstagramFeed.FeedDate = userinf2.data[j].created_time.ToString();
                            }
                            catch { }
                            try
                            {
                                objInstagramFeed.FeedId = userinf2.data[j].id;
                            }
                            catch { }
                            try
                            {
                                objInstagramFeed.Type = userinf2.data[j].type.ToString();
                                if (objInstagramFeed.Type == "video")
                                {
                                    objInstagramFeed.VideoUrl = userinf2.data[j].videos.standard_resolution.url.ToString();
                                }
                            }
                            catch { } 
                            try
                            {
                                objInstagramFeed.FeedImageUrl = userinf2.data[j].images.low_resolution.url.ToString();
                            }
                            catch { }
                            try
                            {
                                objInstagramFeed.InstagramId = objInsAccount.InstagramId;
                            }
                            catch { }
                            try
                            {
                                objInstagramFeed.LikeCount = userinf2.data[j].likes.count;
                            }
                            catch { }
                            try
                            {
                                objInstagramFeed.CommentCount = userinf2.data[j].comments.count;
                            }
                            catch { }
                            try
                            {
                                string str = userinf2.data[j].user_has_liked.ToString();
                                if (str == "False")
                                {
                                    objInstagramFeed.IsLike = 0;
                                }
                                else { objInstagramFeed.IsLike = 1; }
                            }
                            catch { }
                            try
                            {
                                objInstagramFeed.AdminUser = userinf2.data[j].caption.from.username;
                            }
                            catch { }
                            try
                            {
                                objInstagramFeed.Feed = userinf2.data[j].caption.text;
                            }
                            catch { }
                            try
                            {
                                objInstagramFeed.ImageUrl = userinf2.data[j].caption.from.profile_picture;
                            }
                            catch { }
                            try
                            {
                                objInstagramFeed.FromId = userinf2.data[j].caption.from.id;
                            }
                            catch { }
                            try
                            {
                                objInstagramFeed.FeedUrl = userinf2.data[j].link;
                            }
                            catch { }
                            //try
                            //{
                            //    objInstagramFeed.UserId = objInsAccount.UserId.;
                            //}
                            //catch { }

                            var ret = instagramFeedRepo.Find<Domain.Socioboard.MongoDomain.InstagramFeed>(t => t.FeedId.Equals(objInstagramFeed.FeedId) && t.InstagramId.Equals(objInstagramFeed.InstagramId));
                            var task = Task.Run(async () =>
                            {
                                return await ret;
                            });
                            int count = task.Result.Count;

                            if (count < 1)
                            {
                                instagramFeedRepo.Add(objInstagramFeed);
                            }
                            else
                            {
                                FilterDefinition<BsonDocument> filter = new BsonDocument("FeedId", objInstagramFeed.FeedId);
                                var update = Builders<BsonDocument>.Update.Set("IsLike", objInstagramFeed.IsLike).Set("CommentCount", objInstagramFeed.CommentCount).Set("LikeCount", objInstagramFeed.LikeCount).Set("Type", objInstagramFeed.Type).Set("VideoUrl", objInstagramFeed.VideoUrl);
                                instagramFeedRepo.Update<Domain.Socioboard.MongoDomain.InstagramFeed>(update, filter);
                            }

                            //if (!objInstagramFeedRepository.checkInstagramFeedExists(userinf2.data[j].id, objInsAccount.UserId))
                            //{
                            //    objInstagramFeedRepository.addInstagramFeed(objInstagramFeed);
                            //}
                            List<Domain.Socioboard.MongoDomain.InstagramComment> lstInstagramComment = new List<Domain.Socioboard.MongoDomain.InstagramComment>();
                            usercmts = objComment.GetComment(userinf2.data[j].id, objInsAccount.AccessToken);
                            for (int cmt = 0; cmt < usercmts.data.Count(); cmt++)
                            {
                                try
                                {

                                    Domain.Socioboard.MongoDomain.InstagramComment objInstagramComment = new Domain.Socioboard.MongoDomain.InstagramComment();

                                    try
                                    {
                                        objInstagramComment.Comment = usercmts.data[cmt].text;
                                    }
                                    catch { }
                                    try
                                    {
                                        objInstagramComment.CommentDate = usercmts.data[cmt].created_time.ToString();
                                    }
                                    catch { }
                                    try
                                    {
                                        objInstagramComment.CommentId = usercmts.data[cmt].id;
                                    }
                                    catch { }
                                    //try
                                    //{
                                    //    objInstagramComment.EntryDate = DateTime.UtcNow.ToString();
                                    //}
                                    //catch { }
                                    try
                                    {
                                        objInstagramComment.FeedId = userinf2.data[j].id;
                                    }
                                    catch { }
                                    try
                                    {
                                        objInstagramComment.Id = ObjectId.GenerateNewId();
                                        objInstagramComment.strId = ObjectId.GenerateNewId().ToString();
                                    }
                                    catch { }
                                    try
                                    {
                                        objInstagramComment.InstagramId = objInsAccount.InstagramId;
                                    }
                                    catch { }
                                    try
                                    {
                                        // objInstagramComment.UserId = objInsAccount.UserId;
                                    }
                                    catch { }
                                    try
                                    {
                                        objInstagramComment.FromName = usercmts.data[cmt].from.username;
                                    }
                                    catch { }
                                    try
                                    {
                                        objInstagramComment.FromProfilePic = usercmts.data[cmt].from.profile_picture;
                                    }
                                    catch { }

                                    lstInstagramComment.Add(objInstagramComment);
                                }
                                catch (Exception ex)
                                {
                                    logger.Error(ex.StackTrace);
                                }
                            }
                            instagarmCommentRepo.AddList(lstInstagramComment);

                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        [WebMethod]
        public string AddComment(string UserId, string FeedId, string Text, string InstagramId)
        {
            MongoRepository instagarmCommentRepo = new MongoRepository("InstagramComment");
            Domain.Socioboard.MongoDomain.InstagramComment _InstagramComment = new Domain.Socioboard.MongoDomain.InstagramComment();
            //GlobusInstagramLib.Authentication.ConfigurationIns configi = new GlobusInstagramLib.Authentication.ConfigurationIns("https://api.instagram.com/oauth/authorize/", ConfigurationManager.AppSettings["InstagramClientKey"], ConfigurationManager.AppSettings["InstagramClientSec"], ConfigurationManager.AppSettings["RedirectUrl"], "http://api.instagram.com/oauth/access_token", "https://api.instagram.com/v1/", "");
            //oAuthInstagram _api = new oAuthInstagram();
            //_api = oAuthInstagram.GetInstance(configi);
            objInstagramAccount = objInstagramAccountRepository.getInstagramAccountDetailsById(InstagramId);
            CommentController objComment = new CommentController();
            string ret = objComment.PostCommentAdd(FeedId, Text, objInstagramAccount.AccessToken);

            if (!string.IsNullOrEmpty(ret))
            {
                try
                {
                    JObject JData = JObject.Parse(ret);
                    string commentid = JData["data"]["id"].ToString();
                    string time = JData["data"]["created_time"].ToString();
                    string profilepic = JData["data"]["from"]["profile_picture"].ToString();
                    string username = JData["data"]["from"]["username"].ToString();
                    _InstagramComment.Id = ObjectId.GenerateNewId();
                    _InstagramComment.strId = ObjectId.GenerateNewId().ToString();
                    _InstagramComment.FeedId = FeedId;
                    _InstagramComment.InstagramId = InstagramId;
                    _InstagramComment.FromProfilePic = profilepic;
                    _InstagramComment.FromName = username;
                    _InstagramComment.CommentDate = time;
                    _InstagramComment.Comment = Text;
                    _InstagramComment.CommentId = commentid;
                    //objInstagramCommentRepository.addInstagramComment(_InstagramComment);

                    instagarmCommentRepo.Add(_InstagramComment);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return null;
                }

            }
            else { return null; }

            return new JavaScriptSerializer().Serialize(_InstagramComment);


        }
        [WebMethod]
        public string PostFollow(string FollowingId, string FollowingName, string FromId, string AccessToken)
        {
            Relationship _Relationship = new Relationship();
            //objInstagramAccount = objInstagramAccountRepository.getInstagramAccountDetailsById(InstagramId);
            string ret = _Relationship.FollowPost(FollowingId, AccessToken);
            return "success";
        }
        [WebMethod]
        public string PostUnfollow(string FollowingId, string FollowingName, string FromId, string AccessToken)
        {
            Relationship _Relationship=new Relationship(); 
            //objInstagramAccount = objInstagramAccountRepository.getInstagramAccountDetailsById(InstagramId);
            string ret = _Relationship.UnfollowPost(FollowingId, AccessToken);
            return "success";
        }
        [WebMethod]
        public string PostBlock(string FollowingId, string InstagramId)
        {

            Relationship _Relationship = new Relationship();
            objInstagramAccount = objInstagramAccountRepository.getInstagramAccountDetailsById(InstagramId);
            string ret = _Relationship.BlockUserPost(FollowingId, objInstagramAccount.AccessToken);
            return "";
        }
        [WebMethod]
        public string GetInstagramUserPosts(string profile_id, string access_token)
        {
            string code_status = "false";
            Domain.Socioboard.Domain.InstagramSelfFeed send_data = new Domain.Socioboard.Domain.InstagramSelfFeed();
            string url = "https://api.instagram.com/v1/users/" + profile_id + "/media/recent?access_token=" + access_token;
            try
            {
                JObject post_data = JObject.Parse(ApiInstagramHttp(url));

                try
                {
                    dynamic items = post_data["data"];
                    foreach (var item in items)
                    {
                        Guid Id = Guid.NewGuid();

                        string post_url = string.Empty;
                        string feed_url = string.Empty;
                        string user_name = string.Empty;
                        string type = item["type"].ToString();
                        string feed_id = item["id"].ToString();
                        string created_time = item["created_time"].ToString();
                        DateTime create_time = DateExtension.ToDateTime(DateTime.Now, long.Parse(created_time));
                        if (create_time.Date >= DateTime.Now.AddDays(-90).Date)
                        {
                            if (type.Equals("video"))
                            {
                                try
                                {
                                    post_url = item["videos"]["standard_resolution"]["url"].ToString();
                                    feed_url = item["link"].ToString();
                                    user_name = item["user"]["username"].ToString();
                                }
                                catch (Exception)
                                {
                                }

                            }
                            else if (type.Equals("image"))
                            {
                                try
                                {
                                    post_url = item["images"]["standard_resolution"]["url"].ToString();
                                    feed_url = item["link"].ToString();
                                    user_name = item["user"]["username"].ToString();
                                }
                                catch (Exception)
                                {
                                }
                            }

                            send_data.Id = Id;

                            send_data.ProfileId = profile_id;
                            send_data.FeedId = feed_id;
                            send_data.Accesstoken = access_token;
                            send_data.Post_url = post_url;
                            send_data.Link = feed_url;
                            send_data.Type = type;
                            send_data.Created_Time = create_time;
                            //string i = new JavaScriptSerializer().Serialize(send_data);
                            insertdata(send_data);
                            code_status = "true";
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }

                try
                {
                    dynamic pagination_url = post_data["pagination"];

                    string next_url = string.Empty;

                    if (pagination_url != null)
                    {
                        try
                        {
                            next_url = post_data["pagination"]["next_url"].ToString();
                        }
                        catch (Exception)
                        {
                            pagination_url = null;
                        }
                        while (pagination_url != null)
                        {
                            JObject post_data_next = JObject.Parse(ApiInstagramHttp(next_url));
                            try
                            {
                                next_url = post_data_next["pagination"]["next_url"].ToString();
                                pagination_url = post_data_next["pagination"];
                            }
                            catch (Exception)
                            {
                                pagination_url = null;
                            }
                            try
                            {
                                dynamic items_next = post_data_next["data"];
                                foreach (var item in items_next)
                                {
                                    Guid Id = Guid.NewGuid();
                                    string post_url = string.Empty;
                                    string feed_url = string.Empty;
                                    string user_name = string.Empty;
                                    string feed_id = item["id"].ToString();
                                    string type = item["type"].ToString();
                                    string created_time = item["created_time"].ToString();
                                    DateTime create_time = DateExtension.ToDateTime(DateTime.Now, long.Parse(created_time));
                                    if (create_time.Date >= DateTime.Now.AddDays(-90).Date)
                                    {
                                        if (type.Equals("video"))
                                        {
                                            try
                                            {
                                                post_url = item["videos"]["standard_resolution"]["url"].ToString();
                                                feed_url = item["link"].ToString();
                                                user_name = item["user"]["username"].ToString();
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }
                                        else if (type.Equals("image"))
                                        {
                                            try
                                            {
                                                post_url = item["images"]["standard_resolution"]["url"].ToString();
                                                feed_url = item["link"].ToString();
                                                user_name = item["user"]["username"].ToString();
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }
                                        send_data.Id = Id;
                                        send_data.ProfileId = profile_id;
                                        send_data.FeedId = feed_id;
                                        send_data.Accesstoken = access_token;
                                        send_data.Post_url = post_url;
                                        send_data.Link = feed_url;
                                        send_data.Type = type;
                                        send_data.Created_Time = create_time;
                                        //string i = new JavaScriptSerializer().Serialize(send_data);
                                        insertdata(send_data);
                                        code_status = "true";
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.Message);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
            catch (Exception ex)
            {

            }
            return code_status;
        }




        public string ApiInstagramHttp(string url)
        {
            try
            {

                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";
                httpRequest.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse httResponse = (HttpWebResponse)httpRequest.GetResponse();
                Stream responseStream = httResponse.GetResponseStream();
                StreamReader responseStreamReader = new StreamReader(responseStream, System.Text.Encoding.Default);
                string pageContent = responseStreamReader.ReadToEnd();
                responseStreamReader.Close();
                responseStream.Close();
                httResponse.Close();
                return pageContent;
            }
            catch (Exception ex)
            {
                return "";
            }

        }


        public void insertdata(Domain.Socioboard.Domain.InstagramSelfFeed insert)
        {

            //Domain.Socioboard.Domain.InstagramSelfFeed insert = (Domain.Socioboard.Domain.InstagramSelfFeed)new JavaScriptSerializer().Deserialize(i, typeof(Domain.Socioboard.Domain.InstagramSelfFeed));

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        //Proceed action, to save data.
                        bool exist = session.Query<Domain.Socioboard.Domain.InstagramSelfFeed>()
                           .Any(x => x.FeedId == insert.FeedId);
                        if (!exist)
                        {
                            session.Save(insert);
                            transaction.Commit();

                        }

                    }// End Using Trasaction
                }
                catch (Exception e)
                {
                    logger.Error("notinserteddata>>");
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                }
            }// End using session

        }

        [WebMethod]
        public string GetInstagramPostComments(string profile_id, string access_token)
        {
            string code_status = "false";
            Domain.Socioboard.Domain.InstagramPostComments insert = new Domain.Socioboard.Domain.InstagramPostComments();
            JObject post_data = new JObject();
            string url = "https://api.instagram.com/v1/users/" + profile_id + "/media/recent?access_token=" + access_token+"&count=100";
            try
            {
                post_data = JObject.Parse(ApiInstagramHttp(url));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                dynamic items = post_data["data"];
                foreach (var item in items)
                {

                    Guid Id = Guid.NewGuid();
                    string feed_id = item["id"].ToString();
                    string feed_type = item["type"].ToString();
                    string created_time_feed = item["created_time"].ToString();
                    DateTime create_time_feed = DateExtension.ToDateTime(DateTime.Now, long.Parse(created_time_feed));
                    if (create_time_feed.Date >= DateTime.Now.AddDays(-90).Date)
                    {
                        dynamic comments = item["comments"]["data"];

                        foreach (var comment in comments)
                        {

                            string created_time = comment["created_time"].ToString();
                            DateTime create_time = DateExtension.ToDateTime(DateTime.Now, long.Parse(created_time));
                            string text = comment["text"].ToString();
                            string commented_by_id = comment["from"]["id"].ToString();
                            string commented_by_name = comment["from"]["username"].ToString();
                            string comment_id = comment["id"].ToString();

                            insert.Id = Id;
                            insert.Profile_Id = profile_id;
                            insert.Feed_Id = feed_id;
                            insert.Commented_By_Id = commented_by_id;
                            insert.Commented_By_Name = commented_by_name;
                            insert.Created_Time = create_time;
                            insert.Comment_Id = comment_id;
                            insert.Comment = text;
                            insert.Feed_Type = feed_type;
                            //string i = new JavaScriptSerializer().Serialize(insert);
                            insertpostcomments(insert);
                            code_status = "true";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            //try
            //{
            //    dynamic pagination_url = post_data["pagination"];

            //    string next_url = string.Empty;

            //    if (pagination_url != null)
            //    {
            //        try
            //        {

            //            next_url = post_data["pagination"]["next_url"].ToString();

            //        }
            //        catch (Exception)
            //        {

            //            pagination_url = null;

            //        }
            //        while (pagination_url != null)
            //        {



            //            JObject post_data_next = JObject.Parse(ApiInstagramHttp(next_url));

            //            try
            //            {
            //                next_url = post_data_next["pagination"]["next_url"].ToString();

            //                pagination_url = post_data_next["pagination"];

            //            }
            //            catch (Exception)
            //            {

            //                pagination_url = null;
            //            }

            //            try
            //            {
            //                dynamic items_next = post_data_next["data"];
            //                foreach (var item in items_next)
            //                {

            //                    Guid Id = Guid.NewGuid();
            //                    string feed_id = item["id"].ToString();
            //                    string feed_type = item["type"].ToString();
            //                    string created_time_feed = item["created_time"].ToString();
            //                    DateTime create_time_feed = DateExtension.ToDateTime(DateTime.Now, long.Parse(created_time_feed));
            //                    if (create_time_feed.Date >= DateTime.Now.AddDays(-90).Date)
            //                    {
            //                        try
            //                        {
            //                            dynamic comments = item["comments"]["data"];
            //                            foreach (var comment in comments)
            //                            {
            //                                try
            //                                {
            //                                    string created_time = comment["created_time"].ToString();
            //                                    DateTime create_time = DateExtension.ToDateTime(DateTime.Now, long.Parse(created_time));
            //                                    string text = comment["text"].ToString();
            //                                    string commented_by_id = comment["from"]["id"].ToString();
            //                                    string commented_by_name = comment["from"]["username"].ToString();
            //                                    string comment_id = comment["id"].ToString();

            //                                    insert.Id = Id;
            //                                    insert.Profile_Id = profile_id;
            //                                    insert.Feed_Id = feed_id;
            //                                    insert.Commented_By_Id = commented_by_id;
            //                                    insert.Commented_By_Name = commented_by_name;
            //                                    insert.Created_Time = create_time;
            //                                    insert.Comment_Id = comment_id;
            //                                    insert.Feed_Type = feed_type;
            //                                    insert.Comment = text;
            //                                    //string i = new JavaScriptSerializer().Serialize(insert);
            //                                    insertpostcomments(insert);
            //                                    code_status = "true";
            //                                }
            //                                catch (Exception ex)
            //                                {
            //                                    logger.Error(ex.Message);
            //                                }
            //                            }
            //                        }
            //                        catch (Exception ex)
            //                        {
            //                            logger.Error(ex.Message);
            //                        }
            //                    }
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                logger.Error(ex.Message);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.Message);
            //}

            return code_status;

        }


        [WebMethod]
        public string GetInstagramPostLikes(string profile_id, string access_token, int status)
        {
            string code_status = "false";
            Domain.Socioboard.Domain.InstagramPostLikes insert = new Domain.Socioboard.Domain.InstagramPostLikes();
            JObject post_data = new JObject();
            string url = "https://api.instagram.com/v1/users/" + profile_id + "/media/recent?access_token=" + access_token + "&count=30";
            try
            {
                post_data = JObject.Parse(ApiInstagramHttp(url));
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            try
            {
                dynamic items = post_data["data"];
                foreach (var item in items)
                {
                    string post_url = string.Empty;
                    string feed_url = string.Empty;
                    string user_name = string.Empty;
                    Guid Id = Guid.NewGuid();
                    string feed_id = item["id"].ToString();
                    string feed_type = item["type"].ToString();
                    string created_time_feed = item["created_time"].ToString();
                    DateTime create_time_feed = DateExtension.ToDateTime(DateTime.Now, long.Parse(created_time_feed));

                    Domain.Socioboard.Domain.InstagramSelfFeed send_data = new Domain.Socioboard.Domain.InstagramSelfFeed();

                    try
                    {
                        if (feed_type.Equals("video"))
                        {
                            try
                            {
                                post_url = item["videos"]["standard_resolution"]["url"].ToString();
                                feed_url = item["link"].ToString();
                                user_name = item["user"]["username"].ToString();
                            }
                            catch (Exception)
                            {
                            }

                        }
                        else if (feed_type.Equals("image"))
                        {
                            try
                            {
                                post_url = item["images"]["standard_resolution"]["url"].ToString();
                                feed_url = item["link"].ToString();
                                user_name = item["user"]["username"].ToString();
                            }
                            catch (Exception)
                            {
                            }
                        }

                        send_data.Id = Id;

                        send_data.ProfileId = profile_id;
                        send_data.FeedId = feed_id;
                        send_data.Accesstoken = access_token;
                        send_data.Post_url = post_url;
                        send_data.Link = feed_url;
                        send_data.Type = feed_type;
                        send_data.Created_Time = create_time_feed;
                        //string i = new JavaScriptSerializer().Serialize(send_data);
                        insertdata(send_data);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }



                    try
                    {
                        dynamic likes = item["likes"]["data"];
                        foreach (var like in likes)
                        {
                            try
                            {
                                string liked_by_id = like["id"].ToString();
                                string liked_by_name = like["username"].ToString();
                                insert.Id = Id;
                                insert.Profile_Id = profile_id;
                                insert.Feed_Id = feed_id;
                                insert.Liked_By_Id = liked_by_id;
                                insert.Liked_By_Name = liked_by_name;
                                insert.Feed_Type = feed_type;
                                insert.Created_Date = DateTime.Now;
                                insert.Status = status;
                                //string i = new JavaScriptSerializer().Serialize(insert);
                                insertpostlikes(insert);
                                code_status = "true";
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
            }
            //}
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            //try
            //{
            //    dynamic pagination_url = post_data["pagination"];
            //    string next_url = string.Empty;
            //    if (pagination_url != null)
            //    {
            //        try
            //        {
            //            next_url = post_data["pagination"]["next_url"].ToString();
            //        }
            //        catch (Exception)
            //        {
            //            pagination_url = null;
            //        }
            //        while (pagination_url != null)
            //        {
            //            JObject post_data_next = JObject.Parse(ApiInstagramHttp(next_url));
            //            try
            //            {
            //                next_url = post_data_next["pagination"]["next_url"].ToString();
            //                pagination_url = post_data_next["pagination"];
            //            }
            //            catch (Exception)
            //            {
            //                pagination_url = null;
            //            }
            //            try
            //            {
            //                dynamic items_next = post_data_next["data"];
            //                foreach (var item in items_next)
            //                {
            //                    Guid Id = Guid.NewGuid();
            //                    string feed_id = item["id"].ToString();
            //                    string feed_type = item["type"].ToString();
            //                    string created_time_feed = item["created_time"].ToString();
            //                    DateTime create_time_feed = DateExtension.ToDateTime(DateTime.Now, long.Parse(created_time_feed));

            //                    //if (create_time_feed.Date >= DateTime.Now.AddDays(-90).Date)
            //                    //{
            //                    try
            //                    {
            //                        dynamic likes = item["likes"]["data"];
            //                        foreach (var like in likes)
            //                        {
            //                            try
            //                            {
            //                                string liked_by_id = like["id"].ToString();
            //                                string liked_by_name = like["username"].ToString();
            //                                insert.Id = Id;
            //                                insert.Profile_Id = profile_id;
            //                                insert.Feed_Id = feed_id;
            //                                insert.Liked_By_Id = liked_by_id;
            //                                insert.Liked_By_Name = liked_by_name;
            //                                insert.Feed_Type = feed_type;
            //                                //string i = new JavaScriptSerializer().Serialize(insert);
            //                                insertpostlikes(insert);
            //                                code_status = "true";
            //                            }
            //                            catch (Exception ex)
            //                            {
            //                                logger.Error(ex.Message);
            //                            }
            //                        }
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        logger.Error(ex.Message);
            //                    }
            //                }
            //            }
            //            //}
            //            catch (Exception ex)
            //            {
            //                logger.Error(ex.Message);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.Message);
            //}

            return code_status;

        }



        [WebMethod]
        public string GetInstagramFollower(string userid, string profile_id, string access_token, int status)
        {
            string code_status = "false";
            Domain.Socioboard.Domain.InboxMessages insert = new Domain.Socioboard.Domain.InboxMessages();
            JObject post_data = new JObject();
            string url = "https://api.instagram.com/v1/users/" + profile_id + "/followed-by?access_token=" + access_token + "&cout=100";
            try
            {
                post_data = JObject.Parse(ApiInstagramHttp(url));
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            try
            {
                dynamic items = post_data["data"];
                foreach (var item in items)
                {
                    try
                    {
                        Guid Id = Guid.NewGuid();
                        string user_name = item["username"].ToString();
                        string id = item["id"].ToString();
                        string full_name = item["full_name"].ToString();
                        string image_url = item["profile_picture"].ToString();
                        DateTime CreatedTime = DateTime.Now;
                        insert.Id = Id;
                        insert.UserId = Guid.Parse(userid);
                        insert.MessageId = "";
                        insert.ProfileId = profile_id;
                        insert.FromId = id;
                        insert.FromName = user_name;
                        insert.RecipientId = profile_id;
                        insert.RecipientName = "";
                        insert.Message = "";
                        insert.FromImageUrl = image_url;
                        insert.RecipientImageUrl = "";
                        insert.ProfileType = "insta";
                        insert.MessageType = "insta_follower";
                        insert.CreatedTime = CreatedTime;
                        insert.EntryTime = CreatedTime;
                        insert.FollowerCount = "";
                        insert.FollowingCount = "";
                        insert.Status = status;
                        //string i = new JavaScriptSerializer().Serialize(insert);
                        insertfollowerfollowingdata(insert);
                        code_status = "true";
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

            //try
            //{
            //    dynamic pagination_url = post_data["pagination"];
            //    string next_url = string.Empty;
            //    if (pagination_url != null)
            //    {
            //        try
            //        {
            //            next_url = post_data["pagination"]["next_url"].ToString();
            //        }
            //        catch (Exception)
            //        {
            //            pagination_url = null;
            //        }
            //        while (pagination_url != null)
            //        {
            //            JObject post_data_next = JObject.Parse(ApiInstagramHttp(next_url));
            //            try
            //            {
            //                next_url = post_data_next["pagination"]["next_url"].ToString();
            //                pagination_url = post_data_next["pagination"];
            //            }
            //            catch (Exception)
            //            {
            //                pagination_url = null;
            //            }
            //            try
            //            {
            //                dynamic items_next = post_data_next["data"];
            //                foreach (var item in items_next)
            //                {
            //                    try
            //                    {
            //                        Guid Id = Guid.NewGuid();
            //                        string user_name = item["username"].ToString();
            //                        string id = item["id"].ToString();
            //                        string full_name = item["full_name"].ToString();
            //                        string image_url = item["profile_picture"].ToString();
            //                        DateTime CreatedTime = DateTime.Now;
            //                        insert.Id = Id;
            //                        insert.UserId = Guid.Parse(userid);
            //                        insert.MessageId = "";
            //                        insert.ProfileId = profile_id;
            //                        insert.FromId = id;
            //                        insert.FromName = user_name;
            //                        insert.RecipientId = profile_id;
            //                        insert.RecipientName = "";
            //                        insert.Message = "";
            //                        insert.FromImageUrl = image_url;
            //                        insert.RecipientImageUrl = "";
            //                        insert.ProfileType = "insta";
            //                        insert.MessageType = "insta_follower";
            //                        insert.CreatedTime = CreatedTime;
            //                        insert.EntryTime = CreatedTime;
            //                        insert.FollowerCount = "";
            //                        insert.FollowingCount = "";
            //                        insert.Status = status;
            //                        //string i = new JavaScriptSerializer().Serialize(insert);
            //                        insertfollowerfollowingdata(insert);
            //                        code_status = "true";
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        logger.Error(ex.Message);
            //                    }
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                logger.Error(ex.Message);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.Message);
            //}
            return code_status;
        }


        [WebMethod]
        public string GetInstagramFollowing(string userid, string profile_id, string access_token, int status)
        {
            string code_status = "false";
            Domain.Socioboard.Domain.InboxMessages insert = new Domain.Socioboard.Domain.InboxMessages();
            JObject post_data = new JObject();
            string url = "https://api.instagram.com/v1/users/" + profile_id + "/follows?access_token=" + access_token + "&count=100";
            try
            {
                post_data = JObject.Parse(ApiInstagramHttp(url));

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            try
            {
                dynamic items = post_data["data"];
                foreach (var item in items)
                {
                    try
                    {
                        Guid Id = Guid.NewGuid();

                        string user_name = item["username"].ToString();
                        string id = item["id"].ToString();
                        string full_name = item["full_name"].ToString();
                        DateTime CreatedTime = DateTime.Now;

                        insert.Id = Id;
                        insert.UserId = Guid.Parse(userid);
                        insert.MessageId = "";
                        insert.ProfileId = profile_id;
                        insert.FromId = profile_id;
                        insert.FromName = "";
                        insert.RecipientId = id;
                        insert.RecipientName = full_name;
                        insert.Message = "";
                        insert.FromImageUrl = "";
                        insert.RecipientImageUrl = "";
                        insert.ProfileType = "insta";
                        insert.MessageType = "insta_following";
                        insert.CreatedTime = CreatedTime;
                        insert.EntryTime = CreatedTime;
                        insert.FollowerCount = "";
                        insert.FollowingCount = "";
                        insert.Status = status;
                        //string i = new JavaScriptSerializer().Serialize(insert);
                        insertfollowerfollowingdata(insert);
                        code_status = "true";
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
            //try
            //{
            //    dynamic pagination_url = post_data["pagination"];
            //    string next_url = string.Empty;
            //    if (pagination_url != null)
            //    {
            //        try
            //        {
            //            next_url = post_data["pagination"]["next_url"].ToString();

            //        }
            //        catch (Exception)
            //        {
            //            pagination_url = null;
            //        }
            //        while (pagination_url != null)
            //        {
            //            JObject post_data_next = JObject.Parse(ApiInstagramHttp(next_url));
            //            try
            //            {
            //                next_url = post_data_next["pagination"]["next_url"].ToString();
            //                pagination_url = post_data_next["pagination"];
            //            }
            //            catch (Exception)
            //            {
            //                pagination_url = null;
            //            }

            //            try
            //            {
            //                dynamic items_next = post_data_next["data"];

            //                foreach (var item in items_next)
            //                {
            //                    try
            //                    {
            //                        Guid Id = Guid.NewGuid();
            //                        string user_name = item["username"].ToString();
            //                        string id = item["id"].ToString();
            //                        string full_name = item["full_name"].ToString();
            //                        DateTime CreatedTime = DateTime.Now;
            //                        insert.Id = Id;
            //                        insert.UserId = Guid.Parse(userid);
            //                        insert.MessageId = "";
            //                        insert.ProfileId = id;
            //                        insert.FromId = profile_id;
            //                        insert.FromName = "";
            //                        insert.RecipientId = id;
            //                        insert.RecipientName = full_name;
            //                        insert.Message = "";
            //                        insert.FromImageUrl = "";
            //                        insert.RecipientImageUrl = "";
            //                        insert.ProfileType = "insta";
            //                        insert.MessageType = "insta_following";
            //                        insert.CreatedTime = CreatedTime;
            //                        insert.EntryTime = CreatedTime;
            //                        insert.FollowerCount = "";
            //                        insert.FollowingCount = "";
            //                        insert.Status = status;
            //                        //string i = new JavaScriptSerializer().Serialize(insert);
            //                        insertfollowerfollowingdata(insert);
            //                        code_status = "true";
            //                    }
            //                    catch (Exception)
            //                    {
            //                    }
            //                }
            //            }
            //            catch (Exception)
            //            {
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.Message);
            //}
            return code_status;
        }

        [WebMethod]
        public string GetInstagramUserDetails(string profile_id, string access_token)
        {
            string code_status = "false";
            Domain.Socioboard.Domain.InstagramUserDetails insert = new Domain.Socioboard.Domain.InstagramUserDetails();
            JObject post_data = new JObject();
            string url = "https://api.instagram.com/v1/users/" + profile_id + "?access_token=" + access_token;

            try
            {
                post_data = JObject.Parse(ApiInstagramHttp(url));
            }
            catch (Exception)
            {
            }

            try
            {
                dynamic item = post_data["data"];
                //foreach (var item in items)
                //{

                try
                {
                    Guid Id = Guid.NewGuid();
                    string insta_name = item["username"].ToString();
                    string full_name = item["full_name"].ToString();
                    string imageUrl = item["profile_picture"].ToString();
                    string media_count = item["counts"]["media"].ToString();
                    DateTime Created_Time = DateTime.Now;
                    string follower = item["counts"]["followed_by"].ToString();
                    string following = item["counts"]["follows"].ToString();
                    insert.Id = Id;
                    insert.Profile_Id = profile_id;
                    insert.Insta_Name = insta_name;
                    insert.Full_Name = full_name;
                    insert.Media_Count = media_count;
                    insert.Created_Time = Created_Time;
                    insert.Follower = follower;
                    insert.Following = following;

                    try
                    {
                        objInstagramAccountRepository.updateInstagramAccount(media_count, follower, following, insta_name, imageUrl, profile_id);
                    }
                    catch { }

                    string i = new JavaScriptSerializer().Serialize(insert);

                    DateTime t1 = DateTime.Now.Date;
                    DateTime t2 = DateTime.Now.Date.AddHours(12);
                    DateTime t3 = DateTime.Now.AddDays(1).Date.AddSeconds(-1);
                    if (DateTime.Now.TimeOfDay >= t1.TimeOfDay && DateTime.Now.TimeOfDay < t2.TimeOfDay)
                    {
                        if (objInstagramAccountRepository.IsInstagramAccountExistsFirst(profile_id))
                        {
                            objInstagramAccountRepository.UpdateInstagramAccountFollowerFirst(insert);
                        }
                        else
                        {
                            insertuserdetails(insert);
                        }
                    }
                    if (DateTime.Now.TimeOfDay >= t2.TimeOfDay && DateTime.Now.TimeOfDay < t3.TimeOfDay)
                    {
                        if (objInstagramAccountRepository.IsInstagramAccountExistsSecond(profile_id))
                        {
                            objInstagramAccountRepository.UpdateInstagramAccountFollowerSecond(insert);
                        }
                        else
                        {
                            insertuserdetails(insert);
                        }
                    }

                    //insertuserdetails(insert);
                    code_status = "true";
                                      


                }
                catch (Exception ex)
                {
                }

                //}
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            return code_status;
        }



        public void insertpostcomments(Domain.Socioboard.Domain.InstagramPostComments insert)
        {

            //Domain.Socioboard.Domain.InstagramPostComments insert = (Domain.Socioboard.Domain.InstagramPostComments)new JavaScriptSerializer().Deserialize(i, typeof(Domain.Socioboard.Domain.InstagramPostComments));

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        //Proceed action, to save data.
                        try
                        {
                            bool exist = session.Query<Domain.Socioboard.Domain.InstagramPostComments>()
                                              .Any(x => x.Comment_Id == insert.Comment_Id && x.Profile_Id == insert.Profile_Id);
                            if (exist)
                            {
                                int _i = session.CreateQuery("Update InstagramPostComments set Comment = : Comment , Created_Time = : Created_Time where Comment_Id=:Comment_Id and Profile_Id=:Profile_Id")
                                    .SetParameter("Comment", insert.Comment)
                                    .SetParameter("Created_Time", insert.Created_Time)
                                    .SetParameter("Comment_Id", insert.Comment_Id)
                                    .SetParameter("Profile_Id", insert.Profile_Id)
                               .ExecuteUpdate();
                                transaction.Commit();
                                logger.Error("inserteddata>>");
                            }
                            else
                            {
                                session.Save(insert);
                                transaction.Commit();
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }// End Using Trasaction
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    logger.Error("notinserteddata>>");
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);

                }
            }
        }

        public void insertpostlikes(Domain.Socioboard.Domain.InstagramPostLikes insert)
        {

            //Domain.Socioboard.Domain.InstagramPostLikes insert = (Domain.Socioboard.Domain.InstagramPostLikes)new JavaScriptSerializer().Deserialize(i, typeof(Domain.Socioboard.Domain.InstagramPostLikes));

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        //Proceed action, to save data.
                        try
                        {
                            bool exist = session.Query<Domain.Socioboard.Domain.InstagramPostLikes>()
                                          .Any(x => x.Profile_Id == insert.Profile_Id && x.Feed_Id == insert.Feed_Id && x.Liked_By_Id == insert.Liked_By_Id);
                            if (!exist)
                            {
                                session.Save(insert);
                                transaction.Commit();
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }// End Using Trasaction
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    logger.Error("notinserteddata>>");
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                }
            }
        }
        public void insertuserdetails(Domain.Socioboard.Domain.InstagramUserDetails insert)
        {

            //Domain.Socioboard.Domain.InstagramUserDetails insert = (Domain.Socioboard.Domain.InstagramUserDetails)new JavaScriptSerializer().Deserialize(i, typeof(Domain.Socioboard.Domain.InstagramUserDetails));

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(insert);
                        transaction.Commit();

                    }// End Using Trasaction
                }
                catch (Exception e)
                {
                    logger.Error("notinserteddata>>");
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                }
            }
        }

        public void insertfollowerfollowingdata(Domain.Socioboard.Domain.InboxMessages insert)
        {

            //Domain.Socioboard.Domain.InboxMessages insert = (Domain.Socioboard.Domain.InboxMessages)new JavaScriptSerializer().Deserialize(i, typeof(Domain.Socioboard.Domain.InboxMessages));

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        //Proceed action, to save data.
                        bool exist = session.Query<Domain.Socioboard.Domain.InboxMessages>()
                           .Any(x => x.UserId == insert.UserId && x.ProfileId == insert.ProfileId && x.RecipientId == insert.RecipientId);
                        if (exist)
                        {
                            try
                            {
                                int _i = session.CreateQuery("Update InboxMessages set FromName = : FromName, FromImageUrl=:FromImageUrl where ProfileId=:ProfileId and RecipientId=:RecipientId")
                                                        .SetParameter("FromName", insert.FromName)
                                                        .SetParameter("ProfileId", insert.ProfileId)
                                                        .SetParameter("RecipientId", insert.RecipientId)
                                                     .SetParameter("FromImageUrl", insert.FromImageUrl)
                                                           .ExecuteUpdate();
                                transaction.Commit();
                                logger.Error("inserteddata>>");
                            }
                            catch (Exception ex)
                            {
                            }


                        }
                        else
                        {
                            session.Save(insert);
                            transaction.Commit();
                        }
                    }// End Using Trasaction
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    logger.Error("notinserteddata>>");
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);


                }
            }// End using session

        }


    }
}
