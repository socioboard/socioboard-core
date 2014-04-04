using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using GlobusTwitterLib.App.Core;
using GlobusTwitterLib.Authentication;

namespace GlobusTwitterLib.Twitter.Core.SearchMethods
{
    public class Search
    {
        private XmlDocument xmlResult;
       

        public Search()
        {
            xmlResult = new XmlDocument();
        }

        #region Basic Authentcation

        #region SearchMethod

        /// <summary>
        /// This Method Will Get All Trends Of Twitter
        /// </summary>
        /// <param name="User">Twitter User Name And Password</param>
        /// <returns>Json Text Of Trends</returns>
        public XmlDocument SearchMethod(TwitterUser User, string SearchKey)
        {
            TwitterWebRequest twtWebReq = new TwitterWebRequest();
            string RequestUrl = Globals.SearchUrl + SearchKey;
            string response = twtWebReq.PerformWebRequest(User, RequestUrl, "Get", true, "");
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }
        #endregion

        #region Trends
        /// <summary>
        /// This Method Will Get All Trends Of Twitter
        /// </summary>
        /// <param name="User">Twitter User Name And Password</param>
        /// <returns>Json Text Of Trends</returns>
        public string Trends(TwitterUser User)
        {
            TwitterWebRequest twtWebReq = new TwitterWebRequest();
            string response = twtWebReq.PerformWebRequest(User, Globals.TrendsUrl, "Get", true, "");
            return response;
        }


        #endregion 

        #endregion

        #region OAuth

        #region SearchMethod

        /// <summary>
        /// This Method Will Get All Trends Of Twitter Using OAUTH
        /// </summary>
        /// <param name="User">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <returns>Json Text Of Trends</returns>
        public XmlDocument SearchMethod(oAuthTwitter OAuth, string SearchKey)
        {
            TwitterWebRequest twtWebReq = new TwitterWebRequest();
            string RequestUrl = Globals.SearchUrl + SearchKey;
            string response = OAuth.oAuthWebRequest(oAuthTwitter.Method.GET, RequestUrl, string.Empty);
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }
        #endregion

        #region Trends
        /// <summary>
        /// This Method Will Get All Trends Of Twitter Using OAUTH
        /// </summary>
        /// <param name="User">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <returns>Json Text Of Trends</returns>
        public string Trends(oAuthTwitter OAuth)
        {
            string response = OAuth.oAuthWebRequest(oAuthTwitter.Method.GET, Globals.TrendsUrl, String.Empty);
            return response;
        }


        #endregion

        #endregion

    }
}
