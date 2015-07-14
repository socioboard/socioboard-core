using Api.Socioboard.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;
using NHibernate.Criterion;

namespace Api.Socioboard.Services
{
    public class InboxMessagesRepository
    {
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


    }
}