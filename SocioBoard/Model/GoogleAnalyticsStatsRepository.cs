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
        public void addGoogleAnalyticsStats(GoogleAnalyticsStats gastats)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(gastats);
                    transaction.Commit();
                }
            }
        }

        public int deleteGoogleAnalyticsStats(string FBuserid, Guid userid)
        {
            throw new NotImplementedException();
        }

        public void updateGoogleAnalyticsStats(GoogleAnalyticsStats fbaccount)
        {
            throw new NotImplementedException();
        }

        public ArrayList getAllGoogleAnalyticsStatsOfUser(Guid UserId,int days)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from GoogleAnalyticsStats where UserId = :userid");
                    query.SetParameter("userid", UserId);
                    ArrayList alstgaStats = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstgaStats.Add(item);
                    }
                    return alstgaStats;
                }
            }

        }

        public ArrayList getGoogleAnalyticsStatsById(string gaAccountId, Guid userId, int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateSQLQuery("Select * from GoogleAnalyticsStats where GaProfileId = :gaAccountId and UserId=:userId and gaCountry Is Not Null");
                    query.SetParameter("gaAccountId", gaAccountId);
                    query.SetParameter("userId", userId);
                    ArrayList alstgaStats = new ArrayList();

                    foreach (var item in query.List())
                    {
                        alstgaStats.Add(item);
                    }

                    return alstgaStats;
                }
            }
        }

        public ArrayList getGoogleAnalyticsStatsYearById(string gaAccountId, Guid userId, int days,string duration)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    string strSql = "Select * from GoogleAnalyticsStats where GaProfileId = :gaAccountId and UserId=:userId ";
                    if (duration == "day")
                        strSql = strSql + " and gaDate Is Not Null";
                    else if (duration == "month")
                        strSql = strSql + " and gaMonth Is Not Null";
                    else if (duration == "year")
                        strSql = strSql + " and gaYear Is Not Null";
                    NHibernate.IQuery query = session.CreateSQLQuery(strSql);
                    query.SetParameter("gaAccountId", gaAccountId);
                    query.SetParameter("userId", userId);
                    ArrayList alstgaStats = new ArrayList();

                    foreach (var item in query.List())
                    {
                        alstgaStats.Add(item);
                    }

                    return alstgaStats;
                }
            }
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

        public bool checkGoogleAnalyticsStatsExists(string gaAccountId, Guid Userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                }
            }
        }

        public bool checkGoogleAnalyticsDateStatsExists(string gaProfileId,string strType,string strValue,Guid Userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                }
            }
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

        public GoogleAnalyticsStats getGoogleAnalyticsStatsDetails(string gaAccountId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                }
            }
        }
    }
}