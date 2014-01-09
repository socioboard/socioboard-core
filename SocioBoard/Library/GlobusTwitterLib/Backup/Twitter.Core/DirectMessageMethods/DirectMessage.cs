using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using GlobusTwitterLib.App.Core;
using GlobusTwitterLib.Authentication;

namespace GlobusTwitterLib.Twitter.Core.DirectMessageMethods
{
    public class DirectMessage
    {
        private XmlDocument xmlResult;

        public DirectMessage()
        {
            xmlResult = new XmlDocument();
        }

        #region Basic Authentication
        /// <summary>
        /// This Method Will Get All Direct Message Of User
        /// </summary>
        /// <param name="User">Twitter User Name And Password</param>
        /// <param name="Count">Number Of DirectMessage</param>
        /// <returns>Xml Text Of DirectMessage</returns>
        public XmlDocument DirectMessages(TwitterUser User, string Count)
        {
            TwitterWebRequest twtWebReq = new TwitterWebRequest();
            string RequestUrl = Globals.DirectMessageGetByUserUrl + Count;
            string response = twtWebReq.PerformWebRequest(User, RequestUrl, "Get", true, "");
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }

        /// <summary>
        /// This Method Will Get All Direct Message Sent By User
        /// </summary>
        /// <param name="User">Twitter User Name And Password</param>
        /// <param name="Count">Number Of DirectMessage Sent By User</param>
        /// <returns>Xml Text Of DirectMessage Sent By User</returns>
        public XmlDocument DirectMessage_Sent(TwitterUser User, string Count)
        {
            TwitterWebRequest twtWebReq = new TwitterWebRequest();
            string RequestUrl = Globals.DirectMessageSentByUserUrl + Count;
            string response = twtWebReq.PerformWebRequest(User, RequestUrl, "Get", true, "");
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }

        /// <summary>
        /// This will Send DirectMessage to the User
        /// </summary>
        /// <param name="User">Twitter UserName Password</param>
        /// <param name="DirectMessage">DirectMessage</param>
        /// <param name="UserId">USerId Whom You Want to Send Direct Message</param>
        /// <returns></returns>
        public XmlDocument NewDirectMessage(TwitterUser User, string DirectMessage, string ScreenName)
        {

            TwitterWebRequest twtWebReq = new TwitterWebRequest();
            string RequestUrl = Globals.NewDirectMessage + "?screen_name=" + ScreenName + "&text=" + DirectMessage;
            string response = twtWebReq.PerformWebRequest(User, RequestUrl, "Post", true, "");
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }

        /// <summary>
        /// This will Method will delete Direct Message by Direct Message Id
        /// </summary>
        /// <param name="User">Twitter UserName and Password</param>
        /// <param name="DirectMessageId">Direct Message Id</param>
        /// <returns></returns>
        public XmlDocument DeleteDirectMessage(TwitterUser User, int DirectMessageId)
        {

            TwitterWebRequest twtWebReq = new TwitterWebRequest();
            string RequestUrl = Globals.NewDirectMessage + DirectMessageId + ".xml";
            string response = twtWebReq.PerformWebRequest(User, RequestUrl, "Post", true, "");
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        } 
        #endregion


        #region OAuth
        /// <summary>
        /// This Method Will Get All Direct Message Of User Using OAUTH
        /// </summary>
        /// <param name="OAuth">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="Count">Number Of DirectMessage</param>
        /// <returns>Xml Text Of DirectMessage</returns>
        public XmlDocument DirectMessages(oAuthTwitter OAuth, string Count)
        {
            string RequestUrl = Globals.DirectMessageGetByUserUrl + Count;
            string response = OAuth.oAuthWebRequest(oAuthTwitter.Method.GET,RequestUrl,string.Empty);
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }

        /// <summary>
        /// This Method Will Get All Sent Direct Message By User Using OAuth
        /// </summary>
        /// <param name="OAuth">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="Count">Number Of DirectMessage Sent By User</param>
        /// <returns>Xml Text Of DirectMessage Sent By User</returns>
        public XmlDocument DirectMessage_Sent(oAuthTwitter OAuth, string Count)
        {
            string RequestUrl = Globals.DirectMessageSentByUserUrl + Count;
            string response = OAuth.oAuthWebRequest(oAuthTwitter.Method.GET, RequestUrl, string.Empty);
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }

        /// <summary>
        /// This will Send DirectMessage to the User
        /// </summary>
        /// <param name="User">Twitter UserName Password</param>
        /// <param name="DirectMessage">DirectMessage</param>
        /// <param name="UserId">USerId Whom You Want to Send Direct Message</param>
        /// <returns></returns>
        public XmlDocument NewDirectMessage(oAuthTwitter OAuth, string DirectMessage, string ScreenName)
        {

           
            string RequestUrl = Globals.NewDirectMessage + "?screen_name=" + ScreenName + "&text=";
            string response = OAuth.oAuthWebRequest(oAuthTwitter.Method.POST, RequestUrl, OAuth.UrlEncode(DirectMessage));//twtWebReq.PerformWebRequest(User, RequestUrl, "Post", true, "");
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }

        /// <summary>
        /// This will Send DirectMessage to the User
        /// </summary>
        /// <param name="User">Twitter UserName Password</param>
        /// <param name="DirectMessage">DirectMessage</param>
        /// <param name="UserId">USerId Whom You Want to Send Direct Message</param>
        /// <returns></returns>
        public XmlDocument NewDirectMessage(oAuthTwitter OAuth, string DirectMessage, int  UserId)
        {


            string RequestUrl = Globals.NewDirectMessage + "?user_id=" + UserId + "&text=";
            string response = OAuth.oAuthWebRequest(oAuthTwitter.Method.POST, RequestUrl, OAuth.UrlEncode(DirectMessage));//twtWebReq.PerformWebRequest(User, RequestUrl, "Post", true, "");
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }
        #endregion

        
    }
}
