using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using GlobusTwitterLib.App.Core;
using GlobusTwitterLib.Authentication;

namespace GlobusTwitterLib.Twitter.Core.TimeLineMethods
{
    public class TimeLine
    {
        private XmlDocument xmlResult;

        public TimeLine()
        {
            xmlResult = new XmlDocument();
        }

        #region Basic Authentication 
        /// <summary>
        /// Get All Mentions Of User
        /// </summary>
        /// <param name="User">Twitter User And Password</param>
        /// <param name="Count">Number Of Tweets</param>
        /// <returns>Return XmlText Of Mentionc Of User</returns>
        public XmlDocument Status_Mention(TwitterUser User, string Count)
        {
            TwitterWebRequest twtWebReq = new TwitterWebRequest();
            string RequestUrl = Globals.MentionUrl + Count;
            string response = twtWebReq.PerformWebRequest(User, RequestUrl, "Get", true, "");
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }

        /// <summary>
        /// Get All Tweets Sent By User And Friend
        /// </summary>
        /// <param name="User">Twitter User And Password</param>
        /// <param name="Count">Number Of Tweets</param>
        /// <returns>Return XmlText Of Tweets Sent By User And Friend</returns>
        public XmlDocument Status_HomeTimeLine(TwitterUser User, string Count)
        {
            TwitterWebRequest twtWebReq = new TwitterWebRequest();
            string RequestUrl = Globals.HomeTimeLineUrl + Count;
            string response = twtWebReq.PerformWebRequest(User, RequestUrl, "Get", true, "");
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }

        /// <summary>
        /// Get All Tweets Sent By User
        /// </summary>
        /// <param name="User">Twitter User And Password</param>
        /// <param name="Count">Number Of Tweets</param>
        /// <returns>Return XmlText Of Tweets Sent By User</returns>
        public XmlDocument Status_UserTimeLine(TwitterUser User, string Count)
        {
            TwitterWebRequest twtWebReq = new TwitterWebRequest();
            string RequestUrl = Globals.UserTimeLineUrl + Count;
            string response = twtWebReq.PerformWebRequest(User, RequestUrl, "Get", true, "");
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }

        /// <summary>
        /// Get All ReTweets Sent By User
        /// </summary>
        /// <param name="User">Twitter User And Password</param>
        /// <param name="Count">Number Of ReTweets</param>
        /// <returns>Return XmlText Of ReTweets</returns>
        public XmlDocument Status_RetweetedByMe(TwitterUser User, string Count)
        {
            TwitterWebRequest twtWebReq = new TwitterWebRequest();
            string RequestUrl = Globals.RetweetedByMeUrl + Count;
            string response = twtWebReq.PerformWebRequest(User, RequestUrl, "Get", true, "");
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        } 
        #endregion

        #region OAuth
        /// <summary>
        /// Get All Mentions Of User Using OAUTH
        /// </summary>
        /// <param name="OAuth">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="Count">Number Of Tweets</param>
        /// <returns>Return XmlText Of Mentionc Of User</returns>
        public XmlDocument Status_Mention(oAuthTwitter OAuth, string Count)
        {
            string RequestUrl = Globals.MentionUrl + Count;
            string response = OAuth.oAuthWebRequest(oAuthTwitter.Method.GET, RequestUrl, string.Empty);
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }

        /// <summary>
        /// Get All Tweets Sent By User And Friend Using OAUTH
        /// </summary>
        /// <param name="OAuth">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="Count">Number Of Tweets</param>
        /// <returns>Return XmlText Of Tweets Sent By User And Friend</returns>
        public XmlDocument Status_HomeTimeLine(oAuthTwitter OAuth, string Count)
        {
            string RequestUrl = Globals.HomeTimeLineUrl + Count;
            string response = OAuth.oAuthWebRequest(oAuthTwitter.Method.GET, RequestUrl, string.Empty);
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }

        /// <summary>
        /// Get All Tweets Sent By User Using OAUTH
        /// </summary>
        /// <param name="OAuth">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="Count">Number Of Tweets</param>
        /// <returns>Return XmlText Of Tweets Sent By User</returns>
        public XmlDocument Status_UserTimeLine(oAuthTwitter OAuth, string Count,string ScreenName)
        {
            string RequestUrl = Globals.UserTimeLineUrl + ScreenName+"&count="+Count;
            string response = OAuth.oAuthWebRequest(oAuthTwitter.Method.GET, RequestUrl, string.Empty);
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }

        /// <summary>
        /// Get All ReTweets Sent By User Using OAUTH
        /// </summary>
        /// <param name="OAuth">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="Count">Number Of ReTweets</param>
        /// <returns>Return XmlText Of ReTweets</returns>
        public XmlDocument Status_RetweetedByMe(oAuthTwitter OAuth, string Count)
        {
            string RequestUrl = Globals.RetweetedByMeUrl + Count;
            string response = OAuth.oAuthWebRequest(oAuthTwitter.Method.GET, RequestUrl, string.Empty);
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }  
        #endregion
    }
}
