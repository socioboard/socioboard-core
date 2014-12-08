using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Services
{
    public class LinkedInFeedRepository : ILinkedInFeedRepository
    {

        /// <addLinkedInFeed>
        /// Add a new linkedin feed
        /// </summary>
        /// <param name="lifeed">Set Values in a LinkedInFeed Class Property and Pass the Object of LinkedInFeed Class (SocioBoard.Domain.LinkedInFeed).</param>
        public void addLinkedInFeed(Domain.Socioboard.Domain.LinkedInFeed lifeed)
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



        public bool checkLinkedInUserExists(string ProfileId, Guid Userid)
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
                        List<Domain.Socioboard.Domain.LinkedInFeed> alst = session.CreateQuery("from LinkedInFeed where UserId = :userid and ProfileId = :fbuserid")
                        .SetParameter("userid", Userid)
                        .SetParameter("fbuserid", ProfileId)
                        .List<Domain.Socioboard.Domain.LinkedInFeed>()
                       .ToList<Domain.Socioboard.Domain.LinkedInFeed>();
                        if (alst.Count == 0 || alst == null)
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

        public int deleteLinkedInFeed(Domain.Socioboard.Domain.LinkedInFeed lifeed)
        {
            throw new NotImplementedException();
        }

        public int updateLinkedInFeed(Domain.Socioboard.Domain.LinkedInFeed lifeed)
        {
            throw new NotImplementedException();
        }


        /// <getAllLinkedInFeedsOfUser>
        /// Get the all linkedIn Feeds Of User
        /// </summary>
        /// <param name="UserId">id of user account(Guid)</param>
        /// <param name="profileid">Profile id of linkedin (String)</param>
        /// <returns>Return object of LinkedInFeed Class with value of each member in the form of list.(List<LinkedInFeed>)</returns>
        public List<Domain.Socioboard.Domain.LinkedInFeed> getAllLinkedInFeedsOfUser(Guid UserId, string profileid)
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
                        List<Domain.Socioboard.Domain.LinkedInFeed> alst = session.CreateQuery("from LinkedInFeed where UserId = :userid and ProfileId = :profileId")
                        .SetParameter("userid", UserId)
                        .SetParameter("profileId", profileid)
                        .List<Domain.Socioboard.Domain.LinkedInFeed>()
                        .ToList<Domain.Socioboard.Domain.LinkedInFeed>();

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


        public List<Domain.Socioboard.Domain.LinkedInFeed> getAllLinkedInUserFeeds(string profileid)
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
                        List<Domain.Socioboard.Domain.LinkedInFeed> alst = session.CreateQuery("from LinkedInFeed where  ProfileId = :profileId ORDER BY FeedsDate DESC")
                        .SetParameter("profileId", profileid)
                        .List<Domain.Socioboard.Domain.LinkedInFeed>()
                        .ToList<Domain.Socioboard.Domain.LinkedInFeed>();

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

        // Edited by Antima

        /// <getAllLinkedInFeedsOfUser>
        /// Get the all linkedIn Feeds Of User
        /// </summary>
        /// <param name="UserId">id of user account(Guid)</param>
        /// <param name="profileid">Profile id of linkedin (String)</param>
        /// <returns>Return object of LinkedInFeed Class with value of each member in the form of list.(List<LinkedInFeed>)</returns>
        public List<Domain.Socioboard.Domain.LinkedInFeed> getAllLinkedInFeedsOfUser(Guid UserId, string profileid, int count)
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
                        List<Domain.Socioboard.Domain.LinkedInFeed> alst = session.CreateQuery("from LinkedInFeed where UserId = :userid and ProfileId = :profileId")
                        .SetParameter("userid", UserId)
                        .SetParameter("profileId", profileid)
                        .SetFirstResult(count)
                        .SetMaxResults(10)
                        .List<Domain.Socioboard.Domain.LinkedInFeed>()
                        .ToList<Domain.Socioboard.Domain.LinkedInFeed>();

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
        public List<Domain.Socioboard.Domain.LinkedInFeed> getAllLinkedInFeedsOfProfile(string ProfileId)
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
                        List<Domain.Socioboard.Domain.LinkedInFeed> alst = session.CreateQuery("from LinkedInFeed where ProfileId = :ProfileId ORDER BY EntryDate DESC")
                        .SetParameter("ProfileId", ProfileId)
                        .List<Domain.Socioboard.Domain.LinkedInFeed>()
                        .ToList<Domain.Socioboard.Domain.LinkedInFeed>();

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

        // Edited by Antima

        public List<Domain.Socioboard.Domain.LinkedInFeed> getAllLinkedInFeedsOfProfileWithId(string ProfileId, string Id)
        {
            List<Domain.Socioboard.Domain.LinkedInFeed> objlist = new List<Domain.Socioboard.Domain.LinkedInFeed>();

            Guid id = Guid.Parse(Id);
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all feed of account
                        objlist = session.CreateQuery("from LinkedInFeed where ProfileId = :ProfileId and Id = :Id")
                        .SetParameter("ProfileId", ProfileId)
                        .SetParameter("Id", id)
                        .List<Domain.Socioboard.Domain.LinkedInFeed>()
                        .ToList<Domain.Socioboard.Domain.LinkedInFeed>();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
            return objlist;
        }

    }
}