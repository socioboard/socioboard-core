using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Services
{
    public class WordpressSitesRepository
    {
        public void AddWordpressSites(Domain.Socioboard.Domain.WordpressSites _WordpressSites)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data.
                    session.Save(_WordpressSites);
                    transaction.Commit();

                }// End Using Trasaction
            }// End using session
        }

        public bool IsSiteAllreadyExist(Guid UserId, string WPUserId, string SiteId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to Check if FacebookUser is Exist in database or not by UserId and FbuserId.
                        // And Set the reuired paremeters to find the specific values.
                        List<Domain.Socioboard.Domain.WordpressSites> alst = session.CreateQuery("from WordpressSites where UserId = :userid and WPUserId = :WpUserId and SiteId =: SiteId")
                        .SetParameter("userid", UserId)
                        .SetParameter("WpUserId", WPUserId)
                        .SetParameter("SiteId", SiteId)
                        .List<Domain.Socioboard.Domain.WordpressSites>()
                        .ToList<Domain.Socioboard.Domain.WordpressSites>();
                        if (alst.Count == 0 || alst == null)
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

        public List<Domain.Socioboard.Domain.WordpressSites> GetAllSitesFromWPUserId(Guid UserId, string WPUserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    try
                    {
                        List<Domain.Socioboard.Domain.WordpressSites> lstmsg = session.CreateQuery("from WordpressSites where UserId = :userid and WPUserId = :WPUserId")
                                        .SetParameter("userid", UserId)
                                        .SetParameter("WPUserId", WPUserId).List<Domain.Socioboard.Domain.WordpressSites>().ToList<Domain.Socioboard.Domain.WordpressSites>();
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        return new List<Domain.Socioboard.Domain.WordpressSites>();
                    }

                }
            }
        }

        public Domain.Socioboard.Domain.WordpressSites GetSiteBySiteId(Guid UserId, string SiteId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to Check if FacebookUser is Exist in database or not by UserId and FbuserId.
                        // And Set the reuired paremeters to find the specific values.
                        NHibernate.IQuery alst = session.CreateQuery("from WordpressSites where UserId = :userid and SiteId =: SiteId");
                        alst.SetParameter("userid", UserId);
                        alst.SetParameter("SiteId", SiteId);
                        return (Domain.Socioboard.Domain.WordpressSites)alst.UniqueResult();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return new Domain.Socioboard.Domain.WordpressSites();
                    }

                }//End Transaction
            }//End session
        }
    }
}