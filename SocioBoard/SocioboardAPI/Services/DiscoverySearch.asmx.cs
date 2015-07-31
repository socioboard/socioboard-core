using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using GlobusTwitterLib.Authentication;
using GlobusTwitterLib.Twitter.Core.SearchMethods;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using GlobusTwitterLib.Twitter.Core.UserMethods;
using log4net;
using Ionic.Zlib;
using Facebook;
//using System.IO.Compression;

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
    public class DiscoverySearch : System.Web.Services.WebService
    {
        ILog logger = LogManager.GetLogger(typeof(DiscoverySearch)); 
        Domain.Socioboard.Domain.DiscoverySearch objDiscoverySearch = new Domain.Socioboard.Domain.DiscoverySearch();
        DiscoverySearchRepository dissearchrepo = new DiscoverySearchRepository();
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string DiscoverySearchFacebook(string UserId, string keyword)
        {
            List<Domain.Socioboard.Domain.DiscoverySearch> lstDiscoverySearch = new List<Domain.Socioboard.Domain.DiscoverySearch>();
            string profileid = string.Empty;
            try
            {
                ////  lstDiscoverySearch = dissearchrepo.GetAllSearchKeywordsByUserId(Guid.Parse(UserId), keyword, "facebook");

                //FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
                //List<Domain.Socioboard.Domain.FacebookAccount> asltFbAccount = fbAccRepo.getFbAccounts();
              
                //#region Added Sumit Gupta [27/01/15]
                //string accesstoken = string.Empty;
                //foreach (Domain.Socioboard.Domain.FacebookAccount item in asltFbAccount)
                //{
                //    try
                //    {
                //        FacebookClient fb = new FacebookClient();
                //        fb.AccessToken = item.AccessToken;

                //        dynamic me = fb.Get("v2.0/me");
                //        string id = me["id"].ToString();
                //        accesstoken = item.AccessToken;
                //        break;
                //    }
                //    catch (Exception ex)
                //    {

                //    }

                //}
                //#endregion

                //////Access Token HARD CODED temporarily
                ////accesstoken = "CAAKMrAl97iIBAD9MqfWtfjIxwFVteGCLVZBsoHpc1TZCH8Kf3KQuMebkbNYLb282cUTisu6iGZBiZAzzwxWvDhh20vCzs5mZCFZBblZBXu40BQisUjoOCZARUQklHBiK3Cx7DOgdXtbvupC4xJ1VpPjKspwiZBRzNYncjgQAyUqd5sGsXUDHcqKy0UBYkmbfq7QZCFgpyG5icOPeMhRb4TXJaic7UF7B1WHLhw2A5EW0kb3AZDZD";

                //string facebookSearchUrl = "https://graph.facebook.com/search?q=" + keyword + " &type=post&access_token=" + accesstoken + "&limit=100";
                //var facerequest = (HttpWebRequest)WebRequest.Create(facebookSearchUrl);
                //facerequest.Method = "GET";
                //string outputface = string.Empty;
                //using (var response = facerequest.GetResponse())
                //{
                //    using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                //    {
                //        outputface = stream.ReadToEnd();
                //    }
                //}
                //if (!outputface.StartsWith("["))
                //    outputface = "[" + outputface + "]";
                //JArray facebookSearchResult = JArray.Parse(outputface);
                //foreach (var item in facebookSearchResult)
                //{
                //    var data = item["data"];

                //    foreach (var chile in data)
                //    {
                //        try
                //        {
                //            objDiscoverySearch = new Domain.Socioboard.Domain.DiscoverySearch();
                //            objDiscoverySearch.SearchKeyword = keyword;
                //            objDiscoverySearch.Network = "facebook";
                //            objDiscoverySearch.Id = Guid.NewGuid();
                //            objDiscoverySearch.UserId = Guid.Parse(UserId);

                //            if (!dissearchrepo.isKeywordPresentforNetwork(objDiscoverySearch.SearchKeyword, objDiscoverySearch.Network))
                //            {
                //                dissearchrepo.addNewSearchResult(objDiscoverySearch);
                //            }
                //        }
                //        catch (Exception ex)
                //        {
                //            Console.WriteLine(ex.StackTrace);
                //        }
                //        try
                //        {
                //            Domain.Socioboard.Domain.DiscoverySearch objSearchHistory = new Domain.Socioboard.Domain.DiscoverySearch();
                //            objSearchHistory.CreatedTime = DateTime.Parse(chile["created_time"].ToString());
                //            objSearchHistory.EntryDate = DateTime.Now;
                //            objSearchHistory.FromId = chile["from"]["id"].ToString();
                //            try
                //            {
                //                objSearchHistory.FromName = chile["from"]["name"].ToString();
                //            }
                //            catch { }
                //            try
                //            {
                //                objSearchHistory.ProfileImageUrl = "http://graph.facebook.com/" + chile["from"]["id"] + "/picture?type=small";
                //            }
                //            catch { }
                //            objSearchHistory.SearchKeyword = keyword;
                //            objSearchHistory.Network = "facebook";
                //            try
                //            {
                //                objSearchHistory.Message = chile["message"].ToString();
                //            }
                //            catch { }
                //            try
                //            {
                //                objSearchHistory.MessageId = chile["id"].ToString();
                //            }
                //            catch { }
                //            objSearchHistory.Id = Guid.NewGuid();
                //            objSearchHistory.UserId = Guid.Parse(UserId);
                //            lstDiscoverySearch.Add(objSearchHistory);
                //        }
                //        catch { }
                //    }
                //}
                List<Domain.Socioboard.Domain.DiscoverySearch> fb_data = null;

                return new JavaScriptSerializer().Serialize(fb_data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }
        }

        #region Commented by Antima
        //public string DiscoverySearchFacebook(string UserId, string keyword)
        //{
        //    List<Domain.Socioboard.Domain.DiscoverySearch> lstDiscoverySearch = new List<Domain.Socioboard.Domain.DiscoverySearch>();
        //    string profileid = string.Empty;
        //    try
        //    {
        //        lstDiscoverySearch = dissearchrepo.GetAllSearchKeywordsByUserId(Guid.Parse(UserId), keyword, "facebook");
        //        if (lstDiscoverySearch.Count < 1)
        //        {



        //            FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
        //            ArrayList asltFbAccount = fbAccRepo.getAllFacebookAccounts();
        //            string accesstoken = string.Empty;
        //            //foreach (Domain.Socioboard.Domain.FacebookAccount item in asltFbAccount)
        //            //{
        //            //    accesstoken = item.AccessToken;
        //            //    if (this.CheckFacebookToken(accesstoken, keyword))
        //            //    {

        //            //        break;
        //            //    }
        //            //}

        //            //Access Token HARD CODED temporarily
        //            accesstoken = "CAAKMrAl97iIBAD9MqfWtfjIxwFVteGCLVZBsoHpc1TZCH8Kf3KQuMebkbNYLb282cUTisu6iGZBiZAzzwxWvDhh20vCzs5mZCFZBblZBXu40BQisUjoOCZARUQklHBiK3Cx7DOgdXtbvupC4xJ1VpPjKspwiZBRzNYncjgQAyUqd5sGsXUDHcqKy0UBYkmbfq7QZCFgpyG5icOPeMhRb4TXJaic7UF7B1WHLhw2A5EW0kb3AZDZD";

        //            string facebookSearchUrl = "https://graph.facebook.com/search?q=" + keyword + " &type=post&access_token=" + accesstoken + "&limit=100";
        //            var facerequest = (HttpWebRequest)WebRequest.Create(facebookSearchUrl);
        //            facerequest.Method = "GET";
        //            string outputface = string.Empty;
        //            using (var response = facerequest.GetResponse())
        //            {
        //                using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
        //                {
        //                    outputface = stream.ReadToEnd();
        //                }
        //            }
        //            if (!outputface.StartsWith("["))
        //                outputface = "[" + outputface + "]";
        //            JArray facebookSearchResult = JArray.Parse(outputface);
        //            foreach (var item in facebookSearchResult)
        //            {
        //                var data = item["data"];

        //                foreach (var chile in data)
        //                {
        //                    try
        //                    {
        //                        objDiscoverySearch = new Domain.Socioboard.Domain.DiscoverySearch();
        //                        objDiscoverySearch.CreatedTime = DateTime.Parse(chile["created_time"].ToString());
        //                        objDiscoverySearch.EntryDate = DateTime.Now;
        //                        objDiscoverySearch.FromId = chile["from"]["id"].ToString();
        //                        try
        //                        {
        //                            objDiscoverySearch.FromName = chile["from"]["name"].ToString();
        //                        }
        //                        catch { }
        //                        try
        //                        {
        //                            objDiscoverySearch.ProfileImageUrl = "http://graph.facebook.com/" + chile["from"]["id"] + "/picture?type=small";
        //                        }
        //                        catch { }
        //                        objDiscoverySearch.SearchKeyword = keyword;
        //                        objDiscoverySearch.Network = "facebook";
        //                        try
        //                        {
        //                            objDiscoverySearch.Message = chile["message"].ToString();
        //                        }
        //                        catch { }
        //                        try
        //                        {
        //                            objDiscoverySearch.MessageId = chile["id"].ToString();
        //                        }
        //                        catch { }
        //                        objDiscoverySearch.Id = Guid.NewGuid();
        //                        objDiscoverySearch.UserId = Guid.Parse(UserId);

        //                        string postURL = "https://www.facebook.com/" + objDiscoverySearch.MessageId;

        //                        if (!dissearchrepo.isKeywordPresent(objDiscoverySearch.SearchKeyword, objDiscoverySearch.MessageId))
        //                        {
        //                            dissearchrepo.addNewSearchResult(objDiscoverySearch);
        //                        }
        //                        lstDiscoverySearch.Add(objDiscoverySearch);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Console.WriteLine(ex.StackTrace);
        //                    }
        //                }
        //            }

        //            return new JavaScriptSerializer().Serialize(lstDiscoverySearch);
        //        }
        //        else
        //        {
        //            return new JavaScriptSerializer().Serialize(lstDiscoverySearch);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //        return new JavaScriptSerializer().Serialize("Please try Again");
        //    }
        //}
        #endregion

        public bool CheckFacebookToken(string fbtoken, string txtvalue)
        {
            bool checkFacebookToken = false;
            try
            {
                string facebookSearchUrl = "https://graph.facebook.com/search?q=" + txtvalue + " &type=post&access_token=" + fbtoken;
                var facerequest = (HttpWebRequest)WebRequest.Create(facebookSearchUrl);
                facerequest.Method = "GET";
                string outputface = string.Empty;
                using (var response = facerequest.GetResponse())
                {
                    using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                    {
                        outputface = stream.ReadToEnd();
                    }
                }
                checkFacebookToken = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return checkFacebookToken;
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string DiscoverySearchTwitter(string UserId, string keyword)
        {
            List<Domain.Socioboard.Domain.DiscoverySearch> lstDiscoverySearch = new List<Domain.Socioboard.Domain.DiscoverySearch>();
            string profileid = string.Empty;
            try
            {
                oAuthTwitter oauth = new oAuthTwitter();
                Twitter obj = new Twitter();

                TwitterAccountRepository twtAccRepo = new TwitterAccountRepository();
                List<Domain.Socioboard.Domain.TwitterAccount> alst = twtAccRepo.getTwtAccount();
                foreach (Domain.Socioboard.Domain.TwitterAccount item in alst)
                {
                    oauth.AccessToken = item.OAuthToken;
                    oauth.AccessTokenSecret = item.OAuthSecret;
                    oauth.TwitterUserId = item.TwitterUserId;
                    oauth.TwitterScreenName = item.TwitterScreenName;
                    obj.SetCofigDetailsForTwitter(oauth);
                    try
                    {
                        Users _Users = new Users();
                        JArray _AccountVerify = _Users.Get_Account_Verify_Credentials(oauth);
                        string id = _AccountVerify["id_str"].ToString();
                        break;
                    }
                    catch (Exception ex)
                    {

                    }
                    //if (this.CheckTwitterToken(oauth, keyword))
                    //{
                    //    break;
                    //}
                }

                Search search = new Search();
                JArray twitterSearchResult = search.Get_Search_Tweets(oauth, keyword);
                foreach (var item in twitterSearchResult)
                {
                    var results = item["statuses"];
                    foreach (var chile in results)
                    {
                        try
                        {
                            objDiscoverySearch = new Domain.Socioboard.Domain.DiscoverySearch();
                            objDiscoverySearch.SearchKeyword = keyword;
                            objDiscoverySearch.Network = "twitter";
                            objDiscoverySearch.Id = Guid.NewGuid();
                            objDiscoverySearch.UserId = Guid.Parse(UserId);

                            if (!dissearchrepo.isKeywordPresentforNetwork(objDiscoverySearch.SearchKeyword, objDiscoverySearch.Network))
                            {
                                dissearchrepo.addNewSearchResult(objDiscoverySearch);
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }

                        try
                        {
                            objDiscoverySearch = new Domain.Socioboard.Domain.DiscoverySearch();
                            objDiscoverySearch.CreatedTime = Utility.ParseTwitterTime(chile["created_at"].ToString().TrimStart('"').TrimEnd('"')); ;
                            objDiscoverySearch.EntryDate = DateTime.Now;
                            objDiscoverySearch.FromId = chile["user"]["id_str"].ToString().TrimStart('"').TrimEnd('"');
                            objDiscoverySearch.FromName = chile["user"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
                            objDiscoverySearch.ProfileImageUrl = chile["user"]["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
                            objDiscoverySearch.SearchKeyword = keyword;
                            objDiscoverySearch.Network = "twitter";
                            objDiscoverySearch.Message = chile["text"].ToString().TrimStart('"').TrimEnd('"');
                            objDiscoverySearch.MessageId = chile["id_str"].ToString().TrimStart('"').TrimEnd('"');
                            objDiscoverySearch.Id = Guid.NewGuid();
                            objDiscoverySearch.UserId = Guid.Parse(UserId);

                            lstDiscoverySearch.Add(objDiscoverySearch);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                }
                return new JavaScriptSerializer().Serialize(lstDiscoverySearch);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string DiscoverySearchGplus(string UserId, string keyword)
        {
            List<Domain.Socioboard.Domain.DiscoverySearch> GplusDiscoverySearch = new List<Domain.Socioboard.Domain.DiscoverySearch>();
            string profileid = string.Empty;
            try
            {
                string searchResultObj = GplusDiscoverySearchHelper.GooglePlus(keyword);
                GooglePlusAccountRepository gplusAccRepo = new GooglePlusAccountRepository();
                ArrayList alst = gplusAccRepo.getAllGooglePlusAccounts();

                GlobusGooglePlusLib.Authentication.oAuthToken oauth = new GlobusGooglePlusLib.Authentication.oAuthToken();
                GooglePlusActivities obj = new GooglePlusActivities();

                JObject GplusActivities = JObject.Parse(GplusDiscoverySearchHelper.GooglePlus(keyword));

                foreach (JObject gobj in JArray.Parse(GplusActivities["items"].ToString()))
                {
                    Domain.Socioboard.Domain.DiscoverySearch gpfeed = new Domain.Socioboard.Domain.DiscoverySearch();
                    gpfeed.Id = Guid.NewGuid();

                    try
                    {
                        gpfeed.MessageId = gobj["url"].ToString();
                        gpfeed.CreatedTime = DateTime.Parse(gobj["published"].ToString());
                        gpfeed.Message = gobj["title"].ToString();
                        gpfeed.FromId = gobj["actor"]["id"].ToString();
                        gpfeed.FromName = gobj["actor"]["displayName"].ToString();
                        gpfeed.ProfileImageUrl = gobj["actor"]["image"]["url"].ToString();
                    }
                    catch { }

                    GplusDiscoverySearch.Add(gpfeed);
                }
            }
            catch { }
            return new JavaScriptSerializer().Serialize(GplusDiscoverySearch);
        }

        #region Commented by Antima
        //public string DiscoverySearchTwitter(string UserId, string keyword)
        //{
        //    List<Domain.Socioboard.Domain.DiscoverySearch> lstDiscoverySearch = new List<Domain.Socioboard.Domain.DiscoverySearch>();
        //    string profileid = string.Empty;
        //    try
        //    {

        //        oAuthTwitter oauth = new oAuthTwitter();
        //        Twitter obj = new Twitter();

        //        TwitterAccountRepository twtAccRepo = new TwitterAccountRepository();
        //        ArrayList alst = twtAccRepo.getAllTwitterAccounts();
        //        foreach (Domain.Socioboard.Domain.TwitterAccount item in alst)
        //        {
        //            oauth.AccessToken = item.OAuthToken;
        //            oauth.AccessTokenSecret = item.OAuthSecret;
        //            oauth.TwitterUserId = item.TwitterUserId;
        //            oauth.TwitterScreenName = item.TwitterScreenName;
        //            obj.SetCofigDetailsForTwitter(oauth);
        //            if (this.CheckTwitterToken(oauth, keyword))
        //            {
        //                break;
        //            }
        //        }

        //        Search search = new Search();
        //        JArray twitterSearchResult = search.Get_Search_Tweets(oauth, keyword);
        //        foreach (var item in twitterSearchResult)
        //        {
        //            var results = item["statuses"];
        //            foreach (var chile in results)
        //            {
        //                try
        //                {
        //                    objDiscoverySearch = new Domain.Socioboard.Domain.DiscoverySearch();
        //                    objDiscoverySearch.CreatedTime = Utility.ParseTwitterTime(chile["created_at"].ToString().TrimStart('"').TrimEnd('"')); ;
        //                    objDiscoverySearch.EntryDate = DateTime.Now;
        //                    objDiscoverySearch.FromId = chile["user"]["id_str"].ToString().TrimStart('"').TrimEnd('"');
        //                    objDiscoverySearch.FromName = chile["user"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
        //                    objDiscoverySearch.ProfileImageUrl = chile["user"]["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
        //                    objDiscoverySearch.SearchKeyword = keyword;
        //                    objDiscoverySearch.Network = "twitter";
        //                    objDiscoverySearch.Message = chile["text"].ToString().TrimStart('"').TrimEnd('"');
        //                    objDiscoverySearch.MessageId = chile["id_str"].ToString().TrimStart('"').TrimEnd('"');
        //                    objDiscoverySearch.Id = Guid.NewGuid();
        //                    objDiscoverySearch.UserId = Guid.Parse(UserId);

        //                    string postID = chile["id"].ToString();
        //                    string postURL = "https://twitter.com/" + objDiscoverySearch.FromName + "/status/" + postID;

        //                    if (!dissearchrepo.isKeywordPresent(objDiscoverySearch.SearchKeyword, objDiscoverySearch.MessageId))
        //                    {
        //                        dissearchrepo.addNewSearchResult(objDiscoverySearch);
        //                    }
        //                    lstDiscoverySearch.Add(objDiscoverySearch);
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine(ex.StackTrace);
        //                }
        //            }
        //        }

        //        return new JavaScriptSerializer().Serialize(lstDiscoverySearch);

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //        return new JavaScriptSerializer().Serialize("Please try Again");
        //    }
        //}
        #endregion

        public bool CheckTwitterToken(oAuthTwitter objoAuthTwitter, string txtvalue)
        {
            bool CheckTwitterToken = false;
            //oAuthTwitter oAuthTwt = new oAuthTwitter();
            Search search = new Search();

            try
            {
                JArray twitterSearchResult = search.Get_Search_Tweets(objoAuthTwitter, txtvalue);
                CheckTwitterToken = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return CheckTwitterToken;
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string contactSearchFacebook(string keyword)
        {
            List<Domain.Socioboard.Domain.DiscoverySearch> lstDiscoverySearch = new List<Domain.Socioboard.Domain.DiscoverySearch>();
            string profileid = string.Empty;
            try
            {
                FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
                List<Domain.Socioboard.Domain.FacebookAccount> asltFbAccount = fbAccRepo.getFbAccounts();
                string accesstoken = string.Empty;
                foreach (Domain.Socioboard.Domain.FacebookAccount item in asltFbAccount)
                {
                    try
                    {
                        FacebookClient fb = new FacebookClient();
                        fb.AccessToken = item.AccessToken;

                        dynamic me = fb.Get("v2.0/me");
                        string id = me["id"].ToString();
                        accesstoken = item.AccessToken;
                        break;
                    }
                    catch (Exception ex)
                    {

                    }
                     
                }
                //string facebookSearchUrl = "https://graph.facebook.com/search?q=" + keyword + " &type=post&access_token=" + accesstoken + "&limit=100";
                string facebookSearchUrl = "https://graph.facebook.com/search?q=" + keyword + " &limit=20&type=user&access_token=" + accesstoken;
                var facerequest = (HttpWebRequest)WebRequest.Create(facebookSearchUrl);
                facerequest.Method = "GET";
                string outputface = string.Empty;
                using (var response = facerequest.GetResponse())
                {
                    using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                    {
                        outputface = stream.ReadToEnd();
                    }
                }
                if (!outputface.StartsWith("["))
                    outputface = "[" + outputface + "]";
                JArray facebookSearchResult = JArray.Parse(outputface);
                foreach (var item in facebookSearchResult)
                {
                    var data = item["data"];

                    foreach (var chile in data)
                    {
                        try
                        {
                            objDiscoverySearch = new Domain.Socioboard.Domain.DiscoverySearch();
                            objDiscoverySearch.FromId = chile["id"].ToString();
                            objDiscoverySearch.FromName = chile["name"].ToString();
                            lstDiscoverySearch.Add(objDiscoverySearch);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                }

                return new JavaScriptSerializer().Serialize(lstDiscoverySearch);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string contactSearchTwitter(string keyword)
        {
            List<Domain.Socioboard.Domain.DiscoverySearch> lstDiscoverySearch = new List<Domain.Socioboard.Domain.DiscoverySearch>();
            string profileid = string.Empty;
            try
            {

                oAuthTwitter oauth = new oAuthTwitter();
                Twitter obj = new Twitter();

                TwitterAccountRepository twtAccRepo = new TwitterAccountRepository();
                //ArrayList alst = twtAccRepo.getAllTwitterAccounts();
                List<Domain.Socioboard.Domain.TwitterAccount> alst = twtAccRepo.getTwtAccount();
                foreach (Domain.Socioboard.Domain.TwitterAccount item in alst)
                {
                    oauth.AccessToken = item.OAuthToken;
                    oauth.AccessTokenSecret = item.OAuthSecret;
                    oauth.TwitterUserId = item.TwitterUserId;
                    oauth.TwitterScreenName = item.TwitterScreenName;
                    obj.SetCofigDetailsForTwitter(oauth);
                    try
                    {
                        Users _Users = new Users();
                        JArray _AccountVerify = _Users.Get_Account_Verify_Credentials(oauth);
                        string id = _AccountVerify["id_str"].ToString();
                        break;
                    }
                    catch (Exception ex)
                    {
                       
                    }

                    //if (this.CheckTwitterToken(oauth, keyword))
                    //{
                    //    break;
                    //}
                }

                Users twtUser = new Users();
                JArray twitterSearchResult = twtUser.Get_Users_Search(oauth, keyword, "20");
                foreach (var item in twitterSearchResult)
                {
                    try
                    {
                        objDiscoverySearch = new Domain.Socioboard.Domain.DiscoverySearch();
                        objDiscoverySearch.FromId = item["screen_name"].ToString();
                        objDiscoverySearch.FromName = item["screen_name"].ToString();
                        objDiscoverySearch.SearchKeyword = keyword;
                        lstDiscoverySearch.Add(objDiscoverySearch);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                }


                return new JavaScriptSerializer().Serialize(lstDiscoverySearch);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }
        }

        // Edited by Antima

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllSearchKeywords(string UserId)
        {
            try
            {
                List<string> objdiscoverysearch = dissearchrepo.getAllSearchKeywords(Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(objdiscoverysearch);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        //Added by vikas [19-05-2015]

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string TwitterLinkBuilder(string q)
        {
            string ret = string.Empty;
            JArray output = new JArray();
            SortedDictionary<string, string> requestParameters = new SortedDictionary<string, string>();
            try
            {
                var oauth_url = " https://api.twitter.com/1.1/search/tweets.json?q=" + q.Trim() + "&result_type=recent";
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
                List<string> _lst = new List<string>();
                foreach (var item in output)
                {
                    try
                    {
                        string _urls = item["entities"]["urls"][0]["expanded_url"].ToString();
                        if (!string.IsNullOrEmpty(_urls))
                            _lst.Add(_urls);
                    }
                    catch { }
                }

                ret = new JavaScriptSerializer().Serialize(_lst);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                ret = "";
            }

            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GPlusLinkBuilder(string q)
        {
            string ret = string.Empty;
            string response = string.Empty;
            JArray output = new JArray();
            string Key = "AIzaSyCISaVFe_TJknn92J7xw2diFEi6Z_aroYE";
            string RequestUrl = " https://www.googleapis.com/plus/v1/activities?orderBy=recent&query=" + q + "&alt=json&key=" + Key;
            try
            {
                var gpluspagerequest = (HttpWebRequest)WebRequest.Create(RequestUrl);
                gpluspagerequest.Method = "GET";
                try
                {
                    using (var gplusresponse = gpluspagerequest.GetResponse())
                    {
                        using (var stream = new StreamReader(gplusresponse.GetResponseStream(), Encoding.GetEncoding(1252)))
                        {
                            response = stream.ReadToEnd();
                            output = JArray.Parse(JObject.Parse(response)["items"].ToString());
                        }
                    }
                    List<string> _lst = new List<string>();
                    foreach (var item in output)
                    {
                        try
                        {
                            string _urls = item["object"]["attachments"][0]["url"].ToString();
                            if (!string.IsNullOrEmpty(_urls) && !_urls.StartsWith("https://plus.google.com"))
                                _lst.Add(_urls);
                        }
                        catch { }
                    }
                    ret = new JavaScriptSerializer().Serialize(_lst);

                }
                catch (Exception e) { }
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                ret = "";
            }
            return ret;
        }
    }
}
