using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using GlobusWordpressLib;
using GlobusWordpressLib.Authentication;
using GlobusWordpressLib.App.Core;
using Newtonsoft.Json.Linq;
using System.Drawing;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for Wordpress
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Wordpress : System.Web.Services.WebService
    {
        WordpressAccountRepository objWPAccountRepo = new WordpressAccountRepository();
        TeamMemberProfileRepository objTeamMemberProfileRepo = new TeamMemberProfileRepository();
        TeamRepository objTeamRepository = new TeamRepository();
        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
        WordpressSitesRepository objWordpressSitesRepository = new WordpressSitesRepository();
        WordpressFeedsRepository objWordpressFeedsRepository = new WordpressFeedsRepository();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddWordpressAccount(string code, string UserId, string GroupId)
        {
            WordpressAccountRepository objWPAccountRepo = new WordpressAccountRepository();
            Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
            Domain.Socioboard.Domain.WordpressAccount objWordpressAccount = new Domain.Socioboard.Domain.WordpressAccount();
            string client_id = ConfigurationManager.AppSettings["WordpessClientID"];
            string redirect_uri = ConfigurationManager.AppSettings["WordpessCallBackURL"];
            string client_secret = ConfigurationManager.AppSettings["WordpessClientSecret"];
            oAuthWordpress _oAuth = new oAuthWordpress();
            _oAuth.client_id = client_id;
            _oAuth.client_secret = client_secret;
            _oAuth.redirect_uri = redirect_uri;
            _oAuth.code = code;
            string postdata=_oAuth.PostDataToGetAccessToken();
            try
            {
                string _token = _oAuth.APIWebRequest(Globals._TokenUrl, postdata);
                string access_token = JObject.Parse(_token)["access_token"].ToString();
                _oAuth.access_token = access_token;
                //string postdata1 = _oAuth.PostDataToGetAccessToken();
                //string _me = _oAuth.APIWebRequest(Globals._UserInfo, postdata1);
                string userinfo = _oAuth.APIWebRequestToGetUserInfo(Globals._UserInfo);
                JObject WP_User = JObject.Parse(userinfo);
                #region AddWPAccount
                objWordpressAccount.Id = Guid.NewGuid();
                objWordpressAccount.WpUserId = WP_User["ID"].ToString();
                objWordpressAccount.WpUserName = WP_User["username"].ToString();
                objWordpressAccount.DisplayName = WP_User["display_name"].ToString();
                objWordpressAccount.EmailId = WP_User["email"].ToString();
                objWordpressAccount.PrimaryBlogId = WP_User["primary_blog"].ToString();
                objWordpressAccount.TokenSiteId = WP_User["token_site_id"].ToString();
                objWordpressAccount.UserAvtar = WP_User["avatar_URL"].ToString();
                objWordpressAccount.ProfileUrl = WP_User["profile_URL"].ToString();
                objWordpressAccount.SiteCount = Int32.Parse(WP_User["site_count"].ToString());
                objWordpressAccount.UserId = Guid.Parse(UserId);
                objWordpressAccount.AccessToken = access_token;
                if (!objWPAccountRepo.IsProfileAllreadyExist(objWordpressAccount.UserId, objWordpressAccount.WpUserId))
                {
                    objWPAccountRepo.AddWordpressAccount(objWordpressAccount);
                }
                #endregion

                #region AddTeamMemberProfiles
                Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(GroupId));
                objTeamMemberProfile.Id = Guid.NewGuid();
                objTeamMemberProfile.ProfileId = WP_User["ID"].ToString();
                objTeamMemberProfile.TeamId = objTeam.Id;
                objTeamMemberProfile.ProfileType = "wordpress";
                objTeamMemberProfile.Status = 1;
                objTeamMemberProfile.StatusUpdateDate = DateTime.Now;
                if (!objTeamMemberProfileRepo.checkTeamMemberProfile(objTeam.Id, objTeamMemberProfile.ProfileId))
                {
                    objTeamMemberProfileRepo.addNewTeamMember(objTeamMemberProfile);
                }
                #endregion
                #region AddSocialProfile
                Domain.Socioboard.Domain.SocialProfile objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
                objSocialProfile.Id = Guid.NewGuid();
                objSocialProfile.UserId = Guid.Parse(UserId);
                objSocialProfile.ProfileType = "wordpress";
                objSocialProfile.ProfileId = WP_User["ID"].ToString();
                objSocialProfile.ProfileDate = DateTime.Now;
                objSocialProfile.ProfileStatus = 1;
                if (!objSocialProfilesRepository.checkUserProfileExist(objSocialProfile))
                {
                    objSocialProfilesRepository.addNewProfileForUser(objSocialProfile);
                }

                GetUserSites(UserId, objWordpressAccount.WpUserId);

                //GetUsersofblog(UserId,objWordpressAccount.WpUserId);
                #endregion
            }
            catch (Exception ex)
            {
            }
            return "";
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void GetUsersofblog(string UserId, string SiteId)
        {
            Domain.Socioboard.Domain.WordpressAccount objWordpressAccount = new Domain.Socioboard.Domain.WordpressAccount();
            objWordpressAccount = objWPAccountRepo.GetWordpressAccountById(Guid.Parse(UserId),SiteId);
            oAuthWordpress _oAuth = new oAuthWordpress();
            _oAuth.access_token = objWordpressAccount.AccessToken;
            string userinfo = _oAuth.APIWebRequestToGetUserInfo(Globals._Usersofblog + objWordpressAccount.PrimaryBlogId + "/users");
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void GetUserSites(string UserId, string WpUserId)
        {
            Domain.Socioboard.Domain.WordpressAccount objWordpressAccount = new Domain.Socioboard.Domain.WordpressAccount();
            
            objWordpressAccount = objWPAccountRepo.GetWordpressAccountById(Guid.Parse(UserId), WpUserId);
            oAuthWordpress _oAuth = new oAuthWordpress();
            _oAuth.access_token = objWordpressAccount.AccessToken;
            string usersites = _oAuth.APIWebRequestToGetUserInfo(Globals._UserSites);
            JObject User_Site = JObject.Parse(usersites);
            var Site_Data = User_Site["sites"];
            foreach (var site in Site_Data)
            {
                Domain.Socioboard.Domain.WordpressSites objWordpressSites = new Domain.Socioboard.Domain.WordpressSites();
                objWordpressSites.Id = Guid.NewGuid();
                objWordpressSites.SiteId = site["ID"].ToString();
                objWordpressSites.SiteName = site["name"].ToString();
                objWordpressSites.Description = site["description"].ToString();
                objWordpressSites.Post_Count = site["post_count"].ToString();
                objWordpressSites.Subscribers_Count = site["subscribers_count"].ToString();
                objWordpressSites.SiteURL = site["URL"].ToString();
                objWordpressSites.UserId = Guid.Parse(UserId);
                objWordpressSites.WPUserId = WpUserId;
                objWordpressSites.CreatedTime = DateTime.Parse(site["options"]["created_at"].ToString());
                objWordpressSites.EntryTime = DateTime.Now;
                if (!objWordpressSitesRepository.IsSiteAllreadyExist(objWordpressSites.UserId, objWordpressSites.WPUserId, objWordpressSites.SiteId))
                {
                    objWordpressSitesRepository.AddWordpressSites(objWordpressSites);
                }
                GetUserFeedBySite(UserId, objWordpressSites.WPUserId, objWordpressSites.SiteId, objWordpressAccount.AccessToken);
            }
        }
            
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void GetUserFeedBySite(string UserId, string WPUserId, string SiteId, string AccessToken)
        {
            Domain.Socioboard.Domain.WordpressFeeds _WordpressFeeds = new Domain.Socioboard.Domain.WordpressFeeds();
            oAuthWordpress _oAuth = new oAuthWordpress();
            _oAuth.access_token = AccessToken;
            string userposts = _oAuth.APIWebRequestToGetUserInfo(Globals._UserPosts.Replace("[SiteId]",SiteId));
            JObject User_Site = JObject.Parse(userposts);
            var postcontaint = User_Site["posts"];
            foreach (var post_item in postcontaint)
            {
                _WordpressFeeds.Id=Guid.NewGuid();
                _WordpressFeeds.SiteId = post_item["site_ID"].ToString();
                _WordpressFeeds.PostId = post_item["ID"].ToString();
                _WordpressFeeds.CreatedTime = DateTime.Parse(post_item["date"].ToString());
                _WordpressFeeds.ModifiedTime = DateTime.Parse(post_item["modified"].ToString());
                _WordpressFeeds.Title = post_item["title"].ToString();
                _WordpressFeeds.PostUrl = post_item["URL"].ToString();
                _WordpressFeeds.PostContent = post_item["content"].ToString();
                _WordpressFeeds.CommentCount = post_item["comment_count"].ToString();
                _WordpressFeeds.LikeCount = post_item["like_count"].ToString();
                _WordpressFeeds.ILike = post_item["i_like"].ToString();
                _WordpressFeeds.EntryTime = DateTime.Now;
                _WordpressFeeds.WPUserId = WPUserId;
                _WordpressFeeds.UserId = Guid.Parse(UserId);

                if (!objWordpressFeedsRepository.checkWordpressFeedExists(_WordpressFeeds.PostId,Guid.Parse(UserId),SiteId))
                {
                    objWordpressFeedsRepository.addWordpressFeed(_WordpressFeeds); 
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string PostBlog(string SiteId, string WPUserId, string Message, string Tittle,string tags, string UserId, byte[] imagebytes)
        {
            //string file = string.Empty;
            //MemoryStream stream = new MemoryStream(image);
            //Image img = Image.FromStream(stream);
            //FileStream fs= stream as FileStream;
            //string filename = fs.Name;
            //string type = img.RawFormat.GetType().ToString();
            //var path = Server.MapPath("~/" + System.Configuration.ConfigurationManager.AppSettings["domain"] + "/Contents/img/upload");
            //string imageid=Guid.NewGuid().ToString();
            //file = path + "\\" + SiteId + "_" + WPUserId + "_" + imageid + ".png";
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}
           
            //File.WriteAllBytes(file,image);
            string encodedimg = string.Empty;
            try
            {
                encodedimg = Convert.ToBase64String(imagebytes);
            }
            catch (Exception ex)
            {
            }
            Domain.Socioboard.Domain.WordpressAccount objWordpressAccount = new Domain.Socioboard.Domain.WordpressAccount();
            objWordpressAccount = objWPAccountRepo.GetWordpressAccountById(Guid.Parse(UserId), WPUserId);
            oAuthWordpress _oAuth = new oAuthWordpress();
            try
            {
                _oAuth.access_token = objWordpressAccount.AccessToken;
                string postData = _oAuth.PostDataToPostBlog(Tittle, Message, tags, encodedimg);
                string _response = _oAuth.APIWebRequest(Globals._PostBlog.Replace("[SiteId]", SiteId), postData);
            }
            catch (Exception ex)
            {
            }
            return "Success";
        }

       

    }
}
