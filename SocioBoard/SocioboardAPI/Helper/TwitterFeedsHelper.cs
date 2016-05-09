using Api.Socioboard.Model;
using Api.Socioboard.Model.MongoModels;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Api.Socioboard.Helper
{
    public static class TwitterFeedsHelper
    {
       

        public static JArray getAllDataWithHashtag(string hasttagname, Api.Socioboard.Model.BoardRepository Boardrepo, Guid BaordTwitterId)
        {

            if (hasttagname.Contains("#"))
            {
                hasttagname = hasttagname.Replace("#", "%23");
            }
            string url = "https://twitter.com/hashtag/" + hasttagname + "?src=tren";
            GlobusHttpHelper globushelper = new GlobusHttpHelper();


            JArray dataArray = new JArray();

            # region scraping data

            string Name_From = string.Empty;
            string Id_From = string.Empty;
            string Create_edat = string.Empty;
            string Imageurl = string.Empty;
            string Text = string.Empty;
            string Retweetcount = string.Empty;
            string Favoritedcount = string.Empty;
            string FeedId = string.Empty;
            string FeedUrl = string.Empty;
            // string Name_From = string.Empty;
            int exitcount = 0;

            string page = globushelper.getHtmlfromUrl(new Uri(url), "", "");

            List<string> dataList = new List<string>();






            //if (page.Contains("class=\"tweet original-tweet js-stream-tweet js-actionable-tweet js-profile-popup-actionable js-original-tweet")) //js-stream-item stream-item stream-item expanding-stream-item
            if (page.Contains("js-stream-item stream-item stream-item expanding-stream-item"))
            {

                string[] listPage = Regex.Split(page, "js-stream-item stream-item stream-item expanding-stream-item");

                List<string> listPageList = listPage.ToList();
                listPageList.RemoveAt(0);


                foreach (string item in listPageList)
                {

                    if (item.Contains("!DOCTYPE html"))
                    {

                        continue;
                    }


                    try
                    {
                        string[] Name_FromList = Regex.Split(item, "fullname js-action-profile-name show-popup-with-id");

                        Name_From = Utils.getBetween(Name_FromList[1], ">", "<");
                    }
                    catch { };


                    try
                    {
                        string[] Name_FromList = Regex.Split(item, "account-group js-account-group js-action-profile js-user-profile-link js-nav");

                        Id_From = Utils.getBetween(Name_FromList[1], "data-user-id=\"", "\"");
                    }
                    catch { };




                    try
                    {
                        //string[] Name_FromList = Regex.Split(item, "_timestamp js-short-timestamp js-relative-timestamp");  //tweet-timestamp js-permalink js-nav js-tooltip
                        //  Create_edat = Utils.getBetween(Name_FromList[1], ">", "<");
                        string[] Name_FromList = Regex.Split(item, "tweet-timestamp js-permalink js-nav js-tooltip");

                        Create_edat = Utils.getBetween(Name_FromList[1], "title=\"", "\"");
                    }
                    catch { };



                    try
                    {
                        //data-src="

                        // string[] Name_FromList = Regex.Split(item, "class=\" is-preview\"");

                        Imageurl = Utils.getBetween(item, "data-src=\"", "\"");
                        if (!string.IsNullOrEmpty(Imageurl))
                        {
                            Imageurl = "https://twitter.com" + Imageurl;
                        }
                        else
                        {
                            Imageurl = Utils.getBetween(item, "data-img-src=\"", "\"");

                        }

                    }
                    catch { };

                    try
                    {
                        string[] Name_FromList = Regex.Split(item, "class=\"TweetTextSize  js-tweet-text tweet-text\"");



                        Text = Utils.getBetween(Name_FromList[1], ">", "</p>");
                        int count = 0;

                        if (Text.Contains("<a "))
                        {
                            try
                            {
                                count = Regex.Split(Text, "<a").Count();
                            }
                            catch { };
                            for (int i = 0; i < count; i++)
                            {
                                try
                                {
                                    if (Text.Contains("<a "))
                                    {
                                        string ReplaceInBetween = Utils.getBetween(Text, "<a ", "</a>");
                                        ReplaceInBetween = "<a " + ReplaceInBetween + "</a>";
                                        Text = Text.Replace(ReplaceInBetween, "");
                                    }
                                }
                                catch { };
                            }
                        }

                        if (Text.Contains("<img"))
                        {

                            try
                            {
                                count = Regex.Split(Text, "<img").Count();
                            }
                            catch { };
                            for (int i = 0; i < count; i++)
                            {
                                try
                                {
                                    if (Text.Contains("<img "))
                                    {
                                        string ReplaceInBetween = Utils.getBetween(Text, "<img ", ">");
                                        ReplaceInBetween = "<img " + ReplaceInBetween + ">";
                                        Text = Text.Replace(ReplaceInBetween, "");
                                    }
                                }
                                catch { };
                            }
                        }





                        if (Text.Contains("<a "))
                        {
                            string ReplaceInBetween = Utils.getBetween(Text, "<a ", "</a>");
                            ReplaceInBetween = "<a " + ReplaceInBetween + "</a>";
                            Text = Text.Replace(ReplaceInBetween, "").Replace("&", "").Replace("#", "").Replace(";", "");
                        }
                        Text = Text.Replace("&", "").Replace("#", "").Replace(";", "");


                    }
                    catch { };

                    try
                    {
                        //Retweet
                        string[] Name_FromListFirst = Regex.Split(item, ">Retweet<");

                        string[] Name_FromList = Regex.Split(Name_FromListFirst[1], "class=\"ProfileTweet-actionCountForPresentation\"");
                        Retweetcount = Utils.getBetween(Name_FromList[1], ">", "<");
                    }
                    catch { };


                    try
                    {
                        //>Favorite<
                        string[] Name_FromListFirst = Regex.Split(item, ">Favorite<");
                        string[] Name_FromList = Regex.Split(Name_FromListFirst[1], "class=\"ProfileTweet-actionCountForPresentation\"");
                        Favoritedcount = Utils.getBetween(Name_FromList[1], ">", "<");
                    }
                    catch { };


                    try
                    {
                        //  string[] Name_FromList = Regex.Split(item, "class=\"ProfileTweet-actionCountForPresentation\"");
                        FeedId = Utils.getBetween(item, "data-tweet-id=\"", "\"");
                    }
                    catch { };


                    try
                    {
                        //  string[] Name_FromList = Regex.Split(item, "class=\"ProfileTweet-actionCountForPresentation\"");
                        FeedUrl = Utils.getBetween(item, "data-permalink-path=\"", "\"");
                        FeedUrl = "https://twitter.com" + FeedUrl;
                    }
                    catch { };

                    string data = "Name_From : " + Name_From + "                 +                 " + "Id_From : " + Id_From + "                 +                 " + "Create_edat : " + Create_edat + "                 +                 " + "Imageurl : " + Imageurl + "                 +                 " + "Text : " + Text + "                 +                 " + "Retweetcount : " + Retweetcount + "                 +                 " + "Favoritedcount : " + Favoritedcount + "                 +                 " + "FeedId : " + FeedId + "                 +                 " + "FeedUrl : " + FeedUrl;
                    dataList.Add(data);


                    JObject feedobj = new JObject();
                    try
                    {
                        string[] datearry = Create_edat.Split(' ');
                        Create_edat = DateTime.Parse(datearry[3] + " " + datearry[4] + " " + datearry[5] + " " + datearry[0] + " " + datearry[1]).ToString();
                    }
                    catch { }
                    feedobj.Add("Name_From", Name_From);
                    feedobj.Add("Id_From", Id_From);
                    feedobj.Add("Create_edat", Create_edat);
                    feedobj.Add("Imageurl", Imageurl);
                    feedobj.Add("Text", Text);
                    feedobj.Add("Retweetcount", Retweetcount);
                    feedobj.Add("Favoritedcount", Favoritedcount);
                    feedobj.Add("FeedId", FeedId);
                    feedobj.Add("FeedUrl", FeedUrl);
                    dataArray.Add(feedobj);



                    Domain.Socioboard.Domain.Boardtwitterfeeds twitterfeed = new Domain.Socioboard.Domain.Boardtwitterfeeds();
                    twitterfeed.Id = Guid.NewGuid();
                    twitterfeed.FromName = feedobj["Name_From"].ToString();
                    twitterfeed.FromId = feedobj["Id_From"].ToString();
                    twitterfeed.FromPicUrl = feedobj["Imageurl"].ToString();
                    twitterfeed.Text = feedobj["Text"].ToString();
                    twitterfeed.Feedid = feedobj["FeedId"].ToString();
                    twitterfeed.Twitterprofileid = BaordTwitterId;
                    twitterfeed.Isvisible = true;
                    try
                    {
                        twitterfeed.Createdat = DateTime.Parse(feedobj["Create_edat"].ToString());
                    }
                    catch { }
                    try
                    {
                        twitterfeed.Retweetcount = Convert.ToInt32(feedobj["Retweetcount"].ToString());
                    }
                    catch { }
                    try
                    {
                        twitterfeed.Favoritedcount = Convert.ToInt32(feedobj["Favoritedcount"].ToString());
                    }
                    catch { }
                    if (!Boardrepo.checkTwitterFeedExists(twitterfeed.Feedid, BaordTwitterId))
                    {
                        //twtFeedsList.Add(twitterfeed);
                        Boardrepo.addBoardTwitterFeed(twitterfeed);
                    }
                    exitcount++;

                    // "https://twitter.com/hashtag/" + hasttagname + "?src=tren"

                }


            }


            try
            {

                string paginationdataurl = Utils.getBetween(page, "data-max-position=\"", "\"");

                string paginationUrl = "https://twitter.com/i/search/timeline?q=" + hasttagname + "&src=typd&vertical=default&include_available_features=1&include_entities=1&max_position=" + paginationdataurl;



                while (exitcount <500)
                {

                    page = globushelper.getHtmlfromUrl(new Uri(paginationUrl), "", "");


                    if (!page.Contains("has_more_items"))
                    {

                        return dataArray;

                    }




                    try
                    {



                        //js-stream-item stream-item stream-item expanding-stream-item


                        if (page.Contains("js-stream-item stream-item stream-item expanding-stream-item"))
                        {

                            string[] listPage = Regex.Split(page, "js-stream-item stream-item stream-item expanding-stream-item");

                            List<string> listPageList = listPage.ToList();
                            listPageList.RemoveAt(0);


                            foreach (string item in listPageList)
                            {

                                if (item.Contains("!DOCTYPE html"))
                                {

                                    continue;
                                }


                                try
                                {
                                    // string[] Name_FromList = Regex.Split(item, "fullname js-action-profile-name show-popup-with-id");

                                    Name_From = Utils.getBetween(item, "data-name=\\\"", "\\\"");
                                }
                                catch { };


                                try
                                {
                                    string[] Name_FromList = Regex.Split(item, "account-group js-account-group js-action-profile js-user-profile-link js-nav");

                                    Id_From = Utils.getBetween(item, "data-user-id=\\\"", "\\\"");
                                }
                                catch { };




                                try
                                {
                                    // string[] Name_FromList = Regex.Split(item, "_timestamp js-short-timestamp js-relative-timestamp");

                                    Create_edat = Utils.getBetween(item, "tooltip\\\" title=\\\"", "\\\"");
                                }
                                catch { };



                                try
                                {
                                    // string[] Name_FromList = Regex.Split(item, "class=\" is-preview\"");

                                    //    Imageurl = Utils.getBetween(item, "src=\\\"", "\\\""); data-img-src=\"

                                    Imageurl = Utils.getBetween(item, "data-img-src=\\\"", "\\\"");
                                    Imageurl = Imageurl.Replace("\\", "");

                                }
                                catch { };




                                try
                                {
                                    string[] Name_FromList = Regex.Split(item, "js-tweet-text tweet-text");

                                    Text = Utils.getBetween(Name_FromList[1], "data-aria-label-part=\\\"0\\\"\\", "\\");

                                    if (Text.Contains("u003e"))
                                    {
                                        Text = Text.Replace("u003e", "");

                                    }

                                    /*
                                    if (Text.Contains("<a "))
                                    {
                                        string ReplaceInBetween = Utils.getBetween(Text, "<a ", "</a>");
                                        ReplaceInBetween = "<a " + ReplaceInBetween + "</a>";
                                        Text = Text.Replace(ReplaceInBetween, "");
                                    }

                                    if (Text.Contains("<a "))
                                    {
                                        string ReplaceInBetween = Utils.getBetween(Text, "<a ", "</a>");
                                        ReplaceInBetween = "<a " + ReplaceInBetween + "</a>";
                                        Text = Text.Replace(ReplaceInBetween, "");
                                    }
                                     */


                                }
                                catch { };

                                try
                                {
                                    //Retweet
                                    string[] Name_FromListFirst = Regex.Split(item, "retweet");

                                    // string[] Name_FromList = Regex.Split(Name_FromListFirst[1], "class=\"ProfileTweet-actionCountForPresentation\"");
                                    Retweetcount = Utils.getBetween(Name_FromListFirst[1], "data-tweet-stat-count=\\\"", "\\\"");
                                }
                                catch { };


                                try
                                {
                                    //>Favorite<
                                    string[] Name_FromListFirst = Regex.Split(item, "favorite");
                                    // string[] Name_FromList = Regex.Split(Name_FromListFirst[1], "class=\"ProfileTweet-actionCountForPresentation\"");
                                    Favoritedcount = Utils.getBetween(Name_FromListFirst[1], "data-tweet-stat-count=\\\"", "\\\"");
                                }
                                catch { };


                                try
                                {
                                    //  string[] Name_FromList = Regex.Split(item, "class=\"ProfileTweet-actionCountForPresentation\"");
                                    FeedId = Utils.getBetween(item, "ndata-tweet-id=\\\"", "\\\"");
                                }
                                catch { };

                                try
                                {
                                    //  string[] Name_FromList = Regex.Split(item, "class=\"ProfileTweet-actionCountForPresentation\"");
                                    FeedUrl = Utils.getBetween(item, "ndata-permalink-path=\\\"", "\"");
                                    FeedUrl = FeedUrl.Replace("\\", "");

                                    FeedUrl = "https://twitter.com" + FeedUrl;
                                }
                                catch { };

                                string data = "Name_From : " + Name_From + "                 +                 " + "Id_From : " + Id_From + "                 +                 " + "Create_edat : " + Create_edat + "                 +                 " + "Imageurl : " + Imageurl + "                 +                 " + "Text : " + Text + "                 +                 " + "Retweetcount : " + Retweetcount + "                 +                 " + "Favoritedcount : " + Favoritedcount + "                 +                 " + "FeedId : " + FeedId + "                 +                 " + "FeedUrl : " + FeedUrl;
                                dataList.Add(data);
                                JObject feedobj = new JObject();
                                try
                                {
                                    string[] datearry = Create_edat.Split(' ');
                                    Create_edat = DateTime.Parse(datearry[3] + " " + datearry[4] + " " + datearry[5] + " " + datearry[0] + " " + datearry[1]).ToString();
                                }
                                catch { }
                                feedobj.Add("Name_From", Name_From);
                                feedobj.Add("Id_From", Id_From);
                                feedobj.Add("Create_edat", Create_edat);
                                feedobj.Add("Imageurl", Imageurl);
                                feedobj.Add("Text", Text);
                                feedobj.Add("Retweetcount", Retweetcount);
                                feedobj.Add("Favoritedcount", Favoritedcount);
                                feedobj.Add("FeedId", FeedId);
                                feedobj.Add("FeedUrl", FeedUrl);
                                dataArray.Add(feedobj);



                                Domain.Socioboard.Domain.Boardtwitterfeeds twitterfeed = new Domain.Socioboard.Domain.Boardtwitterfeeds();
                                twitterfeed.Id = Guid.NewGuid();
                                twitterfeed.FromName = feedobj["Name_From"].ToString();
                                twitterfeed.FromId = feedobj["Id_From"].ToString();
                                twitterfeed.FromPicUrl = feedobj["Imageurl"].ToString();
                                twitterfeed.Text = feedobj["Text"].ToString();
                                twitterfeed.Feedid = feedobj["FeedId"].ToString();
                                twitterfeed.Twitterprofileid = BaordTwitterId;
                                twitterfeed.Isvisible = true;
                                try
                                {
                                    twitterfeed.Createdat = DateTime.Parse(feedobj["Create_edat"].ToString());
                                }
                                catch { }
                                try
                                {
                                    twitterfeed.Retweetcount = Convert.ToInt32(feedobj["Retweetcount"].ToString());
                                }
                                catch { }
                                try
                                {
                                    twitterfeed.Favoritedcount = Convert.ToInt32(feedobj["Favoritedcount"].ToString());
                                }
                                catch { }
                                exitcount++;

                                if (!Boardrepo.checkTwitterFeedExists(twitterfeed.Feedid, BaordTwitterId))
                                {
                                    //twtFeedsList.Add(twitterfeed);
                                    Boardrepo.addBoardTwitterFeed(twitterfeed);
                                }

                                // "https://twitter.com/hashtag/" + hasttagname + "?src=tren"

                            }

                        }
                        try
                        {

                            paginationdataurl = Utils.getBetween(page, "min_position\":\"", "\"");

                            paginationUrl = "https://twitter.com/i/search/timeline?q=" + hasttagname + "&src=typd&vertical=default&include_available_features=1&include_entities=1&max_position=" + paginationdataurl;

                        }
                        catch { }
                    }
                    catch { }

                }


            }
            catch { }




            #endregion




            return dataArray;



        }



        public static JArray getAllDataWithHashtagMongo(string hasttagname,  string BaordTwitterId)
        {

            if (hasttagname.Contains("#"))
            {
                hasttagname = hasttagname.Replace("#", "%23");
            }
            string url = "https://twitter.com/hashtag/" + hasttagname + "?src=tren";
            GlobusHttpHelper globushelper = new GlobusHttpHelper();


            JArray dataArray = new JArray();

            # region scraping data

            string Name_From = string.Empty;
            string Id_From = string.Empty;
            string Create_edat = string.Empty;
            string Imageurl = string.Empty;
            string Text = string.Empty;
            string Retweetcount = string.Empty;
            string Favoritedcount = string.Empty;
            string FeedId = string.Empty;
            string FeedUrl = string.Empty;
            string profileImage = string.Empty;
            // string Name_From = string.Empty;

            int exit = 0;
            string page = globushelper.getHtmlfromUrl(new Uri(url), "", "");

            List<string> dataList = new List<string>();

            MongoRepository boardtwtfeedsrepo = new MongoRepository("MongoBoardTwtFeeds");





            //if (page.Contains("class=\"tweet original-tweet js-stream-tweet js-actionable-tweet js-profile-popup-actionable js-original-tweet")) //js-stream-item stream-item stream-item expanding-stream-item
            if (page.Contains("js-stream-item stream-item stream-item expanding-stream-item"))
            {

                string[] listPage = Regex.Split(page, "js-stream-item stream-item stream-item expanding-stream-item");

                List<string> listPageList = listPage.ToList();
                listPageList.RemoveAt(0);


                foreach (string item in listPageList)
                {

                    #region commented

                    /*
                    if (item.Contains("!DOCTYPE html"))
                    {

                        continue;
                    }


                    try
                    {
                        string[] Name_FromList = Regex.Split(item, "fullname js-action-profile-name show-popup-with-id");

                        Name_From = Utils.getBetween(Name_FromList[1], ">", "<");
                    }
                    catch { };


                    try
                    {
                        string[] Name_FromList = Regex.Split(item, "account-group js-account-group js-action-profile js-user-profile-link js-nav");

                        Id_From = Utils.getBetween(Name_FromList[1], "data-user-id=\"", "\"");
                    }
                    catch { };




                    try
                    {
                        //string[] Name_FromList = Regex.Split(item, "_timestamp js-short-timestamp js-relative-timestamp");  //tweet-timestamp js-permalink js-nav js-tooltip
                        //  Create_edat = Utils.getBetween(Name_FromList[1], ">", "<");
                        string[] Name_FromList = Regex.Split(item, "tweet-timestamp js-permalink js-nav js-tooltip");

                        Create_edat = Utils.getBetween(Name_FromList[1], "title=\"", "\"");
                    }
                    catch { };



                    try
                    {
                        //data-src="

                        // string[] Name_FromList = Regex.Split(item, "class=\" is-preview\"");

                        Imageurl = Utils.getBetween(item, "data-src=\"", "\"");
                        if (!string.IsNullOrEmpty(Imageurl))
                        {
                            Imageurl = "https://twitter.com" + Imageurl;
                        }
                        else
                        {
                            Imageurl = Utils.getBetween(item, "data-img-src=\"", "\"");

                        }

                    }
                    catch { };

                    try
                    {
                        string[] Name_FromList = Regex.Split(item, "class=\"TweetTextSize  js-tweet-text tweet-text\"");



                        Text = Utils.getBetween(Name_FromList[1], ">", "</p>");
                        int count = 0;

                        if (Text.Contains("<a "))
                        {
                            try
                            {
                                count = Regex.Split(Text, "<a").Count();
                            }
                            catch { };
                            for (int i = 0; i < count; i++)
                            {
                                try
                                {
                                    if (Text.Contains("<a "))
                                    {
                                        string ReplaceInBetween = Utils.getBetween(Text, "<a ", "</a>");
                                        ReplaceInBetween = "<a " + ReplaceInBetween + "</a>";
                                        Text = Text.Replace(ReplaceInBetween, "");
                                    }
                                }
                                catch { };
                            }
                        }

                        if (Text.Contains("<img"))
                        {

                            try
                            {
                                count = Regex.Split(Text, "<img").Count();
                            }
                            catch { };
                            for (int i = 0; i < count; i++)
                            {
                                try
                                {
                                    if (Text.Contains("<img "))
                                    {
                                        string ReplaceInBetween = Utils.getBetween(Text, "<img ", ">");
                                        ReplaceInBetween = "<img " + ReplaceInBetween + ">";
                                        Text = Text.Replace(ReplaceInBetween, "");
                                    }
                                }
                                catch { };
                            }
                        }





                        if (Text.Contains("<a "))
                        {
                            string ReplaceInBetween = Utils.getBetween(Text, "<a ", "</a>");
                            ReplaceInBetween = "<a " + ReplaceInBetween + "</a>";
                            Text = Text.Replace(ReplaceInBetween, "").Replace("&", "").Replace("#", "").Replace(";", "");
                        }
                        Text = Text.Replace("&", "").Replace("#", "").Replace(";", "");


                    }
                    catch { };

                    try
                    {
                        //Retweet
                        string[] Name_FromListFirst = Regex.Split(item, ">Retweet<");

                        string[] Name_FromList = Regex.Split(Name_FromListFirst[1], "class=\"ProfileTweet-actionCountForPresentation\"");
                        Retweetcount = Utils.getBetween(Name_FromList[1], ">", "<");
                    }
                    catch { };


                    try
                    {
                        //>Favorite<
                        string[] Name_FromListFirst = Regex.Split(item, ">Favorite<");
                        string[] Name_FromList = Regex.Split(Name_FromListFirst[1], "class=\"ProfileTweet-actionCountForPresentation\"");
                        Favoritedcount = Utils.getBetween(Name_FromList[1], ">", "<");
                    }
                    catch { };


                    try
                    {
                        //  string[] Name_FromList = Regex.Split(item, "class=\"ProfileTweet-actionCountForPresentation\"");
                        FeedId = Utils.getBetween(item, "data-tweet-id=\"", "\"");
                    }
                    catch { };


                    try
                    {
                        //  string[] Name_FromList = Regex.Split(item, "class=\"ProfileTweet-actionCountForPresentation\"");
                        FeedUrl = Utils.getBetween(item, "data-permalink-path=\"", "\"");
                        FeedUrl = "https://twitter.com" + FeedUrl;
                    }
                    catch { };


                    */

                    #endregion

                    #region NewCode

                    Name_From = "";
                    Id_From = "";
                    Create_edat = "";
                    Imageurl = "";
                    Text = string.Empty;
                    Retweetcount = "";
                    Favoritedcount = "";
                    FeedId = "";
                    FeedUrl = "";
                    // string Name_From = string.Empty;
                    profileImage = "";





                    if (item.Contains("!DOCTYPE html"))
                    {

                        continue;
                    }


                    try
                    {
                        string[] Name_FromList = Regex.Split(item, "fullname js-action-profile-name show-popup-with-id");

                        Name_From = Utils.getBetween(Name_FromList[1], ">", "<");
                    }
                    catch { };


                    try
                    {
                        string[] Name_FromList = Regex.Split(item, "account-group js-account-group js-action-profile js-user-profile-link js-nav");

                        Id_From = Utils.getBetween(Name_FromList[1], "data-user-id=\"", "\"");
                    }
                    catch { };




                    try
                    {
                        //string[] Name_FromList = Regex.Split(item, "_timestamp js-short-timestamp js-relative-timestamp");  //tweet-timestamp js-permalink js-nav js-tooltip
                        //  Create_edat = Utils.getBetween(Name_FromList[1], ">", "<");
                        string[] Name_FromList = Regex.Split(item, "tweet-timestamp js-permalink js-nav js-tooltip");

                        Create_edat = Utils.getBetween(Name_FromList[1], "title=\"", "\"");
                    }
                    catch { };



                    try
                    {
                        //data-src="

                        // string[] Name_FromList = Regex.Split(item, "class=\" is-preview\"");

                        Imageurl = Utils.getBetween(item, "data-src=\"", "\"");
                        if (!string.IsNullOrEmpty(Imageurl))
                        {
                            Imageurl = "https://twitter.com" + Imageurl;
                        }
                        else
                        {
                            Imageurl = Utils.getBetween(item, "data-img-src=\"", "\"");

                        }

                    }
                    catch { };

                    try
                    {
                        string[] Name_FromList = Regex.Split(item, "class=\"TweetTextSize  js-tweet-text tweet-text\"");



                        Text = Utils.getBetween(Name_FromList[1], ">", "</p>");
                        int count = 0;

                        if (Text.Contains("<a "))
                        {
                            try
                            {
                                count = Regex.Split(Text, "<a").Count();
                            }
                            catch { };
                            for (int i = 0; i < count; i++)
                            {
                                try
                                {
                                    if (Text.Contains("<a "))
                                    {
                                        string ReplaceInBetween = Utils.getBetween(Text, "<a ", "</a>");
                                        ReplaceInBetween = "<a " + ReplaceInBetween + "</a>";
                                        Text = Text.Replace(ReplaceInBetween, "");
                                    }
                                }
                                catch { };
                            }
                        }

                        if (Text.Contains("<img"))
                        {

                            try
                            {
                                count = Regex.Split(Text, "<img").Count();
                            }
                            catch { };
                            for (int i = 0; i < count; i++)
                            {
                                try
                                {
                                    if (Text.Contains("<img "))
                                    {
                                        string ReplaceInBetween = Utils.getBetween(Text, "<img ", ">");
                                        ReplaceInBetween = "<img " + ReplaceInBetween + ">";
                                        Text = Text.Replace(ReplaceInBetween, "");
                                    }
                                }
                                catch { };
                            }
                        }





                        if (Text.Contains("<a "))
                        {
                            string ReplaceInBetween = Utils.getBetween(Text, "<a ", "</a>");
                            ReplaceInBetween = "<a " + ReplaceInBetween + "</a>";
                            Text = Text.Replace(ReplaceInBetween, "").Replace("&", "").Replace("#", "").Replace(";", "");
                        }
                        Text = Text.Replace("&", "").Replace("#", "").Replace(";", "");


                    }
                    catch { };

                    try
                    {
                        //Retweet
                        string[] Name_FromListFirst = Regex.Split(item, ">Retweet<");

                        string[] Name_FromList = Regex.Split(Name_FromListFirst[1], "class=\"ProfileTweet-actionCountForPresentation\"");
                        Retweetcount = Utils.getBetween(Name_FromList[1], ">", "<");
                    }
                    catch { };


                    try
                    {
                        //>Favorite<
                        string[] Name_FromListFirst = Regex.Split(item, ">Favorite<");
                        string[] Name_FromList = Regex.Split(Name_FromListFirst[1], "class=\"ProfileTweet-actionCountForPresentation\"");
                        Favoritedcount = Utils.getBetween(Name_FromList[1], ">", "<");
                    }
                    catch { };


                    try
                    {
                        //  string[] Name_FromList = Regex.Split(item, "class=\"ProfileTweet-actionCountForPresentation\"");
                        FeedId = Utils.getBetween(item, "data-tweet-id=\"", "\"");
                    }
                    catch { };


                    try
                    {
                        //  string[] Name_FromList = Regex.Split(item, "class=\"ProfileTweet-actionCountForPresentation\"");
                        FeedUrl = Utils.getBetween(item, "data-permalink-path=\"", "\"");
                        FeedUrl = "https://twitter.com" + FeedUrl;
                    }
                    catch { };


                    try
                    {
                        //  string[] Name_FromList = Regex.Split(item, "class=\"ProfileTweet-actionCountForPresentation\"");
                        FeedUrl = Utils.getBetween(item, "data-permalink-path=\"", "\"");
                        FeedUrl = "https://twitter.com" + FeedUrl;
                    }
                    catch { };


                    try
                    {
                        //  string[] Name_FromList = Regex.Split(item, "class=\"ProfileTweet-actionCountForPresentation\"");
                        profileImage = Utils.getBetween(item, "src=\"", "\"");
                        // profileImage = "https://twitter.com" + FeedUrl;
                    }
                    catch { }; Name_From = "";
                    Id_From = "";
                    Create_edat = "";
                    Imageurl = "";
                    Text = string.Empty;
                    Retweetcount = "";
                    Favoritedcount = "";
                    FeedId = "";
                    FeedUrl = "";
                    // string Name_From = string.Empty;
                    profileImage = "";





                    if (item.Contains("!DOCTYPE html"))
                    {

                        continue;
                    }


                    try
                    {
                        string[] Name_FromList = Regex.Split(item, "fullname js-action-profile-name show-popup-with-id");

                        Name_From = Utils.getBetween(Name_FromList[1], ">", "<");
                    }
                    catch { };


                    try
                    {
                        string[] Name_FromList = Regex.Split(item, "account-group js-account-group js-action-profile js-user-profile-link js-nav");

                        Id_From = Utils.getBetween(Name_FromList[1], "data-user-id=\"", "\"");
                    }
                    catch { };




                    try
                    {
                        //string[] Name_FromList = Regex.Split(item, "_timestamp js-short-timestamp js-relative-timestamp");  //tweet-timestamp js-permalink js-nav js-tooltip
                        //  Create_edat = Utils.getBetween(Name_FromList[1], ">", "<");
                        string[] Name_FromList = Regex.Split(item, "tweet-timestamp js-permalink js-nav js-tooltip");

                        Create_edat = Utils.getBetween(Name_FromList[1], "title=\"", "\"");
                    }
                    catch { };



                    try
                    {
                        //data-src="

                        // string[] Name_FromList = Regex.Split(item, "class=\" is-preview\"");

                        Imageurl = Utils.getBetween(item, "data-src=\"", "\"");
                        if (!string.IsNullOrEmpty(Imageurl))
                        {
                            Imageurl = "https://twitter.com" + Imageurl;
                        }
                        else
                        {
                            Imageurl = Utils.getBetween(item, "data-img-src=\"", "\"");

                        }

                    }
                    catch { };

                    try
                    {
                        string[] Name_FromList = Regex.Split(item, "class=\"TweetTextSize  js-tweet-text tweet-text\"");



                        Text = Utils.getBetween(Name_FromList[1], ">", "</p>");
                        int count = 0;

                        if (Text.Contains("<a "))
                        {
                            try
                            {
                                count = Regex.Split(Text, "<a").Count();
                            }
                            catch { };
                            for (int i = 0; i < count; i++)
                            {
                                try
                                {
                                    if (Text.Contains("<a "))
                                    {
                                        string ReplaceInBetween = Utils.getBetween(Text, "<a ", "</a>");
                                        ReplaceInBetween = "<a " + ReplaceInBetween + "</a>";
                                        Text = Text.Replace(ReplaceInBetween, "");
                                    }
                                }
                                catch { };
                            }
                        }

                        if (Text.Contains("<img"))
                        {

                            try
                            {
                                count = Regex.Split(Text, "<img").Count();
                            }
                            catch { };
                            for (int i = 0; i < count; i++)
                            {
                                try
                                {
                                    if (Text.Contains("<img "))
                                    {
                                        string ReplaceInBetween = Utils.getBetween(Text, "<img ", ">");
                                        ReplaceInBetween = "<img " + ReplaceInBetween + ">";
                                        Text = Text.Replace(ReplaceInBetween, "");
                                    }
                                }
                                catch { };
                            }
                        }





                        if (Text.Contains("<a "))
                        {
                            string ReplaceInBetween = Utils.getBetween(Text, "<a ", "</a>");
                            ReplaceInBetween = "<a " + ReplaceInBetween + "</a>";
                            Text = Text.Replace(ReplaceInBetween, "").Replace("&", "").Replace("#", "").Replace(";", "");
                        }
                        Text = Text.Replace("&", "").Replace("#", "").Replace(";", "");


                    }
                    catch { };

                    try
                    {
                        //Retweet
                        string[] Name_FromListFirst = Regex.Split(item, ">Retweet<");

                        string[] Name_FromList = Regex.Split(Name_FromListFirst[1], "class=\"ProfileTweet-actionCountForPresentation\"");
                        Retweetcount = Utils.getBetween(Name_FromList[1], ">", "<");
                    }
                    catch { };


                    try
                    {
                        //>Favorite<
                        string[] Name_FromListFirst = Regex.Split(item, ">Favorite<");
                        string[] Name_FromList = Regex.Split(Name_FromListFirst[1], "class=\"ProfileTweet-actionCountForPresentation\"");
                        Favoritedcount = Utils.getBetween(Name_FromList[1], ">", "<");
                    }
                    catch { };


                    try
                    {
                        //  string[] Name_FromList = Regex.Split(item, "class=\"ProfileTweet-actionCountForPresentation\"");
                        FeedId = Utils.getBetween(item, "data-tweet-id=\"", "\"");
                    }
                    catch { };


                    try
                    {
                        //  string[] Name_FromList = Regex.Split(item, "class=\"ProfileTweet-actionCountForPresentation\"");
                        FeedUrl = Utils.getBetween(item, "data-permalink-path=\"", "\"");
                        FeedUrl = "https://twitter.com" + FeedUrl;
                    }
                    catch { };


                    try
                    {
                        //  string[] Name_FromList = Regex.Split(item, "class=\"ProfileTweet-actionCountForPresentation\"");
                        FeedUrl = Utils.getBetween(item, "data-permalink-path=\"", "\"");
                        FeedUrl = "https://twitter.com" + FeedUrl;
                    }
                    catch { };


                    try
                    {
                        //  string[] Name_FromList = Regex.Split(item, "class=\"ProfileTweet-actionCountForPresentation\"");
                        profileImage = Utils.getBetween(item, "src=\"", "\"");
                        // profileImage = "https://twitter.com" + FeedUrl;
                    }
                    catch { };


                    #endregion


                    string data = "Name_From : " + Name_From + "                 +                 " + "Id_From : " + Id_From + "                 +                 " + "Create_edat : " + Create_edat + "                 +                 " + "Imageurl : " + Imageurl + "                 +                 " + "Text : " + Text + "                 +                 " + "Retweetcount : " + Retweetcount + "                 +                 " + "Favoritedcount : " + Favoritedcount + "                 +                 " + "FeedId : " + FeedId + "                 +                 " + "FeedUrl : " + FeedUrl;
                    dataList.Add(data);


                    JObject feedobj = new JObject();
                    try
                    {

                        string[] datearry = Create_edat.Split(' ');
                        Create_edat = DateTime.Parse(datearry[3] + " " + datearry[4] + " " + datearry[5] + " " + datearry[0] + " " + datearry[1]).ToString();
                    
                    }
                    catch { }
                    feedobj.Add("Name_From", Name_From);
                    feedobj.Add("Id_From", Id_From);
                    feedobj.Add("Create_edat", Create_edat);
                    feedobj.Add("Imageurl", Imageurl);
                    feedobj.Add("Text", Text);
                    feedobj.Add("Retweetcount", Retweetcount);
                    feedobj.Add("Favoritedcount", Favoritedcount);
                    feedobj.Add("FeedId", FeedId);
                    feedobj.Add("FeedUrl", FeedUrl);
                    feedobj.Add("profileImage", profileImage);
                    dataArray.Add(feedobj);



                    MongoBoardTwtFeeds twitterfeed = new MongoBoardTwtFeeds();
                    twitterfeed.Id = ObjectId.GenerateNewId();
                    twitterfeed.FromName = feedobj["Name_From"].ToString();
                    twitterfeed.FromId = feedobj["Id_From"].ToString();
                    twitterfeed.Imageurl = feedobj["Imageurl"].ToString();
                    twitterfeed.FromPicUrl = feedobj["profileImage"].ToString();  //please Add ProfileImageField in MongoBoardTwtFeeds class
                    twitterfeed.Text = feedobj["Text"].ToString();
                    twitterfeed.Feedid = feedobj["FeedId"].ToString();
                    twitterfeed.Twitterprofileid = BaordTwitterId;
                    twitterfeed.Isvisible = true;
                    try
                    {
                        twitterfeed.Createdat = DateTime.Parse(feedobj["Create_edat"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
                    }
                    catch { }
                    try
                    {
                        twitterfeed.Retweetcount = Convert.ToInt32(feedobj["Retweetcount"].ToString());
                    }
                    catch { }
                    try
                    {
                        twitterfeed.Favoritedcount = Convert.ToInt32(feedobj["Favoritedcount"].ToString());
                    }
                    catch { }
                    
                        //twtFeedsList.Add(twitterfeed);
                    boardtwtfeedsrepo.Add<MongoBoardTwtFeeds>(twitterfeed);
                    exit++;

                    // "https://twitter.com/hashtag/" + hasttagname + "?src=tren"

                }


            }


            try
            {

                string paginationdataurl = Utils.getBetween(page, "data-max-position=\"", "\"");

                string paginationUrl = "https://twitter.com/i/search/timeline?q=" + hasttagname + "&src=typd&vertical=default&include_available_features=1&include_entities=1&max_position=" + paginationdataurl;



                while (true)
                {
                    exit++;
                    if (exit == 500) 
                    {
                        return dataArray;
                    }
                    page = globushelper.getHtmlfromUrl(new Uri(paginationUrl), "", "");


                    if (!page.Contains("has_more_items"))
                    {

                        return dataArray;

                    }




                    try
                    {



                        //js-stream-item stream-item stream-item expanding-stream-item


                        if (page.Contains("js-stream-item stream-item stream-item expanding-stream-item"))
                        {

                            string[] listPage = Regex.Split(page, "js-stream-item stream-item stream-item expanding-stream-item");

                            List<string> listPageList = listPage.ToList();
                            listPageList.RemoveAt(0);


                            foreach (string item in listPageList)
                            {

                                #region Commented
                                /*

                                if (item.Contains("!DOCTYPE html"))
                                {

                                    continue;
                                }


                                try
                                {
                                    // string[] Name_FromList = Regex.Split(item, "fullname js-action-profile-name show-popup-with-id");

                                    Name_From = Utils.getBetween(item, "data-name=\\\"", "\\\"");
                                }
                                catch { };


                                try
                                {
                                    string[] Name_FromList = Regex.Split(item, "account-group js-account-group js-action-profile js-user-profile-link js-nav");

                                    Id_From = Utils.getBetween(item, "data-user-id=\\\"", "\\\"");
                                }
                                catch { };




                                try
                                {
                                    // string[] Name_FromList = Regex.Split(item, "_timestamp js-short-timestamp js-relative-timestamp");

                                    Create_edat = Utils.getBetween(item, "tooltip\\\" title=\\\"", "\\\"");
                                }
                                catch { };



                                try
                                {
                                    // string[] Name_FromList = Regex.Split(item, "class=\" is-preview\"");

                                    //    Imageurl = Utils.getBetween(item, "src=\\\"", "\\\""); data-img-src=\"

                                    Imageurl = Utils.getBetween(item, "data-img-src=\\\"", "\\\"");
                                    Imageurl = Imageurl.Replace("\\", "");

                                }
                                catch { };




                                try
                                {
                                    string[] Name_FromList = Regex.Split(item, "js-tweet-text tweet-text");

                                    Text = Utils.getBetween(Name_FromList[1], "data-aria-label-part=\\\"0\\\"\\", "\\");

                                    if (Text.Contains("u003e"))
                                    {
                                        Text = Text.Replace("u003e", "");

                                    }

                                    /*
                                    if (Text.Contains("<a "))
                                    {
                                        string ReplaceInBetween = Utils.getBetween(Text, "<a ", "</a>");
                                        ReplaceInBetween = "<a " + ReplaceInBetween + "</a>";
                                        Text = Text.Replace(ReplaceInBetween, "");
                                    }

                                    if (Text.Contains("<a "))
                                    {
                                        string ReplaceInBetween = Utils.getBetween(Text, "<a ", "</a>");
                                        ReplaceInBetween = "<a " + ReplaceInBetween + "</a>";
                                        Text = Text.Replace(ReplaceInBetween, "");
                                    }
                                     


                                }
                                catch { };

                                try
                                {
                                    //Retweet
                                    string[] Name_FromListFirst = Regex.Split(item, "retweet");

                                    // string[] Name_FromList = Regex.Split(Name_FromListFirst[1], "class=\"ProfileTweet-actionCountForPresentation\"");
                                    Retweetcount = Utils.getBetween(Name_FromListFirst[1], "data-tweet-stat-count=\\\"", "\\\"");
                                }
                                catch { };


                                try
                                {
                                    //>Favorite<
                                    string[] Name_FromListFirst = Regex.Split(item, "favorite");
                                    // string[] Name_FromList = Regex.Split(Name_FromListFirst[1], "class=\"ProfileTweet-actionCountForPresentation\"");
                                    Favoritedcount = Utils.getBetween(Name_FromListFirst[1], "data-tweet-stat-count=\\\"", "\\\"");
                                }
                                catch { };


                                try
                                {
                                    //  string[] Name_FromList = Regex.Split(item, "class=\"ProfileTweet-actionCountForPresentation\"");
                                    FeedId = Utils.getBetween(item, "ndata-tweet-id=\\\"", "\\\"");
                                }
                                catch { };

                                try
                                {
                                    //  string[] Name_FromList = Regex.Split(item, "class=\"ProfileTweet-actionCountForPresentation\"");
                                    FeedUrl = Utils.getBetween(item, "ndata-permalink-path=\\\"", "\"");
                                    FeedUrl = FeedUrl.Replace("\\", "");

                                    FeedUrl = "https://twitter.com" + FeedUrl;
                                }
                                catch { };
                                */

                                #endregion



                                 #region NewCode

                    
                                Name_From = "";
                                Id_From = "";
                                Create_edat = "";
                                Imageurl = "";
                                Text = string.Empty;
                                Retweetcount = "";
                                Favoritedcount = "";
                                FeedId = "";
                                FeedUrl = "";
                                // string Name_From = string.Empty;
                                profileImage = "";







                                if (item.Contains("!DOCTYPE html"))
                                {

                                    continue;
                                }





                                try
                                {
                                   // string[] Name_FromList = Regex.Split(item, "fullname js-action-profile-name show-popup-with-id");

                                    Name_From = Utils.getBetween(item, "data-name=\\\"", "\\\"");

                                    if (Name_From.Contains("&apm;"))
                                    {
                                        Name_From = Name_From.Replace("&amp;", " ");
 
                                    }
                                }
                                catch { };


                                try
                                {
                                    string[] Name_FromList = Regex.Split(item, "account-group js-account-group js-action-profile js-user-profile-link js-nav");

                                    Id_From = Utils.getBetween(item, "data-user-id=\\\"", "\\\"");
                                }
                                catch { };




                                try
                                {
                                   // string[] Name_FromList = Regex.Split(item, "_timestamp js-short-timestamp js-relative-timestamp");

                                    Create_edat = Utils.getBetween(item , "tooltip\\\" title=\\\"", "\\\"");
                                }
                                catch { };



                                try
                                {
                                   // string[] Name_FromList = Regex.Split(item, "class=\" is-preview\"");

                                //    Imageurl = Utils.getBetween(item, "src=\\\"", "\\\""); data-img-src=\"

                                    Imageurl = Utils.getBetween(item, "data-img-src=\\\"", "\\\""); 
                                    Imageurl = Imageurl.Replace("\\", "");

                                }
                                catch { };




                                try
                                {
                                    // string[] Name_FromList = Regex.Split(item, "class=\" is-preview\"");

                                    //    Imageurl = Utils.getBetween(item, "src=\\\"", "\\\""); data-img-src=\"

                                    profileImage = Utils.getBetween(item, "src=\\\"", "\\\"");
                                    profileImage = profileImage.Replace("\\", "");

                                }
                                catch { };





                                try
                                {
                                    string[] Name_FromList = Regex.Split(item, "js-tweet-text tweet-text");

                                    string betweenText = Utils.getBetween(Name_FromList[1], "data-aria-label-part=\\\"0\\\"\\", "twitter-timeline-link");

                                    if (string.IsNullOrEmpty(betweenText))
                                    {
                                        betweenText = Utils.getBetween(Name_FromList[1], "data-aria-label-part=\\\"0\\\"\\", "tweet-details");

                                    }


                                     Text = Utils.getBetween(Name_FromList[1], "data-aria-label-part=\\\"0\\\"\\", "\\");

                                     Text = "";

                                      //  Text =  Text + " " +  Utils.getBetween(Name_FromList[1], ";", "\\");
                                  
                                    //string[] stringTextSplit = Regex.Split(Name_FromList[1],"#10;");
                                    //List<string> stringTextSplitList = stringTextSplit.ToList();
                                    //stringTextSplitList.RemoveAt(0);
                                    //foreach (string item1 in stringTextSplitList)
                                    //{
                                    //    Text += Utils.getBetween(item1, "", "\\");
                                    //}



                                     string[] stringTextSplit = Regex.Split(betweenText,"u003e");
                                    List<string> stringTextSplitList = stringTextSplit.ToList();
                                    stringTextSplitList.RemoveAt(0);
                                    foreach (string item1 in stringTextSplitList)
                                    {
                                        Text += Utils.getBetween(item1, "", "\\");
                                    }





                                    try
                                    {
                                        Text = Text.Replace("u003e", "").Replace("#39;", "").Replace("amp;", "").Replace(";", "");




                                    }
                                    catch { };

                                    /*
                                    if (Text.Contains("<a "))
                                    {
                                        string ReplaceInBetween = Utils.getBetween(Text, "<a ", "</a>");
                                        ReplaceInBetween = "<a " + ReplaceInBetween + "</a>";
                                        Text = Text.Replace(ReplaceInBetween, "");
                                    }

                                    if (Text.Contains("<a "))
                                    {
                                        string ReplaceInBetween = Utils.getBetween(Text, "<a ", "</a>");
                                        ReplaceInBetween = "<a " + ReplaceInBetween + "</a>";
                                        Text = Text.Replace(ReplaceInBetween, "");
                                    }
                                     */


                                }
                                catch { };

                                try
                                {
                                    //Retweet
                                    string[] Name_FromListFirst = Regex.Split(item, "retweet");

                                   // string[] Name_FromList = Regex.Split(Name_FromListFirst[1], "class=\"ProfileTweet-actionCountForPresentation\"");
                                    Retweetcount = Utils.getBetween(Name_FromListFirst[1], "data-tweet-stat-count=\\\"", "\\\"");
                                }
                                catch { };


                                try
                                {
                                    //>Favorite<
                                    string[] Name_FromListFirst = Regex.Split(item, "favorite");
                                   // string[] Name_FromList = Regex.Split(Name_FromListFirst[1], "class=\"ProfileTweet-actionCountForPresentation\"");
                                    Favoritedcount = Utils.getBetween(Name_FromListFirst[1], "data-tweet-stat-count=\\\"", "\\\"");
                                }
                                catch { };


                                try
                                {
                                    //  string[] Name_FromList = Regex.Split(item, "class=\"ProfileTweet-actionCountForPresentation\"");
                                    FeedId = Utils.getBetween(item, "ndata-tweet-id=\\\"", "\\\"");
                                    FeedId = Utils.getBetween(item, "=\\\"", "\\\"");
                                }
                                catch { };

                                try
                                {
                                    //  string[] Name_FromList = Regex.Split(item, "class=\"ProfileTweet-actionCountForPresentation\"");
                                    FeedUrl = Utils.getBetween(item, "ndata-permalink-path=\\\"", "\"");
                                    FeedUrl = FeedUrl.Replace("\\", "");

                                    FeedUrl = "https://twitter.com" + FeedUrl;
                                }
                                catch { };




    #endregion


                                string data = "Name_From : " + Name_From + "                 +                 " + "Id_From : " + Id_From + "                 +                 " + "Create_edat : " + Create_edat + "                 +                 " + "Imageurl : " + Imageurl + "                 +                 " + "Text : " + Text + "                 +                 " + "Retweetcount : " + Retweetcount + "                 +                 " + "Favoritedcount : " + Favoritedcount + "                 +                 " + "FeedId : " + FeedId + "                 +                 " + "FeedUrl : " + FeedUrl;
                                dataList.Add(data);
                                JObject feedobj = new JObject();
                                try
                                {
                                    string[] datearry = Create_edat.Split(' ');
                                    Create_edat = DateTime.Parse(datearry[3] + " " + datearry[4] + " " + datearry[5] + " " + datearry[0] + " " + datearry[1]).ToString();
                                }
                                catch { }
                                feedobj.Add("Name_From", Name_From);
                                feedobj.Add("Id_From", Id_From);
                                feedobj.Add("Create_edat", Create_edat);
                                feedobj.Add("Imageurl", Imageurl);
                                feedobj.Add("profileImage", profileImage);
                                feedobj.Add("Text", Text);
                                feedobj.Add("Retweetcount", Retweetcount);
                                feedobj.Add("Favoritedcount", Favoritedcount);
                                feedobj.Add("FeedId", FeedId);
                                feedobj.Add("FeedUrl", FeedUrl);
                                dataArray.Add(feedobj);



                                MongoBoardTwtFeeds twitterfeed = new MongoBoardTwtFeeds();
                                twitterfeed.Id = ObjectId.GenerateNewId();
                                twitterfeed.FromName = feedobj["Name_From"].ToString();
                                twitterfeed.FromId = feedobj["Id_From"].ToString();
                                twitterfeed.Imageurl = feedobj["Imageurl"].ToString();
                                twitterfeed.FromPicUrl = feedobj["profileImage"].ToString();  //please Add ProfileImageField in MongoBoardTwtFeeds class
                                twitterfeed.Text = feedobj["Text"].ToString();
                                twitterfeed.Feedid = feedobj["FeedId"].ToString();
                                twitterfeed.Twitterprofileid = BaordTwitterId;
                                twitterfeed.Isvisible = true;
                                try
                                {
                                    twitterfeed.Createdat = DateTime.Parse(feedobj["Create_edat"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
                                }
                                catch { }
                                try
                                {
                                    twitterfeed.Retweetcount = Convert.ToInt32(feedobj["Retweetcount"].ToString());
                                }
                                catch { }
                                try
                                {
                                    twitterfeed.Favoritedcount = Convert.ToInt32(feedobj["Favoritedcount"].ToString());
                                }
                                catch { }

                                boardtwtfeedsrepo.Add<MongoBoardTwtFeeds>(twitterfeed);

                                exit++;
                                // "https://twitter.com/hashtag/" + hasttagname + "?src=tren"

                            }

                        }
                        try
                        {

                            paginationdataurl = Utils.getBetween(page, "min_position\":\"", "\"");

                            paginationUrl = "https://twitter.com/i/search/timeline?q=" + hasttagname + "&src=typd&vertical=default&include_available_features=1&include_entities=1&max_position=" + paginationdataurl;

                        }
                        catch { }
                    }
                    catch { }

                }


            }
            catch { }




            #endregion




            return dataArray;



        }
    }
}