using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
namespace SocioBoard.Model
{
    public class RssFeedsRepository : IRssFeedsRepository
    {
        public void AddRssFeed(RssFeeds rss)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(rss);
                    transaction.Commit();
                }
            }
        }

        public void DeleteRss(RssFeeds rss)
        {
            throw new NotImplementedException();
        }

        public void UpdateRss(RssFeeds rss)
        {
            throw new NotImplementedException();
        }

        public List<RssFeeds> getAllActiveRssFeeds(Guid UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<RssFeeds> lst = session.CreateQuery("from RssFeeds where UserId =:userid and Status = 0")
                        .SetParameter("userid", UserId)
                        .List<RssFeeds>()
                        .ToList<RssFeeds>();

                       //List<RssFeeds> lst = new List<RssFeeds>();
                       //foreach (RssFeeds item in query.Enumerable<RssFeeds>())
                       //{
                       //    lst.Add(item);
                       //}
                       return lst;
                    }
                    catch (Exception ezx)
                    {
                        Console.WriteLine(ezx.StackTrace);
                        return null;
                    }
                
                }
            }
        
        }

        public IEnumerable<RssFeeds> getAllActiveRssFeeds()
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {


                        return session.CreateCriteria(typeof(RssFeeds)).List<RssFeeds>().Where(x => x.Status == false);

                        //NHibernate.IQuery query = session.CreateQuery("from RssFeeds where Status = 0");
                        //List<RssFeeds> lst = new List<RssFeeds>();
                        //foreach (RssFeeds item in query.Enumerable<RssFeeds>())
                        //{
                        //    lst.Add(item);
                        //}
                        //return lst;
                    }
                    catch (Exception ezx)
                    {
                        Console.WriteLine(ezx.StackTrace);
                        return null;
                    }

                }
            }
        }
        public int updateFeedStatus(Guid UserId,string message)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = session.CreateQuery("Update RssFeeds set Status = 1 where UserId=:userid and Message =:message")
                                   .SetParameter("userid",UserId)
                                   .SetParameter("message",message)
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


        /*Toggle must be play/pause**/
        public int updateFeedStatus(string Toggle,Guid Id)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = 0;
                        if (Toggle == "pause")
                        {
                            i = session.CreateQuery("Update RssFeeds set Status = 1 where Id=:userid")
                                       .SetParameter("userid", Id)
                                       .ExecuteUpdate();
                            transaction.Commit();
                           
                        }
                        else if(Toggle == "play")
                        {
                            i = session.CreateQuery("Update RssFeeds set Status = 0 where Id=:userid")
                                       .SetParameter("userid", Id)
                                       .ExecuteUpdate();
                            transaction.Commit();
                        
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

        public int DeleteRssMessage(Guid Id)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from RssFeeds where Id = :userid ");
                        query.SetParameter("userid", Id);
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

        public int DeleteRssFeedsByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from RssFeeds where UserId = :userid")
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