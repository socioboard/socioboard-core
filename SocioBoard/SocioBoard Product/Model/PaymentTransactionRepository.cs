using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class PaymentTransactionRepository
    {

        /// <SavePayPalTransaction>
        /// Save PayPal Transaction Details
        /// </summary>
        /// <param name="paymentTransaction">Set the payment details in a payment Transaction Class Property and Pass the Object of PaymentTransaction Class.(Domain.paymentTransaction)</param>
        public void SavePayPalTransaction(PaymentTransaction paymentTransaction)
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
                }//End Transaction
            }//End Session
        }
    }
}