using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Api.Socioboard.Model;
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
    public class FacebookAccount : System.Web.Services.WebService
    {
        FacebookAccountRepository objFacebookAccountRepository = new FacebookAccountRepository();
        FacebookFeedRepository objFacebookFeedRepository = new FacebookFeedRepository();
        FacebookMessageRepository objFacebookMessageRepository = new FacebookMessageRepository();
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
        TeamRepository objTeamRepository = new TeamRepository();
        GroupProfileRepository objGroupProfileRepository = new GroupProfileRepository();
        UserRepository objUserRepository = new UserRepository();
        Domain.Socioboard.Domain.FacebookAccount objFacebook;
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddFacebookAccount(string FbUserId, string FbUserName, string AccessToken, string Friends, string EmailId, string Type, string ProfileUrl, string IsActive, string UserId, string GroupId)
        {
            try
            {
                objFacebook = new Domain.Socioboard.Domain.FacebookAccount();
                objFacebook.Id = Guid.NewGuid();
                objFacebook.FbUserId = FbUserId;
                objFacebook.FbUserName = FbUserName;
                objFacebook.AccessToken = AccessToken;
                objFacebook.Friends = Convert.ToInt16(Friends);
                objFacebook.EmailId = EmailId;
                objFacebook.Type = Type;
                objFacebook.ProfileUrl = ProfileUrl;
                objFacebook.IsActive = Convert.ToInt16(IsActive);
                objFacebook.UserId = Guid.Parse(UserId);
                objFacebookAccountRepository.addFacebookUser(objFacebook);



                return new JavaScriptSerializer().Serialize("Added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string getFacebookAccountDetailsById(string UserId, string ProfileId)
        //{
        //    try
        //    {
        //        Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(ProfileId, Guid.Parse(UserId));
        //        return new JavaScriptSerializer().Serialize(objFacebookAccount);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //        return "Something Went Wrong";
        //    }
        //}

        // Edited by Antima[20/12/2014]

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getFacebookAccountDetailsById(string UserId, string ProfileId)
        {
            try
            {
                Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = new Domain.Socioboard.Domain.FacebookAccount();
                if (objFacebookAccountRepository.checkFacebookUserExists(ProfileId, Guid.Parse(UserId)))
                {
                    objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(ProfileId, Guid.Parse(UserId));
                    return new JavaScriptSerializer().Serialize(objFacebookAccount);
                }
                else
                {
                    objFacebookAccount = objFacebookAccountRepository.getUserDetails(ProfileId);
                    return new JavaScriptSerializer().Serialize(objFacebookAccount);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }





        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetFacebookAccountDetailsById(string UserId)
        {
            try
            {
                Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(UserId);
                return new JavaScriptSerializer().Serialize(objFacebookAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string IsFacebookuserExist(string UserId, string ProfileId)
        {
            string str = string.Empty;
            try
            {
                bool ret = objFacebookAccountRepository.checkFacebookUserExists(ProfileId, Guid.Parse(UserId));
                if (ret == false)
                {
                    str = "False";
                }
                else
                {
                    str = "True";
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
        public string DeleteFacebookAccount(string UserId, string ProfileId,string GroupId)
        {
            try
            {
                objFacebookAccountRepository.deleteFacebookUser(ProfileId, Guid.Parse(UserId));
                //objFacebookFeedRepository.deleteAllFeedsOfUser(ProfileId, Guid.Parse(UserId));
                //objFacebookMessageRepository.deleteAllMessagesOfUser(ProfileId, Guid.Parse(UserId));
                Domain.Socioboard.Domain.Team objTeam=objTeamRepository.GetTeamByGroupId(Guid.Parse(GroupId));
                objTeamMemberProfileRepository.DeleteTeamMemberProfileByTeamIdProfileId(ProfileId, objTeam.Id);
                objGroupProfileRepository.DeleteGroupProfile(Guid.Parse(UserId), ProfileId, Guid.Parse(GroupId));
                objSocialProfilesRepository.deleteProfile(Guid.Parse(UserId), ProfileId);
                return new JavaScriptSerializer().Serialize("");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetFacebookAccountByUserId(string UserId)
        {
            try
            {
                List<Domain.Socioboard.Domain.FacebookAccount> lstFacebookAccount = objFacebookAccountRepository.GetAllFacebookAccountByUserId(Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(lstFacebookAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }
        //getAllFacebookAccountsOfUser
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFacebookAccountsOfUser(string UserId)
        {
            try
            {
                List<Domain.Socioboard.Domain.FacebookAccount> objFacebookAccount = objFacebookAccountRepository.getAllFacebookAccountsOfUser(Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(objFacebookAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        //getUserDetails
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getUserDetails(string FbUserId)
        {
            try
            {
                Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = objFacebookAccountRepository.getUserDetails(FbUserId);
                return new JavaScriptSerializer().Serialize(objFacebookAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllFacebookAccountsByUserIdAndGroupId(string userid,string groupid)
        {
            try
            {
                List<Domain.Socioboard.Domain.FacebookAccount> lstFacebookAccount = new List<Domain.Socioboard.Domain.FacebookAccount>();
                Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(groupid));
                List<Domain.Socioboard.Domain.TeamMemberProfile> lstTeamMemberProfile=objTeamMemberProfileRepository.GetTeamMemberProfileByTeamIdAndProfileType(objTeam.Id,"facebook");
                foreach (var item in lstTeamMemberProfile)
                {
                    try
                    {
                        if (objFacebookAccountRepository.checkFacebookUserExists(item.ProfileId, Guid.Parse(userid)))
                        {
                            lstFacebookAccount.Add(objFacebookAccountRepository.getFacebookAccountDetailsById(item.ProfileId, Guid.Parse(userid)));
                        }
                        else
                        {
                            lstFacebookAccount.Add(objFacebookAccountRepository.getFacebookAccountDetailsById(item.ProfileId));
                        }
                    }
                    catch (Exception)
                    {
                       
                    }
                }
                return new JavaScriptSerializer().Serialize(lstFacebookAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllFacebookPageByUserIdAndGroupId(string userid, string groupid)
        {
            try
            {
                List<Domain.Socioboard.Domain.FacebookAccount> lstFacebookAccount = new List<Domain.Socioboard.Domain.FacebookAccount>();
                Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(groupid));
                List<Domain.Socioboard.Domain.TeamMemberProfile> lstTeamMemberProfile = objTeamMemberProfileRepository.GetTeamMemberProfileByTeamIdAndProfileType(objTeam.Id, "facebook_page");
                foreach (var item in lstTeamMemberProfile)
                {
                    try
                    {
                        lstFacebookAccount.Add(objFacebookAccountRepository.getFacebookAccountDetailsById(item.ProfileId, Guid.Parse(userid)));
                    }
                    catch (Exception)
                    {

                    }
                }
                return new JavaScriptSerializer().Serialize(lstFacebookAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        //vikash
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllFacebookAccountDetails(string profileid,string userid)
        {
            try
            {
                List<Domain.Socioboard.Domain.FacebookAccount> lstfbprofileDetails = objFacebookAccountRepository.getAllAccountDetail(profileid,Guid.Parse(userid));
                return new JavaScriptSerializer().Serialize(lstfbprofileDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }

        }

        public string getFbToken()
        {

            Domain.Socioboard.Domain.FacebookAccount _FacebookAccount = objFacebookAccountRepository.getToken();
            string token = _FacebookAccount.AccessToken;

            return token;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllFacebookAccounts()
        {
            try
            {
                FacebookAccountRepository objFbRepo = new FacebookAccountRepository();
                ArrayList lstFBAcc = objFbRepo.getAllFacebookAccounts();
                return new JavaScriptSerializer().Serialize(lstFBAcc);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        public string AddFacebookAccountFromTweetBoard(string UserId, string GroupId, string ProfileId, string AccessToken, string FriendsCount, string Name, string EmailId)
        {
            if (objUserRepository.IsUserExist(Guid.Parse(UserId)))
            {

                if (!objFacebookAccountRepository.checkFacebookUserExists(ProfileId, Guid.Parse(UserId)))
                {

                    Domain.Socioboard.Domain.FacebookAccount _FacebookAccount = new Domain.Socioboard.Domain.FacebookAccount();
                    _FacebookAccount.Id = Guid.NewGuid();
                    _FacebookAccount.ProfileType = "account";
                    _FacebookAccount.IsActive = 1;
                    _FacebookAccount.AccessToken = AccessToken;
                    _FacebookAccount.EmailId = EmailId;
                    _FacebookAccount.FbUserId = ProfileId;
                    _FacebookAccount.FbUserName = Name;
                    _FacebookAccount.Friends = Int32.Parse(FriendsCount);
                    objFacebookAccountRepository.addFacebookUser(_FacebookAccount);

                    #region Add TeamMemberProfile
                    Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(GroupId));
                    Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
                    if (objTeamMemberProfileRepository.checkTeamMemberProfile(objTeam.Id, ProfileId))
                    {
                        objTeamMemberProfile.Id = Guid.NewGuid();
                        objTeamMemberProfile.TeamId = objTeam.Id;
                        objTeamMemberProfile.Status = 1;
                        objTeamMemberProfile.ProfileType = "facebook";
                        objTeamMemberProfile.StatusUpdateDate = DateTime.Now;
                        objTeamMemberProfile.ProfileId = ProfileId;
                        objTeamMemberProfile.ProfileName = Name;
                        objTeamMemberProfile.ProfilePicUrl = "http://graph.facebook.com/" + objTeamMemberProfile.ProfileId + "/picture?type=small";
                        objTeamMemberProfileRepository.addNewTeamMember(objTeamMemberProfile);
                    }
                    #endregion
                    #region SocialProfile
                    Domain.Socioboard.Domain.SocialProfile objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
                    objSocialProfile.Id = Guid.NewGuid();
                    objSocialProfile.ProfileType = "facebook";
                    objSocialProfile.ProfileId = ProfileId;
                    objSocialProfile.UserId = Guid.Parse(UserId);
                    objSocialProfile.ProfileDate = DateTime.Now;
                    objSocialProfile.ProfileStatus = 1;
                    #endregion
                    if (!objSocialProfilesRepository.checkUserProfileExist(objSocialProfile))
                    {
                        objSocialProfilesRepository.addNewProfileForUser(objSocialProfile);
                    }
                    return "account added";
                }
                else
                {
                    return "account already exist";
                }
            }
            else {
                return "user not exist";
            }
        }
        [WebMethod]
        public string AllFacebookPageDetails()
        {
            try
            {
                return new JavaScriptSerializer().Serialize(objFacebookAccountRepository.GetAllFacebookPages());
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new List<Domain.Socioboard.Domain.FacebookAccount>());
            }
        }







        [WebMethod]
        public string MovefbFeedsToMongo() 
        {
            string output = string.Empty;
            List<Domain.Socioboard.Domain.FacebookFeed> fbfeeds = objFacebookFeedRepository.getAllFeedDetailForMongo();
            MongoRepository mongorepo = new MongoRepository("MongoFacebookFeed");

            foreach (var item in fbfeeds) 
            {
                Domain.Socioboard.Domain.MongoFacebookFeed Mongofbfeed = new Domain.Socioboard.Domain.MongoFacebookFeed();
                Mongofbfeed.Id = MongoDB.Bson.ObjectId.GenerateNewId();
                Mongofbfeed.EntryDate = item.EntryDate.ToString("yyyy/MM/dd HH:mm:ss");
                Mongofbfeed.FbComment = item.FbComment;
                Mongofbfeed.FbLike = item.FbLike;
                Mongofbfeed.FeedDate = item.FeedDate.ToString("yyyy/MM/dd HH:mm:ss");
                Mongofbfeed.FeedDescription = item.FeedDescription;
                Mongofbfeed.FeedId = item.FeedId;
                Mongofbfeed.FromId = item.FromId;
                Mongofbfeed.FromName = item.FromName;
                Mongofbfeed.FromProfileUrl = item.FromProfileUrl;
                Mongofbfeed.FromScreenName = item.FromScreenName;
                Mongofbfeed.InReplyToStatusUserId = item.InReplyToStatusUserId;
                Mongofbfeed.MessageDate = item.MessageDate.ToString();
                Mongofbfeed.MessageId = item.MessageId;
                Mongofbfeed.Picture = item.Picture;
                Mongofbfeed.ProfileId = item.ProfileId;
                Mongofbfeed.ProfileType = item.ProfileType;
                Mongofbfeed.ReadStatus = item.ReadStatus;
                Mongofbfeed.ScreenName = item.ScreenName;
                Mongofbfeed.SourceUrl = item.SourceUrl;
                Mongofbfeed.TwitterMsg = item.TwitterMsg;
                Mongofbfeed.Type = item.Type;
                Mongofbfeed.UserId = item.UserId.ToString();
               mongorepo.Add<Domain.Socioboard.Domain.MongoFacebookFeed>(Mongofbfeed);
        
            }

            return output;
        
        }

        [WebMethod]
        public string MoveFbMessageToMongo() 
        {
            string output = string.Empty;
            bool exit = true;
            int skip = 0;
                    MongoRepository mongorepo = new MongoRepository("FacebookMessage");

            while(exit)
            {
                List<Domain.Socioboard.Domain.FacebookMessage> fbmsgs = objFacebookMessageRepository.getAllFacebookMessagesMongo(skip);
                if (fbmsgs.Count() == 0) 
                {
                    exit = false;
                }

                foreach (var item in fbmsgs)
                {
                    Domain.Socioboard.MongoDomain.FacebookMessage mfbmsg = new Domain.Socioboard.MongoDomain.FacebookMessage();
                    mfbmsg.Id = MongoDB.Bson.ObjectId.GenerateNewId();
                    mfbmsg.EntryDate = item.EntryDate.ToString("yyyy/MM/dd HH:mm:ss");
                    mfbmsg.FbComment = item.FbComment;
                    mfbmsg.FbLike = item.FbLike;
                    mfbmsg.FromId = item.FromId;
                    mfbmsg.FromName = item.FromName;
                    mfbmsg.FromProfileUrl = item.FromProfileUrl;
                    mfbmsg.IsArchived = item.IsArchived.ToString();
                    mfbmsg.Message = item.Message;
                    mfbmsg.MessageDate = item.MessageDate.ToString("yyyy/MM/dd HH:mm:ss");
                    mfbmsg.MessageId = item.MessageId;
                    mfbmsg.Picture = item.Picture;
                    mfbmsg.ProfileId = item.ProfileId;
                    mfbmsg.Type = item.Type;
                    mfbmsg.UserId = item.UserId.ToString();
                    mongorepo.Add<Domain.Socioboard.MongoDomain.FacebookMessage>(mfbmsg);
                }
                skip = skip + 50;
            }
          
            return output;
        }

        [WebMethod]
        public string MoveTwitterFeedTOMOngo() 
        {
            TwitterFeedRepository twtfeedrepo = new TwitterFeedRepository();
            string output = string.Empty;
            bool exit = true;
            int skip = 0;
            MongoRepository mongorepo = new MongoRepository("TwitterFeed");

            while (exit)
            {
                List<Domain.Socioboard.Domain.TwitterFeed> fbmsgs = twtfeedrepo.getAllTwitterFeedsMongo(skip);
                if (fbmsgs.Count() == 0)
                {
                    exit = false;
                }

                foreach (var item in fbmsgs)
                {
                    Domain.Socioboard.MongoDomain.TwitterFeed mfbmsg = new Domain.Socioboard.MongoDomain.TwitterFeed();
                    mfbmsg.Id = MongoDB.Bson.ObjectId.GenerateNewId();
                  //  mfbmsg.EntryDate = item.EntryDate.ToString();
                    mfbmsg.Feed = item.Feed;
                    mfbmsg.FeedDate = item.FeedDate.ToString("yyyy/MM/dd HH:mm:ss");
                    mfbmsg.FromId = item.FromId;
                    mfbmsg.FromName = item.FromName;
                    mfbmsg.FromProfileUrl = item.FromProfileUrl;
                    mfbmsg.FromScreenName = item.FromScreenName;
                    mfbmsg.MessageId = item.MessageId;
                    mfbmsg.InReplyToStatusUserId = item.InReplyToStatusUserId;
                    mfbmsg.MessageId = item.MessageId;
                    mfbmsg.MediaUrl = string.Empty ;
                    mfbmsg.ProfileId = item.ProfileId;
                    mfbmsg.Type = item.Type;
                    mfbmsg.ScreenName = item.ScreenName;
                    mfbmsg.SourceUrl = item.SourceUrl;
                    mfbmsg.strId = mfbmsg.Id.ToString();
                   // mfbmsg.UserId = item.UserId.ToString();
                    mongorepo.Add<Domain.Socioboard.MongoDomain.TwitterFeed>(mfbmsg);
                }
                skip = skip + 50;
            }

            return output;
        }

        [WebMethod]
        public string MoveTwitterMessagesTOMOngo()
        {
            TwitterMessageRepository twtfeedrepo = new TwitterMessageRepository();
            string output = string.Empty;
            bool exit = true;
            int skip = 0;
            MongoRepository mongorepo = new MongoRepository("TwitterMessage");

            while (exit)
            {
                List<Domain.Socioboard.Domain.TwitterMessage> fbmsgs = twtfeedrepo.getAllTwitterMessagesMongo(skip);
                if (fbmsgs.Count() == 0)
                {
                    exit = false;
                }

                foreach (var item in fbmsgs)
                {
                    Domain.Socioboard.MongoDomain.TwitterMessage mfbmsg = new Domain.Socioboard.MongoDomain.TwitterMessage();
                    mfbmsg.Id = MongoDB.Bson.ObjectId.GenerateNewId();
                    //  mfbmsg.EntryDate = item.EntryDate.ToString();
                    mfbmsg.FromId = item.FromId;
                    mfbmsg.FromName = item.FromName;
                    mfbmsg.FromProfileUrl = item.FromProfileUrl;
                    mfbmsg.FromScreenName = item.FromScreenName;
                    mfbmsg.MessageId = item.MessageId;
                    mfbmsg.InReplyToStatusUserId = item.InReplyToStatusUserId;
                    mfbmsg.MessageDate = item.MessageDate.ToString("yyyy/MM/dd HH:mm:ss");
                    mfbmsg.IsArchived = item.IsArchived;
                    mfbmsg.ProfileId = item.ProfileId;
                    mfbmsg.Type = item.Type;
                    mfbmsg.ScreenName = item.ScreenName;
                    mfbmsg.SourceUrl = item.SourceUrl;
                    mfbmsg.ReadStatus = mfbmsg.ReadStatus;
                    mfbmsg.ProfileType = item.ProfileType;
                    mfbmsg.TwitterMsg = item.TwitterMsg;
                    mongorepo.Add<Domain.Socioboard.MongoDomain.TwitterMessage>(mfbmsg);
                }
                skip = skip + 50;
            }

            return output;
        }

        [WebMethod]
        public string MoveInstagramCommentToMongo()
        {
            InstagramCommentRepository twtfeedrepo = new InstagramCommentRepository();
            string output = string.Empty;
            bool exit = true;
            int skip = 0;
            MongoRepository mongorepo = new MongoRepository("InstagramComment");

            while (exit)
            {
                List<Domain.Socioboard.Domain.InstagramComment> fbmsgs = twtfeedrepo.getAllInstagramCommentMongo(skip);
                if (fbmsgs.Count() == 0)
                {
                    exit = false;
                }

                foreach (var item in fbmsgs)
                {
                    Domain.Socioboard.MongoDomain.InstagramComment mfbmsg = new Domain.Socioboard.MongoDomain.InstagramComment();
                    mfbmsg.Id = MongoDB.Bson.ObjectId.GenerateNewId();
                    //  mfbmsg.EntryDate = item.EntryDate.ToString();
                    mfbmsg.Comment = item.Comment;
                    mfbmsg.CommentDate = item.CommentDate;
                    mfbmsg.CommentId = item.CommentId;
                    mfbmsg.FeedId = item.FeedId;
                    mfbmsg.FromName = item.FromName;
                    mfbmsg.FromProfilePic = item.FromProfilePic;
                    mfbmsg.InstagramId = item.InstagramId;
                    mfbmsg.strId = item.Id.ToString();

                    mongorepo.Add<Domain.Socioboard.MongoDomain.InstagramComment>(mfbmsg);
                }
                skip = skip + 50;
            }

            return output;
        }



        [WebMethod]
        public string MoveInstagramFeedToMongo()
        {
            InstagramFeedRepository twtfeedrepo = new InstagramFeedRepository();
            string output = string.Empty;
            bool exit = true;
            int skip = 0;
            MongoRepository mongorepo = new MongoRepository("InstagramFeed");

            while (exit)
            {
                List<Domain.Socioboard.Domain.InstagramFeed> fbmsgs = twtfeedrepo.getAllInstagramFeedMongo(skip);
                if (fbmsgs.Count() == 0)
                {
                    exit = false;
                }

                foreach (var item in fbmsgs)
                {
                    Domain.Socioboard.MongoDomain.InstagramFeed mfbmsg = new Domain.Socioboard.MongoDomain.InstagramFeed();
                    mfbmsg.Id = MongoDB.Bson.ObjectId.GenerateNewId();
                    //  mfbmsg.EntryDate = item.EntryDate.ToString();
                    mfbmsg.AdminUser = item.AdminUser;
                    mfbmsg.CommentCount = item.CommentCount;
                    mfbmsg.Feed = item.Feed;
                    mfbmsg.FeedId = item.FeedId;
                    mfbmsg.FeedDate = item.FeedDate;
                    mfbmsg.FeedId = item.FeedId;
                    mfbmsg.InstagramId = item.InstagramId;
                    mfbmsg.strId = item.Id.ToString();
                    mfbmsg.FeedImageUrl = item.FeedImageUrl;
                    mfbmsg.FeedUrl = item.FeedUrl;
                    mfbmsg.FromId = item.FromId;
                    mfbmsg.ImageUrl = item.ImageUrl;
                    mfbmsg.InstagramId = item.InstagramId;
                    mfbmsg.IsLike = item.IsLike;
                    mfbmsg.LikeCount = item.LikeCount;
                    mfbmsg.strId = item.Id.ToString();
                    mongorepo.Add<Domain.Socioboard.MongoDomain.InstagramFeed>(mfbmsg);
                }
                skip = skip + 50;
            }

            return output;
        }


    }
}
