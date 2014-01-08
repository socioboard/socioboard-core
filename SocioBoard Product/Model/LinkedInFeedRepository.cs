using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class LinkedInFeedRepository:ILinkedInFeedRepository
    {
        public void addLinkedInFeed(LinkedInFeed lifeed)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(lifeed);
                    transaction.Commit();
                }
            }
        }

        public int deleteLinkedInFeed(LinkedInFeed lifeed)
        {
            throw new NotImplementedException();
        }

        public int updateLinkedInFeed(LinkedInFeed lifeed)
        {
            throw new NotImplementedException();
        }

        public List<LinkedInFeed> getAllLinkedInFeedsOfUser(Guid UserId, string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<LinkedInFeed> alst = session.CreateQuery("from LinkedInFeed where UserId = :userid and ProfileId = :profileId")
                     .SetParameter("userid", UserId)
                       .SetParameter("profileId", profileid)
                       .List<LinkedInFeed>()
                       .ToList<LinkedInFeed>();

                        #region Oldcode
                        //List<LinkedInFeed> alst = new List<LinkedInFeed>();
                        //foreach (LinkedInFeed item in query.Enumerable<LinkedInFeed>().OrderByDescending(x=>x.FeedsDate))
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

        public bool checkLinkedInFeedExists(string feedid, Guid Userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from LinkedInFeed where UserId = :userid and FeedId = :msgid");
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

        public int deleteAllFeedsOfUser(string fbuserid, Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from LinkedInFeed where UserId = :userid and ProfileId = :profileId");
                        query.SetParameter("userid", userid);
                        query.SetParameter("profileId", fbuserid);
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


        /******************************************************************************************************/

        public List<LinkedInFeed> getAllLinkedInFeedsOfProfile(string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<LinkedInFeed> alst = session.CreateQuery("from LinkedInFeed where ProfileId = :profileId")
                        .SetParameter("profileId", profileid)
                        .List<LinkedInFeed>()
                        .ToList<LinkedInFeed>();

                        #region oldcode
                        //List<LinkedInFeed> alst = new List<LinkedInFeed>();
                        //foreach (LinkedInFeed item in query.Enumerable<LinkedInFeed>().OrderByDescending(x => x.FeedsDate))
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