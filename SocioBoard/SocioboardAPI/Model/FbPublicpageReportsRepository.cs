using Api.Socioboard.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;

namespace Api.Socioboard.Model
{
    public class FbPublicpageReportsRepository
    {
        public void addFacebookPageReports(Domain.Socioboard.Domain.Fbpublicpagereports fbpublicpagereports)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //proceed action, to save data.
                    try
                    {
                        session.Save(fbpublicpagereports);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End session
        }

        public List<Domain.Socioboard.Domain.Fbpublicpagereports> GetReports(string PageId, DateTime startDate, DateTime endDate)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                List<Domain.Socioboard.Domain.Fbpublicpagereports> alst = session.Query<Domain.Socioboard.Domain.Fbpublicpagereports>().Where(x => x.Pageid == PageId && x.Pageid == PageId && x.Date <= startDate.Date && x.Date >= endDate.AddDays(1).Date).ToList();

                return alst;
            }
        }

      

        public bool IsReportExist(DateTime date, string PageId) 
        {
            //DateTime stTime = DateTime.Parse(date.ToString("dd-MM-yyyy"));
            //DateTime enTime = DateTime.Parse(date.AddDays(1).ToString("dd-MM-yyyy"));
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.Fbpublicpagereports> alst = session.Query<Domain.Socioboard.Domain.Fbpublicpagereports>().Where(x => x.Pageid == PageId && x.Pageid == PageId && x.Date >= date.Date && x.Date <= date.Date).ToList();

                        if (alst.Count() > 0)
                        {
                            return true;
                        }
                        else 
                        {
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                      //  Console.WriteLine(ex.StackTrace);
                        return false;
                    }
                }//End Transaction
            }//End session
        }
        
    }
}