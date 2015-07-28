using System.Web.Script.Services;
using System.Web.Services;
using Domain.Socioboard.Domain;
using GlobusGooglePlusLib.Authentication;
using Newtonsoft.Json.Linq;
using System;
using GlobusGooglePlusLib.Youtube.Core;
using System.Configuration;
using System.Web.Script.Serialization;
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
    public class Youtube : System.Web.Services.WebService
    {
        ILog logger = LogManager.GetLogger(typeof(Youtube));

        GroupsRepository objGroupsRepository = new GroupsRepository();
        TeamRepository objTeamRepository = new TeamRepository();
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
        YoutubeAccountRepository objYoutubeAccountRepository = new YoutubeAccountRepository();
        YoutubeChannelRepository objYoutubeChannelRepository = new YoutubeChannelRepository();
        ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
        Domain.Socioboard.Domain.ScheduledMessage objScheduledMessage;
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddYoutubeAccount(string client_id, string client_secret, string redirect_uri, string UserId, string GroupId, string code)
        {
            #region Local variables Inisitalisation
            string ret = string.Empty;
            string objRefresh = string.Empty;
            string refreshToken = string.Empty;
            string access_token = string.Empty;
            oAuthTokenYoutube ObjoAuthTokenYoutube = new oAuthTokenYoutube();
            oAuthToken objToken = new oAuthToken();
            Domain.Socioboard.Domain.YoutubeAccount objYoutubeAccount = new Domain.Socioboard.Domain.YoutubeAccount();
            Domain.Socioboard.Domain.YoutubeChannel objYoutubeChannel;
            YoutubeChannelRepository objYoutubeChannelRepository = new YoutubeChannelRepository();
            YoutubeAccountRepository objYoutubeAccountRepository = new YoutubeAccountRepository();
            #endregion
            #region Get AccessToken and RefreshToken
            objToken.ConsumerKey = client_id;
            objToken.ConsumerSecret = client_secret;
            try
            {
                objRefresh = ObjoAuthTokenYoutube.GetRefreshToken(code, client_id, client_secret, redirect_uri);
                logger.Error("Abhay: ObjoAuthTokenYoutube()");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            JObject objaccesstoken = JObject.Parse(objRefresh);
            try
            {
                refreshToken = objaccesstoken["refresh_token"].ToString();

            }
            catch (Exception ex)
            {
                access_token = objaccesstoken["access_token"].ToString();
                ObjoAuthTokenYoutube.RevokeToken(access_token);
                Console.WriteLine(ex.StackTrace);
                return "Refresh Token Not Found";
            }

            try
            {

                access_token = objaccesstoken["access_token"].ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

            }
            #endregion
            #region Get user Profile and Add Youtube Account
            JArray userinfo = new JArray();
            try
            {
                userinfo = objToken.GetUserInfo("me", access_token.ToString());
            }
            catch (Exception ex)
            {
            }
            foreach (var itemEmail in userinfo)
            {
                try
                {
                    objYoutubeAccount.Id = Guid.NewGuid();
                    objYoutubeAccount.Ytuserid = itemEmail["id"].ToString();
                    objYoutubeAccount.Emailid = itemEmail["email"].ToString();
                    try
                    {
                        objYoutubeAccount.Ytusername = itemEmail["given_name"].ToString();
                    }
                    catch (Exception ex)
                    {
                        objYoutubeAccount.Ytusername = itemEmail["name"].ToString();
                    }
                    objYoutubeAccount.Ytprofileimage = itemEmail["picture"].ToString();
                    objYoutubeAccount.Accesstoken = access_token;
                    objYoutubeAccount.Refreshtoken = refreshToken;
                    objYoutubeAccount.Isactive = 1;
                    objYoutubeAccount.Entrydate = DateTime.Now;
                    objYoutubeAccount.UserId = Guid.Parse(UserId);
                    if (!objYoutubeAccountRepository.checkYoutubeUserExists(objYoutubeAccount))
                    {
                        YoutubeAccountRepository.Add(objYoutubeAccount);
                        ret = "Account Added Successfully";
                    }
                    else
                    {
                        ret = "Account already Exist !";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

            }


            #endregion
            #region Add Youtube Channels
            GlobusGooglePlusLib.Youtube.Core.Channels ObjChannel = new GlobusGooglePlusLib.Youtube.Core.Channels();
            JArray objarray = new JArray();
            try
            {
                string part = (oAuthTokenYoutube.Parts.contentDetails.ToString() + "," + oAuthTokenYoutube.Parts.statistics.ToString());
                string Value = ObjChannel.Get_Channel_List(access_token, part, 50, true);
                JObject UserChannels = JObject.Parse(@Value);
                objarray = (JArray)UserChannels["items"];
            }
            catch (Exception ex)
            {
            }

            foreach (var item in objarray)
            {
                objYoutubeChannel = new Domain.Socioboard.Domain.YoutubeChannel();
                try
                {
                    objYoutubeChannel.Id = Guid.NewGuid();
                    objYoutubeChannel.Channelid = item["id"].ToString();
                    objYoutubeChannel.Likesid = item["contentDetails"]["relatedPlaylists"]["likes"].ToString();
                    objYoutubeChannel.Favoritesid = item["contentDetails"]["relatedPlaylists"]["favorites"].ToString();
                    objYoutubeChannel.Uploadsid = item["contentDetails"]["relatedPlaylists"]["uploads"].ToString();
                    objYoutubeChannel.Watchhistoryid = item["contentDetails"]["relatedPlaylists"]["watchHistory"].ToString();
                    objYoutubeChannel.Watchlaterid = item["contentDetails"]["relatedPlaylists"]["watchLater"].ToString();
                    objYoutubeChannel.Googleplususerid = objYoutubeAccount.Ytuserid;
                    objYoutubeChannel.UserId = Guid.Parse(UserId);
                    try
                    {
                        string viewcnt = item["statistics"]["viewCount"].ToString();
                        objYoutubeChannel.ViewCount = Convert.ToInt32(viewcnt);
                        string videocnt = item["statistics"]["videoCount"].ToString();
                        objYoutubeChannel.VideoCount = Convert.ToInt32(videocnt);
                        string commentcnt = item["statistics"]["commentCount"].ToString();
                        objYoutubeChannel.CommentCount = Convert.ToInt32(commentcnt);
                        string Subscribercnt = item["statistics"]["subscriberCount"].ToString();
                        objYoutubeChannel.SubscriberCount = Convert.ToInt32(Subscribercnt);
                        try
                        {
                            string str = item["statistics"]["hiddenSubscriberCount"].ToString();
                            if (str == "false")
                            {
                                objYoutubeChannel.HiddenSubscriberCount = 0;
                            }
                            else
                            {
                                objYoutubeChannel.HiddenSubscriberCount = 1;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                if (!objYoutubeChannelRepository.checkYoutubeChannelExists(objYoutubeChannel.Channelid, Guid.Parse(UserId)))
                {
                    YoutubeChannelRepository.Add(objYoutubeChannel);
                }
            }
            #endregion
            #region Add TeamMemberProfile
            Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(GroupId));
            Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
            objTeamMemberProfile.Id = Guid.NewGuid();
            objTeamMemberProfile.TeamId = objTeam.Id;
            objTeamMemberProfile.Status = 1;
            objTeamMemberProfile.ProfileType = "youtube";
            objTeamMemberProfile.StatusUpdateDate = DateTime.Now;
            objTeamMemberProfile.ProfileId = objYoutubeAccount.Ytuserid;

            objTeamMemberProfile.ProfileName = objYoutubeAccount.Ytusername;
            objTeamMemberProfile.ProfilePicUrl = objYoutubeAccount.Ytprofileimage;

            if (!objTeamMemberProfileRepository.checkTeamMemberProfile(objTeam.Id, objYoutubeAccount.Ytuserid))
            {
                objTeamMemberProfileRepository.addNewTeamMember(objTeamMemberProfile);
            }
            #endregion
            #region SocialProfile
            Domain.Socioboard.Domain.SocialProfile objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
            objSocialProfile.Id = Guid.NewGuid();
            objSocialProfile.ProfileType = "youtube";
            objSocialProfile.ProfileId = objYoutubeAccount.Ytuserid;
            objSocialProfile.UserId = Guid.Parse(UserId);
            objSocialProfile.ProfileDate = DateTime.Now;
            objSocialProfile.ProfileStatus = 1;
            if (!objSocialProfilesRepository.checkUserProfileExist(objSocialProfile))
            {
                objSocialProfilesRepository.addNewProfileForUser(objSocialProfile);
            }
            #endregion
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetYoutubeChannelVideos(string userid, string profileid)
        {
            string ret = string.Empty;
            string strfinaltoken = string.Empty;
            string channelDetails = string.Empty;
            Domain.Socioboard.Domain.YoutubeAccount objYoutubeAccount = objYoutubeAccountRepository.getYoutubeAccountDetailsById(profileid, Guid.Parse(userid));
            Domain.Socioboard.Domain.YoutubeChannel objYoutubeChannel = objYoutubeChannelRepository.getYoutubeChannelDetailsById(profileid, Guid.Parse(userid));

            oAuthTokenYoutube objoAuthTokenYoutube = new oAuthTokenYoutube();
            string finaltoken = objoAuthTokenYoutube.GetAccessToken(objYoutubeAccount.Refreshtoken);
            JObject objArray = JObject.Parse(finaltoken);
            //foreach (var item in objArray)
            //{
            try
            {
                strfinaltoken = objArray["access_token"].ToString();
                // break;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            //}
            PlaylistItems objPlaylistItems = new PlaylistItems();
            try
            {
                channelDetails = objPlaylistItems.Get_PlaylistItems_List(strfinaltoken, GlobusGooglePlusLib.Authentication.oAuthTokenYoutube.Parts.snippet.ToString(), objYoutubeChannel.Uploadsid);
            }
            catch (Exception ex)
            {

            }



            return channelDetails;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GoogleLogin(string code)
        {
            string ret = string.Empty;
            string objRefresh = string.Empty;
            string refreshToken = string.Empty;
            string access_token = string.Empty;
            oAuthTokenYoutube ObjoAuthTokenYoutube = new oAuthTokenYoutube();
            oAuthToken objToken = new oAuthToken();
            Domain.Socioboard.Domain.User objuser = new Domain.Socioboard.Domain.User();
            try
            {
                logger.Error("Abhay before ObjoAuthTokenYoutube.GetRefreshToken: " + new System.Diagnostics.StackFrame(0, true).GetFileLineNumber());
                objRefresh = ObjoAuthTokenYoutube.GetRefreshToken(code, ConfigurationManager.AppSettings["YtconsumerKey"], ConfigurationManager.AppSettings["YtconsumerSecret"], ConfigurationManager.AppSettings["Ytredirect_uri"]);
                logger.Error("Abhay: " + new System.Diagnostics.StackFrame(0, true).GetFileLineNumber());

                logger.Error("1 " + code + " " + ConfigurationManager.AppSettings["YtconsumerKey"] + " " + ConfigurationManager.AppSettings["YtconsumerSecret"] + " " + ConfigurationManager.AppSettings["Ytredirect_uri"]);

            }
            catch (Exception ex)
            {
                logger.Error("2 " + code + " " + ConfigurationManager.AppSettings["YtconsumerKey"] + " " + ConfigurationManager.AppSettings["YtconsumerSecret"] + " " + ConfigurationManager.AppSettings["Ytredirect_uri"]);
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            JObject objaccesstoken = JObject.Parse(objRefresh);
            try
            {
                refreshToken = objaccesstoken["refresh_token"].ToString();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {

                access_token = objaccesstoken["access_token"].ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

            }

            JArray userinfo = new JArray();
            try
            {
                userinfo = objToken.GetUserInfo("me", access_token.ToString());
            }
            catch (Exception ex)
            {
            }

            foreach (var itemEmail in userinfo)
            {
                try
                {
                    objuser.EmailId = itemEmail["email"].ToString();
                    objuser.UserName = itemEmail["given_name"].ToString();
                    objuser.ProfileUrl = itemEmail["picture"].ToString();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

            }
            return (new JavaScriptSerializer().Serialize(objuser)) + "_#_" + access_token;
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        private void AddYouTubeChannels(string UserId, string access_token, string Ytuserid)
        {
            Domain.Socioboard.Domain.YoutubeChannel objYoutubeChannel;
            GlobusGooglePlusLib.Youtube.Core.Channels ObjChannel = new GlobusGooglePlusLib.Youtube.Core.Channels();
            JArray objarray = new JArray();
            try
            {
                string part = (oAuthTokenYoutube.Parts.contentDetails.ToString() + "," + oAuthTokenYoutube.Parts.statistics.ToString());
                string Value = ObjChannel.Get_Channel_List(access_token, part, 50, true);
                JObject UserChannels = JObject.Parse(@Value);
                objarray = (JArray)UserChannels["items"];
            }
            catch (Exception ex)
            {
            }

            foreach (var item in objarray)
            {
                objYoutubeChannel = new Domain.Socioboard.Domain.YoutubeChannel();
                try
                {
                    objYoutubeChannel.Id = Guid.NewGuid();
                    objYoutubeChannel.Channelid = item["id"].ToString();
                    objYoutubeChannel.Likesid = item["contentDetails"]["relatedPlaylists"]["likes"].ToString();
                    objYoutubeChannel.Favoritesid = item["contentDetails"]["relatedPlaylists"]["favorites"].ToString();
                    objYoutubeChannel.Uploadsid = item["contentDetails"]["relatedPlaylists"]["uploads"].ToString();
                    objYoutubeChannel.Watchhistoryid = item["contentDetails"]["relatedPlaylists"]["watchHistory"].ToString();
                    objYoutubeChannel.Watchlaterid = item["contentDetails"]["relatedPlaylists"]["watchLater"].ToString();
                    objYoutubeChannel.Googleplususerid = Ytuserid;
                    objYoutubeChannel.UserId = Guid.Parse(UserId);
                    try
                    {
                        string viewcnt = item["statistics"]["viewCount"].ToString();
                        objYoutubeChannel.ViewCount = Convert.ToInt32(viewcnt);
                        string videocnt = item["statistics"]["videoCount"].ToString();
                        objYoutubeChannel.VideoCount = Convert.ToInt32(videocnt);
                        string commentcnt = item["statistics"]["commentCount"].ToString();
                        objYoutubeChannel.CommentCount = Convert.ToInt32(commentcnt);
                        string Subscribercnt = item["statistics"]["subscriberCount"].ToString();
                        objYoutubeChannel.SubscriberCount = Convert.ToInt32(Subscribercnt);
                        try
                        {
                            string str = item["statistics"]["hiddenSubscriberCount"].ToString();
                            if (str == "false")
                            {
                                objYoutubeChannel.HiddenSubscriberCount = 0;
                            }
                            else
                            {
                                objYoutubeChannel.HiddenSubscriberCount = 1;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                if (!objYoutubeChannelRepository.checkYoutubeChannelExists(objYoutubeChannel.Channelid, Guid.Parse(UserId)))
                {
                    YoutubeChannelRepository.Add(objYoutubeChannel);
                }
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void Get_Channel_List_serarch(string token, string search)
        {
            GlobusGooglePlusLib.Youtube.Core.Channels ObjChannel = new GlobusGooglePlusLib.Youtube.Core.Channels();
            JArray objarray = new JArray();
            try
            {
                //string part = (oAuthTokenYoutube.Parts.contentDetails.ToString() + "," + oAuthTokenYoutube.Parts.statistics.ToString());
                string Value = ObjChannel.Get_Channel_List_serarch(token, search);
                JObject UserChannels = JObject.Parse(@Value);
                objarray = (JArray)UserChannels["items"];
            }
            catch (Exception ex)
            {
            }
        }




        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getYoutubeData(string UserId, string youtubeId)
        {
            string ret = string.Empty;
            try
            {
                Guid userId = Guid.Parse(UserId);
                oAuthToken OAuthToken = new oAuthToken();
                OAuthToken.ConsumerKey = ConfigurationManager.AppSettings["YtconsumerKey"];
                OAuthToken.ConsumerSecret = ConfigurationManager.AppSettings["YtconsumerSecret"];
                YoutubeAccountRepository ObjYoutubeAccountRepository = new YoutubeAccountRepository();
                Domain.Socioboard.Domain.YoutubeAccount ObjYoutubeAccount = ObjYoutubeAccountRepository.getYoutubeAccountDetailsById(youtubeId, userId);
                AddYouTubeChannels(userId.ToString(), ObjYoutubeAccount.Accesstoken, youtubeId);
                return "Youtube Channel is added";
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SheduleYoutubeMessage(string YoutubeId, string UserId, string sscheduledmsgguid)
        {
            string str = string.Empty;
            try
            {
                objScheduledMessage = objScheduledMessageRepository.GetScheduledMessageDetails(Guid.Parse(sscheduledmsgguid));
                Domain.Socioboard.Domain.YoutubeAccount ObjYoutubeAccount = objYoutubeAccountRepository.getYoutubeAccountDetailsById(YoutubeId, Guid.Parse(UserId));
                oAuthToken OAuthToken = new oAuthToken();
                OAuthToken.ConsumerKey = ConfigurationManager.AppSettings["YtconsumerKey"];
                OAuthToken.ConsumerSecret = ConfigurationManager.AppSettings["YtconsumerSecret"];

            }
            catch (Exception ex)
            {

                throw;
            }
            return str;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateYouTubeAccountByAdmin(string ObjYouTube)
        {
            Domain.Socioboard.Domain.YoutubeAccount ObjYouTubeAccount = (Domain.Socioboard.Domain.YoutubeAccount)(new JavaScriptSerializer().Deserialize(ObjYouTube, typeof(Domain.Socioboard.Domain.YoutubeAccount)));
            try
            {
                objYoutubeAccountRepository.updateYoutubeUser(ObjYouTubeAccount);
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
        public string AddYoutubeAccountwithLogin(string client_id, string client_secret, string redirect_uri, string UserId, string GroupId, string access_token)
        {
            #region Local variables Inisitalisation
            string ret = string.Empty;
            string objRefresh = string.Empty;
            string refreshToken = string.Empty;
            oAuthTokenYoutube ObjoAuthTokenYoutube = new oAuthTokenYoutube();
            oAuthToken objToken = new oAuthToken();
            Domain.Socioboard.Domain.YoutubeAccount objYoutubeAccount = new Domain.Socioboard.Domain.YoutubeAccount();
            Domain.Socioboard.Domain.YoutubeChannel objYoutubeChannel;
            YoutubeChannelRepository objYoutubeChannelRepository = new YoutubeChannelRepository();
            YoutubeAccountRepository objYoutubeAccountRepository = new YoutubeAccountRepository();
            #endregion
            #region Get user Profile and Add Youtube Account
            JArray userinfo = new JArray();
            try
            {
                userinfo = objToken.GetUserInfo("me", access_token.ToString());
            }
            catch (Exception ex)
            {
            }
            foreach (var itemEmail in userinfo)
            {
                try
                {
                    objYoutubeAccount.Id = Guid.NewGuid();
                    objYoutubeAccount.Ytuserid = itemEmail["id"].ToString();
                    objYoutubeAccount.Emailid = itemEmail["email"].ToString();
                    try
                    {
                        objYoutubeAccount.Ytusername = itemEmail["given_name"].ToString();
                    }
                    catch (Exception ex)
                    {
                        objYoutubeAccount.Ytusername = itemEmail["name"].ToString();
                    }
                    objYoutubeAccount.Ytprofileimage = itemEmail["picture"].ToString();
                    objYoutubeAccount.Accesstoken = access_token;
                    objYoutubeAccount.Refreshtoken = refreshToken;
                    objYoutubeAccount.Isactive = 1;
                    objYoutubeAccount.Entrydate = DateTime.Now;
                    objYoutubeAccount.UserId = Guid.Parse(UserId);
                    if (!objYoutubeAccountRepository.checkYoutubeUserExists(objYoutubeAccount))
                    {
                        YoutubeAccountRepository.Add(objYoutubeAccount);
                        ret = "Account Added Successfully";
                    }
                    else
                    {
                        ret = "Account already Exist !";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

            }


            #endregion
            #region Add Youtube Channels
            GlobusGooglePlusLib.Youtube.Core.Channels ObjChannel = new GlobusGooglePlusLib.Youtube.Core.Channels();
            JArray objarray = new JArray();
            try
            {
                string part = (oAuthTokenYoutube.Parts.contentDetails.ToString() + "," + oAuthTokenYoutube.Parts.statistics.ToString());
                string Value = ObjChannel.Get_Channel_List(access_token, part, 50, true);
                JObject UserChannels = JObject.Parse(@Value);
                objarray = (JArray)UserChannels["items"];
            }
            catch (Exception ex)
            {
            }

            foreach (var item in objarray)
            {
                objYoutubeChannel = new Domain.Socioboard.Domain.YoutubeChannel();
                try
                {
                    objYoutubeChannel.Id = Guid.NewGuid();
                    objYoutubeChannel.Channelid = item["id"].ToString();
                    objYoutubeChannel.Likesid = item["contentDetails"]["relatedPlaylists"]["likes"].ToString();
                    objYoutubeChannel.Favoritesid = item["contentDetails"]["relatedPlaylists"]["favorites"].ToString();
                    objYoutubeChannel.Uploadsid = item["contentDetails"]["relatedPlaylists"]["uploads"].ToString();
                    objYoutubeChannel.Watchhistoryid = item["contentDetails"]["relatedPlaylists"]["watchHistory"].ToString();
                    objYoutubeChannel.Watchlaterid = item["contentDetails"]["relatedPlaylists"]["watchLater"].ToString();
                    objYoutubeChannel.Googleplususerid = objYoutubeAccount.Ytuserid;
                    objYoutubeChannel.UserId = Guid.Parse(UserId);
                    try
                    {
                        string viewcnt = item["statistics"]["viewCount"].ToString();
                        objYoutubeChannel.ViewCount = Convert.ToInt32(viewcnt);
                        string videocnt = item["statistics"]["videoCount"].ToString();
                        objYoutubeChannel.VideoCount = Convert.ToInt32(videocnt);
                        string commentcnt = item["statistics"]["commentCount"].ToString();
                        objYoutubeChannel.CommentCount = Convert.ToInt32(commentcnt);
                        string Subscribercnt = item["statistics"]["subscriberCount"].ToString();
                        objYoutubeChannel.SubscriberCount = Convert.ToInt32(Subscribercnt);
                        try
                        {
                            string str = item["statistics"]["hiddenSubscriberCount"].ToString();
                            if (str == "false")
                            {
                                objYoutubeChannel.HiddenSubscriberCount = 0;
                            }
                            else
                            {
                                objYoutubeChannel.HiddenSubscriberCount = 1;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                if (!objYoutubeChannelRepository.checkYoutubeChannelExists(objYoutubeChannel.Channelid, Guid.Parse(UserId)))
                {
                    YoutubeChannelRepository.Add(objYoutubeChannel);
                }
            }
            #endregion
            #region Add TeamMemberProfile
            Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(GroupId));
            Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
            objTeamMemberProfile.Id = Guid.NewGuid();
            objTeamMemberProfile.TeamId = objTeam.Id;
            objTeamMemberProfile.Status = 1;
            objTeamMemberProfile.ProfileType = "youtube";
            objTeamMemberProfile.StatusUpdateDate = DateTime.Now;
            objTeamMemberProfile.ProfileId = objYoutubeAccount.Ytuserid;

            objTeamMemberProfile.ProfileName = objYoutubeAccount.Ytusername;
            objTeamMemberProfile.ProfilePicUrl = objYoutubeAccount.Ytprofileimage;

            if (!objTeamMemberProfileRepository.checkTeamMemberProfile(objTeam.Id, objYoutubeAccount.Ytuserid))
            {
                objTeamMemberProfileRepository.addNewTeamMember(objTeamMemberProfile);
            }
            #endregion
            #region SocialProfile
            Domain.Socioboard.Domain.SocialProfile objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
            objSocialProfile.Id = Guid.NewGuid();
            objSocialProfile.ProfileType = "youtube";
            objSocialProfile.ProfileId = objYoutubeAccount.Ytuserid;
            objSocialProfile.UserId = Guid.Parse(UserId);
            objSocialProfile.ProfileDate = DateTime.Now;
            objSocialProfile.ProfileStatus = 1;
            if (!objSocialProfilesRepository.checkUserProfileExist(objSocialProfile))
            {
                objSocialProfilesRepository.addNewProfileForUser(objSocialProfile);
            }
            #endregion
            return ret;
        }

    
    
    
    }
}
