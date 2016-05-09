using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Api.Socioboard.Helper
{
    public static class TwitterScrapeHelper
    {
        public static List<string> GetLatestTrendsFromTwiter()
        {
            ILog logger = LogManager.GetLogger(typeof(TwitterScrapeHelper));
            List<string> lstOfTrends = new List<string>();
            try
            {
                GlobusHttpHelper objGlobusHttpHelper = new GlobusHttpHelper();
                string homepageUrl = "https://twitter.com/";
                string trendsUrl = "https://twitter.com/i/trends?k=0b49e0976b&pc=true&personalized=false&show_context=true&src=module&woeid=23424977";

                string homePageResponse = objGlobusHttpHelper.getHtmlfromUrl(new Uri(trendsUrl), "", "");

                try
                {
                    string[] GetTrends = Regex.Split(homePageResponse, "data-trend-name=");
                    GetTrends = GetTrends.Skip(1).ToArray();
                    foreach (string item in GetTrends)
                    {
                        try
                        {
                            string trend = Utils.getBetween(item, "\"", "\\\"").Replace("&#39;", "'");
                            lstOfTrends.Add(trend);
                        }
                        catch (Exception ex)
                        {
                            //GlobusLogHelper.log.Error("Error Get Current Trends ==> " + ex.Message);
                            logger.Error("Error Get Current Trends ==> " + ex.Message);
                            logger.Error( ex.StackTrace);

                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("Error Get Current Trends ==> " + ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error Get Current Trends ==> " + ex.Message);
                logger.Error(ex.StackTrace);
            }
            return lstOfTrends;
        }
    }
}