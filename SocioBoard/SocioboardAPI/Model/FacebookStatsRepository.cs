using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using NHibernate.Criterion;
using NHibernate.Type;
using Domain.Socioboard.Domain;
using Api.Socioboard.Helper;

namespace SocioBoard.Model
{
    public class FacebookStatsRepository //: IFacebookStatsRepository
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

        public ArrayList getTotalFacebookStatsOfUser(Guid UserId)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateSQLQuery("Select * from FacebookStats where UserId = :userid ")
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

        public bool checkFacebookStatsExists(string FbUserId, Guid Userid, int FanCount,int maleCount ,int FemaleCount)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from FacebookStats where UserId = :userid and MaleCount = :malecount and FanCount = :fancount and FemaleCount = :femalecount and FbUserId = :fbuserid and  Date_format(EntryDate,'%yy-%m-%d') LIKE Date_format(Now(),'%yy-%m-%d')");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("fbuserid", FbUserId);
                        query.SetParameter("fancount", FanCount);
                        query.SetParameter("malecount", maleCount);
                        query.SetParameter("femalecount", FemaleCount);
                        List<FacebookStats> result = query.List<FacebookStats>()
                       .ToList<FacebookStats>();;

                        if (result.Count == 0)
                            return true;
                        else
                            return false;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return false;
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
        
        public List<FacebookStats> getAllAccountDetail(string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        string str = "from FacebookStats where  FbUserId IN(";
                        string[] arrsrt = profileid.Split(',');
                        foreach (string sstr in arrsrt)
                        {
                            str += Convert.ToInt64(sstr) + ",";
                        }
                        str = str.Substring(0, str.Length - 1);
                        str += ") group by FbUserId";
                        List<FacebookStats> alst = session.CreateQuery(str)
                       .List<FacebookStats>()
                       .ToList<FacebookStats>();
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
        
        public ArrayList FancountFacebookStats(string FBuserid,int days)
        {
            //List<FacebookStats> lstFacebookStats = new List<FacebookStats>();
            //string fan1=string.Empty;
            //int Fancnt=0;
            var frmdate = DateTime.Now;

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //NHibernate.IQuery query = session.CreateSQLQuery("select * from  Facebookstats where UserId = :userid and FbUserId = :fbuserid ORDER BY EntryDate DESC")
                        //                .SetParameter("fbuserid", FBuserid)
                        //                .SetParameter("userid", userid);

                        /// coded by hozefa......
                        NHibernate.IQuery query = session.CreateSQLQuery("select * from Facebookstats where FbUserId = :fbuserid and EntryDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY)  ORDER BY EntryDate DESC ")
                                        .SetParameter("fbuserid", FBuserid);
                                        

                        ArrayList alstFBfanCnt = new ArrayList();
                        //List<FacebookStats>().ToList<FacebookStats>();
                        foreach (var item in query.List())
                        {
                            alstFBfanCnt.Add(item);
                        }


                        return alstFBfanCnt;
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