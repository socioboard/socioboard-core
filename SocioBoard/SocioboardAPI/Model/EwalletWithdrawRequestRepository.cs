using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Services
{
    public class EwalletWithdrawRequestRepository
    {
        public void Add(Domain.Socioboard.Domain.EwalletWithdrawRequest _EwalletWithdrawRequest)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(_EwalletWithdrawRequest);
                    transaction.Commit();
                }
            }
        }


        public List<Domain.Socioboard.Domain.EwalletWithdrawRequest> GetEwalletWithdraw(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.EwalletWithdrawRequest> query = session.CreateQuery("from EwalletWithdrawRequest where UserId =: userid")
                        .SetParameter("userid", userid).List<Domain.Socioboard.Domain.EwalletWithdrawRequest>().ToList().OrderByDescending(d=>d.RequestDate).ToList();
                        return query;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return new List<Domain.Socioboard.Domain.EwalletWithdrawRequest>();
                    }

                }//End Transaction
            }//End Session
        }

        public List<Domain.Socioboard.Domain.EwalletWithdrawRequest> GetAllEwalletWithdraw()
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.EwalletWithdrawRequest> query = session.CreateQuery("from EwalletWithdrawRequest")
                       .List<Domain.Socioboard.Domain.EwalletWithdrawRequest>().ToList().OrderByDescending(d => d.RequestDate).ToList();
                        return query;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return new List<Domain.Socioboard.Domain.EwalletWithdrawRequest>();
                    }

                }//End Transaction
            }//End Session
        }

        public int UpdatePaymentStatus(Guid Id,int status)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int ret = session.CreateQuery("Update EwalletWithdrawRequest set Status =:status where Id =: id")
                            .SetParameter("status", status)
                            .SetParameter("id", Id)
                            .ExecuteUpdate();
                        transaction.Commit();
                        return ret;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }

                }//End Transaction
            }//En
        }

    }
}