using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class RssReaderRepository : IRssReader
    {
        public void AddRssReader(RssReader rss)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

            }
        }

        public int DeleteRssReader(RssReader rss)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from RssReader where Id = :adsid")
                                        .SetParameter("adsid", rss.Id);
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

        public void UpdateRssReader(RssReader rss)
        {
            throw new NotImplementedException();
        }

        public List<RssReader> getAllRss(Guid Id)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<RssReader> lstRssReader = session.CreateQuery("from RssReader where Id =:userid")
                        .SetParameter("userid", Id)
                        .List<RssReader>()
                        .ToList<RssReader>();

                        //List<RssReader> lstRssReader = new List<RssReader>();
                        //foreach (RssReader item in query.Enumerable<RssReader>())
                        //{
                        //    lstRssReader.Add(item);
                        //}
                        return lstRssReader;
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }
            }
        }

        public List<RssReader> geturlRssFeed(string strUrl)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<RssReader> lstRssReader = session.CreateQuery("from RssReader where FeedsUrl =:strUrl and Status=:status")
                       .SetParameter("strUrl", strUrl)
                       .SetParameter("status", false)
                       .List<RssReader>()
                       .ToList<RssReader>();

                        //List<RssReader> lstRssReader = new List<RssReader>();
                        //foreach (RssReader item in query.Enumerable<RssReader>())
                        //{
                        //    lstRssReader.Add(item);
                        //}
                        return lstRssReader;
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }
            }
        }

        public bool CheckFeedExists(string FeedsUrl, string Message,string PublishedDate)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from RssReader where FeedsUrl =:feedurl and Description =:desc and PublishedDate=:published");
                        query.SetParameter("feedurl", FeedsUrl);
                        query.SetParameter("desc", Message);
                        query.SetParameter("published", PublishedDate);
                        RssFeeds rss = query.UniqueResult<RssFeeds>();
                        if (rss == null)
                        {
                            return false;
                        }
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

        public void UpdateStatus(Guid Id)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = session.CreateQuery("update RssReader set Status= 1 where Id = :id")
                        .SetParameter("id", Id)
                        .ExecuteUpdate();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            }
        }

        public int DeleteRssReaderByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from RssReader where UserId = :userid")
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