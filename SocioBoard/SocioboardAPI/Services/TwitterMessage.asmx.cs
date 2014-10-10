using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Domain.Socioboard.Domain;
using System.Collections;
namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for Group
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class TwitterMessage : System.Web.Services.WebService
    {
        TwitterMessageRepository objTwitterMessageRepository = new TwitterMessageRepository();
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTwitterMessages(string TwitterId, string Userid)
        {
            List<Domain.Socioboard.Domain.TwitterMessage> lsttwtmsg = objTwitterMessageRepository.getAllTwitterMessagesOfUser(Guid.Parse(Userid), TwitterId);
            return new JavaScriptSerializer().Serialize(lsttwtmsg);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getUnreadMessages(string Profileid)
        {
            List<Domain.Socioboard.Domain.TwitterMessage> lsttwtmsg = objTwitterMessageRepository.getUnreadMessages(Profileid);
            return new JavaScriptSerializer().Serialize(lsttwtmsg);
        }

        //getAlltwtMessagesOfUser
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAlltwtMessagesOfUser(string Profileid)
        {
            List<Domain.Socioboard.Domain.TwitterMessage> lsttwtmsg = objTwitterMessageRepository.getAlltwtMessagesOfUser(Profileid);
            return new JavaScriptSerializer().Serialize(lsttwtmsg);
        }

        //getAlltwtMessages
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAlltwtMessages(string Profileid)
        {
            List<Domain.Socioboard.Domain.TwitterMessage> lsttwtmsg = objTwitterMessageRepository.getAlltwtMessages(Profileid);
            return new JavaScriptSerializer().Serialize(lsttwtmsg);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAlltwtMessages1(string Profileid, string userid)
        {
            List<Domain.Socioboard.Domain.TwitterMessage> lsttwtmsg = objTwitterMessageRepository.getAlltwtMessages1(Profileid, Guid.Parse(userid));
            return new JavaScriptSerializer().Serialize(lsttwtmsg);
        }

        public string getTwtMention(string profileId, Guid userid, int days)
        {
            TwitterMessageRepository objretwt = new TwitterMessageRepository();
            string count = string.Empty;
            try
            {
                ArrayList alstTwt = objretwt.getMentionStatsByProfileId(profileId, userid, days);
                count = alstTwt[0].ToString();
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return count;
        }

        public string getRetweets(string profileId, Guid userid, int days)
        {
            TwitterMessageRepository objretwt = new TwitterMessageRepository();

            string strcount = string.Empty;
            try
            {

                ArrayList alstTwt = objretwt.getRetweetStatsByProfileId(profileId, userid, days);
                strcount = alstTwt[0].ToString();
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return strcount;

        }

     

        public string GetAllRetweetMentionBydays(string userid, string profileId, string days)
        { 
             Guid UserId=Guid.Parse(userid);
             string data = string.Empty;

             try
             {
                           
                 string usertweets = string.Empty;
                 string usertweetsDates = string.Empty;
                 string retweets = string.Empty;
                 string retweetsDates = string.Empty;
                 string mentions = string.Empty;
                 string mentionsDates = string.Empty;

                 int _TwitterMention = objTwitterMessageRepository.getTotalMentionBydays(UserId, profileId, Convert.ToInt32(days));
                 int _TwitterRetweet = objTwitterMessageRepository.getTotalRetweetBydays(UserId, profileId, Convert.ToInt32(days));


                 dynamic _TwitterMessage = objTwitterMessageRepository.getAllRetweetMentionBydays(UserId, profileId, Convert.ToInt32(days));

                 if (_TwitterMessage != null)
                 {
                     foreach (var item in _TwitterMessage)
                     {
                         if (item[3] == "twt_usertweets")
                         {
                             usertweets += item[0].ToString() + ',';
                             usertweetsDates += item[1].ToString() + ',';
                         }
                         else if (item[3] == "twt_retweets")
                         {
                             retweets += item[0].ToString() + ',';
                             retweetsDates += item[1].ToString() + ',';
                         }
                         else if (item[3] == "twt_mentions")
                         {
                             mentions += item[0].ToString() + ',';
                             mentionsDates += item[1].ToString() + ',';
                         }
                     }

                     try
                     {
                         usertweets = usertweets.Substring(0, usertweets.Length - 1);
                         usertweetsDates = usertweetsDates.Substring(0, usertweetsDates.Length - 1);
                         usertweets = "usrtwet^"+usertweets + "^" + usertweetsDates;
                     }
                     catch (Exception ex)
                     {
                         Console.WriteLine(ex.StackTrace);
                     }
                     try
                     {
                         mentions = mentions.Substring(0, mentions.Length - 1);
                         mentionsDates = mentionsDates.Substring(0, mentionsDates.Length - 1);
                         mentions = "mention^" + mentions + "^" + mentionsDates;
                     }
                     catch (Exception ex)
                     {
                         Console.WriteLine(ex.StackTrace);
                     }
                     try
                     {
                         retweets = retweets.Substring(0, retweets.Length - 1);
                         retweetsDates = retweetsDates.Substring(0, retweetsDates.Length - 1);
                         retweets = "retwet^" + retweets + "^" + retweetsDates;
                     }
                     catch (Exception ex)
                     {
                         Console.WriteLine(ex.StackTrace);
                     }
                     
                 }

                 String totalmention = "metion"+_TwitterMention;
                 string totalretweet = "retwet" + _TwitterRetweet;

                 data = usertweets + "@" + mentions + "@" + retweets + "@" + totalmention + "@" + totalretweet;
             }
             catch (Exception ex)
             {
                 Console.WriteLine(ex.StackTrace);
             }
            
             return data;
        
        }





    }
}
