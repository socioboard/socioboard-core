using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class FacebookFeedRepository : IFacebookFeedRepository
    {
        public void addFacebookFeed(FacebookFeed fbfeed)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(fbfeed);
                    transaction.Commit();
                }
            }
        }

        public int deleteFacebookFeed(FacebookFeed fbfeed)
        {
            throw new NotImplementedException();
        }

        public int updateFacebookFeed(FacebookFeed fbfeed)
        {
            throw new NotImplementedException();
        }

        public List<FacebookFeed> getAllFacebookFeedsOfUser(Guid UserId, string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<FacebookFeed> alst = session.CreateQuery("from FacebookFeed where UserId = :userid and ProfileId = :profileId")
                        .SetParameter("userid", UserId)
                        .SetParameter("profileId", profileid)
                        .List<FacebookFeed>()
                        .ToList<FacebookFeed>();


                        #region oldcode
                        //List<FacebookFeed> alst = new List<FacebookFeed>();
                        //foreach (FacebookFeed item in query.Enumerable<FacebookFeed>().OrderByDescending(x => x.FeedDate))
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

        public List<FacebookFeed> getAllFacebookFeedsOfUser(Guid UserId, string profileid, int count)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<FacebookFeed> alst = session.CreateQuery("from FacebookFeed where UserId = :userid and ProfileId = :profileId")
                        .SetParameter("userid", UserId)
                        .SetParameter("profileId", profileid)
                        .SetFirstResult(count)
                        .SetMaxResults(10)
                         .List<FacebookFeed>()
                         .ToList<FacebookFeed>();



                        #region oldcode
                        ////List<FacebookFeed> alst = new List<FacebookFeed>();
                        ////foreach (FacebookFeed item in query.Enumerable<FacebookFeed>().OrderByDescending(x => x.FeedDate))
                        ////{
                        ////    alst.Add(item);
                        ////} 
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


        public bool checkFacebookFeedExists(string feedid, Guid Userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from FacebookFeed where UserId = :userid and FeedId = :msgid");
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
                        NHibernate.IQuery query = session.CreateQuery("delete from FacebookFeed where UserId = :userid and ProfileId = :profileId");
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

        //public int updateFacebookFeedStatus(string fbfeed)
        //{

        //    using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //    {
        //        using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //        { 



        //        }
        //    }

        //}


        public int countUnreadMessages(Guid UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from FacebookFeed where ReadStatus = 0 and UserId=:userid")
                                     .SetParameter("userid", UserId);
                        int i = 0;
                        foreach (var item in query.Enumerable<FacebookFeed>())
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

        public List<FacebookFeed> getUnreadMessages(Guid UserId, string profileId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<FacebookFeed> lstfbfeed = session.CreateQuery("from FacebookFeed where ReadStatus = 0 and UserId=:userid and ProfileId = :profid")
                                     .SetParameter("userid", UserId)
                                     .SetParameter("profid", profileId)
                                     .List<FacebookFeed>()
                                     .ToList<FacebookFeed>();

                        #region Oldcode
                        //List<FacebookFeed> lstfbfeed = new List<FacebookFeed>();
                        //foreach (FacebookFeed item in query.Enumerable<FacebookFeed>())
                        //{
                        //    lstfbfeed.Add(item);
                        //} 
                        #endregion


                        return lstfbfeed;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }
        /***********************************************************************************************************/


        public List<FacebookFeed> getAllFacebookUserFeeds(string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<FacebookFeed> alst = session.CreateQuery("from FacebookFeed where  ProfileId = :profileId")
                        .SetParameter("profileId", profileid)
                        .List<FacebookFeed>()
                        .ToList<FacebookFeed>();

                        #region oldcode
                        //List<FacebookFeed> alst = new List<FacebookFeed>();
                        //foreach (FacebookFeed item in query.Enumerable<FacebookFeed>().OrderByDescending(x => x.FeedDate))
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

        public bool checkFacebookFeedExists(string feedsid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from FacebookFeed where  FeedId = :msgid");
                        query.SetParameter("msgid", feedsid);
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

        public int updateMessageStatus(Guid UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = session.CreateQuery("Update FacebookFeed set ReadStatus =1 where UserId = :id")
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

        public List<FacebookFeed> getAllReadFacebookFeeds(Guid UserId, string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<FacebookFeed> alst = session.CreateQuery("from FacebookFeed where ReadStatus = 1 and UserId = :userid and ProfileId = :profileId")
                       .SetParameter("userid", UserId)
                       .SetParameter("profileId", profileid)
                       .List<FacebookFeed>()
                       .ToList<FacebookFeed>();


                        #region oldcode
                        //List<FacebookFeed> alst = new List<FacebookFeed>();
                        //foreach (FacebookFeed item in query.Enumerable<FacebookFeed>().OrderByDescending(x => x.FeedDate))
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

        public int countInteractions(Guid UserId, string profileid, int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select count(*) from FacebookFeed where ProfileId=:profileid and FeedDate<=DATE_ADD(NOW(),INTERVAL -" + days + " DAY)");
                        query.SetParameter("profileid", profileid);
                        int i = 0;
                        foreach (var item in query.List())
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


        public int DeleteFacebookFeedByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from FacebookFeed where UserId = :userid")
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



        /********************************************************************************************************/
       


    }
}