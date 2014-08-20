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
        public string getNewFollowers(string profileId, int days)
        {
            string strTwtArray = string.Empty;
            // string strnewTwtArray = string.Empty;

            int strTwtArray1 = 0;
            int strnewTwtArray1 = 0;
            try
            {


                TwitterStatsRepository objtwtStatsRepo = new TwitterStatsRepository();

                ArrayList arrTwtStats = objtwtStatsRepo.getTwitterStatsByIdDay(profileId, days);

                ArrayList arrTwtBeforestats = objtwtStatsRepo.getTwitterStatsByIdbeforeDay(profileId, days);


                if (arrTwtBeforestats.Count == 0)
                {
                    ArrayList arrTwtBeforestat = objtwtStatsRepo.getTwitterStByIdbeforeDay(profileId, days);
                    Array temp1 = (Array)arrTwtBeforestat[0];
                    strnewTwtArray1 = Convert.ToInt16(temp1.GetValue(4));
                }
                else
                {
                    Array temp = (Array)arrTwtBeforestats[0];
                    strnewTwtArray1 = Convert.ToInt16(temp.GetValue(4));
                }

                try
                {
                    Array temp2 = (Array)arrTwtStats[0];
                    strTwtArray1 = Convert.ToInt16(temp2.GetValue(4));
                }
                catch (Exception Err)
                {
                    Console.Write(Err.Message.ToString());
                }


                strnewTwtArray1 = (strTwtArray1 - strnewTwtArray1);
                if (strnewTwtArray1 > 0)
                {
                    strnewTwtArray1 = strnewTwtArray1;
                }
                else
                {
                    strnewTwtArray1 = 0;
                }

                strTwtArray = (strnewTwtArray1.ToString()) + ",";
                string str = string.Empty;
                if (arrTwtStats.Count < 7)
                {
                    for (int i = 0; i < 7 - arrTwtStats.Count; i++)
                    {
                        str += "0,";
                    }
                }
                strTwtArray = str + strTwtArray;
                strTwtArray = strTwtArray.Substring(0, strTwtArray.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.Message.ToString());
            }
            return strTwtArray;
        }


        public string getNewFollowing(string profileId, int days)
        {
            string strTwtFollowing = string.Empty;
            int strTwtFollowing1 = 0;
            int strnewTwtFollowing = 0;
            try
            {

                TwitterStatsRepository objtwtStatsRepo = new TwitterStatsRepository();
                ArrayList arrTwtStats = objtwtStatsRepo.getTwitterStatsByIdDay(profileId, days);
                ArrayList arrTwtBeforestats = objtwtStatsRepo.getTwitterStatsByIdbeforeDay(profileId, days);

                if (arrTwtBeforestats.Count == 0)
                {
                    ArrayList arrTwtBeforestat = objtwtStatsRepo.getTwitterStByIdbeforeDay(profileId,days);
                    Array temp1 = (Array)arrTwtBeforestat[0];
                    strnewTwtFollowing = Convert.ToInt16(temp1.GetValue(3));
                }
                else
                {
                    Array temp = (Array)arrTwtBeforestats[0];
                    strnewTwtFollowing = Convert.ToInt16(temp.GetValue(3));
                }

                try
                {
                    Array temp2 = (Array)arrTwtStats[0];
                    strTwtFollowing1 = Convert.ToInt16(temp2.GetValue(3));
                }
                catch (Exception Err)
                {
                    Console.Write(Err.Message.ToString());
                }


                strTwtFollowing1 = (strTwtFollowing1 - strnewTwtFollowing);
                if (strTwtFollowing1 > 0)
                {
                    strTwtFollowing1 = strTwtFollowing1;
                }
                else
                {
                    strTwtFollowing1 = 0;
                }

                strTwtFollowing = (strTwtFollowing1.ToString()) + ",";
                string str = string.Empty;
                if (arrTwtStats.Count < 7)
                {
                    for (int i = 0; i < 7 - arrTwtStats.Count; i++)
                    {
                        str += "0,";
                    }
                }
                strTwtFollowing = str + strTwtFollowing;
                strTwtFollowing = strTwtFollowing.Substring(0, strTwtFollowing.Length - 1);

            }
            catch (Exception Err)
            {
                Console.Write(Err.Message.ToString());
            }
            return strTwtFollowing;
        }


        public string GetFollowersAgeWise(User user, int days)
        {
            string strTwtAgeArray = string.Empty;
            try
            {
                TwitterStatsRepository objtwtStatsRepo = new TwitterStatsRepository();
                object arrTwtStats = objtwtStatsRepo.getFollowersAgeCount(user.Id, days);
                string[] arr = ((IEnumerable)arrTwtStats).Cast<object>().Select(x => x.ToString()).ToArray();
                strTwtAgeArray = "0,";
                for (int i = 0; i < arr.Count(); i++)
                {
                    strTwtAgeArray += arr[i] + ",";
                }
                strTwtAgeArray = strTwtAgeArray.Substring(0, strTwtAgeArray.Length - 1);
                //strTwtArray += "]";
            }
            catch (Exception Err)
            {
                Console.Write(Err.Message.ToString());
            }
            return strTwtAgeArray;
        }

        public string getIncomingMsg(string profileId, int days)
        {
            string strArray = string.Empty;
            try
            {
                TwitterMessageRepository objtwttatsRepo = new TwitterMessageRepository();
                ArrayList alstTwt = objtwttatsRepo.gettwtMessageStatsByProfileId(profileId, days);
                string str = string.Empty;
                //  strArray = "[";
                int cnt = 0;
                if (alstTwt.Count > 0)
                {
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
                        str = str + "0,";
                    }
                }
                strArray = str + strArray.Substring(0, strArray.Length - 1);
                // strArray += "]";              
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }

            return strArray;
        }

        public string getSentMsg(string profileId, int days)
        {
            string strArray = string.Empty;
            try
            {
                TwitterMessageRepository objtwttatsRepo = new TwitterMessageRepository();
                //  ArrayList alstTwt = objtwttatsRepo.gettwtFeedsStatsByProfileId(user.Id, profileId,days);
                ArrayList alstTwt = objtwttatsRepo.gettwtscheduledByProfileId(profileId, days);
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

        public string getDirectMessageRecieve(string profileId, int days)
        {
            TwitterDirectMessageRepository objtwtdm = new TwitterDirectMessageRepository();
            string strArray = string.Empty;
            try
            {
                int cnt = 0;
                ArrayList alstTwt = objtwtdm.gettwtDMRecieveStatsByProfileId(profileId, days);
                string str = string.Empty;
                if (alstTwt.Count > 0)
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
                        str = str + "0,";
                    }
                }
                strArray = str + strArray.Substring(0, strArray.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return strArray;
        }

        public string getDirectMessageSent(string profileId, int days)
        {
            TwitterDirectMessageRepository objtwtdm = new TwitterDirectMessageRepository();
            string strArray = string.Empty;
            try
            {
                int cnt = 0;
                ArrayList alstTwt = objtwtdm.gettwtDMSendStatsByProfileId(profileId, days);
                string str = string.Empty;
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
                        str = str + "0,";
                    }
                }
                strArray = str + strArray.Substring(0, strArray.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return strArray;
        }

        public string getRetweets(string profileId, int days)
        {
            TwitterMessageRepository objretwt = new TwitterMessageRepository();
            string strArray = string.Empty;
            string str = string.Empty;
            try
            {
                int cnt = 0;
                ArrayList alstTwt = objretwt.getRetweetStatsByProfileId(profileId, days);

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

    

        public string getEngagements(string profileId, int days)
        {
            string strArray = string.Empty;
            //try
            //{
            //    TwitterStatsRepository objtwtstatsRepo = new TwitterStatsRepository();
            //    ArrayList alstTwt = objtwtstatsRepo.getTwitterStatsById( profileId, days);
            //    int increament = 0;
            //    if (alstTwt.Count > 5)
            //    {
            //        increament = alstTwt.Count / 5;
            //    }

            //    //  strArray = "[";
            //    string str = string.Empty;
            //    int cnt = 0;
            //    if (alstTwt.Count > 0)
            //    {
            //        if (increament > 0)
            //        {
            //            for (int i = 0; i < alstTwt.Count; i = i + increament)
            //            {
            //                Array temp = (Array)alstTwt[i];
            //                strArray = strArray + temp.GetValue(7).ToString() + ",";
            //                cnt++;
            //            }
            //        }
            //        else
            //        {
            //            foreach (var itemTS in alstTwt)
            //            {
            //                Array temp = (Array)itemTS;
            //                strArray = strArray + temp.GetValue(7).ToString() + ",";
            //                cnt++;
            //            }
            //        }

            //    }
            //    if (cnt < 7)
            //    {
            //        for (int j = 0; j < 7 - cnt; j++)
            //        {
            //            str = str + "0,";
            //        }
            //    }

            //    strArray = str + strArray;
            //    strArray = strArray.Substring(0, strArray.Length - 1);

            //    //  strArray += "]";
            //}
            //catch (Exception Err)
            //{
            //    Console.Write(Err.StackTrace);
            //}

            for (int i = 0; i < 7; i++)
            {

                // strArray += RandomDay() + ",";
                strArray += getrandomno() + ",";
                System.Threading.Thread.Sleep(10);
            }
            return strArray;
        }

        public string getInfluence( string profileId, int days)
        {
            string strArray = string.Empty;
            //try
            //{
            //    TwitterStatsRepository objtwtstatsRepo = new TwitterStatsRepository();
            //    ArrayList alstTwt = objtwtstatsRepo.getTwitterStatsById(profileId, days);
            //    int increament = 0;
            //    if (alstTwt.Count > 5)
            //    {
            //        increament = alstTwt.Count / 5;
            //    }

            //    //  strArray = "[";
            //    string str = string.Empty;
            //    int cnt = 0;
            //    if (alstTwt.Count > 0)
            //    {
            //        if (increament > 0)
            //        {
            //            for (int i = 0; i < alstTwt.Count; i = i + increament)
            //            {
            //                Array temp = (Array)alstTwt[i];
            //                strArray = strArray + temp.GetValue(8).ToString() + ",";
            //                cnt++;
            //            }
            //        }
            //        else
            //        {
            //            foreach (var itemTS in alstTwt)
            //            {
            //                Array temp = (Array)itemTS;
            //                strArray = strArray + temp.GetValue(8).ToString() + ",";
            //                cnt++;
            //            }
            //        }

            //    }
            //    if (cnt < 7)
            //    {
            //        for (int j = 0; j < 7 - cnt; j++)
            //        {
            //            str = str + "0,";
            //        }
            //    }

            //    strArray = str + strArray;
            //    strArray = strArray.Substring(0, strArray.Length - 1);

            //    //  strArray += "]";
            //}
            //catch (Exception Err)
            //{
            //    Console.Write(Err.StackTrace);
            //}

            for (int i = 0; i < 7; i++)
            {

               // strArray += RandomDay() + ",";
                strArray += getrandomno() + ",";
                System.Threading.Thread.Sleep(10);
            }
            return strArray;
        }

        public string getTwtMention( string profileId, int days)
        {
            TwitterMessageRepository objretwt = new TwitterMessageRepository();
            string strArray = string.Empty;
            string str = string.Empty;
            try
            {
                int cnt = 0;
                ArrayList alstTwt = objretwt.getMentionStatsByProfileId(profileId, days);

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

        public string getdate(string profileId, int days)
        {
            string strArray = string.Empty;
            //try
            //{
            //    TwitterStatsRepository objtwtstatsRepo = new TwitterStatsRepository();
            //    ArrayList alstTwt = objtwtstatsRepo.getTwitterStatsById(profileId, days);
            //    int increament = 0;
            //    if (alstTwt.Count > 5)
            //    {
            //        increament = alstTwt.Count / 5;
            //    }

            //    //  strArray = "[";
            //    string str = string.Empty;
            //    int cnt = 0;
            //    if (alstTwt.Count > 0)
            //    {
            //        if (increament > 0)
            //        {
            //            for (int i = 0; i < alstTwt.Count; i = i + increament)
            //            {
            //                Array temp = (Array)alstTwt[i];
            //                strArray = strArray + temp.GetValue(16).ToString() + ",";
            //                cnt++;
            //            }
            //        }
            //        else
            //        {
            //            foreach (var itemTS in alstTwt)
            //            {
            //                Array temp = (Array)itemTS;
            //                strArray = strArray + temp.GetValue(16).ToString() + ",";
            //                cnt++;
            //            }
            //        }




            //    }
            //    if (cnt < 7)
            //    {
            //        for (int j = 0; j < 7 - cnt; j++)
            //        {
            //            str = str + "0,";
            //        }
            //    }

            //    strArray = str + strArray;
            //    strArray = strArray.Substring(0, strArray.Length - 1);

            //    //  strArray += "]";
            //}
            //catch (Exception Err)
            //{
            //    Console.Write(Err.StackTrace);
            //}

            for (int i = 0; i < 7; i++)
            {

                strArray += RandomDay() + ",";
                //strImpression += getrandomno() + ",";
                System.Threading.Thread.Sleep(10);
            }
            return strArray;
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