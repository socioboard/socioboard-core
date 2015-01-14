using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Mapping;
using System.Collections;
using NHibernate.Criterion;
using Domain.Socioboard.Domain;
using Api.Socioboard.Helper;


namespace Api.Socioboard.Services
{
    public class TwitterAccountRepository : ITwitterAccountRepository
    {

        /// <addTwitterkUser>
        /// Add new Twitter account. 
        /// </summary>
        /// <param name="TwtAccount">Set Values in a TwitterAccount Class Property and Pass the Object of TwitterAccount Class.(Domein.TwitterAccount)</param>
        public void addTwitterkUser(Domain.Socioboard.Domain.TwitterAccount TwtAccount)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data.
                    session.Save(TwtAccount);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }



        /// <deleteTwitterUser>
        /// Delete Twitter User
        /// </summary>
        /// <param name="userid">User id.(Guid)</param>
        /// <param name="twtuserid">Twitter id.(string)</param>
        /// <returns>Return 1 for success and 0 for failure.(int) </returns>
        public int deleteTwitterUser(Guid userid, string twtuserid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete existing account by user id and twitter id.
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
                }//End Transaction
            }//End Session
        }


        /// <updateTwitterUser>
        /// Update Twitter User
        /// </summary>
        /// <param name="TwtAccount">Set Values in a TwitterAccount Class Property and Pass the Object of TwitterAccount Class.(Domein.TwitterAccount)</param>
        /// <returns>Return 1 for success and 0 for failure.(int) </returns>
        public int updateTwitterUser(Domain.Socioboard.Domain.TwitterAccount TwtAccount)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to update Account details.
                        int i = session.CreateQuery("Update TwitterAccount set TwitterScreenName =:twtScreenName,OAuthToken=:twtToken,OAuthSecret=:tokenSecret,FollowersCount=:followerscount,FollowingCount=:followingcount,ProfileImageUrl=:imageurl where TwitterUserId = :id and UserId =:userid")
                                   .SetParameter("twtScreenName", TwtAccount.TwitterScreenName)
                                   .SetParameter("twtToken", TwtAccount.OAuthToken)
                                   .SetParameter("tokenSecret", TwtAccount.OAuthSecret)
                                   .SetParameter("followerscount", TwtAccount.FollowersCount)
                                   .SetParameter("followingcount", TwtAccount.FollowingCount)
                                   .SetParameter("imageurl", TwtAccount.ProfileImageUrl)
                                   .SetParameter("id", TwtAccount.TwitterUserId)
                                   .SetParameter("userid", TwtAccount.UserId)
                                   .ExecuteUpdate();
                        transaction.Commit();
                        return i;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }//End Transaction
            }//End Session
        }


        /// <getAllTwitterAccountsOfUser>
        /// Get All Twitter Accounts Of User
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <returns>Return values in the form of array list.(ArrayList)</returns>
        //public System.Collections.ArrayList getAllTwitterAccountsOfUser(Guid UserId)
        //{
        //    //Creates a database connection and opens up a session
        //    using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //    {
        //        //After Session creation, start Transaction.
        //        using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //        {
        //            //Proceed action, to get twitter account by user id.
        //            NHibernate.IQuery query = session.CreateQuery("from TwitterAccount where UserId = :userid");
        //            query.SetParameter("userid", UserId);
        //            ArrayList alstTwtAccounts = new ArrayList();

        //            foreach (var item in query.Enumerable())
        //            {
        //                alstTwtAccounts.Add(item);
        //            }
        //            return alstTwtAccounts;
        //        }//End Transaction
        //    }//End Session
        //}
        public List<Domain.Socioboard.Domain.TwitterAccount> getAllTwitterAccountsOfUser(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get twitter account by user id.
                    NHibernate.IQuery query = session.CreateQuery("from TwitterAccount where UserId = :userid");
                    query.SetParameter("userid", UserId);
                    //ArrayList alstTwtAccounts = new ArrayList();
                    List<Domain.Socioboard.Domain.TwitterAccount> alstTwtAccounts = new List<Domain.Socioboard.Domain.TwitterAccount>();

                    foreach (var item in query.Enumerable())
                    {
                        alstTwtAccounts.Add((Domain.Socioboard.Domain.TwitterAccount)item);
                    }
                    return alstTwtAccounts;
                }//End Transaction
            }//End Session
        }




        public List<Domain.Socioboard.Domain.TwitterAccount> getAllAccountDetail(string profileid, Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        string str = "from TwitterAccount where UserId =: userid and TwitterUserId IN(";
                        string[] arrsrt = profileid.Split(',');
                        foreach (string sstr in arrsrt)
                        {
                            str += Convert.ToInt64(sstr) + ",";
                        }
                        str = str.Substring(0, str.Length - 1);
                        str += ") group by TwitterUserId";
                        List<Domain.Socioboard.Domain.TwitterAccount> alst = session.CreateQuery(str).SetParameter("userid", userid)
                       .List<Domain.Socioboard.Domain.TwitterAccount>()
                       .ToList<Domain.Socioboard.Domain.TwitterAccount>();
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



        /// <getAllTwitterAccounts>
        /// Get All Twitter Accounts
        /// </summary>
        /// <returns>Return values in the form of array list.(ArrayList)</returns>
        public System.Collections.ArrayList getAllTwitterAccounts()
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get all twitter accounts.
                    NHibernate.IQuery query = session.CreateQuery("from TwitterAccount");
                    ArrayList alstTwtAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstTwtAccounts.Add(item);
                    }
                    return alstTwtAccounts;

                }//End Transaction
            }//End Session
        }



        /// <checkTwitterUserExists>
        /// Check the existing Twitter account by twitter id and user id.
        /// </summary>
        /// <param name="TwtUserId">Twitter id.(string)</param>
        /// <param name="Userid">User id.(Guid)</param>
        /// <returns>True or False.(bool) </returns>
        public bool checkTwitterUserExists(string TwtUserId, Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get twitter account of user by user id.
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

                }//End Transaction
            }//End Session
        }


        /// <getUserInformation>
        /// Get User twitter account Information
        /// </summary>
        /// <param name="userid">User id.(Guid)</param>
        /// <param name="twtuserid">Twitter account id.(string)</param>
        /// <returns>Return the object of TwitterAccount class with value.(Domain.TwitterAccount)</returns>
        public Domain.Socioboard.Domain.TwitterAccount GetUserInformation(Guid userid, string twtuserid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get account details by user id and twitter id.
                        NHibernate.IQuery query = session.CreateQuery("from TwitterAccount where UserId = :userid and TwitterUserId = :Twtuserid");
                        query.SetParameter("userid", userid);
                        query.SetParameter("Twtuserid", twtuserid);
                        Domain.Socioboard.Domain.TwitterAccount result = query.UniqueResult<Domain.Socioboard.Domain.TwitterAccount>();
                        return result;

                        //TwitterAccount result= session.QueryOver<TwitterAccount>().Where(x => x.UserId == userid).And(x=>x.TwitterUserId == twtuserid).SingleOrDefault<TwitterAccount>();
                        //return result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }




        public Domain.Socioboard.Domain.TwitterAccount getUserInformation(string twtuserid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get account details by user id and twitter id.
                        List<Domain.Socioboard.Domain.TwitterAccount> objlst = session.CreateQuery("from TwitterAccount where  TwitterUserId = :Twtuserid")

                         .SetParameter("Twtuserid", twtuserid)
                        .List<Domain.Socioboard.Domain.TwitterAccount>().ToList<Domain.Socioboard.Domain.TwitterAccount>();
                        Domain.Socioboard.Domain.TwitterAccount result = new Domain.Socioboard.Domain.TwitterAccount();
                        if (objlst.Count > 0)
                        {
                            result = objlst[0];
                        }
                        return result;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }




        /// <getUserInformation>
        /// Get User account Information by screen Name and user id.
        /// </summary>
        /// <param name="screenname">Tqitter account screen name.(String)</param>
        /// <param name="userid">User id.(Guid)</param>
        /// <returns>Return the object of TwitterAccount class with value.(Domain.TwitterAccount)</returns>
        public Domain.Socioboard.Domain.TwitterAccount getUserInformation(string screenname, Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get twitter account details by screen name and user id.
                        NHibernate.IQuery query = session.CreateQuery("from TwitterAccount where UserId = :userid and TwitterUserId = :Twtuserid");
                        query.SetParameter("userid", userid);
                        query.SetParameter("Twtuserid", screenname);
                        Domain.Socioboard.Domain.TwitterAccount result = query.UniqueResult<Domain.Socioboard.Domain.TwitterAccount>();
                        return result;

                        //TwitterAccount result= session.QueryOver<TwitterAccount>().Where(x => x.UserId == userid).And(x=>x.TwitterUserId == twtuserid).SingleOrDefault<TwitterAccount>();
                        //return result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }


        /// <getUserInfo>
        /// getUserAccount Information by profile id.
        /// </summary>
        /// <param name="profileid">Account profile id.(String)</param>
        /// <returns>Return the object of TwitterAccount class with value.(Domain.TwitterAccount)</returns>
        public Domain.Socioboard.Domain.TwitterAccount getUserInfo(string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        try
                        {
                            //Proceed action, to get account details by profile id.
                            NHibernate.IQuery query = session.CreateQuery("from TwitterAccount where TwitterUserId = :Twtuserid");
                            query.SetParameter("Twtuserid", profileid);
                            Domain.Socioboard.Domain.TwitterAccount twtAccount = new Domain.Socioboard.Domain.TwitterAccount();
                            foreach (Domain.Socioboard.Domain.TwitterAccount item in query.Enumerable<Domain.Socioboard.Domain.TwitterAccount>())
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

                    catch
                    {
                        return null;
                    }
                }//End Transaction
            }//End Session
        }


        /// <DeleteTwitterAccountByUserid>
        /// Delete Twitter Account By Userid
        /// </summary>
        /// <param name="userid">User id.(Guid)</param>
        /// <returns>Return 1 for success and 0 for failure.(int) </returns>
        public int DeleteTwitterAccountByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete twitter account info by user id.
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
                }//End Transaction
            }//End Session
        }




        ArrayList ITwitterAccountRepository.getAllTwitterAccountsOfUser(Guid UserId)
        {
            throw new NotImplementedException();
        }
    }
}