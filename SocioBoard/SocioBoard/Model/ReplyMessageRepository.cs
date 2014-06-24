using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class ReplyMessageRepository
    {
        /// <Add New reply Message>
        /// Add new reply message in Reply Message table 
        /// For Linkedin and google Plus.
        /// </summary>
        /// <param name="ReplyMessage">These parameter containe all Column data for new entry.</param>
        /// <returns></returns>
        public int AddReplyMessage(ReplyMessage ReplyMessage)
        {
            int res = 0;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(ReplyMessage);
                    transaction.Commit();
                    res= 1;
                }
            }

            return res;
        }

    }
}