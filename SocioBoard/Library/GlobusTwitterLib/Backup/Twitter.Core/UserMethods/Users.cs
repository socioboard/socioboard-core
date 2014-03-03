using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using GlobusTwitterLib.App.Core;
using GlobusTwitterLib.Authentication;

namespace GlobusTwitterLib.Twitter.Core.UserMethods
{
    public class Users
    {
        private XmlDocument xmlResult;

        public Users()
        {
            xmlResult = new XmlDocument();
        }

        #region OAuth
        #region Friends Status

        /// <summary>
        /// This Method Get All Friends Details of User
        /// </summary>
        /// <param name="oAuth">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="ScreenName">User Screen Name</param>
        /// <returns></returns>
        public XmlDocument FriendsStatus(oAuthTwitter oAuth, string ScreenName, string cursor)
        {
            string RequestUrl = Globals.FriendStatusUrl + ScreenName + ".xml?cursor=" + cursor;
            string response = oAuth.oAuthWebRequest(oAuthTwitter.Method.GET, RequestUrl, string.Empty);
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }
        #endregion

        #region Followers Status

        /// <summary>
        /// This Method Get All Followers Details of User
        /// </summary>
        /// <param name="oAuth">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="ScreenName">User Screen Name</param>
        /// <returns></returns>
        public XmlDocument FollowersStatus(oAuthTwitter oAuth, string ScreenName, string cursor)
        {
            string RequestUrl = Globals.FollowerStatusUrl + ScreenName + ".xml?cursor=" + cursor;
            string response = oAuth.oAuthWebRequest(oAuthTwitter.Method.GET, RequestUrl, string.Empty);
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }
        #endregion
        #endregion
    }
}
