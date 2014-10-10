using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Services
{
    public class FacebookFanPageRepository
    {
        public int getAllFancountDetail(Guid UserId, string profileid, int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select FanpageCount from FacebookFanPageRepository where UserId=:UserId and  ProfilePageId=:profileid and EntryDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY) order by EntryDate desc limit 1");
                        query.SetParameter("profileid", profileid);
                        query.SetParameter("UserId", UserId);
                        int i = 0;
                        foreach (var item in query.List())
                        {
                            i = Convert.ToInt32(item);
                        }
                        return i;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }

                }//End Transaction  
            }//End Session

        }

        public int getAllFancountDetailbeforedays(Guid UserId, string profileid, int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select FanpageCount from FacebookFanPageRepository where UserId=:UserId and  ProfilePageId=:profileid and EntryDate < DATE_ADD(NOW(),INTERVAL -" + days + " DAY) order by EntryDate desc limit 1");
                        query.SetParameter("profileid", profileid);
                        query.SetParameter("UserId", UserId);
                        int i = 0;
                        foreach (var item in query.List())
                        {
                            i = Convert.ToInt32(item);
                        }
                        return i;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }

                }//End Transaction  
            }//End Session

        }

    
     
    }
}