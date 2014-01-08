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
        public void addFacebookInsightStats(FacebookInsightStats fbinsightstats)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(fbinsightstats);
                    transaction.Commit();
                }
            }
        }

        public int deleteFacebookInsightStats(string FBuserid, Guid userid)
        {
            throw new NotImplementedException();
        }

        public void updateFacebookInsightStats(FacebookInsightStats fbaccount)
        {
            throw new NotImplementedException();
        }

        public ArrayList getAllFacebookInsightStatsOfUser(Guid UserId)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from FacebookInsightStats where UserId = :userid");
                    query.SetParameter("userid", UserId);
                    ArrayList alstFBInsightStats = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstFBInsightStats.Add(item);
                    }
                    return alstFBInsightStats;
                }
            }

        }

        public ArrayList getFacebookInsightStatsById(string Fbuserid, Guid userId,int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateSQLQuery("Select * from FacebookInsightStats where FbUserId = :Fbuserid and UserId=:userId and CountDate<=DATE_ADD(NOW(),INTERVAL -" + days + " DAY) Group BY Week(CountDate)");
                    query.SetParameter("Fbuserid", Fbuserid);
                    query.SetParameter("userId", userId);
                     ArrayList alstFBInsightStats = new ArrayList();

                     foreach (var item in query.List())
                     {
                         alstFBInsightStats.Add(item);
                     }
                   
                    return alstFBInsightStats;
                }
            }
        }

        public ArrayList getFacebookInsightStatsLocationById(string Fbuserid, Guid userId, int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateSQLQuery("Select * from FacebookInsightStats where FbUserId = :Fbuserid and UserId=:userId and Location is not null and CountDate<=DATE_ADD(NOW(),INTERVAL -" + days + " DAY) Group BY Location,Week(CountDate)");
                    query.SetParameter("Fbuserid", Fbuserid);
                    query.SetParameter("userId", userId);
                    ArrayList alstFBInsightStats = new ArrayList();

                    foreach (var item in query.List())
                    {
                        alstFBInsightStats.Add(item);
                    }

                    return alstFBInsightStats;
                }
            }
        }

        public ArrayList getFacebookInsightStatsAgeWiseById(string Fbuserid, Guid userId, int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateSQLQuery("Select * from FacebookInsightStats where FbUserId = :Fbuserid and UserId=:userId and AgeDiff is not null and CountDate<=DATE_SUB(NOW(),INTERVAL " + days + " DAY) Group BY Week(CountDate)");
                    query.SetParameter("Fbuserid", Fbuserid);
                    query.SetParameter("userId", userId);
                    ArrayList alstFBInsightStats = new ArrayList();

                    foreach (var item in query.List())
                    {
                        alstFBInsightStats.Add(item);
                    }

                    return alstFBInsightStats;
                }
            }
        }

        public bool checkFacebookInsightStatsExists(string FbUserId, Guid Userid, string countdate, string agediff)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                }
            }
        }

        public bool checkFbIPageImprStatsExists(string FbUserId, Guid Userid, string countdate)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                }
            }
        }

        public bool checkFbILocationStatsExists(string FbUserId, Guid Userid, string countdate, string location)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                }
            }
        }  

        public FacebookInsightStats getInsightStatsDetails(string FbUserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                }
            }
        }
    }
}