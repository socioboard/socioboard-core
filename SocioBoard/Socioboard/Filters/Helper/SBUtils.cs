using Newtonsoft.Json.Linq;
using Socioboard.Api.TeamMemberProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
using Domain.Socioboard.Factory;
using System.Web.Script.Serialization;
using System.Configuration;
using Facebook;


namespace Socioboard.Helper
{
    public static class SBUtils
    {
        public static Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, object> GetUserProfilesccordingToGroup()
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, object> dict_TeamMember = new Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, object>();
            Api.Team.Team objApiTeam = new Api.Team.Team();
            JObject team = JObject.Parse(objApiTeam.GetTeamByGroupId(System.Web.HttpContext.Current.Session["group"].ToString()));
            Api.TeamMemberProfile.TeamMemberProfile objApiTeamMemberProfile = new Api.TeamMemberProfile.TeamMemberProfile();
            JArray TeamMemberProfiles = JArray.Parse(objApiTeamMemberProfile.GetTeamMemberProfilesByTeamId(Convert.ToString(team["Id"])));

            foreach (var item in TeamMemberProfiles)
            {
                try
                {
                    Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = GetTeamMemberProfileFromJObject(item);

                    ISocialSiteAccount objISocialSiteAccount = GetSocialAccountFromTeamMemberProfile(objUser, objTeamMemberProfile);
                    SocialSiteAccountFactory objSocialSiteAccountFactory = new SocialSiteAccountFactory(objTeamMemberProfile.ProfileType);
                    dict_TeamMember.Add(objTeamMemberProfile, objISocialSiteAccount);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    //return null;
                }
            }
            return dict_TeamMember;


        }

        private static Domain.Socioboard.Domain.TeamMemberProfile GetTeamMemberProfileFromJObject(JToken item)
        {
            Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
            objTeamMemberProfile.Id = Guid.Parse(Convert.ToString(item["Id"]));
            objTeamMemberProfile.TeamId = Guid.Parse(Convert.ToString(item["TeamId"]));
            objTeamMemberProfile.ProfileId = Convert.ToString(item["ProfileId"]);
            objTeamMemberProfile.ProfileType = Convert.ToString(item["ProfileType"]);
            objTeamMemberProfile.Status = Convert.ToInt16(Convert.ToString(item["Status"]));
            objTeamMemberProfile.StatusUpdateDate = Convert.ToDateTime(Convert.ToString(item["StatusUpdateDate"]));
            return objTeamMemberProfile;
        }

        private static ISocialSiteAccount GetSocialAccountFromTeamMemberProfile(User objUser, Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile)
        {
            ISocialSiteAccount objSocialSiteAccount = null;
            if (objTeamMemberProfile.ProfileType == "facebook")
            {
                Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
                objSocialSiteAccount = (FacebookAccount)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.getFacebookAccountDetailsById(objUser.Id.ToString(), objTeamMemberProfile.ProfileId.ToString()), typeof(FacebookAccount)));
            }
            else if (objTeamMemberProfile.ProfileType == "twitter")
            {
                Api.TwitterAccount.TwitterAccount ApiobjTwitterAccount = new Api.TwitterAccount.TwitterAccount();
                objSocialSiteAccount = (TwitterAccount)(new JavaScriptSerializer().Deserialize(ApiobjTwitterAccount.GetTwitterAccountDetailsById(objUser.Id.ToString(), objTeamMemberProfile.ProfileId.ToString()), typeof(TwitterAccount)));
            }
            else if (objTeamMemberProfile.ProfileType == "linkedin")
            {
                Api.LinkedinAccount.LinkedinAccount ApiobjLinkedinAccount = new Api.LinkedinAccount.LinkedinAccount();
                objSocialSiteAccount = (LinkedInAccount)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinAccount.GetLinkedinAccountDetailsById(objUser.Id.ToString(), objTeamMemberProfile.ProfileId.ToString()), typeof(LinkedInAccount)));
            }
            else if (objTeamMemberProfile.ProfileType == "instagram")
            {
                Api.InstagramAccount.InstagramAccount ApiobjInstagramAccount = new Api.InstagramAccount.InstagramAccount();
                objSocialSiteAccount = (InstagramAccount)(new JavaScriptSerializer().Deserialize(ApiobjInstagramAccount.UserInformation(objUser.Id.ToString(), objTeamMemberProfile.ProfileId.ToString()), typeof(InstagramAccount)));
            }
            else if (objTeamMemberProfile.ProfileType == "youtube")
            {
                Api.YoutubeAccount.YoutubeAccount ApiobjYoutubeAccount = new Api.YoutubeAccount.YoutubeAccount();
                objSocialSiteAccount = (YoutubeAccount)(new JavaScriptSerializer().Deserialize(ApiobjYoutubeAccount.GetYoutubeAccountDetailsById(objUser.Id.ToString(), objTeamMemberProfile.ProfileId.ToString()), typeof(YoutubeAccount)));
            }
            else if (objTeamMemberProfile.ProfileType == "tumblr")
            {
                Api.TumblrAccount.TumblrAccount ApiobjTumblrAccount = new Api.TumblrAccount.TumblrAccount();
                objSocialSiteAccount = (TumblrAccount)(new JavaScriptSerializer().Deserialize(ApiobjTumblrAccount.GetTumblrAccountDetailsById(objUser.Id.ToString(), objTeamMemberProfile.ProfileId.ToString()), typeof(TumblrAccount)));
            }
            return objSocialSiteAccount;
        }

        public static List<Domain.Socioboard.Domain.TeamMemberProfile> GetUserTeamMemberProfiles()
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            List<Domain.Socioboard.Domain.TeamMemberProfile> lstTeamMemberProfile = new List<Domain.Socioboard.Domain.TeamMemberProfile>();
            Api.Team.Team objApiTeam = new Api.Team.Team();
            string groupid = System.Web.HttpContext.Current.Session["group"].ToString();
            JObject team = JObject.Parse(objApiTeam.GetTeamByGroupId(System.Web.HttpContext.Current.Session["group"].ToString()));
            Api.TeamMemberProfile.TeamMemberProfile objApiTeamMemberProfile = new Api.TeamMemberProfile.TeamMemberProfile();
            JArray TeamMemberProfiles = JArray.Parse(objApiTeamMemberProfile.GetTeamMemberProfilesByTeamId(Convert.ToString(team["Id"])));

            foreach (var item in TeamMemberProfiles)
            {
                try
                {
                    Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = GetTeamMemberProfileFromJObject(item);
                    lstTeamMemberProfile.Add(objTeamMemberProfile);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    //return null;
                }
            }
            return lstTeamMemberProfile;


        }

        public static List<object> check(List<Domain.Socioboard.Domain.TeamMemberProfile> TeamMemberProfile)
        {

            return null;
        }
     
        public static Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, Dictionary<object, List<object>>> GetUserProfilesSnapsAccordingToGroup(List<Domain.Socioboard.Domain.TeamMemberProfile> TeamMemberProfile)
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, Dictionary<object, List<object>>> dic_profilessnap = new Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, Dictionary<object, List<object>>>();
            var dicprofilefeeds = new Dictionary<object, List<object>>();
            foreach (Domain.Socioboard.Domain.TeamMemberProfile item in TeamMemberProfile)
            {
                List<object> feeds = null;
                if (item.ProfileType == "facebook")
                {
                    feeds = new List<object>();
                    Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
                    FacebookAccount objFacebookAccount = (FacebookAccount)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.getFacebookAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(FacebookAccount)));
                    Api.FacebookFeed.FacebookFeed ApiobjFacebookFeed = new Api.FacebookFeed.FacebookFeed();
                    List<FacebookFeed> lstFacebookFeed = (List<FacebookFeed>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookFeed.getAllFacebookFeedsByUserIdAndProfileId(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(List<FacebookFeed>)));
                    foreach (var facebookfeed in lstFacebookFeed)
                    {
                        feeds.Add(facebookfeed);
                    }
                    dicprofilefeeds.Add(objFacebookAccount, feeds);
                    dic_profilessnap.Add(item, dicprofilefeeds);
                }

                if (item.ProfileType == "twitter")
                {
                    feeds = new List<object>();
                    Api.TwitterAccount.TwitterAccount ApiobjTwitterAccount = new Api.TwitterAccount.TwitterAccount();
                    TwitterAccount objTwitterAccount = (TwitterAccount)(new JavaScriptSerializer().Deserialize(ApiobjTwitterAccount.GetTwitterAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(TwitterAccount)));
                    Api.TwitterFeed.TwitterFeed ApiobjTwitterFeed = new Api.TwitterFeed.TwitterFeed();
                    List<TwitterFeed> lstTwitterFeed = (List<TwitterFeed>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterFeed.GetAllTwitterFeedsByUserIdAndProfileId(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(List<TwitterFeed>)));
                    foreach (var twitterfeed in lstTwitterFeed)
                    {
                        feeds.Add(twitterfeed);
                    }
                    dicprofilefeeds.Add(objTwitterAccount, feeds);
                    dic_profilessnap.Add(item, dicprofilefeeds);
                }

                if (item.ProfileType == "linkedin")
                {
                    feeds = new List<object>();
                    Api.LinkedinAccount.LinkedinAccount ApiobjLinkedinAccount = new Api.LinkedinAccount.LinkedinAccount();
                    LinkedInAccount objLinkedInAccount = (LinkedInAccount)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinAccount.GetLinkedinAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(LinkedInAccount)));
                    Api.LinkedInFeed.LinkedInFeed ApiobjLinkedInFeed = new Api.LinkedInFeed.LinkedInFeed();
                    List<LinkedInFeed> lstLinkedInFeed = (List<LinkedInFeed>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedInFeed.GetLinkedInFeeds(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(List<LinkedInFeed>)));
                    foreach (var LinkedInFeed in lstLinkedInFeed)
                    {
                        feeds.Add(LinkedInFeed);
                    }
                    dicprofilefeeds.Add(objLinkedInAccount, feeds);
                    dic_profilessnap.Add(item, dicprofilefeeds);
                }
                if (item.ProfileType == "instagram")
                {
                    feeds = new List<object>();
                    Api.InstagramAccount.InstagramAccount ApiobjInstagramAccount = new Api.InstagramAccount.InstagramAccount();
                    InstagramAccount objInstagramAccount = (InstagramAccount)(new JavaScriptSerializer().Deserialize(ApiobjInstagramAccount.UserInformation(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(InstagramAccount)));
                    dicprofilefeeds.Add(objInstagramAccount, feeds);
                    dic_profilessnap.Add(item, dicprofilefeeds);
                }

                if (item.ProfileType == "tumblr")
                {
                    feeds = new List<object>();
                    Api.TumblrAccount.TumblrAccount ApiobjTumblrAccount = new Api.TumblrAccount.TumblrAccount();
                    TumblrAccount objTumblrAccount = (TumblrAccount)(new JavaScriptSerializer().Deserialize(ApiobjTumblrAccount.GetTumblrAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(TumblrAccount)));
                    dicprofilefeeds.Add(objTumblrAccount, feeds);
                    dic_profilessnap.Add(item, dicprofilefeeds);
                }


                if (item.ProfileType == "youtube")
                {
                    feeds = new List<object>();
                    Api.YoutubeAccount.YoutubeAccount ApiobjYoutubeAccount = new Api.YoutubeAccount.YoutubeAccount();
                    YoutubeAccount objYoutubeAccount = (YoutubeAccount)(new JavaScriptSerializer().Deserialize(ApiobjYoutubeAccount.GetYoutubeAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(YoutubeAccount)));
                    Api.YoutubeChannel.YoutubeChannel ApiobjYoutubeChannel = new Api.YoutubeChannel.YoutubeChannel();
                    YoutubeChannel objYoutubeChannel = (YoutubeChannel)(new JavaScriptSerializer().Deserialize(ApiobjYoutubeChannel.GetAllYoutubeChannelByUserIdAndProfileId(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(YoutubeChannel)));
                    List<YoutubeChannel> lstYoutubeChannel = new List<YoutubeChannel>();
                    lstYoutubeChannel.Add(objYoutubeChannel);
                    foreach (var youtubechannel in lstYoutubeChannel)
                    {
                        feeds.Add(youtubechannel);
                    }
                    dicprofilefeeds.Add(objYoutubeAccount, feeds);
                    dic_profilessnap.Add(item, dicprofilefeeds);
                }

            }
            return dic_profilessnap;
        }

        public static int GetUserPackageProfileCount(string accounttype)
        {
            int tot_acc = 0;
            if (accounttype.ToString().ToLower() == AccountType.Deluxe.ToString().ToLower())
                tot_acc = 50;
            else if (accounttype.ToString().ToLower() == AccountType.Standard.ToString().ToLower())
                tot_acc = 10;
            else if (accounttype.ToString().ToLower() == AccountType.Premium.ToString().ToLower())
                tot_acc = 20;
            else if (accounttype.ToString().ToLower() == AccountType.Free.ToString().ToLower())
                tot_acc = 5;

            else if (accounttype.ToString().ToLower() == AccountType.SocioBasic.ToString().ToLower())
                tot_acc = 100;
            else if (accounttype.ToString().ToLower() == AccountType.SocioStandard.ToString().ToLower())
                tot_acc = 200;
            else if (accounttype.ToString().ToLower() == AccountType.SocioPremium.ToString().ToLower())
                tot_acc = 500;
            else if (accounttype.ToString().ToLower() == AccountType.SocioDeluxe.ToString().ToLower())
                tot_acc = 1000;
            return tot_acc;
        }

        public static string Getsortpofilename(string name)
        {
            string ret = string.Empty;
            try
            {
                if (name.Length > 10)
                {
                    ret = name.Substring(0, 10) + "..";
                }
                else
                {
                    ret = name;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return ret;

        }

        public static Dictionary<string, List<object>> GetFeedsMenuAccordingToGroup()
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Dictionary<string, List<object>> dic_profilessnap = new Dictionary<string, List<object>>();
               
            List<object> socialaccounts = null;
                
                    socialaccounts = new List<object>();
                    Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
                    List<FacebookAccount> lstFacebookAccount = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.GetAllFacebookAccountsByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<FacebookAccount>)));
                    foreach (var FacebookAccount in lstFacebookAccount)
                    {
                        socialaccounts.Add(FacebookAccount);
                    }
                    dic_profilessnap.Add("facebook", socialaccounts);
               
                    socialaccounts = new List<object>();
                    Api.TwitterAccount.TwitterAccount ApiobjTwitterAccount = new Api.TwitterAccount.TwitterAccount();
                    List<TwitterAccount> lstTwitterAccount = (List<TwitterAccount>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterAccount.GetAllTwitterAccountsByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<TwitterAccount>)));
                    foreach (var TwitterAccount in lstTwitterAccount)
                    {
                        socialaccounts.Add(TwitterAccount);
                    }
                    dic_profilessnap.Add("twitter", socialaccounts);
               
                    socialaccounts = new List<object>();
                    Api.LinkedinAccount.LinkedinAccount ApiobjLinkedinAccount = new Api.LinkedinAccount.LinkedinAccount();
                    List<LinkedInAccount> lstLinkedinAccount = (List<LinkedInAccount>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinAccount.GetAllLinkedinAccountsByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<LinkedInAccount>)));
                    foreach (var LinkedInAccount in lstLinkedinAccount)
                    {
                        socialaccounts.Add(LinkedInAccount);
                    }
                    dic_profilessnap.Add("linkedin", socialaccounts);
                
                    socialaccounts = new List<object>();
                    Api.InstagramAccount.InstagramAccount ApiobjInstagramAccount = new Api.InstagramAccount.InstagramAccount();
                    List<InstagramAccount> lstInstagramAccount = (List<InstagramAccount>)(new JavaScriptSerializer().Deserialize(ApiobjInstagramAccount.GetAllInstagramAccountsByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<InstagramAccount>)));
                    foreach (var InstagramAccount in lstInstagramAccount)
                    {
                        socialaccounts.Add(InstagramAccount);
                    }
                    dic_profilessnap.Add("instagram", socialaccounts);
                
                    socialaccounts = new List<object>();
                    Api.TumblrAccount.TumblrAccount ApiobjTumblrAccount = new Api.TumblrAccount.TumblrAccount();
                    List<TumblrAccount> lstTumblrAccount = (List<TumblrAccount>)(new JavaScriptSerializer().Deserialize(ApiobjTumblrAccount.GetAllTumblrAccountsByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<TumblrAccount>)));
                    foreach (var TumblrAccount in lstTumblrAccount)
                    {
                        socialaccounts.Add(TumblrAccount);
                    }
                    dic_profilessnap.Add("tumblr", socialaccounts);
             
                    socialaccounts = new List<object>();
                    Api.YoutubeAccount.YoutubeAccount ApiobjYoutubeAccount = new Api.YoutubeAccount.YoutubeAccount();
                    List<YoutubeAccount> lstYoutubeAccount = (List<YoutubeAccount>)(new JavaScriptSerializer().Deserialize(ApiobjYoutubeAccount.GetAllYoutubeAccountsByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<YoutubeAccount>)));
                    foreach (var YoutubeAccount in lstYoutubeAccount)
                    {
                        socialaccounts.Add(YoutubeAccount);
                    }
                    dic_profilessnap.Add("youtube", socialaccounts);
                

            
            return dic_profilessnap;
        }

        public static Dictionary<string, Dictionary<List<object>, List<object>>> GetGroupsMenuAccordingToGroup()
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Dictionary<string, Dictionary<List<object>, List<object>>> _ReturnDicValue = new Dictionary<string, Dictionary<List<object>, List<object>>>();
            Dictionary<List<object>, List<object>> dic_profilessnap = new Dictionary<List<object>, List<object>>();
            List<object> socialaccounts = null;
            List<object> accountsgroup = null;

            socialaccounts = new List<object>();
            accountsgroup = new List<object>();

            Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
            List<FacebookAccount> lstFacebookAccount = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.GetAllFacebookAccountsByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<FacebookAccount>)));
            foreach (var FacebookAccount in lstFacebookAccount)
            {

                List<FacebookGroup> lstFacebookGroup = GetGroupName(FacebookAccount.AccessToken.ToString());
                foreach (var FacebookGroup in lstFacebookGroup)
                {
                    accountsgroup.Add(FacebookGroup);
                }
                socialaccounts.Add(FacebookAccount);
            }
            dic_profilessnap.Add(socialaccounts, accountsgroup);
            _ReturnDicValue.Add("facebook", dic_profilessnap);

            dic_profilessnap = new Dictionary<List<object>, List<object>>();
            socialaccounts = new List<object>();
            accountsgroup = new List<object>();

            Api.LinkedinAccount.LinkedinAccount ApiobjLinkedinAccount = new Api.LinkedinAccount.LinkedinAccount();
            Api.Linkedin.Linkedin ApiobjLinkedin = new Api.Linkedin.Linkedin();

            List<LinkedInAccount> lstLinkedinAccount = (List<LinkedInAccount>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinAccount.GetAllLinkedinAccountsByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<LinkedInAccount>)));
            foreach (var LinkedInAccount in lstLinkedinAccount)
            {
                //var testJsonStr= ApiobjLinkedin.GetLinkedGroupsDetail(LinkedInAccount.LinkedinUserId.ToString(), LinkedInAccount.UserId.ToString());
                //List<LinkedInGroup> objLinkedInGroup = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LinkedInGroup>>(testJsonStr);


                List<LinkedInGroup.Group_Updates> objLinkedInGroup = (List<LinkedInGroup.Group_Updates>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedin.GetLinkedGroupsDetail(LinkedInAccount.LinkedinUserId.ToString(), LinkedInAccount.UserId.ToString()), typeof(List<LinkedInGroup.Group_Updates>)));
                //Newtonsoft.Json.Linq.JObject obj = JObject.Parse(testJsonStr);

                foreach (var LinkedInGroup in objLinkedInGroup)
                {
                   

                    accountsgroup.Add(LinkedInGroup);
                    
                }
                 socialaccounts.Add(LinkedInAccount);
            }
            dic_profilessnap.Add(socialaccounts, accountsgroup);
            _ReturnDicValue.Add("linkedin", dic_profilessnap);

            return _ReturnDicValue;
        }

        public static List<FacebookGroup> GetGroupName(string accesstoken)
        {
            List<FacebookGroup> lstGroupName = new List<FacebookGroup>();
            try
            {
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = accesstoken;
                dynamic groups = fb.Get("me/groups");

                foreach (var item in groups["data"])
                {
                    try
                    {
                        FacebookGroup objFacebookGroup = new FacebookGroup();
                        objFacebookGroup.Name = item["name"].ToString();
                        objFacebookGroup.GroupId = item["id"].ToString();
                        lstGroupName.Add(objFacebookGroup);
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

            return lstGroupName;
        }       

        public static Dictionary<string,List<object>> GetFbGroupDataAccordingGroupId(string groupid,string accesstoken)
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Dictionary<string, List<object>> dic_profilessnap = new Dictionary<string, List<object>>();
             List<object> groupdata = null;

            groupdata = new List<object>();
            Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();        

            List<FacebookGroupData> lstFacebookAccount = (List<FacebookGroupData>)(new JavaScriptSerializer().Deserialize(ApiobjFacebook.GetAllFbGroupdata(groupid.ToString(), accesstoken.ToString()), typeof(List<FacebookGroupData>)));
            foreach (var FacebookGroupData in lstFacebookAccount)
                {
                    groupdata.Add(FacebookGroupData);                    
                }
            dic_profilessnap.Add("facebook", groupdata);
            return dic_profilessnap;
        }

        public static Dictionary<string, List<object>> GetLinkedinGroupDataAccordingGroupId(string groupid, string linkedinId)
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Dictionary<string, List<object>> dic_profilessnap = new Dictionary<string, List<object>>();
             List<object> groupsdata = null;           
            
            groupsdata = new List<object>();


            Api.Linkedin.Linkedin ApiobjLinkedin = new Api.Linkedin.Linkedin();         

            List<LinkedInGroup.Group_Updates> objLinkedInGroup = (List<LinkedInGroup.Group_Updates>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedin.GetLinkedGroupsDataDetail(objUser.Id.ToString(), groupid.ToString(), linkedinId.ToString()), typeof(List<LinkedInGroup.Group_Updates>)));
             foreach (var LinkedInGroup in objLinkedInGroup)
                {                   
                    groupsdata.Add(LinkedInGroup);                   
                }
             dic_profilessnap.Add("linkedin", groupsdata);

             return dic_profilessnap;
        }
      
        public static string GetFacebookRedirectLink()
        {
            return "http://www.facebook.com/dialog/oauth/?scope=user_friends,read_friendlists,publish_actions,publish_stream,read_stream,read_insights,manage_pages,user_checkins,user_photos,read_mailbox,manage_notifications,read_page_mailboxes,email,user_videos,user_groups,offline_access,publish_actions,manage_pages&client_id=" + ConfigurationManager.AppSettings["ClientId"] + "&redirect_uri=" + ConfigurationManager.AppSettings["RedirectUrl"] + "&response_type=code";
        }

        public static string CommentOnLinkedinPost(string groupid, string GpPostid, string message, string LinkedinUserId)
        {
              User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Api.Linkedin.Linkedin ApiobjLinkedin = new Api.Linkedin.Linkedin();
            string status = ApiobjLinkedin.CommentOnLinkedInPost(groupid, GpPostid, message, LinkedinUserId, objUser.Id.ToString());

            return "success";
        }

        public static string LikeOnLinkedinPost(string GpPostid, string LinkedinUserId, string isLike)
        {
              User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Api.Linkedin.Linkedin ApiobjLinkedin = new Api.Linkedin.Linkedin();
            string status = ApiobjLinkedin.LikeOnLinkedinPost(GpPostid,LinkedinUserId, isLike, objUser.Id.ToString());

            return "success";
        }

        public static string FollowLinkedinPost(string GpPostid, string LinkedinUserId, string isFollowing)
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Api.Linkedin.Linkedin ApiobjLinkedin = new Api.Linkedin.Linkedin();
            string status = ApiobjLinkedin.FollowLinkedinPost(GpPostid, LinkedinUserId, isFollowing, objUser.Id.ToString());

            return "success";
        }

        public static string PostOnFBGroupFeeds(string gid, string ack, string msg)
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();
            string status = ApiobjFacebook.PostOnFBGroupFeeds(gid, ack, msg, objUser.Id.ToString());

            return status;
        }

        public static string PostLinkedInGroupFeeds(string gid, string linkedInUserId, string msg, string title)
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Api.Linkedin.Linkedin ApiobjLinkedin = new Api.Linkedin.Linkedin();
            string status = ApiobjLinkedin.PostLinkedInGroupFeeds(gid, linkedInUserId, msg, title, objUser.Id.ToString());

            return "success";
        }

        public static string PostOnSelectedGroups(string SelectedGroupId, string title, string msg,string intrval, string clienttime, string time, string date,string imagefile )
        {

            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            try
            {
                ScheduledMessage _ScheduledMessage = new ScheduledMessage();
                GroupScheduleMessage _GroupScheduleMessage = new GroupScheduleMessage();
                
                 
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            Api.GroupScheduleMessage.GroupScheduleMessage ApiObjGroupScheduleMessage= new Api.GroupScheduleMessage.GroupScheduleMessage();

                int intervaltime=Convert.ToInt32(intrval);

                HttpContext.Current.Session["scheduletime"] = null;
                var SelctGroupId = SelectedGroupId.ToString().Split(',');

                foreach (var item in SelctGroupId)
                {
                     string[] networkingwithid = item.Split('_');

                         if (networkingwithid[1] == "fb")
                                {
                                    try
                                    {
                                        string facebookgrouppost = string.Empty;
                                        string groupid = networkingwithid[2];
                                        string profileid = networkingwithid[0];
                                        if (intervaltime != 0)
                                        {
                                            if (HttpContext.Current.Session["scheduletime"] == null)
                                            {
                                                string servertime = CompareDateWithclient(clienttime, date + " " + time);
                                                _ScheduledMessage.ScheduleTime = Convert.ToDateTime(servertime);
                                                DateTime d1 = _ScheduledMessage.ScheduleTime;
                                                DateTime d2 = d1.AddMinutes(intervaltime);
                                                HttpContext.Current.Session["scheduletime"] = d2;
                                            }
                                            else
                                            {
                                                DateTime d1 = (DateTime)HttpContext.Current.Session["scheduletime"];
                                                _ScheduledMessage.ScheduleTime = d1;
                                                DateTime d2 = d1.AddMinutes(intervaltime);
                                                HttpContext.Current.Session["scheduletime"] = d2;
                                            }
                                        }
                                        _ScheduledMessage.CreateTime = DateTime.Now;
                                        _ScheduledMessage.ProfileType = "facebookgroup";
                                        _ScheduledMessage.ProfileId = profileid;
                                      //  _ScheduledMessage.Id = Guid.NewGuid();
                                        if (!string.IsNullOrEmpty(imagefile))
                                        {
                                            var path = System.Configuration.ConfigurationManager.AppSettings["MailSenderDomain"] + "Contents/img/upload";
                                            string filepath = path + "/" +imagefile;
                                            _ScheduledMessage.PicUrl = filepath;
                                        }
                                        else
                                        {
                                            _ScheduledMessage.PicUrl = "Null";
                                        }

                                        _ScheduledMessage.ClientTime = Convert.ToDateTime(clienttime);
                                        _ScheduledMessage.ShareMessage = msg;
                                        _ScheduledMessage.UserId = objUser.Id;
                                        _ScheduledMessage.Status = false;

                                        string retmsg = ApiobjScheduledMessage.AddGroupScheduleMessages(_ScheduledMessage.ScheduleTime.ToString(), _ScheduledMessage.CreateTime.ToString(), _ScheduledMessage.ProfileType, _ScheduledMessage.ProfileId, _ScheduledMessage.PicUrl, _ScheduledMessage.ClientTime.ToString(), _ScheduledMessage.ShareMessage, _ScheduledMessage.UserId.ToString(), _ScheduledMessage.Status.ToString());

                                      //  _GroupScheduleMessage.Id = Guid.NewGuid();
                                        _GroupScheduleMessage.ScheduleMessageId = _ScheduledMessage.Id;
                                        _GroupScheduleMessage.GroupId = groupid;

                                        string returnmsg = ApiObjGroupScheduleMessage.AddGroupScheduleMessage(_GroupScheduleMessage.ScheduleMessageId.ToString(), _GroupScheduleMessage.GroupId);



                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.StackTrace);
                                    }
                                }
                         else if (networkingwithid[1] == "lin")
                         {
                             try
                             {
                                 string groupid = networkingwithid[2];
                                 string profileid = networkingwithid[0];
                                 if (intervaltime != 0)
                                 {
                                     if (HttpContext.Current.Session["scheduletime"] == null)
                                     {
                                         string servertime = CompareDateWithclient(clienttime, date + " " + time);
                                         _ScheduledMessage.ScheduleTime = Convert.ToDateTime(servertime);
                                         DateTime d1 = _ScheduledMessage.ScheduleTime;
                                         DateTime d2 = d1.AddMinutes(intervaltime);
                                         HttpContext.Current.Session["scheduletime"] = d2;
                                     }
                                     else
                                     {
                                         DateTime d1 = (DateTime)HttpContext.Current.Session["scheduletime"];
                                         _ScheduledMessage.ScheduleTime = d1;
                                         DateTime d2 = d1.AddMinutes(intervaltime);
                                         HttpContext.Current.Session["scheduletime"] = d2;
                                     }
                                 }
                                 string message = title + "$%^_^%$" + msg;
                                 _ScheduledMessage.CreateTime = DateTime.Now;
                                 _ScheduledMessage.ProfileType = "linkedingroup";
                                 _ScheduledMessage.ProfileId = profileid;
                                 _ScheduledMessage.Id = Guid.NewGuid();
                                 if (!string.IsNullOrEmpty(imagefile))
                                 {
                                     var path = System.Configuration.ConfigurationManager.AppSettings["MailSenderDomain"] + "Contents/img/upload";
                                     string filepath = path + "/" + imagefile;
                                     _ScheduledMessage.PicUrl = filepath;

                                 }
                                 else
                                 {
                                     _ScheduledMessage.PicUrl = "Null";
                                 }
                                 _ScheduledMessage.ClientTime = Convert.ToDateTime(clienttime);
                                 _ScheduledMessage.ShareMessage = message; ;
                                 _ScheduledMessage.UserId = objUser.Id;
                                 _ScheduledMessage.Status = false;

                                 string retmsg = ApiobjScheduledMessage.AddGroupScheduleMessages(_ScheduledMessage.ScheduleTime.ToString(), _ScheduledMessage.CreateTime.ToString(), _ScheduledMessage.ProfileType, _ScheduledMessage.ProfileId, _ScheduledMessage.PicUrl, _ScheduledMessage.ClientTime.ToString(), _ScheduledMessage.ShareMessage, _ScheduledMessage.UserId.ToString(), _ScheduledMessage.Status.ToString());

                               
                                 _GroupScheduleMessage.ScheduleMessageId = _ScheduledMessage.Id;
                                 _GroupScheduleMessage.GroupId = groupid;

                                 string returnmsg = ApiObjGroupScheduleMessage.AddGroupScheduleMessage(_GroupScheduleMessage.ScheduleMessageId.ToString(), _GroupScheduleMessage.GroupId);


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
                Console.WriteLine(ex.StackTrace);
            }
            return "success";        
        }

        public static string CompareDateWithclient(string clientdate, string scheduletime)
        {
            DateTime client = Convert.ToDateTime(clientdate);
            string strTimeNow = String.Format("{0:s}", client).Replace('T', ' ');

            DateTime server = DateTime.Now;
            DateTime schedule = Convert.ToDateTime(scheduletime);
            if (DateTime.Compare(client, server) > 0)
            {

                double minutes = (server - client).TotalMinutes;
                schedule = schedule.AddMinutes(minutes);

            }
            else if (DateTime.Compare(client, server) == 0)
            {


            }
            else if (DateTime.Compare(client, server) < 0)
            {
                double minutes = (server - client).TotalMinutes;
                schedule = schedule.AddMinutes(-minutes);
            }
            return schedule.ToString();
        }

    }

}