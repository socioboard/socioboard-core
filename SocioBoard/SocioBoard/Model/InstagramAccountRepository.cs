using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
using System.Collections;

namespace SocioBoard.Model
{
    public class InstagramAccountRepository : IInstagramAccountRepository
    {
        public void addInstagramUser(InstagramAccount insaccount)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(insaccount);
                    transaction.Commit();
                }
            }
        }

        public int deleteInstagramUser(string Insuserid, Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from InstagramAccount where InstagramId = :InstagramId and UserId = :UserId")
                                        .SetParameter("InstagramId", Insuserid)
                                        .SetParameter("UserId", userid);
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

        public void updateInstagramUser(InstagramAccount insaccount)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.CreateQuery("Update InstagramAccount set InsUserName =:InsUserName,ProfileUrl=:ProfileUrl,AccessToken =:AccessToken,Followers =:Followers,FollowedBy=:FollowedBy,TotalImages=:TotalImages where InstagramId = :InstagramId and UserId = :userid")
                            .SetParameter("InsUserName", insaccount.InsUserName)
                            .SetParameter("ProfileUrl",insaccount.ProfileUrl)
                            .SetParameter("AccessToken", insaccount.AccessToken)
                            .SetParameter("Followers", insaccount.Followers)
                            .SetParameter("FollowedBy", insaccount.FollowedBy)
                            .SetParameter("TotalImages", insaccount.TotalImages)
                            .SetParameter("InstagramId",insaccount.InstagramId)
                            .SetParameter("userid", insaccount.UserId)
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

        public ArrayList getAllInstagramAccountsOfUser(Guid UserId)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from InstagramAccount where UserId = :userid");
                    query.SetParameter("userid", UserId);
                    ArrayList alstInsAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstInsAccounts.Add(item);
                    }
                    return alstInsAccounts;

                }
            }

        }

        public ArrayList getAllInstagramAccounts()
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from InstagramAccount");
                    ArrayList alstInsAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstInsAccounts.Add(item);
                    }
                    return alstInsAccounts;

                }
            }

        }

        public InstagramAccount getInstagramAccountDetailsById(string Insuserid, Guid userId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from InstagramAccount where  InstagramId = :InstagramId and UserId = :UserId")
                     .SetParameter("InstagramId", Insuserid)
                     .SetParameter("UserId", userId);
                    InstagramAccount result = (InstagramAccount)query.UniqueResult();
                    return result;
                }
            }
        }

        public bool checkInstagramUserExists(string InsUserId, Guid Userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from InstagramAccount where  InstagramId = :InstagramId and UserId = :UserId");
                        query.SetParameter("InstagramId", InsUserId);
                        query.SetParameter("UserId", Userid);
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

        public InstagramAccount getInstagramAccountById(string InsId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from InstagramAccount where  InstagramId = :InstagramId");
                        query.SetParameter("InstagramId", InsId);
                        InstagramAccount insAccount = new InstagramAccount();
                        foreach (InstagramAccount item in query.Enumerable<InstagramAccount>())
                        {
                            insAccount = item;
                            break;
                        }
                        return insAccount;

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