using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GlobusGooglePlusLib.Authentication;
using Newtonsoft.Json.Linq;
using SocioBoard.Model;
using SocioBoard.Domain;
using log4net;

namespace SocioBoard
{
    public partial class YoutubeManager : System.Web.UI.Page
    {

        ILog logger = LogManager.GetLogger(typeof(YoutubeManager));
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = (User)Session["LoggedUser"];
            if (!IsPostBack)
            {
                AccessToken();
            }
        }

        private void AccessToken()
        {
            SocioBoard.Domain.User user = (User)Session["LoggedUser"];
            oAuthTokenYoutube ObjoAuthTokenYoutube = new oAuthTokenYoutube();
            oAuthToken objToken = new oAuthToken();
            //GlobusGooglePlusLib.App.Core.PeopleController obj = new GlobusGooglePlusLib.App.Core.PeopleController();
            logger.Error("Error1:oAuthToken");

            string refreshToken = string.Empty;
            string access_token = string.Empty;


            try
            {
                string objRefresh = string.Empty;
                objRefresh= ObjoAuthTokenYoutube.GetRefreshToken(Request.QueryString["code"]);
                if (!objRefresh.StartsWith("["))
                    objRefresh = "[" + objRefresh + "]";
                JArray objArray = JArray.Parse(objRefresh);

                logger.Error("Error1:GetRefreshToken");

                if (!objRefresh.Contains("refresh_token"))
                {
                    logger.Error("Error0:refresh_token");
                     access_token = objArray[0]["access_token"].ToString();
                     string abc =ObjoAuthTokenYoutube.RevokeToken(access_token);
                     Response.Redirect("https://accounts.google.com/o/oauth2/auth?client_id=" + System.Configuration.ConfigurationManager.AppSettings["YtconsumerKey"] + "&redirect_uri=" + System.Configuration.ConfigurationManager.AppSettings["Ytredirect_uri"] + "&scope=https://www.googleapis.com/auth/youtube+https://www.googleapis.com/auth/youtube.readonly+https://www.googleapis.com/auth/youtubepartner+https://www.googleapis.com/auth/youtubepartner-channel-audit+https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile+https://www.googleapis.com/auth/plus.me&response_type=code&access_type=offline");
                     logger.Error("Error1:refresh_token");
                }


                foreach (var item in objArray)
                {
                    logger.Error("Abhay Item :"+item);
                    try
                    {
                        try
                        {
                            refreshToken = item["refresh_token"].ToString();

                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.StackTrace);
                            logger.Error(ex.Message);

                            Console.WriteLine(ex.StackTrace);

                        }
                        

                        access_token = item["access_token"].ToString();

                        break;

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                        Console.WriteLine(ex.StackTrace);

                    }
                }


                //Get user Profile 
                #region <<Get user Profile>>
                JArray objEmail = new JArray();
                try
                {
                    objEmail = objToken.GetUserInfo("me", access_token.ToString());
                }
                catch (Exception ex)
                {
                    logger.Error("Email : "+ objEmail);
                    logger.Error(ex.Message);
                }


                #endregion


                GlobusGooglePlusLib.Youtube.Core.Channels ObjChannel = new GlobusGooglePlusLib.Youtube.Core.Channels();
                //Get the Channels of user 

                JArray objarray=new JArray ();
                try
                {

                    string part = (oAuthTokenYoutube.Parts.contentDetails.ToString() + "," + oAuthTokenYoutube.Parts.statistics.ToString());
                    string Value= ObjChannel.Get_Channel_List(access_token, part, 50, true);
                    logger.Error("Successfully GetValus");
                    logger.Error("Value :" +Value);
                    JObject UserChannels = JObject.Parse(@Value);
                     logger.Error("Successfully Convert Jobj");
                    logger.Error("Successfully Convert Jobj");

                    objarray = (JArray)UserChannels["items"];
                    logger.Error("Successfully Convert JArr");

                }
                catch (Exception ex)
                {
                    logger.Error("UserChannels :" + ex.Message);
                }
               

                YoutubeAccount objYoutubeAccount = new YoutubeAccount();
                YoutubeChannel objYoutubeChannel = new YoutubeChannel();
                YoutubeChannelRepository objYoutubeChannelRepository = new YoutubeChannelRepository();
                YoutubeAccountRepository objYoutubeAccountRepository = new YoutubeAccountRepository();

                //put condition here to check is user already exise if exist then update else insert
                string ytuerid = "";
                foreach (var itemEmail in objEmail)
                {
                    logger.Error("itemEmail :" + itemEmail);
                    try
                    {
                        objYoutubeAccount.Id = Guid.NewGuid();
                        ytuerid = itemEmail["id"].ToString();
                        objYoutubeAccount.Ytuserid = itemEmail["id"].ToString();
                        objYoutubeAccount.Emailid = itemEmail["email"].ToString();
                        objYoutubeAccount.Ytusername = itemEmail["given_name"].ToString();
                        objYoutubeAccount.Ytprofileimage = itemEmail["picture"].ToString();
                        objYoutubeAccount.Accesstoken = access_token;
                        objYoutubeAccount.Refreshtoken = refreshToken;
                        objYoutubeAccount.Isactive = 1;
                        objYoutubeAccount.Entrydate = DateTime.Now;
                        objYoutubeAccount.UserId = user.Id;
                    }
                    catch (Exception ex)
                    {
                        logger.Error("itemEmail1 :" + ex.Message);
                        logger.Error("itemEmail1 :" + ex.StackTrace);
                        Console.WriteLine(ex.StackTrace);
                    }

                }

                foreach (var item in objarray)
                {
                    try
                    {    
                        objYoutubeChannel.Id = Guid.NewGuid();
                        objYoutubeChannel.Channelid = item["id"].ToString();
                        objYoutubeChannel.Likesid = item["contentDetails"]["relatedPlaylists"]["likes"].ToString();
                        objYoutubeChannel.Favoritesid = item["contentDetails"]["relatedPlaylists"]["favorites"].ToString();
                        objYoutubeChannel.Uploadsid = item["contentDetails"]["relatedPlaylists"]["uploads"].ToString();
                        objYoutubeChannel.Watchhistoryid = item["contentDetails"]["relatedPlaylists"]["watchHistory"].ToString();
                        objYoutubeChannel.Watchlaterid = item["contentDetails"]["relatedPlaylists"]["watchLater"].ToString();
                        objYoutubeChannel.Googleplususerid = ytuerid;
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
                                logger.Error("aagaya1: " + ex);
                                Console.WriteLine(ex.StackTrace);
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error("aagaya2: " + ex);
                            Console.WriteLine(ex.StackTrace);
                        }


                    }
                    catch (Exception ex)
                    {
                        logger.Error("aagaya3: " + ex);
                        Console.WriteLine(ex.StackTrace);
                        logger.Error(ex.StackTrace);
                        logger.Error(ex.Message);
                    }

                }
                //YoutubeChannelRepository.Add(objYoutubeChannel);
                SocialProfile objSocialProfile = new SocialProfile();
                SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
                objSocialProfile.Id = Guid.NewGuid();
                objSocialProfile.UserId = user.Id;
                objSocialProfile.ProfileId = ytuerid;
                objSocialProfile.ProfileType = "youtube";
                objSocialProfile.ProfileDate = DateTime.Now;
                objSocialProfile.ProfileStatus = 1;
                logger.Error("aagaya");
                if (!objSocialProfilesRepository.checkUserProfileExist(objSocialProfile))
                {
                    objSocialProfilesRepository.addNewProfileForUser(objSocialProfile);

                    if (!objYoutubeAccountRepository.checkYoutubeUserExists(objYoutubeAccount))
                    {
                        logger.Error("Abhay");
                        YoutubeAccountRepository.Add(objYoutubeAccount);
                        logger.Error("Abhay account add ho gaya");
                        YoutubeChannelRepository.Add(objYoutubeChannel);
                        logger.Error("Channel account add ho gaya");
                        GroupRepository objGroupRepository = new GroupRepository();
                        SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)HttpContext.Current.Session["GroupName"];
                        Groups lstDetails = objGroupRepository.getGroupName(team.GroupId);
                        if (lstDetails.GroupName == "Socioboard")
                        {
                            TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
                            TeamMemberProfile teammemberprofile = new TeamMemberProfile();
                            teammemberprofile.Id = Guid.NewGuid();
                            teammemberprofile.TeamId = team.Id;
                            teammemberprofile.ProfileId = objYoutubeAccount.Ytuserid;
                            teammemberprofile.ProfileType = "youtube";
                            teammemberprofile.StatusUpdateDate = DateTime.Now;
                            objTeamMemberProfileRepository.addNewTeamMember(teammemberprofile);
                        }
                    }
                }
                else
                {
                    logger.Error("Suraj");
                    if (!objYoutubeAccountRepository.checkYoutubeUserExists(objYoutubeAccount))
                    {
                        YoutubeAccountRepository.Add(objYoutubeAccount);
                        YoutubeChannelRepository.Add(objYoutubeChannel);
                    }
                    else
                    {
                        Response.Redirect("Home.aspx");
                    }
                }

                Response.Redirect("Home.aspx");

            }
            catch (Exception Err)
            {
                Console.Write(Err.Message.ToString());
                logger.Error(Err.StackTrace );
                logger.Error(Err.Message);
            }
        }
    }
}