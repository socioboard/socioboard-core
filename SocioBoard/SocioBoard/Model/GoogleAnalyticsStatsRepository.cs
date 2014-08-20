using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
using System.Collections;

namespace SocioBoard.Model
{
    public class GoogleAnalyticsStatsRepository : IGoogleAnalyticsStatsRepository
    {

        /// <addGoogleAnalyticsStats>
        /// Add a new Google Analytics Stats
        /// </summary>
        /// <param name="gastats">Set Values in a GoogleAnalyticsStats Class Property and Pass the same Object of GoogleAnalyticsStats Class.(Domain.GoogleAnalyticsStats)</param>
        public void addGoogleAnalyticsStats(GoogleAnalyticsStats gastats)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data.
                    session.Save(gastats);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }


        public int deleteGoogleAnalyticsStats(string FBuserid, Guid userid)
        {
            throw new NotImplementedException();
        }

        public void updateGoogleAnalyticsStats(GoogleAnalyticsStats fbaccount)
        {
            throw new NotImplementedException();
        }


        /// <getAllGoogleAnalyticsStatsOfUser>
        /// Get All Google Analytics Stats Of User
        /// </summary>
        /// <param name="UserId">Id of user(Guid)</param>
        /// <param name="days">Number of days.(int)</param>
        /// <returns>List of array</returns>
        public ArrayList getAllGoogleAnalyticsStatsOfUser(Guid UserId,int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get all data by user id.
                    NHibernate.IQuery query = session.CreateQuery("from GoogleAnalyticsStats where UserId = :userid");
                    query.SetParameter("userid", UserId);
                    ArrayList alstgaStats = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstgaStats.Add(item);
                    }
                    return alstgaStats;
                }//End Transaction
            }//End Session
        }


        /// <getGoogleAnalyticsStatsById>
        /// Get the Google Analytics Stats details By Id
        /// </summary>
        /// <param name="gaAccountId">Id of Google Account(string)</param>
        /// <param name="userId">Id of user(Guid)</param>
        /// <param name="days">Number of days.(int)</param>
        /// <returns>List of array</returns>
        public ArrayList getGoogleAnalyticsStatsById(string gaAccountId, Guid userId, int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get all details.
                    NHibernate.IQuery query = session.CreateSQLQuery("Select * from GoogleAnalyticsStats where GaProfileId = :gaAccountId and UserId=:userId and gaCountry Is Not Null");
                    query.SetParameter("gaAccountId", gaAccountId);
                    query.SetParameter("userId", userId);
                    ArrayList alstgaStats = new ArrayList();

                    foreach (var item in query.List())
                    {
                        alstgaStats.Add(item);
                    }

                    return alstgaStats;
                }//End Transaction
            }//End Session
        }


        /// <getGoogleAnalyticsStatsYearById>
        /// Get Google Analytics Stats Year By Id
        /// </summary>
        /// <param name="gaAccountId">Google account id(String).</param>
        /// <param name="userId">User id (Guid).</param>
        /// <param name="days">Number of Days for getting records(int).</param>
        /// <param name="duration">Duration type(day or month or year)</param>
        /// <returns>List of array.</returns>
        public ArrayList getGoogleAnalyticsStatsYearById(string gaAccountId, Guid userId, int days,string duration)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Make query and filteration for getting specific records.
                    string strSql = "Select * from GoogleAnalyticsStats where GaProfileId = :gaAccountId and UserId=:userId ";
                    if (duration == "day")
                        strSql = strSql + " and gaDate Is Not Null";
                    else if (duration == "month")
                        strSql = strSql + " and gaMonth Is Not Null";
                    else if (duration == "year")
                        strSql = strSql + " and gaYear Is Not Null";

                    //Proceed action, to get recods.
                    NHibernate.IQuery query = session.CreateSQLQuery(strSql);
                    query.SetParameter("gaAccountId", gaAccountId);
                    query.SetParameter("userId", userId);
                    ArrayList alstgaStats = new ArrayList();

                    foreach (var item in query.List())
                    {
                        alstgaStats.Add(item);
                    }

                    return alstgaStats;
                }// End Transaction
            }//End Session
        }

        //public ArrayList getFacebookInsightStatsAgeWiseById(string Fbuserid, Guid userId, int days)
        //{
        //    using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //    {
        //        using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //        {
        //            NHibernate.IQuery query = session.CreateSQLQuery("Select * from FacebookInsightStats where FbUserId = :Fbuserid and UserId=:userId and AgeDiff is not null and CountDate<=DATE_ADD(NOW(),INTERVAL -" + days + " DAY) Group BY CountDate");
        //            query.SetParameter("Fbuserid", Fbuserid);
        //            query.SetParameter("userId", userId);
        //            ArrayList alstFBInsightStats = new ArrayList();

        //            foreach (var item in query.List())
        //            {
        //                alstFBInsightStats.Add(item);
        //            }

        //            return alstFBInsightStats;
        //        }
        //    }
        //}



        /// <checkGoogleAnalyticsStatsExists>
        /// Check the user is exist in Google Analytics Stats by user id and google analytics id.
        /// </summary>
        /// <param name="gaAccountId">Google analitics account id (String)</param>
        /// <param name="Userid">Id of user(Guid)</param>
        /// <returns>Bool (True or False)</returns>
        public bool checkGoogleAnalyticsStatsExists(string gaAccountId, Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to get details of google analytics stats.
                        NHibernate.IQuery query = session.CreateQuery("from GoogleAnalyticsStats where UserId = :userid and GaAccountId = :gaAccountId");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("gaAccountId", gaAccountId);
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

                }//End transaction
            }//End session
        }



        /// <checkGoogleAnalyticsDateStatsExists>
        /// Check the existing google analytics stats by date for user and google analytics profile id. 
        /// </summary>
        /// <param name="gaProfileId">Profile id of google analytics(String)</param>
        /// <param name="strType">Type of records duration (day or month or year)</param>
        /// <param name="strValue">From Date(String)</param>
        /// <param name="Userid">Id of User(GUID)</param>
        /// <returns>Bool(True or False)</returns>
        public bool checkGoogleAnalyticsDateStatsExists(string gaProfileId,string strType,string strValue,Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get data.
                        string strSql = "from GoogleAnalyticsStats where UserId = :userid and GaProfileId = :gaProfileId";
                        if (strType == "day")
                            strSql = strSql + " and gaDate=:strDate";
                        else if (strType == "month")
                            strSql = strSql + " and gaMonth=:strDate";
                        else if (strType == "year")
                            strSql = strSql + " and gaYear=:strDate";
                        else if (strType == "country")
                            strSql = strSql + " and gaCountry=:strDate";
                        NHibernate.IQuery query = session.CreateQuery(strSql);
                        query.SetParameter("userid", Userid);
                        query.SetParameter("gaProfileId", gaProfileId);
                        query.SetParameter("strDate", strValue);
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



        //public bool checkFbIPageImprStatsExists(string FbUserId, Guid Userid, string countdate)
        //{
        //    using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //    {
        //        using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //        {
        //            try
        //            {
        //                NHibernate.IQuery query = session.CreateQuery("from FacebookInsightStats where UserId = :userid and FbUserId = :fbuserid and CountDate=:countdate");
        //                query.SetParameter("userid", Userid);
        //                query.SetParameter("fbuserid", FbUserId);
        //                query.SetParameter("countdate", countdate);
        //                var result = query.UniqueResult();

        //                if (result == null)
        //                    return false;
        //                else
        //                    return true;

        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //                return true;
        //            }

        //        }
        //    }
        //}

        //public bool checkFbILocationStatsExists(string FbUserId, Guid Userid, string countdate, string location)
        //{
        //    using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //    {
        //        using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //        {
        //            try
        //            {
        //                NHibernate.IQuery query = session.CreateQuery("from FacebookInsightStats where UserId = :userid and FbUserId = :fbuserid and CountDate=:countdate and Location=:location");
        //                query.SetParameter("userid", Userid);
        //                query.SetParameter("fbuserid", FbUserId);
        //                query.SetParameter("countdate", countdate);
        //                query.SetParameter("location", location);
        //                var result = query.UniqueResult();

        //                if (result == null)
        //                    return false;
        //                else
        //                    return true;

        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //                return true;
        //            }

        //        }
        //    }
        //}



        /// <getGoogleAnalyticsStatsDetails>
        /// Get google analytics stats detail of account.
        /// </summary>
        /// <param name="gaAccountId">Id of Google analytics account .(string)</param>
        /// <returns>Return object of GoogleAnalyticsStats class.(Domein.GoogleAnalyticsStats)</returns>
        public GoogleAnalyticsStats getGoogleAnalyticsStatsDetails(string gaAccountId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get details of google stats.
                        NHibernate.IQuery query = session.CreateQuery("from GoogleAnalyticsStats where GaAccountId = :gaAccountId");
                        query.SetParameter("gaAccountId", gaAccountId);
                        List<GoogleAnalyticsStats> lst = new List<GoogleAnalyticsStats>();
                        foreach (GoogleAnalyticsStats item in query.Enumerable())
                        {
                            lst.Add(item);
                            break;
                        }
                        GoogleAnalyticsStats fbacc = lst[0];
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



        /// <DeleteGoogleAnalyticsStatsByUserid>
        /// 
        /// </summary>
        /// <param name="userid">User account id (Guid).</param>
        /// <returns>Return 1 for successfully deleted Or 0 for not deleted.(Int)</returns>
        public int DeleteGoogleAnalyticsStatsByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to Delete data by user id.
                        NHibernate.IQuery query = session.CreateQuery("delete from GoogleAnalyticsStats where UserId = :userid")
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