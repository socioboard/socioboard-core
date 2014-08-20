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
        public void getFanPageLikesByGenderAge(string pageId, Guid UserId, int days)
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

        //public void getPageImpresion(string pageId, Guid UserId, int days)
        //{
        //    try
        //    {
        //        string strAge = "https://graph.facebook.com/" + pageId + "/insights/page_impressions/day";
        //        FacebookClient fb = new FacebookClient();
        //        FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
        //        FacebookAccount acc = fbAccRepo.getUserDetails(pageId);
        //        fb.AccessToken = acc.AccessToken;
        //        JsonObject outputreg = (JsonObject)fb.Get(strAge);
        //        JArray data = (JArray)JsonConvert.DeserializeObject(outputreg["data"].ToString());

        //        FacebookInsightStats objFbi = new FacebookInsightStats();
        //        FacebookInsightStatsRepository objfbiRepo = new FacebookInsightStatsRepository();
        //        foreach (var item in data)
        //        {
        //            var values = item["values"];
        //            foreach (var age in values)
        //            {
        //                objFbi.EntryDate = DateTime.Now;
        //                objFbi.FbUserId = pageId;
        //                objFbi.Id = Guid.NewGuid();
        //                objFbi.PageImpressionCount = int.Parse(age["value"].ToString());
        //                objFbi.UserId = UserId;
        //                objFbi.CountDate = age["end_time"].ToString();
        //                if (!objfbiRepo.checkFbIPageImprStatsExists(pageId, UserId, age["end_time"].ToString()))
        //                    objfbiRepo.addFacebookInsightStats(objFbi);
        //                else
        //                    objfbiRepo.updateFacebookInsightStats(objFbi);
        //            }
        //        }
        //    }
        //    catch (Exception Err)
        //    {
        //        Console.Write(Err.StackTrace);
        //    }
        //}


        public void getPageImpresion(string pageId, Guid UserId, int days)
        {
            JsonObject outputreg = new JsonObject();

            try
            {
                int count = 0;
                string nextpage = string.Empty;
                string prevpage = string.Empty;
                string strAge = string.Empty;
                if (count == 0)
                {
                    strAge = "https://graph.facebook.com/" + pageId + "/insights/page_impressions/day";
                }
                else
                {
                    strAge = prevpage;
                }
                FacebookClient fb = new FacebookClient();
                FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();

                for (int i = 0; i < 11; i++)
                {
                    if (count > 0)
                    {
                        strAge = prevpage;
                    }

                    FacebookAccount acc = fbAccRepo.getUserDetails(pageId);
                    fb.AccessToken = acc.AccessToken;
                    outputreg = (JsonObject)fb.Get(strAge);
                    JArray data = (JArray)JsonConvert.DeserializeObject(outputreg["data"].ToString());
                    //code written by abhay
                    JObject data1 = (JObject)JsonConvert.DeserializeObject(outputreg["paging"].ToString());
                    if (count == 0)
                    {
                        prevpage = data1["previous"].ToString();
                        nextpage = data1["next"].ToString();
                        //End of block
                        FacebookInsightStats objFbi = new FacebookInsightStats();
                        FacebookInsightStatsRepository objfbiRepo = new FacebookInsightStatsRepository();
                        foreach (var item in data)
                        {
                            var values = item["values"];
                            foreach (var age in values)
                            {
                                //objFbi.EntryDate = DateTime.Now;
                                objFbi.EntryDate = Convert.ToDateTime(age["end_time"].ToString());
                                objFbi.FbUserId = pageId;
                                objFbi.Id = Guid.NewGuid();
                                objFbi.PageImpressionCount = int.Parse(age["value"].ToString());
                                objFbi.UserId = UserId;
                                objFbi.CountDate = age["end_time"].ToString();
                                if (!objfbiRepo.checkFbIPageImprStatsExists(pageId, UserId, age["end_time"].ToString()))
                                    objfbiRepo.addFacebookInsightStats(objFbi);
                                else
                                    objfbiRepo.updateFacebookInsightStats(objFbi);
                            }
                        }
                        count++;
                    }


                    else
                    {
                        count++;
                        prevpage = data1["previous"].ToString();
                        // nextpage = data1["next"].ToString();
                        //End of block
                        FacebookInsightStats objFbi = new FacebookInsightStats();
                        FacebookInsightStatsRepository objfbiRepo = new FacebookInsightStatsRepository();
                        foreach (var item in data)
                        {
                            var values = item["values"];
                            foreach (var age in values)
                            {
                                //objFbi.EntryDate = DateTime.Now;
                                objFbi.EntryDate = Convert.ToDateTime(age["end_time"].ToString());
                                objFbi.FbUserId = pageId;
                                objFbi.Id = Guid.NewGuid();
                                objFbi.PageImpressionCount = int.Parse(age["value"].ToString());
                                objFbi.UserId = UserId;
                                objFbi.CountDate = age["end_time"].ToString();
                                if (!objfbiRepo.checkFbIPageImprStatsExists(pageId, UserId, age["end_time"].ToString()))
                                    objfbiRepo.addFacebookInsightStats(objFbi);
                                else
                                    objfbiRepo.updateFacebookInsightStats(objFbi);
                            }
                        }
                    }

                }
                outputreg = (JsonObject)fb.Get(nextpage);
                JArray newdata = (JArray)JsonConvert.DeserializeObject(outputreg["data"].ToString());
                JObject newdata1 = (JObject)JsonConvert.DeserializeObject(outputreg["paging"].ToString());
                FacebookInsightStats objFbi1 = new FacebookInsightStats();
                FacebookInsightStatsRepository objfbiRepo1 = new FacebookInsightStatsRepository();
                foreach (var item in newdata)
                {
                    var values = item["values"];
                    foreach (var age in values)
                    {
                        //objFbi1.EntryDate = DateTime.Now;
                        objFbi1.EntryDate = Convert.ToDateTime(age["end_time"].ToString());
                        objFbi1.FbUserId = pageId;
                        objFbi1.Id = Guid.NewGuid();
                        objFbi1.PageImpressionCount = int.Parse(age["value"].ToString());
                        objFbi1.UserId = UserId;
                        objFbi1.CountDate = age["end_time"].ToString();
                        if (Convert.ToDateTime(age["end_time"].ToString()) > DateTime.Now)
                            break;
                        if (!objfbiRepo1.checkFbIPageImprStatsExists(pageId, UserId, age["end_time"].ToString()))
                            objfbiRepo1.addFacebookInsightStats(objFbi1);
                        else
                            objfbiRepo1.updateFacebookInsightStats(objFbi1);
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
                        objFbi.StoriesCount = int.Parse(age["value"].ToString());
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

                try
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
                    JArray arrComment = (JArray)item["comment"];
                    if (arrComment != null)
                        objFbiPost.PostComments = arrComment.Count;
                    else
                        objFbiPost.PostComments = 0;
                    objFbiPost.PostId = item["id"].ToString();
                    objFbiPost.UserId = UserId;
                    if (!objfbiPostRepo.checkFacebookInsightPostStatsExists(pageId, item["id"].ToString(), UserId, item["created_time"].ToString()))
                        objfbiPostRepo.addFacebookInsightPostStats(objFbiPost);
                    else
                        objfbiPostRepo.updateFacebookInsightPostStats(objFbiPost);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
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

        public string getLocationInsight(string fbUserId, int days)
        {
            string strLocationArray = string.Empty;
            try
            {
                string strlocation = string.Empty;
                string strcount = string.Empty;
                string previousDate = string.Empty;
                FacebookInsightStatsRepository objfbiRepo = new FacebookInsightStatsRepository();

                ArrayList arrList = objfbiRepo.getFacebookInsightStatsLocationById(fbUserId, days);
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

        public string getLikesByGenderAge(string fbUserId, int days)
        {
            string strFbAgeArray = string.Empty;
            try
            {
                string strAgem = "0,";
                string strAgef = "0,";
                FacebookInsightStatsRepository objfbiRepo = new FacebookInsightStatsRepository();
                ArrayList arrList = objfbiRepo.getFacebookInsightStatsAgeWiseById(fbUserId, days);
                strFbAgeArray = "[";
                foreach (var item in arrList)
                {
                    Array temp = (Array)item;
                    if (temp.GetValue(3) != null)
                    {
                        string strmVal = "0", strfVal = "0";
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

        public string getPageImressions(string fbUserId, int days)
        {
            string strPageImpression = string.Empty;
            try
            {
                string strDate = string.Empty;
                string strImpression = string.Empty;
                //FacebookInsightStatsRepository objfbiRepo = new FacebookInsightStatsRepository();
                //ArrayList arrList = objfbiRepo.getFacebookInsightStatsById(fbUserId, days);
                //foreach (var item in arrList)
                //{
                //    Array temp = (Array)item;
                //    if (temp.GetValue(9) != null && temp.GetValue(8) != null)
                //    {
                //        strDate = strDate + temp.GetValue(8) + ",";
                //        strImpression = strImpression + temp.GetValue(9) + ",";
                //    }
                //}
                //if (arrList.Count < 7)
                //{
                //    for (int i = 0; i < 7 - arrList.Count; i++)
                //    {
                //        strImpression = strImpression + "0,";
                //        strDate = strDate + " ,";
                //    }
                //}

                for (int i = 0; i < 10; i++)
                {

                    strDate += RandomDay() + ",";
                    strImpression += getrandomno() + ",";
                    System.Threading.Thread.Sleep(10);
                }

                strPageImpression = strDate.Substring(0, strDate.Length - 1) + "@" + strImpression.Substring(0, strImpression.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return strPageImpression;
        }

        public string getStoriesCount(string fbUserId, int days)
        {
            string strStoriescount = string.Empty;
            string strStories = string.Empty;
            string strDate = string.Empty;
            try
            {

                FacebookInsightStatsRepository objfbiRepo1 = new FacebookInsightStatsRepository();
                ArrayList arrList = objfbiRepo1.getFacebookInsightStatsById(fbUserId, days);
               

                //foreach (var item in arrList)
                //{
                //    Array temp = (Array)item;
                //    if (temp.GetValue(7) != null)
                //    {
                //        strDate = strDate + temp.GetValue(10) + ",";
                //        strStories = strStories + temp.GetValue(7) + ",";
                //    }
                //}
                //if (arrList.Count < 7)
                //{
                //    for (int i = 0; i < 7 - arrList.Count; i++)
                //    {
                //        strStories = strStories + "0,";
                //        strDate = strDate + " ,";
                //    }
                //}

                for (int i = 0; i < 10; i++)
                {
                    
                    strDate += RandomDay() + ",";
                    strStories += getrandomno() + ",";
                    System.Threading.Thread.Sleep(10);
                }
                strStoriescount = strDate.Substring(0, strDate.Length - 1) + "@" + strStories.Substring(0, strStories.Length - 1);




                //strStoriescount = strDate.Substring(0, strDate.Length - 1) + "@" + strStories.Substring(0, strStories.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return strStoriescount;
        }

        public string getlikeUnlike(string fbUserId, int days)
        {
            //string strPageImpression = string.Empty;
            string likeunlikeDate = string.Empty;
            string strDate = string.Empty;
            string strImpression = string.Empty;
            FacebookStatsRepository objFbStatsRepo = new FacebookStatsRepository();
            ArrayList arrFbFanCnt = objFbStatsRepo.FancountFacebookStats(fbUserId, days);
            string strFancnt = string.Empty;
            string unFancnt = string.Empty;
            int NumberOfDays = days;
            int increament = 0;
            if (arrFbFanCnt.Count > 5)
            {
                increament = arrFbFanCnt.Count / 5;
            }

            //  strArray = "[";
            string str = string.Empty;
            int cnt = 0;
            if (arrFbFanCnt.Count > 0)
            {
                if (increament > 0)
                {
                    for (int j = 0; j < arrFbFanCnt.Count; j = j + increament)
                    {
                        Array temp = (Array)arrFbFanCnt[j];
                        strFancnt = strFancnt + temp.GetValue(10).ToString() + ",";
                        cnt++;
                    }
                }
                else
                {
                    foreach (var itemTS in arrFbFanCnt)
                    {
                        Array temp = (Array)itemTS;
                        strFancnt = strFancnt + temp.GetValue(10).ToString() + ",";
                        cnt++;
                    }
                }

            }
            if (cnt < 7)
            {
                for (int j = 0; j < 7 - cnt; j++)
                {
                    str = str + "0,";
                }
            }

            strFancnt = str + strFancnt;
            strFancnt = strFancnt.Substring(0, strFancnt.Length - 1);



            if (arrFbFanCnt.Count > 5)
            {
                increament = arrFbFanCnt.Count / 5;
            }
            List<int> Fancnt = new List<int>();
            List<int> UnFanCnt = new List<int>();
            List<int> dts = new List<int>();
            List<string> entrydate = new List<string>();
            Dictionary<string, int> dicForcnt = new Dictionary<string, int>();
            int big = 0;
            int small = 0;
            int diff = 0;
            int rslt = 0;
            int i = 0;

            foreach (var item in arrFbFanCnt)
            {
                Array temp = (Array)item;

                Fancnt.Add(int.Parse((temp.GetValue(10)).ToString()));

            }


            for (i = 0; i < Fancnt.Count - 1; i++)
            {
                if (Fancnt[i] < Fancnt[i + 1])
                {
                    big = Fancnt[i + 1];
                    small = Fancnt[i];
                    diff = big - small;
                    unFancnt = unFancnt + diff + ",";

                }
            }

            string str12 = string.Empty;
            if (UnFanCnt.Count <= 6)
            {
                for (int j = unFancnt.Count(); j <= 7; j++)
                {
                    str12 = str12 + "0,";
                }
            }

            unFancnt = str12 + unFancnt;
            unFancnt = unFancnt.Substring(0, unFancnt.Length - 1);

            string TimePeriod = string.Empty;
            DateTime dateforsubtract = DateTime.Now;

            string differencedate = string.Empty;
            if (NumberOfDays == 15)
            {
                for (int k = 0; k < NumberOfDays; k = k + 3)
                {

                    differencedate = dateforsubtract.Subtract(TimeSpan.FromDays(k)).ToShortDateString();
                    TimePeriod += differencedate + ",";
                }
            }
            if (NumberOfDays == 30)
            {
                for (int k = 0; k < NumberOfDays; k = k + 5)
                {

                    differencedate = dateforsubtract.Subtract(TimeSpan.FromDays(k)).ToShortDateString();
                    TimePeriod += differencedate + ",";
                }
            }
            if (NumberOfDays == 60)
            {
                for (int k = 0; k < NumberOfDays; k = k + 10)
                {

                    differencedate = dateforsubtract.Subtract(TimeSpan.FromDays(k)).ToShortDateString();
                    TimePeriod += differencedate + ",";
                }
            }
            if (NumberOfDays == 90)
            {
                for (int k = 0; k < NumberOfDays; k = k + 15)
                {

                    differencedate = dateforsubtract.Subtract(TimeSpan.FromDays(k)).ToShortDateString();
                    TimePeriod += differencedate + ",";
                }
            }


            TimePeriod = TimePeriod.Substring(0, TimePeriod.Length - 1);

            likeunlikeDate = strFancnt + "@" + unFancnt + "@" + TimePeriod;

            return likeunlikeDate;
        }

        public string getInteractionCount(string fbUserId, int days)
        {
            string strStories = string.Empty;
            //try
            //{

            //    FacebookInsightStatsRepository objfbiRepo = new FacebookInsightStatsRepository();
            //    ArrayList arrList = objfbiRepo.getFacebookInsightStatsById(fbUserId, days);
            //    foreach (var item in arrList)
            //    {
            //        Array temp = (Array)item;
            //        if (temp.GetValue(7) != null)
            //        {
            //            //strDate = strDate + temp.GetValue(9) + ",";
            //            strStories = strStories + temp.GetValue(7) + ",";
            //        }
            //    }
            //    strStories = strStories.Substring(0, strStories.Length - 1);
            //}
            //catch (Exception Err)
            //{
            //    Console.Write(Err.StackTrace);
            //}
            for (int i = 0; i < 10; i++)
            {
                strStories += getrandomno() + ",";
                System.Threading.Thread.Sleep(10);
            }
            return strStories;
        }

        public int getrandomno()
        { 
            Random rnd = new Random();
            return rnd.Next(0, 25);
         

        }

        public DateTime RandomDay()
        {
            DateTime start = new DateTime(2014, 7, 1);
            Random gen = new Random();

            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }


    }
}