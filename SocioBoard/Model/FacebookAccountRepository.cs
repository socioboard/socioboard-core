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
        public void addFacebookUser(FacebookAccount fbaccount)
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

        public int deleteFacebookUser(string FBuserid, Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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
                }
            }
        }

        public ArrayList getFbMessageStats(Guid userid,int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Select Distinct Count(MessageId) from FacebookMessage where MessageDate>=DATE_ADD(NOW(),INTERVAL -7 DAY) and UserId =:userid Group by DATE_FORMAT(MessageDate,'%y-%m-%d')
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
                }
            }
        }

        public ArrayList getFbMessageStatsHome(Guid userid, int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //
                        NHibernate.IQuery query = session.CreateSQLQuery("Select Distinct Count(MessageId) from FacebookMessage where MessageDate>=DATE_ADD(NOW(),INTERVAL -"+ days +" DAY) and UserId =:userid Group by DATE_FORMAT(MessageDate,'%y-%m-%d')");
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
                }
            }
        }

        public ArrayList getFbFeedsStats(Guid userid,int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Select Distinct Count(FeedId) from FacebookFeed where EntryDate>=DATE_ADD(NOW(),INTERVAL -7 DAY)  and UserId =:userid Group by DATE_FORMAT(EntryDate,'%y-%m-%d') 
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
                }
            }
        }

        public ArrayList getFbFeedsStatsHome(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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
                }
            }
        }

        public void updateFacebookUser(FacebookAccount fbaccount)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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
                }
            }
        }

        public int UpdateFBAccessTokenByFBUserId(string fbUserId, string accessToken)
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
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }

            return update;
        }

        public ArrayList getAllFacebookAccountsOfUser(Guid UserId)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from FacebookAccount where UserId = :userid");
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

        public ArrayList getAllFacebookPagesOfUser(Guid UserId)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from FacebookAccount where UserId = :userid And Type=:page");
                    query.SetParameter("userid", UserId);
                    query.SetParameter("page", "page");
                    ArrayList alstFBAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstFBAccounts.Add(item);
                    }
                    return alstFBAccounts;

                }
            }

        }

        public ArrayList getAllFacebookAccounts()
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from FacebookAccount");
                    ArrayList alstFBAccounts = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstFBAccounts.Add(item);
                    }
                    return alstFBAccounts;

                }
            }

        }

        public ArrayList getFacebookAccountsOfUser(Guid UserId)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from FacebookAccount where UserId = :userid and Type='account'");
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


        public FacebookAccount getFacebookAccountDetailsById(string Fbuserid, Guid userId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from FacebookAccount where FbUserId = :Fbuserid and UserId=:userId");
                    query.SetParameter("Fbuserid", Fbuserid);
                    query.SetParameter("userId", userId);
                    FacebookAccount result = (FacebookAccount)query.UniqueResult();
                    return result;
                }
            }
        }

        public bool checkFacebookUserExists(string FbUserId, Guid Userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                }
            }
        }

        public FacebookAccount getUserDetails(string FbUserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                }
            }
        }

        public ArrayList getOnlyFacebookAccountsOfUser(Guid UserId)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from FacebookAccount where UserId = :userid and Type='account'");
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

        public void updateFansCount(string fbUserId, Guid UserID, int friends)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                }
            }

        }

        public void updateFriendsCount(string fbUserId, Guid UserID, int friends)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                }
            }

        }

        public ArrayList getpostrepliesgraph(Guid userid,int start,int end)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Select Distinct Count(MessageId) from FacebookMessage where MessageDate>=DATE_ADD(NOW(),INTERVAL -7 DAY) and UserId =:userid Group by DATE_FORMAT(MessageDate,'%y-%m-%d')
                        if (end == 0)
                        {
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
                }
            }
        }

        public int getPagelikebyUserId(Guid UserId)
        {
            int TotalLikes = 0;

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                }
            }

            return TotalLikes;

        }


        public int DeleteFacebookAccountByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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
                }
            }
        }

        
    }
}