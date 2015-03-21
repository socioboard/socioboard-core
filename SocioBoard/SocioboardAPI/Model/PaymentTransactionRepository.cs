using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Socioboard.Helper;
using Domain.Socioboard.Domain;

namespace SocioBoard.Model
{
    public class PaymentTransactionRepository
    {

        /// <SavePayPalTransaction>
        /// Save PayPal Transaction Details
        /// </summary>
        /// <param name="paymentTransaction">Set the payment details in a payment Transaction Class Property and Pass the Object of PaymentTransaction Class.(Domain.paymentTransaction)</param>
        public string SavePayPalTransaction(PaymentTransaction paymentTransaction)
        {
            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        //Proceed action, to save details of paypal transaction.
                        session.Save(paymentTransaction);
                        transaction.Commit();

                        return "Success";
                    }//End Transaction
                }//End Session
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Failure";
            }
        }

        public List<Domain.Socioboard.Domain.PaymentTransaction> GetPaymentDataByUserId(Guid UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.PaymentTransaction> lst = session.CreateQuery("from PaymentTransaction where UserId=: userid ORDER BY PaymentDate DESC")
                                       .SetParameter("userid", UserId)
                                       .List<Domain.Socioboard.Domain.PaymentTransaction>().ToList<Domain.Socioboard.Domain.PaymentTransaction>();
                        return lst;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }


    }
}