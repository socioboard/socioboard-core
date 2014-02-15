using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
using System.Collections;

namespace SocioBoard.Model
{
    public class TwitterMessageRepository:ITwitterMessageRepository
    {

        public void addTwitterMessage(TwitterMessage twtmsg)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(twtmsg);
                    transaction.Commit();
                }
            }
        }

        public int deleteTwitterMessage(TwitterMessage twtmsg)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from TwitterMessage where ProfileId = :twtuserid and UserId = :userid")
                                        .SetParameter("twtuserid", twtmsg.ProfileId)
                                        .SetParameter("userid", twtmsg.UserId);
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


        public int deleteTwitterMessage(string profileid,Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from TwitterMessage where ProfileId = :twtuserid and UserId = :userid")
                                        .SetParameter("twtuserid", profileid)
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

        public int updateTwitterMessage(TwitterMessage fbaccount)
        {
            throw new NotImplementedException();
        }

        public List<TwitterMessage> getAllTwitterMessagesOfUser(Guid UserId,string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from TwitterMessage where UserId = :userid and ProfileId = :profid");
                        query.SetParameter("userid", UserId);
                        query.SetParameter("profid", profileid);
                        List<TwitterMessage> lstmsg = new List<TwitterMessage>();
                        foreach (TwitterMessage item in query.Enumerable<TwitterMessage>().OrderByDescending(x=>x.MessageDate))
                        {
                            lstmsg.Add(item);
                        }
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }


        public List<TwitterMessage> getAllReadMessagesOfUser(Guid UserId, string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<TwitterMessage> lstmsg = session.CreateQuery("from TwitterMessage where ReadStatus = 1 and UserId = :userid and ProfileId = :profid")
                       .SetParameter("userid", UserId)
                       .SetParameter("profid", profileid)
                       .List<TwitterMessage>()
                       .ToList<TwitterMessage>();

                        //List<TwitterMessage> lstmsg = new List<TwitterMessage>();
                        //foreach (TwitterMessage item in query.Enumerable<TwitterMessage>().OrderByDescending(x => x.MessageDate))
                        //{
                        //    lstmsg.Add(item);
                        //}
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }

        public List<TwitterMessage> getAllReadMessagesOfUser(string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<TwitterMessage> lstmsg = session.CreateQuery("from TwitterMessage where ReadStatus = 1 and  ProfileId = :profid")
                       .SetParameter("profid", profileid)
                       .List<TwitterMessage>()
                       .ToList<TwitterMessage>();

                        //List<TwitterMessage> lstmsg = new List<TwitterMessage>();
                        //foreach (TwitterMessage item in query.Enumerable<TwitterMessage>().OrderByDescending(x => x.MessageDate))
                        //{
                        //    lstmsg.Add(item);
                        //}
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }
        public bool checkTwitterMessageExists(string Id, Guid Userid,string messageid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from TwitterMessage where UserId = :userid and ProfileId = :Twtuserid and MessageId = :messid");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("Twtuserid", Id);
                        query.SetParameter("messid", messageid);
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

        public List<TwitterMessage> getAllTwitterMessages(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<TwitterMessage> lstmsg = session.CreateQuery("from TwitterMessage where UserId = :userid")
                        .SetParameter("userid", userid)
                        .List<TwitterMessage>()
                        .ToList<TwitterMessage>();


                        //List<TwitterMessage> lstmsg = new List<TwitterMessage>();
                        //foreach (TwitterMessage item in query.Enumerable<TwitterMessage>().OrderByDescending(x=>x.MessageDate))
                        //{
                        //    lstmsg.Add(item);
                        //}
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }

        /*********************************************************************/

        public List<TwitterMessage> getAllTwitterMessagesOfProfile(string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<TwitterMessage> lstmsg = session.CreateQuery("from TwitterMessage where  ProfileId = :profid")
                        .SetParameter("profid", profileid)
                        .List<TwitterMessage>()
                        .ToList<TwitterMessage>();

                        //List<TwitterMessage> lstmsg = new List<TwitterMessage>();
                        //foreach (TwitterMessage item in query.Enumerable<TwitterMessage>().OrderByDescending(x => x.MessageDate))
                        //{
                        //    lstmsg.Add(item);
                        //}
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }

        public ArrayList gettwtMessageStats(Guid UserId,int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Select Distinct Count(MessageId) from TwitterMessage where EntryDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY) and UserId =:userid Group by DATE_FORMAT(EntryDate,'%y-%m-%d') 
                        NHibernate.IQuery query = session.CreateSQLQuery("SELECT count(Id) FROM TwitterMessage WHERE EntryDate > DATE_SUB(NOW(), INTERVAL "+ days +" DAY) and UserId =:userid GROUP BY WEEK(EntryDate)")
                            .SetParameter("userid", UserId);
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

        public ArrayList gettwtMessageStatsHome(Guid UserId, int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select Distinct Count(MessageId) from TwitterMessage where EntryDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY) and UserId =:userid Group by DATE_FORMAT(EntryDate,'%y-%m-%d')")
                            .SetParameter("userid", UserId);
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

        public ArrayList gettwtMessageStatsByProfileId(Guid UserId,string profileId,int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select Distinct Count(MessageId) from TwitterMessage where EntryDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY) and UserId =:userid and ProfileId='" + profileId + "' Group by DATE_FORMAT(EntryDate,'%y-%m-%d') ")
                            .SetParameter("userid", UserId);
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

        public ArrayList gettwtFeedsStats(Guid UserId,int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Select Distinct Count(MessageId) from TwitterFeed where EntryDate>=DATE_ADD(NOW(),INTERVAL -7 DAY) and UserId =:userid Group by DATE_FORMAT(EntryDate,'%y-%m-%d') 
                        NHibernate.IQuery query = session.CreateSQLQuery("SELECT count(Id) FROM TwitterFeed WHERE FeedDate > DATE_SUB(NOW(), INTERVAL "+ days +" DAY) and UserId =:userid GROUP BY WEEK(FeedDate)")
                            .SetParameter("userid", UserId);
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

        public ArrayList gettwtFeedsStatsHome(Guid UserId,int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select Distinct Count(MessageId) from TwitterFeed where EntryDate>=DATE_ADD(NOW(),INTERVAL -"+ days +" DAY) and UserId =:userid Group by DATE_FORMAT(EntryDate,'%y-%m-%d') ")
                            .SetParameter("userid", UserId);
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

        public ArrayList gettwtFeedsStatsByProfileId(Guid UserId,string profileId,int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select Distinct Count(MessageId) from TwitterFeed where EntryDate>=DATE_ADD(NOW(),INTERVAL -"+ days +" DAY) and UserId =:userid and ProfileId=:profileId Group by DATE_FORMAT(EntryDate,'%y-%m-%d') ")
                            .SetParameter("userid", UserId)
                        .SetParameter("profileId", profileId);
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

        public bool checkTwitterMessageExists(string messageid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from TwitterMessage where MessageId = :messid");
                        query.SetParameter("messid", messageid);
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

        public ArrayList getRetweetStatsByProfileId(Guid UserId, string profileId,int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //NHibernate.IQuery query = session.CreateSQLQuery("Select Distinct Count(MessageId) from TwitterMessage where Type=:retweet and EntryDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY) and UserId =:userid and ProfileId=:profileId Group by DATE_FORMAT(EntryDate,'%y-%m-%d') ")
                            NHibernate.IQuery query = session.CreateSQLQuery("Select Distinct Count(MessageId) from TwitterMessage where Type=:retweet and UserId =:userid and ProfileId=:profileId")
                            .SetParameter("userid", UserId)
                        .SetParameter("profileId", profileId)
                        //.SetParameter("retweet", "twt_retweets");
                        .SetParameter("retweet", "twt_usertweets");
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

        public int getRepliesCount(Guid UserId, string profileId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    int repliesCount = 0;
                    try
                    {                        
                        NHibernate.IQuery query = session.CreateSQLQuery("Select Count(MessageId) from TwitterMessage where InReplyToStatusUserId!=null and UserId =:userid and ProfileId=:profileId")
                            .SetParameter("userid", UserId)
                        .SetParameter("profileId", profileId);
                        ArrayList alstFBmsgs = new ArrayList();

                        foreach (var item in query.List())
                        {
                            repliesCount=int.Parse(item.ToString());
                        }
                        return repliesCount;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return repliesCount;
                    }
                }
            }
        }

        public int getRetweetCount(Guid UserId, string profileId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    int repliesCount = 0;
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select Count(MessageId) from TwitterMessage where Type='twt_retweets' and UserId =:userid and ProfileId=:profileId")
                            .SetParameter("userid", UserId)
                        .SetParameter("profileId", profileId);
                        ArrayList alstFBmsgs = new ArrayList();

                        foreach (var item in query.List())
                        {
                            repliesCount = int.Parse(item.ToString());
                        }
                        return repliesCount;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return repliesCount;
                    }
                }
            }
        }

        public int getUserRetweetCount(Guid UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    int repliesCount = 0;
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select Count(MessageId) from TwitterMessage where Type='twt_retweets' and UserId =:userid")
                            .SetParameter("userid", UserId);
                        ArrayList alstFBmsgs = new ArrayList();

                        foreach (var item in query.List())
                        {
                            repliesCount = int.Parse(item.ToString());
                        }
                        return repliesCount;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return repliesCount;
                    }
                }
            }
        }

        public int updateScreenName(string profileid, string screenname)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = session.CreateQuery("Update TwitterMessage set ScreenName =:twtScreenName where ProfileId = :id")
                                   .SetParameter("twtScreenName", screenname)
                                   .SetParameter("id", profileid)
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

        public List<TwitterMessage> getUnreadMessages(Guid UserId, string Profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from TwitterMessage where ReadStatus=0 and UserId= :userid and ProfileId =:profid")
                                                    .SetParameter("userid", UserId)
                                                    .SetParameter("profid", Profileid);
                        List<TwitterMessage> lsttwtMessage = new List<TwitterMessage>();
                        foreach (TwitterMessage item in query.Enumerable<TwitterMessage>())
                        {
                            lsttwtMessage.Add(item);
                        }
                        return lsttwtMessage;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                
                }
            }
        }
        public List<TwitterMessage> getUnreadMessages(string Profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from TwitterMessage where ReadStatus=0 and ProfileId =:profid")
                                                   
                                                    .SetParameter("profid", Profileid);
                        List<TwitterMessage> lsttwtMessage = new List<TwitterMessage>();
                        foreach (TwitterMessage item in query.Enumerable<TwitterMessage>())
                        {
                            lsttwtMessage.Add(item);
                        }
                        return lsttwtMessage;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }

        public int getCountUnreadMessages(Guid UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from TwitterMessage where ReadStatus=0 and UserId= :userid")
                                                    .SetParameter("userid", UserId);
                                                    
                        int i = 0;
                        foreach (TwitterMessage item in query.Enumerable<TwitterMessage>())
                        {
                            i++;
                        }
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

        public int updateMessageStatus(Guid UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = session.CreateQuery("Update TwitterMessage set ReadStatus =1 where UserId = :id")
                                   .SetParameter("id", UserId)
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


        public ArrayList getpostrepliesgraph(Guid userid, int start, int end)
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
                            NHibernate.IQuery query = session.CreateSQLQuery("select ts.DMRecievedCount from TwitterStats ts,SocialProfile sp where ts.FbUserId= sp.ProfileId and sp.UserId=" + userid + " and sp.ProfileType='twitter' limit"+start+","+end+"");
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
                            NHibernate.IQuery query = session.CreateSQLQuery("select ts.DMRecievedCount from TwitterStats ts,SocialProfile sp where ts.FbUserId= sp.ProfileId and sp.UserId=" + userid + " and sp.ProfileType='twitter'");
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


        public int DeleteTwitterMessageByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from TwitterMessage where UserId = :userid")
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