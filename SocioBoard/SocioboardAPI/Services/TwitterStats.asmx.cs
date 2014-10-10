using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Collections;

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
    public class TwitterStats : System.Web.Services.WebService
    {
        TwitterStatsRepository _TwitterStatsRepository = new TwitterStatsRepository();

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllTwitterStatsDetails(string Profileid,string userid,string days)
        {
           
            int dayscount = Convert.ToInt32(days);
            Guid Userid=Guid.Parse(userid);
            ArrayList arrTwtStats = new ArrayList();
            ArrayList arrTwtBeforestats = new ArrayList();
            dynamic _TwitterStatsReport_1 =null;
            dynamic _TwitterStatsReport_2 = null;
          
            Domain.Socioboard.Domain.TwitterStatsReport _TwitterStats = new Domain.Socioboard.Domain.TwitterStatsReport();
            try
            {
               arrTwtStats = _TwitterStatsRepository.getTwitterStatsByIdDay(Profileid, Userid, dayscount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            try
            {
               arrTwtBeforestats = _TwitterStatsRepository.getTwitterStatsByIdbeforeDay(Profileid, Userid, dayscount);
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.StackTrace);
            }

            if (arrTwtStats.Count != 0)
            {
                _TwitterStatsReport_1 = arrTwtStats[0];

                if (arrTwtBeforestats.Count != 0)
                {
                    _TwitterStatsReport_2 = arrTwtBeforestats[0];


                    _TwitterStats.FollowerCount = Math.Abs(_TwitterStatsReport_1[4] - _TwitterStatsReport_2[4]);
                    _TwitterStats.FollowingCount = Math.Abs(_TwitterStatsReport_1[3] - _TwitterStatsReport_2[3]);
                    _TwitterStats.DMRecievedCount = Math.Abs(_TwitterStatsReport_1[5] - _TwitterStatsReport_2[5]);
                    _TwitterStats.DMSentCount = Math.Abs(_TwitterStatsReport_1[6] - _TwitterStatsReport_2[6]);
                    _TwitterStats.Age1820 = _TwitterStatsReport_1[9] - _TwitterStatsReport_2[9];
                    _TwitterStats.Age2124 = _TwitterStatsReport_1[10] - _TwitterStatsReport_2[10];
                    _TwitterStats.Age2534 = _TwitterStatsReport_1[11] - _TwitterStatsReport_2[11];
                    _TwitterStats.Age3544 = _TwitterStatsReport_1[12] - _TwitterStatsReport_2[12];
                    _TwitterStats.Age4554 = _TwitterStatsReport_1[13] - _TwitterStatsReport_2[13];
                    _TwitterStats.Age5564 = _TwitterStatsReport_1[14] - _TwitterStatsReport_2[14];
                    _TwitterStats.Age65 = _TwitterStatsReport_1[15] - _TwitterStatsReport_2[15];

                }
                else 
                {
                    _TwitterStats.FollowerCount = _TwitterStatsReport_1[4] ;
                    _TwitterStats.FollowingCount = _TwitterStatsReport_1[3] ;
                    _TwitterStats.DMRecievedCount =_TwitterStatsReport_1[5];
                    _TwitterStats.DMSentCount = _TwitterStatsReport_1[6];
                    _TwitterStats.Age1820 = _TwitterStatsReport_1[9];
                    _TwitterStats.Age2124 = _TwitterStatsReport_1[10];
                    _TwitterStats.Age2534 = _TwitterStatsReport_1[11];
                    _TwitterStats.Age3544 = _TwitterStatsReport_1[12];
                    _TwitterStats.Age4554 = _TwitterStatsReport_1[13];
                    _TwitterStats.Age5564 = _TwitterStatsReport_1[14];
                    _TwitterStats.Age65 = _TwitterStatsReport_1[15];

                }             
            }

            List<Domain.Socioboard.Domain.TwitterStatsReport> _lstTwitterStats = new List<Domain.Socioboard.Domain.TwitterStatsReport>();
   
            string Engagement = Engagementreport(Profileid, Userid, dayscount);
            string influence = Influencereport(Profileid, Userid, dayscount);
            string datetime = DateTimereport(Profileid, Userid, dayscount);

            TwitterMessage _TwitterMessage = new TwitterMessage();
            string twtmention = _TwitterMessage.getTwtMention(Profileid, Userid, dayscount);
            string twtretweet = _TwitterMessage.getRetweets(Profileid, Userid, dayscount);

            TwitterAccount _TwitterAccount = new TwitterAccount();
            Domain.Socioboard.Domain.TwitterAccount obj = _TwitterAccount.AcccountDetails(Profileid,Userid);

            _TwitterStats.TwtProfImgUrl = obj.ProfileImageUrl;
            _TwitterStats.TwtUserName = obj.TwitterScreenName;
            _TwitterStats.days = dayscount;
            _TwitterStats.TwitterId = Profileid;
            _TwitterStats.Engagement = Engagement;
            _TwitterStats.Influence = influence;
            _TwitterStats.EntryDate = datetime;
            _TwitterStats.TwtRetweet = Convert.ToInt32(twtretweet);
            _TwitterStats.TwtMention = Convert.ToInt32(twtmention); 


            _lstTwitterStats.Add(_TwitterStats);

           
            return new JavaScriptSerializer().Serialize(_lstTwitterStats);
        }

        public string Engagementreport(string profileid, Guid userid, int days)
        {
            string strArray = string.Empty;
            try
            {
                TwitterStatsRepository objtwtstatsRepo = new TwitterStatsRepository();
                ArrayList alstTwt = objtwtstatsRepo.getTwitterStatsById(profileid,userid ,days);
                int increament = 0;
                if (alstTwt.Count > 5)
                {
                    increament = alstTwt.Count / 5;
                }

                //  strArray = "[";
                string str = string.Empty;
                int cnt = 0;
                if (alstTwt.Count > 0)
                {
                    if (increament > 0)
                    {
                        for (int i = 0; i < alstTwt.Count; i = i + increament)
                        {
                            Array temp = (Array)alstTwt[i];
                            strArray = strArray + temp.GetValue(7).ToString() + ",";
                            cnt++;
                        }
                    }
                    else
                    {
                        foreach (var itemTS in alstTwt)
                        {
                            Array temp = (Array)itemTS;
                            strArray = strArray + temp.GetValue(7).ToString() + ",";
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

                strArray = str + strArray;
                strArray = strArray.Substring(0, strArray.Length - 1);

                //  strArray += "]";
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }

            return strArray;
        
        }

        public string Influencereport(string profileid, Guid userid, int days)
        {
            string strArray = string.Empty;
            try
            {
                TwitterStatsRepository objtwtstatsRepo = new TwitterStatsRepository();
                ArrayList alstTwt = objtwtstatsRepo.getTwitterStatsById(profileid,userid, days);
                int increament = 0;
                if (alstTwt.Count > 5)
                {
                    increament = alstTwt.Count / 5;
                }

                //  strArray = "[";
                string str = string.Empty;
                int cnt = 0;
                if (alstTwt.Count > 0)
                {
                    if (increament > 0)
                    {
                        for (int i = 0; i < alstTwt.Count; i = i + increament)
                        {
                            Array temp = (Array)alstTwt[i];
                            strArray = strArray + temp.GetValue(8).ToString() + ",";
                            cnt++;
                        }
                    }
                    else
                    {
                        foreach (var itemTS in alstTwt)
                        {
                            Array temp = (Array)itemTS;
                            strArray = strArray + temp.GetValue(8).ToString() + ",";
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

                strArray = str + strArray;
                strArray = strArray.Substring(0, strArray.Length - 1);

                //  strArray += "]";
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
           
            return strArray;
        
        
        }

        public string DateTimereport(string profileid, Guid userid, int days)
        { 
           string strArray = string.Empty;
            try
            {
                TwitterStatsRepository objtwtstatsRepo = new TwitterStatsRepository();
                ArrayList alstTwt = objtwtstatsRepo.getTwitterStatsById(profileid, userid,days);
                int increament = 0;
                if (alstTwt.Count > 5)
                {
                    increament = alstTwt.Count / 5;
                }

                //  strArray = "[";
                string str = string.Empty;
                int cnt = 0;
                if (alstTwt.Count > 0)
                {
                    if (increament > 0)
                    {
                        for (int i = 0; i < alstTwt.Count; i = i + increament)
                        {
                            Array temp = (Array)alstTwt[i];
                            strArray = strArray + temp.GetValue(16).ToString() + ",";
                            cnt++;
                        }
                    }
                    else
                    {
                        foreach (var itemTS in alstTwt)
                        {
                            Array temp = (Array)itemTS;
                            strArray = strArray + temp.GetValue(16).ToString() + ",";
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

                strArray = str + strArray;
                strArray = strArray.Substring(0, strArray.Length - 1);

                //  strArray += "]";
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }

            //for (int i = 0; i < 7; i++)
            //{

            //    strArray += RandomDay() + ",";
            //    //strImpression += getrandomno() + ",";
            //    System.Threading.Thread.Sleep(10);
            //}
            return strArray;
        
        
        }




    }
}
