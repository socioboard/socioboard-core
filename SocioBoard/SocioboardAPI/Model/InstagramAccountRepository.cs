using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
using System.Collections;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Services
{
    public class InstagramAccountRepository : IInstagramAccountRepository
    {
        /// <addInstagramUser>
        /// add a new account of Instagram for user.
        /// </summary>
        /// <param name="insaccount">Set Values in a InstagramAccount Class Property and Pass the same Object of InstagramAccount Class.(Domain.InstagramAccount)</param>
        public void addInstagramUser(Domain.Socioboard.Domain.InstagramAccount insaccount)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    //Proceed action, to Save data.
                    session.Save(insaccount);
                    transaction.Commit();
                }//End Transaction
            }//End session
        }


        /// <deleteInstagramUser>
        /// Delete InstagrameUser from Databse by Inuserid(string) and userid(Guid).
        /// </summary>
        /// <param name="Insuserid">Inuserid InstagramAccount(string)</param>
        /// <param name="userid">userid InstagramAccount(Guid)</param>
        /// <returns>Return integer 1 for true and 0 for false</returns>
        public int deleteInstagramUser(string Insuserid, Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action to delete InstagrameUser from Databse.
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
                }//End Transaction
            }//End session
        }


        /// <updateInstagramUser>
        /// update InstagramUser account.
        /// </summary>
        /// <param name="insaccount">Set Values in a InstagramAccount Class Property and Pass the same Object of InstagramAccount Class.(Domain.InstagramAccount)</param>
        public void updateInstagramUser(Domain.Socioboard.Domain.InstagramAccount insaccount)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    //Proceed action to Update InstagrameAccount
                    // And Set the reuired paremeters to find the specific values.
                    try
                    {
                        session.CreateQuery("Update InstagramAccount set InsUserName =:InsUserName,ProfileUrl=:ProfileUrl,AccessToken =:AccessToken,Followers =:Followers,FollowedBy=:FollowedBy,TotalImages=:TotalImages where InstagramId = :InstagramId and UserId = :userid")
                            .SetParameter("InsUserName", insaccount.InsUserName)
                            .SetParameter("ProfileUrl", insaccount.ProfileUrl)
                            .SetParameter("AccessToken", insaccount.AccessToken)
                            .SetParameter("Followers", insaccount.Followers)
                            .SetParameter("FollowedBy", insaccount.FollowedBy)
                            .SetParameter("TotalImages", insaccount.TotalImages)
                            .SetParameter("InstagramId", insaccount.InstagramId)
                            .SetParameter("userid", insaccount.UserId)
                            .ExecuteUpdate();
                        transaction.Commit();


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        // return 0;
                    }
                }//End Transaction
            }//End session
        }


        /// <getAllInstagramAccountsOfUser>
        /// Get all Instagram accounts of User by userId(Guid).
        /// </summary>
        /// <param name="UserId">UserId InstagramAccount(Guid).</param>
        /// <returns>return all instagram accounts of user in form List type.</returns>
        public ArrayList getAllInstagramAccountsOfUser(Guid UserId)
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    //Proceed action to get all Instagram accounts of User.
                    NHibernate.IQuery query = session.CreateQuery("from InstagramAccount where UserId = :userid");
                    query.SetParameter("userid", UserId);
                    ArrayList alstInsAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstInsAccounts.Add(item);
                    }
                    return alstInsAccounts;

                }//End Transaction
            }//End session

        }


        public List<Domain.Socioboard.Domain.InstagramAccount> GetAllInstagramAccountsOfUser(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, To get linkedin account by user id.
                    NHibernate.IQuery query = session.CreateQuery("from InstagramAccount where UserId = :userid");
                    query.SetParameter("userid", UserId);
                    List<Domain.Socioboard.Domain.InstagramAccount> alstLIAccounts = new List<Domain.Socioboard.Domain.InstagramAccount>();

                    foreach (var item in query.Enumerable())
                    {
                        alstLIAccounts.Add((Domain.Socioboard.Domain.InstagramAccount)item);
                    }
                    return alstLIAccounts;

                }//End Transaction
            }//End Session
        }




        /// <getAllInstagramAccounts>
        ///  Get all Instagram accounts from database
        /// </summary>
        /// <returns> Return all Instagram Account in form Array List</returns>
        public ArrayList getAllInstagramAccounts()
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to get all Instagram accounts from database.
                    NHibernate.IQuery query = session.CreateQuery("from InstagramAccount");
                    ArrayList alstInsAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstInsAccounts.Add(item);
                    }
                    return alstInsAccounts;

                }//End Transaction
            }//End session

        }


        /// <getInstagramAccountDetailsById>
        /// Get Instagram Account Details by Insuserid(string) userId(Guid).
        /// </summary>
        /// <param name="Insuserid">Insuserid InstagramAccount(String)</param>
        /// <param name="userId">Insuserid InstagramAccount(Guid)</param>
        /// <returns>Return a object of FacebookMessage Class with  value of each member.</returns>
        public Domain.Socioboard.Domain.InstagramAccount getInstagramAccountDetailsById(string Insuserid, Guid userId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    //Proceed action to get Instagram Account Details.
                    NHibernate.IQuery query = session.CreateQuery("from InstagramAccount where  InstagramId = :InstagramId and UserId = :UserId")
                     .SetParameter("InstagramId", Insuserid)
                     .SetParameter("UserId", userId);
                    Domain.Socioboard.Domain.InstagramAccount result = (Domain.Socioboard.Domain.InstagramAccount)query.UniqueResult();
                    return result;
                }//End Transaction
            }//End session
        }

        public Domain.Socioboard.Domain.InstagramAccount getInstagramAccountDetailsById(string Insuserid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    //Proceed action to get Instagram Account Details.
                    List<Domain.Socioboard.Domain.InstagramAccount> objlst = session.CreateQuery("from InstagramAccount where  InstagramId = :InstagramId ")
                     .SetParameter("InstagramId", Insuserid)
                    .List<Domain.Socioboard.Domain.InstagramAccount>().ToList<Domain.Socioboard.Domain.InstagramAccount>();
                    Domain.Socioboard.Domain.InstagramAccount result = new Domain.Socioboard.Domain.InstagramAccount();
                    if (objlst.Count > 0)
                    {
                        result = objlst[0];
                    }
                    return result;
                }//End Transaction
            }//End session
        }





        /// <checkInstagramUserExists>
        /// Check if instagram user is Exist or not in Database by InUserId(String) and UserId(Guid).
        /// </summary>
        /// <param name="InsUserId">InUserId InstagramAccount(String)</param>
        /// <param name="Userid">UserId InstagramAccount(Guid)</param>
        /// <returns>Return true or false</returns>
        public bool checkInstagramUserExists(string InsUserId, Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action to Check if instagram user is Exist or not in Database.
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

                }//End Transaction
            }//End session
        }


        /// <getInstagramAccountById>
        /// Get Instagram account from database by InsId(String)
        /// </summary>
        /// <param name="InsId"> InsId InstagramAccount(String)</param>
        /// <returns>Return object of InstagramAccount Class with  value of each member.</returns>
        public Domain.Socioboard.Domain.InstagramAccount getInstagramAccountById(string InsId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action to get Instagram account from database.
                        NHibernate.IQuery query = session.CreateQuery("from InstagramAccount where  InstagramId = :InstagramId");
                        query.SetParameter("InstagramId", InsId);
                        Domain.Socioboard.Domain.InstagramAccount insAccount = new Domain.Socioboard.Domain.InstagramAccount();
                        foreach (Domain.Socioboard.Domain.InstagramAccount item in query.Enumerable<Domain.Socioboard.Domain.InstagramAccount>())
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

                }//End Transaction
            }//End session

        }


        /// <DeleteInstagramAccountByUserid>
        /// Delete Instagram Account from Database by UserId.
        /// </summary>
        /// <param name="userid">Userid InstagramAccount(Guid)</param>
        /// <returns>Return integer 1 for true 0 for false</returns>
        public int DeleteInstagramAccountByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action to delete Instagram Account from database.
                        NHibernate.IQuery query = session.CreateQuery("delete from InstagramAccount where UserId = :userid")
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
            }//End session
        }


    }
}