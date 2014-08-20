using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using GlobusTwitterLib.Twitter.Core;
using GlobusTwitterLib;
using GlobusTwitterLib.Authentication;

namespace GlobusTwitterLib.App.Core
{
    public class TwitterUser
    {
        private XmlDocument xmlResult;

        public TwitterUser()
        {
            xmlResult = new XmlDocument();
        }

        #region TwitterUserProperties
        private string _twitterusername;
        private string _twitteruserpassword;
        private string _userid;
        public int ID { get; set; }
        public string ScreenName { get; set; }
        public string Location { get; set; }
        public string TimeZone { get; set; }
        public string Description { get; set; }
        public Uri ProfileImageUri { get; set; }
        public Uri ProfileUri { get; set; }
        public bool IsProtected { get; set; }
        public int NumberOfFollowers { get; set; }
        public int Friends_count { get; set; }
        public string NoofStatusUpdates { get; set; }
        public string StatusCount { get; set; }

        public TwitterUser(string username, string password)
        {
            this._twitterusername = username;
            this._twitteruserpassword = password;
        }

        public string TwitterUserName
        {
            get { return _twitterusername; }
            set { _twitterusername = value; }
        }

        public string TwitterPassword
        {
            get { return _twitteruserpassword; }
            set { _twitteruserpassword = value; }
        }

        public string UserID
        {
            get { return _userid; }
            set { _userid = value; }
        }

        public string FollowsFilePath { get; set; }
        public string UnfollowsFilePath { get; set; }
        public string NewFollowersFilePath { get; set; } 
        #endregion

        public List<direct_messages> DirectMessages = new List<direct_messages>();
        public List<status> StatusTimeLine = new List<status>();
        public List<search> SearchMethod = new List<search>();
        public List<userstatus> UserMethod = new List<userstatus>();

        #region Direct Message Structure
        /// <summary>
        /// Direct Message Structure Of All Elements
        /// </summary>
        public struct direct_messages
        {
            public string Description { get; set; }
            public string Id { get; set; }

            public sender senderObject;
            public recipient recipientObject;

            public struct sender
            {
                public string ScreenName { get; set; }
                public Uri ProfileImageUrl { get; set; }
                public int FollowersCount { get; set; }
                public int FriendsCount { get; set; }
                public int StatusesCount { get; set; }
            }
            public struct recipient
            {

            }
        } 
        #endregion

        #region Status Structure
        /// <summary>
        /// Status Structure Of All Elements
        /// </summary>
        public struct status
        {
            public string Description { get; set; }

            public user userObject;

            public struct user
            {
                public string ScreenName { get; set; }
                public Uri ProfileImageUrl { get; set; }
                public int FollowersCount { get; set; }
                public int FriendsCount { get; set; }
                public int StatusesCount { get; set; }
            }
        } 
        #endregion

        #region Search Structure

        /// <summary>
        /// Search Structure Of All Elements
        /// </summary>
        public struct search
        {
            public string content { get; set; }
            public string link { get; set; }
            public string id { get; set; }

            public author authorObject;

            public struct author
            {
                public string name { get; set; }
            }
        } 
        #endregion

        #region User Satus
        /// <summary>
        /// Get User's Friend Status
        /// </summary>
        public struct userstatus
        {
            public string Id { get; set; }
            public string ScreenName { get; set; }
            public Uri ProfileImageUrl { get; set; }
            public string Next { get; set; }
            public string Previous { get; set; }
        }
        #endregion

        #region Basic Authentication
        #region DirectMessages
        /// <summary>
        /// Get All Direct Messages Of User
        /// </summary>
        /// <param name="User">Twitter User And Password</param>
        /// <param name="Count">Number Of Direct Messages</param>
        /// <returns>Return List Of DirectMessage</returns>
        public List<direct_messages> GetDirectMessages(TwitterUser User, string Count)
        {
            direct_messages direct_Messages = new direct_messages();

            Twitter.Core.DirectMessageMethods.DirectMessage directMessage = new Twitter.Core.DirectMessageMethods.DirectMessage();
            xmlResult = directMessage.DirectMessages(User, Count);
            XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("direct_message");

            foreach (XmlNode xn in xmlNodeList)
            {
                XmlElement idElement = (XmlElement)xn;
                direct_Messages.Id = idElement.GetElementsByTagName("id")[0].InnerText;

                XmlElement textElement = (XmlElement)xn;
                direct_Messages.Description = textElement.GetElementsByTagName("text")[0].InnerText;

                XmlElement screenNameElement = (XmlElement)xn;
                direct_Messages.senderObject.ScreenName = screenNameElement.GetElementsByTagName("screen_name")[0].InnerText;

                XmlElement imageUrlElement = (XmlElement)xn;
                direct_Messages.senderObject.ProfileImageUrl = new Uri(imageUrlElement.GetElementsByTagName("profile_image_url")[0].InnerText);

                XmlElement followersCountElement = (XmlElement)xn;
                direct_Messages.senderObject.FollowersCount = int.Parse(followersCountElement.GetElementsByTagName("followers_count")[0].InnerText);

                XmlElement friendCountElement = (XmlElement)xn;
                direct_Messages.senderObject.FriendsCount = int.Parse(friendCountElement.GetElementsByTagName("friends_count")[0].InnerText);

                XmlElement statusElement = (XmlElement)xn;
                direct_Messages.senderObject.StatusesCount = int.Parse(statusElement.GetElementsByTagName("statuses_count")[0].InnerText);

                DirectMessages.Add(direct_Messages);
            }
            return DirectMessages;

        }

        //public bool NewDirectMessage(TwitterUser User,string DirectMessage,int UserId)
        //{
        //    Twitter.Core.DirectMessageMethods.DirectMessage directMessage = new Twitter.Core.DirectMessageMethods.DirectMessage();
        //    xmlResult = directMessage.NewDirectMessage(User, DirectMessage,UserId);
        //    XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("recipient");
        //    foreach (XmlNode xn in xmlNodeList)
        //    {
        //        return true;
        //    }
        //    return false;


        //}

        #endregion

        #region TimeLine
        /// <summary>
        /// Get All Tweets Sent By User And Friend
        /// </summary>
        /// <param name="User">Twitter User And Password</param>
        /// <param name="Count">Number Of Tweets</param>
        /// <returns>Return List Of Tweets Sent By User And Friend</returns>
        public List<status> GetStatuses_HomeTimeLine(TwitterUser User, string Count)
        {
            status objStatus = new status();

            Twitter.Core.TimeLineMethods.TimeLine timeline = new Twitter.Core.TimeLineMethods.TimeLine();
            xmlResult = timeline.Status_HomeTimeLine(User, Count);
            XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("status");

            foreach (XmlNode xn in xmlNodeList)
            {
                XmlElement textElement = (XmlElement)xn;
                objStatus.Description = textElement.GetElementsByTagName("text")[0].InnerText;

                XmlElement screenNameElement = (XmlElement)xn;
                objStatus.userObject.ScreenName = screenNameElement.GetElementsByTagName("screen_name")[0].InnerText;

                XmlElement imageUrlElement = (XmlElement)xn;
                objStatus.userObject.ProfileImageUrl = new Uri(imageUrlElement.GetElementsByTagName("profile_image_url")[0].InnerText);

                XmlElement followersCountElement = (XmlElement)xn;
                objStatus.userObject.FollowersCount = int.Parse(followersCountElement.GetElementsByTagName("followers_count")[0].InnerText);

                XmlElement friendCountElement = (XmlElement)xn;
                objStatus.userObject.FriendsCount = int.Parse(friendCountElement.GetElementsByTagName("friends_count")[0].InnerText);

                XmlElement statusElement = (XmlElement)xn;
                objStatus.userObject.StatusesCount = int.Parse(statusElement.GetElementsByTagName("statuses_count")[0].InnerText);

                StatusTimeLine.Add(objStatus);
            }
            return StatusTimeLine;

        }

        /// <summary>
        /// Get All Tweets Sent By User
        /// </summary>
        /// <param name="User">Twitter User And Password</param>
        /// <param name="Count">Number Of Tweets</param>
        /// <returns>Return List Of Tweets Sent By User </returns>
        public List<status> GetStatuses_UserTimeLine(TwitterUser User, string Count)
        {
            status objStatus = new status();

            Twitter.Core.TimeLineMethods.TimeLine timeline = new Twitter.Core.TimeLineMethods.TimeLine();
            xmlResult = timeline.Status_UserTimeLine(User, Count);
            XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("status");

            foreach (XmlNode xn in xmlNodeList)
            {
                XmlElement textElement = (XmlElement)xn;
                objStatus.Description = textElement.GetElementsByTagName("text")[0].InnerText;

                XmlElement screenNameElement = (XmlElement)xn;
                objStatus.userObject.ScreenName = screenNameElement.GetElementsByTagName("screen_name")[0].InnerText;

                XmlElement imageUrlElement = (XmlElement)xn;
                objStatus.userObject.ProfileImageUrl = new Uri(imageUrlElement.GetElementsByTagName("profile_image_url")[0].InnerText);

                XmlElement followersCountElement = (XmlElement)xn;
                objStatus.userObject.FollowersCount = int.Parse(followersCountElement.GetElementsByTagName("followers_count")[0].InnerText);

                XmlElement friendCountElement = (XmlElement)xn;
                objStatus.userObject.FriendsCount = int.Parse(friendCountElement.GetElementsByTagName("friends_count")[0].InnerText);

                XmlElement statusElement = (XmlElement)xn;
                objStatus.userObject.StatusesCount = int.Parse(statusElement.GetElementsByTagName("statuses_count")[0].InnerText);

                StatusTimeLine.Add(objStatus);
            }
            return StatusTimeLine;

        }

        /// <summary>
        /// Get All ReTweets Sent By User
        /// </summary>
        /// <param name="User">Twitter User And Password</param>
        /// <param name="Count">Number Of ReTweets</param>
        /// <returns>Return List Of ReTweets</returns>
        public List<status> GetStatuses_ReTweetTimeLine(TwitterUser User, string Count)
        {
            status objStatus = new status();

            Twitter.Core.TimeLineMethods.TimeLine timeline = new Twitter.Core.TimeLineMethods.TimeLine();
            xmlResult = timeline.Status_RetweetedByMe(User, Count);
            XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("status");

            foreach (XmlNode xn in xmlNodeList)
            {
                XmlElement textElement = (XmlElement)xn;
                objStatus.Description = textElement.GetElementsByTagName("text")[0].InnerText;

                XmlElement screenNameElement = (XmlElement)xn;
                objStatus.userObject.ScreenName = screenNameElement.GetElementsByTagName("screen_name")[0].InnerText;

                XmlElement imageUrlElement = (XmlElement)xn;
                objStatus.userObject.ProfileImageUrl = new Uri(imageUrlElement.GetElementsByTagName("profile_image_url")[0].InnerText);

                XmlElement followersCountElement = (XmlElement)xn;
                objStatus.userObject.FollowersCount = int.Parse(followersCountElement.GetElementsByTagName("followers_count")[0].InnerText);

                XmlElement friendCountElement = (XmlElement)xn;
                objStatus.userObject.FriendsCount = int.Parse(friendCountElement.GetElementsByTagName("friends_count")[0].InnerText);

                XmlElement statusElement = (XmlElement)xn;
                objStatus.userObject.StatusesCount = int.Parse(statusElement.GetElementsByTagName("statuses_count")[0].InnerText);

                StatusTimeLine.Add(objStatus);
            }
            return StatusTimeLine;

        }

        #endregion

        #region Status
        /// <summary>
        /// Get User Status
        /// </summary>
        /// <param name="User">Twitter User And Password</param>
        /// <param name="Count">ScreenName Of User</param>
        /// <returns>Return List Of User Detail</returns>
        public List<status> GetStatusOfUser(TwitterUser User, string ScreenName)
        {
            status objStatus = new status();

            Twitter.Core.StatusMethods.Status status = new Twitter.Core.StatusMethods.Status();
            xmlResult = status.ShowStatusByScreenName(User, ScreenName);
            XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("user");

            foreach (XmlNode xn in xmlNodeList)
            {

                XmlElement imageUrlElement = (XmlElement)xn;
                objStatus.userObject.ProfileImageUrl = new Uri(imageUrlElement.GetElementsByTagName("profile_image_url")[0].InnerText);

                XmlElement followersCountElement = (XmlElement)xn;
                objStatus.userObject.FollowersCount = int.Parse(followersCountElement.GetElementsByTagName("followers_count")[0].InnerText);

                XmlElement friendCountElement = (XmlElement)xn;
                objStatus.userObject.FriendsCount = int.Parse(friendCountElement.GetElementsByTagName("friends_count")[0].InnerText);

                XmlElement statusElement = (XmlElement)xn;
                objStatus.userObject.StatusesCount = int.Parse(statusElement.GetElementsByTagName("statuses_count")[0].InnerText);

                StatusTimeLine.Add(objStatus);
            }
            return StatusTimeLine;

        }
        /// <summary>
        /// ReTweet
        /// </summary>
        /// <param name="twitterUser">Twitter UserName And Password</param>
        /// <param name="UserToFollow">TweetId</param>
        /// <returns>Returm True If ReTweet Success</returns>
        public bool ReTweetStatus(TwitterUser twitterUser, string TweetId)
        {
            try
            {
                Twitter.Core.StatusMethods.Status status = new Twitter.Core.StatusMethods.Status();
                xmlResult = status.ReTweetStatus(twitterUser, TweetId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Search
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="User">Twitter User And Password</param>
        /// <param name="Count">SearchKey For Search</param>
        /// <returns>Return Search Result</returns>
        public List<search> GetSearchMethod(TwitterUser User, string SearchKey)
        {
            search objSearch = new search();

            Twitter.Core.SearchMethods.Search search = new Twitter.Core.SearchMethods.Search();
            xmlResult = search.SearchMethod(User, SearchKey);
            XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("entry");

            foreach (XmlNode xn in xmlNodeList)
            {
                XmlElement idElement = (XmlElement)xn;
                objSearch.id = idElement.GetElementsByTagName("id")[0].InnerText;

                XmlElement imageUrlElement = (XmlElement)xn;
                objSearch.content = imageUrlElement.GetElementsByTagName("content")[0].InnerText;

                XmlElement followersCountElement = (XmlElement)xn;
                objSearch.link = followersCountElement.GetElementsByTagName("updated")[0].NextSibling.Attributes["href"].InnerText;

                XmlElement friendCountElement = (XmlElement)xn;
                objSearch.authorObject.name = friendCountElement.GetElementsByTagName("name")[0].InnerText;

                SearchMethod.Add(objSearch);
            }
            return SearchMethod;

        }

        #endregion 
        #endregion

        #region OAuth

        #region DirectMessages

        /// <summary>
        /// Get All Direct Messages Of User
        /// </summary>
        /// <param name="OAuth">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="Count">Number Of Direct Messages</param>
        /// <returns>Return List Of DirectMessage</returns>
        public List<direct_messages> GetDirectMessages(oAuthTwitter OAuth, string Count)
        {
            direct_messages direct_Messages = new direct_messages();

            Twitter.Core.DirectMessageMethods.DirectMessage directMessage = new Twitter.Core.DirectMessageMethods.DirectMessage();
            xmlResult = directMessage.DirectMessages(OAuth, Count);
            XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("direct_message");

            foreach (XmlNode xn in xmlNodeList)
            {
                XmlElement idElement = (XmlElement)xn;
                direct_Messages.Id = idElement.GetElementsByTagName("id")[0].InnerText;

                XmlElement textElement = (XmlElement)xn;
                direct_Messages.Description = textElement.GetElementsByTagName("text")[0].InnerText;

                XmlElement screenNameElement = (XmlElement)xn;
                direct_Messages.senderObject.ScreenName = screenNameElement.GetElementsByTagName("screen_name")[0].InnerText;

                XmlElement imageUrlElement = (XmlElement)xn;
                direct_Messages.senderObject.ProfileImageUrl = new Uri(imageUrlElement.GetElementsByTagName("profile_image_url")[0].InnerText);

                XmlElement followersCountElement = (XmlElement)xn;
                direct_Messages.senderObject.FollowersCount = int.Parse(followersCountElement.GetElementsByTagName("followers_count")[0].InnerText);

                XmlElement friendCountElement = (XmlElement)xn;
                direct_Messages.senderObject.FriendsCount = int.Parse(friendCountElement.GetElementsByTagName("friends_count")[0].InnerText);

                XmlElement statusElement = (XmlElement)xn;
                direct_Messages.senderObject.StatusesCount = int.Parse(statusElement.GetElementsByTagName("statuses_count")[0].InnerText);

                DirectMessages.Add(direct_Messages);
            }
            return DirectMessages;

        }

        #endregion

        #region TimeLine
        /// <summary>
        /// Get All Tweets Sent By User And Friend
        /// </summary>
        /// <param name="User">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="Count">Number Of Tweets</param>
        /// <returns>Return List Of Tweets Sent By User And Friend</returns>
        public List<status> GetStatuses_HomeTimeLine(oAuthTwitter OAuth, string Count)
        {
            status objStatus = new status();

            Twitter.Core.TimeLineMethods.TimeLine timeline = new Twitter.Core.TimeLineMethods.TimeLine();
            xmlResult = timeline.Status_HomeTimeLine(OAuth, Count);
            XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("status");

            foreach (XmlNode xn in xmlNodeList)
            {
                XmlElement textElement = (XmlElement)xn;
                objStatus.Description = textElement.GetElementsByTagName("text")[0].InnerText;

                XmlElement screenNameElement = (XmlElement)xn;
                objStatus.userObject.ScreenName = screenNameElement.GetElementsByTagName("screen_name")[0].InnerText;

                XmlElement imageUrlElement = (XmlElement)xn;
                objStatus.userObject.ProfileImageUrl = new Uri(imageUrlElement.GetElementsByTagName("profile_image_url")[0].InnerText);

                XmlElement followersCountElement = (XmlElement)xn;
                objStatus.userObject.FollowersCount = int.Parse(followersCountElement.GetElementsByTagName("followers_count")[0].InnerText);

                XmlElement friendCountElement = (XmlElement)xn;
                objStatus.userObject.FriendsCount = int.Parse(friendCountElement.GetElementsByTagName("friends_count")[0].InnerText);

                XmlElement statusElement = (XmlElement)xn;
                objStatus.userObject.StatusesCount = int.Parse(statusElement.GetElementsByTagName("statuses_count")[0].InnerText);

                StatusTimeLine.Add(objStatus);
            }
            return StatusTimeLine;

        }

        /// <summary>
        /// Get All Tweets Sent By User
        /// </summary>
        /// <param name="OAuth">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="Count">Number Of Tweets</param>
        /// <returns>Return List Of Tweets Sent By User </returns>
        public List<status> GetStatuses_UserTimeLine(oAuthTwitter OAuth, string Count,string ScreenName)
        {
            status objStatus = new status();

            Twitter.Core.TimeLineMethods.TimeLine timeline = new Twitter.Core.TimeLineMethods.TimeLine();
            xmlResult = timeline.Status_UserTimeLine(OAuth, Count,ScreenName);
            XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("status");

            foreach (XmlNode xn in xmlNodeList)
            {
                XmlElement textElement = (XmlElement)xn;
                objStatus.Description = textElement.GetElementsByTagName("text")[0].InnerText;

                XmlElement screenNameElement = (XmlElement)xn;
                objStatus.userObject.ScreenName = screenNameElement.GetElementsByTagName("screen_name")[0].InnerText;

                XmlElement imageUrlElement = (XmlElement)xn;
                objStatus.userObject.ProfileImageUrl = new Uri(imageUrlElement.GetElementsByTagName("profile_image_url")[0].InnerText);

                XmlElement followersCountElement = (XmlElement)xn;
                objStatus.userObject.FollowersCount = int.Parse(followersCountElement.GetElementsByTagName("followers_count")[0].InnerText);

                XmlElement friendCountElement = (XmlElement)xn;
                objStatus.userObject.FriendsCount = int.Parse(friendCountElement.GetElementsByTagName("friends_count")[0].InnerText);

                XmlElement statusElement = (XmlElement)xn;
                objStatus.userObject.StatusesCount = int.Parse(statusElement.GetElementsByTagName("statuses_count")[0].InnerText);

                StatusTimeLine.Add(objStatus);
            }
            return StatusTimeLine;

        }

        /// <summary>
        /// Get All ReTweets Sent By User
        /// </summary>
        /// <param name="OAuth">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="Count">Number Of ReTweets</param>
        /// <returns>Return List Of ReTweets</returns>
        public List<status> GetStatuses_ReTweetTimeLine(oAuthTwitter OAuth, string Count)
        {
            status objStatus = new status();

            Twitter.Core.TimeLineMethods.TimeLine timeline = new Twitter.Core.TimeLineMethods.TimeLine();
            xmlResult = timeline.Status_RetweetedByMe(OAuth, Count);
            XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("status");

            foreach (XmlNode xn in xmlNodeList)
            {
                XmlElement textElement = (XmlElement)xn;
                objStatus.Description = textElement.GetElementsByTagName("text")[0].InnerText;

                XmlElement screenNameElement = (XmlElement)xn;
                objStatus.userObject.ScreenName = screenNameElement.GetElementsByTagName("screen_name")[0].InnerText;

                XmlElement imageUrlElement = (XmlElement)xn;
                objStatus.userObject.ProfileImageUrl = new Uri(imageUrlElement.GetElementsByTagName("profile_image_url")[0].InnerText);

                XmlElement followersCountElement = (XmlElement)xn;
                objStatus.userObject.FollowersCount = int.Parse(followersCountElement.GetElementsByTagName("followers_count")[0].InnerText);

                XmlElement friendCountElement = (XmlElement)xn;
                objStatus.userObject.FriendsCount = int.Parse(friendCountElement.GetElementsByTagName("friends_count")[0].InnerText);

                XmlElement statusElement = (XmlElement)xn;
                objStatus.userObject.StatusesCount = int.Parse(statusElement.GetElementsByTagName("statuses_count")[0].InnerText);

                StatusTimeLine.Add(objStatus);
            }
            return StatusTimeLine;

        }

        #endregion

        #region Status
        /// <summary>
        /// Get User Status
        /// </summary>
        /// <param name="OAuth">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="Count">ScreenName Of User</param>
        /// <returns>Return List Of User Detail</returns>
        public List<status> GetStatusOfUser(oAuthTwitter OAuth, string ScreenName)
        {
            status objStatus = new status();

            Twitter.Core.StatusMethods.Status status = new Twitter.Core.StatusMethods.Status();
            xmlResult = status.ShowStatusByScreenName(OAuth, ScreenName);
            XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("user");

            foreach (XmlNode xn in xmlNodeList)
            {

                XmlElement imageUrlElement = (XmlElement)xn;
                objStatus.userObject.ProfileImageUrl = new Uri(imageUrlElement.GetElementsByTagName("profile_image_url")[0].InnerText);

                XmlElement followersCountElement = (XmlElement)xn;
                objStatus.userObject.FollowersCount = int.Parse(followersCountElement.GetElementsByTagName("followers_count")[0].InnerText);

                XmlElement friendCountElement = (XmlElement)xn;
                objStatus.userObject.FriendsCount = int.Parse(friendCountElement.GetElementsByTagName("friends_count")[0].InnerText);

                XmlElement statusElement = (XmlElement)xn;
                objStatus.userObject.StatusesCount = int.Parse(statusElement.GetElementsByTagName("statuses_count")[0].InnerText);

                StatusTimeLine.Add(objStatus);
            }
            return StatusTimeLine;

        }
        /// <summary>
        /// ReTweet
        /// </summary>
        /// <param name="OAuth">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="UserToFollow">TweetId</param>
        /// <returns>Returm True If ReTweet Success</returns>
        //public bool ReTweetStatus(oAuthTwitter OAuth, string TweetId)
        //{
        //    try
        //    {
        //        Twitter.Core.StatusMethods.Status status = new Twitter.Core.StatusMethods.Status();
        //        xmlResult = status.ReTweetStatus(OAuth, TweetId);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        #endregion

        #region Search
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="OAuth">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="Count">SearchKey For Search</param>
        /// <returns>Return Search Result</returns>
        public List<search> GetSearchMethod(oAuthTwitter OAuth, string SearchKey)
        {
            search objSearch = new search();

            Twitter.Core.SearchMethods.Search search = new Twitter.Core.SearchMethods.Search();
            xmlResult = search.SearchMethod(OAuth, SearchKey);
            XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("entry");

            foreach (XmlNode xn in xmlNodeList)
            {
                XmlElement idElement = (XmlElement)xn;
                objSearch.id = idElement.GetElementsByTagName("id")[0].InnerText;

                XmlElement imageUrlElement = (XmlElement)xn;
                objSearch.content = imageUrlElement.GetElementsByTagName("content")[0].InnerText;

                XmlElement followersCountElement = (XmlElement)xn;
                objSearch.link = followersCountElement.GetElementsByTagName("updated")[0].NextSibling.Attributes["href"].InnerText;

                XmlElement friendCountElement = (XmlElement)xn;
                string name = friendCountElement.GetElementsByTagName("name")[0].InnerText;
                int FirstPoint = name.IndexOf("(");
                objSearch.authorObject.name = name.Substring(0, FirstPoint).Replace("(", "").Replace(" ", "");

                SearchMethod.Add(objSearch);
            }
            return SearchMethod;

        }

        #endregion  

        #region User Status
        /// <summary>
        /// FriendsStatusOfUser
        /// </summary>
        /// <param name="OAuth">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="ScreenName">User Screen Name</param>
        /// <returns></returns>
        public List<userstatus> GetFriendsStatusOfUser(oAuthTwitter OAuth, string ScreenName, string cursor)
        {
            userstatus objUserStatus = new userstatus();

            Twitter.Core.UserMethods.Users Users = new Twitter.Core.UserMethods.Users();
            xmlResult = Users.FriendsStatus(OAuth, ScreenName, cursor);
            XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("user");

            foreach (XmlNode xn in xmlNodeList)
            {
                XmlElement imageUrlElement = (XmlElement)xn;
                objUserStatus.Id = imageUrlElement.GetElementsByTagName("id")[0].InnerText;

                XmlElement followersCountElement = (XmlElement)xn;
                objUserStatus.ScreenName = followersCountElement.GetElementsByTagName("screen_name")[0].InnerText;

                XmlElement friendCountElement = (XmlElement)xn;
                objUserStatus.ProfileImageUrl = new Uri(friendCountElement.GetElementsByTagName("profile_image_url")[0].InnerText);

                XmlNodeList xmlNodeList_NextPage = xmlResult.GetElementsByTagName("next_cursor");

                objUserStatus.Next = xmlNodeList_NextPage.Item(0).InnerText;

                XmlNodeList xmlNodeList_PreviousPage = xmlResult.GetElementsByTagName("previous_cursor");
                objUserStatus.Next = xmlNodeList_PreviousPage.Item(0).InnerText;

                UserMethod.Add(objUserStatus);
            }


            return UserMethod;

        }

        /// <summary>
        /// FollowersStatusOfUser
        /// </summary>
        /// <param name="OAuth">OAuth Keys Token, TokenSecret, ConsumerKey, ConsumerSecret</param>
        /// <param name="ScreenName">User Screen Name</param>
        /// <returns></returns>
        public List<userstatus> GetFollowersStatusOfUser(oAuthTwitter OAuth, string ScreenName, string cursor)
        {
            userstatus objUserStatus = new userstatus();

            Twitter.Core.UserMethods.Users Users = new Twitter.Core.UserMethods.Users();
            xmlResult = Users.FollowersStatus(OAuth, ScreenName, cursor);
            XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("user");

            foreach (XmlNode xn in xmlNodeList)
            {
                XmlElement imageUrlElement = (XmlElement)xn;
                objUserStatus.Id = imageUrlElement.GetElementsByTagName("id")[0].InnerText;

                XmlElement followersCountElement = (XmlElement)xn;
                objUserStatus.ScreenName = followersCountElement.GetElementsByTagName("screen_name")[0].InnerText;

                XmlElement friendCountElement = (XmlElement)xn;
                objUserStatus.ProfileImageUrl = new Uri(friendCountElement.GetElementsByTagName("profile_image_url")[0].InnerText);



                UserMethod.Add(objUserStatus);
            }



            return UserMethod;

        }
        #endregion

        public bool NewDirectMessage(oAuthTwitter OAuth, string DirectMessage, string ScreenName)
        {
            Twitter.Core.DirectMessageMethods.DirectMessage directMessage = new Twitter.Core.DirectMessageMethods.DirectMessage();
            xmlResult = directMessage.NewDirectMessage(OAuth, DirectMessage, ScreenName);
            XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("direct_message");
            if (xmlNodeList.Count > 0)
            {
                return true;
            }
            return false;
        }
        public bool NewDirectMessage(oAuthTwitter OAuth, string DirectMessage, int UserId)
        {
            Twitter.Core.DirectMessageMethods.DirectMessage directMessage = new Twitter.Core.DirectMessageMethods.DirectMessage();
            xmlResult = directMessage.NewDirectMessage(OAuth, DirectMessage, UserId);
            XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("direct_message");
            if (xmlNodeList.Count > 0)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
