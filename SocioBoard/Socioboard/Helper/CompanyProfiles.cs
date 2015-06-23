using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;
using GlobusInstagramLib.Authentication;
using GlobusLinkedinLib.Authentication;
using GlobusTwitterLib.Authentication;
using log4net;
using Newtonsoft.Json.Linq;
using Facebook;

namespace Socioboard.Helper
{
    public class CompanyProfiles
    {
        ILog logger = LogManager.GetLogger(typeof(CompanyProfiles));

        public string getFacebookPageStoryTellers(string PageId, string Accesstoken)
        {

            string accesstoken = "CAACEdEose0cBAED7aeZC5ROwXep8DhBwHFokyGCep41TxdZCw8ku2DnwN3itcpXbq9UT5huGuFI33CAX1cPQz8dyklchY3RK3XkOmkIUjSjSqnnetFeqOZCipjU607EU2yYfyZBH6okf2IxxvlMMWelykjVbe2vcofG65ZBMS8zI1Stj8NsGFZATZCUhZCVtsh3ZCLsl0wOXeBsoBsxml8kDFF2jtyZAceBDkZD";
            string facebookSearchUrl = "https://graph.facebook.com/v2.3/972279612784220/insights?&access_token=" + accesstoken;
            var facebooklistpagerequest = (HttpWebRequest)WebRequest.Create(facebookSearchUrl);
            facebooklistpagerequest.Method = "GET";
            facebooklistpagerequest.Credentials = CredentialCache.DefaultCredentials;
            facebooklistpagerequest.AllowWriteStreamBuffering = true;
            facebooklistpagerequest.ServicePoint.Expect100Continue = false;
            facebooklistpagerequest.PreAuthenticate = false;
            string outputface = string.Empty;
            try
            {
                using (var response = facebooklistpagerequest.GetResponse())
                {
                    using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                    {
                        outputface = stream.ReadToEnd();
                    }
                }
            }
            catch (Exception e) { }
            return outputface;

        }
        public static string getFacebookResponse(string Url)
        {
            var facebooklistpagerequest = (HttpWebRequest)WebRequest.Create(Url);
            facebooklistpagerequest.Method = "GET";
            facebooklistpagerequest.Credentials = CredentialCache.DefaultCredentials;
            facebooklistpagerequest.AllowWriteStreamBuffering = true;
            facebooklistpagerequest.ServicePoint.Expect100Continue = false;
            facebooklistpagerequest.PreAuthenticate = false;
            string outputface = string.Empty;
            try
            {
                using (var response = facebooklistpagerequest.GetResponse())
                {
                    using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                    {
                        outputface = stream.ReadToEnd();
                    }
                }
            }
            catch (Exception e) { }
            return outputface;
        }
        public Dictionary<DateTime, int> getFacebookPageLikes(string PageId, string Accesstoken, int days)
        {


            //string accesstoken = "CAACEdEose0cBAMQVEgKsHtUogOZCQ9vtBZB6FjsUWuZCVEzVUU01yecHqo1fVpTfQq65tmMUfmlafhmGtzmUY6ZCmZBrEXWMp1sfBLMvtdB7c1HBkSBGxbqT0q0nQY6ZBmtPBhg84IrXy4jBjRdMmP1Mh8hlOC9TRuy86jabDi2ccOyeRXYVZA7vuj4HDYgLhrwlNubCYvkENa6nPuY1PCgkuCv1cS8rXMZD";
            try
            {
                long since = DateTime.Now.AddDays(-days).ToUnixTimestamp();
                long until = DateTime.Now.ToUnixTimestamp();
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = Accesstoken;
                //dynamic kasdfj = fb.Get("v2.3/me/permissions");
                dynamic kajlsfdj = fb.Get("v2.0/" + PageId + "/insights/page_fans?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString());
                string facebookSearchUrl = "https://graph.facebook.com/v2.3/" + PageId + "/insights/page_fans?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + Accesstoken;
                string outputface = getFacebookResponse(facebookSearchUrl);
                Dictionary<DateTime, int> LikesByDay = new Dictionary<DateTime, int>();
                LikesByDay = getFacebookLikesDictonary(outputface);

                return LikesByDay;
            }
            catch (Exception ex)
            {
                return new Dictionary<DateTime, int>();
            }

        }

        public Dictionary<DateTime, int> getFacebookLikesDictonary(string Jobject)
        {
            string outputface = Jobject;
            // Dictionary<DateTime, int> LikesByDay = new Dictionary<DateTime, int>();
            Dictionary<DateTime, int> NewLikesByDay = new Dictionary<DateTime, int>();
            int count = 0;
            bool Isfirst = true;
            try
            {
                JArray likesobj = JArray.Parse(JArray.Parse(JObject.Parse(outputface)["data"].ToString())[0]["values"].ToString());
                foreach (JObject obj in likesobj)
                {
                    try
                    {
                        DateTime date = DateTime.Parse(obj["end_time"].ToString());
                        int likescount = 0;
                        //int likescountNew = 0;
                        try
                        {
                            likescount = Convert.ToInt32(obj["value"].ToString());
                        }
                        catch { }

                        if (Isfirst)
                        {
                            count = likescount;
                            Isfirst = false;
                        }
                        else
                        {
                            int val = likescount - count;
                            count = likescount;
                            NewLikesByDay.Add(date, val);
                        }

                        //LikesByDay.Add(date, likescount);

                    }
                    catch { }
                }
            }
            catch { }






            return NewLikesByDay;

        }


        public Dictionary<DateTime, int> getFacebookPageImpressions(string PageId, string Accesstoken, int days)
        {
            //string accesstoken = "CAACEdEose0cBAMQVEgKsHtUogOZCQ9vtBZB6FjsUWuZCVEzVUU01yecHqo1fVpTfQq65tmMUfmlafhmGtzmUY6ZCmZBrEXWMp1sfBLMvtdB7c1HBkSBGxbqT0q0nQY6ZBmtPBhg84IrXy4jBjRdMmP1Mh8hlOC9TRuy86jabDi2ccOyeRXYVZA7vuj4HDYgLhrwlNubCYvkENa6nPuY1PCgkuCv1cS8rXMZD";
            long since = DateTime.Now.AddDays(-days).ToUnixTimestamp();
            long until = DateTime.Now.ToUnixTimestamp();
            string facebookSearchUrl = "https://graph.facebook.com/v2.3/" + PageId + "/insights/page_impressions?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&period=day&access_token=" + Accesstoken;
            string outputface = getFacebookResponse(facebookSearchUrl);
            Dictionary<DateTime, int> LikesByDay = new Dictionary<DateTime, int>();
            LikesByDay = getFacebookImpressionsDictonary(outputface);
            //try
            //{
            //    string previous1 = JObject.Parse(outputface)["paging"]["previous"].ToString();
            //    outputface = getFacebookResponse(previous1);
            //    LikesByDay = LikesByDay.Concat(getFacebookImpressionsDictonary(outputface)).ToDictionary(x => x.Key, x => x.Value);
            //    try
            //    {
            //        string previous2 = JObject.Parse(outputface)["paging"]["previous"].ToString();
            //        LikesByDay = LikesByDay.Concat(getFacebookImpressionsDictonary(getFacebookResponse(previous2))).ToDictionary(x => x.Key, x => x.Value);
            //    }
            //    catch { }
            //}
            //catch { }
            return LikesByDay;

        }

        public Dictionary<DateTime, int> getFacebookImpressionsDictonary(string Jobject)
        {
            string outputface = Jobject;
            Dictionary<DateTime, int> ImpressionsByDay = new Dictionary<DateTime, int>();
            try
            {
                JArray likesobj = JArray.Parse(JArray.Parse(JObject.Parse(outputface)["data"].ToString())[0]["values"].ToString());
                foreach (JObject obj in likesobj)
                {
                    try
                    {
                        DateTime date = DateTime.Parse(obj["end_time"].ToString());
                        int likescount = Convert.ToInt32(obj["value"].ToString());
                        ImpressionsByDay.Add(date, likescount);
                    }
                    catch { }
                }
            }
            catch { }
            return ImpressionsByDay;

        }




        public Dictionary<DateTime, int> getFacebookPageUnLikes(string PageId, string Accesstoken, int days)
        {
            //string accesstoken = "CAACEdEose0cBAMQVEgKsHtUogOZCQ9vtBZB6FjsUWuZCVEzVUU01yecHqo1fVpTfQq65tmMUfmlafhmGtzmUY6ZCmZBrEXWMp1sfBLMvtdB7c1HBkSBGxbqT0q0nQY6ZBmtPBhg84IrXy4jBjRdMmP1Mh8hlOC9TRuy86jabDi2ccOyeRXYVZA7vuj4HDYgLhrwlNubCYvkENa6nPuY1PCgkuCv1cS8rXMZD";
            long since = DateTime.Now.AddDays(-days).ToUnixTimestamp();
            long until = DateTime.Now.ToUnixTimestamp();
            string facebookSearchUrl = "https://graph.facebook.com/v2.3/" + PageId + "/insights/page_fan_removes_unique?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + Accesstoken;
            string outputface = getFacebookResponse(facebookSearchUrl);
            Dictionary<DateTime, int> LikesByDay = new Dictionary<DateTime, int>();
            LikesByDay = getFacebookUnLikeDictonary(outputface);

            return LikesByDay;

        }

        public Dictionary<DateTime, int> getFacebookUnLikeDictonary(string Jobject)
        {
            string outputface = Jobject;
            Dictionary<DateTime, int> UnLikeByDay = new Dictionary<DateTime, int>();
            try
            {
                JArray likesobj = JArray.Parse(JArray.Parse(JObject.Parse(outputface)["data"].ToString())[0]["values"].ToString());
                foreach (JObject obj in likesobj)
                {
                    try
                    {
                        DateTime date = DateTime.Parse(obj["end_time"].ToString());
                        int likescount = Convert.ToInt32(obj["value"].ToString());
                        UnLikeByDay.Add(date, likescount);
                    }
                    catch { }
                }
            }
            catch { }
            return UnLikeByDay;

        }



        public Dictionary<string, int> getFacebookPageImpressionsByAgenGender(string PageId, string Accesstoken, int days)
        {
            //string accesstoken = "CAACEdEose0cBAMQVEgKsHtUogOZCQ9vtBZB6FjsUWuZCVEzVUU01yecHqo1fVpTfQq65tmMUfmlafhmGtzmUY6ZCmZBrEXWMp1sfBLMvtdB7c1HBkSBGxbqT0q0nQY6ZBmtPBhg84IrXy4jBjRdMmP1Mh8hlOC9TRuy86jabDi2ccOyeRXYVZA7vuj4HDYgLhrwlNubCYvkENa6nPuY1PCgkuCv1cS8rXMZD";
            long since = DateTime.Now.AddDays(-days).ToUnixTimestamp();
            long until = DateTime.Now.ToUnixTimestamp();
            string facebookSearchUrl = "https://graph.facebook.com/v2.3/" + PageId + "/insights/page_impressions_by_age_gender_unique?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&period=day&access_token=" + Accesstoken;
            string outputface = getFacebookResponse(facebookSearchUrl);
            Dictionary<string, int> LikesByDay = new Dictionary<string, int>();
            LikesByDay = getFacebookImpressionsDictonaryByAgenGender(outputface);

            return LikesByDay;

        }

        public Dictionary<string, int> getFacebookImpressionsDictonaryByAgenGender(string Jobject)
        {
            string outputface = Jobject;
            int M1824 = 0;
            int F1824 = 0;
            int M2534 = 0;
            int F2534 = 0;
            int M3544 = 0;
            int F3544 = 0;
            int M4554 = 0;
            int F4554 = 0;
            int M5564 = 0;
            int F5564 = 0;
            int M65plus = 0;
            int F65plus = 0;
            Dictionary<string, int> LikesByGender = new Dictionary<string, int>();
            try
            {
                JArray likesobj = JArray.Parse(JArray.Parse(JObject.Parse(outputface)["data"].ToString())[0]["values"].ToString());
                foreach (JObject obj in likesobj)
                {
                    try
                    {
                        M1824 = M1824 + Convert.ToInt32(obj["value"]["M.18-24"].ToString());
                    }
                    catch { }
                    try
                    {
                        F1824 = F1824 + Convert.ToInt32(obj["value"]["F.18-24"].ToString());
                    }
                    catch { }
                    try
                    {
                        M2534 = M2534 + Convert.ToInt32(obj["value"]["M.25-34"].ToString());
                    }
                    catch { }
                    try
                    {
                        F2534 = F1824 + Convert.ToInt32(obj["value"]["F.25-34"].ToString());
                    }
                    catch { }
                    try
                    {
                        M3544 = M3544 + Convert.ToInt32(obj["value"]["M.35-44"].ToString());
                    }
                    catch { }
                    try
                    {
                        F3544 = F3544 + Convert.ToInt32(obj["value"]["F.35-44"].ToString());
                    }
                    catch { }
                    try
                    {
                        M4554 = M4554 + Convert.ToInt32(obj["value"]["M.45-54"].ToString());
                    }
                    catch { }
                    try
                    {
                        F4554 = F4554 + Convert.ToInt32(obj["value"]["F.45-54"].ToString());
                    }
                    catch { }
                    try
                    {
                        M5564 = M5564 + Convert.ToInt32(obj["value"]["M.55-64"].ToString());
                    }
                    catch { }
                    try
                    {
                        F5564 = F5564 + Convert.ToInt32(obj["value"]["F.55-64"].ToString());
                    }
                    catch { }
                    try
                    {
                        M65plus = M65plus + Convert.ToInt32(obj["value"]["M.65+"].ToString());
                    }
                    catch { }
                    try
                    {
                        F65plus = F65plus + Convert.ToInt32(obj["value"]["F.65+"].ToString());
                    }
                    catch { }
                }
            }
            catch { }
            LikesByGender.Add("M1824", M1824);
            LikesByGender.Add("F1824", F1824);
            LikesByGender.Add("M2534", M2534);
            LikesByGender.Add("F2534", F2534);
            LikesByGender.Add("M3544", M3544);
            LikesByGender.Add("F3544", F3544);
            LikesByGender.Add("M4554", M4554);
            LikesByGender.Add("F4554", F4554);
            LikesByGender.Add("M5564", M5564);
            LikesByGender.Add("F5564", F5564);
            LikesByGender.Add("M65plus", M65plus);
            LikesByGender.Add("F65plus", F65plus);
            return LikesByGender;

        }



        public int getFacebookpageImpressionsOrganic(string PageId, string Accesstoken, int days)
        {
            //string accesstoken = "CAACEdEose0cBAMQVEgKsHtUogOZCQ9vtBZB6FjsUWuZCVEzVUU01yecHqo1fVpTfQq65tmMUfmlafhmGtzmUY6ZCmZBrEXWMp1sfBLMvtdB7c1HBkSBGxbqT0q0nQY6ZBmtPBhg84IrXy4jBjRdMmP1Mh8hlOC9TRuy86jabDi2ccOyeRXYVZA7vuj4HDYgLhrwlNubCYvkENa6nPuY1PCgkuCv1cS8rXMZD";
            long since = DateTime.Now.AddDays(-days).ToUnixTimestamp();
            long until = DateTime.Now.ToUnixTimestamp();
            string facebookSearchUrl = "https://graph.facebook.com/v2.3/" + PageId + "/insights/page_impressions_organic?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&period=day&access_token=" + Accesstoken;
            string outputface = getFacebookResponse(facebookSearchUrl);
            int count = 0;
            count = getFacebookImpressionsOrganicCount(outputface);

            return count;

        }

        public int getFacebookImpressionsOrganicCount(string Jobject)
        {
            string outputface = Jobject;
            int count = 0;
            try
            {
                JArray likesobj = JArray.Parse(JArray.Parse(JObject.Parse(outputface)["data"].ToString())[0]["values"].ToString());
                foreach (JObject obj in likesobj)
                {
                    count = count + Convert.ToInt32(obj["value"].ToString());
                }
            }
            catch { }
            return count;

        }




        public int getFacebookpageImpressionsviral(string PageId, string Accesstoken, int days)
        {
            //string accesstoken = "CAACEdEose0cBAMQVEgKsHtUogOZCQ9vtBZB6FjsUWuZCVEzVUU01yecHqo1fVpTfQq65tmMUfmlafhmGtzmUY6ZCmZBrEXWMp1sfBLMvtdB7c1HBkSBGxbqT0q0nQY6ZBmtPBhg84IrXy4jBjRdMmP1Mh8hlOC9TRuy86jabDi2ccOyeRXYVZA7vuj4HDYgLhrwlNubCYvkENa6nPuY1PCgkuCv1cS8rXMZD";
            long since = DateTime.Now.AddDays(-days).ToUnixTimestamp();
            long until = DateTime.Now.ToUnixTimestamp();
            string facebookSearchUrl = "https://graph.facebook.com/v2.3/" + PageId + "/insights/page_impressions_viral?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&period=day&access_token=" + Accesstoken;
            string outputface = getFacebookResponse(facebookSearchUrl);
            int count = 0;
            count = getFacebookImpressionsViralCount(outputface);

            return count;

        }

        public int getFacebookImpressionsViralCount(string Jobject)
        {
            string outputface = Jobject;
            int count = 0;
            try
            {
                JArray likesobj = JArray.Parse(JArray.Parse(JObject.Parse(outputface)["data"].ToString())[0]["values"].ToString());
                foreach (JObject obj in likesobj)
                {
                    count = count + Convert.ToInt32(obj["value"].ToString());
                }
            }
            catch { }
            return count;

        }


        public int getFacebookpageImpressionsPaid(string PageId, string Accesstoken, int days)
        {
            //string accesstoken = "CAACEdEose0cBAMQVEgKsHtUogOZCQ9vtBZB6FjsUWuZCVEzVUU01yecHqo1fVpTfQq65tmMUfmlafhmGtzmUY6ZCmZBrEXWMp1sfBLMvtdB7c1HBkSBGxbqT0q0nQY6ZBmtPBhg84IrXy4jBjRdMmP1Mh8hlOC9TRuy86jabDi2ccOyeRXYVZA7vuj4HDYgLhrwlNubCYvkENa6nPuY1PCgkuCv1cS8rXMZD";
            long since = DateTime.Now.AddDays(-days).ToUnixTimestamp();
            long until = DateTime.Now.ToUnixTimestamp();
            string facebookSearchUrl = "https://graph.facebook.com/v2.3/" + PageId + "/insights/page_impressions_paid?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&period=day&access_token=" + Accesstoken;
            string outputface = getFacebookResponse(facebookSearchUrl);
            int count = 0;
            count = getFacebookImpressionsPaidCount(outputface);
            return count;

        }

        public int getFacebookImpressionsPaidCount(string Jobject)
        {
            string outputface = Jobject;
            int count = 0;
            try
            {
                JArray likesobj = JArray.Parse(JArray.Parse(JObject.Parse(outputface)["data"].ToString())[0]["values"].ToString());
                foreach (JObject obj in likesobj)
                {
                    count = count + Convert.ToInt32(obj["value"].ToString());
                }
            }
            catch { }
            return count;

        }




        public Dictionary<string, int> getFacebookPageImpressionsByCountry(string PageId, string Accesstoken, int days)
        {
            //string accesstoken = "CAACEdEose0cBAMQVEgKsHtUogOZCQ9vtBZB6FjsUWuZCVEzVUU01yecHqo1fVpTfQq65tmMUfmlafhmGtzmUY6ZCmZBrEXWMp1sfBLMvtdB7c1HBkSBGxbqT0q0nQY6ZBmtPBhg84IrXy4jBjRdMmP1Mh8hlOC9TRuy86jabDi2ccOyeRXYVZA7vuj4HDYgLhrwlNubCYvkENa6nPuY1PCgkuCv1cS8rXMZD";
            long since = DateTime.Now.AddDays(-days).ToUnixTimestamp();
            long until = DateTime.Now.ToUnixTimestamp();
            string facebookSearchUrl = "https://graph.facebook.com/v2.3/" + PageId + "/insights/page_impressions_by_country_unique?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&period=day&access_token=" + Accesstoken;
            string outputface = getFacebookResponse(facebookSearchUrl);
            Dictionary<string, int> LikesByDay = new Dictionary<string, int>();
            LikesByDay = getFacebookImpressionsDictonaryByCountry(outputface);

            return LikesByDay;

        }

        public Dictionary<string, int> getFacebookImpressionsDictonaryByCountry(string Jobject)
        {
            string outputface = Jobject;
            Dictionary<string, int> ImpressionsByCity = new Dictionary<string, int>();
            try
            {
                JArray likesobj = JArray.Parse(JArray.Parse(JObject.Parse(outputface)["data"].ToString())[0]["values"].ToString());
                foreach (JObject obj in likesobj)
                {
                    foreach (var item in obj["value"])
                    {
                        string city = item.ToString().Split(':')[0].Replace("\"", string.Empty).Trim();
                        if (ImpressionsByCity.ContainsKey(city))
                        {
                            ImpressionsByCity[city] = ImpressionsByCity[city] + Convert.ToInt32(item.First.ToString());
                        }
                        else
                        {
                            ImpressionsByCity.Add(city, Convert.ToInt32(item.First.ToString()));
                        }
                    }

                }
            }
            catch { }

            return ImpressionsByCity;

        }



        public Dictionary<string, int> getFacebookPageImpressionsByCity(string PageId, string Accesstoken, int days)
        {
            //string accesstoken = "CAACEdEose0cBAMQVEgKsHtUogOZCQ9vtBZB6FjsUWuZCVEzVUU01yecHqo1fVpTfQq65tmMUfmlafhmGtzmUY6ZCmZBrEXWMp1sfBLMvtdB7c1HBkSBGxbqT0q0nQY6ZBmtPBhg84IrXy4jBjRdMmP1Mh8hlOC9TRuy86jabDi2ccOyeRXYVZA7vuj4HDYgLhrwlNubCYvkENa6nPuY1PCgkuCv1cS8rXMZD";
            long since = DateTime.Now.AddDays(-days).ToUnixTimestamp();
            long until = DateTime.Now.ToUnixTimestamp();
            string facebookSearchUrl = "https://graph.facebook.com/v2.3/" + PageId + "/insights/page_impressions_by_city_unique?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&period=day&access_token=" + Accesstoken;
            string outputface = getFacebookResponse(facebookSearchUrl);
            Dictionary<string, int> LikesByDay = new Dictionary<string, int>();
            LikesByDay = getFacebookImpressionsDictonaryByCity(outputface);

            return LikesByDay;

        }

        public Dictionary<string, int> getFacebookImpressionsDictonaryByCity(string Jobject)
        {
            string outputface = Jobject;
            Dictionary<string, int> ImpressionsByCity = new Dictionary<string, int>();
            try
            {
                JArray likesobj = JArray.Parse(JArray.Parse(JObject.Parse(outputface)["data"].ToString())[0]["values"].ToString());
                foreach (JObject obj in likesobj)
                {
                    foreach (var item in obj["value"])
                    {
                        string city = item.ToString().Split(':')[0].Replace("\"", string.Empty).Trim();
                        if (ImpressionsByCity.ContainsKey(city))
                        {
                            ImpressionsByCity[city] = ImpressionsByCity[city] + Convert.ToInt32(item.First.ToString());
                        }
                        else
                        {
                            ImpressionsByCity.Add(city, Convert.ToInt32(item.First.ToString()));
                        }
                    }

                }
            }
            catch { }

            return ImpressionsByCity;

        }


        public Dictionary<string, int> getTwitterFollowersList(string ScreenName, string Accesstoken, int days)
        {
            JObject output = new JObject();
            try
            {
                SortedDictionary<string, string> requestParameters = new SortedDictionary<string, string>();
                requestParameters.Add("count", "5000");
                requestParameters.Add("screen_name", ScreenName);
                requestParameters.Add("cursor", "-1");
                //Token URL
                var oauth_url = "https://api.twitter.com/1.1/followers/list.json?";
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

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    var objText = reader.ReadToEnd();
                    output = JObject.Parse(objText);


                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            // return output.ToString();



            Dictionary<string, int> LikesByDay = new Dictionary<string, int>();
            // LikesByDay = getTwitterFollowers(outputface);

            return LikesByDay;
        }

        public Dictionary<string, int> getTwitterFollowers(string Jobject)
        {
            string outputface = Jobject;
            Dictionary<string, int> ImpressionsByCity = new Dictionary<string, int>();
            try
            {
                JArray likesobj = JArray.Parse(JArray.Parse(JObject.Parse(outputface)["data"].ToString())[0]["values"].ToString());
                foreach (JObject obj in likesobj)
                {
                    foreach (var item in obj["value"])
                    {
                        string city = item.ToString().Split(':')[0].Replace("\"", string.Empty).Trim();
                        if (ImpressionsByCity.ContainsKey(city))
                        {
                            ImpressionsByCity[city] = ImpressionsByCity[city] + Convert.ToInt32(item.First.ToString());
                        }
                        else
                        {
                            ImpressionsByCity.Add(city, Convert.ToInt32(item.First.ToString()));
                        }
                    }

                }
            }
            catch { }

            return ImpressionsByCity;

        }


        //public Dictionary<string, int> getTwitterRetweetsList(string Id, string Accesstoken, int days)
        //{
        //    JObject output = new JObject();
        //    try
        //    {
        //        SortedDictionary<string, string> requestParameters = new SortedDictionary<string, string>();
        //        requestParameters.Add("count", "100");
        //        //requestParameters.Add("screen_name", ScreenName);
        //        //requestParameters.Add("cursor", "-1");
        //        //Token URL
        //        var oauth_url = "https://api.twitter.com/1.1/statuses/retweets/" + Id + ".json";
        //        var headerFormat = "Bearer {0}";
        //        //var authHeader = string.Format(headerFormat, "AAAAAAAAAAAAAAAAAAAAAOZyVwAAAAAAgI0VcykgJ600le2YdR4uhKgjaMs%3D0MYOt4LpwCTAIi46HYWa85ZcJ81qi0D9sh8avr1Zwf7BDzgdHT");
        //        var authHeader = string.Format(headerFormat, "AAAAAAAAAAAAAAAAAAAAAOZyVwAAAAAAgI0VcykgJ600le2YdR4uhKgjaMs%3D0MYOt4LpwCTAIi46HYWa85ZcJ81qi0D9sh8avr1Zwf7BDzgdHT");

        //        var postBody = requestParameters.ToWebString();
        //        ServicePointManager.Expect100Continue = false;

        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(oauth_url + "?"
        //               + requestParameters.ToWebString());

        //        request.Headers.Add("Authorization", authHeader);
        //        request.Method = "GET";
        //        request.Headers.Add("Accept-Encoding", "gzip");

        //        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
        //        Stream responseStream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
        //        using (var reader = new StreamReader(responseStream))
        //        {

        //            JavaScriptSerializer js = new JavaScriptSerializer();
        //            var objText = reader.ReadToEnd();
        //            output = JObject.Parse(objText);


        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        logger.Error(e.Message);
        //    }

        //    // return output.ToString();



        //    Dictionary<string, int> LikesByDay = new Dictionary<string, int>();
        //    // LikesByDay = getTwitterFollowers(outputface);

        //    return LikesByDay;
        //}

        public string getTwitterRetweets(string Accesstoken, string AccesstokenSecret, int days, string LastId)
        {
            oAuthTwitter oauth = new oAuthTwitter();

            oauth.AccessToken = Accesstoken;
            oauth.AccessTokenSecret = AccesstokenSecret;
            oauth.ConsumerKey = ConfigurationManager.AppSettings["consumerKey"];
            oauth.ConsumerKeySecret = ConfigurationManager.AppSettings["consumerSecret"];
            string RequestUrl = "https://api.twitter.com/1.1/statuses/retweets_of_me.json";
            SortedDictionary<string, string> strdic = new SortedDictionary<string, string>();
            strdic.Add("count", "100");
            if (!string.IsNullOrEmpty(LastId))
            {
                strdic.Add("max_id", LastId);
            }
            //strdic.Add("screen_name", ScreenName);
            string response = oauth.oAuthWebRequest(oAuthTwitter.Method.GET, RequestUrl, strdic);

            return response;

        }

        public Dictionary<DateTime, int> getTwitterRetweetsList(string Id, string Accesstoken, string AccesstokenSecret, int days)
        {
            Accesstoken = "2787373862-Quo4aeQX1Mcw5AJhJlFS1IWzA1M5P9X9lYRV7n1";
            AccesstokenSecret = "3lpIu1qPdEreiQQADpo5AywQWOjW27wpY8jPe2PkXveHK";
            //days = 9;
            Id = "3170677652";
            string response = getTwitterRetweets(Accesstoken, AccesstokenSecret, days, "");

            JArray resarray = JArray.Parse(response);

            DateTime date = DateTime.Now;
            bool Istrue = true; int count = 0;
            Dictionary<DateTime, int> LikesByDay = new Dictionary<DateTime, int>(); List<DateTime> datetimeLIst = new List<DateTime>();
            while (Istrue)
            {


                DateTime Createdat = DateTime.Now;
                string maxId = string.Empty;
                foreach (var resobj in resarray)
                {
                    string Const_TwitterDateTemplate = "ddd MMM dd HH:mm:ss +ffff yyyy";
                    Createdat = DateTime.ParseExact((string)resobj["created_at"], Const_TwitterDateTemplate, new System.Globalization.CultureInfo("en-US"));
                    maxId = resobj["id"].ToString();
                    datetimeLIst.Add(Createdat.Date);
                }
                if (Createdat.Date > DateTime.Now.AddDays(-days).Date && !string.IsNullOrEmpty(maxId))
                {
                    response = getTwitterRetweets(Accesstoken, AccesstokenSecret, days, maxId);
                    resarray = JArray.Parse(response);
                    if (resarray.Count == 0)
                    {
                        Istrue = false;
                    }
                }
                else
                {
                    Istrue = false;
                }
            }


            while (date.Date >= DateTime.Now.AddDays(-days).Date)
            {
                count = 0;
                foreach (DateTime dt in datetimeLIst)
                {
                    if (dt.Date == date.Date)
                    {
                        count++;
                    }

                }
                LikesByDay.Add(date, count);
                date = date.AddDays(-1);
            }



            // LikesByDay = getTwitterFollowers(outputface);

            return LikesByDay;
        }




        public Dictionary<DateTime, int> getTwittermentionsList(string Id, string Accesstoken, string AccesstokenSecret, int days)
        {
            Accesstoken = "2787373862-Quo4aeQX1Mcw5AJhJlFS1IWzA1M5P9X9lYRV7n1";
            AccesstokenSecret = "3lpIu1qPdEreiQQADpo5AywQWOjW27wpY8jPe2PkXveHK";
            //days = 9;
            Id = "3170677652";
            oAuthTwitter oauth = new oAuthTwitter();

            oauth.AccessToken = Accesstoken;
            oauth.AccessTokenSecret = AccesstokenSecret;
            oauth.ConsumerKey = ConfigurationManager.AppSettings["consumerKey"];
            oauth.ConsumerKeySecret = ConfigurationManager.AppSettings["consumerSecret"];
            string RequestUrl = "https://api.twitter.com/1.1/statuses/mentions_timeline.json";
            SortedDictionary<string, string> strdic = new SortedDictionary<string, string>();
            strdic.Add("count", "100");
            //strdic.Add("screen_name", ScreenName);
            string response = oauth.oAuthWebRequest(oAuthTwitter.Method.GET, RequestUrl, strdic);

            JArray resarray = JArray.Parse(response);

            DateTime date = DateTime.Now;
            int count = 0;
            Dictionary<DateTime, int> LikesByDay = new Dictionary<DateTime, int>();
            List<DateTime> datetimeLIst = new List<DateTime>();
            foreach (var resobj in resarray)
            {
                string Const_TwitterDateTemplate = "ddd MMM dd HH:mm:ss +ffff yyyy";
                DateTime Createdat = DateTime.ParseExact((string)resobj["created_at"], Const_TwitterDateTemplate, new System.Globalization.CultureInfo("en-US"));
                datetimeLIst.Add(Createdat.Date);
            }

            while (date.Date >= DateTime.Now.AddDays(-days).Date)
            {
                count = 0;
                foreach (DateTime dt in datetimeLIst)
                {
                    if (dt.Date == date.Date)
                    {
                        count++;
                    }

                }
                LikesByDay.Add(date, count);
                date = date.AddDays(-1);
            }



            // LikesByDay = getTwitterFollowers(outputface);

            return LikesByDay;
        }



        #region FacebookLogic
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



        //Start Facebook Search Logic

        public string SearchFacebookPage(string Keyword)
        {
            string facebookResultPage = string.Empty;
            int likes = 0;
            facebookResultPage = this.getFacebookPage(Keyword.Replace(" ", string.Empty));
            string error = string.Empty;
            try
            {
                JObject checkpage = JObject.Parse(facebookResultPage);
                error = checkpage["error"].ToString();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            if (!string.IsNullOrEmpty(error) || string.IsNullOrEmpty(facebookResultPage))
            {
                string fbpagelist = this.getFacebookkPageList(Keyword);
                if (!fbpagelist.StartsWith("["))
                    fbpagelist = "[" + fbpagelist + "]";
                JArray fbpageArray = JArray.Parse(fbpagelist);
                foreach (var item in fbpageArray)
                {
                    var data = item["data"];

                    foreach (var page in data)
                    {
                        try
                        {
                            string fbpage = this.getFacebookPage(page["id"].ToString());
                            JObject pageresult = JObject.Parse(fbpage);
                            if (Convert.ToInt32(pageresult["likes"].ToString()) > likes)
                            {
                                facebookResultPage = fbpage;
                                likes = Convert.ToInt32(pageresult["likes"].ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);

                        }
                    }
                }

            }
            //Domain.Socioboard.Domain.facebookpageinfo fbPageInfo = new Domain.Socioboard.Domain.facebookpageinfo();
            //JObject fb = JObject.Parse(facebookResultPage);
            //try
            //{
            //    fbPageInfo.companyname = fb["name"].ToString();
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.StackTrace);
            //}
            //try
            //{
            //    fbPageInfo.about = fb["about"].ToString();
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.StackTrace);
            //}
            //try
            //{
            //    fbPageInfo.founded = fb["founded"].ToString();
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.StackTrace);
            //}
            //try
            //{
            //    fbPageInfo.description = fb["description"].ToString();
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.StackTrace);
            //}
            //try
            //{
            //    fbPageInfo.country = fb["location"]["country"].ToString();
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.StackTrace);
            //}
            //try
            //{
            //    fbPageInfo.city = fb["location"]["city"].ToString();
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.StackTrace);
            //}
            //try
            //{
            //    fbPageInfo.pageid = fb["id"].ToString();
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.StackTrace);
            //}
            //try
            //{
            //    fbPageInfo.pagelikes = fb["likes"].ToString();
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.StackTrace);
            //}
            //try
            //{
            //    fbPageInfo.phone = fb["phone"].ToString();
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.StackTrace);
            //}
            //try
            //{
            //    fbPageInfo.username = fb["username"].ToString();
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.StackTrace);
            //}
            //try
            //{
            //    fbPageInfo.link = fb["link"].ToString();
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.StackTrace);
            //}
            //try
            //{
            //    fbPageInfo.mission = fb["mission"].ToString();
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.StackTrace);
            //}
            //try
            //{
            //    fbPageInfo.talkingabout = fb["talking_about_count"].ToString();
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.StackTrace);
            //}
            //try
            //{
            //    fbPageInfo.website = fb["website"].ToString();
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.StackTrace);
            //}

            return facebookResultPage;
        }


        //search facebook for pages and return page list

        public string getFacebookkPageList(string Keyword)
        {
            Api.Companypage.Companypage apicompany = new Api.Companypage.Companypage();
            string accesstoken = "CAACZB5L4uuV8BACXwWhgpnE6lrSuIz0vdr6HtMQM8rUEKFPBVfhuYr56OCvPmRqsWPoYaMtYmaRGPZCqRqa562eaoSXaa1xScB5zKtE5jHFw07wI0GENjFOnluGrduNhHRqJT1iNUCFnTh5GXmZAtc4AiZAPMvVXS9EidsDo9PNVQwd262eSFapVZCFvxJpIZD";
            //foreach (Domain.Socioboard.Domain.FacebookAccount item in asltFbAccount)
            //{
            //    accesstoken = item.AccessToken;
            //    if (this.CheckFacebookToken(accesstoken, Keyword))
            //    {

            //        break;
            //    }
            //}

            // getting search results
            string facebookSearchUrl = "https://graph.facebook.com/v1.0/search?q=" + Keyword + "&type=page&access_token=" + accesstoken + "&limit=10";
            var facebooklistpagerequest = (HttpWebRequest)WebRequest.Create(facebookSearchUrl);
            facebooklistpagerequest.Method = "GET";
            facebooklistpagerequest.Credentials = CredentialCache.DefaultCredentials;
            facebooklistpagerequest.AllowWriteStreamBuffering = true;
            facebooklistpagerequest.ServicePoint.Expect100Continue = false;
            facebooklistpagerequest.PreAuthenticate = false;
            string outputface = string.Empty;
            try
            {
                using (var response = facebooklistpagerequest.GetResponse())
                {
                    using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                    {
                        outputface = stream.ReadToEnd();
                    }
                }
            }
            catch (Exception e) { }
            return outputface;
        }

        //Takes pageId as input and return fb page details

        public string getFacebookPage(string PageId)
        {
            string pageUrl = "http://graph.facebook.com/" + PageId.ToString();
            var fbpage = (HttpWebRequest)WebRequest.Create(pageUrl);
            fbpage.Method = "GET";
            string Outputpage = string.Empty;
            try
            {
                using (var response = fbpage.GetResponse())
                {
                    using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                    {
                        Outputpage = stream.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);

            }
            return Outputpage;
        }


        public string getFacebookPageNotes(string PageId)
        {
            //FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
            //ArrayList asltFbAccount = fbAccRepo.getAllFacebookAccounts();
            Api.Companypage.Companypage apicompany = new Api.Companypage.Companypage();
            string accesstoken = "CAACZB5L4uuV8BACXwWhgpnE6lrSuIz0vdr6HtMQM8rUEKFPBVfhuYr56OCvPmRqsWPoYaMtYmaRGPZCqRqa562eaoSXaa1xScB5zKtE5jHFw07wI0GENjFOnluGrduNhHRqJT1iNUCFnTh5GXmZAtc4AiZAPMvVXS9EidsDo9PNVQwd262eSFapVZCFvxJpIZD";
            //foreach (Domain.Socioboard.Domain.FacebookAccount item in asltFbAccount)
            //{
            //    accesstoken = item.AccessToken;
            //    if (this.CheckFacebookToken(accesstoken, PageId))
            //    {
            //        break;
            //    }
            //}
            string facebookSearchUrl = "https://graph.facebook.com/v1.0/" + PageId + "/notes?access_token=" + accesstoken;
            var facebooklistpagerequest = (HttpWebRequest)WebRequest.Create(facebookSearchUrl);
            facebooklistpagerequest.Method = "GET";
            facebooklistpagerequest.Credentials = CredentialCache.DefaultCredentials;
            facebooklistpagerequest.AllowWriteStreamBuffering = true;
            facebooklistpagerequest.ServicePoint.Expect100Continue = false;
            facebooklistpagerequest.PreAuthenticate = false;
            string outputface = string.Empty;
            try
            {
                using (var response = facebooklistpagerequest.GetResponse())
                {
                    using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                    {
                        outputface = stream.ReadToEnd();
                    }
                }
            }
            catch (Exception e) { }
            return outputface;

        }


        public string getFacebookPageFeeds(string PageId)
        {
            //FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
            //ArrayList asltFbAccount = fbAccRepo.getAllFacebookAccounts();
            Api.Companypage.Companypage apicompany = new Api.Companypage.Companypage();
            string accesstoken = "CAACZB5L4uuV8BACXwWhgpnE6lrSuIz0vdr6HtMQM8rUEKFPBVfhuYr56OCvPmRqsWPoYaMtYmaRGPZCqRqa562eaoSXaa1xScB5zKtE5jHFw07wI0GENjFOnluGrduNhHRqJT1iNUCFnTh5GXmZAtc4AiZAPMvVXS9EidsDo9PNVQwd262eSFapVZCFvxJpIZD";
            //foreach (Domain.Socioboard.Domain.FacebookAccount item in asltFbAccount)
            //{
            //    accesstoken = item.AccessToken;
            //    if (this.CheckFacebookToken(accesstoken, PageId))
            //    {
            //        break;
            //    }
            //}
            string facebookSearchUrl = "https://graph.facebook.com/v1.0/" + PageId + "/feed?limit=1000&access_token=" + accesstoken;
            var facebooklistpagerequest = (HttpWebRequest)WebRequest.Create(facebookSearchUrl);
            facebooklistpagerequest.Method = "GET";
            facebooklistpagerequest.Credentials = CredentialCache.DefaultCredentials;
            facebooklistpagerequest.AllowWriteStreamBuffering = true;
            facebooklistpagerequest.ServicePoint.Expect100Continue = false;
            facebooklistpagerequest.PreAuthenticate = false;
            string outputface = string.Empty;
            try
            {
                using (var response = facebooklistpagerequest.GetResponse())
                {
                    using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                    {
                        outputface = stream.ReadToEnd();
                    }
                }
            }
            catch (Exception e) { }
            return outputface;

        }



        public bool IsfacebookAccountVerified(string fbAccountId)
        {
            bool Isverified = false;
            Api.Companypage.Companypage apicompany = new Api.Companypage.Companypage();
            string AccessToken = "CAACZB5L4uuV8BACXwWhgpnE6lrSuIz0vdr6HtMQM8rUEKFPBVfhuYr56OCvPmRqsWPoYaMtYmaRGPZCqRqa562eaoSXaa1xScB5zKtE5jHFw07wI0GENjFOnluGrduNhHRqJT1iNUCFnTh5GXmZAtc4AiZAPMvVXS9EidsDo9PNVQwd262eSFapVZCFvxJpIZD";
            string Url = "https://graph.facebook.com//v2.1/20528438720?fields=is_verified&access_token=" + AccessToken;
            var fbpage = (HttpWebRequest)WebRequest.Create(Url);
            fbpage.Method = "GET";
            string Outputpage = string.Empty;
            try
            {
                using (var response = fbpage.GetResponse())
                {
                    using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                    {
                        Outputpage = stream.ReadToEnd();
                    }
                }
                JObject JobjResult = JObject.Parse(Outputpage);
                if (JobjResult["is_verified"].ToString().Equals("true"))
                {
                    Isverified = true;
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);

            }
            return Isverified;
        }

        # endregion

        # region twitter Logic

        public string TwitterSearch(string keyword)
        {
            string SingleTwitterPageResult = string.Empty;
            try
            {
                SingleTwitterPageResult = TwitterAccountPageWithoutLogin("", keyword);
                if (!string.IsNullOrEmpty(SingleTwitterPageResult))
                {
                    return SingleTwitterPageResult;
                }
            }
            catch (Exception eee)
            {
                logger.Error(eee.Message);

            }
            //int Followers = 0;
            bool ischanged = false;
            string TwitterResutPage = string.Empty;
            string TwitterResutPageid = string.Empty;
            string ScreenName = string.Empty;
            List<Domain.Socioboard.Domain.DiscoverySearch> lstDiscoverySearch = new List<Domain.Socioboard.Domain.DiscoverySearch>();
            string profileid = string.Empty;
            try
            {
                oAuthTwitter oauth = new oAuthTwitter();
                //Twitter obj = new Twitter();
                //TwitterAccountRepository twtAccRepo = new TwitterAccountRepository();
                //ArrayList alst = twtAccRepo.getAllTwitterAccounts();
                oauth.AccessToken = Twitterapponlykey();
                //oauth.AccessTokenSecret = "beScSFa1uI7MttvgjoDPjxYMKgC0Mq2EUYzYewbbNvobO";
                //oauth.ConsumerKey = "LvHB4sHi0RWcQF7MmrstXhEJE";
                //oauth.ConsumerKeySecret = "vd5cdLeje1sThW4cYonIhqWuvKkGk1mZLDu1j1IAbSkLLqp5Kd";
                //oauth.ConsumerKey = ConfigurationManager.AppSettings["consumerKey"];
                //oauth.ConsumerKeySecret = ConfigurationManager.AppSettings["consumerSecret"];
                string twitterSearchResult = Get_Search_Users(oauth, keyword);
                JArray twitterpageArray = JArray.Parse(twitterSearchResult);
                foreach (var item in twitterpageArray)
                {
                    if (item["verified"].ToString().Equals("True"))
                    {
                        TwitterResutPageid = item["id"].ToString();
                        ScreenName = item["screen_name"].ToString();
                        ischanged = true;
                    }
                }
                if (ischanged)
                {
                    TwitterResutPage = Get_Search_SingleUser(oauth, TwitterResutPageid, ScreenName);
                }
                return TwitterResutPage;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                logger.Error(ex.Message);
                return "";
            }
        }

        public string Twitterapponlykey()
        {
            string retvalu = string.Empty;
            var oauth_consumer_key = "yNt3tISGJji5poVSgSO1Og";
            var oauth_consumer_secret = "BvJQlpnjBxN7rtiGF6fIbcGlTgmRac8O3cOamwmr8X4";
            //Token URL
            var oauth_url = "https://api.twitter.com/oauth2/token";
            var headerFormat = "Basic {0}";
            var authHeader = string.Format(headerFormat,
            Convert.ToBase64String(Encoding.UTF8.GetBytes(Uri.EscapeDataString(oauth_consumer_key) + ":" +
            Uri.EscapeDataString((oauth_consumer_secret)))
            ));

            var postBody = "grant_type=client_credentials";

            ServicePointManager.Expect100Continue = false;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(oauth_url);
            request.Headers.Add("Authorization", authHeader);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";

            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = ASCIIEncoding.ASCII.GetBytes(postBody);
                stream.Write(content, 0, content.Length);
            }

            request.Headers.Add("Accept-Encoding", "gzip");
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream responseStream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
            using (var reader = new StreamReader(responseStream))
            {

                JavaScriptSerializer js = new JavaScriptSerializer();
                var objText = reader.ReadToEnd();
                JObject o = JObject.Parse(objText);
                retvalu = o["access_token"].ToString();

            }
            return retvalu;

        }

        public string TwitterAccountPageWithoutLogin(string UserId, string ScreenName)
        {
            JObject output = new JObject();
            try
            {
                SortedDictionary<string, string> requestParameters = new SortedDictionary<string, string>();
                requestParameters.Add("user_id", UserId);
                requestParameters.Add("screen_name", ScreenName);
                //Token URL
                var oauth_url = "https://api.twitter.com/1.1/users/show.json";
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

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    var objText = reader.ReadToEnd();
                    output = JObject.Parse(objText);


                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return output.ToString();
        }


        public string TwitterUserTimeLine(string ScreenName)
        {
            JArray output = new JArray();
            try
            {
                SortedDictionary<string, string> requestParameters = new SortedDictionary<string, string>();
                //requestParameters.Add("user_id", UserId);
                requestParameters.Add("screen_name", ScreenName);
                requestParameters.Add("count", "198");
                //Token URL
                var oauth_url = "https://api.twitter.com/1.1/statuses/user_timeline.json";
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

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    var objText = reader.ReadToEnd();
                    output = JArray.Parse(objText);

                }
            }
            catch (Exception ee) { }

            return output.ToString();
        }

        public string Get_Search_Users(oAuthTwitter oAuth, string SearchKeyword)
        {

            string RequestUrl = "https://api.twitter.com/1.1/users/search.json";
            SortedDictionary<string, string> strdic = new SortedDictionary<string, string>();
            strdic.Add("q", SearchKeyword);
            string response = oAuth.oAuthWebRequest(oAuthTwitter.Method.GET, RequestUrl, strdic);
            if (!response.StartsWith("["))
                response = "[" + response + "]";
            return response;
        }

        public string Get_Search_SingleUser(oAuthTwitter oAuth, string SearchKeyword, string ScreenName)
        {

            string RequestUrl = "https://api.twitter.com/1.1/users/show.json";
            SortedDictionary<string, string> strdic = new SortedDictionary<string, string>();
            strdic.Add("user_id", SearchKeyword);
            strdic.Add("screen_name", ScreenName);
            string response = oAuth.oAuthWebRequest(oAuthTwitter.Method.GET, RequestUrl, strdic);

            return response;
        }

        # endregion

        # region Linkedin Logic


        public string LinkedinSearch(string keyword)
        {
            string profileid = string.Empty;
            try
            {
                // ArrayList alstLIAccounts = objLinkedinrepo.getAllLinkedinAccounts();
                //Domain.Socioboard.Domain.LinkedInAccount linkacc = (Domain.Socioboard.Domain.LinkedInAccount)alstLIAccounts[0];
                oAuthLinkedIn oauth = new oAuthLinkedIn();
                oauth.ConsumerKey = ConfigurationManager.AppSettings["LiApiKey"];
                oauth.ConsumerSecret = ConfigurationManager.AppSettings["LiSecretKey"];
                //oauth.Token = "49c2202b-2cd4-4c74-b5db-ce8d7f5e029e";
                //oauth.TokenSecret = "a79cfbe5-d268-456e-8fdc-0d12869a1cf3";
                //oauth.Verifier = "52921";
                oauth.Token = "b82db6bb-21bb-44d2-a298-0b093708ddbf";
                oauth.TokenSecret = "f7c9b7b8-9295-46fe-8cb4-914c1c52820f";
                oauth.Verifier = "23836";
                //oauth.AccessTokenGet(linkacc.OAuthToken);
                //TODO: access Token Logic
                oauth.AccessTokenGet("b82db6bb-21bb-44d2-a298-0b093708ddbf");
                //oauth.AccessTokenGet();

                //https://api.linkedin.com/v1/people-search? keywords=[space delimited keywords]
                //oauth.AccessTokenGet(oauth.Token);
                // company.Get_CompanyProfileById(oauth, keyword);
                //string response = oauth.APIWebRequest("GET", "https://api.linkedin.com/v1/companies/" + keyword + ":(id,name,email-domains,description,founded-year,end-year,locations,Specialties,website-url,status,employee-count-range,industries,company-type,logo-url,square-logo-url,blog-rss-url,num-followers,universal-name)", null);
                string response = oauth.APIWebRequest("GET", "https://api.linkedin.com/v1/company-search" + ":(companies:(id,name,universal-name,website-url,industries,status,logo-url,blog-rss-url,twitter-id,employee-count-range,specialties,locations,description,stock-exchange,founded-year,end-year,num-followers))?keywords=" + keyword, null);
                XmlDocument XmlResult = new XmlDocument();
                XmlResult.Load(new StringReader(response));


                XmlNode ResultCompany = null;
                int followers = 0;
                string result = string.Empty;
                XmlNodeList Companies = XmlResult.SelectNodes("company-search/companies/company");
                foreach (XmlNode node in Companies)
                {
                    if (Convert.ToInt32(node.SelectSingleNode("num-followers").InnerText) > followers)
                    {
                        ResultCompany = node;
                        followers = Convert.ToInt32(node.SelectSingleNode("num-followers").InnerText);
                    }

                }



                return response;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }
        }


        public string LinkedinCompanyrecentActivites(string CompanyId)
        {
            string response = string.Empty;
            try
            {
                //ArrayList alstLIAccounts = objLinkedinrepo.getAllLinkedinAccounts();
                //Domain.Socioboard.Domain.LinkedInAccount linkacc = (Domain.Socioboard.Domain.LinkedInAccount)alstLIAccounts[0];
                oAuthLinkedIn oauth = new oAuthLinkedIn();
                oauth.ConsumerKey = ConfigurationManager.AppSettings["LiApiKey"];
                oauth.ConsumerSecret = ConfigurationManager.AppSettings["LiSecretKey"];
                //oauth.Token = "49c2202b-2cd4-4c74-b5db-ce8d7f5e029e";
                //oauth.TokenSecret = "a79cfbe5-d268-456e-8fdc-0d12869a1cf3";
                //oauth.Verifier = "52921";
                oauth.Token = "b82db6bb-21bb-44d2-a298-0b093708ddbf";
                oauth.TokenSecret = "f7c9b7b8-9295-46fe-8cb4-914c1c52820f";
                oauth.Verifier = "23836";
                //oauth.AccessTokenGet(linkacc.OAuthToken);
                //TODO: access Token Logic
                oauth.AccessTokenGet("b82db6bb-21bb-44d2-a298-0b093708ddbf");

                //https://api.linkedin.com/v1/people-search? keywords=[space delimited keywords]
                //oauth.AccessTokenGet(oauth.Token);
                // company.Get_CompanyProfileById(oauth, keyword);
                //string response = oauth.APIWebRequest("GET", "https://api.linkedin.com/v1/companies/" + keyword + ":(id,name,email-domains,description,founded-year,end-year,locations,Specialties,website-url,status,employee-count-range,industries,company-type,logo-url,square-logo-url,blog-rss-url,num-followers,universal-name)", null);
                response = oauth.APIWebRequest("GET", "https://api.linkedin.com/v1/companies/" + CompanyId + "/updates?start=0&count=200&event-type=status-update", null);
            }
            catch (Exception e) { }
            return response;

        }


        public string LinkedinCompnayJobs(string CompanyId)
        {
            string response = string.Empty;
            try
            {
                //ArrayList alstLIAccounts = objLinkedinrepo.getAllLinkedinAccounts();
                //Domain.Socioboard.Domain.LinkedInAccount linkacc = (Domain.Socioboard.Domain.LinkedInAccount)alstLIAccounts[0];
                oAuthLinkedIn oauth = new oAuthLinkedIn();
                oauth.ConsumerKey = ConfigurationManager.AppSettings["LiApiKey"];
                oauth.ConsumerSecret = ConfigurationManager.AppSettings["LiSecretKey"];
                oauth.Token = "49c2202b-2cd4-4c74-b5db-ce8d7f5e029e";
                oauth.TokenSecret = "a79cfbe5-d268-456e-8fdc-0d12869a1cf3";
                oauth.Verifier = "52921";
                //oauth.AccessTokenGet(linkacc.OAuthToken);
                oauth.AccessTokenGet("fd200850-37b4-4845-9671-13e5280c7535");
                //TODO : access Token Logic
                //oauth.AccessTokenGet();

                //https://api.linkedin.com/v1/people-search? keywords=[space delimited keywords]
                //oauth.AccessTokenGet(oauth.Token);
                // company.Get_CompanyProfileById(oauth, keyword);
                //string response = oauth.APIWebRequest("GET", "https://api.linkedin.com/v1/companies/" + keyword + ":(id,name,email-domains,description,founded-year,end-year,locations,Specialties,website-url,status,employee-count-range,industries,company-type,logo-url,square-logo-url,blog-rss-url,num-followers,universal-name)", null);
                response = oauth.APIWebRequest("GET", "https://api.linkedin.com/v1/jobs/" + CompanyId, null);
            }
            catch (Exception e) { }
            return response;

        }
        # endregion

        #region Instagram Logic

        public string InstagramSearch(string keyword, string WebUrl)
        {
            string response = string.Empty;
            string resId = string.Empty;
            try
            {
                GlobusInstagramLib.Authentication.ConfigurationIns configi = new GlobusInstagramLib.Authentication.ConfigurationIns("https://api.instagram.com/oauth/authorize/", "f5f052ccbdf94df490020f852863141b", "6c8ac0efa42c4c918bf33835fc98a793", "http://localhost:9821/InstagramManager/Instagram", "http://api.instagram.com/oauth/access_token", "https://api.instagram.com/v1/", "");
                oAuthInstagram _api = new oAuthInstagram();
                _api = oAuthInstagram.GetInstance(configi);
                AccessToken access = new AccessToken();
                //ArrayList arrList = instagramRepo.getAllInstagramAccounts();
                //Domain.Socioboard.Domain.InstagramAccount instaacc = (Domain.Socioboard.Domain.InstagramAccount)arrList[0];
                //string tk = instaacc.AccessToken;
                string tk = "422418207.d89b5cf.7d26304ef400404d816218f2318f6cc6";
                //TODO : Access token Logic
                response = _api.WebRequest(oAuthInstagram.Method.GET, "https://api.instagram.com/v1/users/search?q=" + keyword + "&access_token=" + tk, null);
                if (!response.StartsWith("["))
                    response = "[" + response + "]";
                JArray Instagramaccarray = JArray.Parse(response);
                foreach (var acc in Instagramaccarray)
                {
                    var data = acc["data"];

                    foreach (var page in data)
                    {
                        try
                        {

                            if (page["website"].ToString().Equals(WebUrl))
                            {
                                resId = page["id"].ToString();
                                break;
                            }
                        }
                        catch (Exception e)
                        {
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            if (string.IsNullOrEmpty(resId))
            {
                return string.Empty;
            }
            return InstagramSingleUser(resId);
        }



        public string InstagramSingleUser(string UserId)
        {
            string response = string.Empty;
            try
            {
                GlobusInstagramLib.Authentication.ConfigurationIns configi = new GlobusInstagramLib.Authentication.ConfigurationIns("https://api.instagram.com/oauth/authorize/", "f5f052ccbdf94df490020f852863141b", "6c8ac0efa42c4c918bf33835fc98a793", "http://localhost:9821/InstagramManager/Instagram", "http://api.instagram.com/oauth/access_token", "https://api.instagram.com/v1/", "");
                oAuthInstagram _api = new oAuthInstagram();
                _api = oAuthInstagram.GetInstance(configi);
                AccessToken access = new AccessToken();
                //ArrayList arrList = instagramRepo.getAllInstagramAccounts();
                //Domain.Socioboard.Domain.InstagramAccount instaacc = (Domain.Socioboard.Domain.InstagramAccount)arrList[1];
                //string tk = instaacc.AccessToken;
                string tk = "422418207.d89b5cf.7d26304ef400404d816218f2318f6cc6";
                response = _api.WebRequest(oAuthInstagram.Method.GET, "https://api.instagram.com/v1/users/" + UserId + "/?access_token=" + tk, null);


            }
            catch (Exception ex)
            {

            }

            return response;
        }



        public string getInstagramCompanyPage(string Keyword)
        {
            int followers = 0;
            string ResultPage = string.Empty;
            try
            {
                string instagrampagelist = getInstagramList(Keyword);
                if (!instagrampagelist.StartsWith("["))
                    instagrampagelist = "[" + instagrampagelist + "]";
                JArray fbpageArray = JArray.Parse(instagrampagelist);
                foreach (var item in fbpageArray)
                {
                    var data = item["data"];

                    foreach (var page in data)
                    {
                        try
                        {
                            string instagrampage = this.getInstagramUserDetails(page["id"].ToString());
                            JObject pageresult = JObject.Parse(instagrampage);
                            if (Convert.ToInt32(pageresult["data"]["counts"]["followed_by"].ToString()) > followers)
                            {
                                ResultPage = instagrampage;
                                followers = Convert.ToInt32(pageresult["data"]["counts"]["followed_by"].ToString());
                            }
                        }
                        catch (Exception e)
                        {
                        }
                    }
                }
            }
            catch (Exception e) { }

            return ResultPage;
        }

        public string getInstagramList(string Keyword)
        {
            string InstagramUrl = "https://api.instagram.com/v1/users/search?q=" + Keyword + "&client_id=" + ConfigurationManager.AppSettings["InstagramClientKey"] + "&count=10";
            HttpWebRequest Instagramlistpagerequest = (HttpWebRequest)WebRequest.Create(InstagramUrl);
            Instagramlistpagerequest.Method = "GET";
            Instagramlistpagerequest.Credentials = CredentialCache.DefaultCredentials;
            Instagramlistpagerequest.AllowWriteStreamBuffering = true;
            Instagramlistpagerequest.ServicePoint.Expect100Continue = false;
            Instagramlistpagerequest.PreAuthenticate = false;
            string outputface = string.Empty;
            try
            {
                using (var response = Instagramlistpagerequest.GetResponse())
                {
                    using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                    {
                        outputface = stream.ReadToEnd();
                    }
                }
            }
            catch (Exception e) { }
            return outputface;
        }


        public string getInstagramUserRecentActivities(string UserId)
        {
            string InstagramUrl = "https://api.instagram.com/v1/users/" + UserId + "/media/recent/?client_id=" + ConfigurationManager.AppSettings["InstagramClientKey"];
            var Instagramlistpagerequest = (HttpWebRequest)WebRequest.Create(InstagramUrl);
            Instagramlistpagerequest.Method = "GET";
            Instagramlistpagerequest.Credentials = CredentialCache.DefaultCredentials;
            Instagramlistpagerequest.AllowWriteStreamBuffering = true;
            Instagramlistpagerequest.ServicePoint.Expect100Continue = false;
            Instagramlistpagerequest.PreAuthenticate = false;
            string outputface = string.Empty;
            try
            {
                using (var response = Instagramlistpagerequest.GetResponse())
                {
                    using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                    {
                        outputface = stream.ReadToEnd();
                    }
                }
            }
            catch (Exception e) { }
            return outputface;
        }

        public string getInstagramUserDetails(string UserId)
        {
            string InstagramUrl = "https://api.instagram.com/v1/users/" + UserId + "/?client_id=" + ConfigurationManager.AppSettings["InstagramClientKey"];
            var Instagramlistpagerequest = (HttpWebRequest)WebRequest.Create(InstagramUrl);
            Instagramlistpagerequest.Method = "GET";
            Instagramlistpagerequest.Credentials = CredentialCache.DefaultCredentials;
            Instagramlistpagerequest.AllowWriteStreamBuffering = true;
            Instagramlistpagerequest.ServicePoint.Expect100Continue = false;
            Instagramlistpagerequest.PreAuthenticate = false;
            string outputface = string.Empty;
            try
            {
                using (var response = Instagramlistpagerequest.GetResponse())
                {
                    using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                    {
                        outputface = stream.ReadToEnd();
                    }
                }
            }
            catch (Exception e) { }
            return outputface;
        }
        #endregion

        #region tumblr Logic

        public string TumblrSearch(string keyword)
        {
            string ret = string.Empty;
            string outputface = string.Empty;
            try
            {
                string key = ConfigurationManager.AppSettings["TumblrClientKey"];
                //TumblrAccountRepository tumaccrepo = new TumblrAccountRepository();
                //Domain.Socioboard.Domain.TumblrAccount TumComAcc = tumaccrepo.getTumblrAccountDetailsById(keyword);
                //if (TumComAcc != null && !string.IsNullOrEmpty(TumComAcc.tblrUserName))
                //{
                //    string TumblrSearchUrl = "http://api.tumblr.com/v2/blog/" + TumComAcc.tblrUserName + ".tumblr.com/posts/text?api_key=" + key + "&limit=10";
                //    var TumblrBlogpagerequest = (HttpWebRequest)WebRequest.Create(TumblrSearchUrl);
                //    TumblrBlogpagerequest.Method = "GET";
                //    try
                //    {
                //        using (var response = TumblrBlogpagerequest.GetResponse())
                //        {
                //            using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                //            {
                //                outputface = stream.ReadToEnd();
                //            }
                //        }
                //    }
                //    catch (Exception ex) { }
                //}
                //else
                //{
                string TumblrSearchUrl = string.Empty;
                if (keyword.Contains(".tumblr.com"))
                {
                    if (keyword.Contains("http://"))
                    {
                        keyword = keyword.Remove(0, 7);
                    }
                    TumblrSearchUrl = "http://api.tumblr.com/v2/blog/" + keyword.Replace(" ", string.Empty) + "posts/text?api_key=" + key + "&limit=10";
                }
                else
                {
                    TumblrSearchUrl = "http://api.tumblr.com/v2/blog/" + keyword.Replace(" ", string.Empty) + ".tumblr.com/posts/text?api_key=" + key + "&limit=10";
                }
                var TumblrBlogpagerequest = (HttpWebRequest)WebRequest.Create(TumblrSearchUrl);
                TumblrBlogpagerequest.Method = "GET";
                try
                {
                    using (var response = TumblrBlogpagerequest.GetResponse())
                    {
                        using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                        {
                            outputface = stream.ReadToEnd();
                        }
                    }
                    JObject outputJson = JObject.Parse(outputface);
                    // Domain.Socioboard.Domain.TumblrAccount newtumbobj = new Domain.Socioboard.Domain.TumblrAccount();
                    //newtumbobj.tblrUserName = outputJson["response"]["blog"]["name"].ToString();
                    //TumblrAccountRepository.Add(newtumbobj);
                }
                catch (Exception ex) { }
                // }

            }
            catch (Exception ex)
            {
                throw;
            }
            return outputface;
        }

        # endregion

        #region youtube Logic

        public string YoutubeSearch(string keyword)
        {
            string response = string.Empty;
            string Key = "AIzaSyCISaVFe_TJknn92J7xw2diFEi6Z_aroYE";
            //YoutubeAccountRepository ytAccrepo = new YoutubeAccountRepository();
            //Domain.Socioboard.Domain.YoutubeAccount ytAccount = ytAccrepo.getYoutubeAccountDetailsByUserName(keyword);
            //if (ytAccount != null && !string.IsNullOrEmpty(ytAccount.Ytusername))
            //{
            //    //string RequestUrl = "https://www.googleapis.com/youtube/v3/search?part=" + part + "&maxResults=" + maxResults + "&q=" + keyword + "&key=" + accesstoken;
            //    //string RequestUrl = "https://www.googleapis.com/youtube/v3/search?part=snippet,id&maxResults=20&q=" + keyword + "&key=" + Key;
            //    string RequestUrl = "https://www.googleapis.com/youtube/v3/channels?part=id,snippet,contentDetails,statistics,topicDetails,invideoPromotion&forUsername=" + ytAccount.Ytusername + "&key=" + Key;
            //    //string RequestUrl = "https://www.googleapis.com/youtube/v3/search?part=snippet,id&q=" + ytAccount.Ytusername + "&type=channel&key=" + Key;
            //    var facebooklistpagerequest = (HttpWebRequest)WebRequest.Create(RequestUrl);
            //    facebooklistpagerequest.Method = "GET";
            //    try
            //    {
            //        using (var youtuberesponse = facebooklistpagerequest.GetResponse())
            //        {
            //            using (var stream = new StreamReader(youtuberesponse.GetResponseStream(), Encoding.GetEncoding(1252)))
            //            {
            //                response = stream.ReadToEnd();
            //            }
            //        }
            //    }
            //    catch (Exception e) { }
            //}
            //else
            //{
            //string RequestUrl = "https://www.googleapis.com/youtube/v3/search?part=" + part + "&maxResults=" + maxResults + "&q=" + keyword + "&key=" + accesstoken;
            //string RequestUrl = "https://www.googleapis.com/youtube/v3/search?part=snippet,id&q=" + ytAccount.Ytusername + "&type=channel&key=" + Key;
            string SearchList = YoutubeSearchList(keyword);
            string RequestUrl = "https://www.googleapis.com/youtube/v3/channels?part=id,snippet,contentDetails,statistics,topicDetails,invideoPromotion&forUsername=" + keyword + "&key=" + Key;

            try
            {
                JObject Listresult = JObject.Parse(SearchList);
                keyword = Listresult["items"][0]["id"]["channelId"].ToString();
                RequestUrl = "https://www.googleapis.com/youtube/v3/channels?part=id,snippet,contentDetails,statistics,topicDetails,invideoPromotion&id=" + keyword + "&key=" + Key;

            }
            catch (Exception eee) { }

            var facebooklistpagerequest = (HttpWebRequest)WebRequest.Create(RequestUrl);
            facebooklistpagerequest.Method = "GET";
            try
            {
                using (var youtuberesponse = facebooklistpagerequest.GetResponse())
                {
                    using (var stream = new StreamReader(youtuberesponse.GetResponseStream(), Encoding.GetEncoding(1252)))
                    {
                        response = stream.ReadToEnd();
                    }
                }
                //if (!response.StartsWith("["))
                //    response = "[" + response + "]";
                //JArray youtubechannels = JArray.Parse(response);
                //JObject resultPage = (JObject)youtubechannels[0];
                //Domain.Socioboard.Domain.YoutubeAccount ytnewacc = new Domain.Socioboard.Domain.YoutubeAccount();
                //ytnewacc.Ytusername = resultPage["items"][0]["snippet"]["title"].ToString();
                //ytnewacc.Ytuserid = resultPage["items"][0]["id"].ToString();
                //YoutubeAccountRepository.Add(ytnewacc);
            }
            catch (Exception e) { }
            //  }


            return response;

        }




        public string YoutubeSearchList(string keyword)
        {
            string response = string.Empty;
            string Key = "AIzaSyCISaVFe_TJknn92J7xw2diFEi6Z_aroYE";

            //string RequestUrl = "https://www.googleapis.com/youtube/v3/search?part=" + part + "&maxResults=" + maxResults + "&q=" + keyword + "&key=" + accesstoken;
            //string RequestUrl = "https://www.googleapis.com/youtube/v3/search?part=snippet,id&maxResults=20&q=" + keyword + "&key=" + Key;
            //string RequestUrl = "https://www.googleapis.com/youtube/v3/channels?part=id,snippet,contentDetails,statistics,topicDetails,invideoPromotion&forUsername=" + ytAccount.Ytusername + "&key=" + Key;
            string RequestUrl = "https://www.googleapis.com/youtube/v3/search?part=snippet,id&q=" + keyword + "&type=channel&key=" + Key;
            var facebooklistpagerequest = (HttpWebRequest)WebRequest.Create(RequestUrl);
            facebooklistpagerequest.Method = "GET";
            try
            {
                using (var youtuberesponse = facebooklistpagerequest.GetResponse())
                {
                    using (var stream = new StreamReader(youtuberesponse.GetResponseStream(), Encoding.GetEncoding(1252)))
                    {
                        response = stream.ReadToEnd();
                    }
                }
            }
            catch (Exception e) { }
            return response;
        }


        public string YoutubeChannelPlayList(string ChannelId)
        {
            string Key = "AIzaSyCISaVFe_TJknn92J7xw2diFEi6Z_aroYE";
            //string RequestUrl = "https://www.googleapis.com/youtube/v3/search?part=" + part + "&maxResults=" + maxResults + "&q=" + keyword + "&key=" + accesstoken;
            //string RequestUrl = "https://www.googleapis.com/youtube/v3/search?part=snippet,id&maxResults=20&q=" + keyword + "&key=" + Key;
            string RequestUrl = "https://www.googleapis.com/youtube/v3/playlists?part=id,snippet,status,contentDetails,player&channelId=" + ChannelId + "&key=" + Key + "&maxResults=49";

            var pagerequest = (HttpWebRequest)WebRequest.Create(RequestUrl);
            pagerequest.Method = "GET";
            string response = string.Empty;
            try
            {
                using (var youtuberesponse = pagerequest.GetResponse())
                {
                    using (var stream = new StreamReader(youtuberesponse.GetResponseStream(), Encoding.GetEncoding(1252)))
                    {
                        response = stream.ReadToEnd();
                    }
                }
            }
            catch (Exception e) { }
            return response;
        }
        #endregion

        #region gplus Loigic
        public string GooglePlus(string keyword)
        {
            string ret = string.Empty;
            try
            {
                string Key = "AIzaSyCISaVFe_TJknn92J7xw2diFEi6Z_aroYE";
                //AIzaSyCvTBnEDnr5DEpvlVDCuxz9K9TK84rX0fE
                //string RequestUrl = "https://www.googleapis.com/youtube/v3/search?part=" + part + "&maxResults=" + maxResults + "&q=" + keyword + "&key=" + accesstoken;
                string RequestUrl = "https://www.googleapis.com/plus/v1/people?query=" + keyword + "&key=" + Key;

                var facebooklistpagerequest = (HttpWebRequest)WebRequest.Create(RequestUrl);
                facebooklistpagerequest.Method = "GET";
                string response = string.Empty;
                try
                {
                    using (var youtuberesponse = facebooklistpagerequest.GetResponse())
                    {
                        using (var stream = new StreamReader(youtuberesponse.GetResponseStream(), Encoding.GetEncoding(1252)))
                        {
                            response = stream.ReadToEnd();
                        }
                    }
                }
                catch (Exception e) { }

                return response;
            }
            catch (Exception ex)
            {

                throw;
            }

            //return ret;
        }

        //Google Plus

        public string GooglePlusSearch(string keyword)
        {
            //GooglePlusAccountRepository gpaccrepo = new GooglePlusAccountRepository();
            //Domain.Socioboard.Domain.GooglePlusAccount gpacc = null;
            //try
            //{
            //    gpacc = gpaccrepo.getUserDetailsByUserName(keyword);

            //}
            //catch (Exception ee) { }
            //if (gpacc != null)
            //{
            //    return GooglePlusgetUser(gpacc.GpUserId);
            //}
            //else
            {
                string ResultPage = string.Empty;
                string FirstResultPage = string.Empty;
                //bool Isfirst = true;
                try
                {
                    ResultPage = GooglePlusgetUser(keyword);
                    if (!string.IsNullOrEmpty(ResultPage))
                    {
                        return ResultPage;
                    }
                    JObject PageList = JObject.Parse(GooglePlusList(keyword));
                    foreach (JObject item in PageList["items"])
                    {
                        if (item["objectType"].ToString().Equals("page"))
                        {
                            try
                            {
                                FirstResultPage = GooglePlusgetUser(item["id"].ToString());
                                JObject jobjrpage = JObject.Parse(FirstResultPage);
                                if (jobjrpage["verified"].ToString().Equals("True"))
                                {
                                    ResultPage = GooglePlusgetUser(item["id"].ToString());
                                    break;
                                }
                            }
                            catch (Exception e) { }

                            //TODO: write exact page refine logic
                        }
                    }
                    //JObject jobjresult = JObject.Parse(ResultPage);
                    //if (gpacc == null)
                    //{
                    //    try
                    //    {
                    //        gpacc = gpaccrepo.getUserDetailsByUserName(jobjresult["displayName"].ToString());

                    //    }
                    //    catch (Exception eeee) { }
                    //}
                    //if (gpacc == null)
                    //{
                    //    Domain.Socioboard.Domain.GooglePlusAccount newgplusacc = new Domain.Socioboard.Domain.GooglePlusAccount();
                    //    newgplusacc.GpUserId = jobjresult["id"].ToString();
                    //    newgplusacc.GpUserName = jobjresult["displayName"].ToString();
                    //    newgplusacc.EntryDate = DateTime.Now;
                    //    gpaccrepo.addGooglePlusUser(newgplusacc);
                    //}
                }
                catch (Exception e) { }

                return ResultPage;
            }

        }


        public string GooglePlusList(string keyword)
        {
            string ret = string.Empty;
            try
            {
                string Key = "AIzaSyCISaVFe_TJknn92J7xw2diFEi6Z_aroYE";
                //AIzaSyCvTBnEDnr5DEpvlVDCuxz9K9TK84rX0fE
                //string RequestUrl = "https://www.googleapis.com/youtube/v3/search?part=" + part + "&maxResults=" + maxResults + "&q=" + keyword + "&key=" + accesstoken;
                string RequestUrl = "https://www.googleapis.com/plus/v1/people?query=" + keyword + "&key=" + Key;

                var facebooklistpagerequest = (HttpWebRequest)WebRequest.Create(RequestUrl);
                facebooklistpagerequest.Method = "GET";
                string response = string.Empty;
                try
                {
                    using (var youtuberesponse = facebooklistpagerequest.GetResponse())
                    {
                        using (var stream = new StreamReader(youtuberesponse.GetResponseStream(), Encoding.GetEncoding(1252)))
                        {
                            response = stream.ReadToEnd();

                        }
                    }
                }
                catch (Exception e) { }

                return response;
            }
            catch (Exception ex)
            {

                throw;
            }

            //return ret;
        }


        public string GooglePlusgetUser(string UserId)
        {
            string ret = string.Empty;
            string response = string.Empty;

            try
            {
                string Key = "AIzaSyCISaVFe_TJknn92J7xw2diFEi6Z_aroYE";
                //AIzaSyCvTBnEDnr5DEpvlVDCuxz9K9TK84rX0fE
                //string RequestUrl = "https://www.googleapis.com/youtube/v3/search?part=" + part + "&maxResults=" + maxResults + "&q=" + keyword + "&key=" + accesstoken;
                string RequestUrl = "https://www.googleapis.com/plus/v1/people/" + UserId + "?key=" + Key;

                var facebooklistpagerequest = (HttpWebRequest)WebRequest.Create(RequestUrl);
                facebooklistpagerequest.Method = "GET";
                try
                {
                    using (var youtuberesponse = facebooklistpagerequest.GetResponse())
                    {
                        using (var stream = new StreamReader(youtuberesponse.GetResponseStream(), Encoding.GetEncoding(1252)))
                        {
                            response = stream.ReadToEnd();

                        }
                    }
                }
                catch (Exception e) { }

            }
            catch (Exception ex)
            {
            }
            return response;


            //return ret;
        }


        public string GooglePlusgetUserRecentActivities(string UserId)
        {
            string ret = string.Empty;
            string response = string.Empty;

            try
            {
                string Key = "AIzaSyCISaVFe_TJknn92J7xw2diFEi6Z_aroYE";
                //AIzaSyCvTBnEDnr5DEpvlVDCuxz9K9TK84rX0fE
                //string RequestUrl = "https://www.googleapis.com/youtube/v3/search?part=" + part + "&maxResults=" + maxResults + "&q=" + keyword + "&key=" + accesstoken;
                string RequestUrl = "https://www.googleapis.com/plus/v1/people/" + UserId + "/activities/public/?key=" + Key + "&maxResults=99";

                var gpluspagerequest = (HttpWebRequest)WebRequest.Create(RequestUrl);
                gpluspagerequest.Method = "GET";
                try
                {
                    using (var gplusresponse = gpluspagerequest.GetResponse())
                    {
                        using (var stream = new StreamReader(gplusresponse.GetResponseStream(), Encoding.GetEncoding(1252)))
                        {
                            response = stream.ReadToEnd();

                        }
                    }
                }
                catch (Exception e) { }

            }
            catch (Exception ex)
            {
            }
            return response;


            //return ret;
        }

        #endregion








    }
}