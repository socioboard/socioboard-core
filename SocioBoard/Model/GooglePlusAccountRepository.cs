using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using SocioBoard.Helper;
using SocioBoard.Domain;

namespace SocioBoard.Model
{
    public class GooglePlusAccountRepository : IGooglePlusAccountRepository
    {
        public void addGooglePlusUser(GooglePlusAccount gpaccount)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(gpaccount);
                    transaction.Commit();
                }
            }
        }

        public int deleteGooglePlusUser(string Gpuserid, Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from GooglePlusAccount where GpUserId = :gpuserid and UserId = :userid")
                                        .SetParameter("gpuserid", Gpuserid)
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

        public void updateGooglePlusUser(GooglePlusAccount gpaccount)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.CreateQuery("Update GooglePlusAccount set GpUserName =:gpusername,AccessToken =:access,RefreshToken=:refreshtoken,GpProfileImage =:gpprofileimage,RefreshToken=:refreshtoken,EmailId=:emailid where GpUserId = :gpuserid and UserId = :userid")
                            .SetParameter("gpusername", gpaccount.GpUserName)
                            .SetParameter("access", gpaccount.AccessToken)
                            .SetParameter("refreshtoken",gpaccount.RefreshToken)
                            .SetParameter("gpprofileimage", gpaccount.GpProfileImage)
                            .SetParameter("emailid", gpaccount.EmailId)
                            .SetParameter("fbuserid", gpaccount.GpUserId)
                            .SetParameter("userid", gpaccount.UserId)
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

        public ArrayList getAllGooglePlusAccounts()
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from GooglePlusAccount");
                    ArrayList alstFBAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstFBAccounts.Add(item);
                    }
                    return alstFBAccounts;

                }
            }

        }

        public ArrayList getAllGooglePlusAccountsOfUser(Guid UserId)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from GooglePlusAccount where UserId = :userid");
                    query.SetParameter("userid", UserId);
                    ArrayList alstFBAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstFBAccounts.Add(item);
                    }
                    return alstFBAccounts;

                }
            }

        }

        public ArrayList getGooglePlusAccountsOfUser(Guid UserId)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from GooglePlusAccount where UserId = :userid and Type='account'");
                    query.SetParameter("userid", UserId);
                    ArrayList alstFBAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstFBAccounts.Add(item);
                    }
                    return alstFBAccounts;

                }
            }

        }


        public GooglePlusAccount getGooglePlusAccountDetailsById(string gpuserid, Guid userId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from GooglePlusAccount where GpUserId = :gpuserid and UserId=:userId");
                    query.SetParameter("gpuserid", gpuserid);
                    query.SetParameter("userId", userId);
                    ArrayList alstFBAccounts = new ArrayList();
                    foreach (var item in query.Enumerable())
                    {
                        alstFBAccounts.Add(item);
                    }
                    GooglePlusAccount result = (GooglePlusAccount)query.UniqueResult();
                    return result;
                }
            }
        }

        public bool checkGooglePlusUserExists(string GpUserId, Guid Userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from GooglePlusAccount where UserId = :userid and GpUserId = :gpuserid");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("gpuserid", GpUserId);
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

        public GooglePlusAccount getUserDetails(string GpUserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from GooglePlusAccount where GpUserId = :gpuserid");

                        query.SetParameter("gpuserid", GpUserId);
                        List<GooglePlusAccount> lst = new List<GooglePlusAccount>();

                        foreach (GooglePlusAccount item in query.Enumerable())
                        {

                            lst.Add(item);
                            break;
                        }
                        GooglePlusAccount fbacc = lst[0];
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


        public int DeleteGooglePlusAccountByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from GooglePlusAccount where UserId = :userid")
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