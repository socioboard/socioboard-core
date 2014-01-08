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
        public void addFacebookInsightPostStats(FacebookInsightPostStats fbinsightstats)
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

        public int deleteFacebookInsightPostStats(string FBuserid, Guid userid)
        {
            throw new NotImplementedException();
        }

        public void updateFacebookInsightPostStats(FacebookInsightPostStats fbaccount)
        {
            throw new NotImplementedException();
        }

        public ArrayList getAllFacebookInsightPostStatsOfUser(Guid UserId)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from FacebookInsightPostStats where UserId = :userid");
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

        public ArrayList getFacebookInsightPostStatsById(string Fbuserid, Guid userId, int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateSQLQuery("Select * from FacebookInsightPostStats where PageId = :Fbuserid and UserId=:userId and PostDate>=date_format(DATE_ADD(NOW(),INTERVAL -" + days + " DAY),'%m/%d/%Y') ORDER BY PostDate");
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

        public bool checkFacebookInsightPostStatsExists(string FbUserId, string postId, Guid Userid, string PostDate)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

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

                }
            }
        }

        public FacebookInsightPostStats getInsightStatsPostDetails(string PostId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
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

                }
            }
        }
    }
}