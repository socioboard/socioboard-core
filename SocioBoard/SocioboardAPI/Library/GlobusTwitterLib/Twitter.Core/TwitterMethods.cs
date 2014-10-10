using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections;
using GlobusTwitterLib.App.Core;
using GlobusLib;


namespace GlobusTwitterLib.Twitter.Core
{
    public class TwitterMethods
    {

        public List<string> getFollowers(TwitterUser twitterUser,string goodProxy)
        {
            List<string> followers=new List<string> ();
            XmlDocument xmlDoc = new XmlDocument();
            TwitterWebRequest twitterWebRequest=new TwitterWebRequest();
            string followerIds = twitterWebRequest.PerformWebRequest(twitterUser, Globals.getFollowersUrl + twitterUser.TwitterUserName, "GET", true, goodProxy);
            xmlDoc.Load(new StringReader(followerIds));
            XmlNodeList xmlNodeList = xmlDoc.GetElementsByTagName("id");
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                followers.Add(xmlNode.InnerText.ToString());
            }
            return followers;
        }

        public List<string> getFollowersofFriends(string FriendId, TwitterUser twitterUser, string goodProxy)
        {
            List<string> followersOfFriend = new List<string>();
            XmlDocument xmlDoc = new XmlDocument();
            TwitterWebRequest twitterWebRequest = new TwitterWebRequest();
            Dictionary<string, string> dic = getUserInfo(twitterUser, FriendId, goodProxy);
            int noofFollowers =Int32.Parse( dic["NoOfFollowers"].ToString());
            string followerIds = "";
            int pageno = 1;
            if (noofFollowers > 5000)
            {
                pageno = noofFollowers / 5000;
                int temp = noofFollowers % 5000;
                if(temp>10)
                   pageno++;
               
                followerIds = twitterWebRequest.PerformWebRequest(twitterUser, Globals.getFollowersUrl + FriendId + ".xml?page="+pageno, "GET", true, goodProxy);
            }
            else
            {
               followerIds = twitterWebRequest.PerformWebRequest(twitterUser, Globals.getFollowersUrl + FriendId + ".xml?page=1", "GET", true, goodProxy);
            }
            
            xmlDoc.Load(new StringReader(followerIds));
            XmlNodeList xmlNodeList = xmlDoc.GetElementsByTagName("id");
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                followersOfFriend.Add(xmlNode.InnerText.ToString());
            }

            return followersOfFriend;
        }
        public List<string> getFriendsofFriends(string FriendId, TwitterUser twitterUser, string goodProxy)
        {
            List<string> followersOfFriend = new List<string>();
            XmlDocument xmlDoc = new XmlDocument();
            TwitterWebRequest twitterWebRequest = new TwitterWebRequest();
            Dictionary<string, string> dic = getUserInfo(twitterUser, FriendId, goodProxy);
            int noofFollowers = Int32.Parse(dic["NoOfFriends"].ToString());
            string FriendIds = "";
            int pageno = 1;
            if (noofFollowers > 5000)
            {
                pageno = noofFollowers / 5000;
                int temp = noofFollowers % 5000;
                if (temp > 10)
                    pageno++;

                FriendIds = twitterWebRequest.PerformWebRequest(twitterUser, Globals.getFriendsUrl + FriendId + ".xml?page=" + pageno, "GET", true, goodProxy);
            }
            else
            {
                FriendIds = twitterWebRequest.PerformWebRequest(twitterUser, Globals.getFriendsUrl + FriendId + ".xml?page=1", "GET", true, goodProxy);
            }

            xmlDoc.Load(new StringReader(FriendIds));
            XmlNodeList xmlNodeList = xmlDoc.GetElementsByTagName("id");
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                followersOfFriend.Add(xmlNode.InnerText.ToString());
            }

            return followersOfFriend;
        }
        public List<string> getFriends(TwitterUser twitterUser,string goodProxy)
        {
            List<string> friends = new List<string>();
            XmlDocument xmlDoc = new XmlDocument();
            TwitterWebRequest twitterWebRequest = new TwitterWebRequest();
            string friendsIds = twitterWebRequest.PerformWebRequest(twitterUser, Globals.getFriendsUrl + twitterUser.TwitterUserName +".xml" , "GET", true,goodProxy);
            xmlDoc.Load(new StringReader(friendsIds));
            XmlNodeList xmlNodeList = xmlDoc.GetElementsByTagName("id");
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                friends.Add(xmlNode.InnerText.ToString());
            }
            return friends ;
        }
        public Dictionary<string,string> getUserInfo(TwitterUser twitterUser,string userid,string goodProxy)
        {
            
            TwitterWebRequest twitterWebRequest = new TwitterWebRequest();
            Dictionary<string, string> dictUserInfo = new Dictionary<string,string >();
            XmlDocument xmlDoc = new XmlDocument();
            string StatusResponse = twitterWebRequest.PerformWebRequest(twitterUser,Globals.getUserStatusUrl +userid  ,"GET",true,goodProxy  );
            //if (StatusResponse.Contains("404"))
            //{
            //    Logger.LogText("No Such Twitter User Found","");
            //    return dictUserInfo;
            //}
            if (StatusResponse.Contains("400"))
            {
                Logger.LogText("Twitter Returned Bad Request. Please try after some time or use a different machine.");
                return dictUserInfo;
            }

            try
            {
                xmlDoc.Load(new StringReader(StatusResponse));
                XmlNodeList xmlNodeFollowers = xmlDoc.GetElementsByTagName("followers_count");
                string noOfFollowers = xmlNodeFollowers[0].InnerText.ToString();
                dictUserInfo.Add("NoOfFollowers", noOfFollowers);
                XmlNodeList xmlNodeFriends = xmlDoc.GetElementsByTagName("friends_count");
                string noOfFriends = xmlNodeFriends[0].InnerText.ToString();
                dictUserInfo.Add("NoOfFriends", noOfFriends);
                XmlNodeList xmlNodeListStatus = xmlDoc.GetElementsByTagName("statuses_count");
                string noOfStatuses = xmlNodeListStatus[0].InnerText.ToString();
                dictUserInfo.Add("NoOfStatus", noOfStatuses);
                XmlNodeList xmlNodeListImage = xmlDoc.GetElementsByTagName("profile_image_url");
                string imageUrl = xmlNodeListImage[0].InnerText.ToString();
                dictUserInfo.Add("ImageUrl", imageUrl);
            }
            catch
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message );
            }
            return dictUserInfo;
        }
        public bool UpdateStatus(TwitterUser twitterUser, string StatusText,string goodProxy)
        {
            TwitterWebRequest twitterWebRequest = new TwitterWebRequest();
            XmlDocument xmlDoc = new XmlDocument();
            string actionUrl = Globals.getStatusUrl + "?status=" + StatusText;
            string Response = twitterWebRequest.PerformWebRequest(twitterUser ,actionUrl ,"POST",true,goodProxy );
            xmlDoc.Load(new StringReader(Response));
            Logger.LogText("Status has been updated");
            return true;
        }
        public bool FollowUser(TwitterUser twitterUser, string UserToFollow,string goodProxy)
        {
            TwitterWebRequest twitterWebRequest = new TwitterWebRequest();
            string actionUrl = Globals.getFollowUserUrl + UserToFollow;
            string response = twitterWebRequest.PerformWebRequest(twitterUser, actionUrl, "POST", true,goodProxy );
            if (response.Contains("The remote server returned an error: (403) Forbidden"))
            {
                return false;
            }
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(new StringReader(response));
                XmlNodeList xmlNodeID = xmlDoc.GetElementsByTagName("id");
                if (xmlNodeID != null)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public bool FollowUserByScreenName(TwitterUser twitterUser, string UserToFollow, string proxy)
        {
            TwitterWebRequest twitterWebRequest = new TwitterWebRequest();
            string actionUrl = "http://twitter.com/friendships/create.xml?screen_name=" + UserToFollow;
            string response = twitterWebRequest.PerformWebRequest(twitterUser, actionUrl, "POST", true, proxy);
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(new StringReader(response));
                XmlNodeList xmlNodeID = xmlDoc.GetElementsByTagName("id");
                if (xmlNodeID != null)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
      

      
        public bool UnfollowUser(TwitterUser twitterUser, string UserToUnfollow,string goodProxy)
        {
            TwitterWebRequest twitterWebRequest = new TwitterWebRequest();
            string actionUrl = Globals.getUnfollowUserUrl + UserToUnfollow;
            string response = twitterWebRequest.PerformWebRequest(twitterUser, actionUrl, "POST", true,goodProxy );
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(new StringReader(response));
                XmlNodeList xmlNodeID = xmlDoc.GetElementsByTagName("id");
                if (xmlNodeID != null)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }

        }
        public List<string> SearchByKeyword(string SearchKey, int pageNumber,string lang, string proxy)
        {
            if (pageNumber < 1 && pageNumber > 15)
                pageNumber = 1;
            List<string> users = new List<string>();
            TwitterWebRequest twitterWebRequest = new TwitterWebRequest();
            XmlDocument xmlDoc = new XmlDocument();
            Random rd = new Random();
            string actionUrl = Globals.getSearchUrl + SearchKey + "&rpp=100&page=" + pageNumber+"&lang="+lang;
            string response = twitterWebRequest.PerformWebRequest(new TwitterUser(), actionUrl, "GET", false, proxy);
            xmlDoc.Load(new StringReader(response));
            XmlNodeList xmlNodeList = xmlDoc.GetElementsByTagName("name");

            foreach (XmlNode node in xmlNodeList)
            {

                string[] user = node.InnerText.Split(' ');
                users.Add(user[0]);

            }
            return users;
        }

        public bool HasADefaultPicture(string UserId)
        {
            string  actionUrl = "http://twitter.com/users/show.xml?id="+UserId;
            TwitterWebRequest twitterWebRequest = new TwitterWebRequest();
            string response = twitterWebRequest.PerformWebRequest(new TwitterUser(), actionUrl, "GET", true, "");
            XmlDocument xmlDoc = new XmlDocument();
            
            xmlDoc.Load(new StringReader(response));
            XmlNodeList xmlNodeID = xmlDoc.GetElementsByTagName("profile_image_url");
            foreach (XmlNode s in xmlNodeID)
            {
                if (s.InnerText.Contains("default_profile_normal"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
