using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using System.Configuration;
using Facebook;
using SocioBoard.Model;
using SocioBoard.Helper;
using log4net;
using GlobusTumblerLib;
using GlobusTumblrLib.Authentication;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using GlobusTumblerLib.App.Core;
using System.Text.RegularExpressions;
namespace SocioBoard
{
    public partial class TumblrManager : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(TumblrManager));

        protected void Page_Load(object sender, EventArgs e)
        {
            User user = (User)Session["LoggedUser"];
            if (!IsPostBack)
            {
                GetAccessToken();
            }
        }


        private void GetAccessToken()
        {
            User user = (User)Session["LoggedUser"];
            if (user == null)
            {
                Response.Redirect("Default.aspx");
            }
            oAuthTumbler requestHelper = new oAuthTumbler();




            string code = Request.QueryString["oauth_verifier"];
            string AccessTokenResponse = requestHelper.GetAccessToken(oAuthTumbler.TumblrToken, code);

            string[] tokens = AccessTokenResponse.Split('&'); //extract access token & secret from response
            string accessToken = tokens[0].Split('=')[1];
            string accessTokenSecret = tokens[1].Split('=')[1];
          
            KeyValuePair<string, string> LoginDetails = new KeyValuePair<string, string>(accessToken, accessTokenSecret);

          
            string sstr = oAuthTumbler.OAuthData(Globals.UsersDashboardUrl, "GET", LoginDetails.Key, LoginDetails.Value, null);
       
            JObject profile = JObject.Parse(oAuthTumbler.OAuthData(Globals.UsersInfoUrl, "GET", LoginDetails.Key, LoginDetails.Value, null));
            JObject UserDashboard = JObject.Parse(oAuthTumbler.OAuthData(Globals.UsersDashboardUrl, "GET", LoginDetails.Key, LoginDetails.Value, null));
            TumblrAccount objTumblrAccount = new TumblrAccount();
            TumblrAccountRepository objTumblrAccountRepository = new TumblrAccountRepository();
            SocialProfile objSocialProfile = new SocialProfile();
            SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
            objSocialProfile.Id = Guid.NewGuid();
            objSocialProfile.UserId = user.Id;
            objSocialProfile.ProfileId = profile["response"]["user"]["name"].ToString();

            objSocialProfile.ProfileType = "tumblr";
            objSocialProfile.ProfileDate = DateTime.Now;
            objSocialProfile.ProfileStatus = 1;

            objTumblrAccount.Id = Guid.NewGuid();
            objTumblrAccount.tblrUserName = profile["response"]["user"]["name"].ToString();
            objTumblrAccount.UserId = user.Id;
            objTumblrAccount.tblrAccessToken = accessToken;
            objTumblrAccount.tblrAccessTokenSecret = accessTokenSecret;
            

            objTumblrAccount.tblrProfilePicUrl = profile["response"]["user"]["name"].ToString();
            objTumblrAccount.IsActive = 1;
            if (!objSocialProfilesRepository.checkUserProfileExist(objSocialProfile))
            {
                objSocialProfilesRepository.addNewProfileForUser(objSocialProfile);
                if (!objTumblrAccountRepository.checkTubmlrUserExists(objTumblrAccount))
                {
                    TumblrAccountRepository.Add(objTumblrAccount);

                    GroupRepository objGroupRepository = new GroupRepository();
                    SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)HttpContext.Current.Session["GroupName"];
                    Groups lstDetails = objGroupRepository.getGroupName(team.GroupId);
                    if (lstDetails.GroupName == "Socioboard")
                    {
                        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
                        TeamMemberProfile teammemberprofile = new TeamMemberProfile();
                        teammemberprofile.Id = Guid.NewGuid();
                        teammemberprofile.TeamId = team.Id;
                        teammemberprofile.ProfileId = objTumblrAccount.tblrUserName;
                        teammemberprofile.ProfileType = "tumblr";
                        teammemberprofile.StatusUpdateDate = DateTime.Now;

                        objTeamMemberProfileRepository.addNewTeamMember(teammemberprofile);
                    }
                }
            }
            else
            {
                if (!objTumblrAccountRepository.checkTubmlrUserExists(objTumblrAccount))
                {
                    TumblrAccountRepository.Add(objTumblrAccount);                  

                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }

            JArray objJarray = (JArray)UserDashboard["response"]["posts"];
        
            TumblrFeed objTumblrFeed = new TumblrFeed();
            TumblrFeedRepository objTumblrFeedRepository = new TumblrFeedRepository();
            foreach (var item in objJarray)
            {
                objTumblrFeed.Id = Guid.NewGuid();
                objTumblrFeed.UserId = user.Id;
                try
                {
                    objTumblrFeed.ProfileId = profile["response"]["user"]["name"].ToString();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                try
                {
                    objTumblrFeed.blogname = item["blog_name"].ToString();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                try
                {
                    objTumblrFeed.blogId = item["id"].ToString();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                try
                {
                    objTumblrFeed.blogposturl = item["post_url"].ToString();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                try
                {
                    String result = item["caption"].ToString();
                    objTumblrFeed.description = Regex.Replace(result, @"<[^>]*>", String.Empty);
                }
                catch (Exception ex)
                {
                    objTumblrFeed.description = null;
                    Console.WriteLine(ex.Message);
                }
                try
                {
                    objTumblrFeed.slug = item["slug"].ToString();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                try
                {
                    objTumblrFeed.type = item["type"].ToString();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                try
                {
                    string test = item["date"].ToString();
                    DateTime dt;
                    if (test.Contains("GMT"))
                    {
                        test = test.Replace("GMT", "").Trim().ToString();
                        dt = Convert.ToDateTime(test);
                    }
                    else
                    {
                        test = test.Replace("GMT", "").Trim().ToString();
                        dt = Convert.ToDateTime(test);
                    }
                    objTumblrFeed.date = dt;
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                try
                {
                    objTumblrFeed.reblogkey = item["reblog_key"].ToString();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                try
                {
                    string str = item["liked"].ToString();
                    if (str == "False")
                    {
                        objTumblrFeed.liked = 0;
                    }
                    else { objTumblrFeed.liked = 1; }
                    
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                try
                {
                    string str = item["followed"].ToString();
                    if (str == "false")
                    {
                        objTumblrFeed.followed = 0;
                    }
                    else { objTumblrFeed.followed = 1; }
                   // objTumblrDashboard.followed = Convert.ToInt16(item["followed"]);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                try
                {
                    objTumblrFeed.canreply = Convert.ToInt16(item["can_reply"]);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                try
                {
                    objTumblrFeed.sourceurl = item["source_url"].ToString();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                try
                {
                    objTumblrFeed.sourcetitle = item["source_title"].ToString();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                try
                {                   
                    JArray asdasd12 = (JArray)item["photos"];
                    foreach (var item1 in asdasd12)
                    {
                        objTumblrFeed.imageurl = item1["original_size"]["url"].ToString();
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                try
                {
                    objTumblrFeed.videourl = item["permalink_url"].ToString();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
               
                try
                {
                    string str = item["note_count"].ToString();
                    objTumblrFeed.notes = Convert.ToInt16(str);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }

                objTumblrFeed.timestamp = DateTime.Now;
                if (!objTumblrFeedRepository.checkTumblrMessageExists(objTumblrFeed))
                {
                    TumblrFeedRepository.Add(objTumblrFeed);
                }                              
            }
            Response.Redirect("Home.aspx");

        }

    }
}