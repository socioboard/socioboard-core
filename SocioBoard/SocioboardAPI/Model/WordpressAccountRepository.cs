using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Services
{
    public class WordpressAccountRepository
    {
        public void AddWordpressAccount(Domain.Socioboard.Domain.WordpressAccount WordpressAccount)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data.
                    session.Save(WordpressAccount);
                    transaction.Commit();

                }// End Using Trasaction
            }// End using session
        }

        public bool IsProfileAllreadyExist(Guid UserId, string WPUserId)
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
                        List<Domain.Socioboard.Domain.WordpressAccount> alst = session.CreateQuery("from WordpressAccount where UserId = :userid and WpUserId = :WpUserId")
                        .SetParameter("userid", UserId)
                        .SetParameter("WpUserId", WPUserId)
                        .List<Domain.Socioboard.Domain.WordpressAccount>()
                        .ToList<Domain.Socioboard.Domain.WordpressAccount>();
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

        public Domain.Socioboard.Domain.WordpressAccount GetWordpressAccountById(Guid id, string wpid)
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
                        NHibernate.IQuery query = session.CreateQuery("from WordpressAccount where UserId = :userid and WpUserId = :WpUserId");
                        query.SetParameter("userid", id);
                        query.SetParameter("WpUserId", wpid);
                        return (Domain.Socioboard.Domain.WordpressAccount)query.UniqueResult();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;   
                    }

                }//End Transaction
            }//End session
        }

        public List<Domain.Socioboard.Domain.WordpressAccount> GetAllWordpressAccount(Guid UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.WordpressAccount> lstWordpressAccount = session.CreateQuery("from WordpressAccount where UserId=:userid")
                            .SetParameter("userid", UserId).List<Domain.Socioboard.Domain.WordpressAccount>().ToList<Domain.Socioboard.Domain.WordpressAccount>();
                        return lstWordpressAccount;
                    }
                    catch (Exception ex)
                    {
                        return new List<Domain.Socioboard.Domain.WordpressAccount>();
                    }
                }
            }
        }

    }
}