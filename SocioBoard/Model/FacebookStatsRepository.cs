using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
using System.Collections;
using NHibernate.Criterion;
using NHibernate.Type;

namespace SocioBoard.Model
{
    public class FacebookStatsRepository : IFacebookStatsRepository
    {
        public void addFacebookStats(FacebookStats fbstats)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(fbstats);
                    transaction.Commit();

                }
            }
        }

        public int deleteFacebookStats(string FBuserid, Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from FacebookStats where FbUserId = :twtuserid and UserId = :userid")
                                        .SetParameter("twtuserid", FBuserid)
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
                }
            }
        }

        public void updateFacebookStats(FacebookStats fbaccount)
        {
            throw new NotImplementedException();
        }

        public ArrayList getAllFacebookStatsOfUser(Guid UserId,int days)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateSQLQuery("Select * from FacebookStats where UserId = :userid and EntryDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY)")
                   .SetParameter("userid", UserId);
                    ArrayList alstFBStats = new ArrayList();

                    foreach (var item in query.List())
                    {
                        alstFBStats.Add(item);
                    }
                    return alstFBStats;

                }
            }

        }

        public FacebookStats getFacebookStatsById(string Fbuserid, Guid userId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from FacebookStats where FbUserId = :Fbuserid and UserId=:userId");
                    query.SetParameter("Fbuserid", Fbuserid);
                    query.SetParameter("userId", userId);
                    FacebookStats result = (FacebookStats)query.UniqueResult();
                    return result;
                }
            }
        }

        public bool checkFacebookStatsExists(string FbUserId, Guid Userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from FacebookStats where UserId = :userid and FbUserId = :fbuserid Date_format(EntryDate,'%yy-%m-%d') LIKE Date_format(Now(),'%yy-%m-%d')");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("fbuserid", FbUserId);
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

        public List<int> GetShareByUserId(Guid Userid)
        {
            List<int> lstShareCount = new List<int>();
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int year = DateTime.Now.Year;

                        for (int i = 1; i < 13; i++)
                        {
                            string month = "0";

                            if (i < 10)
                            {
                                
                                month = month + i.ToString();
                            }
                            else
                            {
                                month = i.ToString();
                            }

                            List<FacebookStats> lstFacebookStats = session.CreateQuery("from FacebookStats where EntryDate Like :yearMonth and UserId= :userId ")
                                .SetParameter("userId", Userid)
                                .SetParameter("yearMonth", "%" + year + "-" + (month) + "%", TypeFactory.GetAnsiStringType(15)).List<FacebookStats>().ToList<FacebookStats>(); //"%" + year + "-" + (month) + "%"

                            lstShareCount.Add(lstFacebookStats.Count);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        
                    }

                    return lstShareCount;

                }
            }
        }


        public int DeleteFacebookStatsByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from FacebookStats where UserId = :userid")
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
                }
            }
        }



    }
}