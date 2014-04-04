using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using GlobusTwitterLib.Authentication;

namespace GlobusTwitterLib.App.Core
{
 public class TwitterFriendsController
    {
        private XmlDocument xmlResult;
       // TwitterUser twitterUser;

        public TwitterFriendsController()
        {
            xmlResult = new XmlDocument();
           // twitterUser = new TwitterUser();
        }

        #region Basic Authentication
        #region FollowUser
        /// <summary>
        /// Follow Twitter User
        /// </summary>
        /// <param name="twitterUser">Twitter UserName And Password</param>
        /// <param name="UserToFollow">ScreenName Of Whom You Want To Follow</param>
        /// <returns>Returm True Follow Is Success</returns>
        public bool FollowUser(TwitterUser twitterUser, string UserToFollow)
        {
            try
            {
                Twitter.Core.FriendshipMethods.Friendship friendship = new GlobusTwitterLib.Twitter.Core.FriendshipMethods.Friendship();
                xmlResult = friendship.Friendships_Create(twitterUser, UserToFollow);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region FollowUserbyId
        /// <summary>
        /// Follow Twitter User
        /// </summary>
        /// <param name="twitterUser">Twitter UserName And Password</param>
        /// <param name="UserToFollow">ScreenName Of Whom You Want To Follow</param>
        /// <returns>Returm True Follow Is Success</returns>
        public bool FollowUser(TwitterUser twitterUser, int UserToFollow)
        {
            try
            {
                Twitter.Core.FriendshipMethods.Friendship friendship = new GlobusTwitterLib.Twitter.Core.FriendshipMethods.Friendship();
                xmlResult = friendship.Friendships_Create(twitterUser, UserToFollow);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region UnFollowUser
        /// <summary>
        /// UnFollow Twitter User
        /// </summary>
        /// <param name="twitterUser">Twitter UserName And Password</param>
        /// <param name="UserToFollow">ScreenName Of Whom You Want To UnFollow</param>
        /// <returns>Returm True UnFollow Is Success</returns>
        public bool UnFollowUser(TwitterUser twitterUser, string UserToUnFollow)
        {
            try
            {
                Twitter.Core.FriendshipMethods.Friendship friendship = new GlobusTwitterLib.Twitter.Core.FriendshipMethods.Friendship();
                xmlResult = friendship.Friendships_Destroy(twitterUser, UserToUnFollow);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region UnFollowUserById
        /// <summary>
        /// UnFollow Twitter User
        /// </summary>
        /// <param name="twitterUser">Twitter UserName And Password</param>
        /// <param name="UserToFollow">ScreenName Of Whom You Want To UnFollow</param>
        /// <returns>Returm True UnFollow Is Success</returns>
        public bool UnFollowUser(TwitterUser twitterUser, int UserToUnFollow)
        {
            try
            {
                Twitter.Core.FriendshipMethods.Friendship friendship = new GlobusTwitterLib.Twitter.Core.FriendshipMethods.Friendship();
                xmlResult = friendship.Friendships_Destroy(twitterUser, UserToUnFollow);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region FriendsId
        /// <summary>
        /// Get FriendsId Of ScreenName
        /// </summary>
        /// <param name="twitterUser">Twitter UserName And Password</param>
        /// <param name="UserToFollow">ScreenName Of Whom You Want To Get FriendsId</param>
        /// <returns>Returm True UnFollow Is Success</returns>
        public List<string> FriendsId(TwitterUser twitterUser, string ScreenName)
        {
            List<string> lstFriendsId = new List<string>();

            Twitter.Core.SocialGraphMethods.SocialGraph socialGraph = new Twitter.Core.SocialGraphMethods.SocialGraph();
            xmlResult = socialGraph.FriendsId(twitterUser, ScreenName);
            XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("id");
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                lstFriendsId.Add(xmlNode.InnerText.ToString());
            }
            return lstFriendsId;

        }
        #endregion

        #region FollowersId
        /// <summary>
        /// Get FollowersId Of ScreenName
        /// </summary>
        /// <param name="twitterUser">Twitter UserName And Password</param>
        /// <param name="UserToFollow">ScreenName Of Whom You Want To Get FollowersId</param>
        /// <returns>Returm True UnFollow Is Success</returns>
        public List<string> FollowersId(TwitterUser twitterUser, string ScreenName)
        {
            List<string> lstFollowersId = new List<string>();

            Twitter.Core.SocialGraphMethods.SocialGraph socialGraph = new Twitter.Core.SocialGraphMethods.SocialGraph();
            xmlResult = socialGraph.FollowersId(twitterUser, ScreenName);
            XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("id");
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                lstFollowersId.Add(xmlNode.InnerText.ToString());
            }
            return lstFollowersId;
        }
        #endregion 
        #endregion

        #region OAuth
        #region FollowUser
        /// <summary>
        /// Follow Twitter User
        /// </summary>
        /// <param name="oAuth">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="UserToFollow">ScreenName Of Whom You Want To Follow</param>
        /// <returns>Returm True Follow Is Success</returns>
        public bool FollowUser(oAuthTwitter oAuth, string UserToFollow)
        {
            Twitter.Core.FriendshipMethods.Friendship friendship = new GlobusTwitterLib.Twitter.Core.FriendshipMethods.Friendship();
            xmlResult = friendship.Friendships_Create(oAuth, UserToFollow);
            return true;   
        }
        #endregion

        #region FollowUserbyId
        /// <summary>
        /// Follow Twitter User
        /// </summary>
        /// <param name="oAuth">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="UserToFollow">ScreenName Of Whom You Want To Follow</param>
        /// <returns>Returm True Follow Is Success</returns>
        public bool FollowUser(oAuthTwitter oAuth, int UserToFollow)
        {
            Twitter.Core.FriendshipMethods.Friendship friendship = new GlobusTwitterLib.Twitter.Core.FriendshipMethods.Friendship();
            xmlResult = friendship.Friendships_Create(oAuth, UserToFollow);
            return true;
        }
        #endregion

        #region UnFollowUser
        /// <summary>
        /// UnFollow Twitter User
        /// </summary>
        /// <param name="oAuth">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="UserToFollow">ScreenName Of Whom You Want To UnFollow</param>
        /// <returns>Returm True UnFollow Is Success</returns>
        public bool UnFollowUser(oAuthTwitter oAuth, string UserToUnFollow)
        {
            Twitter.Core.FriendshipMethods.Friendship friendship = new GlobusTwitterLib.Twitter.Core.FriendshipMethods.Friendship();
            xmlResult = friendship.Friendships_Destroy(oAuth, UserToUnFollow);
            return true;
        }
        #endregion

        #region UnFollowUserById
        /// <summary>
        /// UnFollow Twitter User
        /// </summary>
        /// <param name="oAuth">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="UserToFollow">ScreenName Of Whom You Want To UnFollow</param>
        /// <returns>Returm True UnFollow Is Success</returns>
        public bool UnFollowUser(oAuthTwitter oAuth, int UserToUnFollow)
        {
            Twitter.Core.FriendshipMethods.Friendship friendship = new GlobusTwitterLib.Twitter.Core.FriendshipMethods.Friendship();
            xmlResult = friendship.Friendships_Destroy(oAuth, UserToUnFollow);
            return true;
        }
        #endregion

        #region FriendsId
        /// <summary>
        /// Get FriendsId Of ScreenName
        /// </summary>
        /// <param name="oAuth">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="UserToFollow">ScreenName Of Whom You Want To Get FriendsId</param>
        /// <returns>Returm True UnFollow Is Success</returns>
        public List<string> FriendsId(oAuthTwitter oAuth, string ScreenName)
        {
            List<string> lstFriendsId = new List<string>();

            Twitter.Core.SocialGraphMethods.SocialGraph socialGraph = new Twitter.Core.SocialGraphMethods.SocialGraph();
            xmlResult = socialGraph.FriendsId(oAuth, ScreenName);
            XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("id");
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                lstFriendsId.Add(xmlNode.InnerText.ToString());
            }
            return lstFriendsId;

        }
        #endregion

        #region FollowersId
        /// <summary>
        /// Get FollowersId Of ScreenName
        /// </summary>
        /// <param name="oAuth">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="UserToFollow">ScreenName Of Whom You Want To Get FollowersId</param>
        /// <returns>Returm True UnFollow Is Success</returns>
        public List<string> FollowersId(oAuthTwitter oAuth, string ScreenName)
        {
            List<string> lstFollowersId = new List<string>();

            Twitter.Core.SocialGraphMethods.SocialGraph socialGraph = new Twitter.Core.SocialGraphMethods.SocialGraph();
            xmlResult = socialGraph.FollowersId(oAuth, ScreenName);
            XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("id");
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                lstFollowersId.Add(xmlNode.InnerText.ToString());
            }
            return lstFollowersId;
        }
        #endregion  
        #endregion
    }
}
