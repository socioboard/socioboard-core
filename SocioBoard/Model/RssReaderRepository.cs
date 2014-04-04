using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class RssReaderRepository : IRssReader
    {

        /// <AddRssReader>
        /// Add new RssReader
        /// </summary>
        /// <param name="rss">Set Values in a RssReader Class Property and Pass the Object of RssReader Class.(Domein.RssReader)</param>
        public void AddRssReader(RssReader rss)
        {
            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction.
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        //Proceed action, to save new data.
                        session.Save(rss);
                        transaction.Commit();
                    }//End Transaction
                }//End Session
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }


        /// <DeleteRssReader>
        /// Delete RssReader
        /// </summary>
        /// <param name="rss">Set rssid in a RssReader Class Property and Pass the Object of RssReader Class.(Domein.RssReader)</param>
        /// <returns>Return 1 for true and 0 for false.(int)</returns>
        public int DeleteRssReader(RssReader rss)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to delete RssReader by id.
                        NHibernate.IQuery query = session.CreateQuery("delete from RssReader where Id = :adsid")
                                        .SetParameter("adsid", rss.Id);
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
        
    
        public void UpdateRssReader(RssReader rss)
        {
            throw new NotImplementedException();
        }


        /// <getAllRss>
        /// Get All Rss
        /// </summary>
        /// <param name="Id">Id of feedReader.(Guid)</param>
        /// <returns>Return object of RssReader Class with  value of each member in the form of list.(List<RssReader>)</returns>
        public List<RssReader> getAllRss(Guid Id)
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
                        List<RssReader> lstRssReader = session.CreateQuery("from RssReader where Id =:userid")
                        .SetParameter("userid", Id)
                        .List<RssReader>()
                        .ToList<RssReader>();

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


        /// <geturlRssFeed>
        /// Get RssFeed by URL
        /// </summary>
        /// <param name="strUrl">Url of feed.(String)</param>
        /// <returns>Return object of RssReader Class with  value of each member in the form of list.(List<RssReader>)</returns>
        public List<RssReader> geturlRssFeed(string strUrl)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<RssReader> lstRssReader = session.CreateQuery("from RssReader where FeedsUrl =:strUrl and Status=:status")
                       .SetParameter("strUrl", strUrl)
                       .SetParameter("status", false)
                       .List<RssReader>()
                       .ToList<RssReader>();

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


        /// <CheckFeedExists>
        /// Check the Feed is Exists.
        /// </summary>
        /// <param name="FeedsUrl">Url of feed.(String)</param>
        /// <param name="Message">Message of feedReader.(String)</param>
        /// <param name="PublishedDate">Date and time of publishing.(String)</param>
        /// <returns>True or False.(bool)</returns>
        public bool CheckFeedExists(string FeedsUrl, string Message, string PublishedDate)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from RssReader where FeedsUrl =:feedurl and Description =:desc and PublishedDate=:published");
                        query.SetParameter("feedurl", FeedsUrl);
                        query.SetParameter("desc", Message);
                        query.SetParameter("published", PublishedDate);
                        RssFeeds rss = query.UniqueResult<RssFeeds>();
                        if (rss == null)
                        {
                            return false;
                        }
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


        /// <UpdateStatus>
        /// Update Status of Rss reader.
        /// </summary>
        /// <param name="Id">Id of rss reader.(Guid)</param>
        public void UpdateStatus(Guid Id)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get rss reader by id.
                        int i = session.CreateQuery("update RssReader set Status= 1 where Id = :id")
                        .SetParameter("id", Id)
                        .ExecuteUpdate();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End Session
        }


        /// <DeleteRssReaderByUserid>
        /// Delete RssReader By Userid
        /// </summary>
        /// <param name="userid">User id.(Guid)</param>
        /// <returns>Is exist its return 1 , otherwise its return 0.(int)</returns>
        public int DeleteRssReaderByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to delete data by user id.
                        NHibernate.IQuery query = session.CreateQuery("delete from RssReader where UserId = :userid")
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