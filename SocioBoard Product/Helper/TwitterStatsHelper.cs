using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Model;
using System.Collections;
using SocioBoard.Domain;

namespace SocioBoard.Helper
{
    public class TwitterStatsHelper
    {
        public string getNewFollowers(User user,string profileId,int days)
        {
            string strTwtArray = string.Empty;
            try
            {

                //SocioBoard.Domain.User user = (User)Session["LoggedUser"];
                TwitterStatsRepository objtwtStatsRepo = new TwitterStatsRepository();
                ArrayList arrTwtStats = objtwtStatsRepo.getTwitterStatsByIdDay(user.Id, profileId, days);
                string str = string.Empty;
                foreach (var item in arrTwtStats)
                {
                    Array temp = (Array)item;
                    strTwtArray += (temp.GetValue(4).ToString()) + ",";
                }
                if (arrTwtStats.Count < 7)
                {
                    for (int i = 0; i < 7 - arrTwtStats.Count; i++)
                    {
                        str += "0,";
                    }
                }
                strTwtArray = str + strTwtArray;
                strTwtArray = strTwtArray.Substring(0, strTwtArray.Length - 1);               
                //  strTwtArray += "]";
            }
            catch (Exception Err)
            {
               Console.Write(Err.Message.ToString());
            }
            return strTwtArray;
        }

        public string getNewFollowing(User user, string profileId,int days)
        {
            string strTwtFollowing = string.Empty;
            try
            {

                //SocioBoard.Domain.User user = (User)Session["LoggedUser"];
                TwitterStatsRepository objtwtStatsRepo = new TwitterStatsRepository();
                ArrayList arrTwtStats = objtwtStatsRepo.getTwitterStatsByIdDay(user.Id, profileId, days);
                string str = string.Empty;
                foreach (var item in arrTwtStats)
                {
                    Array temp = (Array)item;
                    strTwtFollowing += (temp.GetValue(3).ToString()) + ",";
                }
                if (arrTwtStats.Count < 7)
                {
                    for (int i = 0; i < 7 - arrTwtStats.Count; i++)
                    {
                        str += "0,";
                    }
                }
                strTwtFollowing = str + strTwtFollowing;
                strTwtFollowing = strTwtFollowing.Substring(0, strTwtFollowing.Length - 1);
                //  strTwtArray += "]";
            }
            catch (Exception Err)
            {
                Console.Write(Err.Message.ToString());
            }
            return strTwtFollowing;
        }

        public string GetFollowersAgeWise(User user,int days)
        {
            string strTwtAgeArray = string.Empty;
            try
            {
                TwitterStatsRepository objtwtStatsRepo = new TwitterStatsRepository();
                object arrTwtStats = objtwtStatsRepo.getFollowersAgeCount(user.Id,days);
                string[] arr = ((IEnumerable)arrTwtStats).Cast<object>().Select(x => x.ToString()).ToArray();
                strTwtAgeArray="0,";
                for (int i = 0; i < arr.Count(); i++)
                {
                    strTwtAgeArray += arr[i] + ",";
                }
                strTwtAgeArray = strTwtAgeArray.Substring(0, strTwtAgeArray.Length - 1) ;
                //strTwtArray += "]";
            }
            catch (Exception Err)
            {
                Console.Write(Err.Message.ToString());
            }
            return strTwtAgeArray;
        }

        public string getIncomingMsg(User user, string profileId, int days)
        {
            string strArray = string.Empty;
            try
            {
                TwitterMessageRepository objtwttatsRepo = new TwitterMessageRepository();
                ArrayList alstTwt = objtwttatsRepo.gettwtMessageStatsByProfileId(user.Id, profileId, days);
                string str = string.Empty;
              //  strArray = "[";
                int cnt = 0;
                if (alstTwt.Count > 0)
                {
                    for (int i = 0; i < alstTwt.Count; i++)
                    {
                        strArray = strArray +alstTwt[i].ToString() + ",";
                        cnt++;
                    }
                }
                if (cnt < 7)
                {
                    for (int j = 0; j < 7 - cnt; j++)
                    {
                        str=str + "0,";
                    }
                }
                strArray = str + strArray.Substring(0,strArray.Length-1);
             // strArray += "]";              
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
           
            return strArray;
        }

        public string getSentMsg(User user, string profileId,int days)
        {
            string strArray = string.Empty;
            try
            {
                TwitterMessageRepository objtwttatsRepo = new TwitterMessageRepository();
                ArrayList alstTwt = objtwttatsRepo.gettwtFeedsStatsByProfileId(user.Id, profileId,days);
              //  strArray = "[";
                int cnt = 0;
                if (alstTwt.Count > 0)
                {
                    for (int i = 0; i < alstTwt.Count; i++)
                    {
                        strArray = strArray + alstTwt[i].ToString();
                        cnt++;
                    }
                }
                if (cnt < 7)
                {
                    for (int j = 0; j < 7 - cnt; j++)
                    {
                        strArray = strArray + ",0";
                    }
                }
              //  strArray += "]";
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return strArray;
        }

        public string getDirectMessageRecieve(User user, string profileId)
        {
            TwitterDirectMessageRepository objtwtdm = new TwitterDirectMessageRepository();
            string strArray = string.Empty;
            try
            {
                int cnt=0;
                ArrayList alstTwt = objtwtdm.gettwtDMRecieveStatsByProfileId(user.Id, profileId);
                if (alstTwt != null)
                {
                  //  strArray = "[";
                    for (int i = 0; i < alstTwt.Count; i++)
                    {
                        strArray = strArray + alstTwt[i].ToString() + ",";
                        cnt++;
                    }
                }
                if (cnt < 7)
                { 
                    for(int j=0;j<7-cnt;j++)
                    {
                        strArray = strArray + "0,";
                    }
                }
                strArray = strArray.Substring(0, strArray.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return strArray;
        }

        public string getDirectMessageSent(User user, string profileId)
        {
            TwitterDirectMessageRepository objtwtdm = new TwitterDirectMessageRepository();
            string strArray = string.Empty;
            try
            {
                int cnt = 0;
                ArrayList alstTwt = objtwtdm.gettwtDMSendStatsByProfileId(user.Id, profileId);
                if (alstTwt != null)
                {
                    //  strArray = "[";
                    for (int i = 0; i < alstTwt.Count; i++)
                    {
                        strArray = strArray + alstTwt[i].ToString() + ",";
                        cnt++;
                    }
                }
                if (cnt < 7)
                {
                    for (int j = 0; j < 7 - cnt; j++)
                    {
                        strArray = strArray + "0,";
                    }
                }
                strArray = strArray.Substring(0, strArray.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return strArray;
        }

        public string getRetweets(User user, string profileId,int days)
        { 
            TwitterMessageRepository objretwt = new TwitterMessageRepository();
            string strArray = string.Empty;
            string str = string.Empty;
            try
            {
                int cnt = 0;
                ArrayList alstTwt = objretwt.getRetweetStatsByProfileId(user.Id, profileId,days);
                
                if (alstTwt != null)
                {
                    //  strArray = "[";
                    for (int i = 0; i < alstTwt.Count; i++)
                    {
                        strArray = strArray + alstTwt[i].ToString() + ",";
                        cnt++;
                    }
                }
                if (cnt < 7)
                {
                    for (int j = 0; j < 7 - cnt; j++)
                    {
                        //strArray = strArray + "0,";
                        str += "0,";
                    }
                }
                //strArray = strArray.Substring(0, strArray.Length - 1);
                strArray = str + strArray;
                strArray = strArray.Substring(0, strArray.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return strArray;
           
        }

        //public string getRetweets(User user, string profileId)
        //{
        //    TwitterMessageRepository objretwt = new TwitterMessageRepository();
        //    string strArray = string.Empty;
        //    try
        //    {
        //        int cnt = 0;
        //        ArrayList alstTwt = objretwt.getRetweetStatsByProfileId(user.Id, profileId);

        //        if (alstTwt != null)
        //        {
        //            //  strArray = "[";
        //            for (int i = 0; i < alstTwt.Count; i++)
        //            {
        //                strArray = strArray + alstTwt[i].ToString() + ",";
        //                cnt++;
        //            }
        //        }
        //        if (cnt < 7)
        //        {
        //            for (int j = 0; j < 7 - cnt; j++)
        //            {
        //                strArray = strArray + "0,";
        //            }
        //        }
        //        strArray = strArray.Substring(0, strArray.Length - 1);
        //    }
        //    catch (Exception Err)
        //    {
        //        Console.Write(Err.StackTrace);
        //    }
        //    return strArray;

        //}

        public string getEngagements(User user, string profileId,int days)
        {
            string strArray = string.Empty;
            try
            {
                TwitterStatsRepository objtwtstatsRepo = new TwitterStatsRepository();
                ArrayList alstTwt = objtwtstatsRepo.getTwitterStatsById(user.Id, profileId,days);
                //  strArray = "[";
                int cnt = 0;
                if (alstTwt.Count > 0)
                {
                    foreach(var itemTS in alstTwt)
                    {
                        Array temp = (Array)itemTS;
                        strArray = strArray + temp.GetValue(7).ToString();
                        cnt++;
                    }                   
                }
                if (cnt < 7)
                {
                    for (int j = 0; j < 7 - cnt; j++)
                    {
                        strArray = strArray + ",0";
                    }
                }
                //  strArray += "]";
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return strArray;
        }

        public string getInfluence(User user, string profileId,int days)
        {
            string strArray = string.Empty;
            try
            {
                TwitterStatsRepository objtwtstatsRepo = new TwitterStatsRepository();
                ArrayList alstTwt = objtwtstatsRepo.getTwitterStatsById(user.Id, profileId,days);
                //  strArray = "[";
                int cnt = 0;
                string str = string.Empty;
                if (alstTwt.Count > 0)
                {
                    foreach(var itemInf in alstTwt)
                    {
                        Array temp = (Array)itemInf;
                        strArray = strArray + temp.GetValue(8).ToString() + ",";
                        cnt++;
                    }
                }
                if (cnt < 7)
                {
                    for (int j = 0; j < 7 - cnt; j++)
                    {
                        str=str + "0,";
                    }
                }
                strArray = str + strArray.Substring(0,strArray.Length-1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return strArray;
        }
      
    }
}