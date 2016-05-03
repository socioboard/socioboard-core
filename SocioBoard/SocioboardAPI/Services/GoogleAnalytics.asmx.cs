using GlobusGooglePlusLib.Authentication;
using GlobussDropboxLib.Authentication;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using GlobusGooglePlusLib.GAnalytics.Core.Accounts;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using GlobusGooglePlusLib.GAnalytics.Core.AnalyticsMethod;
using GlobusTwitterLib.Twitter.Core.SearchMethods;
using GlobusTwitterLib.Twitter.Core.UserMethods;
using GlobusTwitterLib.Authentication;
using MongoDB.Bson;
using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Ionic.Zlib;
using log4net;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for GoogleAnalytics
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class GoogleAnalytics : System.Web.Services.WebService
    {
        ILog logger = LogManager.GetLogger(typeof(GoogleAnalytics));
        GoogleAnalyticsAccountRepository _GoogleAnalyticsAccountRepository = new GoogleAnalyticsAccountRepository();
        TeamRepository objTeamRepository = new TeamRepository();
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
        MongoRepository TwtsearchRepo = new MongoRepository("TwitterUrlMentions");
        MongoRepository ArticlesAndBlogsRepo = new MongoRepository("ArticlesAndBlogs");
        private GroupProfileRepository grpProfileRepo = new Model.GroupProfileRepository();

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAnalyticsProfile(string code)
        {
            Domain.Socioboard.Helper.GoogleAnalyticsProfiles _GoogleAnalyticsProfiles;
            List<Domain.Socioboard.Helper.GoogleAnalyticsProfiles> lstGoogleAnalyticsProfiles = new List<Domain.Socioboard.Helper.GoogleAnalyticsProfiles>();
            string access_token = string.Empty;
            string refresh_token = string.Empty;
            Accounts _Accounts = new Accounts();

            try
            {
                oAuthTokenGa objToken = new oAuthTokenGa();
                objToken.ConsumerKey = ConfigurationManager.AppSettings["YtconsumerKey"];
                objToken.ConsumerSecret = ConfigurationManager.AppSettings["YtconsumerSecret"];
                objToken.RedirectUri = ConfigurationManager.AppSettings["Ytredirect_uri"];

                string accessToken = objToken.GetRefreshToken(code);
                JObject JData = JObject.Parse(accessToken);

                try
                {
                    refresh_token = JData["refresh_token"].ToString();
                }
                catch (Exception ex)
                {
                    access_token = JData["access_token"].ToString();
                    objToken.RevokeToken(access_token);
                    logger.Error("Refresh Token Not Found = > " + ex.Message);
                    return "Refresh Token Not Found";
                }

                access_token = JData["access_token"].ToString();

                string accountsdata = _Accounts.getGaAccounts(access_token);

                JObject JAccountdata = JObject.Parse(accountsdata);

                string EmailId = JAccountdata["username"].ToString();

                foreach (var item in JAccountdata["items"])
                {
                    try
                    {
                        string accountId = item["id"].ToString();
                        string accountName = item["name"].ToString();
                        string profileData = _Accounts.getGaProfiles(access_token, accountId);
                        JObject JProfileData = JObject.Parse(profileData);
                        foreach (var item_profile in JProfileData["items"])
                        {
                            try
                            {
                                _GoogleAnalyticsProfiles = new Domain.Socioboard.Helper.GoogleAnalyticsProfiles();
                                _GoogleAnalyticsProfiles.AccessToken = access_token;
                                _GoogleAnalyticsProfiles.RefreshToken = refresh_token;
                                _GoogleAnalyticsProfiles.AccountId = accountId;
                                _GoogleAnalyticsProfiles.AccountName = accountName;
                                _GoogleAnalyticsProfiles.EmailId = EmailId;
                                _GoogleAnalyticsProfiles.ProfileId = item_profile["id"].ToString();
                                _GoogleAnalyticsProfiles.ProfileName = item_profile["name"].ToString();
                                _GoogleAnalyticsProfiles.WebPropertyId = item_profile["webPropertyId"].ToString();
                                _GoogleAnalyticsProfiles.WebsiteUrl = item_profile["websiteUrl"].ToString();
                                lstGoogleAnalyticsProfiles.Add(_GoogleAnalyticsProfiles);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetAnalyticsProfile = >  " + ex.Message);
            }

            return new JavaScriptSerializer().Serialize(lstGoogleAnalyticsProfiles);

        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddAnalyticsProfiles(string GAProfiles, string UserId, string GroupId)
        {
            try
            {
                Analytics _Analytics = new Analytics();
                List<Domain.Socioboard.Helper.GoogleAnalyticsProfiles> lstGoogleAnalyticsProfiles = (List<Domain.Socioboard.Helper.GoogleAnalyticsProfiles>)new JavaScriptSerializer().Deserialize(GAProfiles, typeof(List<Domain.Socioboard.Helper.GoogleAnalyticsProfiles>));
                Domain.Socioboard.Domain.GoogleAnalyticsAccount _GoogleAnalyticsAccount;
                foreach (var item in lstGoogleAnalyticsProfiles)
                {
                    try
                    {
                        _GoogleAnalyticsAccount = new Domain.Socioboard.Domain.GoogleAnalyticsAccount();
                        _GoogleAnalyticsAccount.UserId = Guid.Parse(UserId);
                        _GoogleAnalyticsAccount.EmailId = item.EmailId;
                        _GoogleAnalyticsAccount.GaAccountId = item.AccountId;
                        _GoogleAnalyticsAccount.GaAccountName = item.AccountName;
                        _GoogleAnalyticsAccount.GaWebPropertyId = item.WebPropertyId;
                        _GoogleAnalyticsAccount.GaProfileId = item.ProfileId;
                        _GoogleAnalyticsAccount.GaProfileName = item.ProfileName;
                        _GoogleAnalyticsAccount.AccessToken = item.AccessToken;
                        _GoogleAnalyticsAccount.RefreshToken = item.RefreshToken;
                        _GoogleAnalyticsAccount.WebsiteUrl = item.WebsiteUrl;
                        string visits = string.Empty;
                        string pageviews = string.Empty;
                        try
                        {
                            string analytics = _Analytics.getAnalyticsData(item.ProfileId, "ga:visits,ga:pageviews", DateTime.UtcNow.AddDays(-7).ToString("yyyy-MM-dd"), DateTime.UtcNow.ToString("yyyy-MM-dd"), item.AccessToken);
                            JObject JData = JObject.Parse(analytics);
                            visits = JData["totalsForAllResults"]["ga:visits"].ToString();
                            pageviews = JData["totalsForAllResults"]["ga:pageviews"].ToString();
                        }
                        catch (Exception ex)
                        {
                            visits = "0";
                            pageviews = "0";
                        }
                        _GoogleAnalyticsAccount.Views = Double.Parse(pageviews);
                        _GoogleAnalyticsAccount.Visits = Double.Parse(visits);
                        _GoogleAnalyticsAccount.ProfilePicUrl = ConfigurationManager.AppSettings["DomainName"] + "/Themes/" + ConfigurationManager.AppSettings["DefaultGroupName"] + "/Contents/img/analytics_img.png";
                        _GoogleAnalyticsAccount.IsActive = true;
                        _GoogleAnalyticsAccount.EntryDate = DateTime.UtcNow;
                        if (!_GoogleAnalyticsAccountRepository.checkGoogelAnalyticsUserExists(item.ProfileId, item.AccountId, Guid.Parse(UserId)))
                        {
                            _GoogleAnalyticsAccountRepository.Add(_GoogleAnalyticsAccount);
                        }
                        #region  TeamMemberProfile
                        Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(GroupId));
                        if (!grpProfileRepo.checkProfileExistsingroup(Guid.Parse(GroupId), item.ProfileId))
                        {
                            //Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
                            //objTeamMemberProfile.ProfileId = item.ProfileId;
                            //objTeamMemberProfile.ProfileName = item.ProfileName;
                            //objTeamMemberProfile.ProfilePicUrl = ConfigurationManager.AppSettings["DomainName"] + "/Themes/Socioboard/Contents/img/analytics_img.png";
                            //objTeamMemberProfile.ProfileType = "googleanalytics";
                            //objTeamMemberProfile.Status = 1;
                            //objTeamMemberProfile.StatusUpdateDate = DateTime.Now;
                            //objTeamMemberProfile.TeamId = objTeam.Id;
                            //objTeamMemberProfileRepository.addNewTeamMember(objTeamMemberProfile);


                            Domain.Socioboard.Domain.GroupProfile grpProfile = new Domain.Socioboard.Domain.GroupProfile();
                            grpProfile.Id = Guid.NewGuid();
                            grpProfile.EntryDate = DateTime.UtcNow;
                            grpProfile.GroupId = Guid.Parse(GroupId);
                            grpProfile.GroupOwnerId = Guid.Parse(UserId);
                            grpProfile.ProfileId = item.ProfileId;
                            grpProfile.ProfileType = "googleanalytics";
                            grpProfile.ProfileName = item.ProfileName;
                            grpProfile.ProfilePic = ConfigurationManager.AppSettings["DomainName"] + "/Themes/" + ConfigurationManager.AppSettings["DefaultGroupName"] + "/Contents/img/analytics_img.png";
                            grpProfileRepo.AddGroupProfile(grpProfile);
                        }
                        #endregion
                        #region SocialProfile
                        Domain.Socioboard.Domain.SocialProfile objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
                        objSocialProfile.ProfileType = "googleanalytics";
                        objSocialProfile.ProfileId = item.ProfileId;
                        objSocialProfile.UserId = Guid.Parse(UserId);
                        objSocialProfile.ProfileDate = DateTime.Now;
                        objSocialProfile.ProfileStatus = 1;
                        if (!objSocialProfilesRepository.checkUserProfileExist(objSocialProfile))
                        {
                            objSocialProfilesRepository.addNewProfileForUser(objSocialProfile);
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        logger.Error("AddAnalyticsProfiles = > " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("AddAnalyticsProfiles = > " + ex.Message);
            }
            return "added successfully";
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateGoogleAnalyticsAccount(string ProfileId, string AccessToken, string HostName)
        {
            try
            {
                HostName = HostName.Replace("www.", "");
                Analytics _Analytics = new Analytics();
                string visits = string.Empty;
                string pageviews = string.Empty;
                string finalToken = string.Empty;
                oAuthTokenGa objToken = new oAuthTokenGa();
                string finaltoken = objToken.GetAccessToken(AccessToken);
                JObject objArray = JObject.Parse(finaltoken);

                try
                {
                    finalToken = objArray["access_token"].ToString();
                    // break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                try
                {
                    string analytics = _Analytics.getAnalyticsData(ProfileId, "ga:visits,ga:pageviews", DateTime.UtcNow.AddDays(-7).ToString("yyyy-MM-dd"), DateTime.UtcNow.ToString("yyyy-MM-dd"), finalToken);
                    JObject JData = JObject.Parse(analytics);
                    visits = JData["totalsForAllResults"]["ga:visits"].ToString();
                    pageviews = JData["totalsForAllResults"]["ga:pageviews"].ToString();

                    double startUnixTime = DateTime.UtcNow.AddDays(-7).Date.ToUnixTimestamp();
                    double endUnixTime = DateTime.UtcNow.Date.ToUnixTimestamp();
                    var ret = TwtsearchRepo.Find<Domain.Socioboard.MongoDomain.TwitterUrlMentions>(t => t.HostName.Equals(HostName));
                    var task = Task.Run(async () =>
                    {
                        return await ret;
                    });
                    int twtCount = task.Result.Where(t => t.Feeddate > startUnixTime && t.Feeddate < endUnixTime).ToList().Count;

                    var ret1 = ArticlesAndBlogsRepo.Find<Domain.Socioboard.MongoDomain.ArticlesAndBlogs>(t => t.HostName.Equals(HostName));
                    var task1 = Task.Run(async () =>
                    {
                        return await ret1;
                    });
                    int articleCount = task1.Result.Where(t => t.Created_Time > startUnixTime && t.Created_Time < endUnixTime).ToList().Count;

                    _GoogleAnalyticsAccountRepository.updateGoogelAnalyticsUser(ProfileId, Double.Parse(visits), Double.Parse(pageviews), Double.Parse(twtCount.ToString()), Double.Parse(articleCount.ToString()));

                }
                catch (Exception ex)
                {
                    logger.Error("UpdateGoogleAnalyticsAccount1 => " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                logger.Error("UpdateGoogleAnalyticsAccount2 => " + ex.Message);
            }

            return "Successfuly Updated";
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetGoogleAnalyticsData(string ProfileId, string AccessToken, string HostName)
        {
            try
            {
                HostName = HostName.Replace("www.", "");
                string arrVisit = string.Empty;
                string arrViews = string.Empty;
                string arrTwtMentions = string.Empty;
                string arrActicleandBlogs = string.Empty;
                string finalToken = string.Empty;
                oAuthTokenGa objToken = new oAuthTokenGa();
                string finaltoken = objToken.GetAccessToken(AccessToken);
                try
                {
                    JObject objArray = JObject.Parse(finaltoken);
                    finalToken = objArray["access_token"].ToString();
                }
                catch (Exception ex)
                {
                    finalToken = AccessToken;
                    Console.WriteLine(ex.StackTrace);
                }
                Analytics _Analytics = new Analytics();
                DateTime startDate = DateTime.UtcNow.AddDays(-90);
                while (startDate.Date < DateTime.UtcNow.Date)
                {
                    try
                    {
                        string visits = string.Empty;
                        string pageviews = string.Empty;
                        try
                        {
                            string analytics = _Analytics.getAnalyticsData(ProfileId, "ga:visits,ga:pageviews", startDate.ToString("yyyy-MM-dd"), startDate.AddDays(2).ToString("yyyy-MM-dd"), finalToken);
                            JObject JData = JObject.Parse(analytics);
                            visits = JData["totalsForAllResults"]["ga:visits"].ToString();
                            pageviews = JData["totalsForAllResults"]["ga:pageviews"].ToString();
                            arrVisit += visits + ",";
                            arrViews += pageviews + ",";
                        }
                        catch (Exception ex)
                        {
                            arrVisit += "0" + ",";
                            arrViews += "0" + ",";
                        }

                        long startUnixTime = startDate.Date.ToUnixTimestamp();
                        long endUnixTime = startDate.AddDays(3).Date.AddSeconds(-1).ToUnixTimestamp();
                        var ret = TwtsearchRepo.Find<Domain.Socioboard.MongoDomain.TwitterUrlMentions>(t => t.HostName.Equals(HostName));
                        var task = Task.Run(async () =>
                        {
                            return await ret;
                        });
                        IList<Domain.Socioboard.MongoDomain.TwitterUrlMentions> lstTwitterUrlMentions = task.Result.ToList();
                        int twtCount = lstTwitterUrlMentions.Count(t => t.Feeddate > startUnixTime && t.Feeddate <= endUnixTime);

                        arrTwtMentions += twtCount.ToString() + ",";

                        var ret1 = ArticlesAndBlogsRepo.Find<Domain.Socioboard.MongoDomain.ArticlesAndBlogs>(t => t.HostName.Equals(HostName));
                        var task1 = Task.Run(async () =>
                        {
                            return await ret1;
                        });
                        IList<Domain.Socioboard.MongoDomain.ArticlesAndBlogs> lstArticlesAndBlogs=task1.Result.ToList();
                        int artucleCount = lstArticlesAndBlogs.Count(t => t.Created_Time > startUnixTime && t.Created_Time <= endUnixTime);

                        arrActicleandBlogs += artucleCount.ToString() + ",";

                        startDate = startDate.AddDays(3);
                    }
                    catch (Exception ex)
                    {
                        logger.Error("GetGoogleAnalyticsData1 => " + ex.Message);
                    }
                }
                arrVisit = arrVisit.TrimEnd(',');
                arrViews = arrViews.TrimEnd(',');
                arrTwtMentions = arrTwtMentions.TrimEnd(',');
                arrActicleandBlogs = arrActicleandBlogs.TrimEnd(',');
                Domain.Socioboard.Domain.GoogleAnalyticsReport _GoogleAnalyticsReport = new Domain.Socioboard.Domain.GoogleAnalyticsReport();
                _GoogleAnalyticsReport.GaProfileId = ProfileId;
                _GoogleAnalyticsReport.Views = arrViews;
                _GoogleAnalyticsReport.Visits = arrVisit;
                _GoogleAnalyticsReport.TwitterMention = arrTwtMentions;
                _GoogleAnalyticsReport.Article_Blogs = arrActicleandBlogs;
                _GoogleAnalyticsAccountRepository.AddGoogleAnalyticsReport(_GoogleAnalyticsReport);
            }
            catch (Exception ex)
            {
                logger.Error("GetGoogleAnalyticsData2 => " + ex.Message);
            }
            return "Updated Successfuly";
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void GetTwitterWebMentions(string HostName)
        {
            try
            {
                HostName = HostName.Replace("www.", "");
                JArray output = new JArray();
                SortedDictionary<string, string> requestParameters = new SortedDictionary<string, string>();
                try
                {
                    var oauth_url = " https://api.twitter.com/1.1/search/tweets.json?q=" + HostName.Trim() + "&result_type=recent&count=30";
                    var headerFormat = "Bearer {0}";
                    var authHeader = string.Format(headerFormat, "AAAAAAAAAAAAAAAAAAAAAOZyVwAAAAAAgI0VcykgJ600le2YdR4uhKgjaMs%3D0MYOt4LpwCTAIi46HYWa85ZcJ81qi0D9sh8avr1Zwf7BDzgdHT");

                    var postBody = requestParameters.ToWebString();
                    ServicePointManager.Expect100Continue = false;

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(oauth_url + "?"
                           + requestParameters.ToWebString());

                    request.Headers.Add("Authorization", authHeader);
                    request.Method = "GET";
                    request.Headers.Add("Accept-Encoding", "gzip");
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    Stream responseStream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
                    using (var reader = new StreamReader(responseStream))
                    {
                        var objText = reader.ReadToEnd();
                        output = JArray.Parse(JObject.Parse(objText)["statuses"].ToString());
                    }
                }
                catch (Exception ex)
                {
                }
                Domain.Socioboard.MongoDomain.TwitterUrlMentions _TwitterUrlMentions;
                try
                {
                    foreach (var chile in output)
                    {
                        try
                        {
                            _TwitterUrlMentions = new Domain.Socioboard.MongoDomain.TwitterUrlMentions();
                            _TwitterUrlMentions.Id = ObjectId.GenerateNewId();
                            _TwitterUrlMentions.Feed = chile["text"].ToString();
                            _TwitterUrlMentions.Feeddate = Utility.ParseTwitterTime(chile["created_at"].ToString()).ToUnixTimestamp();
                            _TwitterUrlMentions.FeedId = chile["id_str"].ToString();
                            _TwitterUrlMentions.FromId = chile["user"]["id_str"].ToString();
                            _TwitterUrlMentions.FromImageUrl = chile["user"]["profile_image_url"].ToString();
                            _TwitterUrlMentions.FromName = chile["user"]["screen_name"].ToString();
                            _TwitterUrlMentions.HostName = HostName;

                            var ret = TwtsearchRepo.Find<Domain.Socioboard.MongoDomain.TwitterUrlMentions>(t => t.FeedId.Equals(_TwitterUrlMentions.FeedId) && t.HostName.Equals(_TwitterUrlMentions.HostName));
                            var task = Task.Run(async () =>
                            {
                                return await ret;
                            });
                            int count = task.Result.Count;

                            if (count < 1)
                            {
                                TwtsearchRepo.Add(_TwitterUrlMentions);
                            }
                        }
                        catch { }
                    }
                }
                catch { }
            }
            catch { }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string DailyMotionPost(string Url)
        {
            try
            {
                string _dailymotionpostRestUrl = "https://api.dailymotion.com/videos/?search=" + Url + "&fields=id,title,created_time,url,description";

                string response = WebRequst(_dailymotionpostRestUrl);

                var jdata = Newtonsoft.Json.Linq.JObject.Parse(response);

                foreach (var item in jdata["list"])
                {
                    Domain.Socioboard.MongoDomain.ArticlesAndBlogs _ArticlesAndBlogs = new Domain.Socioboard.MongoDomain.ArticlesAndBlogs();
                    _ArticlesAndBlogs.Id = ObjectId.GenerateNewId();
                    _ArticlesAndBlogs.Type = "dailymotion";
                    _ArticlesAndBlogs.HostName = Url;
                    
                    try
                    {
                        _ArticlesAndBlogs.VideoId = item["id"].ToString();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                        _ArticlesAndBlogs.VideoId = "";
                    }
                    try
                    {
                        _ArticlesAndBlogs.Title = item["title"].ToString();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                        _ArticlesAndBlogs.Title = "";
                    }
                    try
                    {
                        _ArticlesAndBlogs.VideoUrl = item["url"].ToString();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                        _ArticlesAndBlogs.VideoUrl = "";
                    }
                    try
                    {
                        _ArticlesAndBlogs.Description = item["description"].ToString();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                        _ArticlesAndBlogs.Description = "";
                    }
                    try
                    {
                        _ArticlesAndBlogs.Created_Time =long.Parse(item["created_time"].ToString());
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                        _ArticlesAndBlogs.Created_Time = DateTime.UtcNow.ToUnixTimestamp();
                    }
                    _ArticlesAndBlogs.Url = Url;
                    var ret = ArticlesAndBlogsRepo.Find<Domain.Socioboard.MongoDomain.ArticlesAndBlogs>(t => t.VideoId.Equals(_ArticlesAndBlogs.VideoId) && t.Type.Equals(_ArticlesAndBlogs.Type));
                    var task = Task.Run(async () =>
                    {
                        return await ret;
                    });
                    int count = task.Result.Count;
                    if (count < 1)
                    {
                        ArticlesAndBlogsRepo.Add(_ArticlesAndBlogs);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("DailyMotionPost => " + ex.Message);
            }
            return "Successfully updated";


        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetYoutubeSearchData(string Url)
        {
            try
            {
                string youtubesearchurl = "https://www.googleapis.com/youtube/v3/search?part=snippet&maxResults=15&order=relevance&q=" + Url + "&key=" + ConfigurationManager.AppSettings["Api_Key"];
                string response = WebRequst(youtubesearchurl);
                var Jdata = Newtonsoft.Json.Linq.JObject.Parse(response);

                foreach (var item in Jdata["items"])
                {

                    Domain.Socioboard.MongoDomain.ArticlesAndBlogs _ArticlesAndBlogs = new Domain.Socioboard.MongoDomain.ArticlesAndBlogs();
                    _ArticlesAndBlogs.Id = ObjectId.GenerateNewId();
                    _ArticlesAndBlogs.Type = "youtube";
                    _ArticlesAndBlogs.HostName = Url;
                  
                    try
                    {
                        _ArticlesAndBlogs.VideoId = item["id"]["videoId"].ToString();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                        logger.Error(ex.Message);
                        _ArticlesAndBlogs.VideoId = "";
                    }

                    if (!string.IsNullOrEmpty(_ArticlesAndBlogs.VideoId))
                    {
                        try
                        {
                            _ArticlesAndBlogs.VideoUrl = "https://www.youtube.com/watch?v="+_ArticlesAndBlogs.VideoId;
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.StackTrace);
                            logger.Error(ex.Message);
                            _ArticlesAndBlogs.VideoUrl = "";
                        }
                        _ArticlesAndBlogs.Url = Url;
                        try
                        {
                            _ArticlesAndBlogs.Title = item["snippet"]["title"].ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.StackTrace);
                            logger.Error(ex.Message);
                            _ArticlesAndBlogs.Title = "";
                        }
                        try
                        {
                            _ArticlesAndBlogs.Description = item["snippet"]["description"].ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.StackTrace);
                            logger.Error(ex.Message);
                            _ArticlesAndBlogs.Description = "";
                        }
                        try
                        {
                            _ArticlesAndBlogs.Created_Time = (DateTime.Parse(item["snippet"]["publishedAt"].ToString())).ToUnixTimestamp();
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.StackTrace);
                            logger.Error(ex.Message);
                            _ArticlesAndBlogs.Created_Time = DateTime.UtcNow.ToUnixTimestamp();
                        }

                        var ret = ArticlesAndBlogsRepo.Find<Domain.Socioboard.MongoDomain.ArticlesAndBlogs>(t => t.VideoId.Equals(_ArticlesAndBlogs.VideoId) && t.Type.Equals(_ArticlesAndBlogs.Type));
                        var task = Task.Run(async () =>
                        {
                            return await ret;
                        });
                        int count = task.Result.Count;
                        if (count < 1)
                        {
                            ArticlesAndBlogsRepo.Add(_ArticlesAndBlogs);
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetYoutubeSearchData = > " + ex.Message);
            }

            return "Successfully updated";
        }
       
        public string WebRequst(string Url)
        {

            try
            {
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpRequest.Method = "GET";
                httpRequest.ContentType = "application/json; charset=utf-8";
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
                logger.Error(ex.StackTrace);
                logger.Error(ex.Message);
                return "";
            }
        }

    }
}
