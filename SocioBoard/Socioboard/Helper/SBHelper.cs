using Domain.Socioboard.Domain;
using Domain.Socioboard.Factory;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace Socioboard.Helper
{
    public static class SBHelper
    {
        static ILog logger = LogManager.GetLogger(typeof(SBUtils));
        public static async Task<Dictionary<Domain.Socioboard.Domain.GroupProfile, object>> GetGroupProfiles() 
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            string groupId = System.Web.HttpContext.Current.Session["group"].ToString();
            string accesstoken = string.Empty;
            try {
                accesstoken = System.Web.HttpContext.Current.Session["access_token"].ToString();
            }
            catch { }

            Groups objGroups = null;
            HttpResponseMessage response1 = await WebApiReq.GetReq("api/ApiGroups/GetGroupDetailsByGroupId?GroupId=" + groupId, "Bearer", accesstoken);
            if (response1.IsSuccessStatusCode)
            {
                objGroups = await response1.Content.ReadAsAsync<Domain.Socioboard.Domain.Groups>();
            }



            IEnumerable<GroupProfile> lstGroupProfiles = new List<GroupProfile>();
           
                HttpResponseMessage response = await WebApiReq.GetReq("api/ApiGroupProfiles/GetGroupProfiles?GroupId=" + groupId, "Bearer", accesstoken);
                if (response.IsSuccessStatusCode)
                {
                    lstGroupProfiles = await response.Content.ReadAsAsync<IEnumerable<Domain.Socioboard.Domain.GroupProfile>>();
                }
          
            Dictionary<Domain.Socioboard.Domain.GroupProfile, object> dictGroupProfiles = new Dictionary<Domain.Socioboard.Domain.GroupProfile, object>();

            foreach (var profile in lstGroupProfiles) 
            {
                ISocialSiteAccount objISocialSiteAccount = await GetSocialAccountFromGroupProfile(objGroups.UserId, profile);
                SocialSiteAccountFactory objSocialSiteAccountFactory = new SocialSiteAccountFactory(profile.ProfileType);
                dictGroupProfiles.Add(profile, objISocialSiteAccount);
            }

            return dictGroupProfiles;
        }


        private static async Task<ISocialSiteAccount> GetSocialAccountFromGroupProfile(Guid objUserid, Domain.Socioboard.Domain.GroupProfile objGroupProfile)
        {
            ISocialSiteAccount objSocialSiteAccount = null;
            string accesstoken = string.Empty;
             try {
                accesstoken = System.Web.HttpContext.Current.Session["access_token"].ToString();
            }
            catch { }

            if (objGroupProfile.ProfileType == "facebook" || objGroupProfile.ProfileType == "facebook_page")
            {
                //using (Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount())
                //{
                //    ApiobjFacebookAccount.Timeout = 300000;
                //    objSocialSiteAccount = (FacebookAccount)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.getFacebookAccountDetailsById(objUserid.ToString(), objGroupProfile.ProfileId.ToString()), typeof(FacebookAccount)));
                //}
                FacebookAccount fbaccount = new FacebookAccount();
                HttpResponseMessage fbresponse = await WebApiReq.GetReq("api/ApiFacebookAccount/GetFacebookAcoount?ProfileId=" + objGroupProfile.ProfileId, "Bearer", accesstoken);
                if (fbresponse.IsSuccessStatusCode)
                {
                    fbaccount = await fbresponse.Content.ReadAsAsync<Domain.Socioboard.Domain.FacebookAccount>();
                    objSocialSiteAccount = fbaccount;
                }
            }
            else if (objGroupProfile.ProfileType == "twitter")
            {
                //using (Api.TwitterAccount.TwitterAccount ApiobjTwitterAccount = new Api.TwitterAccount.TwitterAccount())
                //{

                //    ApiobjTwitterAccount.Timeout = 300000;
                //    objSocialSiteAccount = (TwitterAccount)(new JavaScriptSerializer().Deserialize(ApiobjTwitterAccount.GetTwitterAccountDetailsById(objUserid.ToString(), objGroupProfile.ProfileId.ToString()), typeof(TwitterAccount)));

                //}
                TwitterAccount twitterAcc = new TwitterAccount();
                HttpResponseMessage twitterresponse = await WebApiReq.GetReq("api/ApiTwitterAccount/GetTwitterAccountDetailsById?ProfileId=" + objGroupProfile.ProfileId, "Bearer", accesstoken);
                if (twitterresponse.IsSuccessStatusCode)
                {
                    twitterAcc = await twitterresponse.Content.ReadAsAsync<Domain.Socioboard.Domain.TwitterAccount>();
                    objSocialSiteAccount = twitterAcc;
                }
            }
            else if (objGroupProfile.ProfileType == "linkedin")
            {
                //using (Api.LinkedinAccount.LinkedinAccount ApiobjLinkedinAccount = new Api.LinkedinAccount.LinkedinAccount())
                //{

                //    ApiobjLinkedinAccount.Timeout = 300000;
                //    objSocialSiteAccount = (LinkedInAccount)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinAccount.GetLinkedinAccountDetailsById(objUserid.ToString(), objGroupProfile.ProfileId.ToString()), typeof(LinkedInAccount)));

                //}
                LinkedInAccount LinkedinAcc = new LinkedInAccount();
                HttpResponseMessage Linkedinresponse = await WebApiReq.GetReq("api/ApiLinkedinAccount/GetLinkedinAccountDetailsById?ProfileId=" + objGroupProfile.ProfileId, "Bearer", accesstoken);
                if (Linkedinresponse.IsSuccessStatusCode)
                {
                    LinkedinAcc = await Linkedinresponse.Content.ReadAsAsync<Domain.Socioboard.Domain.LinkedInAccount>();
                    objSocialSiteAccount = LinkedinAcc;
                }

            }
            else if (objGroupProfile.ProfileType == "instagram")
            {
                //using (Api.InstagramAccount.InstagramAccount ApiobjInstagramAccount = new Api.InstagramAccount.InstagramAccount())
                //{
                //    objSocialSiteAccount = (InstagramAccount)(new JavaScriptSerializer().Deserialize(ApiobjInstagramAccount.UserInformation(objUserid.ToString(), objGroupProfile.ProfileId.ToString()), typeof(InstagramAccount)));

                //}
                InstagramAccount instAcc = new InstagramAccount();
                HttpResponseMessage response = await WebApiReq.GetReq("api/ApiInstagramAccount/GetInstagramAccount?ProfileId=" + objGroupProfile.ProfileId, "Bearer", accesstoken);
                if (response.IsSuccessStatusCode)
                {
                    instAcc = await response.Content.ReadAsAsync<Domain.Socioboard.Domain.InstagramAccount>();
                    objSocialSiteAccount = instAcc;
                }
            }
            else if (objGroupProfile.ProfileType == "youtube")
            {
                //using (Api.YoutubeAccount.YoutubeAccount ApiobjYoutubeAccount = new Api.YoutubeAccount.YoutubeAccount())
                //{

                //    ApiobjYoutubeAccount.Timeout = 300000;
                //    objSocialSiteAccount = (YoutubeAccount)(new JavaScriptSerializer().Deserialize(ApiobjYoutubeAccount.GetYoutubeAccountDetailsById(objUserid.ToString(), objGroupProfile.ProfileId.ToString()), typeof(YoutubeAccount)));

                //}

                YoutubeAccount ytAcc = new YoutubeAccount();
                HttpResponseMessage response = await WebApiReq.GetReq("api/ApiYoutubeAccount/GetYoutubeAccount?ProfileId=" + objGroupProfile.ProfileId, "Bearer", accesstoken);
                if (response.IsSuccessStatusCode)
                {
                    ytAcc = await response.Content.ReadAsAsync<Domain.Socioboard.Domain.YoutubeAccount>();
                    objSocialSiteAccount = ytAcc;
                }
            }
            else if (objGroupProfile.ProfileType == "tumblr")
            {
                //using (Api.TumblrAccount.TumblrAccount ApiobjTumblrAccount = new Api.TumblrAccount.TumblrAccount())
                //{

                //    ApiobjTumblrAccount.Timeout = 300000;
                //    objSocialSiteAccount = (TumblrAccount)(new JavaScriptSerializer().Deserialize(ApiobjTumblrAccount.GetTumblrAccountDetailsById(objUserid.ToString(), objGroupProfile.ProfileId.ToString()), typeof(TumblrAccount)));

                //}
                TumblrAccount ytAcc = new TumblrAccount();
                HttpResponseMessage response = await WebApiReq.GetReq("api/ApiTumblrAccount/GetTumblrAccountDetailsById?ProfileId=" + objGroupProfile.ProfileId, "Bearer", accesstoken);
                if (response.IsSuccessStatusCode)
                {
                    ytAcc = await response.Content.ReadAsAsync<Domain.Socioboard.Domain.TumblrAccount>();
                    objSocialSiteAccount = ytAcc;
                }

            }
            else if (objGroupProfile.ProfileType == "linkedincompanypage")
            {
                //using (Api.LinkedinCompanyPage.LinkedinCompanyPage objLinkedinCompanyPage = new Api.LinkedinCompanyPage.LinkedinCompanyPage())
                //{

                //    objLinkedinCompanyPage.Timeout = 300000;
                //    objSocialSiteAccount = (LinkedinCompanyPage)(new JavaScriptSerializer().Deserialize(objLinkedinCompanyPage.GetLinkedinCompanyPageDetailsByUserIdAndPageId(objUserid.ToString(), objGroupProfile.ProfileId.ToString()), typeof(LinkedinCompanyPage)));
                //}

                LinkedinCompanyPage licompanypage = new LinkedinCompanyPage();
                HttpResponseMessage response = await WebApiReq.GetReq("api/ApiLinkedinCompanyPage/GetLinkedinCompanyPageDetailsByUserIdAndPageId?ProfileId=" + objGroupProfile.ProfileId, "Bearer", accesstoken);
                if (response.IsSuccessStatusCode)
                {
                    licompanypage = await response.Content.ReadAsAsync<Domain.Socioboard.Domain.LinkedinCompanyPage>();
                    objSocialSiteAccount = licompanypage;
                }


            }
            else if (objGroupProfile.ProfileType == "gplus")
            {
                //using (Api.GooglePlusAccount.GooglePlusAccount ApiobjGooglePlusAccount = new Api.GooglePlusAccount.GooglePlusAccount())
                //{

                //    ApiobjGooglePlusAccount.Timeout = 300000;
                //    objSocialSiteAccount = (GooglePlusAccount)(new JavaScriptSerializer().Deserialize(ApiobjGooglePlusAccount.GetGooglePlusAccountDetailsById(objUserid.ToString(), objGroupProfile.ProfileId), typeof(GooglePlusAccount)));

                //}
                GooglePlusAccount googlePlusAccount = new GooglePlusAccount();
                HttpResponseMessage response = await WebApiReq.GetReq("api/ApiGooglePlusAccount/GetGooglePlusAccount?ProfileId=" + objGroupProfile.ProfileId, "Bearer", accesstoken);
                if (response.IsSuccessStatusCode)
                {
                    googlePlusAccount = await response.Content.ReadAsAsync<Domain.Socioboard.Domain.GooglePlusAccount>();
                    objSocialSiteAccount = googlePlusAccount;
                }
            }
            else if (objGroupProfile.ProfileType == "googleanalytics")
            {
                //using (Api.GoogleAnalyticsAccount.GoogleAnalyticsAccount ApiobjGoogleAnalyticsAccount = new Api.GoogleAnalyticsAccount.GoogleAnalyticsAccount())
                //{

                //    ApiobjGoogleAnalyticsAccount.Timeout = 300000;
                //    objSocialSiteAccount = (GoogleAnalyticsAccount)(new JavaScriptSerializer().Deserialize(ApiobjGoogleAnalyticsAccount.GetGooglePlusAccountDetailsById(objUserid.ToString(), objGroupProfile.ProfileId), typeof(GoogleAnalyticsAccount)));

                //}
                GoogleAnalyticsAccount googlePlusAccount = new GoogleAnalyticsAccount();
                HttpResponseMessage response = await WebApiReq.GetReq("api/ApiGoogleAnalyticsAccount/GetGooglePlusAccountDetailsById?ProfileId=" + objGroupProfile.ProfileId + "&UserId=" + objUserid.ToString(), "Bearer", accesstoken);
                if (response.IsSuccessStatusCode)
                {
                    googlePlusAccount = await response.Content.ReadAsAsync<Domain.Socioboard.Domain.GoogleAnalyticsAccount>();
                    objSocialSiteAccount = googlePlusAccount;
                }
            }


            return objSocialSiteAccount;
        }



        public static async Task<Dictionary<Domain.Socioboard.Domain.GroupProfile, object>> GetGroupProfilesByGroupId(string GroupId)
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            string groupId = GroupId;
            string accesstoken = string.Empty;
            try
            {
                accesstoken = System.Web.HttpContext.Current.Session["access_token"].ToString();
            }
            catch { }

            Groups objGroups = null;
            HttpResponseMessage response1 = await WebApiReq.GetReq("api/ApiGroups/GetGroupDetailsByGroupId?GroupId=" + groupId, "Bearer", accesstoken);
            if (response1.IsSuccessStatusCode)
            {
                objGroups = await response1.Content.ReadAsAsync<Domain.Socioboard.Domain.Groups>();
            }



            IEnumerable<GroupProfile> lstGroupProfiles = new List<GroupProfile>();

            HttpResponseMessage response = await WebApiReq.GetReq("api/ApiGroupProfiles/GetGroupProfiles?GroupId=" + groupId, "Bearer", accesstoken);
            if (response.IsSuccessStatusCode)
            {
                lstGroupProfiles = await response.Content.ReadAsAsync<IEnumerable<Domain.Socioboard.Domain.GroupProfile>>();
            }

            Dictionary<Domain.Socioboard.Domain.GroupProfile, object> dictGroupProfiles = new Dictionary<Domain.Socioboard.Domain.GroupProfile, object>();

            foreach (var profile in lstGroupProfiles)
            {
                ISocialSiteAccount objISocialSiteAccount = await GetSocialAccountFromGroupProfile(objGroups.UserId, profile);
                SocialSiteAccountFactory objSocialSiteAccountFactory = new SocialSiteAccountFactory(profile.ProfileType);
                dictGroupProfiles.Add(profile, objISocialSiteAccount);
            }

            return dictGroupProfiles;
        }



        public static Dictionary<Domain.Socioboard.Domain.GroupProfile, Dictionary<object, List<object>>> GetUserProfilesSnapsAccordingToGroup(List<Domain.Socioboard.Domain.GroupProfile> groupProfile, User objUser, int CountProfileSnapshot = 0)
        {

           // User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Dictionary<Domain.Socioboard.Domain.GroupProfile, Dictionary<object, List<object>>> dic_profilessnap = new Dictionary<Domain.Socioboard.Domain.GroupProfile, Dictionary<object, List<object>>>();
            var dicprofilefeeds = new Dictionary<object, List<object>>();
            List<GroupProfile> lstprofile = groupProfile.Where(t => t.ProfileType != "linkedin").ToList();
            int tempCount = 0;
            foreach (Domain.Socioboard.Domain.GroupProfile item in lstprofile)
            {
                tempCount++;
                if (tempCount <= CountProfileSnapshot)
                {
                    continue;
                }

                //to load only 3 profiles on home page load to speed up page loading
                if (dic_profilessnap.Count >= 3)
                {
                    break;
                }

                List<object> feeds = null;
                if (item.ProfileType == "facebook" || item.ProfileType == "facebook_page")
                {
                    try
                    {
                        feeds = new List<object>();
                        Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
                        ApiobjFacebookAccount.Timeout = 300000;
                        FacebookAccount objFacebookAccount = (FacebookAccount)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.getFacebookAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(FacebookAccount)));
                        Api.FacebookFeed.FacebookFeed ApiobjFacebookFeed = new Api.FacebookFeed.FacebookFeed();
                        ApiobjFacebookFeed.Timeout = 300000;
                        //List<FacebookFeed> lstFacebookFeed = (List<FacebookFeed>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookFeed.getAllFacebookFeedsByUserIdAndProfileId(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(List<FacebookFeed>)));
                        List<MongoFacebookFeed> lstFacebookFeed = (List<MongoFacebookFeed>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookFeed.getAllFacebookFeedsByUserIdAndProfileIdUsingLimit(objUser.Id.ToString(), item.ProfileId.ToString(), "0", "10"), typeof(List<MongoFacebookFeed>)));

                        foreach (var facebookfeed in lstFacebookFeed)
                        {
                            feeds.Add(facebookfeed);
                        }
                        dicprofilefeeds.Add(objFacebookAccount, feeds);
                        dic_profilessnap.Add(item, dicprofilefeeds);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                if (item.ProfileType == "twitter")
                {
                    try
                    {
                        feeds = new List<object>();
                        Api.TwitterAccount.TwitterAccount ApiobjTwitterAccount = new Api.TwitterAccount.TwitterAccount();
                        TwitterAccount objTwitterAccount = (TwitterAccount)(new JavaScriptSerializer().Deserialize(ApiobjTwitterAccount.GetTwitterAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(TwitterAccount)));
                        Api.TwitterFeed.TwitterFeed ApiobjTwitterFeed = new Api.TwitterFeed.TwitterFeed();
                        ApiobjTwitterFeed.Timeout = 300000;
                        //List<TwitterFeed> lstTwitterFeed = (List<TwitterFeed>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterFeed.GetAllTwitterFeedsByUserIdAndProfileId(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(List<TwitterFeed>)));
                        List<Domain.Socioboard.MongoDomain.TwitterFeed> lstTwitterFeed = (List<Domain.Socioboard.MongoDomain.TwitterFeed>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterFeed.getAllFeedsByUserIdAndProfileIdUsingLimit(objUser.Id.ToString(), item.ProfileId.ToString(), "0", "10"), typeof(List<Domain.Socioboard.MongoDomain.TwitterFeed>)));
                        foreach (var twitterfeed in lstTwitterFeed)
                        {
                            feeds.Add(twitterfeed);
                        }
                        dicprofilefeeds.Add(objTwitterAccount, feeds);
                        dic_profilessnap.Add(item, dicprofilefeeds);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                if (item.ProfileType == "linkedin")
                {
                    try
                    {
                        feeds = new List<object>();
                        Api.LinkedinAccount.LinkedinAccount ApiobjLinkedinAccount = new Api.LinkedinAccount.LinkedinAccount();
                        ApiobjLinkedinAccount.Timeout = 300000;
                        LinkedInAccount objLinkedInAccount = (LinkedInAccount)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinAccount.GetLinkedinAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(LinkedInAccount)));
                        Api.LinkedInFeed.LinkedInFeed ApiobjLinkedInFeed = new Api.LinkedInFeed.LinkedInFeed();
                        ApiobjLinkedInFeed.Timeout = 300000;
                        //List<LinkedInFeed> lstLinkedInFeed = (List<LinkedInFeed>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedInFeed.GetLinkedInFeeds(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(List<LinkedInFeed>)));
                        List<LinkedInFeed> lstLinkedInFeed = (List<LinkedInFeed>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedInFeed.GetLinkedInFeedsByUserIdAndProfileIdUsingLimit(objUser.Id.ToString(), item.ProfileId.ToString(), "0", "10"), typeof(List<LinkedInFeed>)));

                        foreach (var LinkedInFeed in lstLinkedInFeed)
                        {
                            feeds.Add(LinkedInFeed);
                        }
                        dicprofilefeeds.Add(objLinkedInAccount, feeds);
                        dic_profilessnap.Add(item, dicprofilefeeds);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if (item.ProfileType == "instagram")
                {
                    try
                    {
                        feeds = new List<object>();
                        Api.InstagramAccount.InstagramAccount ApiobjInstagramAccount = new Api.InstagramAccount.InstagramAccount();
                        ApiobjInstagramAccount.Timeout = 300000;
                        InstagramAccount objInstagramAccount = (InstagramAccount)(new JavaScriptSerializer().Deserialize(ApiobjInstagramAccount.UserInformation(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(InstagramAccount)));
                        dicprofilefeeds.Add(objInstagramAccount, feeds);
                        dic_profilessnap.Add(item, dicprofilefeeds);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                if (item.ProfileType == "tumblr")
                {
                    try
                    {
                        feeds = new List<object>();
                        Api.TumblrAccount.TumblrAccount ApiobjTumblrAccount = new Api.TumblrAccount.TumblrAccount();
                        ApiobjTumblrAccount.Timeout = 300000;
                        TumblrAccount objTumblrAccount = (TumblrAccount)(new JavaScriptSerializer().Deserialize(ApiobjTumblrAccount.GetTumblrAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(TumblrAccount)));
                        dicprofilefeeds.Add(objTumblrAccount, feeds);
                        dic_profilessnap.Add(item, dicprofilefeeds);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }


                if (item.ProfileType == "youtube")
                {
                    try
                    {
                        feeds = new List<object>();
                        Api.YoutubeAccount.YoutubeAccount ApiobjYoutubeAccount = new Api.YoutubeAccount.YoutubeAccount();
                        ApiobjYoutubeAccount.Timeout = 300000;
                        YoutubeAccount objYoutubeAccount = (YoutubeAccount)(new JavaScriptSerializer().Deserialize(ApiobjYoutubeAccount.GetYoutubeAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(YoutubeAccount)));
                        Api.YoutubeChannel.YoutubeChannel ApiobjYoutubeChannel = new Api.YoutubeChannel.YoutubeChannel();
                        ApiobjYoutubeChannel.Timeout = 300000;
                        List<YoutubeChannel> lstYoutubeChannel = (List<YoutubeChannel>)(new JavaScriptSerializer().Deserialize(ApiobjYoutubeChannel.GetAllYoutubeChannelByUserIdAndProfileId(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(List<YoutubeChannel>)));
                        //List<YoutubeChannel> lstYoutubeChannel = new List<YoutubeChannel>();
                        //lstYoutubeChannel.Add(objYoutubeChannel);
                        foreach (var youtubechannel in lstYoutubeChannel)
                        {
                            feeds.Add(youtubechannel);
                        }
                        dicprofilefeeds.Add(objYoutubeAccount, feeds);
                        dic_profilessnap.Add(item, dicprofilefeeds);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                if (item.ProfileType == "linkedincompanypage")
                {
                    try
                    {
                        feeds = new List<object>();
                        Api.LinkedinCompanyPage.LinkedinCompanyPage ApiobjLinkedinCompanyPage = new Api.LinkedinCompanyPage.LinkedinCompanyPage();
                        ApiobjLinkedinCompanyPage.Timeout = 300000;
                        LinkedinCompanyPage objLinkedinCompanypage = (LinkedinCompanyPage)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinCompanyPage.GetLinkedinCompanyPageDetailsByUserIdAndPageId(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(LinkedinCompanyPage)));
                        Api.LinkedinCompanyPage.LinkedinCompanyPage ApiobjLinkedinCompanyPagePost = new Api.LinkedinCompanyPage.LinkedinCompanyPage();
                        ApiobjLinkedinCompanyPage.Timeout = 300000;
                        List<LinkedinCompanyPagePosts> lstlipagepost = (List<LinkedinCompanyPagePosts>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinCompanyPagePost.GetAllLinkedinCompanyPagePostsByUserIdAndProfileId(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(List<LinkedinCompanyPagePosts>)));
                        foreach (var lipagepost in lstlipagepost)
                        {
                            feeds.Add(lipagepost);
                        }
                        dicprofilefeeds.Add(objLinkedinCompanypage, feeds);
                        dic_profilessnap.Add(item, dicprofilefeeds);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if (item.ProfileType == "gplus")
                {
                    try
                    {
                        feeds = new List<object>();
                        Api.GooglePlusAccount.GooglePlusAccount ApiobjGooglePlusAccount = new Api.GooglePlusAccount.GooglePlusAccount();
                        ApiobjGooglePlusAccount.Timeout = 300000;
                        Domain.Socioboard.Domain.GooglePlusAccount _GooglePlusAccount = (GooglePlusAccount)new JavaScriptSerializer().Deserialize(ApiobjGooglePlusAccount.GetGooglePlusAccountDetailsById(objUser.Id.ToString(), item.ProfileId), typeof(GooglePlusAccount));
                        dicprofilefeeds.Add(_GooglePlusAccount, feeds);
                        dic_profilessnap.Add(item, dicprofilefeeds);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if (item.ProfileType == "googleanalytics")
                {
                    try
                    {
                        feeds = new List<object>();
                        Api.GoogleAnalyticsAccount.GoogleAnalyticsAccount ApiobjGoogleAnalyticsAccount = new Api.GoogleAnalyticsAccount.GoogleAnalyticsAccount();
                        ApiobjGoogleAnalyticsAccount.Timeout = 300000;
                        Domain.Socioboard.Domain.GoogleAnalyticsAccount _GoogleAnalyticsAccount = (GoogleAnalyticsAccount)new JavaScriptSerializer().Deserialize(ApiobjGoogleAnalyticsAccount.GetGooglePlusAccountDetailsById(objUser.Id.ToString(), item.ProfileId), typeof(GoogleAnalyticsAccount));
                        dicprofilefeeds.Add(_GoogleAnalyticsAccount, feeds);
                        dic_profilessnap.Add(item, dicprofilefeeds);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }

            }

            return dic_profilessnap;
        }




        public static async Task<Dictionary<string, List<object>>> GetFeedsMenuAccordingToGroup()
        {
            Dictionary<string, List<object>> dic_profilessnap = new Dictionary<string, List<object>>();
            try
            {
                User objUser = (User)System.Web.HttpContext.Current.Session["User"];
                //Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
                //ApiobjGroups.Timeout = 300000;
                //Groups objGroups = (Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(System.Web.HttpContext.Current.Session["group"].ToString()), typeof(Groups)));
                List<object> socialaccounts = null;
                socialaccounts = new List<object>();
                //Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
                //ApiobjFacebookAccount.Timeout = 300000;
                //List<FacebookAccount> lstFacebookAccount = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.GetAllFacebookAccountsByUserIdAndGroupId(objGroups.UserId.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<FacebookAccount>)));

                string groupId = System.Web.HttpContext.Current.Session["group"].ToString();
                string accesstoken = string.Empty;

                try
                {
                    accesstoken = System.Web.HttpContext.Current.Session["access_token"].ToString();
                }
                catch { }
                IEnumerable<Domain.Socioboard.Domain.FacebookAccount> lstFacebookAccount = new List<Domain.Socioboard.Domain.FacebookAccount>();


                HttpResponseMessage response1 = await WebApiReq.GetReq("api/ApiGroupProfiles/GetGroupFacebookProfiles?GroupId=" + groupId+"&UserId="+objUser.Id, "Bearer", accesstoken);
                if (response1.IsSuccessStatusCode)
                {
                    lstFacebookAccount = await response1.Content.ReadAsAsync<IEnumerable<Domain.Socioboard.Domain.FacebookAccount>>();
                }


                foreach (var FacebookAccount in lstFacebookAccount)
                {
                    socialaccounts.Add(FacebookAccount);
                }
                dic_profilessnap.Add("facebook", socialaccounts);





                socialaccounts = new List<object>();
                //Api.TwitterAccount.TwitterAccount ApiobjTwitterAccount = new Api.TwitterAccount.TwitterAccount();
                //ApiobjTwitterAccount.Timeout = 300000;
                //List<TwitterAccount> lstTwitterAccount = (List<TwitterAccount>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterAccount.GetAllTwitterAccountsByUserIdAndGroupId(objGroups.UserId.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<TwitterAccount>)));
                IEnumerable<TwitterAccount> lstTwitterAccount = new List<TwitterAccount>();
                HttpResponseMessage Twitterresponse1 = await WebApiReq.GetReq("api/ApiGroupProfiles/GetGroupTwitterProfiles?GroupId=" + groupId + "&UserId=" + objUser.Id, "Bearer", accesstoken);
                if (Twitterresponse1.IsSuccessStatusCode)
                {
                    lstTwitterAccount = await Twitterresponse1.Content.ReadAsAsync<IEnumerable<Domain.Socioboard.Domain.TwitterAccount>>();
                }
                
                foreach (var TwitterAccount in lstTwitterAccount)
                {
                    socialaccounts.Add(TwitterAccount);
                }
                dic_profilessnap.Add("twitter", socialaccounts);

                socialaccounts = new List<object>();
                //Api.LinkedinAccount.LinkedinAccount ApiobjLinkedinAccount = new Api.LinkedinAccount.LinkedinAccount();
                //ApiobjLinkedinAccount.Timeout = 300000;
                //List<LinkedInAccount> lstLinkedinAccount = (List<LinkedInAccount>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinAccount.GetAllLinkedinAccountsByUserIdAndGroupId(objGroups.UserId.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<LinkedInAccount>)));

                IEnumerable<LinkedInAccount> lstLinkedinAccount = new List<LinkedInAccount>();

                HttpResponseMessage Linkedinresponse1 = await WebApiReq.GetReq("api/ApiGroupProfiles/GetGroupLinkedinProfiles?GroupId=" + groupId + "&UserId=" + objUser.Id, "Bearer", accesstoken);
                if (Linkedinresponse1.IsSuccessStatusCode)
                {
                    lstLinkedinAccount = await Linkedinresponse1.Content.ReadAsAsync<IEnumerable<Domain.Socioboard.Domain.LinkedInAccount>>();
                }

                foreach (var LinkedInAccount in lstLinkedinAccount)
                {
                    socialaccounts.Add(LinkedInAccount);
                }
                dic_profilessnap.Add("linkedin", socialaccounts);


                socialaccounts = new List<object>();
                //Api.LinkedinCompanyPage.LinkedinCompanyPage ApiobjLinkedinCompanyPage = new Api.LinkedinCompanyPage.LinkedinCompanyPage();
                //ApiobjLinkedinCompanyPage.Timeout = 300000;
                //List<LinkedinCompanyPage> lstLinkedinCompanyPage = (List<LinkedinCompanyPage>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinCompanyPage.GetAllLinkedinCompanyPageByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<LinkedinCompanyPage>)));

                IEnumerable<LinkedinCompanyPage> lstLinkedinCompanyPage = new List<LinkedinCompanyPage>();

                HttpResponseMessage LinkedinCompanyPageresponse1 = await WebApiReq.GetReq("api/ApiGroupProfiles/GetGroupLinkedinComanyPageProfiles?GroupId=" + groupId + "&UserId=" + objUser.Id, "Bearer", accesstoken);
                if (LinkedinCompanyPageresponse1.IsSuccessStatusCode)
                {
                    lstLinkedinCompanyPage = await LinkedinCompanyPageresponse1.Content.ReadAsAsync<IEnumerable<Domain.Socioboard.Domain.LinkedinCompanyPage>>();
                }

                foreach (var LiCompanyPage in lstLinkedinCompanyPage)
                {
                    socialaccounts.Add(LiCompanyPage);
                }
                dic_profilessnap.Add("linkedincompanypage", socialaccounts);


                socialaccounts = new List<object>();
                //Api.InstagramAccount.InstagramAccount ApiobjInstagramAccount = new Api.InstagramAccount.InstagramAccount();
                //ApiobjInstagramAccount.Timeout = 300000;
                //List<InstagramAccount> lstInstagramAccount = (List<InstagramAccount>)(new JavaScriptSerializer().Deserialize(ApiobjInstagramAccount.GetAllInstagramAccountsByUserIdAndGroupId(objGroups.UserId.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<InstagramAccount>)));
                IEnumerable<InstagramAccount> lstInstagramAccount = new List<InstagramAccount>();

                HttpResponseMessage Instagramresponse = await WebApiReq.GetReq("api/ApiGroupProfiles/GetGroupInstagramProfiles?GroupId=" + groupId + "&UserId=" + objUser.Id, "Bearer", accesstoken);
                if (Instagramresponse.IsSuccessStatusCode)
                {
                    lstInstagramAccount = await Instagramresponse.Content.ReadAsAsync<IEnumerable<Domain.Socioboard.Domain.InstagramAccount>>();
                }

                foreach (var InstagramAccount in lstInstagramAccount)
                {
                    socialaccounts.Add(InstagramAccount);
                }
                dic_profilessnap.Add("instagram", socialaccounts);



                socialaccounts = new List<object>();
                //Api.TumblrAccount.TumblrAccount ApiobjTumblrAccount = new Api.TumblrAccount.TumblrAccount();
                //ApiobjTumblrAccount.Timeout = 300000;
                //List<TumblrAccount> lstTumblrAccount = (List<TumblrAccount>)(new JavaScriptSerializer().Deserialize(ApiobjTumblrAccount.GetAllTumblrAccountsByUserIdAndGroupId(objGroups.UserId.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<TumblrAccount>)));
                IEnumerable<TumblrAccount> lstTumblrAccount = new List<TumblrAccount>();

                HttpResponseMessage Tumblrresponse = await WebApiReq.GetReq("api/ApiGroupProfiles/GetGroupTumblrProfiles?GroupId=" + groupId + "&UserId=" + objUser.Id, "Bearer", accesstoken);
                if (Tumblrresponse.IsSuccessStatusCode)
                {
                    lstTumblrAccount = await Tumblrresponse.Content.ReadAsAsync<IEnumerable<Domain.Socioboard.Domain.TumblrAccount>>();
                }



                foreach (var TumblrAccount in lstTumblrAccount)
                {
                    socialaccounts.Add(TumblrAccount);
                }
                dic_profilessnap.Add("tumblr", socialaccounts);

                socialaccounts = new List<object>();
                //Api.YoutubeAccount.YoutubeAccount ApiobjYoutubeAccount = new Api.YoutubeAccount.YoutubeAccount();
                //ApiobjYoutubeAccount.Timeout = 300000;
                //List<YoutubeAccount> lstYoutubeAccount = (List<YoutubeAccount>)(new JavaScriptSerializer().Deserialize(ApiobjYoutubeAccount.GetAllYoutubeAccountsByUserIdAndGroupId(objGroups.UserId.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<YoutubeAccount>)));
                IEnumerable<YoutubeAccount> lstYoutubeAccount = new List<YoutubeAccount>();

                HttpResponseMessage Youtuberesponse = await WebApiReq.GetReq("api/ApiGroupProfiles/GetGroupYoutubeProfiles?GroupId=" + groupId + "&UserId=" + objUser.Id, "Bearer", accesstoken);
                if (Youtuberesponse.IsSuccessStatusCode)
                {
                    lstYoutubeAccount = await Youtuberesponse.Content.ReadAsAsync<IEnumerable<Domain.Socioboard.Domain.YoutubeAccount>>();
                }

                
                
                
                foreach (var YoutubeAccount in lstYoutubeAccount)
                {
                    socialaccounts.Add(YoutubeAccount);
                }
                dic_profilessnap.Add("youtube", socialaccounts);

                socialaccounts = new List<object>();
                //Api.GooglePlusAccount.GooglePlusAccount ApiobjGooglePlusAccount = new Api.GooglePlusAccount.GooglePlusAccount();
                //ApiobjGooglePlusAccount.Timeout = 300000;
                //List<GooglePlusAccount> lstGooglePlusAccount = (List<GooglePlusAccount>)(new JavaScriptSerializer().Deserialize(ApiobjGooglePlusAccount.GetAllBloggerAccountByUserIdAndGroupId(objGroups.UserId.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<GooglePlusAccount>)));

                IEnumerable<GooglePlusAccount> lstGooglePlusAccount = new List<GooglePlusAccount>();
                HttpResponseMessage GooglePlusresponse = await WebApiReq.GetReq("api/ApiGroupProfiles/GetGroupGPlusProfiles?GroupId=" + groupId + "&UserId=" + objUser.Id, "Bearer", accesstoken);
                if (GooglePlusresponse.IsSuccessStatusCode)
                {
                    lstGooglePlusAccount = await GooglePlusresponse.Content.ReadAsAsync<IEnumerable<Domain.Socioboard.Domain.GooglePlusAccount>>();
                }

                foreach (var _GooglePlusAccount in lstGooglePlusAccount)
                {
                    socialaccounts.Add(_GooglePlusAccount);
                }
                dic_profilessnap.Add("gplus", socialaccounts);
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                logger.Error(ex.Message);
            }
            return dic_profilessnap;
        }


        public static async Task<List<Domain.Socioboard.Domain.FacebookAccount>> GetUserTeamMemberFbProfiles()
        {
            string accesstoken = string.Empty;
            try
            {
                accesstoken = System.Web.HttpContext.Current.Session["access_token"].ToString();
            }
            catch { }
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            string groupid = System.Web.HttpContext.Current.Session["group"].ToString();
            List<Domain.Socioboard.Domain.FacebookAccount> lstFacebookAccount = new List<Domain.Socioboard.Domain.FacebookAccount>();
            HttpResponseMessage response1 = await WebApiReq.GetReq("api/ApiGroupProfiles/GetGroupFacebookPage?GroupId=" + groupid + "&UserId=" + objUser.Id, "Bearer", accesstoken);
            if (response1.IsSuccessStatusCode)
            {
                lstFacebookAccount = await response1.Content.ReadAsAsync<List<Domain.Socioboard.Domain.FacebookAccount>>();
            }
            return lstFacebookAccount;
        }

        public static async Task<List<Domain.Socioboard.Domain.InstagramAccount>> GetUserTeamMemberInstaProfiles()
        {
            string accesstoken = string.Empty;
            try
            {
                accesstoken = System.Web.HttpContext.Current.Session["access_token"].ToString();
            }
            catch { }
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            string groupid = System.Web.HttpContext.Current.Session["group"].ToString();
            List<Domain.Socioboard.Domain.InstagramAccount> lstInstagramAccount = new List<Domain.Socioboard.Domain.InstagramAccount>();
            HttpResponseMessage Instagramresponse = await WebApiReq.GetReq("api/ApiGroupProfiles/GetGroupInstagramProfiles?GroupId=" + groupid + "&UserId=" + objUser.Id, "Bearer", accesstoken);
            if (Instagramresponse.IsSuccessStatusCode)
            {
                lstInstagramAccount = await Instagramresponse.Content.ReadAsAsync<List<Domain.Socioboard.Domain.InstagramAccount>>();
            }
            return lstInstagramAccount;
        }

        public static async Task<List<Domain.Socioboard.Domain.TwitterAccount>> GetUserTeamMemberTwitterProfiles()
        {
            string accesstoken = string.Empty;
            try
            {
                accesstoken = System.Web.HttpContext.Current.Session["access_token"].ToString();
            }
            catch { }
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            string groupid = System.Web.HttpContext.Current.Session["group"].ToString();

            List<Domain.Socioboard.Domain.TwitterAccount> lstTwitterAccount = new List<TwitterAccount>();
            HttpResponseMessage Twitterresponse1 = await WebApiReq.GetReq("api/ApiGroupProfiles/GetGroupTwitterProfiles?GroupId=" + groupid + "&UserId=" + objUser.Id, "Bearer", accesstoken);
            if (Twitterresponse1.IsSuccessStatusCode)
            {
                lstTwitterAccount = await Twitterresponse1.Content.ReadAsAsync<List<Domain.Socioboard.Domain.TwitterAccount>>();
            }
            return lstTwitterAccount;
            
        }

        public static async Task<List<Domain.Socioboard.Domain.GoogleAnalyticsAccount>> GetUserTeamMemberGAProfiles()
        {
            string accesstoken = string.Empty;
            try
            {
                accesstoken = System.Web.HttpContext.Current.Session["access_token"].ToString();
            }
            catch { }
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            string groupid = System.Web.HttpContext.Current.Session["group"].ToString();
       
            List<Domain.Socioboard.Domain.GoogleAnalyticsAccount> ret_GoogleAnalyticsAccount = new List<Domain.Socioboard.Domain.GoogleAnalyticsAccount>();

            HttpResponseMessage Twitterresponse1 = await WebApiReq.GetReq("api/ApiGroupProfiles/GetGroupGoogleAnalytics?GroupId=" + groupid + "&UserId=" + objUser.Id, "Bearer", accesstoken);
            if (Twitterresponse1.IsSuccessStatusCode)
            {
                ret_GoogleAnalyticsAccount = await Twitterresponse1.Content.ReadAsAsync<List<Domain.Socioboard.Domain.GoogleAnalyticsAccount>>();
            }
       
            return ret_GoogleAnalyticsAccount;
        }

        public static async Task<string> LoadGroups(Guid Id) 
        {
          //  User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            string accesstoken = string.Empty;
            string group = string.Empty;
            try
            {
                accesstoken = System.Web.HttpContext.Current.Session["access_token"].ToString();
            }
            catch { }
            List<Domain.Socioboard.Domain.Groups> lstGroups = new List<Domain.Socioboard.Domain.Groups>();
            HttpResponseMessage response = await WebApiReq.GetReq("api/ApiGroups/GetGroupsOfUser?UserId=" + Id, "Bearer", accesstoken);
            if (response.IsSuccessStatusCode)
            {
                lstGroups = await response.Content.ReadAsAsync<List<Domain.Socioboard.Domain.Groups>>();
            }

            lstGroups = lstGroups.Where(t => t.GroupName == System.Configuration.ConfigurationManager.AppSettings["DefaultGroupName"].ToString()).ToList();
            if (lstGroups.Count() > 0)
            {
                group = lstGroups[0].Id.ToString();
            }
            return group;
        }
    }
}