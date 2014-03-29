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

        /// <addGoogleAnalyticsUser>
        /// Add New Google Analytics User
        /// </summary>
        /// <param name="gaaccount">Set Values in a GoogleAnalyticsAccount Class Property and Pass the same Object of GoogleAnalyticsAccount Class.(Domain.GoogleAnalyticsAccount)</param>
        public void addGoogleAnalyticsUser(GoogleAnalyticsAccount gaaccount)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data.
                    session.Save(gaaccount);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }


        /// <deleteGoogelAnalyticsUser>
        /// Delete googel analytics user account
        /// </summary>
        /// <param name="gauserid">Id of the google account.(String)</param>
        /// <param name="userid">Id of the user.(Guid)</param>
        /// <returns>Return int value when successfullt deleted it's return 1 , otherwise return 0</returns>
        public int deleteGoogelAnalyticsUser(string gauserid, Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete user account.
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
                }//End Transaction
            }//End Session
        }


        /// <updateGoogelAnalyticsUser>
        /// update Googel Analytics User Details
        /// </summary>
        /// <param name="gaaccount">Set Values in a GoogleAnalyticsAccount Class Property and Pass the same Object of GoogleAnalyticsAccount Class.(Domain.GoogleAnalyticsAccount)</param>
        public void updateGoogelAnalyticsUser(GoogleAnalyticsAccount gaaccount)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to update google account deatils.
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
                }//End Transaction
            }//End Session
        }


        /// <getGoogelAnalyticsAccountsOfUser>
        /// Get the details of Googel Analytics Accounts by User id.
        /// </summary>
        /// <param name="UserId">Id of user (Guid)</param>
        /// <returns>Return all google accounts details of related user.(ArrayList) </returns>
        public ArrayList getGoogelAnalyticsAccountsOfUser(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get deatils.
                    NHibernate.IQuery query = session.CreateSQLQuery("Select distinct GaAccountId,GaAccountName from GoogleAnalyticsAccount where UserId = :userid");
                    query.SetParameter("userid", UserId);
                    ArrayList alstFBAccounts = new ArrayList();
                    
                    foreach (var item in query.List())
                    {
                        alstFBAccounts.Add(item);
                    }
                    return alstFBAccounts;

                }//End Transaction
            }//End Session
        }


        /// <getMaxGAStats>
        /// Get the Max Google account stats by year , month and date.
        /// </summary>
        /// <param name="gaprofileid">Id of google account(String)</param>
        /// <returns>List of array.</returns>
        public ArrayList getMaxGAStats(string gaprofileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to details of google account.
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

                }//End Transaction
            }//End Session

        }


        /// <getGoogelAnalyticsProfilesOfUser>
        /// Get Googel Analytics Profiles Details Of User
        /// </summary>
        /// <param name="accountId">Id of user (Guid)</param>
        /// <param name="UserId">Google account Id of user(String)</param>
        /// <returns>Return list of array</returns>
        public ArrayList getGoogelAnalyticsProfilesOfUser(string accountId, Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get all details of user google accounts.
                    NHibernate.IQuery query = session.CreateQuery("from GoogleAnalyticsAccount where UserId = :userid and GaAccountId=:accountId");
                    query.SetParameter("userid", UserId);
                    query.SetParameter("accountId", accountId);
                    ArrayList alstFBAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstFBAccounts.Add(item);
                    }
                    return alstFBAccounts;

                }//End Transaction
            }//End Session
        }


        /// <getAllGoogleAnalyticsAccounts>
        /// Get All Details of Google Analytics Accounts
        /// </summary>
        /// <returns>List of array(Arraylist)</returns>
        public ArrayList getAllGoogleAnalyticsAccounts()
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get all google account details.
                    NHibernate.IQuery query = session.CreateQuery("from GoogleAnalyticsAccount");
                    ArrayList alstFBAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstFBAccounts.Add(item);
                    }
                    return alstFBAccounts;

                }//End transaction
            }//End Session
        }


        /// <getGoogelAnalyticsAccountDetailsById>
        /// Get the details of googel analytics account by Google account id and user id.
        /// </summary>
        /// <param name="gauserid">Id of goole accont(String)</param>
        /// <param name="userId">Id of user(Guid)</param>
        /// <returns>Return object of Google analytic class.(Domein.GoogleAnalyticsAccount)</returns>
        public GoogleAnalyticsAccount getGoogelAnalyticsAccountDetailsById(string gauserid, Guid userId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get deatils of account
                    NHibernate.IQuery query = session.CreateQuery("from GoogleAnalyticsAccount where GaProfileId = :GaAccountId and UserId=:userId");
                    query.SetParameter("GaAccountId", gauserid);
                    query.SetParameter("userId", userId);
                    GoogleAnalyticsAccount result = (GoogleAnalyticsAccount)query.UniqueResult();
                    return result;
                }//End Trnsaction
            }//End Session
        }


        /// <getGoogelAnalyticsAccountHomeDetailsById>
        /// Get Googel Analytics Account Home Details By Id and Google account id.
        /// </summary>
        /// <param name="gauserid">Google account id (String)</param>
        /// <param name="userId">Id of user(Guid)</param>
        /// <returns>Return object of Google analytic class.(Domein.GoogleAnalyticsAccount)</returns>
        public GoogleAnalyticsAccount getGoogelAnalyticsAccountHomeDetailsById(string gauserid, Guid userId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get details of account.
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
                }//End Transaction
            }//End session
        }


        /// <getGoogelAnalyticsAccountHomeDetailsById>
        /// Get the Googel Analytics Account Home Details By Id and google account id.
        /// </summary>
        /// <param name="gauserid">Google account id (String)</param>
        /// <param name="userId">Id of user(Guid)</param>
        /// <returns>Return object of Google analytic class.(Domein.GoogleAnalyticsAccount)</returns>
        public GoogleAnalyticsAccount getGoogelAnalyticsAccountHomeDetailsById(Guid userId, string gauserid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
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
                }//End Transaction
            }//End session
        }


        /// <checkGoogelAnalyticsUserExists>
        /// To check the Google account of user is exist.
        /// </summary>
        /// <param name="gaUserId">Id of google account(string)</param>
        /// <param name="Userid">Id of user (Guid)</param>
        /// <returns>Google account is exist for a user its returns tru otherwise False(Bool)</returns>
        public bool checkGoogelAnalyticsUserExists(string gaUserId, Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to to get account by user id and google account id.
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
                }//End Transaction
            }//End Session 
        }


        /// <checkGoogelAnalyticsProfileExists>
        /// Check the google account profile is exist
        /// </summary>
        /// <param name="gaUserId">Id of google account(String)</param>
        /// <param name="gaProfileId">Id of Google profile id(String)</param>
        /// <param name="Userid">Id of user (Guid)</param>
        /// <returns>If the propfile is exist its returns True or otherwise it retuns False(True or False)</returns>
        public bool checkGoogelAnalyticsProfileExists(string gaUserId,string gaProfileId, Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
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
                }//End Transaction
            }//End Session
        }


        /// <getUserDetails>
        /// Get user details by Google Account id.
        /// </summary>
        /// <param name="gaUserId">Id of google account(String)</param>
        /// <returns>Return object of Google analytic class.(Domein.GoogleAnalyticsAccount)</returns>
        public GoogleAnalyticsAccount getUserDetails(string gaUserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to get details of account by Google account Id.
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
                }//End Transaction
            }//End Session
        }


        /// <DeleteGoogleAnalyticsAccountByUserid>
        /// Delete Google Analytics Account By User id.
        /// </summary>
        /// <param name="userid">Id of the user(Guid)</param>
        /// <returns>Return 1 for successfully deleted Or 0 for not deleted.(Int)</returns>
        public int DeleteGoogleAnalyticsAccountByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete account
                        NHibernate.IQuery query = session.CreateQuery("delete from GoogleAnalyticsAccount where UserId = :userid")
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
                }//End transaction
            }//End Session
        }


    }
}
