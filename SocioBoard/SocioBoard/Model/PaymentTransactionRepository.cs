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
        public void SavePayPalTransaction(PaymentTransaction paymentTransaction)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(paymentTransaction);
                    transaction.Commit();
                }
            }
        }
    }
}