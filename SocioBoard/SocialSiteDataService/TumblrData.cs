using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocioBoard.Helper;
using SocioBoard.Domain;
using SocioBoard.Model;
using System.Collections;
using Newtonsoft.Json.Linq;
using GlobusTumblrLib.Authentication;
using GlobusTumblerLib.App.Core;
using System.Text.RegularExpressions;

namespace SocialSiteDataService
{
    public class TumblrData : SocialSiteDataFeeds
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId">Tumblr User id</param>
        public void GetData(object UserId)
        {
            Guid userId = (Guid)UserId;

            TumblrFeed objTumblrFeed = new TumblrFeed();
            TumblrFeedRepository objTumblrFeedRepository = new TumblrFeedRepository();

            //LinkedInHelper objliHelper = new LinkedInHelper();
            //LinkedInAccountRepository objLiRepo = new LinkedInAccountRepository();

            TumblrAccountRepository objLiRepo = new TumblrAccountRepository();

            //oAuthLinkedIn _oauth = new oAuthLinkedIn();
            List<TumblrAccount> arrTumbAccount = objLiRepo.getTumblrAccountDetailsById(userId);


            foreach (TumblrAccount itemLi in arrTumbAccount)
            {
                //_oauth.Token = itemLi.OAuthToken;
                //_oauth.TokenSecret = itemLi.OAuthSecret;
                //_oauth.Verifier = itemLi.OAuthVerifier;
                //objliHelper.GetUserProfile(_oauth, itemLi.LinkedinUserId, userId);

                //objliHelper.GetLinkedInFeeds(_oauth, itemLi.LinkedinUserId, userId);


                KeyValuePair<string, string> LoginDetails = new KeyValuePair<string, string>(itemLi.tblrAccessToken, itemLi.tblrAccessTokenSecret);


                string sstr = oAuthTumbler.OAuthData(Globals.UsersDashboardUrl, "GET", LoginDetails.Key, LoginDetails.Value, null);

                JObject profile = JObject.Parse(oAuthTumbler.OAuthData(Globals.UsersInfoUrl, "GET", LoginDetails.Key, LoginDetails.Value, null));
                JObject UserDashboard = JObject.Parse(oAuthTumbler.OAuthData(Globals.UsersDashboardUrl, "GET", LoginDetails.Key, LoginDetails.Value, null));


                JArray objJarray = (JArray)UserDashboard["response"]["posts"];

               
                foreach (var item in objJarray)
                {
                    objTumblrFeed.Id = Guid.NewGuid();
                    objTumblrFeed.UserId = userId;
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
                        JArray obj = (JArray)item["photos"];
                        foreach (var item1 in obj)
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
            }

        }

        public void GetSearchData(object parameters)
        {
            throw new NotImplementedException();
        }
    }
}
