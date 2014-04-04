using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
using NHibernate.Mapping;
using System.Collections;

namespace SocioBoard.Model
{
    public class FacebookAccountRepository : IFacebookAccountRepository
    {
        /// <addFacebookUser>
        /// Add new facebok user in  database.
        /// </summary>
        /// <param name="fbaccount">Set Values in a FacebookAccount Class Property and Pass the same Object of FacebookAccount Class.(Domain.FacebookAccount)</param>
        public void addFacebookUser(FacebookAccount fbaccount)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //proceed action, to save data.
                    session.Save(fbaccount);
                    transaction.Commit();
                }//End Transaction
            }//End session
        }


        /// <deleteFacebookUser>
        /// Delete a FacebookAccount from Database by FbUserId(String) and UserId(Guid)
        /// </summary>
        /// <param name="FBuserid">FbUserId of FacebookAccount(string)</param>
        /// <param name="userid">Id of FacebookAccount(Guid)</param>
        /// <returns>Return integer 1 for true 0 for false</returns>
        public int deleteFacebookUser(string FBuserid, Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete a FacebookAccount by FbUserId and UserId.
                        NHibernate.IQuery query = session.CreateQuery("delete from FacebookAccount where FbUserId = :fbuserid and UserId = :userid")
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
                }//End Transaction
            }//End session
        }


        /// <getFbMessageStats>
        /// Get total number of id of FacebookMessage by UserId(Guid) and days(int).
        /// </summary>
        /// <param name="userid">Id of User(Guid)</param>
        /// <param name="days">Int das</param>
        /// <returns>Retutn a id of FacebookMessage in form of ArrayList</returns>
        public ArrayList getFbMessageStats(Guid userid, int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Select Distinct Count(MessageId) from FacebookMessage where MessageDate>=DATE_ADD(NOW(),INTERVAL -7 DAY) and UserId =:userid Group by DATE_FORMAT(MessageDate,'%y-%m-%d')

                        //Proceed action to get total number of id of FacebookMessage by UserId(Guid) and days(int).
                        NHibernate.IQuery query = session.CreateSQLQuery("SELECT count(Id) FROM FacebookMessage WHERE EntryDate > DATE_SUB(NOW(), INTERVAL " + days + " DAY) and UserId =:userid GROUP BY WEEK(EntryDate)");
                        query.SetParameter("userid", userid);
                        ArrayList alstFBmsgs = new ArrayList();

                        foreach (var item in query.List())
                        {
                            alstFBmsgs.Add(item);
                        }
                        return alstFBmsgs;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End session
        }


        /// <getFbMessageStatsHome>
        /// Get total number of MessageId of FacebookMessage by UserId(Guid) and days(int).
        /// </summary>
        /// <param name="userid">Id of User(Guid)</param>
        /// <param name="days">Int das</param>
        /// <returns>Retutn a MessageId of FacebookMessage in form of ArrayList</returns>
        public ArrayList getFbMessageStatsHome(Guid userid, int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to get total number of MessageId of FacebookMessage by UserId(Guid) and days(int).
                        NHibernate.IQuery query = session.CreateSQLQuery("Select Distinct Count(MessageId) from FacebookMessage where MessageDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY) and UserId =:userid Group by DATE_FORMAT(MessageDate,'%y-%m-%d')");
                        query.SetParameter("userid", userid);
                        ArrayList alstFBmsgs = new ArrayList();

                        foreach (var item in query.List())
                        {
                            alstFBmsgs.Add(item);
                        }
                        return alstFBmsgs;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End session
        }


        /// <getFbFeedsStats>
        /// Get total number of id of FacebookFeed by UserId(Guid) and days(int). 
        /// </summary>
        /// <param name="userid">Id of User(Guid)</param>
        /// <param name="days">Int days</param>
        /// <returns>Retutn a id of FacebookFeed in form of ArrayList</returns>
        public ArrayList getFbFeedsStats(Guid userid, int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Select Distinct Count(FeedId) from FacebookFeed where EntryDate>=DATE_ADD(NOW(),INTERVAL -7 DAY)  and UserId =:userid Group by DATE_FORMAT(EntryDate,'%y-%m-%d') 


                        //Proceed action to get total number of id of FacebookFeed by UserId(Guid) and days(int).
                        NHibernate.IQuery query = session.CreateSQLQuery("SELECT count(Id) FROM FacebookFeed WHERE FeedDate > DATE_SUB(NOW(), INTERVAL " + days + " DAY) and UserId =:userid GROUP BY WEEK(FeedDate)");
                        query.SetParameter("userid", userid);
                        ArrayList alstFBmsgs = new ArrayList();

                        foreach (var item in query.List())
                        {
                            alstFBmsgs.Add(item);
                        }
                        return alstFBmsgs;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End session
        }


        /// <getFbFeedsStatsHome>
        /// Get total number of FeedId of FacebookFeed by UserId(Guid).
        /// </summary>
        /// <param name="userid">Id of User(Guid)</param>
        /// <returns>Retutn a FeedId of FacebookFeed in form of ArrayList</returns>
        public ArrayList getFbFeedsStatsHome(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        ////Proceed action to get total number of FeedId of FacebookFeed by UserId(Guid).
                        NHibernate.IQuery query = session.CreateSQLQuery("Select Distinct Count(FeedId) from FacebookFeed where EntryDate>=DATE_ADD(NOW(),INTERVAL -7 DAY)  and UserId =:userid Group by DATE_FORMAT(EntryDate,'%y-%m-%d') ");
                        query.SetParameter("userid", userid);
                        ArrayList alstFBmsgs = new ArrayList();

                        foreach (var item in query.List())
                        {
                            alstFBmsgs.Add(item);
                        }
                        return alstFBmsgs;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End session
        }


        /// <updateFacebookUser>
        /// Update FbUserName,AccessToken,Friends,EmailId,IsActive of existing FacebookAccount by UserId.
        /// </summary>
        /// <param name="fbaccount">Set Values in a FacebookAccount Class Property and Pass the same Object of FacebookAccount Class.(Domain.FacebookAccount)</param>
        public void updateFacebookUser(FacebookAccount fbaccount)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        // Proceed action to Update Data.
                        // And Set the reuired paremeters to find the specific values.
                        session.CreateQuery("Update FacebookAccount set FbUserName =:fbusername,AccessToken =:access,Friends =:friends,EmailId=:emailid,IsActive=:status where FbUserId = :fbuserid and UserId = :userid")
                            .SetParameter("fbusername", fbaccount.FbUserName)
                            .SetParameter("access", fbaccount.AccessToken)
                            .SetParameter("friends", fbaccount.Friends)
                            .SetParameter("emailid", fbaccount.EmailId)
                            .SetParameter("fbuserid", fbaccount.FbUserId)
                            .SetParameter("userid", fbaccount.UserId)
                            .SetParameter("status", fbaccount.IsActive)
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

        /// <UpdateFBAccessTokenByFBUserId>
        /// Update FacebookAccount by fbUserId(String)
        /// </summary>
        /// <param name="fbUserId">fbUserId of FacebookAccount(string)</param>
        /// <param name="accessToken">accessToken of FacebookAccount(string)</param>
        /// <returns>Return integer 1 for true 0 for false.</returns>
        public int UpdateFBAccessTokenByFBUserId(string fbUserId, string accessToken)
        {
            int update = 0;

            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction.
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            //Proceed action, to update Data
                            // And Set the reuired paremeters to find the specific values.
                            update = session.CreateQuery("Update FacebookAccount set AccessToken = :accessToken where  FbUserId = :fbUserId")
                                .SetParameter("accessToken", accessToken)
                                .SetParameter("fbUserId", fbUserId)
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
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }

            return update;
        }


        /// <sugetAllFacebookAccountsOfUsermmary>
        /// Get all Facebook Account of User by UserId(Guid).
        /// </summary>
        /// <param name="UserId">UserId of User(Guid)</param>
        /// <returns>Retutn all FacebookAccount in form of ArrayList</returns>
        public ArrayList getAllFacebookAccountsOfUser(Guid UserId)
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    //Proceed action, to Get all FacebookAccount by UserId(Guid).
                    NHibernate.IQuery query = session.CreateQuery("from FacebookAccount where UserId = :userid");
                    query.SetParameter("userid", UserId);
                    ArrayList alstFBAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstFBAccounts.Add(item);
                    }
                    return alstFBAccounts;
                }//End Transaction
            }//End session
        }


        /// <getAllFacebookPagesOfUser>
        /// Get all Facebook Account of User by UserId(Guid) and Type.
        /// </summary>
        /// <param name="UserId">UserId of User(Guid)</param>
        /// <returns>Retutn all FacebookAccount in form of ArrayList</returns>
        public ArrayList getAllFacebookPagesOfUser(Guid UserId)
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    //Proceed action, to Get all FacebookAccount by UserId(Guid) and Type.
                    NHibernate.IQuery query = session.CreateQuery("from FacebookAccount where UserId = :userid And Type=:page");
                    query.SetParameter("userid", UserId);
                    query.SetParameter("page", "page");
                    ArrayList alstFBAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstFBAccounts.Add(item);
                    }
                    return alstFBAccounts;

                }//End Transaction
            }//End session

        }


        /// <getAllFacebookAccounts>
        /// Get all FacebookAccount.
        /// </summary>
        /// <returns>Retutn all FacebookAccount in form of ArrayList</returns>
        public ArrayList getAllFacebookAccounts()
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to Get all FacebookAccount.
                    NHibernate.IQuery query = session.CreateQuery("from FacebookAccount");
                    ArrayList alstFBAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstFBAccounts.Add(item);
                    }
                    return alstFBAccounts;

                }//End Transaction
            }//End session

        }


        /// <getFacebookAccountsOfUser>
        /// Get all Facebook Account of User by UserId(Guid)
        /// </summary>
        /// <param name="UserId">UserId of User(Guid)</param>
        /// <returns>Retutn all FacebookAccount in form of ArrayList</returns>
        public ArrayList getFacebookAccountsOfUser(Guid UserId)
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    // NHibernate.IQuery query = session.CreateQuery("from FacebookAccount where UserId = :userid and Type='account'");

                    //Proceed action, to Get all FacebookAccount by UserId(Guid).
                    NHibernate.IQuery query = session.CreateQuery("from FacebookAccount where UserId = :userid");
                    query.SetParameter("userid", UserId);
                    ArrayList alstFBAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstFBAccounts.Add(item);
                    }
                    return alstFBAccounts;

                }//End Transaction
            }//End session

        }


        /// <summary>
        /// Get all Facebook Account of User by UserId(Guid) and FbUserId(string).
        /// </summary>
        /// <param name="Fbuserid">Fbuserid of User(String)</param>
        /// <param name="userId">UserId of User(Guid)</param>
        /// <returns>Return a object of FacebookAccount with  value of each member.</returns>
        public FacebookAccount getFacebookAccountDetailsById(string Fbuserid, Guid userId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    NHibernate.IQuery query = session.CreateQuery("from FacebookAccount where FbUserId = :Fbuserid and UserId=:userId");
                    query.SetParameter("Fbuserid", Fbuserid);
                    query.SetParameter("userId", userId);
                    FacebookAccount result = (FacebookAccount)query.UniqueResult();
                    return result;
                }//End Transaction
            }//End session
        }


        /// <checkFacebookUserExists>
        /// Check if FacebookUser is Exist in database or not by UserId and FbuserId.
        /// </summary>
        /// <param name="FbUserId">Fbuserid of User(String)</param>
        /// <param name="Userid">UserId of User(Guid)</param>
        /// <returns>Return true or false</returns>
        public bool checkFacebookUserExists(string FbUserId, Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to Check if FacebookUser is Exist in database or not by UserId and FbuserId.
                        // And Set the reuired paremeters to find the specific values.
                        NHibernate.IQuery query = session.CreateQuery("from FacebookAccount where UserId = :userid and FbUserId = :fbuserid");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("fbuserid", FbUserId);
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


        /// <getUserDetails>
        /// Get User's all Detail from FacebookAccount by FbUserId.
        /// </summary>
        /// <param name="FbUserId">FbUserId of FacebookAccount(string)</param>
        /// <returns>Return a object of FacebookAccount with  value of each member.</returns>
        public FacebookAccount getUserDetails(string FbUserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action to, Get User's all Detail from FacebookAccount by FbUserId.
                        NHibernate.IQuery query = session.CreateQuery("from FacebookAccount where FbUserId = :fbuserid");

                        query.SetParameter("fbuserid", FbUserId);
                        List<FacebookAccount> lst = new List<FacebookAccount>();

                        foreach (FacebookAccount item in query.Enumerable())
                        {

                            lst.Add(item);
                            break;
                        }
                        FacebookAccount fbacc = lst[0];
                        return fbacc;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End session
        }


        /// <getOnlyFacebookAccountsOfUser>
        /// Get all Facebook Account of by UserId(Guid). Type='account'
        /// </summary>
        /// <param name="UserId">UserId of User(Guid)</param>
        /// <returns>Return only FacebookAccount of User in form of ArrayList</returns>
        public ArrayList getOnlyFacebookAccountsOfUser(Guid UserId)
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    //Proceed action to get all Facebook Account of by UserId(Guid). Type='account'
                    NHibernate.IQuery query = session.CreateQuery("from FacebookAccount where UserId = :userid and Type='account'");
                    query.SetParameter("userid", UserId);
                    ArrayList alstFBAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstFBAccounts.Add(item);
                    }
                    return alstFBAccounts;

                }//End Transaction
            }//End session

        }



        /// <updateFansCount>
        /// update afriends in FacebookAccount by UserId(Guid) , FbUserId(string).
        /// </summary>
        /// <param name="fbUserId">fbUserId of FacebookAccount(string)</param>
        /// <param name="UserID">UserId of User(Guid)</param>
        /// <param name="friends">friends of FacebookAccount (Frinds)</param>
        public void updateFansCount(string fbUserId, Guid UserID, int friends)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        // Proceed action to update friends in FacebookAccount by UserId(Guid) , FbUserId(string).
                        session.CreateQuery("update FacebookAccount set Friends =:friends where UserId =:userid and FbUserId =:fbuserid")
                                        .SetParameter("fbuserid", fbUserId)
                                        .SetParameter("friends", friends)
                                        .SetParameter("userid", UserID)
                                        .ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                }//End Transaction
            }//End session

        }


        /// <updateFriendsCount>
        /// update afriends in FacebookAccount by FbUserId(string).
        /// </summary>
        /// <param name="fbUserId">fbUserId of FacebookAccount(string)</param>
        /// <param name="UserID">UserId of User(Guid)</param>
        /// <param name="friends">friends of FacebookAccount (Frinds)</param>
        public void updateFriendsCount(string fbUserId, Guid UserID, int friends)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        // Proceed action to update friends in FacebookAccount by FbUserId(string).
                        session.CreateQuery("update FacebookAccount set Friends =:friends where FbUserId =:fbuserid")
                                        .SetParameter("fbuserid", fbUserId)
                                        .SetParameter("friends", friends)
                                        .ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                }//End Transaction
            }//End session

        }


        /// <getpostrepliesgraph>
        /// Get CommentCount from FacebookStats by UserId(Guid) and Limit of Start to End.
        /// </summary>
        /// <param name="userid">UserId User(Guid)</param>
        /// <param name="start">Integer Start</param>
        /// <param name="end">Integer End</param>
        /// <returns>Return total number of Comment in the form of ArrayList.</returns>
        public ArrayList getpostrepliesgraph(Guid userid, int start, int end)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Select Distinct Count(MessageId) from FacebookMessage where MessageDate>=DATE_ADD(NOW(),INTERVAL -7 DAY) and UserId =:userid Group by DATE_FORMAT(MessageDate,'%y-%m-%d')
                        if (end == 0)
                        {

                            //Proceed action to get CommentCount from FacebookStats by UserId(Guid) and Limit of Start to End.
                            NHibernate.IQuery query = session.CreateSQLQuery("select fs.CommentCount from FacebookStats fs,SocialProfile sp where fs.FbUserId= sp.ProfileId and sp.UserId=" + userid + " and sp.ProfileType='facebook' limit" + start + "," + end + "");
                            query.SetParameter("userid", userid);
                            ArrayList alstFBmsgs = new ArrayList();

                            foreach (var item in query.List())
                            {
                                alstFBmsgs.Add(item);
                            }
                            return alstFBmsgs;
                        }
                        else
                        {

                            //Proceed action to get CommentCount from FacebookStats by UserId(Guid) and Limit of Start to End.
                            NHibernate.IQuery query = session.CreateSQLQuery("select fs.CommentCount from FacebookStats fs,SocialProfile sp where fs.FbUserId= sp.ProfileId and sp.UserId=" + userid + " and sp.ProfileType='facebook' limit" + start + "," + end + "");
                            query.SetParameter("userid", userid);
                            ArrayList alstFBmsgs = new ArrayList();

                            foreach (var item in query.List())
                            {
                                alstFBmsgs.Add(item);
                            }
                            return alstFBmsgs;
                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End session
        }



        /// <getPagelikebyUserId>
        /// Get all like from facebookaccount by UserId(Guid) and TYPE.
        /// </summary>
        /// <param name="UserId">UserId User(Guid)</param>
        /// <returns> integer </returns>
        public int getPagelikebyUserId(Guid UserId)
        {
            int TotalLikes = 0;

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action Get all like from facebookaccount by UserId(Guid) and TYPE.
                        NHibernate.IQuery query = session.CreateQuery("SELECT SUM(Friends) FROM FacebookAccount WHERE TYPE=:PageType AND UserId =:userid")
                                        .SetParameter("userid", UserId)
                                        .SetParameter("PageType", "page");

                        ArrayList alstFBAccounts = new ArrayList();

                        foreach (var item in query.Enumerable())
                        {
                            TotalLikes = Convert.ToInt32(item);
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                }//End Transaction
            }//End session

            return TotalLikes;

        }



        /// <DeleteFacebookAccountByUserid>
        /// Delete FacebookAccount By userId(Guid)
        /// </summary>
        /// <param name="userid">UserId User(Guid)</param>
        /// <returns>Return integer 1 for true 0 for false.</returns>
        public int DeleteFacebookAccountByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action Delete FacebookAccount By userId(Guid)
                        NHibernate.IQuery query = session.CreateQuery("delete from FacebookAccount where UserId = :userid")
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