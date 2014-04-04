using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class TwitterFeedRepository:ITwitterFeedRepository
    {

        /// <addTwitterFeed>
        /// Add Twitter Feed
        /// </summary>
        /// <param name="twtfeed">Set Values in a TwitterFeed Class Property and Pass the Object of TwitterFeed Class.(Domein.TwitterFeed)</param>
        public void addTwitterFeed(TwitterFeed twtfeed)
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


        /// <deleteTwitterFeed>
        /// Delete Twitter Feed
        /// </summary>
        /// <param name="twtfeed">Set Values of twitter profile id and user id in a TwitterFeed Class Property and Pass the Object of TwitterFeed Class.(Domein.TwitterFeed)</param>
        /// <returns>Return 1 for success and 0 for failure.(int) </returns>
        public int deleteTwitterFeed(TwitterFeed twtfeed)
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


        public int updateTwitterFeed(TwitterFeed twtfeed)
        {
            throw new NotImplementedException();
        }


        /// <getAllTwitterFeedOfUsers>
        /// Get All Twitter Feed Of Users
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <param name="profileid">Profile id.(String)</param>
        /// <returns>Return object of TwitterFeed Class with  value of each member in the form of list.(List<TwitterFeed>)</returns>
        public List<TwitterFeed> getAllTwitterFeedOfUsers(Guid UserId, string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<TwitterFeed> lstmsg = session.CreateQuery("from TwitterFeed where UserId = :userid and ProfileId = :profid")
                        .SetParameter("userid", UserId)
                        .SetParameter("profid", profileid)
                        .List<TwitterFeed>()
                        .ToList<TwitterFeed>();


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
        public List<TwitterFeed> getAllTwitterFeeds(Guid userid)
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
                        List<TwitterFeed> lstmsg = session.CreateQuery("from TwitterFeed where UserId = :userid")
                        .SetParameter("userid", userid)
                        .List<TwitterFeed>()
                        .ToList<TwitterFeed>();


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


        /// <getTwitterFeedOfUsers>
        /// Get Twitter Feed Of Users
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <param name="profileid">Twitter account profile id.(String)</param>
        /// <returns>Return object of TwitterFeed Class with  value of each member in the form of list.(List<TwitterFeed>)</returns>
        public List<TwitterFeed> getTwitterFeedOfUsers(Guid UserId, string profileid)
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
                        List<TwitterFeed> lstmsg = session.CreateQuery("from TwitterFeed where UserId = :userid and ProfileId = :profid and Type ='twt_feeds'")
                        .SetParameter("userid", UserId)
                        .SetParameter("profid", profileid)
                        .List<TwitterFeed>()
                        .ToList<TwitterFeed>();

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
        public List<TwitterFeed> getTwitterMentionsOfUser(Guid userId, string profileid)
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
                        List<TwitterFeed> lstmsg = session.CreateQuery("from TwitterFeed where UserId = :userid and ProfileId = :profid and Type ='twt_mentions'")
                        .SetParameter("userid", userId)
                        .SetParameter("profid", profileid)
                        .List<TwitterFeed>()
                        .ToList<TwitterFeed>();

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
        public List<TwitterFeed> getTwitterFeedOfProfile(string profileid)
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
                        List<TwitterFeed> lstmsg = session.CreateQuery("from TwitterFeed where ProfileId = :profid and Type ='twt_feeds'")

                        .SetParameter("profid", profileid)
                        .List<TwitterFeed>()
                        .ToList<TwitterFeed>();

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
        public List<TwitterFeed> getTwitterMentionsOfProfile(string profileid)
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
                        List<TwitterFeed> lstmsg = session.CreateQuery("from TwitterFeed where ProfileId = :profid and Type ='twt_mentions'")
                         .SetParameter("profid", profileid)
                         .List<TwitterFeed>()
                         .ToList<TwitterFeed>();

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

    }
}