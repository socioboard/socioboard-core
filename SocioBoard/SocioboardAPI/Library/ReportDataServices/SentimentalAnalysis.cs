using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReportDataServices
{
    public class SentimentalAnalysis
    {
        public void GetSentiments()
        {
            string ret = string.Empty;
            Api.SentimentalAnalysis.SentimentalAnalysis ApiSentimentalAnalysis = new Api.SentimentalAnalysis.SentimentalAnalysis();
            int startingIndex = 0;
            int count = 10;
            while (true)
            {
                try
                {
                    ApiSentimentalAnalysis.FeedSentiment("0", "5");
                }
                catch (Exception ex)
                {
                }
                try
                {
                    ApiSentimentalAnalysis.Timeout = -1;
                    ret = ApiSentimentalAnalysis.FeedSentiment(startingIndex.ToString(), count.ToString());
                    if (ret == "no_data")
                    {
                        startingIndex = 0;
                    }
                    startingIndex += count;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(10*10*1000);
                   ret="Somthing went wrong"; 
                }
                Console.WriteLine(ret);
                Thread.Sleep(20*1000);
            }
        }
        public void GetSentimentsNew()
        {
            string ret = string.Empty;
            Api.SentimentalAnalysis.SentimentalAnalysis ApiSentimentalAnalysis = new Api.SentimentalAnalysis.SentimentalAnalysis();
            while (true)
            {
                try
                {
                    ret = ApiSentimentalAnalysis.FeedSentiment("0", "5");
                }
                catch (Exception ex)
                {
                    ret = "somthing went wrong";
                }

                Console.WriteLine(ret);
                Thread.Sleep(10*1000);
            }
        }
        public void GetFacebookFeedSentiments()
        {
            string ret = string.Empty;
            Api.SentimentalAnalysis.SentimentalAnalysis ApiSentimentalAnalysis = new Api.SentimentalAnalysis.SentimentalAnalysis();
            int startingIndex = 0;
            int count = 0;
            while (true)
            {
                try
                {
                    ApiSentimentalAnalysis.FacebookFeedSentimet("0", "5");
                }
                catch (Exception ex)
                {
                }
                try
                {
                    ApiSentimentalAnalysis.Timeout = -1;
                    ret = ApiSentimentalAnalysis.FacebookFeedSentimet(startingIndex.ToString(), count.ToString());
                    if (ret == "no_data")
                    {
                        startingIndex = 0;
                    }
                    startingIndex += count;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(60 * 10 * 1000);
                    ret = "Somthing went wrong"; 
                }

                Console.WriteLine(ret);
                Thread.Sleep(20*1000);
            }
        }


        public void GetFacebookMessageSentiments()
        {
            string ret = string.Empty;
            Api.SentimentalAnalysis.SentimentalAnalysis ApiSentimentalAnalysis = new Api.SentimentalAnalysis.SentimentalAnalysis();
            int startingIndex = 0;
            int count = 0;
            while (true)
            {
                try
                {
                    ApiSentimentalAnalysis.FacebookMessageSentiment("0", "5");
                }
                catch (Exception ex)
                {
                }
                try
                {
                    ApiSentimentalAnalysis.Timeout = -1;
                    ret = ApiSentimentalAnalysis.FacebookMessageSentiment(startingIndex.ToString(), count.ToString());
                    if (ret == "no_data")
                    {
                        startingIndex = 0;
                    }
                    startingIndex += count;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(60 * 10 * 1000);
                    ret = "Somthing went wrong";
                }

                Console.WriteLine(ret);
                Thread.Sleep(20 * 1000);
            }
        }

        public void GetTwitterFeedSentiments()
        {
            string ret = string.Empty;
            Api.SentimentalAnalysis.SentimentalAnalysis ApiSentimentalAnalysis = new Api.SentimentalAnalysis.SentimentalAnalysis();
            int startingIndex = 0;
            int count = 0;
            while (true)
            {
                try
                {
                    ApiSentimentalAnalysis.TwitterFeedSentiment("0", "5");
                }
                catch (Exception ex)
                {
                }
                try
                {
                    ApiSentimentalAnalysis.Timeout = -1;
                    ret = ApiSentimentalAnalysis.TwitterFeedSentiment(startingIndex.ToString(), count.ToString());
                    if (ret == "no_data")
                    {
                        startingIndex = 0;
                    }
                    startingIndex += count;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(60 * 10 * 1000);
                    ret = "Somthing went wrong";
                }

                Console.WriteLine(ret);
                Thread.Sleep(20 * 1000);
            }
        }


        public void GetTwitterMessageSentiments()
        {
            string ret = string.Empty;
            Api.SentimentalAnalysis.SentimentalAnalysis ApiSentimentalAnalysis = new Api.SentimentalAnalysis.SentimentalAnalysis();
            int startingIndex = 0;
            int count = 0;
            while (true)
            {
                try
                {
                    ApiSentimentalAnalysis.TwitterMessageSentiment("0", "5");
                }
                catch (Exception ex)
                {
                }
                try
                {
                    ApiSentimentalAnalysis.Timeout = -1;
                    ret = ApiSentimentalAnalysis.TwitterMessageSentiment(startingIndex.ToString(), count.ToString());
                    if (ret == "no_data")
                    {
                        startingIndex = 0;
                    }
                    startingIndex += count;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(60 * 10 * 1000);
                    ret = "Somthing went wrong";
                }

                Console.WriteLine(ret);
                Thread.Sleep(20 * 1000);
            }
        }

        public void GetDailyMotionData()
        {
            string ret = string.Empty;
            Api.SentimentalAnalysis.SentimentalAnalysis ApiSentimentalAnalysis = new Api.SentimentalAnalysis.SentimentalAnalysis();
            while (true)
            {
                try
                {
                    ApiSentimentalAnalysis.Timeout = -1;
                    ret = ApiSentimentalAnalysis.UpdateDailyMotionData();
                }
                catch (Exception ex)
                {
                    Thread.Sleep(60 * 10 * 1000);
                    ret = "Somthing went wrong";
                    
                }
                Console.WriteLine(ret);
                Thread.Sleep(20 * 1000);
            }
        
        }

    }
}
