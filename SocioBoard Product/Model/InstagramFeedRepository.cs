using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Helper;
using SocioBoard.Domain;

namespace SocioBoard.Model
{
    public class InstagramFeedRepository : IInstagramFeed
    {
        public void addInstagramFeed(InstagramFeed insfeed)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(insfeed);
                    transaction.Commit();
                }
            }
        }

        public int deleteInstagramFeed(InstagramFeed fbfeed)
        {
            throw new NotImplementedException();
        }

        public int updateInstagramFeed(InstagramFeed fbfeed)
        {
            throw new NotImplementedException();
        }

        public List<InstagramFeed> getAllInstagramFeedsOfUser(Guid UserId, string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<InstagramFeed> alst = session.CreateQuery("from InstagramFeed where UserId = :userid and InstagramId = :profileId")
                      .SetParameter("userid", UserId)
                       .SetParameter("profileId", profileid)
                       .List<InstagramFeed>()
                       .ToList<InstagramFeed>();

                        #region oldcode
                        //List<InstagramFeed> alst = new List<InstagramFeed>();
                        //foreach (InstagramFeed item in query.Enumerable())
                        //{
                        //    alst.Add(item);
                        //} 
                        #endregion

                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }

        public bool checkInstagramFeedExists(string feedid, Guid Userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from InstagramFeed where UserId = :userid and FeedId = :msgid");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("msgid", feedid);
                        var resutl = query.UniqueResult();

                        if (resutl == null)
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

        public void deleteAllFeedsOfUser(string fbuserid, Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from InstagramFeed where UserId = :userid and InstagramId = :profileId");
                        query.SetParameter("userid", userid);
                        query.SetParameter("profileId", fbuserid);
                        int isUpdated = query.ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);

                    }

                }
            }
        }

        public List<InstagramFeed> getAllInstagramFeedsOfUser(Guid UserId, string profileid,int count)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<InstagramFeed> alst = session.CreateQuery("from InstagramFeed where UserId = :userid and InstagramId = :profileId")
                        .SetParameter("userid", UserId)
                        .SetParameter("profileId", profileid)
                        .SetFirstResult(count)
                        .SetMaxResults(10)
                        .List<InstagramFeed>()
                        .ToList<InstagramFeed>();

                        #region oldcode
                        //List<InstagramFeed> alst = new List<InstagramFeed>();
                        //foreach (InstagramFeed item in query.Enumerable())
                        //{
                        //    alst.Add(item);
                        //} 
                        #endregion

                        return alst;

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