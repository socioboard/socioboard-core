using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Services
{
    public class WordpressFeedsRepository
    {
        public void addWordpressFeed(Domain.Socioboard.Domain.WordpressFeeds _WordpressFeeds)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Procees action, to save new data.
                    session.Save(_WordpressFeeds);
                    transaction.Commit();
                }//End Trasaction
            }//End session
        }

        public bool checkWordpressFeedExists(string PostId, Guid Userid, string siteid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all data of Wordpress feed by user feed id and User id(Guid).
                        NHibernate.IQuery query = session.CreateQuery("from WordpressFeeds where UserId = :userid and PostId = :PostId and SiteId = : SiteId");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("PostId", PostId);
                        query.SetParameter("SiteId", siteid);
                        var resut = query.UniqueResult();
                        if (resut == null)
                            return false;
                        else
                            return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }

                }//End Transaction
            }//End session
        }

        public List<Domain.Socioboard.Domain.WordpressFeeds> GetAllSiteFeedBySiteId(Guid UserId, string SiteId)
        {
              using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.WordpressFeeds> query = session.CreateQuery("from WordpressFeeds where UserId = :userid and SiteId = : SiteId")
                        .SetParameter("userid", UserId)
                        .SetParameter("SiteId", SiteId)
                        .List<Domain.Socioboard.Domain.WordpressFeeds>().ToList<Domain.Socioboard.Domain.WordpressFeeds>();
                        return query;
                    }
                    catch(Exception ex)
                    {
                        return new List<Domain.Socioboard.Domain.WordpressFeeds>();
                    }
                }
              }
 
        }
    }
}