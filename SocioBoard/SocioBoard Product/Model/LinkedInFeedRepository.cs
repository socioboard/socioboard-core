using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class LinkedInFeedRepository : ILinkedInFeedRepository
    {

        /// <addLinkedInFeed>
        /// Add a new linkedin feed
        /// </summary>
        /// <param name="lifeed">Set Values in a LinkedInFeed Class Property and Pass the Object of LinkedInFeed Class (SocioBoard.Domain.LinkedInFeed).</param>
        public void addLinkedInFeed(LinkedInFeed lifeed)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(lifeed);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }

        public int deleteLinkedInFeed(LinkedInFeed lifeed)
        {
            throw new NotImplementedException();
        }

        public int updateLinkedInFeed(LinkedInFeed lifeed)
        {
            throw new NotImplementedException();
        }


        /// <getAllLinkedInFeedsOfUser>
        /// Get the all linkedIn Feeds Of User
        /// </summary>
        /// <param name="UserId">id of user account(Guid)</param>
        /// <param name="profileid">Profile id of linkedin (String)</param>
        /// <returns>Return object of LinkedInFeed Class with value of each member in the form of list.(List<LinkedInFeed>)</returns>
        public List<LinkedInFeed> getAllLinkedInFeedsOfUser(Guid UserId, string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get linkedin feeds
                        List<LinkedInFeed> alst = session.CreateQuery("from LinkedInFeed where UserId = :userid and ProfileId = :profileId")
                        .SetParameter("userid", UserId)
                        .SetParameter("profileId", profileid)
                        .List<LinkedInFeed>()
                        .ToList<LinkedInFeed>();

                        #region Oldcode
                        //List<LinkedInFeed> alst = new List<LinkedInFeed>();
                        //foreach (LinkedInFeed item in query.Enumerable<LinkedInFeed>().OrderByDescending(x=>x.FeedsDate))
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
            }//End Session
        }


        /// <checkLinkedInFeedExists>
        /// Check Linkedin feed is exists or not.
        /// </summary>
        /// <param name="feedid">Feed id (String)</param>
        /// <param name="Userid">User id(Guid)</param>
        /// <returns>True or False (bool)</returns>
        public bool checkLinkedInFeedExists(string feedid, Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to check linkedin feed. 
                        NHibernate.IQuery query = session.CreateQuery("from LinkedInFeed where UserId = :userid and FeedId = :msgid");
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
            }//End Session
        }


        /// <deleteAllFeedsOfUser>
        /// Delete All Feeds Of User
        /// </summary>
        /// <param name="Profileid">Profile id of user(string)</param>
        /// <param name="userid">User id(Guid)</param>
        /// <returns>Return integer 1 for true and 0 for false.</returns>
        public int deleteAllFeedsOfUser(string Profileid, Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to Delete linkedin feed
                        NHibernate.IQuery query = session.CreateQuery("delete from LinkedInFeed where UserId = :userid and ProfileId = :profileId");
                        query.SetParameter("userid", userid);
                        query.SetParameter("profileId", Profileid);
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


        /// <getAllLinkedInFeedsOfProfile>
        ///Get All LinkedIn Feeds Of Profile
        /// </summary>
        /// <param name="profileid">Profile id of linkedin account (string)</param>
        /// <returns>Return object of LinkedInFeed Class with value of each member in the form of list.(List<LinkedInFeed>)</returns>
        public List<LinkedInFeed> getAllLinkedInFeedsOfProfile(string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all feed of account
                        List<LinkedInFeed> alst = session.CreateQuery("from LinkedInFeed where ProfileId = :profileId ORDER BY EntryDate DESC")
                        .SetParameter("profileId", profileid)
                        .List<LinkedInFeed>()
                        .ToList<LinkedInFeed>();

                        #region oldcode
                        //List<LinkedInFeed> alst = new List<LinkedInFeed>();
                        //foreach (LinkedInFeed item in query.Enumerable<LinkedInFeed>().OrderByDescending(x => x.FeedsDate))
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
            }//End Session
        }


        /// <DeleteLinkedInFeedByUserid>
        /// Delete LinkedIn Feed By User id
        /// </summary>
        /// <param name="userid">User id(Guid)</param>
        /// <returns>Return integer 1 for true and 0 for false.</returns>
        public int DeleteLinkedInFeedByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete feeds
                        NHibernate.IQuery query = session.CreateQuery("delete from LinkedInFeed where UserId = :userid")
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