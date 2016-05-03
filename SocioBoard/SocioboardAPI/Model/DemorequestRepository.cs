using Api.Socioboard.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Model
{
    public class DemorequestRepository
    {
        public bool AddDemoRequest(Domain.Socioboard.Domain.Demorequest demoReq)
        {
            bool IsSuccess = false;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to Save data.
                    try
                    {
                        session.Save(demoReq);
                        transaction.Commit();
                        IsSuccess = true;
                        return IsSuccess;
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.StackTrace);
                        return IsSuccess;
                    }

                }//End Transaction
            }//End Session
        }
    }
}