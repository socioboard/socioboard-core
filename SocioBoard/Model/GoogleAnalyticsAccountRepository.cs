using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using System.Collections;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class GoogleAnalyticsAccountRepository :IGoogleAnalyticsAccountRepository
    {
        public void addGoogleAnalyticsUser(GoogleAnalyticsAccount gaaccount)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(gaaccount);
                    transaction.Commit();
                }
            }
        }

        public int deleteGoogelAnalyticsUser(string gauserid, Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from GoogleAnalyticsAccount where GaAccountId = :gauserid and UserId = :userid")
                                        .SetParameter("gauserid", gauserid)
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


        public void updateGoogelAnalyticsUser(GoogleAnalyticsAccount gaaccount)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.CreateQuery("Update GoogleAnalyticsAccount set GaAccountName =:gausername,GaProfileId=:gaprofileid,GaProfileName=:gaprofilename,AccessToken =:access,RefreshToken=:refreshtoken,EmailId=:emailid,IsActive=:status where GaAccountId = :gauserid and UserId = :userid")
                            .SetParameter("gausername", gaaccount.GaAccountName)
                            .SetParameter("gaprofilename",gaaccount.GaProfileName)
                            .SetParameter("gaprofileid",gaaccount.GaProfileId)
                            .SetParameter("access", gaaccount.AccessToken)
                            .SetParameter("refreshtoken",gaaccount.RefreshToken)
                            //.SetParameter("visits", gaaccount.Visits)
                            //.SetParameter("newvisits", gaaccount.NewVisits)
                            //.SetParameter("avgvisits", gaaccount.AvgVisits)
                            .SetParameter("emailid", gaaccount.EmailId)
                            .SetParameter("gauserid", gaaccount.GaAccountId)
                            .SetParameter("userid", gaaccount.UserId)
                            .SetParameter("status", gaaccount.IsActive)
                            .ExecuteUpdate();
                        transaction.Commit();


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        // return 0;
                    }
                }
            }
        }

        public ArrayList getGoogelAnalyticsAccountsOfUser(Guid UserId)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateSQLQuery("Select distinct GaAccountId,GaAccountName from GoogleAnalyticsAccount where UserId = :userid");
                    query.SetParameter("userid", UserId);
                    ArrayList alstFBAccounts = new ArrayList();
                    
                    foreach (var item in query.List())
                    {
                        alstFBAccounts.Add(item);
                    }
                    return alstFBAccounts;

                }
            }

        }

        public ArrayList getMaxGAStats(string gaprofileid)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateSQLQuery("Select 'year',gaYear,gavisits from GoogleAnalyticsStats where gaYear = (select max(gaYear) from GoogleAnalyticsStats) and GaProfileId = :gaprofileid " +
                                                                     " union Select 'month',gaMonth,gavisits from GoogleAnalyticsStats where gaMonth = (select max(gaMonth) from GoogleAnalyticsStats ) and GaProfileId = :gaprofileid " +
                                                                     " union Select 'day',gaDate,gavisits from GoogleAnalyticsStats where gaDate = (select max(gaDate) from GoogleAnalyticsStats) and GaProfileId = :gaprofileid");
                    query.SetParameter("gaprofileid", gaprofileid);
                    ArrayList alstFBAccounts = new ArrayList();

                    foreach (var item in query.List())
                    {
                        alstFBAccounts.Add(item);
                    }
                    return alstFBAccounts;

                }
            }

        }



        public ArrayList getGoogelAnalyticsProfilesOfUser(string accountId, Guid UserId)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from GoogleAnalyticsAccount where UserId = :userid and GaAccountId=:accountId");
                    query.SetParameter("userid", UserId);
                    query.SetParameter("accountId", accountId);
                    ArrayList alstFBAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstFBAccounts.Add(item);
                    }
                    return alstFBAccounts;

                }
            }

        }

        public ArrayList getAllGoogleAnalyticsAccounts()
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from GoogleAnalyticsAccount");
                    ArrayList alstFBAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstFBAccounts.Add(item);
                    }
                    return alstFBAccounts;

                }
            }

        }


        public GoogleAnalyticsAccount getGoogelAnalyticsAccountDetailsById(string gauserid, Guid userId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from GoogleAnalyticsAccount where GaProfileId = :GaAccountId and UserId=:userId");
                    query.SetParameter("GaAccountId", gauserid);
                    query.SetParameter("userId", userId);
                    GoogleAnalyticsAccount result = (GoogleAnalyticsAccount)query.UniqueResult();
                    return result;
                }
            }
        }

        public GoogleAnalyticsAccount getGoogelAnalyticsAccountHomeDetailsById(string gauserid, Guid userId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from GoogleAnalyticsAccount where GaAccountId = :GaAccountId and UserId=:userId");
                        query.SetParameter("GaAccountId", gauserid);
                        query.SetParameter("userId", userId);
                        GoogleAnalyticsAccount result = query.UniqueResult<GoogleAnalyticsAccount>();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }
            }
        }


        public GoogleAnalyticsAccount getGoogelAnalyticsAccountHomeDetailsById(Guid userId, string gauserid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from GoogleAnalyticsAccount where GaAccountId = :GaAccountId and UserId=:userId");
                        query.SetParameter("GaAccountId", gauserid);
                        query.SetParameter("userId", userId);
                        GoogleAnalyticsAccount result = new GoogleAnalyticsAccount();
                        foreach (GoogleAnalyticsAccount item in query.Enumerable<GoogleAnalyticsAccount>())
                        {
                            result = item;
                            break;
                        }
                        return result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }
            }
        }



        public bool checkGoogelAnalyticsUserExists(string gaUserId, Guid Userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from GoogleAnalyticsAccount where UserId = :userid and GaAccountId = :gaUserId");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("gaUserId", gaUserId);
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

        public bool checkGoogelAnalyticsProfileExists(string gaUserId,string gaProfileId, Guid Userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from GoogleAnalyticsAccount where UserId = :userid and GaAccountId = :gaUserId and GaProfileId=:gaProfileId");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("gaUserId", gaUserId);
                        query.SetParameter("gaProfileId",gaProfileId);
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


        public GoogleAnalyticsAccount getUserDetails(string gaUserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from GoogleAnalyticsAccount where GaAccountId = :gaUserId");

                        query.SetParameter("gaUserId", gaUserId);
                        List<GoogleAnalyticsAccount> lst = new List<GoogleAnalyticsAccount>();

                        foreach (GoogleAnalyticsAccount item in query.Enumerable())
                        {

                            lst.Add(item);
                            break;
                        }
                        GoogleAnalyticsAccount fbacc = lst[0];
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
