using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Socioboard.Helper;
using Domain.Socioboard.Domain;
namespace Api.Socioboard.Services
{
    public class RssFeedsRepository 
    {

        /// <AddRssFeed>
        /// Add new Rss feeds
        /// </summary>
        /// <param name="rss">Set Values in a RssFeeds Class Property and Pass the Object of RssFeeds Class.(Domein.RssFeeds)</param>
        public void AddRssFeed(Domain.Socioboard.Domain.RssFeeds rss)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save new Rss feed.
                    session.Save(rss);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }


    


        public bool RssfeddExitbyurl(string feedurl,Guid userid,string profileId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from  RssFeeds u where u.FeedUrl = : feedurl and u.UserId=:userid and u.ProfileId =:profileId");
                        query.SetParameter("feedurl", feedurl);
                        query.SetParameter("profileId", profileId);
                        query.SetParameter("userid", userid);
                        var result = query.UniqueResult();
                        if (result == null)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }
                }
            }
        }


        public bool RssfeddExit(string message,Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from  RssFeeds u where u.Message = : message and u.UserId=:userid");
                        query.SetParameter("message", message)
                        .SetParameter("userid", userid);
                        var result = query.UniqueResult();
                        if (result == null)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }
                }
            }
        }

        public List<Domain.Socioboard.Domain.RssFeeds> getAllRssFeedInfo(Guid Id)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get data from id.
                        List<Domain.Socioboard.Domain.RssFeeds> lstRssReader = session.CreateQuery("from RssFeeds where UserId =:userid group by FeedUrl,ProfileId")
                        .SetParameter("userid", Id.ToString())
                        .List<Domain.Socioboard.Domain.RssFeeds>()
                        .ToList<Domain.Socioboard.Domain.RssFeeds>();

                        //List<RssReader> lstRssReader = new List<RssReader>();
                        //foreach (RssReader item in query.Enumerable<RssReader>())
                        //{
                        //    lstRssReader.Add(item);
                        //}
                        return lstRssReader;
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }
        public List<Domain.Socioboard.Domain.RssFeeds> GetUnsentRssFeedByProfileType(string profiletype)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //proceed action, to get all messages by user id and profileid.
                        List<Domain.Socioboard.Domain.RssFeeds> lstschmsg = session.CreateQuery("from RssFeeds where  ProfileType =:profiletype and Status = 0")
                       .SetParameter("profiletype", profiletype)
                       .List<Domain.Socioboard.Domain.RssFeeds>()
                       .ToList<Domain.Socioboard.Domain.RssFeeds>();


                        //List<ScheduledMessage> lstschmsg = new List<ScheduledMessage>();
                        //foreach (ScheduledMessage item in query.Enumerable())
                        //{
                        //    lstschmsg.Add(item);
                        //}
                        return lstschmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        public void DeleteRss(RssFeeds rss)
        {
            throw new NotImplementedException();
        }

        public void UpdateRss(RssFeeds rss)
        {
            throw new NotImplementedException();
        }


        /// <getAllActiveRssFeeds>
        /// get all cctive rss feeds by user id
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <returns>Return object of RssFeeds Class with  value of each member in the form of list.(List<RssFeeds>)</returns>
        public List<RssFeeds> getAllActiveRssFeeds(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all active rss feeds of user.
                        List<RssFeeds> lst = session.CreateQuery("from RssFeeds where UserId =:userid and Status = 0")
                        .SetParameter("userid", UserId)
                        .List<RssFeeds>()
                        .ToList<RssFeeds>();

                        //List<RssFeeds> lst = new List<RssFeeds>();
                        //foreach (RssFeeds item in query.Enumerable<RssFeeds>())
                        //{
                        //    lst.Add(item);
                        //}
                        return lst;
                    }
                    catch (Exception ezx)
                    {
                        Console.WriteLine(ezx.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }



        public List<RssFeeds> getAllRssFeedsUserbygroup(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all records of scheduled message.
                        List<RssFeeds> lstschtime = session.CreateQuery("from RssFeeds where UserId=:userid group by FeedUrl")
                        .SetParameter("userid", userid)
                            .List<RssFeeds>()
                        .ToList<RssFeeds>();

                        //List<ScheduledMessage> lstschtime = query.Enumerable<ScheduledMessage>().ToList<ScheduledMessage>();

                        return lstschtime;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        public List<RssFeeds> getProfilecountUserbygroup(string feedlink)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all records of scheduled message.
                        List<RssFeeds> lstschtime = session.CreateQuery("from RssFeeds where FeedUrl=:feedlink group by FeedUrl,ProfileId")
                        .SetParameter("feedlink", feedlink)
                            .List<RssFeeds>()
                        .ToList<RssFeeds>();

                        //List<ScheduledMessage> lstschtime = query.Enumerable<ScheduledMessage>().ToList<ScheduledMessage>();

                        return lstschtime;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        public List<RssFeeds> getAllRssFeedsUserbylink(string  link)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all records of scheduled message.
                        List<RssFeeds> lstschtime = session.CreateQuery("from RssFeeds where FeedUrl=:link")
                        .SetParameter("link", link)
                            .List<RssFeeds>()
                        .ToList<RssFeeds>();

                        //List<ScheduledMessage> lstschtime = query.Enumerable<ScheduledMessage>().ToList<ScheduledMessage>();

                        return lstschtime;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }


        // <getAllActiveRssFeeds>
        /// get all cctive rss feeds by ststus
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <returns>Return object of RssFeeds Class with  value of each member in the form of list.(List<RssFeeds>)</returns>
        public List<Domain.Socioboard.Domain.RssFeeds> getAllActiveRssFeedsbystatus()
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all active rss feeds of user.
                        List<Domain.Socioboard.Domain.RssFeeds> lst = session.CreateQuery("from RssFeeds group by FeedUrl,ProfileId,Profiletype")
                        .List<Domain.Socioboard.Domain.RssFeeds>()
                        .ToList<Domain.Socioboard.Domain.RssFeeds>();

                        //List<RssFeeds> lst = new List<RssFeeds>();
                        //foreach (RssFeeds item in query.Enumerable<RssFeeds>())
                        //{
                        //    lst.Add(item);
                        //}
                        return lst;
                    }
                    catch (Exception ezx)
                    {
                        Console.WriteLine(ezx.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }


        /// <getAllActiveRssFeeds>
        /// Get all active rss feeds 
        /// </summary>
        /// <returns>Return object of RssFeeds Class with  value of each member in the form of IEnumerable list.(List<RssFeeds>)</returns>
        public IEnumerable<Domain.Socioboard.Domain.RssFeeds> getAllActiveRssFeeds()
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all feeds
                        return session.CreateCriteria(typeof(Domain.Socioboard.Domain.RssFeeds)).List<Domain.Socioboard.Domain.RssFeeds>().Where(x => x.Status == false);

                        //NHibernate.IQuery query = session.CreateQuery("from RssFeeds where Status = 0");
                        //List<RssFeeds> lst = new List<RssFeeds>();
                        //foreach (RssFeeds item in query.Enumerable<RssFeeds>())
                        //{
                        //    lst.Add(item);
                        //}
                        //return lst;
                    }
                    catch (Exception ezx)
                    {
                        Console.WriteLine(ezx.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }


        /// <updateFeedStatus>
        /// update Feed Status
        /// </summary>
        /// <param name="UserId">User id (Guid)</param>
        /// <param name="message">New Message of feed.(String)</param>
        /// <returns>Return 1 for Successfully updated and 0 for Failed Updation.(int)</returns>
        public int updateFeedStatus(Guid UserId, string message)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all feeds where status is 1.
                        int i = session.CreateQuery("Update RssFeeds set Status = 1 where UserId=:userid and Message =:message")
                                   .SetParameter("userid", UserId)
                                   .SetParameter("message", message)
                                   .ExecuteUpdate();
                        transaction.Commit();
                        return i;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }//End Transaction
            }//End Session
        }


        public int updateFeedurl(Guid UserId,string newurl,string oldurl,string ProfileId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all feeds where status is 1.
                        int i = session.CreateQuery("Update RssFeeds set FeedUrl =:newurl where UserId=:UserId and FeedUrl =:oldurl and ProfileId =:ProfileId")
                                   .SetParameter("UserId", UserId)
                                   .SetParameter("newurl", newurl)
                                   .SetParameter("oldurl", oldurl)
                                   .SetParameter("ProfileId",ProfileId)
                                   .ExecuteUpdate();
                        transaction.Commit();
                        return i;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }//End Transaction
            }//End Session
        }


        
        /*Toggle must be play/pause**/

        /// <updateFeedStatus>
        /// Update feed status
        /// </summary>
        /// <param name="Toggle">Toggle must be play/pause. (String)</param>
        /// <param name="Id">Id of rss feed.</param>
        /// <returns>Return 1 for Updated and 0 for not updated.(int)</returns>
        public int updateFeedStatus(string Toggle, Guid Id)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = 0;
                        //When the toggle value is pause
                        //Than change the rss feeds status is 1.
                        if (Toggle == "pause")
                        {
                            //Proceed action, to change status of rss feed 
                            i = session.CreateQuery("Update RssFeeds set Status = 1 where Id=:userid")
                                       .SetParameter("userid", Id)
                                       .ExecuteUpdate();
                            transaction.Commit();

                        }
                        //When the toggle value is play
                        //Than change the rss feeds status is 0.
                        else if (Toggle == "play")
                        {
                            //Proceed action, to change status of rss feed 
                            i = session.CreateQuery("Update RssFeeds set Status = 0 where Id=:userid")
                                       .SetParameter("userid", Id)
                                       .ExecuteUpdate();
                            transaction.Commit();
                        }
                        return i;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }//End Transaction
            }//End Session
        }


        /// <DeleteRssMessage>
        /// Delete Rss feed Message.
        /// </summary>
        /// <param name="Id">Rssfeed id.(Guid)</param>
        /// <returns>Return 1 for Deleted and 0 for not delete.(int)</returns>
        public int DeleteRssMessage(Guid Id)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete rss feed by user id.
                        NHibernate.IQuery query = session.CreateQuery("delete from RssFeeds where Id = :userid ");
                        query.SetParameter("userid", Id);
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


        /// <DeleteRssFeedsByUserid>
        /// Delete RssFeeds By User id
        /// </summary>
        /// <param name="userid">User id.(Guid)</param>
        /// <returns>Return 1 for Deleted and 0 for not delete.(int)</returns>
        public int DeleteRssFeedsByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to delete rs data from table where user id is same.
                        NHibernate.IQuery query = session.CreateQuery("delete from RssFeeds where UserId = :userid")
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

        public int DeleteProfile(Guid Userid, string FeedUrl, string ProfileId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    int str = 0;
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from RssFeeds where UserId =:UserId and ProfileId=:ProfileId")
                            .SetParameter("UserId", Userid)
                            .SetParameter("ProfileId", ProfileId);
                        str = query.ExecuteUpdate();
                        transaction.Commit();
                        return str;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return str;
                    }
                }
            }
        }
    }
}