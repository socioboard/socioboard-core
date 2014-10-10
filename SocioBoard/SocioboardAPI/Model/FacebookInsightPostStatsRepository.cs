using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using SocioBoard.Helper;
using SocioBoard.Domain; 

namespace SocioBoard.Model
{
    public class FacebookInsightPostStatsRepository : IFacebookInsightPostStats
    {

        /// <addFacebookInsightPostStats>
        /// Add a new facebook insight post stats
        /// </summary>
        /// <param name="fbinsightstats">Set Values in a FacebookInsightPostStats Class Property and Pass the same Object of FacebookInsightPostStats Class as a parameter (SocioBoard.Domain.FacebookInsightPostStats).</param>
        public void addFacebookInsightPostStats(FacebookInsightPostStats fbinsightstats)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save a new data.
                    session.Save(fbinsightstats);
                    transaction.Commit();
                }//End Trsaction
            }//End session
        }

        public int deleteFacebookInsightPostStats(string FBuserid, Guid userid)
        {
            throw new NotImplementedException();
        }

        public void updateFacebookInsightPostStats(FacebookInsightPostStats fbaccount)
        {
            throw new NotImplementedException();
        }


        /// <getAllFacebookInsightPostStatsOfUser>
        /// Get All Facebook Insight Post Stats Of a User.
        /// </summary>
        /// <param name="UserId">User id(Guid)</param>
        /// <returns>Array list of Facebook Insight Post Stats </returns>
        public ArrayList getAllFacebookInsightPostStatsOfUser(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    //Proceed action, to get all facebook insight posts by user id.
                    NHibernate.IQuery query = session.CreateQuery("from FacebookInsightPostStats where UserId = :userid");
                    query.SetParameter("userid", UserId);
                    ArrayList alstFBInsightStats = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstFBInsightStats.Add(item);
                    }
                    return alstFBInsightStats;

                }//End transaction
            }//End session

        }


        /// <getFacebookInsightPostStatsById>
        /// Get Facebook Insight Post Stats By Id
        /// </summary>
        /// <param name="Fbuserid">Face book user id(String)</param>
        /// <param name="userId">User id (Guid)</param>
        /// <param name="days">Number of day's (int)</param>
        /// <returns>List of Array</returns>
        public ArrayList getFacebookInsightPostStatsById(string Fbuserid, Guid userId, int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get all facebook insight posts.
                    NHibernate.IQuery query = session.CreateSQLQuery("Select * from FacebookInsightPostStats where PageId = :Fbuserid and UserId=:userId and PostDate>=date_format(DATE_ADD(NOW(),INTERVAL -" + days + " DAY),'%m/%d/%Y') ORDER BY PostDate");
                    query.SetParameter("Fbuserid", Fbuserid);
                    query.SetParameter("userId", userId);
                    ArrayList alstFBInsightStats = new ArrayList();

                    foreach (var item in query.List())
                    {
                        alstFBInsightStats.Add(item);
                    }

                    return alstFBInsightStats;
                }//End Transaction
            }//End Session
        }


        /// <checkFacebookInsightPostStatsExists>
        /// Check existing Facebook Insight Post Stats
        /// </summary>
        /// <param name="FbUserId">facebook user id(String)</param>
        /// <param name="postId">post id(String)</param>
        /// <param name="Userid"> User id(Guid) </param>
        /// <param name="PostDate">Post date (string)</param>
        /// <returns>bool (True or False)</returns>
        public bool checkFacebookInsightPostStatsExists(string FbUserId, string postId, Guid Userid, string PostDate)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to find Post of Facebook insight
                        NHibernate.IQuery query = session.CreateQuery("from FacebookInsightPostStats where PageId = :PageId and PostDate=:PostDate and PostId=:postid");
                        query.SetParameter("PageId", FbUserId);
                        query.SetParameter("PostDate", PostDate);
                        query.SetParameter("postid", postId);
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


        /// <getInsightStatsPostDetails>
        /// Get Details of Insight Stats Post.
        /// </summary>
        /// <param name="PostId">Post id(String)</param>
        /// <returns>FacebookInsightPostStats class object (Domain.FacebookInsightPostStats) </returns>
        public FacebookInsightPostStats getInsightStatsPostDetails(string PostId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from FacebookInsightPostStats where PostId = :postId");
                        query.SetParameter("postId", PostId);
                        List<FacebookInsightPostStats> lst = new List<FacebookInsightPostStats>();
                        foreach (FacebookInsightPostStats item in query.Enumerable())
                        {
                            lst.Add(item);
                            break;
                        }
                        FacebookInsightPostStats fbacc = lst[0];
                        return fbacc;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }


        /// <DeleteFacebookInsightPostStatsByUserid>
        /// Delete Facebook Insight Post Stats By Userid.
        /// </summary>
        /// <param name="userid">User id (Guid)</param>
        /// <returns>0 for failed and 1 for success.</returns>
        public int DeleteFacebookInsightPostStatsByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete insight
                        NHibernate.IQuery query = session.CreateQuery("delete from FacebookInsightPostStats where UserId = :userid")
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
                }//End trasaction    
            }//End session
        }

    }
}