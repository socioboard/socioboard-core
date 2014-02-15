using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Helper;
using SocioBoard.Domain;
using System.Collections;

namespace SocioBoard.Model
{
    public class LinkedInAccountRepository : ILinkedInAccountRepository
    {
        public void addLinkedinUser(LinkedInAccount fbaccount)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(fbaccount);
                    transaction.Commit();
                }
            }
        }

        public int deleteLinkedinUser(string FBuserid, Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from LinkedInAccount where LinkedinUserId = :fbuserid and UserId = :userid")
                                        .SetParameter("fbuserid", FBuserid)
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

        public void updateLinkedinUser(LinkedInAccount liaccount)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.CreateQuery("Update LinkedInAccount set LinkedinUserName =:LinkedinUserName,OAuthToken =:OAuthToken,OAuthSecret=:OAuthSecret,OAuthVerifier=:OAuthVerifier,EmailId=:EmailId,Connections=:Connections,ProfileUrl =:profileurl where LinkedinUserId = :LinkedinUserId and UserId = :UserId")
                            .SetParameter("LinkedinUserName", liaccount.LinkedinUserName)
                            .SetParameter("OAuthToken", liaccount.OAuthToken)
                            .SetParameter("OAuthSecret", liaccount.OAuthSecret)
                            .SetParameter("OAuthVerifier",liaccount.OAuthVerifier)
                            .SetParameter("EmailId", liaccount.EmailId)
                            .SetParameter("LinkedinUserId", liaccount.LinkedinUserId)
                            .SetParameter("UserId", liaccount.UserId)
                            .SetParameter("profileurl", liaccount.ProfileUrl)
                            .SetParameter("Connections",liaccount.Connections)
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

        public int UpdateLDAccessTokenByLDUserId(string LDUserId, string accessToken)
        {
            int update = 0;

            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            update = session.CreateQuery("Update LinkedInAccount set OAuthToken = :accessToken where UserId = :LDUserId")
                                .SetParameter("accessToken", accessToken)
                                .SetParameter("LDUserId", LDUserId)
                                .ExecuteUpdate();

                            transaction.Commit();
                            update = 1;

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            // return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }

            return update;
        }

        public ArrayList getAllLinkedinAccountsOfUser(Guid UserId)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from LinkedInAccount where UserId = :userid");
                    query.SetParameter("userid", UserId);
                    ArrayList alstLIAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstLIAccounts.Add(item);
                    }
                    return alstLIAccounts;

                }
            }

        }

        public ArrayList getAllLinkedinAccounts()
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from LinkedInAccount");
                    ArrayList alstLIAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstLIAccounts.Add(item);
                    }
                    return alstLIAccounts;

                }
            }

        }

        public LinkedInAccount getLinkedinAccountDetailsById(string liuserid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from LinkedInAccount where LinkedinUserId = :userid");
                        query.SetParameter("userid", liuserid);
                        LinkedInAccount result = new LinkedInAccount();
                        foreach (LinkedInAccount item in query.Enumerable<LinkedInAccount>())
                        {
                            result = item;
                            break;
                        }
                        return result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return null;
                    }
                }
            }
        }


        public bool checkLinkedinUserExists(string liUserId, Guid Userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from LinkedInAccount where UserId = :userid and LinkedinUserId = :liuserid");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("liuserid", liUserId);
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

        public LinkedInAccount getUserInformation(Guid userid, string liuserid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from LinkedInAccount where LinkedinUserId = :LinkedinUserId And UserId=:UserId");
                        query.SetParameter("UserId", userid);
                        query.SetParameter("LinkedinUserId", liuserid);
                        LinkedInAccount result = query.UniqueResult<LinkedInAccount>();
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



        public int DeleteLinkedInAccountByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from LinkedInAccount where UserId = :userid")
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