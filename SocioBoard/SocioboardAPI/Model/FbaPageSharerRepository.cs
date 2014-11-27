using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
using NHibernate.Transform;
using System.Collections;
using System.Data;
using NHibernate.Linq;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Services
{
    public class FbaPageSharerRepository
    {
        /// <addFbaPageSharer>
        /// Add new FbaPageSharer
        /// </summary>
        /// <param name="fbmsg">Set Values in a FbaPageSharer Class Property and Pass the same Object of FbaPageSharer Class.(Domain.FbaPageSharer)</param>
        public void addFbPageSharer(Domain.Socioboard.Domain.FbPageSharer _FbaPageSharer)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data.
                    session.Save(_FbaPageSharer);
                    transaction.Commit();
                }//End Transaction
            }//End session
        }


    }
}