using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class FacebookInsightStatsRepository : IFacebbookInsightStatsRepository
    {
        /// <addFacebookInsightStats>
        /// Add a new FacebbokinsightStats in database.
        /// </summary>
        /// <param name="fbinsightstats">Set Values in a FacebookInsightStats Class Property and Pass the same Object of FacebookInsightStats Class.(Domain.FacebookInsightStats)</param>
        public void addFacebookInsightStats(FacebookInsightStats fbinsightstats)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data.
                    session.Save(fbinsightstats);
                    transaction.Commit();
                }//End Transaction
            }//End session
        }

        public int deleteFacebookInsightStats(string FBuserid, Guid userid)
        {
            throw new NotImplementedException();
        }

        public void updateFacebookInsightStats(FacebookInsightStats fbaccount)
        {
            throw new NotImplementedException();
        }


        /// <getAllFacebookInsightStatsOfUser>
        /// Get all FacebookinsightStats of user from Database by UserId(Guid).
        /// </summary>
        /// <param name="UserId">UserId FacebookInsightStats(Guid).</param>
        /// <returns>Return all Facebookinsight Stats in form of Array List.</returns>
        public ArrayList getAllFacebookInsightStatsOfUser(Guid UserId)
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    //Proceed action, to get all FacebookinsightSatats of user from Database by UserId(Guid).
                    NHibernate.IQuery query = session.CreateQuery("from FacebookInsightStats where UserId = :userid");
                    query.SetParameter("userid", UserId);
                    ArrayList alstFBInsightStats = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstFBInsightStats.Add(item);
                    }
                    return alstFBInsightStats;
                }//End Transaction
            }//End session

        }


        /// <summary>
        /// Get all FacebookinsightStats of user from Database by UserId(Guid) and FbUserId(string).
        /// </summary>
        /// <param name="Fbuserid">FbUserId FacebookInsightStats(String).</param>
        /// <param name="userId">UserId FacebookInsightStats(Guid).</param>
        /// <param name="days">Integer Days.</param>
        /// <returns>Return all Facebookinsight Stats in form of Array List.</returns>
        public ArrayList getFacebookInsightStatsById(string Fbuserid, int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    //Proceed action, to get all FacebookinsightSatats of user from Database by UserId(Guid) and FbUserId(string).

                    NHibernate.IQuery query = session.CreateSQLQuery("Select * from FacebookInsightStats where FbUserId = :Fbuserid and EntryDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY) ORDER BY EntryDate DESC");
                    query.SetParameter("Fbuserid", Fbuserid);
                    
                    ArrayList alstFBInsightStats = new ArrayList();

                    foreach (var item in query.List())
                    {
                        alstFBInsightStats.Add(item);
                    }

                    return alstFBInsightStats;
                }//End Transaction
            }//End session
        }


        /// <getFacebookInsightStatsLocationById>
        /// Get all FacebookinsightStats of user from Database by UserId(Guid) and FbUserId(string) and Location.
        /// </summary>
        /// <param name="Fbuserid">FbUserId FacebookInsightStats(String).</param>
        /// <param name="userId">UserId FacebookInsightStats(Guid).</param>
        /// <param name="days">Integer Days.</param>
        /// <returns>Return all Facebookinsight Stats by Location in form of Array List.</returns>
        public ArrayList getFacebookInsightStatsLocationById(string Fbuserid, int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    //Proceed action to get all FacebookinsightStats of user from Database by UserId(Guid) and FbUserId(string) and Location.
                    NHibernate.IQuery query = session.CreateSQLQuery("Select * from FacebookInsightStats where FbUserId = :Fbuserid and Location is not null and CountDate<=DATE_ADD(NOW(),INTERVAL -" + days + " DAY) Group BY Location,Week(CountDate)");
                    query.SetParameter("Fbuserid", Fbuserid);
                    ArrayList alstFBInsightStats = new ArrayList();

                    foreach (var item in query.List())
                    {
                        alstFBInsightStats.Add(item);
                    }

                    return alstFBInsightStats;
                }//End Transaction
            }//End session
        }


        /// <getFacebookInsightStatsAgeWiseById>
        /// Get all FacebookinsightStats of user from Database by UserId(Guid) and FbUserId(string) and Age wise.
        /// </summary>
        /// <param name="Fbuserid">FbUserId FacebookInsightStats(String).</param>
        /// <param name="userId">UserId FacebookInsightStats(Guid).</param>
        /// <param name="days">Integer Days.</param>
        /// <returns>Return all Facebookinsight Stats by Age wise in form of Array List.</returns>
        public ArrayList getFacebookInsightStatsAgeWiseById(string Fbuserid,int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    //Proceed action to get all FacebookinsightStats of user from Database by UserId(Guid) and FbUserId(string) and Age wise.
                    NHibernate.IQuery query = session.CreateSQLQuery("Select * from FacebookInsightStats where FbUserId = :Fbuserid and AgeDiff is not null and CountDate<=DATE_SUB(NOW(),INTERVAL " + days + " DAY) Group BY Week(CountDate)");
                    query.SetParameter("Fbuserid", Fbuserid);
                    ArrayList alstFBInsightStats = new ArrayList();

                    foreach (var item in query.List())
                    {
                        alstFBInsightStats.Add(item);
                    }

                    return alstFBInsightStats;
                }//End Transaction
            }//End session
        }


        /// <checkFacebookInsightStatsExists>
        ///  Check if InsightFacebookStatsi Exist or Not.
        /// </summary>
        /// <param name="Fbuserid">FbUserId FacebookInsightStats(String).</param>
        /// <param name="userId">UserId FacebookInsightStats(Guid).</param>
        /// <param name="countdate">countdate FacebookInsightStats(String).</param>
        /// <param name="agediff">agediff FacebookInsightStats(String).</param>
        /// <returns>Return True or False</returns>
        public bool checkFacebookInsightStatsExists(string FbUserId, Guid Userid, string countdate, string agediff)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action,to check if InsightFacebookStatsi Exist or Not.
                        NHibernate.IQuery query = session.CreateQuery("from FacebookInsightStats where UserId = :userid and FbUserId = :fbuserid and CountDate=:countdate And AgeDiff =:agediff");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("fbuserid", FbUserId);
                        query.SetParameter("countdate", countdate);
                        query.SetParameter("agediff", agediff);
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
            }//End session
        }


        /// <checkFbIPageImprStatsExists>
        ///  Check the number of total activity of particular User by UserId(Guid), FbUserId(String), CountDate(String).
        /// </summary>
        /// <param name="Fbuserid">FbUserId FacebookInsightStats(String).</param>
        /// <param name="UserId">UserId FacebookInsightStats(Guid).</param>
        /// <param name="countdate">countdate FacebookInsightStats(String).</param>
        /// <returns>Return True or False</returns>
        public bool checkFbIPageImprStatsExists(string FbUserId, Guid Userid, string countdate)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action, to check the number of total activity of particular User.
                        NHibernate.IQuery query = session.CreateQuery("from FacebookInsightStats where UserId = :userid and FbUserId = :fbuserid and CountDate=:countdate");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("fbuserid", FbUserId);
                        query.SetParameter("countdate", countdate);
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
            }//End session
        }


        /// <checkFbILocationStatsExists>
        /// Check if FbInsight Location Stats is Exist or not by UserId(Guid) FbUserId(string) countdate(string) and Location(string).
        /// </summary>
        /// <param name="Fbuserid">FbUserId FacebookInsightStats(String).</param>
        /// <param name="UserId">UserId FacebookInsightStats(Guid).</param>
        /// <param name="countdate">countdate FacebookInsightStats(String).</param>
        /// <param name="location">location FacebookInsightStats(String).</param>
        /// <returns>Return True or False</returns>
        public bool checkFbILocationStatsExists(string FbUserId, Guid Userid, string countdate, string location)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action, to check if FbInsight Location Stats is Exist or not.
                        NHibernate.IQuery query = session.CreateQuery("from FacebookInsightStats where UserId = :userid and FbUserId = :fbuserid and CountDate=:countdate and Location=:location");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("fbuserid", FbUserId);
                        query.SetParameter("countdate", countdate);
                        query.SetParameter("location", location);
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
            }//End session
        }


        /// <FacebookInsightStats>
        /// Get all FacebookInsightStats from database by FbUserId(String).
        /// </summary>
        /// <param name="Fbuserid">FbUserId FacebookInsightStats(String).</param>
        /// <returns>Return a object of FacebookInsightStats Class with  value of each member.</returns>
        public FacebookInsightStats getInsightStatsDetails(string FbUserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action ,to get all FacebookInsightStats from database by FbUserId(String).
                        NHibernate.IQuery query = session.CreateQuery("from FacebookInsightStats where FbUserId = :fbuserid");
                        query.SetParameter("fbuserid", FbUserId);
                        List<FacebookInsightStats> lst = new List<FacebookInsightStats>();
                        foreach (FacebookInsightStats item in query.Enumerable())
                        {
                            lst.Add(item);
                            break;
                        }
                        FacebookInsightStats fbacc = lst[0];
                        return fbacc;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End session
        }


        /// <DeleteFacebookInsightStatsByUserid>
        /// Delete all FacebookInsightStats from database by Userid(Guid).
        /// </summary>
        /// <param name="userid">UserId FacebookInsightStats(Guid).</param>
        /// <returns>Return integer 1 for True 0 for false.</returns>
        public int DeleteFacebookInsightStatsByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action to delete all FacebookInsightStats from database by Userid(Guid).
                        NHibernate.IQuery query = session.CreateQuery("delete from FacebookInsightStats where UserId = :userid")
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
            }//End session
        }



    }
}