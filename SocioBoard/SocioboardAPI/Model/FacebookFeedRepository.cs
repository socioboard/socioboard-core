using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
using Api.Socioboard.Helper;
using NHibernate.Linq;
using NHibernate.Criterion;

namespace Api.Socioboard.Services
{
    public class FacebookFeedRepository : IFacebookFeedRepository
    {

        /// <addFacebookFeed>
        /// add new Facebook Feed
        /// </summary>
        /// <param name="fbfeed">Set Values in a Facebook feed Class Property and Pass the Object of Facebook feed Class (SocioBoard.Domain.Facebookfeed).</param>
        public void addFacebookFeed(Domain.Socioboard.Domain.FacebookFeed fbfeed)
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

        public bool checkFacebookUserExists(string ProfileId, Guid Userid)
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
                        // List<Domain.Socioboard.Domain.FacebookFeed> alst = session.CreateQuery("from FacebookFeed where UserId = :userid and ProfileId = :fbuserid")
                        // .SetParameter("userid", Userid)
                        // .SetParameter("fbuserid", ProfileId)
                        // .List<Domain.Socioboard.Domain.FacebookFeed>()
                        //.ToList<Domain.Socioboard.Domain.FacebookFeed>();
                        // if (alst.Count == 0 || alst == null)
                        //     return false;
                        // else
                        //     return true;

                        bool exist = session.Query<Domain.Socioboard.Domain.FacebookFeed>()
                            .Any(x => x.UserId == Userid && x.ProfileId == ProfileId);
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

        public List<Domain.Socioboard.Domain.FacebookFeed> getAllFeedDetail(string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        string str = "from FacebookFeed where ProfileId IN(";
                        string[] arrsrt = profileid.Split(',');
                        foreach (string sstr in arrsrt)
                        {
                            str += Convert.ToInt64(sstr) + ",";
                        }
                        str = str.Substring(0, str.Length - 1);
                        str += ")";
                        List<Domain.Socioboard.Domain.FacebookFeed> alst = session.CreateQuery(str)
                       .List<Domain.Socioboard.Domain.FacebookFeed>()
                       .ToList<Domain.Socioboard.Domain.FacebookFeed>();
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

        public List<Domain.Socioboard.Domain.FacebookFeed> getAllFeedDetail1(string profileid, Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        string str = "from FacebookFeed where UserId=:userid and ProfileId IN(";
                        string[] arrsrt = profileid.Split(',');
                        foreach (string sstr in arrsrt)
                        {
                            str += Convert.ToInt64(sstr) + ",";
                        }
                        str = str.Substring(0, str.Length - 1);
                        str += ")";
                        List<Domain.Socioboard.Domain.FacebookFeed> alst = session.CreateQuery(str).SetParameter("userid", userid)
                       .List<Domain.Socioboard.Domain.FacebookFeed>()
                       .ToList<Domain.Socioboard.Domain.FacebookFeed>();
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

        public Domain.Socioboard.Domain.FacebookFeed GetFacebookFeedByFeedId(Guid userid, string feedid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from FacebookFeed where UserId =: userid and FeedId = :feedid");
                        query.SetParameter("feedid", feedid);
                        query.SetParameter("userid", userid);
                        var result = query.UniqueResult();
                        return (Domain.Socioboard.Domain.FacebookFeed)result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }

        public int deleteFacebookFeed(Domain.Socioboard.Domain.FacebookFeed fbfeed)
        {
            throw new NotImplementedException();
        }

        public int updateFacebookFeed(Domain.Socioboard.Domain.FacebookFeed fbfeed)
        {
            throw new NotImplementedException();
        }

        /// <getAllFacebookFeedsOfUser>
        /// get All Facebook Feeds Of User
        /// </summary>
        /// <param name="UserId">Userid of FacebookFeed</param>
        /// <param name="profileid">Profileid of FacebookFeed</param>
        /// <returns>List of Facebook feeds.(List<FacebookFeed>)</returns>
        public List<Domain.Socioboard.Domain.FacebookFeed> getAllFacebookFeedsOfUser(Guid UserId, string profileid)
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
                        List<Domain.Socioboard.Domain.FacebookFeed> alst = session.CreateQuery("from FacebookFeed where UserId = :userid and ProfileId = :profileId")
                        .SetParameter("userid", UserId)
                        .SetParameter("profileId", profileid)
                        .List<Domain.Socioboard.Domain.FacebookFeed>()
                        .ToList<Domain.Socioboard.Domain.FacebookFeed>();


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
        public List<Domain.Socioboard.Domain.FacebookFeed> getAllFacebookFeedsOfUser(Guid UserId, string profileid, int count)
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
                        List<Domain.Socioboard.Domain.FacebookFeed> alst = session.CreateQuery("from FacebookFeed where UserId = :userid and ProfileId = :profileId")
                        .SetParameter("userid", UserId)
                        .SetParameter("profileId", profileid)
                        .SetFirstResult(count)
                        .SetMaxResults(10)
                         .List<Domain.Socioboard.Domain.FacebookFeed>()
                         .ToList<Domain.Socioboard.Domain.FacebookFeed>();



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
                        foreach (var item in query.Enumerable<Domain.Socioboard.Domain.FacebookFeed>())
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
        public List<Domain.Socioboard.Domain.FacebookFeed> getUnreadMessages(Guid UserId, string profileId)
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
                        List<Domain.Socioboard.Domain.FacebookFeed> lstfbfeed = session.CreateQuery("from FacebookFeed where ReadStatus = 0 and UserId=:userid and ProfileId = :profid ORDER BY EntryDate DESC")
                            //   List<FacebookFeed> lstfbfeed = session.CreateQuery("from FacebookFeed where ReadStatus = 0  and ProfileId = :profid ORDER BY EntryDate DESC")
                            //  .SetParameter("userid", UserId)
                                     .SetParameter("profid", profileId)
                                     .List<Domain.Socioboard.Domain.FacebookFeed>()
                                     .ToList<Domain.Socioboard.Domain.FacebookFeed>();

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

        public static List<Domain.Socioboard.Domain.FacebookFeed> getUnreadMessages(string profileId)
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
                        List<Domain.Socioboard.Domain.FacebookFeed> lstfbfeed = session.CreateQuery("from FacebookFeed where ReadStatus = 0  and ProfileId = :profid ORDER BY EntryDate DESC")
                                     .SetParameter("profid", profileId)
                                     .List<Domain.Socioboard.Domain.FacebookFeed>()
                                     .ToList<Domain.Socioboard.Domain.FacebookFeed>();

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
        public List<Domain.Socioboard.Domain.FacebookFeed> getAllFacebookUserFeeds(string profileid)
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
                        List<Domain.Socioboard.Domain.FacebookFeed> alst = session.CreateQuery("from FacebookFeed where  ProfileId = :profileId ORDER BY FeedDate DESC")
                        .SetParameter("profileId", profileid)
                        .List<Domain.Socioboard.Domain.FacebookFeed>()
                        .ToList<Domain.Socioboard.Domain.FacebookFeed>();

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
        public int updateMessageStatus(string profileid)
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
                        int i = session.CreateQuery("Update FacebookFeed set ReadStatus =1 where ProfileId = :profileid")
                                   .SetParameter("profileid", profileid)
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
        public List<Domain.Socioboard.Domain.FacebookFeed> getAllReadFacebookFeeds(Guid UserId, string profileid)
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
                        List<Domain.Socioboard.Domain.FacebookFeed> alst = session.CreateQuery("from FacebookFeed where ReadStatus = 1 and UserId = :userid and ProfileId = :profileId ORDER BY FeedDate DESC")
                       .SetParameter("userid", UserId)
                       .SetParameter("profileId", profileid)
                       .List<Domain.Socioboard.Domain.FacebookFeed>()
                       .ToList<Domain.Socioboard.Domain.FacebookFeed>();


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

        public List<Domain.Socioboard.Domain.FacebookFeed> getAllFacebookFeeds(Guid UserId, string profileid)
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
                        List<Domain.Socioboard.Domain.FacebookFeed> alst = session.CreateQuery("from FacebookFeed where  UserId = :userid and ProfileId = :profileId ORDER BY FeedDate DESC")
                       .SetParameter("userid", UserId)
                       .SetParameter("profileId", profileid)
                       .List<Domain.Socioboard.Domain.FacebookFeed>()
                       .ToList<Domain.Socioboard.Domain.FacebookFeed>();


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

        // Edited by Antima
        public List<Domain.Socioboard.Domain.FacebookFeed> getAllFacebookFeeds(Guid UserId, string profileid, int count)
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
                        List<Domain.Socioboard.Domain.FacebookFeed> alst = session.CreateQuery("from FacebookFeed where  UserId = :userid and ProfileId = :profileId ORDER BY FeedDate DESC")
                       .SetParameter("userid", UserId)
                       .SetParameter("profileId", profileid)
                       .SetFirstResult(count)
                        .SetMaxResults(10)
                       .List<Domain.Socioboard.Domain.FacebookFeed>()
                       .ToList<Domain.Socioboard.Domain.FacebookFeed>();

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

        public List<Domain.Socioboard.Domain.FacebookFeed> getAllReadFbFeeds(string profileid)
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

                        string str = "from FacebookFeed where ReadStatus = 1 and ProfileId IN(";
                        string[] arrsrt = profileid.Split(',');
                        foreach (string sstr in arrsrt)
                        {
                            str += Convert.ToInt64(sstr) + ",";
                        }
                        str = str.Substring(0, str.Length - 1);
                        str += ") ORDER BY FeedDate DESC";
                        // List<FacebookFeed> alst = session.CreateQuery("from FacebookFeed where ReadStatus = 1 and UserId = :userid and ProfileId = :profileId ORDER BY FeedDate DESC")
                        List<Domain.Socioboard.Domain.FacebookFeed> alst = session.CreateQuery(str)
                            // .SetParameter("userid", UserId)
                            //.SetParameter("profileId", profileid)
                       .List<Domain.Socioboard.Domain.FacebookFeed>()
                       .ToList<Domain.Socioboard.Domain.FacebookFeed>();




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

        // Edited by Antima[20/12/2014]

        public Domain.Socioboard.Domain.FacebookFeed getFacebookFeedByProfileId(string ProfileId, string FeedId)
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
                        Domain.Socioboard.Domain.FacebookFeed msg = session.CreateQuery("from FacebookFeed where ProfileId = :ProfileId and FeedId =:FeedId")
                        .SetParameter("ProfileId", ProfileId)
                        .SetParameter("FeedId", FeedId).UniqueResult<Domain.Socioboard.Domain.FacebookFeed>();

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

        //Added by Sumit Gupta [12-02-15]
        /// <getAllFacebookUserFeeds>
        /// Get All Facebook User Feeds
        /// </summary>
        /// <param name="profileid">Profile id</param>
        /// <returns>List of Facebbok feeds (List<FacebookFeed>)</returns>
        public List<Domain.Socioboard.Domain.FacebookFeed> getAllFacebookFeedsOfSBUserWithRange(string UserId, string keyword, string noOfDataToSkip)
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
                        List<Domain.Socioboard.Domain.FacebookFeed> alst = session.Query<Domain.Socioboard.Domain.FacebookFeed>().Where(x => x.FeedDescription.Contains(keyword) && x.UserId.Equals(Guid.Parse(UserId))).OrderByDescending(x => x.FeedDate).Take(20)//.CreateQuery("from FacebookFeed where  UserId = :UserId and FeedDescription like %' =:keyword '% ORDER BY FeedDate DESC")
                            //.List<Domain.Socioboard.Domain.FacebookFeed>()
                        .ToList<Domain.Socioboard.Domain.FacebookFeed>();
                        //    .SetParameter("UserId", UserId)
                        //.SetParameter("keyword", keyword)
                        //  .SetFirstResult(Convert.ToInt32(noOfDataToSkip))
                        //.SetMaxResults(20)

                        //.List<Domain.Socioboard.Domain.FacebookFeed>()
                        //.ToList<Domain.Socioboard.Domain.FacebookFeed>();

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

        public List<Domain.Socioboard.Domain.FacebookFeed> getAllFacebookFeedsOfSBUserWithProfileIdAndRange(string UserId, string profileId, string keyword, string noOfDataToSkip)
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
                        List<Domain.Socioboard.Domain.FacebookFeed> alst = session.Query<Domain.Socioboard.Domain.FacebookFeed>().Where(x => x.FeedDescription.Contains(keyword) && x.UserId.Equals(Guid.Parse(UserId)) && x.ProfileId.Equals(profileId)).OrderByDescending(x => x.FeedDate).Take(20)//.CreateQuery("from FacebookFeed where  UserId = :UserId and FeedDescription like %' =:keyword '% ORDER BY FeedDate DESC")
                            //.List<Domain.Socioboard.Domain.FacebookFeed>()
                        .ToList<Domain.Socioboard.Domain.FacebookFeed>();


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
        public List<Domain.Socioboard.Domain.FacebookFeed> getAllFacebookFeedsOfSBUserWithRangeAndProfileId(string UserId, string profileId, string noOfDataToSkip, string noOfResultsFromTop)
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
                        List<Domain.Socioboard.Domain.FacebookFeed> alst = session.Query<Domain.Socioboard.Domain.FacebookFeed>().Where(x => x.UserId.Equals(Guid.Parse(UserId)) && x.ProfileId.Equals(profileId)).OrderByDescending(x => x.FeedDate).Skip(Convert.ToInt32(noOfDataToSkip)).Take(Convert.ToInt32(noOfResultsFromTop))//.CreateQuery("from FacebookFeed where  UserId = :UserId and FeedDescription like %' =:keyword '% ORDER BY FeedDate DESC")
                        .ToList<Domain.Socioboard.Domain.FacebookFeed>();

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
        public List<Domain.Socioboard.Domain.FacebookFeed> getAllFacebookFeedsOfSBUserWithRangeByProfileId(string profileid, string noOfDataToSkip, string noOfResultsFromTop)
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
                        List<Domain.Socioboard.Domain.FacebookFeed> alst = session.Query<Domain.Socioboard.Domain.FacebookFeed>().Where(x => x.ProfileId.Equals(profileid)).OrderByDescending(x => x.FeedDate).Skip(Convert.ToInt32(noOfDataToSkip)).Take(Convert.ToInt32(noOfResultsFromTop))//.CreateQuery("from FacebookFeed where  UserId = :UserId and FeedDescription like %' =:keyword '% ORDER BY FeedDate DESC")
                        .ToList<Domain.Socioboard.Domain.FacebookFeed>();

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


        public int GetFeedCountByProfileIdAndUserId(Guid UserId, string profileids)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        string[] arrsrt = profileids.Split(',');
                        var results = session.QueryOver<Domain.Socioboard.Domain.FacebookFeed>().Where(U => U.UserId == UserId).AndRestrictionOn(m => m.ProfileId).IsIn(arrsrt).Select(Projections.RowCount()).FutureValue<int>().Value;
                        return Int16.Parse(results.ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }//End Transaction
            }// End se
        }

        public void AddFacebookPagePost(Domain.Socioboard.Domain.FacebookPagePost _FacebookPagePost)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        bool exist = session.Query<Domain.Socioboard.Domain.FacebookPagePost>()
                               .Any(x => x.PostId == _FacebookPagePost.PostId);
                        if (exist)
                        {
                            int i = session.CreateQuery("Update FacebookPagePost set PageName =:PageName, Message =: Message, Link=:Link, Name=: Name, Description=:Description,Likes=:Likes, Comments=:Comments,Shares=:Shares,Reach=:Reach, Talking=:Talking,EngagedUsers=:EngagedUsers where PostId=:PostId")
                                .SetParameter("PageName", _FacebookPagePost.PageName)
                                .SetParameter("Message", _FacebookPagePost.Message)
                                .SetParameter("Link", _FacebookPagePost.Link)
                                .SetParameter("Name", _FacebookPagePost.Name)
                                .SetParameter("Name", _FacebookPagePost.Name)
                                .SetParameter("Description", _FacebookPagePost.Description)
                                .SetParameter("Likes", _FacebookPagePost.Likes)
                                .SetParameter("Comments", _FacebookPagePost.Comments)
                                .SetParameter("Shares", _FacebookPagePost.Shares)
                                .SetParameter("Reach", _FacebookPagePost.Reach)
                                .SetParameter("Talking", _FacebookPagePost.Talking)
                                .SetParameter("EngagedUsers", _FacebookPagePost.EngagedUsers)
                                .SetParameter("PostId", _FacebookPagePost.PostId)
                                .ExecuteUpdate();
                            transaction.Commit();

                        }
                        else 
                        {
                            session.Save(_FacebookPagePost);
                            transaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }// End se
        }
        public List<Domain.Socioboard.Domain.FacebookPagePost> GetFacebookPagePostByDay(string ProfileId, int days)
        { 
            using(NHibernate.ISession session=SessionFactory.GetNewSession())
            {
                //List<Domain.Socioboard.Domain.FacebookPagePost> lstfacebookpost = session.Query<Domain.Socioboard.Domain.FacebookPagePost>().Where(x => x.PageId == ProfileId && x.CreatedTime < DateTime.Now.AddDays(1).Date && x.CreatedTime > DateTime.Now.AddDays(-days).Date).ToList();
                List<Domain.Socioboard.Domain.FacebookPagePost> lstfacebookpost = session.CreateQuery("from FacebookPagePost where PageId=:PageId")
                    .SetParameter("PageId", ProfileId).List<Domain.Socioboard.Domain.FacebookPagePost>().Where(x=> x.CreatedTime < DateTime.Now.AddDays(1).Date && x.CreatedTime > DateTime.Now.AddDays(-days).Date).ToList();
                return lstfacebookpost;
            }
        }

        public List<Domain.Socioboard.Domain.FacebookFeed> getAllFeedDetailForMongo()
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        // string str = "from FacebookFeed ";
                        // List<Domain.Socioboard.Domain.FacebookFeed> alst = session.CreateQuery(str)
                        //.List<Domain.Socioboard.Domain.FacebookFeed>()
                        //.ToList<Domain.Socioboard.Domain.FacebookFeed>();

                        List<Domain.Socioboard.Domain.FacebookFeed> alst = session.Query<Domain.Socioboard.Domain.FacebookFeed>().ToList<Domain.Socioboard.Domain.FacebookFeed>();
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
    }
}