using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
using System.Collections;

namespace SocioBoard.Model
{
    public class TwitterDirectMessageRepository : ITwitterDirectMessagesRepository
    {
        public void addNewDirectMessage(TwitterDirectMessages twtDirectMessages)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(twtDirectMessages);
                    transaction.Commit();
                }
            }
        }

        public int deleteDirectMessage(Guid userid,string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from TwitterDirectMessages where SenderId = :twtuserid and UserId = :userid")
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

        public void updateDirectMessage(TwitterDirectMessages twtDirectMessages)
        {
            throw new NotImplementedException();
        }

        public List<TwitterDirectMessages> getAllDirectMessagesByScreenName(string screenName)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    List<TwitterDirectMessages> alstFBAccounts = session.CreateQuery("from TwitterDirectMessages where SenderScreenName = :teamid")
                    .SetParameter("teamid", screenName)
                    .List<TwitterDirectMessages>()
                    .ToList<TwitterDirectMessages>();

                    //List<TwitterDirectMessages> alstFBAccounts = new List<TwitterDirectMessages>();

                    //foreach (TwitterDirectMessages item in query.Enumerable())
                    //{
                    //    alstFBAccounts.Add(item);
                    //}
                    return alstFBAccounts;

                }
            }
        }
        public bool checkExistsDirectMessages(string MessageId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from TwitterDirectMessages where MessageId = :userid ");
                        query.SetParameter("userid", MessageId);

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

        public List<TwitterDirectMessages> getAllDirectMessagesById(string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    List<TwitterDirectMessages> alstFBAccounts = session.CreateQuery("from TwitterDirectMessages where SenderId = :teamid")
                    .SetParameter("teamid", profileid)
                    .List<TwitterDirectMessages>()
                    .ToList<TwitterDirectMessages>();

                    //List<TwitterDirectMessages> alstFBAccounts = new List<TwitterDirectMessages>();

                    //foreach (TwitterDirectMessages item in query.Enumerable())
                    //{
                    //    alstFBAccounts.Add(item);
                    //}

                    return alstFBAccounts;

                }
            }
        }

        public ArrayList gettwtDMRecieveStatsByProfileId(Guid UserId, string profileId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select Distinct Count(MessageId) from TwitterDirectMessages where EntryDate>=DATE_ADD(NOW(),INTERVAL -7 DAY) and UserId =:userid and RecipientId='" + profileId + "' Group by DATE_FORMAT(EntryDate,'%y-%m-%d') ")
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

        public ArrayList gettwtDMSendStatsByProfileId(Guid UserId, string profileId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select Distinct Count(MessageId) from TwitterDirectMessages where EntryDate>=DATE_ADD(NOW(),INTERVAL -7 DAY) and UserId =:userid and SenderId='" + profileId + "' Group by DATE_FORMAT(EntryDate,'%y-%m-%d') ")
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


        public int DeleteTwitterDirectMessagesByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from TwitterDirectMessages where UserId = :userid")
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