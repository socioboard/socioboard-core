using Api.Socioboard.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;
using NHibernate.Criterion;
using log4net;
namespace Api.Socioboard.Services
{
    public class InboxMessagesRepository
    {
        ILog logger = LogManager.GetLogger(typeof(InboxMessagesRepository));
        public void AddInboxMessages(Domain.Socioboard.Domain.InboxMessages _InboxMessages)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start and open Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to save data.
                    session.Save(_InboxMessages);
                    transaction.Commit();

                }//End Using trasaction
            }//End Using session
        }

        public bool checkInboxMessageExists(Guid userid, string messageid, string type)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from InboxMessages where UserId = :userid and MessageId =:messageid and MessageType=:MessageType");
                        query.SetParameter("userid", userid);
                        query.SetParameter("messageid", messageid);
                        query.SetParameter("MessageType",type);
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
                }//End using transaction
            }//End using session
        }

        public bool checkInboxMessageFriendExists(Guid userid, string fromid,string recipientid, string type)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from InboxMessages where UserId = :userid and FromId =:fromid and RecipientId =:recipientid and MessageType=:MessageType");
                        query.SetParameter("userid", userid);
                        query.SetParameter("fromid", fromid);
                        query.SetParameter("recipientid", recipientid);
                        query.SetParameter("MessageType", type);
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
                }//End using transaction
            }//End using session
        }


        public bool checkInboxMessageRetweetExists(Guid userid, string messageid, string fromid, string recipientid, string type)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from InboxMessages where UserId = :userid and MessageId=:messageid and FromId =:fromid and RecipientId =:recipientid and MessageType=:MessageType");
                        query.SetParameter("userid", userid);
                        query.SetParameter("fromid", fromid);
                        query.SetParameter("recipientid", recipientid);
                        query.SetParameter("messageid", messageid);
                        query.SetParameter("MessageType", type);
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
                }//End using transaction
            }//End using session
        }

        public List<Domain.Socioboard.Domain.InboxMessages> getInboxMessageByGroup(Guid UserId, string profileids, string noOfDataToSkip, string noOfResultsFromTop) 
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        string[] arrsrt = profileids.Split(',');
                        List<Domain.Socioboard.Domain.InboxMessages> results = session.Query<Domain.Socioboard.Domain.InboxMessages>().Where(U => U.UserId.Equals(UserId) && arrsrt.Contains(U.ProfileId)).OrderByDescending(x => x.CreatedTime).Skip(Convert.ToInt32(noOfDataToSkip)).Take(Convert.ToInt32(noOfResultsFromTop)).ToList<Domain.Socioboard.Domain.InboxMessages>();
                        return results;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return new List<Domain.Socioboard.Domain.InboxMessages>();
                    }
                }//End using transaction
            }//End using session
        }

        public List<Domain.Socioboard.Domain.InboxMessages> getInboxMessageByGroupandMessageType(Guid UserId, string profileids, string MessageType, string noOfDataToSkip, string noOfResultsFromTop)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        string[] arrsrt = profileids.Split(',');
                        string[] arrstrtype = MessageType.Split(',');
                        List<Domain.Socioboard.Domain.InboxMessages> results = session.Query<Domain.Socioboard.Domain.InboxMessages>().Where(U => U.UserId.Equals(UserId) && arrsrt.Contains(U.ProfileId) && arrstrtype.Contains(U.MessageType) && U.Status == 0).OrderByDescending(x => x.CreatedTime).Skip(Convert.ToInt32(noOfDataToSkip)).Take(Convert.ToInt32(noOfResultsFromTop)).ToList<Domain.Socioboard.Domain.InboxMessages>();
                        //string query = "from InboxMessages Where ProfileId In(" + profileids + ") and MessageType In(" + MessageType + ") and UserId =: UserId and Status=0 Order By CreatedTime DESC";
                        //List<Domain.Socioboard.Domain.InboxMessages> results = session.CreateQuery(query)
                        //    .SetParameter("UserId", UserId)
                        //    .SetFirstResult(Convert.ToInt32(noOfDataToSkip))
                        //    .SetMaxResults(Convert.ToInt32(noOfResultsFromTop)).List<Domain.Socioboard.Domain.InboxMessages>().ToList();

                        return results;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return new List<Domain.Socioboard.Domain.InboxMessages>();
                    }
                }//End using transaction
            }//End using session
        }

        public Domain.Socioboard.Domain.InboxMessages getInboxMessageByMessageId(Guid UserId, Guid MessageId) 
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery results = session.CreateQuery("from InboxMessages Where UserId =: UserId and Id =: MessageId")
                            .SetParameter("UserId", UserId)
                            .SetParameter("MessageId", MessageId);
                        Domain.Socioboard.Domain.InboxMessages result = (Domain.Socioboard.Domain.InboxMessages)results.UniqueResult();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return new Domain.Socioboard.Domain.InboxMessages();
                    }
                }//End using transaction
            }//End using session
        
        }

        public int GetInboxMessageCount(Guid UserId, string profileids)
        { 
         //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    string[] arrsrt = profileids.Split(',');
                    var results = session.QueryOver<Domain.Socioboard.Domain.InboxMessages>().Where(U => U.UserId == UserId && U.Status==0).AndRestrictionOn(m => m.ProfileId).IsIn(arrsrt).Select(Projections.RowCount()).FutureValue<int>().Value;
                    return Int16.Parse(results.ToString());
                }
                catch (Exception ex)
                {
                    return 0;
                }
                               
            }
        
        }

        public List<Domain.Socioboard.Domain.InboxMessages> GetAllFollowersOfUser(Guid id, string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    List<Domain.Socioboard.Domain.InboxMessages> inboxmessage = session.CreateQuery("from InboxMessages where MessageType=twt_followers and UserId=:userid and ProfileId=:profileid")
                               .SetParameter("userid", id)
                               .SetParameter("profileid", profileid)
                               .List<Domain.Socioboard.Domain.InboxMessages>().ToList();
                    return inboxmessage;
                }
                catch (Exception ex)
                {
                    return new List<Domain.Socioboard.Domain.InboxMessages>();
                }
            }
        }
        //vikash 20-08-2015
        public int GetTwitterMentionCount(Guid UserId, string profileids, int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    string[] arrsrt = profileids.Split(',');
                    var results = session.QueryOver<Domain.Socioboard.Domain.InboxMessages>().Where(U => U.UserId == UserId && U.MessageType == "twt_mention" && U.CreatedTime < DateTime.Now && U.CreatedTime > DateTime.Now.AddDays(-days).Date.AddSeconds(-1)).AndRestrictionOn(m => m.ProfileId).IsIn(arrsrt).Select(Projections.RowCount()).FutureValue<int>().Value;
                    return Int16.Parse(results.ToString());
                }
                catch (Exception ex)
                {
                    logger.Error("GetTwitterMentionCount => " + ex.Message);
                    return 0;
                }
            } 
        }

        public int GetTwitterRetweetCount(Guid UserId, string profileids, int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    string[] arrsrt = profileids.Split(',');
                    var results = session.QueryOver<Domain.Socioboard.Domain.InboxMessages>().Where(U => U.UserId == UserId && U.MessageType == "twt_retweet" && U.CreatedTime < DateTime.Now && U.CreatedTime > DateTime.Now.AddDays(-days).Date.AddSeconds(-1)).AndRestrictionOn(m => m.ProfileId).IsIn(arrsrt).Select(Projections.RowCount()).FutureValue<int>().Value;
                    return Int16.Parse(results.ToString());
                }
                catch (Exception ex)
                {
                    logger.Error("GetTwitterRetweetCount => " + ex.Message);
                    return 0;
                }
            }
        }

        public int GetTwitterFollowersCount(Guid UserId, string profileids, int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    string[] arrsrt = profileids.Split(',');
                    var results = session.QueryOver<Domain.Socioboard.Domain.InboxMessages>().Where(U => U.UserId == UserId && U.MessageType == "twt_followers" && U.Status == 0 && U.CreatedTime < DateTime.Now && U.CreatedTime > DateTime.Now.AddDays(-days).Date.AddSeconds(-1)).AndRestrictionOn(m => m.ProfileId).IsIn(arrsrt).Select(Projections.RowCount()).FutureValue<int>().Value;
                    return Int16.Parse(results.ToString());
                }
                catch (Exception ex)
                {
                    logger.Error("GetTwitterFollowersCount => " + ex.Message);
                    return 0;
                }
            }
        }

        public List<Domain.Socioboard.Domain.InboxMessages> GetTwitterFollowers(Guid UserId, string profileid, int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    List<Domain.Socioboard.Domain.InboxMessages> inboxmessage = session.CreateQuery("from InboxMessages where MessageType=twt_followers and UserId=:userid and ProfileId=:profileid")
                               .SetParameter("userid", UserId)
                               .SetParameter("profileid", profileid)
                               .List<Domain.Socioboard.Domain.InboxMessages>().Where(U => U.Status == 0 && U.CreatedTime < DateTime.Now && U.CreatedTime > DateTime.Now.AddDays(-days).Date.AddSeconds(-1)).ToList();
                    return inboxmessage;
                }
                catch (Exception ex)
                {
                    logger.Error("GetTwitterFollowersCount => " + ex.Message);
                    return new List<Domain.Socioboard.Domain.InboxMessages>();
                }
            }
        }

        public int GetInsagramFollowerCount(Guid UserId, string ProfileIds, int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    string[] arrsrt = ProfileIds.Split(',');
                    var results = session.QueryOver<Domain.Socioboard.Domain.InboxMessages>().Where(U => U.UserId == UserId && U.MessageType == "insta_followers" && U.Status == 0 && U.CreatedTime < DateTime.Now && U.CreatedTime > DateTime.Now.AddDays(-days).Date.AddSeconds(-1)).AndRestrictionOn(m => m.ProfileId).IsIn(arrsrt).Select(Projections.RowCount()).FutureValue<int>().Value;
                    return Int16.Parse(results.ToString());
                }
                catch (Exception ex)
                {
                    logger.Error("GetInstagramFollowersCount => " + ex.Message);
                    return 0;
                }
            }
        }

        public int GetInsagramFollowingCount(Guid UserId, string ProfileIds, int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    string[] arrsrt = ProfileIds.Split(',');
                    var results = session.QueryOver<Domain.Socioboard.Domain.InboxMessages>().Where(U => U.UserId == UserId && U.MessageType == "insta_following" && U.Status == 0 && U.CreatedTime < DateTime.Now && U.CreatedTime > DateTime.Now.AddDays(-days).Date.AddSeconds(-1)).AndRestrictionOn(m => m.ProfileId).IsIn(arrsrt).Select(Projections.RowCount()).FutureValue<int>().Value;
                    return Int16.Parse(results.ToString());
                }
                catch (Exception ex)
                {
                    logger.Error("GetInstagramFollowersCount => " + ex.Message);
                    return 0;
                }
            }
        }

        public int UpdateTwitterFollowerInfo(Domain.Socioboard.Domain.InboxMessages _InboxMessages)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    int i = session.CreateQuery("Update InboxMessages set FromName =: FromName, Message =: Message, FromImageUrl =: FromImageUrl, FollowerCount =: FollowerCount, FollowingCount =: FollowingCount where FromId =: FromId and MessageType=:MessageType")
                        .SetParameter("FromName", _InboxMessages.FromName)
                        .SetParameter("Message", _InboxMessages.Message)
                        .SetParameter("FromImageUrl", _InboxMessages.FromImageUrl)
                        .SetParameter("MessageType", _InboxMessages.MessageType)
                        .SetParameter("FollowerCount", _InboxMessages.FollowerCount)
                        .SetParameter("FollowingCount", _InboxMessages.FollowingCount)
                        .SetParameter("FromId", _InboxMessages.FromId)
                        .ExecuteUpdate();
                    transaction.Commit();
                    return i;
                }
            }
        }


    }
}