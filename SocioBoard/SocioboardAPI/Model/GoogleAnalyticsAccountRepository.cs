using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using Api.Socioboard.Helper;
using NHibernate.Linq;
namespace Api.Socioboard.Services
{
    public class GoogleAnalyticsAccountRepository
    {

        /// <addGoogleAnalyticsUser>
        /// Add New Google Analytics User
        /// </summary>
        /// <param name="gaaccount">Set Values in a GoogleAnalyticsAccount Class Property and Pass the same Object of GoogleAnalyticsAccount Class.(Domain.GoogleAnalyticsAccount)</param>
        public void Add(Domain.Socioboard.Domain.GoogleAnalyticsAccount _GoogleAnalyticsAccount)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data.
                    session.Save(_GoogleAnalyticsAccount);
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
        public int deleteGoogelAnalyticsUser(string Profileid, Guid userid)
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
                        NHibernate.IQuery query = session.CreateQuery("delete from GoogleAnalyticsAccount where GaProfileId = :gauserid and UserId = :userid")
                                        .SetParameter("gauserid", Profileid)
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
        public void updateGoogelAnalyticsUser(string ProfileId, double visit, double pageView, double twtMention, double articleAndBlog)
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
                        session.CreateQuery("Update GoogleAnalyticsAccount set Visits =: Visits, Views=: Views, TwitterPosts =: TwitterPosts, WebMentions =: WebMentions where GaProfileId=:gaprofileid")
                            .SetParameter("gaprofileid", ProfileId)
                            .SetParameter("Visits", visit)
                            .SetParameter("Views", pageView)
                            .SetParameter("TwitterPosts", twtMention)
                            .SetParameter("WebMentions", articleAndBlog)
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
        public List<Domain.Socioboard.Domain.GoogleAnalyticsAccount> getGoogelAnalyticsAccountsByUser(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Proceed action, to get deatils.
                List<Domain.Socioboard.Domain.GoogleAnalyticsAccount> lstGoogleAnalyticsAccount = session.CreateQuery("from GoogleAnalyticsAccount where UserId = :userid")
                    .SetParameter("userid", UserId).List<Domain.Socioboard.Domain.GoogleAnalyticsAccount>().ToList();
                return lstGoogleAnalyticsAccount;

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
        public List<Domain.Socioboard.Domain.GoogleAnalyticsAccount> getAllGoogleAnalyticsAccounts()
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Proceed action, to get all google account details.
                List<Domain.Socioboard.Domain.GoogleAnalyticsAccount> lstGoogleAnalyticsAccount = session.Query<Domain.Socioboard.Domain.GoogleAnalyticsAccount>().GroupBy(t => t.GaProfileId).Select(g=>g.First()).ToList();
                return lstGoogleAnalyticsAccount;
            }//End Session
        }


        /// <getGoogelAnalyticsAccountDetailsById>
        /// Get the details of googel analytics account by Google account id and user id.
        /// </summary>
        /// <param name="gauserid">Id of goole accont(String)</param>
        /// <param name="userId">Id of user(Guid)</param>
        /// <returns>Return object of Google analytic class.(Domein.GoogleAnalyticsAccount)</returns>
        public Domain.Socioboard.Domain.GoogleAnalyticsAccount getGoogleAnalyticsAccountDetailsById(string gauserid, Guid userId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Proceed action, to get deatils of account
                NHibernate.IQuery query = session.CreateQuery("from GoogleAnalyticsAccount where GaProfileId = :GaAccountId and UserId=:userId")
                .SetParameter("GaAccountId", gauserid)
                .SetParameter("userId", userId);
                Domain.Socioboard.Domain.GoogleAnalyticsAccount result = (Domain.Socioboard.Domain.GoogleAnalyticsAccount)query.UniqueResult();
                return result;

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
        public bool checkGoogelAnalyticsUserExists(string gaProfileId, string gaAccount, Guid Userid)
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
                        NHibernate.IQuery query = session.CreateQuery("from GoogleAnalyticsAccount where UserId = :userid and GaAccountId = :gaUserId and GaProfileId =: gaProfileId");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("gaUserId", gaAccount);
                        query.SetParameter("gaProfileId", gaProfileId);
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
        public bool checkGoogelAnalyticsProfileExists(string gaUserId, string gaProfileId, Guid Userid)
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
                        query.SetParameter("gaProfileId", gaProfileId);
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
        public Domain.Socioboard.Domain.GoogleAnalyticsAccount getUserDetails(string gaUserId)
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
                        List<Domain.Socioboard.Domain.GoogleAnalyticsAccount> lst = new List<Domain.Socioboard.Domain.GoogleAnalyticsAccount>();

                        foreach (Domain.Socioboard.Domain.GoogleAnalyticsAccount item in query.Enumerable())
                        {

                            lst.Add(item);
                            break;
                        }
                        Domain.Socioboard.Domain.GoogleAnalyticsAccount fbacc = lst[0];
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
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from GoogleAnalyticsAccount where UserId = :userid")
                                        .SetParameter("userid", userid);
                        int isUpdated = query.ExecuteUpdate();
                        transaction.Commit();
                        return isUpdated;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }
        }

        public void AddGoogleAnalyticsReport(Domain.Socioboard.Domain.GoogleAnalyticsReport _GoogleAnalyticsReport)
        { 
         //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                bool exist = session.Query<Domain.Socioboard.Domain.GoogleAnalyticsReport>().Any(t => t.GaProfileId == _GoogleAnalyticsReport.GaProfileId);
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    if (!exist)
                    {
                        session.Save(_GoogleAnalyticsReport);
                        transaction.Commit();
                    }
                    else 
                    {
                        try
                        {
                            session.CreateQuery("Update GoogleAnalyticsReport set Visits=:Visits, Views=:Views,TwitterMention=:TwitterMention, Article_Blogs =: Article_Blogs where GaProfileId =: GaProfileId")
                                                    .SetParameter("Visits", _GoogleAnalyticsReport.Visits)
                                                    .SetParameter("Views", _GoogleAnalyticsReport.Views)
                                                    .SetParameter("GaProfileId", _GoogleAnalyticsReport.GaProfileId)
                                                    .SetParameter("TwitterMention", _GoogleAnalyticsReport.TwitterMention)
                                                    .SetParameter("Article_Blogs", _GoogleAnalyticsReport.Article_Blogs)
                                                    .ExecuteUpdate();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
        
        }

        public Domain.Socioboard.Domain.GoogleAnalyticsReport GetGoogleAnalyticsReport(string ProfileId)
        {
            Domain.Socioboard.Domain.GoogleAnalyticsReport _GoogleAnalyticsReport;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    _GoogleAnalyticsReport = session.Query<Domain.Socioboard.Domain.GoogleAnalyticsReport>().Where(t => t.GaProfileId == ProfileId).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    _GoogleAnalyticsReport = new Domain.Socioboard.Domain.GoogleAnalyticsReport();
                }
            }
            return _GoogleAnalyticsReport;
        }
    }
}
