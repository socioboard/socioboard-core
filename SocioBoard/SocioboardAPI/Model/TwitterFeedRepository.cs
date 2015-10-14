using Api.Socioboard.Helper;
using Domain.Socioboard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;
using NHibernate.Criterion;

namespace Api.Socioboard.Services
{
    public class TwitterFeedRepository:ITwitterFeedRepository
    {
        /// <addTwitterFeed>
        /// Add Twitter Feed
        /// </summary>
        /// <param name="twtfeed">Set Values in a TwitterFeed Class Property and Pass the Object of TwitterFeed Class.(Domein.TwitterFeed)</param>
        public void addTwitterFeed(Domain.Socioboard.Domain.TwitterFeed twtfeed)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data.
                    session.Save(twtfeed);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }

        public bool checkTwitteUserExists(string ProfileId, Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to Check if FacebookUser is Exist in database or not by UserId and FbuserId.
                        // And Set the reuired paremeters to find the specific values.
                       // List<Domain.Socioboard.Domain.TwitterFeed> alst = session.CreateQuery("from TwitterFeed where UserId = :userid and ProfileId = :fbuserid")
                       // .SetParameter("userid", Userid)
                       // .SetParameter("fbuserid", ProfileId)
                       // .List<Domain.Socioboard.Domain.TwitterFeed>()
                       //.ToList<Domain.Socioboard.Domain.TwitterFeed>();
                       // if (alst.Count == 0 || alst == null)
                       //     return false;
                       // else
                       //     return true;

                        bool exist = session.Query<Domain.Socioboard.Domain.TwitterFeed>()
                                     .Any(x => x.UserId == Userid && x.ProfileId==ProfileId);
                        return exist;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }

                }//End Transaction
            }//End session
        }

        public List<Domain.Socioboard.Domain.TwitterFeed> getAllTwitterUserFeeds(string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all facebook feeds of profile by facebook profile id  
                        List<Domain.Socioboard.Domain.TwitterFeed> alst = session.CreateQuery("from TwitterFeed where  ProfileId = :profileId ORDER BY FeedDate DESC")
                        .SetParameter("profileId", profileid)
                        .List<Domain.Socioboard.Domain.TwitterFeed>()
                        .ToList<Domain.Socioboard.Domain.TwitterFeed>();

                        #region oldcode
                        //List<FacebookFeed> alst = new List<FacebookFeed>();
                        //foreach (FacebookFeed item in query.Enumerable<FacebookFeed>().OrderByDescending(x => x.FeedDate))
                        //{
                        //    alst.Add(item);
                        //} 
                        #endregion

                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }// End session
        }

        /// <deleteTwitterFeed>
        /// Delete Twitter Feed
        /// </summary>
        /// <param name="twtfeed">Set Values of twitter profile id and user id in a TwitterFeed Class Property and Pass the Object of TwitterFeed Class.(Domein.TwitterFeed)</param>
        /// <returns>Return 1 for success and 0 for failure.(int) </returns>
        public int deleteTwitterFeed(Domain.Socioboard.Domain.TwitterFeed twtfeed)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete feeds by profile id and user id.
                        NHibernate.IQuery query = session.CreateQuery("delete from TwitterFeed where ProfileId = :twtuserid and UserId = :userid")
                                        .SetParameter("twtuserid", twtfeed.ProfileId)
                                        .SetParameter("userid", twtfeed.UserId);
                        int isUpdated = query.ExecuteUpdate();
                        transaction.Commit();
                        return isUpdated;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }//End Transaction
            }//End Session
        }

        /// <deleteTwitterFeed>
        /// Delete Twitter Feed
        /// </summary>
        /// <param name="profileid">Twitter profile id.(string)</param>
        /// <param name="userid">user id.(Guid)</param>
        /// <returns>Return 1 for success and 0 for failure.(int) </returns>
        public int deleteTwitterFeed(string profileid,Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from TwitterFeed where ProfileId = :twtuserid and UserId = :userid")
                                        .SetParameter("twtuserid", profileid)
                                        .SetParameter("userid", userid);
                        int isUpdated = query.ExecuteUpdate();
                        transaction.Commit();
                        return isUpdated;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }//End Transaction
            }//End Session
        }

        public int updateTwitterFeed(Domain.Socioboard.Domain.TwitterFeed twtfeed)
        {
            throw new NotImplementedException();
        }

        /// <getAllTwitterFeedOfUsers>
        /// Get All Twitter Feed Of Users
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <param name="profileid">Profile id.(String)</param>
        /// <returns>Return object of TwitterFeed Class with  value of each member in the form of list.(List<TwitterFeed>)</returns>
        public List<Domain.Socioboard.Domain.TwitterFeed> getAllTwitterFeedOfUsers(Guid UserId, string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.TwitterFeed> lstmsg = session.CreateQuery("from TwitterFeed where UserId = :userid and ProfileId = :profid")
                        .SetParameter("userid", UserId)
                        .SetParameter("profid", profileid)
                        .SetFirstResult(0)
                        .SetMaxResults(1500)
                        .List<Domain.Socioboard.Domain.TwitterFeed>()
                        .ToList<Domain.Socioboard.Domain.TwitterFeed>();


                        //List<TwitterFeed> lstmsg = new List<TwitterFeed>();
                        //foreach (TwitterFeed item in query.Enumerable<TwitterFeed>().OrderByDescending(x=>x.FeedDate))
                        //{
                        //    lstmsg.Add(item);
                        //}
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        // Edited by Antima

         /// <getAllTwitterFeedOfUsers>
        /// Get All Twitter Feed Of Users
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <param name="profileid">Profile id.(String)</param>
        /// <returns>Return object of TwitterFeed Class with  value of each member in the form of list.(List<TwitterFeed>)</returns>
        public List<Domain.Socioboard.Domain.TwitterFeed> getAllTwitterFeedOfUsers(Guid UserId, string profileid,int count)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.TwitterFeed> lstmsg = session.CreateQuery("from TwitterFeed where UserId = :userid and ProfileId = :profid")
                        .SetParameter("userid", UserId)
                        .SetParameter("profid", profileid)
                        .SetFirstResult(count)
                        .SetMaxResults(10)
                        .List<Domain.Socioboard.Domain.TwitterFeed>()
                        .ToList<Domain.Socioboard.Domain.TwitterFeed>();
                      
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        /// <checkTwitterFeedExists>
        /// Check Twitter Feed is Exists or not
        /// </summary>
        /// <param name="Id">Id of twitter feed.(string)</param>
        /// <param name="Userid">User id.(Guid)</param>
        /// <param name="messageId">Message id of feed.(string) </param>
        /// <returns>True or False.(bool)</returns>
        public bool checkTwitterFeedExists(string Id, Guid Userid, string messageId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to find twitter feeds by user id, profile id and message id.
                        NHibernate.IQuery query = session.CreateQuery("from TwitterFeed where UserId = :userid and ProfileId = :Twtuserid and MessageId = :messid");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("Twtuserid", Id);
                        query.SetParameter("messid", messageId);
                        var result = query.UniqueResult();

                        if (result == null)
                            return false;
                        else
                            return true;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }
                }//End Transaction
            }//End Session
        }

        /// <getAllTwitterFeeds>
        /// Get All Twitter Feeds
        /// </summary>
        /// <param name="userid">User id.(Guid)</param>
        /// <returns>Return object of TwitterFeed Class with  value of each member in the form of list.(List<TwitterFeed>)</returns>
        public List<Domain.Socioboard.Domain.TwitterFeed> getAllTwitterFeeds(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get twitter feed by user id.
                        List<Domain.Socioboard.Domain.TwitterFeed> lstmsg = session.CreateQuery("from TwitterFeed where UserId = :userid")
                        .SetParameter("userid", userid)
                        .List<Domain.Socioboard.Domain.TwitterFeed>()
                        .ToList<Domain.Socioboard.Domain.TwitterFeed>();
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        /// <getTwitterFeedOfUsers>
        /// Get Twitter Feed Of Users
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <param name="profileid">Twitter account profile id.(String)</param>
        /// <returns>Return object of TwitterFeed Class with  value of each member in the form of list.(List<TwitterFeed>)</returns>
        public List<Domain.Socioboard.Domain.TwitterFeed> getTwitterFeedOfUsers(Guid UserId, string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get feeds by user id and profile id.
                        List<Domain.Socioboard.Domain.TwitterFeed> lstmsg = session.CreateQuery("from TwitterFeed where UserId = :userid and ProfileId = :profid and Type ='twt_feeds'")
                        .SetParameter("userid", UserId)
                        .SetParameter("profid", profileid)
                        .List<Domain.Socioboard.Domain.TwitterFeed>()
                        .ToList<Domain.Socioboard.Domain.TwitterFeed>();

                        //List<TwitterFeed> lstmsg = new List<TwitterFeed>();
                        //foreach (TwitterFeed item in query.Enumerable<TwitterFeed>().OrderByDescending(x=>x.FeedDate))
                        //{
                        //    lstmsg.Add(item);
                        //}
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        /// <getTwitterMentionsOfUser>
        /// Get Twitter Mentions Of User
        /// </summary>
        /// <param name="userId">User id.(Guid)</param>
        /// <param name="profileid">Profile id.(String)</param>
        /// <returns>Return object of TwitterFeed Class with  value of each member in the form of list.(List<TwitterFeed>)</returns>
        public List<Domain.Socioboard.Domain.TwitterFeed> getTwitterMentionsOfUser(Guid userId, string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get feeds by user id and profile id.
                        List<Domain.Socioboard.Domain.TwitterFeed> lstmsg = session.CreateQuery("from TwitterFeed where UserId = :userid and ProfileId = :profid and Type ='twt_mentions'")
                        .SetParameter("userid", userId)
                        .SetParameter("profid", profileid)
                        .List<Domain.Socioboard.Domain.TwitterFeed>()
                        .ToList<Domain.Socioboard.Domain.TwitterFeed>();

                        //List<TwitterFeed> lstmsg = new List<TwitterFeed>();
                        //foreach (TwitterFeed item in query.Enumerable<TwitterFeed>().OrderByDescending(x=>x.FeedDate))
                        //{
                        //    lstmsg.Add(item);
                        //}


                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        /// <getTwitterFeedOfProfile>
        /// Get Twitter Feed Of Profile
        /// </summary>
        /// <param name="profileid">profile id.(string)</param>
        /// <returns>Return object of TwitterFeed Class with  value of each member in the form of list.(List<TwitterFeed>)</returns>
        public List<Domain.Socioboard.Domain.TwitterFeed> getTwitterFeedOfProfile(string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get feeds of twitter account by profile id.
                        List<Domain.Socioboard.Domain.TwitterFeed> lstmsg = session.CreateQuery("from TwitterFeed where ProfileId = :profid and Type ='twt_feeds' order by FeedDate desc")

                        .SetParameter("profid", profileid)
                        .List<Domain.Socioboard.Domain.TwitterFeed>()
                        .ToList<Domain.Socioboard.Domain.TwitterFeed>();

                        //List<TwitterFeed> lstmsg = new List<TwitterFeed>();
                        //foreach (TwitterFeed item in query.Enumerable<TwitterFeed>().OrderByDescending(x => x.FeedDate))
                        //{
                        //    lstmsg.Add(item);
                        //}
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        /// <getTwitterMentionsOfProfile>
        /// Get Twitter Mentions Of Profile
        /// </summary>
        /// <param name="profileid">Profile id.(String)</param>
        /// <returns>Return object of TwitterFeed Class with  value of each member in the form of list.(List<TwitterFeed>)</returns>
        public List<Domain.Socioboard.Domain.TwitterFeed> getTwitterMentionsOfProfile(string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //proceed action, to get account feeds by profile id.
                        List<Domain.Socioboard.Domain.TwitterFeed> lstmsg = session.CreateQuery("from TwitterFeed where ProfileId = :profid and Type ='twt_mentions'")
                         .SetParameter("profid", profileid)
                         .List<Domain.Socioboard.Domain.TwitterFeed>()
                         .ToList<Domain.Socioboard.Domain.TwitterFeed>();

                        //List<TwitterFeed> lstmsg = new List<TwitterFeed>();
                        //foreach (TwitterFeed item in query.Enumerable<TwitterFeed>().OrderByDescending(x => x.FeedDate))
                        //{
                        //    lstmsg.Add(item);
                        //}
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        /// <checkTwitterFeedExists>
        /// Check Twitter Feed Exists
        /// </summary>
        /// <param name="messageId">Message id.(string)</param>
        /// <returns>True or False.(bool)</returns>
        public bool checkTwitterFeedExists(string messageId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to get feed by message id.
                        NHibernate.IQuery query = session.CreateQuery("from TwitterFeed where MessageId = :messid");
                        
                        query.SetParameter("messid", messageId);
                        var result = query.UniqueResult();

                        if (result == null)
                            return false;
                        else
                            return true;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }
                }//End Transaction
            }//End Session
        }

        /// <DeleteTwitterFeedByUserid>
        /// Delete Twitter Feed By Userid
        /// </summary>
        /// <param name="userid">User id.(Guid)</param>
        /// <returns>Return 1 for success and 0 for failure.(int) </returns>
        public int DeleteTwitterFeedByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete all feed of user.
                        NHibernate.IQuery query = session.CreateQuery("delete from TwitterFeed where UserId = :userid")
                                        .SetParameter("userid", userid);
                        int isUpdated = query.ExecuteUpdate();
                        transaction.Commit();
                        return isUpdated;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }//End Transaction
            }//End Session
        }

        public int getAllInboxMessagesByProfileid(Guid userid, string profileid, int day)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    DateTime AssignDate = DateTime.Now;
                    //string AssinDate = AssignDate.AddDays(-day).ToString("yyyy-MM-dd HH:mm:ss");
                    DateTime AssinDate = AssignDate.AddDays(-day);
                    try
                    {
                       // string str = "count(Id) from TwitterFeed where Userid=:userid and ProfileId IN(";
                       // string[] arrsrt = profileid.Split(',');
                       // foreach (string sstr in arrsrt)
                       // {
                       //     str += "'" + (sstr) + "'" + ",";
                       // }
                       // str = str.Substring(0, str.Length - 1);
                       // str += ")";
                       // List<Domain.Socioboard.Domain.TwitterFeed> alst = session.CreateQuery(str)
                       //.SetParameter("userid", userid)
                       //.List<Domain.Socioboard.Domain.TwitterFeed>().Where(d=>d.FeedDate.Date>=AssinDate.Date)
                       //.ToList<Domain.Socioboard.Domain.TwitterFeed>();
                       // return alst;

                        string[] arrsrt = profileid.Split(',');

                        int alst = session.Query<Domain.Socioboard.Domain.TwitterFeed>().Where(x => x.UserId.Equals(userid) && arrsrt.Contains(x.ProfileId) && x.FeedDate.Date>=AssinDate.Date).Count()//.CreateQuery("from FacebookFeed where  UserId = :UserId and FeedDescription like %' =:keyword '% ORDER BY FeedDate DESC")
                       ;

                        return alst;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }

                }//End Trasaction
            }//End session

        }

        public Domain.Socioboard.Domain.TwitterFeed getTwitterFeed(string id)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get feeds of twitter account by profile id.
                        Domain.Socioboard.Domain.TwitterFeed lstmsg = session.CreateQuery("from TwitterFeed where Id = :id")
                        .SetParameter("id", id).UniqueResult<Domain.Socioboard.Domain.TwitterFeed>();


                        //List<TwitterFeed> lstmsg = new List<TwitterFeed>();
                        //foreach (TwitterFeed item in query.Enumerable<TwitterFeed>().OrderByDescending(x => x.FeedDate))
                        //{
                        //    lstmsg.Add(item);
                        //}
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        // Edited by Antima[20/12/2014]

        public Domain.Socioboard.Domain.TwitterFeed getTwitterFeedByProfileId(string ProfileId, string MessageId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get feeds of twitter account by profile id.
                        Domain.Socioboard.Domain.TwitterFeed msg = session.CreateQuery("from TwitterFeed where ProfileId = :ProfileId and MessageId =:MessageId")
                        .SetParameter("ProfileId", ProfileId)
                        .SetParameter("MessageId", MessageId).UniqueResult<Domain.Socioboard.Domain.TwitterFeed>();

                        return msg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }


        /// <getAllTwitterFeedOfUsers>
        /// Get All Twitter Feed Of Users
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <param name="profileid">Profile id.(String)</param>
        /// <returns>Return object of TwitterFeed Class with  value of each member in the form of list.(List<TwitterFeed>)</returns>
        public List<Domain.Socioboard.Domain.TwitterFeed> getAllTwitterFeedOfUsersByKeyword(string UserId, string profileid, string keyword, int count)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //List<Domain.Socioboard.Domain.TwitterFeed> lstmsg = session.CreateQuery("from TwitterFeed where UserId = :userid and ProfileId = :profid")
                        //.SetParameter("userid", UserId)
                        //.SetParameter("profid", profileid)
                        //.SetFirstResult(count)
                        //.SetMaxResults(10)
                        //.List<Domain.Socioboard.Domain.TwitterFeed>()
                        //.ToList<Domain.Socioboard.Domain.TwitterFeed>();

                        List<Domain.Socioboard.Domain.TwitterFeed> lstmsg = session.Query<Domain.Socioboard.Domain.TwitterFeed>().Where(x => x.Feed.Contains(keyword) && x.UserId.Equals(Guid.Parse(UserId)) && x.ProfileId.Equals(profileid)).OrderByDescending(x => x.FeedDate).Take(20)//.CreateQuery("from FacebookFeed where  UserId = :UserId and FeedDescription like %' =:keyword '% ORDER BY FeedDate DESC")
                            //.List<Domain.Socioboard.Domain.FacebookFeed>()
                       .ToList<Domain.Socioboard.Domain.TwitterFeed>();

                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        /// <getAllFacebookUserFeeds>
        /// Get All Facebook User Feeds
        /// </summary>
        /// <param name="profileid">Profile id</param>
        /// <returns>List of Facebbok feeds (List<FacebookFeed>)</returns>
        public List<Domain.Socioboard.Domain.TwitterFeed> getAllFeedsOfSBUserWithRangeAndProfileId(string UserId, string profileId, string noOfDataToSkip, string noOfResultsFromTop)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all facebook feeds of profile by facebook profile id  
                        List<Domain.Socioboard.Domain.TwitterFeed> alst = session.Query<Domain.Socioboard.Domain.TwitterFeed>().Where(x => x.UserId.Equals(Guid.Parse(UserId)) && x.ProfileId.Equals(profileId)).OrderByDescending(x => x.FeedDate).Skip(Convert.ToInt32(noOfDataToSkip)).Take(Convert.ToInt32(noOfResultsFromTop))//.CreateQuery("from FacebookFeed where  UserId = :UserId and FeedDescription like %' =:keyword '% ORDER BY FeedDate DESC")
                        .ToList<Domain.Socioboard.Domain.TwitterFeed>();

                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }// End session

        }

        /// <getAllFacebookUserFeeds>
        /// Get All Facebook User Feeds
        /// </summary>
        /// <param name="profileid">Profile id</param>
        /// <returns>List of Facebbok feeds (List<FacebookFeed>)</returns>
        public List<Domain.Socioboard.Domain.TwitterFeed> getAllFeedsOfSBUserWithRangeByProfileId(string profileid, string noOfDataToSkip, string noOfResultsFromTop)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all facebook feeds of profile by facebook profile id  
                        List<Domain.Socioboard.Domain.TwitterFeed> alst = session.Query<Domain.Socioboard.Domain.TwitterFeed>().Where(x => x.ProfileId.Equals(profileid)).OrderByDescending(x => x.FeedDate).Skip(Convert.ToInt32(noOfDataToSkip)).Take(Convert.ToInt32(noOfResultsFromTop))//.CreateQuery("from FacebookFeed where  UserId = :UserId and FeedDescription like %' =:keyword '% ORDER BY FeedDate DESC")
                        .ToList<Domain.Socioboard.Domain.TwitterFeed>();

                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }// End se

        }




        public List<Domain.Socioboard.Domain.TwitterFeed> getAllTwitterFeedsMongo(int skip)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action to get all Facebook Message of User.
                        //List<Domain.Socioboard.Domain.TwitterFeed> alst = session.CreateQuery("from TwitterFeed")
                        // .List<Domain.Socioboard.Domain.TwitterFeed>().Skip(skip).Take(50)
                        // .ToList<Domain.Socioboard.Domain.TwitterFeed>();

                        List<Domain.Socioboard.Domain.TwitterFeed> alst = session.Query<Domain.Socioboard.Domain.TwitterFeed>().Skip(Convert.ToInt32(skip)).Take(50).ToList<Domain.Socioboard.Domain.TwitterFeed>();


                        #region oldcode
                        //List<FacebookMessage> alst = new List<FacebookMessage>();
                        //foreach (FacebookMessage item in query.Enumerable<FacebookMessage>().OrderByDescending(x => x.MessageDate))
                        //{
                        //    alst.Add(item);
                        //} 
                        #endregion

                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End session
        }
    }
}