using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using NHibernate.Mapping;
using SocioBoard.Helper;
using System.Collections;
using NHibernate.Criterion;


namespace SocioBoard.Model
{
    public class TwitterAccountRepository:ITwitterAccountRepository
    {
        public void addTwitterkUser(TwitterAccount TwtAccount)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(TwtAccount);
                    transaction.Commit();
                }
            }
        }

       


        public int deleteTwitterUser(Guid userid,string twtuserid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from TwitterAccount where TwitterUserId = :twtuserid and UserId = :userid")
                                        .SetParameter("twtuserid", twtuserid)
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

        public int updateTwitterUser(TwitterAccount TwtAccount)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = session.CreateQuery("Update TwitterAccount set TwitterScreenName =:twtScreenName,OAuthToken=:twtToken,OAuthSecret=:tokenSecret,FollowersCount=:followerscount,FollowingCount=:followingcount,ProfileImageUrl=:imageurl where TwitterUserId = :id and UserId =:userid")
                                   .SetParameter("twtScreenName", TwtAccount.TwitterScreenName)
                                   .SetParameter("twtToken", TwtAccount.OAuthToken)
                                   .SetParameter("tokenSecret", TwtAccount.OAuthSecret)
                                   .SetParameter("followerscount", TwtAccount.FollowersCount)
                                   .SetParameter("followingcount",TwtAccount.FollowingCount)
                                   .SetParameter("imageurl",TwtAccount.ProfileImageUrl)
                                   .SetParameter("id",TwtAccount.TwitterUserId)
                                   .SetParameter("userid",TwtAccount.UserId)
                                   .ExecuteUpdate();
                        transaction.Commit();
                        return i;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }
            }
        }

        public System.Collections.ArrayList getAllTwitterAccountsOfUser(Guid UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from TwitterAccount where UserId = :userid");
                    query.SetParameter("userid", UserId);
                    ArrayList alstTwtAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstTwtAccounts.Add(item);
                    }
                    return alstTwtAccounts;
                }
            }
        }

        public System.Collections.ArrayList getAllTwitterAccounts()
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from TwitterAccount");
                    ArrayList alstTwtAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstTwtAccounts.Add(item);
                    }
                    return alstTwtAccounts;

                }
            }
        }

        public bool checkTwitterUserExists(string TwtUserId, Guid Userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from TwitterAccount where UserId = :userid and TwitterUserId = :Twtuserid");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("Twtuserid", TwtUserId);
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
        public TwitterAccount getUserInformation(Guid userid, string twtuserid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from TwitterAccount where UserId = :userid and TwitterUserId = :Twtuserid");
                        query.SetParameter("userid", userid);
                        query.SetParameter("Twtuserid", twtuserid);
                        TwitterAccount result = query.UniqueResult<TwitterAccount>();
                        return result;

                        //TwitterAccount result= session.QueryOver<TwitterAccount>().Where(x => x.UserId == userid).And(x=>x.TwitterUserId == twtuserid).SingleOrDefault<TwitterAccount>();
                        //return result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }

        public TwitterAccount getUserInformation(string screenname,Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from TwitterAccount where UserId = :userid and TwitterScreenName = :Twtuserid");
                        query.SetParameter("userid", userid);
                        query.SetParameter("Twtuserid", screenname);
                        TwitterAccount result = query.UniqueResult<TwitterAccount>();
                        return result;

                        //TwitterAccount result= session.QueryOver<TwitterAccount>().Where(x => x.UserId == userid).And(x=>x.TwitterUserId == twtuserid).SingleOrDefault<TwitterAccount>();
                        //return result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }



        public TwitterAccount getUserInfo(string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        try
                        {
                            NHibernate.IQuery query = session.CreateQuery("from TwitterAccount where TwitterUserId = :Twtuserid");
                            query.SetParameter("Twtuserid", profileid);
                            TwitterAccount twtAccount = new TwitterAccount();
                            foreach (TwitterAccount item in query.Enumerable<TwitterAccount>())
                            {
                                twtAccount = item;
                                break;
                            }

                            return twtAccount;

                            //TwitterAccount result= session.QueryOver<TwitterAccount>().Where(x => x.UserId == userid).And(x=>x.TwitterUserId == twtuserid).SingleOrDefault<TwitterAccount>();
                            //return result;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            return null;
                        }

                         
                    }

                    catch {
                        return null;
                    }
                }
            }
        }



        public int DeleteTwitterAccountByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from TwitterAccount where UserId = :userid")
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