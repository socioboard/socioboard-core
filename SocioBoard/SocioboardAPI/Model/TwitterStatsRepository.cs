using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
using System.Collections;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Services
{
    public class TwitterStatsRepository : ITwitterStatsRepository
    {
        public struct AgeWiseTwitterStats
        { 
            public int	Age1820  { get; set; }
	        public int Age2124 { get; set; }
	        public int Age2534 { get; set; }
	        public int Age3544 { get; set; }
	        public int Age4554 { get; set; }
	        public int Age5564 { get; set; }
            public int Age65 { get; set; }
        }
        public void addTwitterStats(Domain.Socioboard.Domain.TwitterStats TwtStats)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(TwtStats);
                    transaction.Commit();
                }
            }
        }

        public int deleteTwitterStats(Guid userid, string twtuserid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from TwitterStats where TwitterId = :twtuserid and UserId = :userid")
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

        public int updateTwitterStats(Domain.Socioboard.Domain.TwitterStats TwtStats)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = session.CreateQuery("Update  from TwitterStats where TwitterId = :twtuserid and UserId = :userid")
                                  .SetParameter("twtuserid", TwtStats.TwitterId)
                                  .SetParameter("userid", TwtStats.UserId)
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

        public System.Collections.ArrayList getAllTwitterStatsOfUser(string Profileid,Guid UserId,int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateSQLQuery("Select * from TwitterStats where TwitterId=:Profileid and UserId =:userid and EntryDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY) Group By Date(EntryDate)");
                    query.SetParameter("userid", UserId);
                    query.SetParameter("Profileid", Profileid);
                    ArrayList alstTwtStats = new ArrayList();

                    foreach (var item in query.List())
                    {
                        alstTwtStats.Add(item);
                    }
                    return alstTwtStats;

                }
            }
        }

        public System.Collections.ArrayList getTotalTwitterStatsOfUser(Guid UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateSQLQuery("Select * from TwitterStats where UserId =:userid");
                    query.SetParameter("userid", UserId);
                    ArrayList alstTwtStats = new ArrayList();

                    foreach (var item in query.List())
                    {
                        alstTwtStats.Add(item);
                    }
                    return alstTwtStats;

                }
            }
        }

        public bool checkTwitterStatsExists(string TwtUserId, Guid Userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from TwitterStats where UserId = :userid and TwitterId = :Twtuserid and Date_format(EntryDate,'%yy-%m-%d') LIKE Date_format(Now(),'%yy-%m-%d')");
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

        public bool checkTwitterStatsExists(string TwtUserId, Guid Userid, int FollowerCount, int FollowingCount)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //NHibernate.IQuery query = session.CreateQuery("from TwitterStats where UserId = :userid and TwitterId = :Twtuserid and FollowerCount: followercount and FollowingCount: followingcount and Date_format(EntryDate,'%yy-%m-%d') LIKE Date_format(Now(),'%yy-%m-%d')");
                        NHibernate.IQuery query = session.CreateQuery("from TwitterStats where UserId = :userid and TwitterId = :Twtuserid and FollowerCount = :followercount and FollowingCount = :followingcount and Date_format(EntryDate,'%yy-%m-%d') LIKE Date_format(Now(),'%yy-%m-%d')");

                        query.SetParameter("userid", Userid);
                        query.SetParameter("Twtuserid", TwtUserId);
                        query.SetParameter("followercount", FollowerCount);
                        query.SetParameter("followingcount", FollowingCount);

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

        public bool checkTwitterStatsExistsnew(string TwtUserId, Guid Userid,int followercount)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from TwitterStats where UserId = :userid and FollowerCount=:followercount and TwitterId = :Twtuserid and Date_format(EntryDate,'%yy-%m-%d') LIKE Date_format(Now(),'%yy-%m-%d')");
                        query.SetParameter("userid", Userid);

                        query.SetParameter("followercount", followercount);
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

        public ArrayList getTwitterStatsByIdDay(string twtuserid, Guid userid, int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        NHibernate.IQuery query = session.CreateSQLQuery("Select * from TwitterStats where UserId=:userid and TwitterId = :Twtuserid and EntryDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY) order by EntryDate desc limit 1");
                        query.SetParameter("userid", userid);
                        query.SetParameter("Twtuserid", twtuserid);
                        ArrayList alstTwtStats = new ArrayList();

                        foreach (var item in query.List())
                        {
                            alstTwtStats.Add(item);
                        }
                        return alstTwtStats;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }

        public ArrayList getTwitterStatsById(string twtuserid, Guid userid,int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Select * from TwitterStats where UserId = :userid and TwitterId = :Twtuserid and EntryDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY) Group By Date(EntryDate)
                        NHibernate.IQuery query = session.CreateSQLQuery("SELECT * FROM TwitterStats WHERE UserId=:userid and EntryDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY) and  TwitterId = :Twtuserid Group By Date(EntryDate)");
                        query.SetParameter("userid", userid);
                        query.SetParameter("Twtuserid", twtuserid);
                        ArrayList alstTwtStats = new ArrayList();

                        foreach (var item in query.List())
                        {
                            alstTwtStats.Add(item);
                        }
                        return alstTwtStats;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }

        public ArrayList calculteStats(Guid userid, string twtuserid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from TwitterStats where UserId=:userid and TwitterUserId = :Twtuserid");
                        query.SetParameter("userid", userid);
                        query.SetParameter("Twtuserid", twtuserid);
                        ArrayList alstTwtStats = new ArrayList();

                        foreach (Domain.Socioboard.Domain.TwitterStats item in query.Enumerable())
                        {
                            alstTwtStats.Add(item);
                        }
                        return alstTwtStats;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }

        public int getTweetsCount(Guid userid, string twtuserid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("Select Count(ID) as Tweets from TwitterMessage where UserId=:userid and profileId=:Twtuserid And Type='twt_usertweets'");
                        query.SetParameter("userid", userid);
                        query.SetParameter("Twtuserid", twtuserid);
                        int tweetCount = 0;
                        foreach (var item in query.Enumerable())
                        {
                            tweetCount = int.Parse(item.ToString());
                        }
                        return tweetCount;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }

                }
            }
        }

        public int getFeedsCount(Guid userid, string twtuserid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("select count(profileId) from TwitterFeed where UserId=:userid and profileId=:Twtuserid And Type='twt_feeds'");
                        query.SetParameter("userid", userid);
                        query.SetParameter("Twtuserid", twtuserid);
                        int feedCount = 0;
                        foreach (var item in query.Enumerable())
                        {
                            feedCount = int.Parse(item.ToString());
                        }
                        return feedCount;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }

                }
            }
        }

        public int getFollowersFollowingCount(Guid userid, string twtuserid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("Select FollowersCount,FollowingCount,'','' From TwitterAccount where UserId=:userid and profileId=:Twtuserid");
                        query.SetParameter("userid", userid);
                        query.SetParameter("Twtuserid", twtuserid);
                        int feedCount = 0;
                        foreach (var item in query.Enumerable())
                        {
                            feedCount = int.Parse(item.ToString());
                        }
                        return feedCount;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }

                }
            }
        }

        public object getFollowersAgeCount(Guid userid,int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select MAx(Age1820),MAx(Age2124),MAx(Age2534),MAx(Age3544),MAx(Age4554),MAx(Age5564),MAx(Age65) From TwitterStats where UserId=:userid and EntryDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY)");
                        query.SetParameter("userid", userid);
                      //  ArrayList alstTwtStats = new ArrayList();
                        object alstTwtStats = query.UniqueResult();
                        //foreach (object item in query.Enumerable())
                        //{
                           
                        //    alstTwtStats.Add(item);
                        //}
                        return alstTwtStats;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }

        public string getAgeDiffCount(string profileid, int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select MAx(Age1820),MAx(Age2124),MAx(Age2534),MAx(Age3544),MAx(Age4554),MAx(Age5564),MAx(Age65) From TwitterStats where TwitterId=:profileid and EntryDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY)");
                        query.SetParameter("profileid", profileid);
                        //  ArrayList alstTwtStats = new ArrayList();
                        string alstTwtStats = string.Empty;
                        foreach (var item in query.List())
                        {
                            Array temp = (Array)item;
                            alstTwtStats = "0," + temp.GetValue(0).ToString() + "," + temp.GetValue(1).ToString() + "," + temp.GetValue(2).ToString() + "," + temp.GetValue(3).ToString() + "," + temp.GetValue(4).ToString() + "," + temp.GetValue(5).ToString();
                        }
                        //foreach (object item in query.Enumerable())
                        //{

                        //    alstTwtStats.Add(item);
                        //}
                        return alstTwtStats;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }

        public ArrayList getFollowerFollowingCountMonth(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("Select Distinct Max(FollowerCount),Max(FollowingCount),Month(EntryDate) from TwitterStats  where UserId=:userid Group by MONTH(EntryDate)");
                        query.SetParameter("userid", userid);
                        ArrayList alstTwtStats = new ArrayList();
                        foreach (var item in query.List())
                        {
                            alstTwtStats.Add(item);
                        }
                       // object alstTwtStats = query.List();                       
                        return alstTwtStats;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }

        public int DeleteTwitterStatsByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from TwitterStats where UserId = :userid")
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

        public List<TwitterStats> getAllAccountDetail(string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        string str = "from TwitterStats where  TwitterId IN(";
                        string[] arrsrt = profileid.Split(',');
                        foreach (string sstr in arrsrt)
                        {
                            str += Convert.ToInt64(sstr) + ",";
                        }
                        str = str.Substring(0, str.Length - 1);
                        str += ") group by TwitterId";
                        List<TwitterStats> alst = session.CreateQuery(str)
                       .List<TwitterStats>()
                       .ToList<TwitterStats>();
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

        public ArrayList getTwitterStatsByIdbeforeDay(string twtuserid,Guid userid ,int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        NHibernate.IQuery query = session.CreateSQLQuery("Select * from TwitterStats where UserId=:userid and  TwitterId = :Twtuserid and EntryDate < DATE_ADD(NOW(),INTERVAL -" + days + " DAY) order by EntryDate desc limit 1");
                        query.SetParameter("userid", userid);
                        query.SetParameter("Twtuserid", twtuserid);
                        ArrayList alstTwtStats = new ArrayList();

                        foreach (var item in query.List())
                        {
                            alstTwtStats.Add(item);
                        }
                        return alstTwtStats;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }

        public ArrayList getTwitterStByIdbeforeDay(string twtuserid, int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        NHibernate.IQuery query = session.CreateSQLQuery("Select * from TwitterStats where  TwitterId = :Twtuserid and EntryDate>= DATE_ADD(NOW(),INTERVAL -" + days + " DAY) order by EntryDate asc limit 1");
                      //  query.SetParameter("days", days);
                        query.SetParameter("Twtuserid", twtuserid);
                        ArrayList alstTwtStats = new ArrayList();

                        foreach (var item in query.List())
                        {
                            alstTwtStats.Add(item);
                        }
                        return alstTwtStats;

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