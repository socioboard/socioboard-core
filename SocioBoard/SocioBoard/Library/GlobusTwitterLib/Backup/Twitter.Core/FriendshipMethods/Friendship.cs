using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using GlobusTwitterLib.App.Core;
using GlobusTwitterLib.Authentication;

namespace GlobusTwitterLib.Twitter.Core.FriendshipMethods
{
   public class Friendship
    {
        private XmlDocument xmlResult;

        public Friendship()
        {
            xmlResult = new XmlDocument();
        }

        #region Basic Authentication
        #region Friendships_Create
        /// <summary>
        /// Follow Twitter User
        /// </summary>
        /// <param name="twitterUser">Twitter UserName And Password</param>
        /// <param name="UserToFollow">ScreenName Of Whom You Want To Follow</param>
        /// <returns>Returm Xml</returns>
        public XmlDocument Friendships_Create(TwitterUser User, string ScreenName)
        {
            TwitterWebRequest twtWebReq = new TwitterWebRequest();
            string RequestUrl = Globals.FollowerUrl + ScreenName;
            string response = twtWebReq.PerformWebRequest(User, RequestUrl, "Post", true, "");
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }
        #endregion

        #region FriendshipsById_Create
        /// <summary>
        /// Follow Twitter User
        /// </summary>
        /// <param name="twitterUser">Twitter UserName And Password</param>
        /// <param name="UserToFollow">ScreenName Of Whom You Want To Follow</param>
        /// <returns>Returm Xml</returns>
        public XmlDocument Friendships_Create(TwitterUser User, int UserId)
        {
            TwitterWebRequest twtWebReq = new TwitterWebRequest();
            string RequestUrl = Globals.FollowerUrlById + UserId;
            string response = twtWebReq.PerformWebRequest(User, RequestUrl, "Post", true, "");
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }
        #endregion

        #region Friendships_Destroy
        /// <summary>
        /// UnFollow Twitter User
        /// </summary>
        /// <param name="twitterUser">Twitter UserName And Password</param>
        /// <param name="UserToFollow">ScreenName Of Whom You Want To UnFollow</param>
        /// <returns>Returm Xml</returns>
        public XmlDocument Friendships_Destroy(TwitterUser User, string ScreenName)
        {
            TwitterWebRequest twtWebReq = new TwitterWebRequest();
            string RequestUrl = Globals.UnFollowUrl + ScreenName;
            string response = twtWebReq.PerformWebRequest(User, RequestUrl, "Post", true, "");
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }
        #endregion

        #region FriendshipsById_Destroy
        /// <summary>
        /// UnFollow Twitter User
        /// </summary>
        /// <param name="twitterUser">Twitter UserName And Password</param>
        /// <param name="UserToFollow">ScreenName Of Whom You Want To UnFollow</param>
        /// <returns>Returm Xml</returns>
        public XmlDocument Friendships_Destroy(TwitterUser User, int UserId)
        {
            TwitterWebRequest twtWebReq = new TwitterWebRequest();
            string RequestUrl = Globals.UnFollowUrlById + UserId;
            string response = twtWebReq.PerformWebRequest(User, RequestUrl, "Post", true, "");
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }
        #endregion 
        #endregion

        #region OAuth
        #region Friendships_Create
        /// <summary>
        /// Follow Twitter User Using OAUTH
        /// </summary>
        /// <param name="twitterUser">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="UserToFollow">ScreenName Of Whom You Want To Follow</param>
        /// <returns>Returm Xml</returns>
        public XmlDocument Friendships_Create(oAuthTwitter oAuth, string ScreenName)
        {
            string RequestUrl = Globals.FollowerUrl + ScreenName;
            string response = oAuth.oAuthWebRequest(oAuthTwitter.Method.POST, RequestUrl, string.Empty);
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }
        #endregion

        #region FriendshipsById_Create
        /// <summary>
        /// Follow Twitter User Using OAUTH
        /// </summary>
        /// <param name="twitterUser">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="UserToFollow">ScreenName Of Whom You Want To Follow</param>
        /// <returns>Returm Xml</returns>
        public XmlDocument Friendships_Create(oAuthTwitter oAuth, int UserId)
        {
            string RequestUrl = Globals.FollowerUrlById + UserId;
            string response = oAuth.oAuthWebRequest(oAuthTwitter.Method.POST, RequestUrl, string.Empty);
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }
        #endregion

        #region Friendships_Destroy
        /// <summary>
        /// UnFollow Twitter User Using OAUTH
        /// </summary>
        /// <param name="twitterUser">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="UserToFollow">ScreenName Of Whom You Want To UnFollow</param>
        /// <returns>Returm Xml</returns>
        public XmlDocument Friendships_Destroy(oAuthTwitter oAuth, string ScreenName)
        {
            string RequestUrl = Globals.UnFollowUrl + ScreenName;
            string response = oAuth.oAuthWebRequest(oAuthTwitter.Method.POST, RequestUrl, string.Empty);
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }
        #endregion

        #region FriendshipsById_Destroy
        /// <summary>
        /// UnFollow Twitter User Using OAUTH
        /// </summary>
        /// <param name="twitterUser">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="UserToFollow">ScreenName Of Whom You Want To UnFollow</param>
        /// <returns>Returm Xml</returns>
        public XmlDocument Friendships_Destroy(oAuthTwitter oAuth, int UserId)
        {
            string RequestUrl = Globals.UnFollowUrlById + UserId;
            string response = oAuth.oAuthWebRequest(oAuthTwitter.Method.POST, RequestUrl, string.Empty);
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }
        #endregion 
        #endregion
    }
}
