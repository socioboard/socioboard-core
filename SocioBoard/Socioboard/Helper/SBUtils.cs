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
using System.Collections.ObjectModel;
using log4net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.IO;
using Socioboard.App_Start;


namespace Socioboard.Helper
{
    public static class SBUtils
    {
        static ILog logger = LogManager.GetLogger(typeof(SBUtils));
        public static Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, object> GetUserProfilesccordingToGroup()
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            ApiobjGroups.Timeout = 300000;
            //ApiobjGroups.GetGroupDetailsByGroupId
            Groups objGroups = (Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(System.Web.HttpContext.Current.Session["group"].ToString()), typeof(Groups)));
            Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, object> dict_TeamMember = new Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, object>();
            Api.Team.Team objApiTeam = new Api.Team.Team();
            objApiTeam.Timeout = 300000;
            JObject team = JObject.Parse(objApiTeam.GetTeamByGroupId(System.Web.HttpContext.Current.Session["group"].ToString()));
            Api.TeamMemberProfile.TeamMemberProfile objApiTeamMemberProfile = new Api.TeamMemberProfile.TeamMemberProfile();
            objApiTeamMemberProfile.Timeout = 300000;
            //JArray TeamMemberProfiles = JArray.Parse(objApiTeamMemberProfile.GetTeamMemberProfilesByTeamId(Convert.ToString(team["Id"])));

            List<Domain.Socioboard.Domain.TeamMemberProfile> lstTeamMemberProfiles = (List<Domain.Socioboard.Domain.TeamMemberProfile>)new JavaScriptSerializer().Deserialize(objApiTeamMemberProfile.GetTeamMemberProfilesByTeamId(Convert.ToString(team["Id"])), typeof(List<Domain.Socioboard.Domain.TeamMemberProfile>));

            //foreach (var item in TeamMemberProfiles)
            //{
            //    try
            //    {
            //        Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = GetTeamMemberProfileFromJObject(item);

            //        ISocialSiteAccount objISocialSiteAccount = GetSocialAccountFromTeamMemberProfile(objGroups.UserId, objTeamMemberProfile);
            //        SocialSiteAccountFactory objSocialSiteAccountFactory = new SocialSiteAccountFactory(objTeamMemberProfile.ProfileType);
            //        dict_TeamMember.Add(objTeamMemberProfile, objISocialSiteAccount);
            //    }
            //    catch (Exception ex)
            //    {

            //        Console.WriteLine(ex.Message);
            //        //return null;
            //    }
            //}

            foreach (Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile in lstTeamMemberProfiles)
            {
                try
                {
                    ISocialSiteAccount objISocialSiteAccount = GetSocialAccountFromTeamMemberProfile(objGroups.UserId, objTeamMemberProfile);
                    SocialSiteAccountFactory objSocialSiteAccountFactory = new SocialSiteAccountFactory(objTeamMemberProfile.ProfileType);
                    dict_TeamMember.Add(objTeamMemberProfile, objISocialSiteAccount);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
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

        [CustomException]
        private static ISocialSiteAccount GetSocialAccountFromTeamMemberProfile(Guid objUserid, Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile)
        {
            ISocialSiteAccount objSocialSiteAccount = null;

            if (objTeamMemberProfile.ProfileType == "facebook" || objTeamMemberProfile.ProfileType == "facebook_page")
            {
                using (Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount())
                {
                    ApiobjFacebookAccount.Timeout = 300000;
                    objSocialSiteAccount = (FacebookAccount)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.getFacebookAccountDetailsById(objUserid.ToString(), objTeamMemberProfile.ProfileId.ToString()), typeof(FacebookAccount)));
                }
            }
            else if (objTeamMemberProfile.ProfileType == "twitter")
            {
                using (Api.TwitterAccount.TwitterAccount ApiobjTwitterAccount = new Api.TwitterAccount.TwitterAccount())
                {

                    ApiobjTwitterAccount.Timeout = 300000;
                    objSocialSiteAccount = (TwitterAccount)(new JavaScriptSerializer().Deserialize(ApiobjTwitterAccount.GetTwitterAccountDetailsById(objUserid.ToString(), objTeamMemberProfile.ProfileId.ToString()), typeof(TwitterAccount)));

                }
            }
            else if (objTeamMemberProfile.ProfileType == "linkedin")
            {
                using (Api.LinkedinAccount.LinkedinAccount ApiobjLinkedinAccount = new Api.LinkedinAccount.LinkedinAccount())
                {

                    ApiobjLinkedinAccount.Timeout = 300000;
                    objSocialSiteAccount = (LinkedInAccount)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinAccount.GetLinkedinAccountDetailsById(objUserid.ToString(), objTeamMemberProfile.ProfileId.ToString()), typeof(LinkedInAccount)));

                }
            }
            else if (objTeamMemberProfile.ProfileType == "instagram")
            {
                using (Api.InstagramAccount.InstagramAccount ApiobjInstagramAccount = new Api.InstagramAccount.InstagramAccount())
                {

                    ApiobjInstagramAccount.Timeout = 300000;
                    objSocialSiteAccount = (InstagramAccount)(new JavaScriptSerializer().Deserialize(ApiobjInstagramAccount.UserInformation(objUserid.ToString(), objTeamMemberProfile.ProfileId.ToString()), typeof(InstagramAccount)));

                }
            }
            else if (objTeamMemberProfile.ProfileType == "youtube")
            {
                using (Api.YoutubeAccount.YoutubeAccount ApiobjYoutubeAccount = new Api.YoutubeAccount.YoutubeAccount())
                {

                    ApiobjYoutubeAccount.Timeout = 300000;
                    objSocialSiteAccount = (YoutubeAccount)(new JavaScriptSerializer().Deserialize(ApiobjYoutubeAccount.GetYoutubeAccountDetailsById(objUserid.ToString(), objTeamMemberProfile.ProfileId.ToString()), typeof(YoutubeAccount)));

                }
            }
            else if (objTeamMemberProfile.ProfileType == "tumblr")
            {
                using (Api.TumblrAccount.TumblrAccount ApiobjTumblrAccount = new Api.TumblrAccount.TumblrAccount())
                {

                    ApiobjTumblrAccount.Timeout = 300000;
                    objSocialSiteAccount = (TumblrAccount)(new JavaScriptSerializer().Deserialize(ApiobjTumblrAccount.GetTumblrAccountDetailsById(objUserid.ToString(), objTeamMemberProfile.ProfileId.ToString()), typeof(TumblrAccount)));

                }
            }
            else if (objTeamMemberProfile.ProfileType == "linkedincompanypage")
            {
                using (Api.LinkedinCompanyPage.LinkedinCompanyPage objLinkedinCompanyPage = new Api.LinkedinCompanyPage.LinkedinCompanyPage())
                {

                    objLinkedinCompanyPage.Timeout = 300000;
                    objSocialSiteAccount = (LinkedinCompanyPage)(new JavaScriptSerializer().Deserialize(objLinkedinCompanyPage.GetLinkedinCompanyPageDetailsByUserIdAndPageId(objUserid.ToString(), objTeamMemberProfile.ProfileId.ToString()), typeof(LinkedinCompanyPage)));

                }
            }
            else if (objTeamMemberProfile.ProfileType == "gplus")
            {
                using (Api.GooglePlusAccount.GooglePlusAccount ApiobjGooglePlusAccount = new Api.GooglePlusAccount.GooglePlusAccount())
                {

                    ApiobjGooglePlusAccount.Timeout = 300000;
                    objSocialSiteAccount = (GooglePlusAccount)(new JavaScriptSerializer().Deserialize(ApiobjGooglePlusAccount.GetGooglePlusAccountDetailsById(objUserid.ToString(), objTeamMemberProfile.ProfileId), typeof(GooglePlusAccount)));

                }
            }


            return objSocialSiteAccount;
        }

        public static List<Domain.Socioboard.Domain.TeamMemberProfile> GetUserTeamMemberProfiles()
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            string groupid = System.Web.HttpContext.Current.Session["group"].ToString();

            List<Domain.Socioboard.Domain.TeamMemberProfile> lstTeamMemberProfile = new List<Domain.Socioboard.Domain.TeamMemberProfile>();
            
            Api.Team.Team objApiTeam = new Api.Team.Team();
            objApiTeam.Timeout = 300000;
            JObject team = JObject.Parse(objApiTeam.GetTeamByGroupId(System.Web.HttpContext.Current.Session["group"].ToString()));

            Api.TeamMemberProfile.TeamMemberProfile objApiTeamMemberProfile = new Api.TeamMemberProfile.TeamMemberProfile();
            objApiTeamMemberProfile.Timeout = 300000;
            //JArray TeamMemberProfiles = JArray.Parse(objApiTeamMemberProfile.GetTeamMemberProfilesByTeamId(Convert.ToString(team["Id"])));
            lstTeamMemberProfile = (List<Domain.Socioboard.Domain.TeamMemberProfile>)new JavaScriptSerializer().Deserialize(objApiTeamMemberProfile.GetTeamMemberProfilesByTeamId(Convert.ToString(team["Id"])), typeof(List<Domain.Socioboard.Domain.TeamMemberProfile>));
            //foreach (var item in TeamMemberProfiles)
            //{
            //    try
            //    {
            //        Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = GetTeamMemberProfileFromJObject(item);
            //        lstTeamMemberProfile.Add(objTeamMemberProfile);
            //    }
            //    catch (Exception ex)
            //    {

            //        Console.WriteLine(ex.Message);
            //        //return null;
            //    }
            //}
            return lstTeamMemberProfile;


        }

        public static List<object> check(List<Domain.Socioboard.Domain.TeamMemberProfile> TeamMemberProfile)
        {

            return null;
        }

        //public static Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, Dictionary<object, List<object>>> GetUserProfilesSnapsAccordingToGroup(List<Domain.Socioboard.Domain.TeamMemberProfile> TeamMemberProfile)
        //{
        //    User objUser = (User)System.Web.HttpContext.Current.Session["User"];
        //    Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, Dictionary<object, List<object>>> dic_profilessnap = new Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, Dictionary<object, List<object>>>();
        //    var dicprofilefeeds = new Dictionary<object, List<object>>();
        //    foreach (Domain.Socioboard.Domain.TeamMemberProfile item in TeamMemberProfile)
        //    {
        //        List<object> feeds = null;
        //        if (item.ProfileType == "facebook" || item.ProfileType == "facebook_page")
        //        {
        //            try
        //            {
        //                feeds = new List<object>();
        //                Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
        //                FacebookAccount objFacebookAccount = (FacebookAccount)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.getFacebookAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(FacebookAccount)));
        //                Api.FacebookFeed.FacebookFeed ApiobjFacebookFeed = new Api.FacebookFeed.FacebookFeed();
        //                //List<FacebookFeed> lstFacebookFeed = (List<FacebookFeed>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookFeed.getAllFacebookFeedsByUserIdAndProfileId(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(List<FacebookFeed>)));
        //                List<FacebookFeed> lstFacebookFeed = (List<FacebookFeed>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookFeed.getAllFacebookFeedsByUserIdAndProfileIdUsingLimit(objUser.Id.ToString(), item.ProfileId.ToString(), "0", "10"), typeof(List<FacebookFeed>)));

        //                foreach (var facebookfeed in lstFacebookFeed)
        //                {
        //                    feeds.Add(facebookfeed);
        //                }
        //                dicprofilefeeds.Add(objFacebookAccount, feeds);
        //                dic_profilessnap.Add(item, dicprofilefeeds);
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.Message);
        //            }
        //        }

        //        if (item.ProfileType == "twitter")
        //        {
        //            try
        //            {
        //                feeds = new List<object>();
        //                Api.TwitterAccount.TwitterAccount ApiobjTwitterAccount = new Api.TwitterAccount.TwitterAccount();
        //                TwitterAccount objTwitterAccount = (TwitterAccount)(new JavaScriptSerializer().Deserialize(ApiobjTwitterAccount.GetTwitterAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(TwitterAccount)));
        //                Api.TwitterFeed.TwitterFeed ApiobjTwitterFeed = new Api.TwitterFeed.TwitterFeed();
        //                //List<TwitterFeed> lstTwitterFeed = (List<TwitterFeed>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterFeed.GetAllTwitterFeedsByUserIdAndProfileId(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(List<TwitterFeed>)));
        //                List<TwitterFeed> lstTwitterFeed = (List<TwitterFeed>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterFeed.getAllFeedsByUserIdAndProfileIdUsingLimit(objUser.Id.ToString(), item.ProfileId.ToString(),"0","10"), typeof(List<TwitterFeed>)));
        //                foreach (var twitterfeed in lstTwitterFeed)
        //                {
        //                    feeds.Add(twitterfeed);
        //                }
        //                dicprofilefeeds.Add(objTwitterAccount, feeds);
        //                dic_profilessnap.Add(item, dicprofilefeeds);
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.Message);
        //            }
        //        }

        //        if (item.ProfileType == "linkedin")
        //        {
        //            try
        //            {
        //                feeds = new List<object>();
        //                Api.LinkedinAccount.LinkedinAccount ApiobjLinkedinAccount = new Api.LinkedinAccount.LinkedinAccount();
        //                LinkedInAccount objLinkedInAccount = (LinkedInAccount)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinAccount.GetLinkedinAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(LinkedInAccount)));
        //                Api.LinkedInFeed.LinkedInFeed ApiobjLinkedInFeed = new Api.LinkedInFeed.LinkedInFeed();
        //                //List<LinkedInFeed> lstLinkedInFeed = (List<LinkedInFeed>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedInFeed.GetLinkedInFeeds(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(List<LinkedInFeed>)));
        //                List<LinkedInFeed> lstLinkedInFeed = (List<LinkedInFeed>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedInFeed.GetLinkedInFeedsByUserIdAndProfileIdUsingLimit(objUser.Id.ToString(), item.ProfileId.ToString(), "0", "10"), typeof(List<LinkedInFeed>)));

        //                foreach (var LinkedInFeed in lstLinkedInFeed)
        //                {
        //                    feeds.Add(LinkedInFeed);
        //                }
        //                dicprofilefeeds.Add(objLinkedInAccount, feeds);
        //                dic_profilessnap.Add(item, dicprofilefeeds);
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.Message);
        //            }
        //        }
        //        if (item.ProfileType == "instagram")
        //        {
        //            try
        //            {
        //                feeds = new List<object>();
        //                Api.InstagramAccount.InstagramAccount ApiobjInstagramAccount = new Api.InstagramAccount.InstagramAccount();
        //                InstagramAccount objInstagramAccount = (InstagramAccount)(new JavaScriptSerializer().Deserialize(ApiobjInstagramAccount.UserInformation(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(InstagramAccount)));
        //                dicprofilefeeds.Add(objInstagramAccount, feeds);
        //                dic_profilessnap.Add(item, dicprofilefeeds);
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.Message);
        //            }
        //        }

        //        if (item.ProfileType == "tumblr")
        //        {
        //            try
        //            {
        //                feeds = new List<object>();
        //                Api.TumblrAccount.TumblrAccount ApiobjTumblrAccount = new Api.TumblrAccount.TumblrAccount();
        //                TumblrAccount objTumblrAccount = (TumblrAccount)(new JavaScriptSerializer().Deserialize(ApiobjTumblrAccount.GetTumblrAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(TumblrAccount)));
        //                dicprofilefeeds.Add(objTumblrAccount, feeds);
        //                dic_profilessnap.Add(item, dicprofilefeeds);
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.Message);
        //            }
        //        }


        //        if (item.ProfileType == "youtube")
        //        {
        //            try
        //            {
        //                feeds = new List<object>();
        //                Api.YoutubeAccount.YoutubeAccount ApiobjYoutubeAccount = new Api.YoutubeAccount.YoutubeAccount();
        //                YoutubeAccount objYoutubeAccount = (YoutubeAccount)(new JavaScriptSerializer().Deserialize(ApiobjYoutubeAccount.GetYoutubeAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(YoutubeAccount)));
        //                Api.YoutubeChannel.YoutubeChannel ApiobjYoutubeChannel = new Api.YoutubeChannel.YoutubeChannel();
        //                YoutubeChannel objYoutubeChannel = (YoutubeChannel)(new JavaScriptSerializer().Deserialize(ApiobjYoutubeChannel.GetAllYoutubeChannelByUserIdAndProfileId(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(YoutubeChannel)));
        //                List<YoutubeChannel> lstYoutubeChannel = new List<YoutubeChannel>();
        //                lstYoutubeChannel.Add(objYoutubeChannel);
        //                foreach (var youtubechannel in lstYoutubeChannel)
        //                {
        //                    feeds.Add(youtubechannel);
        //                }
        //                dicprofilefeeds.Add(objYoutubeAccount, feeds);
        //                dic_profilessnap.Add(item, dicprofilefeeds);
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.Message);
        //            }
        //        }

        //        if (item.ProfileType == "linkedincompanypage")
        //        {
        //            try
        //            {
        //                feeds = new List<object>();
        //                Api.LinkedinCompanyPage.LinkedinCompanyPage ApiobjLinkedinCompanyPage = new Api.LinkedinCompanyPage.LinkedinCompanyPage();
        //                LinkedinCompanyPage objLinkedinCompanypage = (LinkedinCompanyPage)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinCompanyPage.GetLinkedinCompanyPageDetailsByUserIdAndPageId(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(LinkedinCompanyPage)));
        //                Api.LinkedinCompanyPage.LinkedinCompanyPage ApiobjLinkedinCompanyPagePost = new Api.LinkedinCompanyPage.LinkedinCompanyPage();
        //                List<LinkedinCompanyPagePosts> lstlipagepost = (List<LinkedinCompanyPagePosts>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinCompanyPagePost.GetAllLinkedinCompanyPagePostsByUserIdAndProfileId(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(List<LinkedinCompanyPagePosts>)));
        //                foreach (var lipagepost in lstlipagepost)
        //                {
        //                    feeds.Add(lipagepost);
        //                }
        //                dicprofilefeeds.Add(objLinkedinCompanypage, feeds);
        //                dic_profilessnap.Add(item, dicprofilefeeds);
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.Message);
        //            }
        //        }

        //    }
        //    return dic_profilessnap;
        //}
        public static Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, Dictionary<object, List<object>>> GetUserProfilesSnapsAccordingToGroup(List<Domain.Socioboard.Domain.TeamMemberProfile> TeamMemberProfile, int CountProfileSnapshot = 0)
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, Dictionary<object, List<object>>> dic_profilessnap = new Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, Dictionary<object, List<object>>>();
            var dicprofilefeeds = new Dictionary<object, List<object>>();

            int tempCount = 0;
            foreach (Domain.Socioboard.Domain.TeamMemberProfile item in TeamMemberProfile)
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
                        List<FacebookFeed> lstFacebookFeed = (List<FacebookFeed>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookFeed.getAllFacebookFeedsByUserIdAndProfileIdUsingLimit(objUser.Id.ToString(), item.ProfileId.ToString(), "0", "10"), typeof(List<FacebookFeed>)));

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
                        List<TwitterFeed> lstTwitterFeed = (List<TwitterFeed>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterFeed.getAllFeedsByUserIdAndProfileIdUsingLimit(objUser.Id.ToString(), item.ProfileId.ToString(), "0", "10"), typeof(List<TwitterFeed>)));
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
            Dictionary<string, List<object>> dic_profilessnap = new Dictionary<string, List<object>>();
            try
            {
                User objUser = (User)System.Web.HttpContext.Current.Session["User"];
                Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
                ApiobjGroups.Timeout = 300000;
                Groups objGroups = (Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(System.Web.HttpContext.Current.Session["group"].ToString()), typeof(Groups)));
                List<object> socialaccounts = null;
                socialaccounts = new List<object>();
                Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
                ApiobjFacebookAccount.Timeout = 300000; 
                List<FacebookAccount> lstFacebookAccount = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.GetAllFacebookAccountsByUserIdAndGroupId(objGroups.UserId.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<FacebookAccount>)));
                foreach (var FacebookAccount in lstFacebookAccount)
                {
                    socialaccounts.Add(FacebookAccount);
                }
                dic_profilessnap.Add("facebook", socialaccounts);

                socialaccounts = new List<object>();
                Api.TwitterAccount.TwitterAccount ApiobjTwitterAccount = new Api.TwitterAccount.TwitterAccount();
                ApiobjTwitterAccount.Timeout = 300000;
                List<TwitterAccount> lstTwitterAccount = (List<TwitterAccount>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterAccount.GetAllTwitterAccountsByUserIdAndGroupId(objGroups.UserId.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<TwitterAccount>)));
                foreach (var TwitterAccount in lstTwitterAccount)
                {
                    socialaccounts.Add(TwitterAccount);
                }
                dic_profilessnap.Add("twitter", socialaccounts);

                socialaccounts = new List<object>();
                Api.LinkedinAccount.LinkedinAccount ApiobjLinkedinAccount = new Api.LinkedinAccount.LinkedinAccount();
                ApiobjLinkedinAccount.Timeout = 300000;
                List<LinkedInAccount> lstLinkedinAccount = (List<LinkedInAccount>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinAccount.GetAllLinkedinAccountsByUserIdAndGroupId(objGroups.UserId.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<LinkedInAccount>)));
                foreach (var LinkedInAccount in lstLinkedinAccount)
                {
                    socialaccounts.Add(LinkedInAccount);
                }
                dic_profilessnap.Add("linkedin", socialaccounts);

                socialaccounts = new List<object>();
                Api.LinkedinCompanyPage.LinkedinCompanyPage ApiobjLinkedinCompanyPage = new Api.LinkedinCompanyPage.LinkedinCompanyPage();
                ApiobjLinkedinCompanyPage.Timeout = 300000;
                List<LinkedinCompanyPage> lstLinkedinCompanyPage = (List<LinkedinCompanyPage>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinCompanyPage.GetAllLinkedinCompanyPageByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<LinkedinCompanyPage>)));
                foreach (var LiCompanyPage in lstLinkedinCompanyPage)
                {
                    socialaccounts.Add(LiCompanyPage);
                }
                dic_profilessnap.Add("linkedincompanypage", socialaccounts);


                socialaccounts = new List<object>();
                Api.InstagramAccount.InstagramAccount ApiobjInstagramAccount = new Api.InstagramAccount.InstagramAccount();
                ApiobjInstagramAccount.Timeout = 300000;
                List<InstagramAccount> lstInstagramAccount = (List<InstagramAccount>)(new JavaScriptSerializer().Deserialize(ApiobjInstagramAccount.GetAllInstagramAccountsByUserIdAndGroupId(objGroups.UserId.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<InstagramAccount>)));
                foreach (var InstagramAccount in lstInstagramAccount)
                {
                    socialaccounts.Add(InstagramAccount);
                }
                dic_profilessnap.Add("instagram", socialaccounts);

                socialaccounts = new List<object>();
                Api.TumblrAccount.TumblrAccount ApiobjTumblrAccount = new Api.TumblrAccount.TumblrAccount();
                ApiobjTumblrAccount.Timeout = 300000;
                List<TumblrAccount> lstTumblrAccount = (List<TumblrAccount>)(new JavaScriptSerializer().Deserialize(ApiobjTumblrAccount.GetAllTumblrAccountsByUserIdAndGroupId(objGroups.UserId.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<TumblrAccount>)));
                foreach (var TumblrAccount in lstTumblrAccount)
                {
                    socialaccounts.Add(TumblrAccount);
                }
                dic_profilessnap.Add("tumblr", socialaccounts);

                socialaccounts = new List<object>();
                Api.YoutubeAccount.YoutubeAccount ApiobjYoutubeAccount = new Api.YoutubeAccount.YoutubeAccount();
                ApiobjYoutubeAccount.Timeout = 300000;
                List<YoutubeAccount> lstYoutubeAccount = (List<YoutubeAccount>)(new JavaScriptSerializer().Deserialize(ApiobjYoutubeAccount.GetAllYoutubeAccountsByUserIdAndGroupId(objGroups.UserId.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<YoutubeAccount>)));
                foreach (var YoutubeAccount in lstYoutubeAccount)
                {
                    socialaccounts.Add(YoutubeAccount);
                }
                dic_profilessnap.Add("youtube", socialaccounts);

                socialaccounts = new List<object>();
                Api.GooglePlusAccount.GooglePlusAccount ApiobjGooglePlusAccount = new Api.GooglePlusAccount.GooglePlusAccount();
                ApiobjGooglePlusAccount.Timeout = 300000;
                List<GooglePlusAccount> lstGooglePlusAccount = (List<GooglePlusAccount>)(new JavaScriptSerializer().Deserialize(ApiobjGooglePlusAccount.GetAllBloggerAccountByUserIdAndGroupId(objGroups.UserId.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<GooglePlusAccount>)));
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

        //public static Dictionary<string, Dictionary<List<object>, List<object>>> GetGroupsMenuAccordingToGroup()
        //{
        //    User objUser = (User)System.Web.HttpContext.Current.Session["User"];
        //    Dictionary<string, Dictionary<List<object>, List<object>>> _ReturnDicValue = new Dictionary<string, Dictionary<List<object>, List<object>>>();
        //    Dictionary<List<object>, List<object>> dic_profilessnap = new Dictionary<List<object>, List<object>>();
        //    List<object> socialaccounts = null;
        //    List<object> accountsgroup = null;
        //    socialaccounts = new List<object>();
        //    accountsgroup = new List<object>();


        //    Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
        //    List<FacebookAccount> lstFacebookAccount = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.GetAllFacebookAccountsByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<FacebookAccount>)));
        //    foreach (var FacebookAccount in lstFacebookAccount)
        //    {


        //        List<FacebookGroup> lstFacebookGroup = GetGroupName(FacebookAccount.AccessToken.ToString());

        //        if (lstFacebookGroup == null || lstFacebookGroup.Count==0)
        //        {
        //            accountsgroup.Add(null);

        //        }
        //        foreach (var FacebookGroup in lstFacebookGroup)
        //        {
        //            accountsgroup.Add(FacebookGroup);
        //        }
        //        socialaccounts.Add(FacebookAccount);
        //    }
        //    dic_profilessnap.Add(socialaccounts, accountsgroup);
        //    _ReturnDicValue.Add("facebook", dic_profilessnap);

        //    dic_profilessnap = new Dictionary<List<object>, List<object>>();
        //    socialaccounts = new List<object>();
        //    accountsgroup = new List<object>();

        //    Api.LinkedinAccount.LinkedinAccount ApiobjLinkedinAccount = new Api.LinkedinAccount.LinkedinAccount();
        //    Api.Linkedin.Linkedin ApiobjLinkedin = new Api.Linkedin.Linkedin();

        //    List<LinkedInAccount> lstLinkedinAccount = (List<LinkedInAccount>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinAccount.GetAllLinkedinAccountsByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<LinkedInAccount>)));
        //    foreach (var LinkedInAccount in lstLinkedinAccount)
        //    {
        //        //var testJsonStr= ApiobjLinkedin.GetLinkedGroupsDetail(LinkedInAccount.LinkedinUserId.ToString(), LinkedInAccount.UserId.ToString());
        //        //List<LinkedInGroup> objLinkedInGroup = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LinkedInGroup>>(testJsonStr);


        //        List<LinkedInGroup.Group_Updates> objLinkedInGroup = (List<LinkedInGroup.Group_Updates>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedin.GetLinkedGroupsDetail(LinkedInAccount.LinkedinUserId.ToString(), LinkedInAccount.UserId.ToString()), typeof(List<LinkedInGroup.Group_Updates>)));
        //        //Newtonsoft.Json.Linq.JObject obj = JObject.Parse(testJsonStr);

        //        foreach (var LinkedInGroup in objLinkedInGroup)
        //        {


        //            accountsgroup.Add(LinkedInGroup);

        //        }
        //        socialaccounts.Add(LinkedInAccount);
        //    }
        //    dic_profilessnap.Add(socialaccounts, accountsgroup);
        //    _ReturnDicValue.Add("linkedin", dic_profilessnap);

        //    return _ReturnDicValue;
        //}

        public static Dictionary<string, Dictionary<object, List<object>>> GetGroupsMenuAccordingToGroup()
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Dictionary<string, Dictionary<object, List<object>>> _ReturnDicValue = new Dictionary<string, Dictionary<object, List<object>>>();
            Dictionary<object, List<object>> dic_profilessnap = new Dictionary<object, List<object>>();
            object socialaccounts = null;

            List<object> accountsgroup = null;
            socialaccounts = new object();


            Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
            ApiobjFacebookAccount.Timeout = 300000;
            List<FacebookAccount> lstFacebookAccount = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.GetAllFacebookAccountsByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<FacebookAccount>)));
            foreach (var FacebookAccount in lstFacebookAccount)
            {
                accountsgroup = new List<object>();
                List<FacebookGroup> lstFacebookGroup = GetGroupName(FacebookAccount.AccessToken.ToString());

                foreach (var FacebookGroup in lstFacebookGroup)
                {
                    accountsgroup.Add(FacebookGroup);
                }
                socialaccounts = (object)FacebookAccount;
                dic_profilessnap.Add(socialaccounts, accountsgroup);
            }
            _ReturnDicValue.Add("facebook", dic_profilessnap);

            dic_profilessnap = new Dictionary<object, List<object>>();
            socialaccounts = new object();
            accountsgroup = new List<object>();

            Api.LinkedinAccount.LinkedinAccount ApiobjLinkedinAccount = new Api.LinkedinAccount.LinkedinAccount();
            ApiobjLinkedinAccount.Timeout = 300000;
            Api.Linkedin.Linkedin ApiobjLinkedin = new Api.Linkedin.Linkedin();

            List<LinkedInAccount> lstLinkedinAccount = (List<LinkedInAccount>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinAccount.GetAllLinkedinAccountsByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<LinkedInAccount>)));
            foreach (var LinkedInAccount in lstLinkedinAccount)
            {
                accountsgroup = new List<object>();
                List<LinkedInGroup.Group_Updates> objLinkedInGroup = (List<LinkedInGroup.Group_Updates>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedin.GetLinkedGroupsDetail(LinkedInAccount.LinkedinUserId.ToString(), LinkedInAccount.UserId.ToString()), typeof(List<LinkedInGroup.Group_Updates>)));

                foreach (var LinkedInGroup in objLinkedInGroup)
                {
                    accountsgroup.Add(LinkedInGroup);
                }
                socialaccounts = (object)LinkedInAccount;
                dic_profilessnap.Add(socialaccounts, accountsgroup);
            }

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
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                dynamic groups = fb.Get("v2.0/me/groups");

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

        public static Dictionary<string, List<object>> GetFbGroupDataAccordingGroupId(string groupid, string accesstoken, string profileid)
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Dictionary<string, List<object>> dic_profilessnap = new Dictionary<string, List<object>>();
            List<object> groupdata = null;

            groupdata = new List<object>();
            Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();
            ApiobjFacebook.Timeout = 300000;
            List<FacebookGroupData> lstFacebookAccount = (List<FacebookGroupData>)(new JavaScriptSerializer().Deserialize(ApiobjFacebook.GetAllFbGroupdata(groupid.ToString(), accesstoken.ToString(), profileid), typeof(List<FacebookGroupData>)));
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
            //return "http://www.facebook.com/v2.0/dialog/oauth/?scope=user_friends,read_friendlists,publish_actions,read_stream,read_insights,manage_pages,user_checkins,user_photos,read_mailbox,manage_notifications,read_page_mailboxes,email,user_videos,user_groups,offline_access,publish_actions,manage_pages&client_id=" + ConfigurationManager.AppSettings["ClientId"] + "&redirect_uri=" + ConfigurationManager.AppSettings["RedirectUrl"] + "&response_type=code";
            //return "http://www.facebook.com/v2.0/dialog/oauth/?scope=user_friends,user_status,read_friendlists,publish_actions,read_stream,read_insights,manage_pages,user_checkins,user_photos,read_mailbox,manage_notifications,read_page_mailboxes,email,user_videos,user_groups,offline_access,publish_actions,manage_pages&client_id=" + ConfigurationManager.AppSettings["ClientId"] + "&redirect_uri=" + ConfigurationManager.AppSettings["RedirectUrl"] + "&response_type=code";
            return ConfigurationManager.AppSettings["FacebookAuthUrl"] + "&client_id=" + ConfigurationManager.AppSettings["ClientId"] + "&redirect_uri=" + ConfigurationManager.AppSettings["RedirectUrl"] + "&response_type=code";
        }

        public static string CommentOnLinkedinPost(string groupid, string GpPostid, string message, string LinkedinUserId)
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Api.Linkedin.Linkedin ApiobjLinkedin = new Api.Linkedin.Linkedin();
            ApiobjLinkedin.Timeout = 300000;
            string status = ApiobjLinkedin.CommentOnLinkedInPost(groupid, GpPostid, message, LinkedinUserId, objUser.Id.ToString());

            return "success";
        }

        public static string LikeOnLinkedinPost(string GpPostid, string LinkedinUserId, string isLike)
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Api.Linkedin.Linkedin ApiobjLinkedin = new Api.Linkedin.Linkedin();
            ApiobjLinkedin.Timeout = 300000;
            string status = ApiobjLinkedin.LikeOnLinkedinPost(GpPostid, LinkedinUserId, isLike, objUser.Id.ToString());

            return "success";
        }

        public static string FollowLinkedinPost(string GpPostid, string LinkedinUserId, string isFollowing)
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Api.Linkedin.Linkedin ApiobjLinkedin = new Api.Linkedin.Linkedin();
            ApiobjLinkedin.Timeout = 300000;
            string status = ApiobjLinkedin.FollowLinkedinPost(GpPostid, LinkedinUserId, isFollowing, objUser.Id.ToString());

            return "success";
        }

        public static string PostOnFBGroupFeeds(string gid, string ack, string msg)
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();
            ApiobjFacebook.Timeout = 300000;
            string status = ApiobjFacebook.PostOnFBGroupFeeds(gid, ack, msg, objUser.Id.ToString());

            return status;
        }

        public static string PostLinkedInGroupFeeds(string gid, string linkedInUserId, string msg, string title)
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Api.Linkedin.Linkedin ApiobjLinkedin = new Api.Linkedin.Linkedin();
            ApiobjLinkedin.Timeout = 300000;
            string status = ApiobjLinkedin.PostLinkedInGroupFeeds(gid, linkedInUserId, msg, title, objUser.Id.ToString());

            return "success";
        }

        public static string PostOnSelectedGroups(string SelectedGroupId, string title, string msg, string intrval, string clienttime, string time, string date, string imagefile)
        {

            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            try
            {
                ScheduledMessage _ScheduledMessage = new ScheduledMessage();
                GroupScheduleMessage _GroupScheduleMessage = new GroupScheduleMessage();


                Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
                ApiobjScheduledMessage.Timeout = 300000;
                Api.GroupScheduleMessage.GroupScheduleMessage ApiObjGrpSchduleMessage = new Api.GroupScheduleMessage.GroupScheduleMessage();
                ApiObjGrpSchduleMessage.Timeout = 300000;

                int intervaltime = Convert.ToInt32(intrval);

                HttpContext.Current.Session["scheduletime"] = null;
                var SelctGroupId = SelectedGroupId.ToString().Split(',');


                foreach (var item in SelctGroupId)
                {
                    string[] networkingwithid = item.Split(new string[] { "*#*" }, StringSplitOptions.None);


                    if (networkingwithid[1] == "fb")
                    {
                        try
                        {
                            string facebookgrouppost = string.Empty;
                            string groupid = networkingwithid[2];
                            string profileid = networkingwithid[0];
                            //if (intervaltime != 0)
                            //{
                            //    if (HttpContext.Current.Session["scheduletime"] == null)
                            //    {
                            //        string servertime = CompareDateWithclient(clienttime, date + " " + time);
                            //        _ScheduledMessage.ScheduleTime = Convert.ToDateTime(servertime);
                            //        DateTime d1 = _ScheduledMessage.ScheduleTime;
                            //        DateTime d2 = d1.AddMinutes(intervaltime);
                            //        HttpContext.Current.Session["scheduletime"] = d2;
                            //    }
                            //    else
                            //    {
                            //        DateTime d1 = (DateTime)HttpContext.Current.Session["scheduletime"];
                            //        _ScheduledMessage.ScheduleTime = d1;
                            //        DateTime d2 = d1.AddMinutes(intervaltime);
                            //        HttpContext.Current.Session["scheduletime"] = d2;
                            //    }
                            //}
                            _ScheduledMessage.CreateTime = DateTime.Now;
                            _ScheduledMessage.ProfileType = "facebookgroup";
                            _ScheduledMessage.ProfileId = profileid;
                            _ScheduledMessage.Id = Guid.NewGuid();
                            if (!string.IsNullOrEmpty(imagefile))
                            {
                                //var path = System.Configuration.ConfigurationManager.AppSettings["MailSenderDomain"] + "Contents/img/upload";
                                //var path = "www.socioboard.com/Themes/" + System.Configuration.ConfigurationManager.AppSettings["domain"] + "/Contents/img/upload";
                                //string filepath = path + "/" + imagefile;
                                _ScheduledMessage.PicUrl = imagefile;
                            }
                            else
                            {
                                _ScheduledMessage.PicUrl = "";
                            }

                            //_ScheduledMessage.ClientTime = Convert.ToDateTime(clienttime);
                            _ScheduledMessage.ShareMessage = msg;
                            _ScheduledMessage.UserId = objUser.Id;
                            _ScheduledMessage.Status = false;
                            //Domain.Socioboard.Domain.ScheduledMessage _Schedulemessage = (Domain.Socioboard.Domain.ScheduledMessage)new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.AddGroupScheduleMessages(_ScheduledMessage.ScheduleTime.ToString(), _ScheduledMessage.CreateTime.ToString(), _ScheduledMessage.ProfileType.ToString(), _ScheduledMessage.ProfileId.ToString(), _ScheduledMessage.PicUrl.ToString(), _ScheduledMessage.ClientTime.ToString(), _ScheduledMessage.ShareMessage.ToString(), _ScheduledMessage.UserId.ToString(), _ScheduledMessage.Status.ToString()), typeof(Domain.Socioboard.Domain.ScheduledMessage));
                            Domain.Socioboard.Domain.ScheduledMessage _Schedulemessage = (Domain.Socioboard.Domain.ScheduledMessage)new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.AddGroupScheduleMessages(date + " " + time, "it will create at server", _ScheduledMessage.ProfileType.ToString(), _ScheduledMessage.ProfileId.ToString(), _ScheduledMessage.PicUrl.ToString(), clienttime, _ScheduledMessage.ShareMessage.ToString(), _ScheduledMessage.UserId.ToString(), _ScheduledMessage.Status.ToString()), typeof(Domain.Socioboard.Domain.ScheduledMessage));

                            _GroupScheduleMessage.Id = Guid.NewGuid();
                            _GroupScheduleMessage.ScheduleMessageId = _Schedulemessage.Id;
                            _GroupScheduleMessage.GroupId = groupid;
                            ApiObjGrpSchduleMessage.AddGroupScheduleMessage(_GroupScheduleMessage.ScheduleMessageId.ToString(), _GroupScheduleMessage.GroupId.ToString());
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
                            //if (intervaltime != 0)
                            //{
                            //    if (HttpContext.Current.Session["scheduletime"] == null)
                            //    {
                            //        string servertime = CompareDateWithclient(clienttime, date + " " + time);
                            //        _ScheduledMessage.ScheduleTime = Convert.ToDateTime(servertime);
                            //        DateTime d1 = _ScheduledMessage.ScheduleTime;
                            //        DateTime d2 = d1.AddMinutes(intervaltime);
                            //        HttpContext.Current.Session["scheduletime"] = d2;
                            //    }
                            //    else
                            //    {
                            //        DateTime d1 = (DateTime)HttpContext.Current.Session["scheduletime"];
                            //        _ScheduledMessage.ScheduleTime = d1;
                            //        DateTime d2 = d1.AddMinutes(intervaltime);
                            //        HttpContext.Current.Session["scheduletime"] = d2;
                            //    }
                            //}
                            string message = title + "$%^_^%$" + msg;
                            _ScheduledMessage.CreateTime = DateTime.Now;
                            _ScheduledMessage.ProfileType = "linkedingroup";
                            _ScheduledMessage.ProfileId = profileid;
                            _ScheduledMessage.Id = Guid.NewGuid();
                            if (!string.IsNullOrEmpty(imagefile))
                            {
                                // var path = System.Configuration.ConfigurationManager.AppSettings["MailSenderDomain"] + "Contents/img/upload";
                                //var path = "www.socioboard.com/Themes/" + System.Configuration.ConfigurationManager.AppSettings["domain"] + "/Contents/img/upload";
                                //string filepath = path + "/" + imagefile;
                                _ScheduledMessage.PicUrl = imagefile;

                            }
                            else
                            {
                                _ScheduledMessage.PicUrl = "";
                            }
                            _ScheduledMessage.ClientTime = Convert.ToDateTime(clienttime);
                            _ScheduledMessage.ShareMessage = message; ;
                            _ScheduledMessage.UserId = objUser.Id;
                            _ScheduledMessage.Status = false;
                            //Domain.Socioboard.Domain.ScheduledMessage _Schedulemessage = (Domain.Socioboard.Domain.ScheduledMessage)new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.AddGroupScheduleMessages(_ScheduledMessage.ScheduleTime.ToString(), _ScheduledMessage.CreateTime.ToString(), _ScheduledMessage.ProfileType.ToString(), _ScheduledMessage.ProfileId.ToString(), _ScheduledMessage.PicUrl.ToString(), _ScheduledMessage.ClientTime.ToString(), _ScheduledMessage.ShareMessage.ToString(), _ScheduledMessage.UserId.ToString(), _ScheduledMessage.Status.ToString()), typeof(Domain.Socioboard.Domain.ScheduledMessage));
                            Domain.Socioboard.Domain.ScheduledMessage _Schedulemessage = (Domain.Socioboard.Domain.ScheduledMessage)new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.AddGroupScheduleMessages(date + " " + time, "it will create at server", _ScheduledMessage.ProfileType.ToString(), _ScheduledMessage.ProfileId.ToString(), _ScheduledMessage.PicUrl.ToString(), clienttime, _ScheduledMessage.ShareMessage.ToString(), _ScheduledMessage.UserId.ToString(), _ScheduledMessage.Status.ToString()), typeof(Domain.Socioboard.Domain.ScheduledMessage));

                            _GroupScheduleMessage.Id = Guid.NewGuid();
                            _GroupScheduleMessage.ScheduleMessageId = _Schedulemessage.Id;
                            _GroupScheduleMessage.GroupId = groupid;
                            ApiObjGrpSchduleMessage.AddGroupScheduleMessage(_GroupScheduleMessage.ScheduleMessageId.ToString(), _GroupScheduleMessage.GroupId.ToString());
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

        //Modified by sumit gupta [27-02-15]
        public static string PostOnSelectedGroupsModified(string SelectedGroupId, string title, string msg, string intrval, string clienttime, string time, string date, string imagefile)
        {

            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            try
            {
                ScheduledMessage _ScheduledMessage = new ScheduledMessage();
                GroupScheduleMessage _GroupScheduleMessage = new GroupScheduleMessage();


                Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();

                Api.GroupScheduleMessage.GroupScheduleMessage ApiObjGrpSchduleMessage = new Api.GroupScheduleMessage.GroupScheduleMessage();


                int intervaltime = Convert.ToInt32(intrval);

                HttpContext.Current.Session["scheduletime"] = null;
                var SelctGroupId = SelectedGroupId.ToString().Split(',');


                foreach (var item in SelctGroupId)
                {
                    string[] networkingwithid = item.Split(new string[] { "*#*" }, StringSplitOptions.None);


                    if (networkingwithid[1] == "fb")
                    {
                        try
                        {
                            string facebookgrouppost = string.Empty;
                            string groupid = networkingwithid[2];
                            string profileid = networkingwithid[0];
                            //if (intervaltime != 0)
                            //{
                            //    if (HttpContext.Current.Session["scheduletime"] == null)
                            //    {
                            //        string servertime = CompareDateWithclient(clienttime, date + " " + time);
                            //        _ScheduledMessage.ScheduleTime = Convert.ToDateTime(servertime);
                            //        DateTime d1 = _ScheduledMessage.ScheduleTime;
                            //        DateTime d2 = d1.AddMinutes(intervaltime);
                            //        HttpContext.Current.Session["scheduletime"] = d2;
                            //    }
                            //    else
                            //    {
                            //        DateTime d1 = (DateTime)HttpContext.Current.Session["scheduletime"];
                            //        _ScheduledMessage.ScheduleTime = d1;
                            //        DateTime d2 = d1.AddMinutes(intervaltime);
                            //        HttpContext.Current.Session["scheduletime"] = d2;
                            //    }
                            //}
                            _ScheduledMessage.CreateTime = DateTime.Now;
                            _ScheduledMessage.ProfileType = "facebookgroup";
                            _ScheduledMessage.ProfileId = profileid;
                            _ScheduledMessage.Id = Guid.NewGuid();
                            if (!string.IsNullOrEmpty(imagefile))
                            {
                                //var path = System.Configuration.ConfigurationManager.AppSettings["MailSenderDomain"] + "Contents/img/upload";
                                //var path = "www.socioboard.com/Themes/" + System.Configuration.ConfigurationManager.AppSettings["domain"] + "/Contents/img/upload";
                                //string filepath = path + "/" + imagefile;
                                _ScheduledMessage.PicUrl = imagefile;
                            }
                            else
                            {
                                _ScheduledMessage.PicUrl = "";
                            }

                            _ScheduledMessage.ClientTime = Convert.ToDateTime(clienttime);
                            _ScheduledMessage.ShareMessage = msg;
                            _ScheduledMessage.UserId = objUser.Id;
                            _ScheduledMessage.Status = false;
                            Domain.Socioboard.Domain.ScheduledMessage _Schedulemessage = (Domain.Socioboard.Domain.ScheduledMessage)new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.AddGroupScheduleMessages(_ScheduledMessage.ScheduleTime.ToString(), _ScheduledMessage.CreateTime.ToString(), _ScheduledMessage.ProfileType.ToString(), _ScheduledMessage.ProfileId.ToString(), _ScheduledMessage.PicUrl.ToString(), _ScheduledMessage.ClientTime.ToString(), _ScheduledMessage.ShareMessage.ToString(), _ScheduledMessage.UserId.ToString(), _ScheduledMessage.Status.ToString()), typeof(Domain.Socioboard.Domain.ScheduledMessage));

                            _GroupScheduleMessage.Id = Guid.NewGuid();
                            _GroupScheduleMessage.ScheduleMessageId = _Schedulemessage.Id;
                            _GroupScheduleMessage.GroupId = groupid;
                            ApiObjGrpSchduleMessage.AddGroupScheduleMessage(_GroupScheduleMessage.ScheduleMessageId.ToString(), _GroupScheduleMessage.GroupId.ToString());
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
                                // var path = System.Configuration.ConfigurationManager.AppSettings["MailSenderDomain"] + "Contents/img/upload";
                                //var path = "www.socioboard.com/Themes/" + System.Configuration.ConfigurationManager.AppSettings["domain"] + "/Contents/img/upload";
                                //string filepath = path + "/" + imagefile;
                                _ScheduledMessage.PicUrl = imagefile;

                            }
                            else
                            {
                                _ScheduledMessage.PicUrl = "";
                            }
                            _ScheduledMessage.ClientTime = Convert.ToDateTime(clienttime);
                            _ScheduledMessage.ShareMessage = message; ;
                            _ScheduledMessage.UserId = objUser.Id;
                            _ScheduledMessage.Status = false;
                            Domain.Socioboard.Domain.ScheduledMessage _Schedulemessage = (Domain.Socioboard.Domain.ScheduledMessage)new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.AddGroupScheduleMessages(_ScheduledMessage.ScheduleTime.ToString(), _ScheduledMessage.CreateTime.ToString(), _ScheduledMessage.ProfileType.ToString(), _ScheduledMessage.ProfileId.ToString(), _ScheduledMessage.PicUrl.ToString(), _ScheduledMessage.ClientTime.ToString(), _ScheduledMessage.ShareMessage.ToString(), _ScheduledMessage.UserId.ToString(), _ScheduledMessage.Status.ToString()), typeof(Domain.Socioboard.Domain.ScheduledMessage));
                            _GroupScheduleMessage.Id = Guid.NewGuid();
                            _GroupScheduleMessage.ScheduleMessageId = _Schedulemessage.Id;
                            _GroupScheduleMessage.GroupId = groupid;
                            ApiObjGrpSchduleMessage.AddGroupScheduleMessage(_GroupScheduleMessage.ScheduleMessageId.ToString(), _GroupScheduleMessage.GroupId.ToString());
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
            try
            {
                DateTime client = Convert.ToDateTime(clientdate);

                DateTime server = DateTime.Now;
                DateTime schedule = Convert.ToDateTime(scheduletime);
                {
                    var kind = schedule.Kind; // will equal DateTimeKind.Unspecified
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
                        schedule = schedule.AddMinutes(minutes);
                    }
                }
                return schedule.ToString();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return "";
            }
        }
        public static string CompareDateWithclientlocal(string clientdate, string scheduletime)
        {
            try
            {
                DateTime client = Convert.ToDateTime(clientdate);
                DateTime server = DateTime.Now;
                DateTime schedule = Convert.ToDateTime(scheduletime);
                {
                    var kind = schedule.Kind; // will equal DateTimeKind.Unspecified
                    if (DateTime.Compare(client, server) > 0)
                    {
                        double minutes = (client - server).TotalMinutes;
                        schedule = schedule.AddMinutes(minutes);
                    }
                    else if (DateTime.Compare(client, server) == 0)
                    {
                    }
                    else if (DateTime.Compare(client, server) < 0)
                    {
                        double minutes = (client - server).TotalMinutes;
                        schedule = schedule.AddMinutes(minutes);
                    }
                }
                return schedule.ToString();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return "";
            }
        }

        //public static string CompareDateWithclient(string clientdate, string scheduletime)
        //{
        //    DateTime client = Convert.ToDateTime(clientdate);
        //    string strTimeNow = String.Format("{0:s}", client).Replace('T', ' ');

        //    DateTime server = DateTime.Now;
        //    DateTime schedule = Convert.ToDateTime(scheduletime);
        //    if (DateTime.Compare(client, server) > 0)
        //    {

        //        double minutes = (server - client).TotalMinutes;
        //        schedule = schedule.AddMinutes(minutes);

        //    }
        //    else if (DateTime.Compare(client, server) == 0)
        //    {


        //    }
        //    else if (DateTime.Compare(client, server) < 0)
        //    {
        //        double minutes = (server - client).TotalMinutes;
        //        schedule = schedule.AddMinutes(-minutes);
        //    }
        //    return schedule.ToString();
        //}

        public static Domain.Socioboard.Domain.Team GetTeamFromGroupId()
        {
            Api.Team.Team objApiTeam = new Api.Team.Team();
            objApiTeam.Timeout = 300000;
            string groupid = HttpContext.Current.Session["group"].ToString();
            Domain.Socioboard.Domain.Team team = (Domain.Socioboard.Domain.Team)new JavaScriptSerializer().Deserialize(objApiTeam.GetTeamByGroupId(HttpContext.Current.Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Team));
            return team;
        }

        public static Dictionary<Domain.Socioboard.Domain.SocialProfile, object> GetAllUserProfiles()
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Dictionary<Domain.Socioboard.Domain.SocialProfile, object> dict_TeamMember = new Dictionary<Domain.Socioboard.Domain.SocialProfile, object>();
            Api.SocialProfile.SocialProfile ApiobjSocialProfile = new Api.SocialProfile.SocialProfile();
            ApiobjSocialProfile.Timeout = 300000;
            List<Domain.Socioboard.Domain.SocialProfile> lstSocialProfile = (List<Domain.Socioboard.Domain.SocialProfile>)new JavaScriptSerializer().Deserialize(ApiobjSocialProfile.GetAllSocialProfilesOfUser(objUser.Id.ToString()), typeof(List<Domain.Socioboard.Domain.SocialProfile>));
            foreach (var item in lstSocialProfile)
            {
                try
                {
                    if (item.ProfileType == "facebook" || item.ProfileType == "facebook_page")
                    {
                        Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
                        ApiobjFacebookAccount.Timeout = 300000;
                        FacebookAccount objFacebookAccount = (FacebookAccount)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.getFacebookAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(FacebookAccount)));
                        dict_TeamMember.Add(item, objFacebookAccount);
                    }
                    else if (item.ProfileType == "twitter")
                    {
                        Api.TwitterAccount.TwitterAccount ApiobjTwitterAccount = new Api.TwitterAccount.TwitterAccount();
                        ApiobjTwitterAccount.Timeout = 300000;
                        TwitterAccount objTwitterAccount = (TwitterAccount)(new JavaScriptSerializer().Deserialize(ApiobjTwitterAccount.GetTwitterAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(TwitterAccount)));
                        dict_TeamMember.Add(item, objTwitterAccount);
                    }
                    else if (item.ProfileType == "linkedin")
                    {
                        Api.LinkedinAccount.LinkedinAccount ApiobjLinkedinAccount = new Api.LinkedinAccount.LinkedinAccount();
                        ApiobjLinkedinAccount.Timeout = 300000;
                        LinkedInAccount objLinkedInAccount = (LinkedInAccount)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinAccount.GetLinkedinAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(LinkedInAccount)));
                        dict_TeamMember.Add(item, objLinkedInAccount);
                    }
                    else if (item.ProfileType == "instagram")
                    {
                        Api.InstagramAccount.InstagramAccount ApiobjInstagramAccount = new Api.InstagramAccount.InstagramAccount();
                        ApiobjInstagramAccount.Timeout = 300000;
                        InstagramAccount objInstagramAccount = (InstagramAccount)(new JavaScriptSerializer().Deserialize(ApiobjInstagramAccount.UserInformation(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(InstagramAccount)));
                        dict_TeamMember.Add(item, objInstagramAccount);
                    }
                    else if (item.ProfileType == "youtube")
                    {
                        Api.YoutubeAccount.YoutubeAccount ApiobjYoutubeAccount = new Api.YoutubeAccount.YoutubeAccount();
                        ApiobjYoutubeAccount.Timeout = 300000;
                        YoutubeAccount objYoutubeAccount = (YoutubeAccount)(new JavaScriptSerializer().Deserialize(ApiobjYoutubeAccount.GetYoutubeAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(YoutubeAccount)));
                        dict_TeamMember.Add(item, objYoutubeAccount);
                    }
                    else if (item.ProfileType == "tumblr")
                    {
                        Api.TumblrAccount.TumblrAccount ApiobjTumblrAccount = new Api.TumblrAccount.TumblrAccount();
                        ApiobjTumblrAccount.Timeout = 300000;
                        TumblrAccount objTumblrAccount = (TumblrAccount)(new JavaScriptSerializer().Deserialize(ApiobjTumblrAccount.GetTumblrAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(TumblrAccount)));
                        dict_TeamMember.Add(item, objTumblrAccount);
                    }
                    else if (item.ProfileType == "gplus")
                    {
                        Api.GooglePlusAccount.GooglePlusAccount ApiobjGooglePlusAccount = new Api.GooglePlusAccount.GooglePlusAccount();
                        ApiobjGooglePlusAccount.Timeout = 300000;
                        GooglePlusAccount objGplusAccount = (GooglePlusAccount)(new JavaScriptSerializer().Deserialize(ApiobjGooglePlusAccount.GetGooglePlusAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(GooglePlusAccount)));
                        dict_TeamMember.Add(item, objGplusAccount);
                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    //return null;
                }
            }
            return dict_TeamMember;


        }

        public static Dictionary<string, List<object>> GetReportsMenuAccordingToGroup()
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Dictionary<string, List<object>> dic_profilessnap = new Dictionary<string, List<object>>();
            List<object> socialaccounts = null;
            socialaccounts = new List<object>();
            Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
            ApiobjFacebookAccount.Timeout = 300000;
            List<FacebookAccount> lstFacebookAccount = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.GetAllFacebookPageByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<FacebookAccount>)));

            foreach (var FacebookAccount in lstFacebookAccount)
            {
                socialaccounts.Add(FacebookAccount);

            }
            dic_profilessnap.Add("facebook", socialaccounts);

            socialaccounts = new List<object>();
            Api.TwitterAccount.TwitterAccount ApiobjTwitterAccount = new Api.TwitterAccount.TwitterAccount();
            ApiobjTwitterAccount.Timeout = 300000;
            List<TwitterAccount> lstTwitterAccount = (List<TwitterAccount>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterAccount.GetAllTwitterAccountsByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<TwitterAccount>)));
            foreach (var TwitterAccount in lstTwitterAccount)
            {
                socialaccounts.Add(TwitterAccount);
            }
            dic_profilessnap.Add("twitter", socialaccounts);
            return dic_profilessnap;
        }

        public static TwitterReportDetail GetTwitterReportData(string twtProfileId, int days)
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];

            Api.TwitterMessage.TwitterMessage ApiobjTwitterMessage = new Api.TwitterMessage.TwitterMessage();
            ApiobjTwitterMessage.Timeout = 300000;
            Api.TwitterStats.TwitterStats ApiobjTwitterStats = new Api.TwitterStats.TwitterStats();
            ApiobjTwitterStats.Timeout = 300000;
            List<TwitterStatsReport> _TwitterStats = (List<TwitterStatsReport>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterStats.GetAllTwitterStatsDetails(twtProfileId, objUser.Id.ToString(), days.ToString()), typeof(List<TwitterStatsReport>)));
            TwitterReportDetail _TwitterReportDetail = new TwitterReportDetail() { lstTwitterStats = _TwitterStats };

            return _TwitterReportDetail;
        }

        public struct TwitterReportDetail
        {
            public List<TwitterStatsReport> lstTwitterStats { get; set; }
            //public List<TwitterMessage> lstTwitterMessage { get; set; }
        }

        public static List<TaskByUser> GetTeamReportData(int days)
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            List<TaskByUser> _TaskByUser = new List<TaskByUser>();
            Api.Tasks.Tasks ApiobjTasks = new Api.Tasks.Tasks();
            ApiobjTasks.Timeout = 300000;
            try
            {
                _TaskByUser = (List<TaskByUser>)(new JavaScriptSerializer().Deserialize(ApiobjTasks.GetAllTaskByUserIdAndGroupId(objUser.Id.ToString(), objUser.UserName, objUser.ProfileUrl, days.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<TaskByUser>)));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return _TaskByUser;

        }

        public static GroupStatDetails GetGroupStatsData(int days)
        {
            GroupStatDetails _GroupStatDetails = new GroupStatDetails();
            try
            {

                User objUser = (User)System.Web.HttpContext.Current.Session["User"];
                List<Domain.Socioboard.Domain.TeamMemberProfile> lstTeamMemberProfile = new List<Domain.Socioboard.Domain.TeamMemberProfile>();
                Api.Team.Team objApiTeam = new Api.Team.Team();
                objApiTeam.Timeout = 300000;
                string groupid = System.Web.HttpContext.Current.Session["group"].ToString();
                JObject team = JObject.Parse(objApiTeam.GetTeamByGroupId(System.Web.HttpContext.Current.Session["group"].ToString()));
                Api.TeamMemberProfile.TeamMemberProfile objApiTeamMemberProfile = new Api.TeamMemberProfile.TeamMemberProfile();
                objApiTeamMemberProfile.Timeout = 300000;
                //JArray TeamMemberProfiles = JArray.Parse(objApiTeamMemberProfile.GetTeamMemberProfilesByTeamId(Convert.ToString(team["Id"])));

                //  var asd=  objApiTeamMemberProfile.GetTeamMembeDetailsForGroupReport(Convert.ToString(team["Id"]), objUser.Id.ToString(), days.ToString());

                _GroupStatDetails = (GroupStatDetails)(new JavaScriptSerializer().Deserialize(objApiTeamMemberProfile.GetTeamMembeDetailsForGroupReport(Convert.ToString(team["Id"]), objUser.Id.ToString(), days.ToString()), typeof(GroupStatDetails)));


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }


            return _GroupStatDetails;
        }

        public static Dictionary<Domain.Socioboard.Domain.GroupProfile, object> GetProfilesConnectedwithgroup()
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Dictionary<Domain.Socioboard.Domain.GroupProfile, object> dict_TeamMember = new Dictionary<Domain.Socioboard.Domain.GroupProfile, object>();
            Api.GroupProfile.GroupProfile ApiobjGroupProfile = new Api.GroupProfile.GroupProfile();
            ApiobjGroupProfile.Timeout = 300000;
            List<Domain.Socioboard.Domain.GroupProfile> lstSocialProfile = (List<Domain.Socioboard.Domain.GroupProfile>)new JavaScriptSerializer().Deserialize(ApiobjGroupProfile.GetAllProfilesConnectedWithGroup(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["selectedgroupid"].ToString()), typeof(List<Domain.Socioboard.Domain.GroupProfile>));

            foreach (var item in lstSocialProfile)
            {
                try
                {
                    if (item.ProfileType == "facebook")
                    {
                        Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
                        ApiobjFacebookAccount.Timeout = 300000;
                        FacebookAccount objFacebookAccount = (FacebookAccount)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.getFacebookAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(FacebookAccount)));
                        dict_TeamMember.Add(item, objFacebookAccount);
                    }
                    else if (item.ProfileType == "twitter")
                    {
                        Api.TwitterAccount.TwitterAccount ApiobjTwitterAccount = new Api.TwitterAccount.TwitterAccount();
                        ApiobjTwitterAccount.Timeout = 300000;
                        TwitterAccount objTwitterAccount = (TwitterAccount)(new JavaScriptSerializer().Deserialize(ApiobjTwitterAccount.GetTwitterAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(TwitterAccount)));
                        dict_TeamMember.Add(item, objTwitterAccount);
                    }
                    else if (item.ProfileType == "linkedin")
                    {
                        Api.LinkedinAccount.LinkedinAccount ApiobjLinkedinAccount = new Api.LinkedinAccount.LinkedinAccount();
                        ApiobjLinkedinAccount.Timeout = 300000;
                        LinkedInAccount objLinkedInAccount = (LinkedInAccount)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinAccount.GetLinkedinAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(LinkedInAccount)));
                        dict_TeamMember.Add(item, objLinkedInAccount);
                    }
                    else if (item.ProfileType == "instagram")
                    {
                        Api.InstagramAccount.InstagramAccount ApiobjInstagramAccount = new Api.InstagramAccount.InstagramAccount();
                        ApiobjInstagramAccount.Timeout = 300000;
                        InstagramAccount objInstagramAccount = (InstagramAccount)(new JavaScriptSerializer().Deserialize(ApiobjInstagramAccount.UserInformation(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(InstagramAccount)));
                        dict_TeamMember.Add(item, objInstagramAccount);
                    }
                    else if (item.ProfileType == "youtube")
                    {
                        Api.YoutubeAccount.YoutubeAccount ApiobjYoutubeAccount = new Api.YoutubeAccount.YoutubeAccount();
                        ApiobjYoutubeAccount.Timeout = 300000;
                        YoutubeAccount objYoutubeAccount = (YoutubeAccount)(new JavaScriptSerializer().Deserialize(ApiobjYoutubeAccount.GetYoutubeAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(YoutubeAccount)));
                        dict_TeamMember.Add(item, objYoutubeAccount);
                    }
                    else if (item.ProfileType == "tumblr")
                    {
                        Api.TumblrAccount.TumblrAccount ApiobjTumblrAccount = new Api.TumblrAccount.TumblrAccount();
                        ApiobjTumblrAccount.Timeout = 300000;
                        TumblrAccount objTumblrAccount = (TumblrAccount)(new JavaScriptSerializer().Deserialize(ApiobjTumblrAccount.GetTumblrAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(TumblrAccount)));
                        dict_TeamMember.Add(item, objTumblrAccount);
                    }
                    else if (item.ProfileType == "gplus")
                    {
                        Api.GooglePlusAccount.GooglePlusAccount ApiobjGooglePlusAccount = new Api.GooglePlusAccount.GooglePlusAccount();
                        ApiobjGooglePlusAccount.Timeout = 300000;
                        GooglePlusAccount objGplusAccount = (GooglePlusAccount)(new JavaScriptSerializer().Deserialize(ApiobjGooglePlusAccount.GetGooglePlusAccountDetailsById(objUser.Id.ToString(), item.ProfileId.ToString()), typeof(GooglePlusAccount)));
                        dict_TeamMember.Add(item, objGplusAccount);
                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    //return null;
                }
            }
            return dict_TeamMember;


        }

        public static Dictionary<string, string> GetTumblerData(string tumblrusername)
        {
            Dictionary<string, string> dic_tmblerdata = new Dictionary<string, string>();
            try
            {
                Api.Tumblr.Tumblr ApiobjTumblr = new Api.Tumblr.Tumblr();

                Domain.Socioboard.Domain.TumblerData objTumblerData = (Domain.Socioboard.Domain.TumblerData)(new JavaScriptSerializer().Deserialize(ApiobjTumblr.TumblerData(tumblrusername), typeof(Domain.Socioboard.Domain.TumblerData)));
                string LikesCount = objTumblerData.LikesCount;
                string PostCount = objTumblerData.PostCount;
                dic_tmblerdata.Add("PostCount", PostCount);
                dic_tmblerdata.Add("LikesCount", LikesCount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dic_tmblerdata;
        }

        //vikash
        public static string GetAllTwitterFollowersCountofUser(string profileid, string userid)
        {
            int _totalCount = 0;
            string TotalFollowerCount = string.Empty;
            try
            {
                Api.TwitterAccount.TwitterAccount ApiobjTwitterAccount = new Api.TwitterAccount.TwitterAccount();
                ApiobjTwitterAccount.Timeout = 300000;
                List<TwitterAccount> lstAllTwitterAccountDetails = (List<TwitterAccount>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterAccount.GetAllAccountDetailsByProfileId(profileid, userid), typeof(List<TwitterAccount>)));
                foreach (TwitterAccount item in lstAllTwitterAccountDetails)
                {
                    _totalCount += item.FollowersCount;
                }
                TotalFollowerCount = _totalCount.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return TotalFollowerCount;
        }
        // vikash

        //public static string GetAllFacebookFancountofUser(string profileid, string userid)
        //{
        //    int _totalCount = 0;
        //    string TotalfanCount = string.Empty;
        //    try
        //    {
        //        Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
        //        List<FacebookAccount> lstAllFacebookAccountDetails = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.GetAllFacebookAccountDetails(profileid, userid), typeof(List<FacebookAccount>)));
        //        foreach (FacebookAccount item in lstAllFacebookAccountDetails)
        //        {
        //            if (item.Type == "Page")
        //            {
        //                _totalCount += item.Friends;
        //            }
        //        }
        //        TotalfanCount = _totalCount.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return TotalfanCount;
        //}
        // vikash [02/12/2014]
        public static string GetAllFacebookFancountofUser(string profileid, string userid)
        {
            int _totalCount = 0;
            string TotalfanCount = string.Empty;
            try
            {
                Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
                ApiobjFacebookAccount.Timeout = 300000;
                List<FacebookAccount> lstAllFacebookAccountDetails = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.GetAllFacebookAccountDetails(profileid, userid), typeof(List<FacebookAccount>)));
                foreach (FacebookAccount item in lstAllFacebookAccountDetails)
                {
                    if (item.Type == "Page")
                    {
                        _totalCount += item.Friends;
                    }
                }
                if (_totalCount > 1000000)
                {
                    int r = _totalCount % 1000000;
                    int t = _totalCount / 1000000;
                    TotalfanCount = t.ToString() + "." + (r / 10000).ToString() + "M";
                }
                else if (_totalCount > 1000)
                {
                    int r = _totalCount % 1000;
                    int t = _totalCount / 1000;
                    TotalfanCount = t.ToString() + "." + (r / 100).ToString() + "K";
                }
                else
                {
                    TotalfanCount = _totalCount.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return TotalfanCount;
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public static string RenderViewToString1(System.Web.Mvc.ControllerContext context, TempDataDictionary tempData, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = context.RouteData.GetRequiredString("action");

            var viewData = new ViewDataDictionary(model);

            using (var sw = new System.IO.StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                var viewContext = new ViewContext(context, viewResult.View, viewData, tempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        public static string RenderViewToString(System.Web.Mvc.ControllerContext context, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = context.RouteData.GetRequiredString("action");

            var viewData = new ViewDataDictionary(model);

            using (var sw = new System.IO.StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                var viewContext = new ViewContext(context, viewResult.View, viewData, new TempDataDictionary(), sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        public static List<FbPagePost> FbPagePostDetails(string FbUserId, string UserId)
        {
            List<FbPagePost> _FbPagePost = new List<FbPagePost>();
            try
            {
                Api.Facebook.Facebook _Facebook = new Api.Facebook.Facebook();
                _Facebook.Timeout = 300000;
                _FbPagePost = (List<FbPagePost>)(new JavaScriptSerializer().Deserialize(_Facebook.GetAllFbPagePostDetails(FbUserId, UserId), typeof(List<FbPagePost>)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return _FbPagePost;
        }


        public static string GenerateRandomUniqueString()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            return GuidString;
        }

        public static List<string> ReadFiletoStringList(string filepath)
        {
            List<string> list = new List<string>();
            using (StreamReader reader = new StreamReader(filepath))
            {
                string text = "";
                while ((text = reader.ReadLine()) != null)
                {
                    list.Add(text);
                }
            }
            return list;


        }

        public static List<Affiliates> GetAffiliatesData(Guid UserId, Guid FriendsUserId)
        {
            Api.Affiliates.Affiliates ApiAffiliate = new Socioboard.Api.Affiliates.Affiliates();
            List<Affiliates> lstAffiliate = (List<Affiliates>)new JavaScriptSerializer().Deserialize(ApiAffiliate.GetAffilieteDetailbyUserId(UserId.ToString(), FriendsUserId.ToString()), typeof(List<Affiliates>));
            return lstAffiliate;
        }

        public static string GetWordpressRedirectLink()
        {
            return "https://public-api.wordpress.com/oauth2/authorize?client_id=" + ConfigurationManager.AppSettings["WordpessClientID"] + "&redirect_uri=" + ConfigurationManager.AppSettings["WordpessCallBackURL"] + "&response_type=code&scope=global";
        }
    }

}