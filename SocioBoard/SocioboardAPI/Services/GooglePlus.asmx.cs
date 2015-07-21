using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using GlobusGooglePlusLib.App.Core;
using GlobusGooglePlusLib.Authentication;
using log4net;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Web.Script.Serialization;


namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for GooglePlus
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class GooglePlus : System.Web.Services.WebService
    {
        ILog logger = LogManager.GetLogger(typeof(GooglePlus));
        GooglePlusAccountRepository objGooglePlusAccountRepository = new GooglePlusAccountRepository();
        TeamRepository objTeamRepository = new TeamRepository();
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
        GooglePlusActivitiesRepository objGooglePlusActivitiesRepository = new GooglePlusActivitiesRepository();
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddGPlusAccount(string client_id, string client_secret, string redirect_uri, string UserId, string GroupId, string code)
        {
            string ret = string.Empty;
            string objRefresh = string.Empty;
            string refreshToken = string.Empty;
            string access_token = string.Empty;
            try
            {

                oAuthTokenGPlus ObjoAuthTokenGPlus = new oAuthTokenGPlus();
                oAuthToken objToken = new oAuthToken();
                objToken.ConsumerKey = client_id;
                objToken.ConsumerSecret = client_secret;

                try
                {
                    objRefresh = ObjoAuthTokenGPlus.GetRefreshToken(code, client_id, client_secret, redirect_uri);
                    logger.Error("vikash: ObjoAuthTokenGPlus()");
                }
                catch (Exception ex) { }
                JObject objaccesstoken = JObject.Parse(objRefresh);

                try
                {
                    refreshToken = objaccesstoken["refresh_token"].ToString();

                }
                catch (Exception ex)
                {
                    access_token = objaccesstoken["access_token"].ToString();
                    ObjoAuthTokenGPlus.RevokeToken(access_token);
                    Console.WriteLine(ex.StackTrace);
                    ret = "Refresh Token Not Found";
                    return ret;

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
                    userinfo = objToken.GetUserInfo("self", access_token.ToString());
                }
                catch (Exception ex)
                {
                }
                Domain.Socioboard.Domain.GooglePlusAccount _GooglePlusAccount = new Domain.Socioboard.Domain.GooglePlusAccount();
                foreach (var itemuserinfo in userinfo)
                {

                    try
                    {
                        _GooglePlusAccount.Id = Guid.NewGuid();
                        _GooglePlusAccount.GpUserId = itemuserinfo["id"].ToString();
                        _GooglePlusAccount.GpUserName = itemuserinfo["name"].ToString();
                        _GooglePlusAccount.GpProfileImage = itemuserinfo["picture"].ToString();
                        _GooglePlusAccount.IsActive = 1;
                        _GooglePlusAccount.ProfileType = "gplus";
                        _GooglePlusAccount.AccessToken = access_token;
                        _GooglePlusAccount.RefreshToken = refreshToken;
                        _GooglePlusAccount.EmailId = itemuserinfo["email"].ToString();
                        _GooglePlusAccount.UserId = Guid.Parse(UserId);
                        _GooglePlusAccount.EntryDate = DateTime.Now;


                    }
                    catch (Exception ex)
                    {
                        logger.Error("AddGPlusAccount => GooglePlusAccount =>" + ex.Message);
                    }

                }
                #region Get_InYourCircles
                try
                {
                    string _InyourCircles = ObjoAuthTokenGPlus.APIWebRequestToGetUserInfo(Globals.strGetPeopleList.Replace("[userId]", _GooglePlusAccount.GpUserId).Replace("[collection]", "visible") + "?key=" + ConfigurationManager.AppSettings["Api_Key"].ToString(), access_token);
                    JObject J_InyourCircles = JObject.Parse(_InyourCircles);
                    _GooglePlusAccount.InYourCircles = Convert.ToInt32(J_InyourCircles["totalItems"].ToString());
                }
                catch (Exception ex)
                {
                    _GooglePlusAccount.InYourCircles = 0;
                }
                #endregion

                #region Get_HaveYouInCircles
                try
                {
                    string _HaveYouInCircles = ObjoAuthTokenGPlus.APIWebRequestToGetUserInfo(Globals.strGetPeopleProfile + _GooglePlusAccount.GpUserId + "?key=" + ConfigurationManager.AppSettings["Api_Key"].ToString(), access_token);
                    JObject J_HaveYouInCircles = JObject.Parse(_HaveYouInCircles);
                    _GooglePlusAccount.HaveYouInCircles = Convert.ToInt32(J_HaveYouInCircles["circledByCount"].ToString());
                }
                catch (Exception ex)
                {
                    _GooglePlusAccount.HaveYouInCircles = 0;
                }
                #endregion

                #region Add GPlusAccount
                if (!objGooglePlusAccountRepository.checkGooglePlusUserExists(_GooglePlusAccount.GpUserId, _GooglePlusAccount.UserId))
                {
                    objGooglePlusAccountRepository.addGooglePlusUser(_GooglePlusAccount);
                    #region Add TeamMemberProfile
                    Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(GroupId));
                    Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
                    objTeamMemberProfile.Id = Guid.NewGuid();
                    objTeamMemberProfile.TeamId = objTeam.Id;
                    objTeamMemberProfile.Status = 1;
                    objTeamMemberProfile.ProfileType = "gplus";
                    objTeamMemberProfile.StatusUpdateDate = DateTime.Now;
                    objTeamMemberProfile.ProfileId = _GooglePlusAccount.GpUserId;
                    objTeamMemberProfile.ProfilePicUrl = _GooglePlusAccount.GpProfileImage;
                    objTeamMemberProfile.ProfileName = _GooglePlusAccount.GpUserName;
                    objTeamMemberProfileRepository.addNewTeamMember(objTeamMemberProfile);
                    #endregion
                    #region SocialProfile
                    Domain.Socioboard.Domain.SocialProfile objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
                    objSocialProfile.Id = Guid.NewGuid();
                    objSocialProfile.ProfileType = "gplus";
                    objSocialProfile.ProfileId = _GooglePlusAccount.GpUserId;
                    objSocialProfile.UserId = Guid.Parse(UserId);
                    objSocialProfile.ProfileDate = DateTime.Now;
                    objSocialProfile.ProfileStatus = 1;
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

                #endregion
                GetUserActivities(UserId, _GooglePlusAccount.GpUserId, access_token);
                return new JavaScriptSerializer().Serialize(_GooglePlusAccount);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return "";
            }
            
        }

        public void GetUserActivities(string UserId, string ProfileId, string AcessToken)
        {
            oAuthTokenGPlus ObjoAuthTokenGPlus = new oAuthTokenGPlus();
            try
            {
                Domain.Socioboard.Domain.GooglePlusActivities _GooglePlusActivities = null;
                string _Activities = ObjoAuthTokenGPlus.APIWebRequestToGetUserInfo(Globals.strGetActivitiesList.Replace("[ProfileId]", ProfileId) + "?key=" + ConfigurationManager.AppSettings["Api_Key"].ToString(), AcessToken);
                JObject J_Activities = JObject.Parse(_Activities);
                foreach (var item in J_Activities["items"])
                {
                    _GooglePlusActivities = new Domain.Socioboard.Domain.GooglePlusActivities();
                    _GooglePlusActivities.Id = Guid.NewGuid();
                    _GooglePlusActivities.UserId = Guid.Parse(UserId);
                    _GooglePlusActivities.GpUserId = ProfileId;
                    try
                    {
                        _GooglePlusActivities.FromUserName = item["actor"]["displayName"].ToString();
                    }
                    catch { }
                    try
                    {
                        _GooglePlusActivities.FromId = item["actor"]["id"].ToString();
                    }
                    catch { }
                    try
                    {
                        _GooglePlusActivities.ActivityId = item["id"].ToString();
                    }
                    catch { }
                    try
                    {
                        _GooglePlusActivities.ActivityUrl = item["url"].ToString();
                    }
                    catch { }
                    try
                    {
                        _GooglePlusActivities.Title = item["title"].ToString();
                    }
                    catch { }
                    _GooglePlusActivities.EntryDate = DateTime.Now;
                    try
                    {
                        _GooglePlusActivities.FromProfileImage = item["actor"]["image"]["url"].ToString();
                    }
                    catch { }
                    try
                    {
                        _GooglePlusActivities.Content = item["object"]["content"].ToString();
                    }
                    catch { }
                    try
                    {
                        _GooglePlusActivities.PublishedDate = Convert.ToDateTime(item["published"].ToString());
                    }
                    catch { }
                    try
                    {
                        _GooglePlusActivities.PlusonersCount = Convert.ToInt32(item["object"]["plusoners"]["totalItems"].ToString());
                    }
                    catch { }
                    try
                    {
                        _GooglePlusActivities.RepliesCount = Convert.ToInt32(item["object"]["replies"]["totalItems"].ToString());
                    }
                    catch { }
                    try
                    {
                        _GooglePlusActivities.ResharersCount = Convert.ToInt32(item["object"]["resharers"]["totalItems"].ToString());
                    }
                    catch { }
                    try
                    {
                        _GooglePlusActivities.AttachmentType = item["object"]["attachments"][0]["objectType"].ToString();
                        if (_GooglePlusActivities.AttachmentType == "video") 
                        {
                            _GooglePlusActivities.Attachment = item["object"]["attachments"][0]["embed"]["url"].ToString();
                        }
                        else if (_GooglePlusActivities.AttachmentType == "photo") {
                            _GooglePlusActivities.Attachment = item["object"]["attachments"][0]["image"]["url"].ToString();
                        }
                        else if (_GooglePlusActivities.AttachmentType == "article")
                        {
                            try
                            {
                                _GooglePlusActivities.Attachment = item["object"]["attachments"][0]["image"]["url"].ToString();
                            }
                            catch { }
                            try
                            {
                                _GooglePlusActivities.ArticleDisplayname = item["object"]["attachments"][0]["displayName"].ToString();
                            }
                            catch { }
                            try
                            {
                                _GooglePlusActivities.ArticleContent = item["object"]["attachments"][0]["content"].ToString();
                            }
                            catch { }
                            try
                            {
                                _GooglePlusActivities.Link = item["object"]["attachments"][0]["url"].ToString();
                            }
                            catch { }
                        }
                    }
                    catch (Exception ex)
                    {
                        _GooglePlusActivities.AttachmentType = "note";
                        _GooglePlusActivities.Attachment = "";
                    }
                    if (!objGooglePlusActivitiesRepository.checkgoogleplusActivityExists(_GooglePlusActivities.ActivityId, Guid.Parse(UserId)))
                    {
                        objGooglePlusActivitiesRepository.addgoogleplusActivity(_GooglePlusActivities);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetUserActivities => "+ex.Message);
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetGPusData(string UserId, string ProfileId)
        {


            return "";
        }

    }
}
