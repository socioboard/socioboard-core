using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using Domain.Socioboard.Domain;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Services
{
    public class GooglePlusAccountRepository : IGooglePlusAccountRepository
    {

        /// <addGooglePlusUser>
        /// Add google account of user
        /// </summary>
        /// <param name="gpaccount">Set Values in a GooglePlusAccount Class Property and Pass the same Object of GooglePlusAccount Class.(Domain.GooglePlusAccount)</param>
        public void addGooglePlusUser(Domain.Socioboard.Domain.GooglePlusAccount gpaccount)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to Save new data .
                    session.Save(gpaccount);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }


        /// <deleteGooglePlusUser>
        /// Delete existing Google pluse account
        /// </summary>
        /// <param name="Gpuserid" Type="String">Id of google plus account(String).</param>
        /// <param name="userid" Type="gudi">Is of user(Guid).</param>
        /// <returns type="int">Return 1 when Data is successfully deleted Otherwise retun 0. (int)</returns>
        public int deleteGooglePlusUser(string Gpuserid, Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to Delete account
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
                }//End Transaction
            }//End Session
        }

        
        /// <updateGooglePlusUser>
        /// Update google pluse user account details.
        /// </summary>
        /// <param name="gpaccount">Set Values in a GooglePlusAccount Class Property and Pass the same Object of GooglePlusAccount Class.(Domain.GooglePlusAccount)</param>
        public void updateGooglePlusUser(Domain.Socioboard.Domain.GooglePlusAccount gpaccount)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to update google pluse account details
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
                }//End Transaction 
            }//End Session
        }


        /// <getAllGooglePlusAccounts>
        /// Get the all google plus account details
        /// </summary>
        /// <returns></returns>
        public ArrayList getAllGooglePlusAccounts()
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get all details of google plus account
                    NHibernate.IQuery query = session.CreateQuery("from GooglePlusAccount");
                    ArrayList alstFBAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstFBAccounts.Add(item);
                    }
                    return alstFBAccounts;

                }//End transaction
            }//End Session

        }


        /// <getAllGooglePlusAccountsOfUser>
        /// Get the all google plus account of user
        /// </summary>
        /// <param name="UserId">Id of user(Guid)</param>
        /// <returns>List of array(array).</returns>
        public ArrayList getAllGooglePlusAccountsOfUser(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get account details by user id.
                    NHibernate.IQuery query = session.CreateQuery("from GooglePlusAccount where UserId = :userid");
                    query.SetParameter("userid", UserId);
                    ArrayList alstFBAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstFBAccounts.Add(item);
                    }
                    return alstFBAccounts;

                }// End Transaction
            }//End Session

        }


        /// <getAllGooglePlusAccountsOfUser>
        /// Get the google plus account of user.
        /// </summary>
        /// <param name="UserId">Id of user(Guid)</param>
        /// <returns>List of array(array).</returns>
        public ArrayList getGooglePlusAccountsOfUser(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get account details by user id and where type is account.
                    NHibernate.IQuery query = session.CreateQuery("from GooglePlusAccount where UserId = :userid and Type='account'");
                    query.SetParameter("userid", UserId);
                    ArrayList alstFBAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstFBAccounts.Add(item);
                    }
                    return alstFBAccounts;

                }//End Transaction
            }//End Session
        }

        
        /// <getGooglePlusAccountDetailsById>
        /// Get the details of Google account By Id.
        /// </summary>
        /// <param name="gpuserid">Google Plus user id(string).</param>
        /// <param name="userId">User id(Guid)</param>
        /// <returns>Return the object of group where we get all values from object.(Domain.GooglePlusAccount)</returns>
        public Domain.Socioboard.Domain.GooglePlusAccount getGooglePlusAccountDetailsById(string gpuserid, Guid userId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get Details of account by user id and Google plus id.
                    NHibernate.IQuery query = session.CreateQuery("from GooglePlusAccount where GpUserId = :gpuserid and UserId=:userId");
                    query.SetParameter("gpuserid", gpuserid);
                    query.SetParameter("userId", userId);
                    ArrayList alstFBAccounts = new ArrayList();
                    foreach (var item in query.Enumerable())
                    {
                        alstFBAccounts.Add(item);
                    }
                    Domain.Socioboard.Domain.GooglePlusAccount result = (Domain.Socioboard.Domain.GooglePlusAccount)query.UniqueResult();
                    return result;
                }//End Transaction
            }//End Session
        }

        
        /// <checkGooglePlusUserExists>
        /// Check the account is exist or not.
        /// </summary>
        /// <param name="GpUserId">Id of Google plus account.(String)</param>
        /// <param name="Userid">Id of user(Guid)</param>
        /// <returns>Bool (True or False)</returns>
        public bool checkGooglePlusUserExists(string GpUserId, Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to find details of account. 
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

                }//End Transaction
            }//End Session
        }

        
        /// <getUserDetails>
        /// Get the details of account by Google plus Id.
        /// </summary>
        /// <param name="GpUserId">Id of goole plus account(String)</param>
        /// <param name="gpaccount">Set Values in a GooglePlusAccount Class Property and Pass the same Object of GooglePlusAccount Class.(Domain.GooglePlusAccount)</param>
        public Domain.Socioboard.Domain.GooglePlusAccount getUserDetails(string GpUserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get Account detail by google id.
                        NHibernate.IQuery query = session.CreateQuery("from GooglePlusAccount where GpUserId = :gpuserid");

                        query.SetParameter("gpuserid", GpUserId);
                        List<Domain.Socioboard.Domain.GooglePlusAccount> lst = new List<Domain.Socioboard.Domain.GooglePlusAccount>();

                        foreach (Domain.Socioboard.Domain.GooglePlusAccount item in query.Enumerable())
                        {

                            lst.Add(item);
                            break;
                        }
                        Domain.Socioboard.Domain.GooglePlusAccount fbacc = lst[0];
                        return fbacc;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }

        
        /// <DeleteGooglePlusAccountByUserid>
        /// Delete GooglePlus Account By User id
        /// </summary>
        /// <param name="userid">Id of user(Guid)</param>
        /// <returns>Return 1 when Data is successfully deleted Otherwise retun 0. (int)</returns>
        public int DeleteGooglePlusAccountByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete Account.
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
                }//End Transaction
            }//End Session
        }


    }
}