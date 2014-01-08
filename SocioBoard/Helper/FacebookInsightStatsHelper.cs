using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using Facebook;
using SocioBoard.Domain;
using SocioBoard.Model;
using Newtonsoft.Json;
using System.Collections;

namespace SocioBoard.Helper
{
    public class FacebookInsightStatsHelper
    {
        public void getFanPageLikesByGenderAge(string pageId,Guid UserId, int days)
        {
            try
            {
                string strAge = "https://graph.facebook.com/" + pageId + "/insights/page_fans_gender_age";
                FacebookClient fb = new FacebookClient();
                FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
                FacebookAccount acc = fbAccRepo.getUserDetails(pageId);
                fb.AccessToken = acc.AccessToken;
                ///////////////////////////////////////////////////
                // string codedataurlgraphic = objAuthentication.RequestUrl(strAge, strToken);
                //if (txtDateSince.Text != "")
                //    strAge = strAge + "&since=" + txtDateSince.Text;
                //if (txtDateUntill.Text != "")
                //    strAge = strAge + "&until=" + txtDateUntill.Text;

                JsonObject outputreg = (JsonObject)fb.Get(strAge);
                JArray data = (JArray)JsonConvert.DeserializeObject(outputreg["data"].ToString());
                FacebookInsightStats objFbi = new FacebookInsightStats();
                FacebookInsightStatsRepository objfbiRepo = new FacebookInsightStatsRepository();
                foreach (var item in data)
                {
                    var values = item["values"];
                    foreach (var age in values)
                    {
                        var ageVal = age["value"];
                        var agevalarray = ageVal.ToString().Substring(1, ageVal.ToString().Length - 2).Split(',');
                        for (int i = 0; i < agevalarray.Count(); i++)
                        {
                            var genderagearray = agevalarray[i].Split(':');
                            var gender = genderagearray[0].Split('.');
                            objFbi.AgeDiff = gender[1].Trim();
                            objFbi.Gender = gender[0].Trim();
                            objFbi.EntryDate = DateTime.Now;
                            objFbi.FbUserId = pageId;
                            objFbi.Id = Guid.NewGuid();
                            objFbi.PeopleCount = int.Parse(genderagearray[1]);
                            objFbi.UserId = UserId;
                            objFbi.CountDate = age["end_time"].ToString();
                            if (!objfbiRepo.checkFacebookInsightStatsExists(pageId, UserId, age["end_time"].ToString(), gender[1].Trim()))
                                objfbiRepo.addFacebookInsightStats(objFbi);
                            else
                                objfbiRepo.updateFacebookInsightStats(objFbi);
                        }
                        // strFbAgeArray=strFbAgeArray+
                    }
                }
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
        }

        public void getPageImpresion(string pageId, Guid UserId, int days)
        {
            try
            {
                string strAge = "https://graph.facebook.com/" + pageId + "/insights/page_impressions/day";
                FacebookClient fb = new FacebookClient();
                FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
                FacebookAccount acc = fbAccRepo.getUserDetails(pageId);
                fb.AccessToken = acc.AccessToken;
                JsonObject outputreg = (JsonObject)fb.Get(strAge);
                JArray data = (JArray)JsonConvert.DeserializeObject(outputreg["data"].ToString());
                FacebookInsightStats objFbi = new FacebookInsightStats();
                FacebookInsightStatsRepository objfbiRepo = new FacebookInsightStatsRepository();
                foreach (var item in data)
                {
                    var values = item["values"];
                    foreach (var age in values)
                    {
                        objFbi.EntryDate = DateTime.Now;
                        objFbi.FbUserId = pageId;
                        objFbi.Id = Guid.NewGuid();
                        objFbi.PageImpressionCount = int.Parse( age["value"].ToString());
                        objFbi.UserId = UserId;
                        objFbi.CountDate = age["end_time"].ToString();
                        if (!objfbiRepo.checkFbIPageImprStatsExists(pageId, UserId, age["end_time"].ToString()))
                            objfbiRepo.addFacebookInsightStats(objFbi);
                        else
                            objfbiRepo.updateFacebookInsightStats(objFbi);
                    }
                }
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
        }

        public void getStories(string pageId, Guid UserId, int days)
        {
            try
            {
                string strStories = "https://graph.facebook.com/" + pageId + "/insights/post_stories";
                FacebookClient fb = new FacebookClient();
                FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
                FacebookAccount acc = fbAccRepo.getUserDetails(pageId);
                fb.AccessToken = acc.AccessToken;
                JsonObject outputreg = (JsonObject)fb.Get(strStories);
                JArray data = (JArray)JsonConvert.DeserializeObject(outputreg["data"].ToString());
                FacebookInsightStats objFbi = new FacebookInsightStats();
                FacebookInsightStatsRepository objfbiRepo = new FacebookInsightStatsRepository();
                foreach (var item in data)
                {
                    var values = item["values"];
                    foreach (var age in values)
                    {
                        objFbi.EntryDate = DateTime.Now;
                        objFbi.FbUserId = pageId;
                        objFbi.Id = Guid.NewGuid();
                        objFbi.StoriesCount= int.Parse(age["value"].ToString());
                        objFbi.UserId = UserId;
                        objFbi.CountDate = age["end_time"].ToString();
                        objfbiRepo.addFacebookInsightStats(objFbi);
                    }
                }
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
        }

        public void getLocation(string pageId, Guid UserId, int days)
        {
            try
            {
                string strStories = "https://graph.facebook.com/" + pageId + "/insights/page_fans_country";
                FacebookClient fb = new FacebookClient();
                FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
                FacebookAccount acc = fbAccRepo.getUserDetails(pageId);
                fb.AccessToken = acc.AccessToken;
                JsonObject outputreg = (JsonObject)fb.Get(strStories);
                JArray data = (JArray)JsonConvert.DeserializeObject(outputreg["data"].ToString());
                FacebookInsightStats objFbi = new FacebookInsightStats();
                FacebookInsightStatsRepository objfbiRepo = new FacebookInsightStatsRepository();
                foreach (var item in data)
                {
                    var values = item["values"];
                      foreach (var loc in values)
                    {
                        var locVal = loc["value"];
                        var locvalarray = locVal.ToString().Substring(1, locVal.ToString().Length - 2).Split(',');
                        for (int i = 0; i < locvalarray.Count(); i++)
                        {
                            var locationarr = locvalarray[i].Split(':');
                            objFbi.EntryDate = DateTime.Now;
                            objFbi.FbUserId = pageId;
                            objFbi.Id = Guid.NewGuid();
                            objFbi.Location = locationarr[0].ToString();
                            objFbi.PeopleCount = int.Parse(locationarr[1].ToString());
                            objFbi.UserId = UserId;
                            objFbi.CountDate = loc["end_time"].ToString();
                            if (!objfbiRepo.checkFbILocationStatsExists(pageId, UserId, loc["end_time"].ToString(), locationarr[0].ToString()))
                                objfbiRepo.addFacebookInsightStats(objFbi);
                            else
                                objfbiRepo.updateFacebookInsightStats(objFbi);
                        }
                      
                    }
                }
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
        }

        public void getFanPost(string pageId, Guid UserId, int days)
        { 
             string strStories = "https://graph.facebook.com/" + pageId + "/feed";
                FacebookClient fb = new FacebookClient();
                FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
                FacebookAccount acc = fbAccRepo.getUserDetails(pageId);
                fb.AccessToken = acc.AccessToken;
                JsonObject outputreg = (JsonObject)fb.Get(strStories);
                JArray data = (JArray)JsonConvert.DeserializeObject(outputreg["data"].ToString());
                FacebookInsightPostStats objFbiPost = new FacebookInsightPostStats();
                FacebookInsightPostStatsRepository objfbiPostRepo = new FacebookInsightPostStatsRepository();
                foreach (var item in data)
                {
                    objFbiPost.Id = Guid.NewGuid();
                    objFbiPost.EntryDate = DateTime.Now;
                    objFbiPost.PageId = pageId;
                    try
                    {
                        objFbiPost.PostMessage = item["story"].ToString();
                    }
                    catch (Exception Err)
                    {
                        objFbiPost.PostMessage = item["message"].ToString();
                    }
                    objFbiPost.PostDate = item["created_time"].ToString();
                    JArray arrComment=(JArray)item["comment"];
                    if (arrComment != null)
                        objFbiPost.PostComments = arrComment.Count;
                    else
                        objFbiPost.PostComments = 0;
                    objFbiPost.PostId=item["id"].ToString();
                    objFbiPost.UserId = UserId;
                    if(!objfbiPostRepo.checkFacebookInsightPostStatsExists(pageId,item["id"].ToString(),UserId,item["created_time"].ToString()))
                        objfbiPostRepo.addFacebookInsightPostStats(objFbiPost);
                    else
                        objfbiPostRepo.updateFacebookInsightPostStats(objFbiPost);
                }
        }

        public string getPostDetails(string fbUserId, Guid UserId, int days)
        {
            try
            {
                string strlocation = string.Empty;
                string strcount = string.Empty;
                string previousDate = string.Empty;
                FacebookInsightPostStatsRepository objfbiRepo = new FacebookInsightPostStatsRepository();

                ArrayList arrList = objfbiRepo.getFacebookInsightPostStatsById(fbUserId, UserId, days);
                string fanpost = string.Empty;
                foreach (var item in arrList)
                {
                    Array temp = (Array)item;
                    fanpost = fanpost + "<div class=\"message-sent-table\" ><div class=\"labe-1\">" + temp.GetValue(3) + "</div>" +
                               "<div class=\"labe-4\">" + temp.GetValue(4) + "</div><div class=\"labe-5\">" + temp.GetValue(5) + "</div><div class=\"labe-6\">" + temp.GetValue(6) + "</div><div class=\"labe-5\">" + temp.GetValue(5) + "</div></div>";

                }
                return fanpost;
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
                return null;
            }
        }

        public string getLocationInsight(string fbUserId, Guid UserId, int days)
        {
            string strLocationArray = string.Empty;
            try
            {
                string strlocation = string.Empty;
                string strcount = string.Empty;
                string previousDate = string.Empty;
                FacebookInsightStatsRepository objfbiRepo = new FacebookInsightStatsRepository();

                ArrayList arrList = objfbiRepo.getFacebookInsightStatsLocationById(fbUserId, UserId, days);
                foreach (var item in arrList)
                {
                    Array temp = (Array)item;

                    if (temp.GetValue(5) != null && temp.GetValue(6) != null)
                    {
                        strlocation = strlocation + temp.GetValue(5).ToString().Replace("\r\n  \"", " ").Replace('"', ' ').Trim() + ",";
                        strcount = strcount + temp.GetValue(6) + ",";
                    }

                }
                if (arrList.Count == 0)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        strlocation += "0,";
                        strcount += "0,";
                    }
                }
                strLocationArray = strlocation.Substring(0, strlocation.Length - 1) + "@" + strcount.Substring(0, strcount.Length - 1);

            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return strLocationArray;
        }

        public string getLikesByGenderAge(string fbUserId, Guid UserId, int days)
        {
            string strFbAgeArray = string.Empty;
            try
            {
                string strAgem = "0,";
                string strAgef = "0,";
                FacebookInsightStatsRepository objfbiRepo = new FacebookInsightStatsRepository();
                ArrayList arrList = objfbiRepo.getFacebookInsightStatsAgeWiseById(fbUserId, UserId, days);
                strFbAgeArray = "[";
                foreach (var item in arrList)
                {
                    Array temp = (Array)item;
                    if (temp.GetValue(3) != null)
                    {
                        string strmVal="0",strfVal="0";
                        if (temp.GetValue(3).ToString().Contains("M"))
                        {
                            strmVal = temp.GetValue(6).ToString();
                            strfVal = "0";
                        }
                        if (temp.GetValue(3).ToString().Contains("F"))
                        {
                            strfVal = temp.GetValue(6).ToString();
                            strmVal = "0";
                        }
                        strFbAgeArray = strFbAgeArray + "[" + strmVal + "," + strfVal + "],";
                    }
                }
                if (arrList.Count < days)
                {
                    for (int i = 0; i < days - arrList.Count; i++)
                    {
                        strFbAgeArray = strFbAgeArray + "[0,0],";
                    }
                }
                strFbAgeArray = strFbAgeArray.Substring(0, strFbAgeArray.Length - 1) + "]";
               // strFbAgeArray = strAgem.Substring(0, strAgem.Length - 1) + "@" + strAgef.Substring(0, strAgef.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return strFbAgeArray;
        }

        public string getPageImressions(string fbUserId, Guid UserId, int days)
        {
            string strPageImpression = string.Empty;
            try
            {
                string strDate = string.Empty;
                string strImpression = string.Empty;
                FacebookInsightStatsRepository objfbiRepo = new FacebookInsightStatsRepository();
                ArrayList arrList = objfbiRepo.getFacebookInsightStatsById(fbUserId, UserId, days);
                foreach (var item in arrList)
                {
                    Array temp = (Array)item;
                    if (temp.GetValue(9) != null && temp.GetValue(8) != null)
                    {
                        strDate = strDate + temp.GetValue(8) + ",";
                        strImpression = strImpression + temp.GetValue(9) + ",";
                    }
                }
                if (arrList.Count < 7)
                {
                    for (int i = 0; i < 7 - arrList.Count; i++)
                    {
                        strImpression = strImpression + "0,";
                        strDate = strDate + " ,";
                    }
                }
                strPageImpression = strDate.Substring(0, strDate.Length - 1) + "@" + strImpression.Substring(0, strImpression.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return strPageImpression;
        }

        public string getStoriesCount(string fbUserId, Guid UserId, int days)
        {
            string strStories = string.Empty;
            try
            {
              
                FacebookInsightStatsRepository objfbiRepo = new FacebookInsightStatsRepository();
                ArrayList arrList = objfbiRepo.getFacebookInsightStatsById(fbUserId, UserId, days);
                foreach (var item in arrList)
                {
                    Array temp = (Array)item;
                    if (temp.GetValue(7) != null)
                    {
                        //strDate = strDate + temp.GetValue(9) + ",";
                        strStories = strStories + temp.GetValue(7) + ",";
                    }
                }
                if (arrList.Count < 7)
                {
                    for (int i = 0; i < 7 - arrList.Count; i++)
                    {
                        strStories = strStories + "0,";
                        //strDate = strDate + " ,";
                    }
                }
                strStories = strStories.Substring(0, strStories.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return strStories;
        }

        public string getInteractionCount(string fbUserId, Guid UserId, int days)
        {
            string strStories = string.Empty;
            try
            {

                FacebookInsightStatsRepository objfbiRepo = new FacebookInsightStatsRepository();
                ArrayList arrList = objfbiRepo.getFacebookInsightStatsById(fbUserId, UserId, days);
                foreach (var item in arrList)
                {
                    Array temp = (Array)item;
                    if (temp.GetValue(7) != null)
                    {
                        //strDate = strDate + temp.GetValue(9) + ",";
                        strStories = strStories + temp.GetValue(7) + ",";
                    }
                }
                strStories = strStories.Substring(0, strStories.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return strStories;
        }

       
    }
}