using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class FacebookFeedRepository : IFacebookFeedRepository
    {

        /// <addFacebookFeed>
        /// add new Facebook Feed
        /// </summary>
        /// <param name="fbfeed">Set Values in a Facebook feed Class Property and Pass the Object of Facebook feed Class (SocioBoard.Domain.Facebookfeed).</param>
        public void addFacebookFeed(FacebookFeed fbfeed)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Procees action, to save new data.
                    session.Save(fbfeed);
                    transaction.Commit();
                }//End Trasaction
            }//End session
        }

        
        public int deleteFacebookFeed(FacebookFeed fbfeed)
        {
            throw new NotImplementedException();
        }

        public int updateFacebookFeed(FacebookFeed fbfeed)
        {
            throw new NotImplementedException();
        }


        /// <getAllFacebookFeedsOfUser>
        /// get All Facebook Feeds Of User
        /// </summary>
        /// <param name="UserId">Userid of FacebookFeed</param>
        /// <param name="profileid">Profileid of FacebookFeed</param>
        /// <returns>List of Facebook feeds.(List<FacebookFeed>)</returns>
        public List<FacebookFeed> getAllFacebookFeedsOfUser(Guid UserId, string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all data of facebook feed by user id and profileid.
                        List<FacebookFeed> alst = session.CreateQuery("from FacebookFeed where UserId = :userid and ProfileId = :profileId")
                        .SetParameter("userid", UserId)
                        .SetParameter("profileId", profileid)
                        .List<FacebookFeed>()
                        .ToList<FacebookFeed>();


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
            }//End session 
        }


        /// <getAllFacebookFeedsOfUser>
        /// Get All Facebook Feeds Of User
        /// </summary>
        /// <param name="UserId">Userid of FacebookFeed</param>
        /// <param name="profileid">Profileid of FacebookFeed</param>
        /// <param name="count">Get Total number of data.</param>
        /// <returns>List of Facebook feeds.(List<FacebookFeed>)</returns>
        public List<FacebookFeed> getAllFacebookFeedsOfUser(Guid UserId, string profileid, int count)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all data of facebook feed by user id and profileid.
                        List<FacebookFeed> alst = session.CreateQuery("from FacebookFeed where UserId = :userid and ProfileId = :profileId")
                        .SetParameter("userid", UserId)
                        .SetParameter("profileId", profileid)
                        .SetFirstResult(count)
                        .SetMaxResults(10)
                         .List<FacebookFeed>()
                         .ToList<FacebookFeed>();



                        #region oldcode
                        ////List<FacebookFeed> alst = new List<FacebookFeed>();
                        ////foreach (FacebookFeed item in query.Enumerable<FacebookFeed>().OrderByDescending(x => x.FeedDate))
                        ////{
                        ////    alst.Add(item);
                        ////} 
                        #endregion

                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }


        /// <checkFacebookFeedExists>
        /// Check Facebook Feed is Exists
        /// </summary>
        /// <param name="feedid">Facebook feed Id</param>
        /// <param name="Userid">User id (Guid)</param>
        /// <returns>True or false (bool)</returns>
        public bool checkFacebookFeedExists(string feedid, Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all data of facebook feed by user feed id and User id(Guid).
                        NHibernate.IQuery query = session.CreateQuery("from FacebookFeed where UserId = :userid and FeedId = :msgid");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("msgid", feedid);
                        var resutl = query.UniqueResult();

                        if (resutl == null)
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
            }//End session
        }


        /// <deleteAllFeedsOfUser>
        /// Delete All Feeds Of User
        /// </summary>
        /// <param name="fbuserid">Facebook user id</param>
        /// <param name="userid">User id(Guid)</param>
        public void deleteAllFeedsOfUser(string fbuserid, Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to Delete data
                        NHibernate.IQuery query = session.CreateQuery("delete from FacebookFeed where UserId = :userid and ProfileId = :profileId");
                        query.SetParameter("userid", userid);
                        query.SetParameter("profileId", fbuserid);
                        int isUpdated = query.ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);

                    }

                }//End Transaction
            }//End session
        }


        //public int updateFacebookFeedStatus(string fbfeed)
        //{

        //    using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //    {
        //        using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //        { 



        //        }
        //    }

        //}


        /// <countUnreadMessages>
        /// Get the total counts of Unread Messages of user from facebook feed.
        /// </summary>
        /// <param name="UserId">User id(Guid)</param>
        /// <returns>Todal number of messages(Int)</returns>
        public int countUnreadMessages(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to Unread messages of related user.
                        NHibernate.IQuery query = session.CreateQuery("from FacebookFeed where ReadStatus = 0 and UserId=:userid")
                                     .SetParameter("userid", UserId);
                        int i = 0;
                        //Count rows 
                        foreach (var item in query.Enumerable<FacebookFeed>())
                        {
                            i++;
                        }
                        return i;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }

                }//End Transaction
            }//End session

        }


        /// <getUnreadMessages>
        /// Get the All of Unread Messages of user.
        /// </summary>
        /// <param name="UserId">User Id(Guid)</param>
        /// <param name="profileId">Profile Id</param>
        /// <returns>List of Faceook Feeds(List<FacebookFeed>) </returns>
        public List<FacebookFeed> getUnreadMessages(Guid UserId, string profileId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to get all feeds of user.
                        List<FacebookFeed> lstfbfeed = session.CreateQuery("from FacebookFeed where ReadStatus = 0 and UserId=:userid and ProfileId = :profid ORDER BY EntryDate DESC")
                                     .SetParameter("userid", UserId)
                                     .SetParameter("profid", profileId)
                                     .List<FacebookFeed>()
                                     .ToList<FacebookFeed>();

                        #region Oldcode
                        //List<FacebookFeed> lstfbfeed = new List<FacebookFeed>();
                        //foreach (FacebookFeed item in query.Enumerable<FacebookFeed>())
                        //{
                        //    lstfbfeed.Add(item);
                        //} 
                        #endregion


                        return lstfbfeed;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End session
        }


        /// <getAllFacebookUserFeeds>
        /// Get All Facebook User Feeds
        /// </summary>
        /// <param name="profileid">Profile id</param>
        /// <returns>List of Facebbok feeds (List<FacebookFeed>)</returns>
        public List<FacebookFeed> getAllFacebookUserFeeds(string profileid)
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
                        List<FacebookFeed> alst = session.CreateQuery("from FacebookFeed where  ProfileId = :profileId ORDER BY EntryDate DESC")
                        .SetParameter("profileId", profileid)
                        .List<FacebookFeed>()
                        .ToList<FacebookFeed>();

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


        /// <checkFacebookFeedExists>
        /// Check Exists FacebookFeed by feed id. 
        /// </summary>
        /// <param name="feedsid" type="String">Feed Id</param>
        /// <returns>Bool value (True or False)</returns>
        public bool checkFacebookFeedExists(string feedsid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get feed by messages id.
                        NHibernate.IQuery query = session.CreateQuery("from FacebookFeed where  FeedId = :msgid");
                        query.SetParameter("msgid", feedsid);
                        var resutl = query.UniqueResult();

                        if (resutl == null)
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


        /// <updateMessageStatus>
        /// Update/Change Message Status
        /// </summary>
        /// <param name="UserId">User Id (Guid)</param>
        /// <returns>When process is successfullt done its return 1 otherwise return 0.</returns>
        public int updateMessageStatus(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to update status by user id.
                        int i = session.CreateQuery("Update FacebookFeed set ReadStatus =1 where UserId = :id")
                                   .SetParameter("id", UserId)
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
            }//End session
        }
        

        /// <getAllReadFacebookFeeds>
        /// Get All Read Facebook Feeds
        /// </summary>
        /// <param name="UserId">User id(Guid)</param>
        /// <param name="profileid">Facebook profile Id(String)</param>
        /// <returns>List of Facebook feeds(List<FacebookFeed>)</returns>
        public List<FacebookFeed> getAllReadFacebookFeeds(Guid UserId, string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all read feed messages by user id and profile id. 
                        //Order by EntryDate DESC
                        List<FacebookFeed> alst = session.CreateQuery("from FacebookFeed where ReadStatus = 1 and UserId = :userid and ProfileId = :profileId ORDER BY EntryDate DESC")
                       .SetParameter("userid", UserId)
                       .SetParameter("profileId", profileid)
                       .List<FacebookFeed>()
                       .ToList<FacebookFeed>();


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

                }//End Trasaction
            }//End session
        }


        /// <countInteractions>
        /// Count total number of Interactions
        /// </summary>
        /// <param name="UserId">User id (Guid)</param>
        /// <param name="profileid"> Facebook Profile (string) </param>
        /// <param name="days">Number of day/s (int)</param>
        /// <returns>Todatl number of Interactions</returns>
        public int countInteractions(Guid UserId, string profileid, int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select count(*) from FacebookFeed where ProfileId=:profileid and FeedDate<=DATE_ADD(NOW(),INTERVAL -" + days + " DAY)");
                        query.SetParameter("profileid", profileid);
                        int i = 0;
                        foreach (var item in query.List())
                        {
                            i++;
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


        /// <DeleteFacebookFeedByUserid>
        /// Delete Facebook Feed By Userid
        /// </summary>
        /// <param name="userid">User id (Guid)</param>
        /// <returns>0 for failure and 1 for success (int)</returns>
        public int DeleteFacebookFeedByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action , to delete data by user id.
                        NHibernate.IQuery query = session.CreateQuery("delete from FacebookFeed where UserId = :userid")
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
                }//End Trsansaction
            }//End session
        }

    }
}