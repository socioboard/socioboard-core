using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class TwitterFeedRepository:ITwitterFeedRepository
    {

        public void addTwitterFeed(TwitterFeed twtfeed)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(twtfeed);
                    transaction.Commit();
                }
            }
        }

        public int deleteTwitterFeed(TwitterFeed twtfeed)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from TwitterFeed where ProfileId = :twtuserid and UserId = :userid")
                                        .SetParameter("twtuserid", twtfeed.ProfileId)
                                        .SetParameter("userid", twtfeed.UserId);
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

        public int deleteTwitterFeed(string profileid,Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from TwitterFeed where ProfileId = :twtuserid and UserId = :userid")
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

        public int updateTwitterFeed(TwitterFeed twtfeed)
        {
            throw new NotImplementedException();
        }

        public List<TwitterFeed> getAllTwitterFeedOfUsers(Guid UserId, string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<TwitterFeed> lstmsg = session.CreateQuery("from TwitterFeed where UserId = :userid and ProfileId = :profid")
                        .SetParameter("userid", UserId)
                        .SetParameter("profid", profileid)
                        .List<TwitterFeed>()
                        .ToList<TwitterFeed>();


                        //List<TwitterFeed> lstmsg = new List<TwitterFeed>();
                        //foreach (TwitterFeed item in query.Enumerable<TwitterFeed>().OrderByDescending(x=>x.FeedDate))
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

        public bool checkTwitterFeedExists(string Id, Guid Userid, string messageId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from TwitterFeed where UserId = :userid and ProfileId = :Twtuserid and MessageId = :messid");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("Twtuserid", Id);
                        query.SetParameter("messid", messageId);
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

        public List<TwitterFeed> getAllTwitterFeeds(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<TwitterFeed> lstmsg = session.CreateQuery("from TwitterFeed where UserId = :userid")
                        .SetParameter("userid", userid)
                        .List<TwitterFeed>()
                        .ToList<TwitterFeed>();


                        //List<TwitterFeed> lstmsg = new List<TwitterFeed>();
                        //foreach (TwitterFeed item in query.Enumerable<TwitterFeed>().OrderByDescending(x=>x.FeedDate))
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

        public List<TwitterFeed> getTwitterFeedOfUsers(Guid UserId, string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<TwitterFeed> lstmsg = session.CreateQuery("from TwitterFeed where UserId = :userid and ProfileId = :profid and Type ='twt_feeds'")
                        .SetParameter("userid", UserId)
                        .SetParameter("profid", profileid)
                        .List<TwitterFeed>()
                        .ToList<TwitterFeed>();

                        //List<TwitterFeed> lstmsg = new List<TwitterFeed>();
                        //foreach (TwitterFeed item in query.Enumerable<TwitterFeed>().OrderByDescending(x=>x.FeedDate))
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


        public List<TwitterFeed> getTwitterMentionsOfUser(Guid userId, string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<TwitterFeed> lstmsg = session.CreateQuery("from TwitterFeed where UserId = :userid and ProfileId = :profid and Type ='twt_mentions'")
                        .SetParameter("userid", userId)
                        .SetParameter("profid", profileid)
                        .List<TwitterFeed>()
                        .ToList<TwitterFeed>();

                        //List<TwitterFeed> lstmsg = new List<TwitterFeed>();
                        //foreach (TwitterFeed item in query.Enumerable<TwitterFeed>().OrderByDescending(x=>x.FeedDate))
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



        /****************************************************************************************/

        public List<TwitterFeed> getTwitterFeedOfProfile(string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<TwitterFeed> lstmsg = session.CreateQuery("from TwitterFeed where ProfileId = :profid and Type ='twt_feeds'")

                        .SetParameter("profid", profileid)
                        .List<TwitterFeed>()
                        .ToList<TwitterFeed>();

                        //List<TwitterFeed> lstmsg = new List<TwitterFeed>();
                        //foreach (TwitterFeed item in query.Enumerable<TwitterFeed>().OrderByDescending(x => x.FeedDate))
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

        public List<TwitterFeed> getTwitterMentionsOfProfile(string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<TwitterFeed> lstmsg = session.CreateQuery("from TwitterFeed where ProfileId = :profid and Type ='twt_mentions'")
                         .SetParameter("profid", profileid)
                         .List<TwitterFeed>()
                         .ToList<TwitterFeed>();

                        //List<TwitterFeed> lstmsg = new List<TwitterFeed>();
                        //foreach (TwitterFeed item in query.Enumerable<TwitterFeed>().OrderByDescending(x => x.FeedDate))
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


        public bool checkTwitterFeedExists(string messageId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from TwitterFeed where MessageId = :messid");
                        
                        query.SetParameter("messid", messageId);
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

        public int DeleteTwitterFeedByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from TwitterFeed where UserId = :userid")
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